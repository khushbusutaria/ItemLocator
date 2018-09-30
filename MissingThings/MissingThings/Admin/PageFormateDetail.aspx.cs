using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class PageFormateDetail : PageBase_Admin
{
    tblPageFormat objPageFormat;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iPageFormateid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;

            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
                SetValuesToControls();
            }
        }
    }



    private bool SaveData()
    {
        objPageFormat = new tblPageFormat();
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objPageFormat.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objPageFormat.AddNew();
            objPageFormat.AppCreatedDate = System.DateTime.Now;
            objPageFormat.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objPageFormat.AppPageFormatName = txtPageFormatName.Text;
        objPageFormat.AppPageName = txtPageName.Text;
        objPageFormat.AppDescription = txtDescription.Text;
        objPageFormat.AppIsActive = chkIsActive.Checked;

        if (FileUploadImg.HasFile)
        {
            objClsCommon = new clsCommon();
            string strError = "";
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objClsCommon.FileUpload_Images(FileUploadImg.PostedFile, txtPageFormatName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/PageFormatImages/", ref strError, 0, objPageFormat.s_AppImage);
            if (strError == "")
            {
                objPageFormat.AppImage = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }

        }


        objPageFormat.Save();
        iPageFormateid = objPageFormat.AppPageFormatId;
        objPageFormat = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objPageFormat = new tblPageFormat();
            if (objPageFormat.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtPageFormatName.Text = objPageFormat.AppPageFormatName;
                txtDescription.Text = objPageFormat.AppDescription;
                txtPageName.Text = objPageFormat.AppPageName;
                chkIsActive.Checked = objPageFormat.AppIsActive;

                if (objPageFormat.AppImage != "")
                {
                    imgCurrent.ImageUrl = objPageFormat.AppImage;
                }
            }
            objPageFormat = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("PageFormate.aspx", true);
    }

    private void ResetControls()
    {
        txtPageFormatName.Text = "";
        txtDescription.Text = "";
        txtPageName.Text = "";
        chkIsActive.Checked = true;
        imgCurrent.ImageUrl = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Page Formate has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Page Formate has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iPageFormateid.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Page Formate has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Page Formate has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("PageFormate.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Page Formate has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Page Formate has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}