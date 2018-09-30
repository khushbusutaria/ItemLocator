using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class UserDetail : PageBase_Admin
{
    tblUser objUser;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iUserid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            
            objClsCommon = new clsCommon();
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
            {
                objClsCommon.FillDropDownList(ddlRoleName, "tblRole", tblRole.ColumnNames.AppRoleName, tblRole.ColumnNames.AppRoleID, "--Select Role--", tblRole.ColumnNames.AppRoleID, appFunctions.Enum_SortOrderBy.Asc);
            }
            else
            {
                objClsCommon.FillDropDownList(ddlRoleName, "tblRole", tblRole.ColumnNames.AppRoleName, tblRole.ColumnNames.AppRoleID, "--Select Role--", tblRole.ColumnNames.AppRoleID, appFunctions.Enum_SortOrderBy.Asc, tblRole.ColumnNames.AppCreatedBy + "=" + Session[appFunctions.Session.UserID.ToString()].ToString());
            }
            objClsCommon = null;
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
        objClsCommon = new clsCommon();
        objUser = new tblUser();

        if (objClsCommon.IsRecordExists("tblUser", tblUser.ColumnNames.AppUserName, tblUser.ColumnNames.AppUserId, txtUserName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("User Name is already exists", Enums.MessageType.Warning);
            return false;
        }

        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objUser.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objUser.AddNew();
            objUser.AppCreatedDate = System.DateTime.Now;
            objUser.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objUser.s_AppUserName = txtUserName.Text;
        objEncrypt = new clsEncryption();
        objUser.AppPassword = objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey);
        objEncrypt = null;
        objUser.s_AppRoleId = ddlRoleName.SelectedValue;

        objUser.AppFullName = txtFullName.Text;
        objUser.AppEmail = txtEmailAddress.Text;
        objUser.AppMobile = txtMobileNo.Text;
        objUser.AppAddress = txtAddress.Text;
        objUser.AppIsActive = chkIsActive.Checked;
        objUser.AppIsSuperAdmin = Convert.ToBoolean(0);

        if (FileUploadImg.HasFile)
        {
            objClsCommon = new clsCommon();
            string strError = "";
            string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
            string strPath = objClsCommon.FileUpload_Images(FileUploadImg.PostedFile, txtFullName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/User/", ref strError, 0, objUser.s_AppPhoto);
            if (strError == "")
            {
                objUser.AppPhoto = strPath;
            }
            else
            {
                DInfo.ShowMessage(strError, Enums.MessageType.Error);
                return false;
            }

        }
        objUser.AppDescription = txtDescription.Text;

        objUser.Save();
        iUserid = objUser.AppUserId;
        objUser = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objUser = new tblUser();
            if (objUser.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtUserName.Text = objUser.AppUserName;
                objEncrypt = new clsEncryption();
                txtPassword.Attributes.Add("value", objEncrypt.Decrypt(objUser.AppPassword, appFunctions.strKey));
                ddlRoleName.SelectedValue = objUser.s_AppRoleId;

                txtFullName.Text = objUser.AppFullName;
                txtMobileNo.Text = objUser.AppMobile;
                txtEmailAddress.Text = objUser.AppEmail;
                txtAddress.Text = objUser.AppAddress;
                chkIsActive.Checked = objUser.AppIsActive;
                imgUserPhoto.ImageUrl = objUser.s_AppPhoto;
                txtDescription.Text = objUser.AppDescription;
            }
            objUser = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("User.aspx", true);
    }

    private void ResetControls()
    {

        txtUserName.Text = "";
        txtPassword.Attributes.Add("value", "");
        ddlRoleName.SelectedIndex = 0;
        txtFullName.Text = "";
        txtMobileNo.Text = "";
        txtEmailAddress.Text = "";
        txtAddress.Text = "";
        chkIsActive.Checked = true;
        imgUserPhoto.ImageUrl = "";
        txtDescription.Text = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("User has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("User has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iUserid.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "User has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "User has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("User.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("User has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("User has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}