using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;

public partial class MyProfile : PageBase_Admin
{
    tblUser objUser;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

            if (Session[appFunctions.Session.UserID.ToString()] != null)
            {
                hdnPKID.Value = Convert.ToString(Session[appFunctions.Session.UserID.ToString()]);
                SetValuesToControls();
            }
            lnkbtnRemovePhoto.Attributes.Add("onclick", "return confirm('Are you sure to delete?');");

        }
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objUser = new tblUser();
            if (objUser.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {

                txtFullName.Text = objUser.AppFullName;
                txtMobileNo.Text = objUser.AppMobile;
                txtEmailAddress.Text = objUser.AppEmail;
                txtAddress.Text = objUser.AppAddress;
                imgUserPhoto.ImageUrl = objUser.s_AppPhoto;
            }
            objUser = null;
        }
    }

    private bool SaveData()
    {
        objClsCommon = new clsCommon();
        objUser = new tblUser();

        if (objClsCommon.IsRecordExists("tblUser", tblUser.ColumnNames.AppEmail, tblUser.ColumnNames.AppUserId, txtEmailAddress.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("Email Address is already exists", Enums.MessageType.Warning);
            return false;
        }

        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objUser.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }


        objUser.AppFullName = txtFullName.Text;
        objUser.AppMobile = txtMobileNo.Text;
        objUser.AppEmail = txtEmailAddress.Text;
        objUser.AppAddress = txtAddress.Text;
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
        objUser.Save();
        hdnPKID.Value = objUser.AppUserId.ToString();
        objUser = null;
        return true;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {

            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Your Profile has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Your Profile has been updated successfully", Enums.MessageType.Successfull);
            }
            SetValuesToControls();
        }
    }
    protected void lnkbtnRemovePhoto_Click(object sender, EventArgs e)
    {

        objUser = new tblUser();
        if (objUser.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
        {

            objUser.AppPhoto = "";
            objUser.Save();
        }

        objUser = null;
    }
}