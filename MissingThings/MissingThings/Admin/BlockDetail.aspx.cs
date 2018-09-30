using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class BlockDetail : PageBase_Admin
{
    tblBlock objBlock;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iBlockid = 0;
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

        objBlock = new tblBlock();


        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objBlock.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objBlock.AddNew();
            objBlock.AppCreatedDate = System.DateTime.Now;
            objBlock.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objBlock.AppBlockName = txtBlockName.Text;
        objBlock.AppControlId = txtControlID.Text;
        objBlock.AppContent = ckeBlockContent.Text;
        objBlock.AppIsShowContent = ChkIsShowContent.Checked;

        objBlock.Save();
        iBlockid = objBlock.AppBlockId;
        objBlock = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objBlock = new tblBlock();
            if (objBlock.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtBlockName.Text = objBlock.AppBlockName;
                txtControlID.Text = objBlock.AppControlId;
                ckeBlockContent.Text = objBlock.AppContent;
                ChkIsShowContent.Checked = objBlock.AppIsShowContent;
                if (! (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
                {
                    //divControlID.Visible = false;
                    //divIsShowContent.Visible = false;
                
                    divControlID.Style.Add("display", "none");
                    divIsShowContent.Style.Add("display", "none");
                }
               
            }
            objBlock = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Block.aspx", true);
    }

    private void ResetControls()
    {

        txtBlockName.Text = "";
        ckeBlockContent.Text = "";
        txtControlID.Text = "";
        ChkIsShowContent.Checked = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Block has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Block has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iBlockid.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Block has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Block has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("Block.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Block has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Block has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}