using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Security.Permissions;
using System.Xml;

public partial class SettingsDetail : PageBase_Admin
{
    tblSettings objSettings;
    Byte[] byteArray;
    clsEncryption objEncrypt;
    clsCommon objCommon;
    int intPKID = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            Page.Form.Attributes.Add("enctype", "multipart/form-data");

            objCommon = new clsCommon();
            objCommon.FillRecordPerPage(ref ddlPerMenu);
            objCommon = null;

            //chkDataBaseSettings.Attributes.Add("onclick", "javascript:checkLink();");

            SetValuesToControl();
        }
    }

    private void SetValuesToControl()
    {
        objSettings = new tblSettings();
        objEncrypt = new clsEncryption();
        objSettings.LoadAll();
        if (objSettings.RowCount > 0)
        {
            try
            {
                txtSiteName.Text = objSettings.AppSiteName;
                txtFooter.Text = objSettings.AppFooterText;
                chkSiteOffline.Checked = objSettings.AppIsSiteOffline;
                txtSiteOfflineMessage.Text = objSettings.AppSiteOfflineMessage;
                txtSiteTagLine.Text = objSettings.AppSiteTagLine;
                ddlPerMenu.SelectedValue = objSettings.AppSiteDefaultListLimit;
                txtPathToFolder.Text = objSettings.AppPathToFolder;
                //txtDatasourceName.Text = objSettings.AppDatasource;
                //txtDatasourceUserName.Text = objSettings.AppDatasourceUserName;
                //txtDataBaseName.Text = objSettings.AppDatabaseName;
                txtSMTP.Text = objSettings.AppSMTP;
                txtSiteEmail.Text = objSettings.AppSiteEmail;
                txtPortNumber.Text = objSettings.AppPortNumber;
                txtClientSiteUrl.Text = objSettings.AppClientSiteURL;
                txtRecepientEmail.Text = objSettings.AppRecepientEmail;

                if (objSettings.AppSiteOfflineImage.ToString() != "" && objSettings.AppSiteOfflineImage.ToString() != null)
                {
                    imgOfflineImage.ImageUrl = objSettings.AppSiteOfflineImage;
                }
                if (objSettings.AppSiteLogo.ToString() != "" && objSettings.AppSiteLogo.ToString() != null)
                {
                    ImgLogo.ImageUrl = objSettings.AppSiteLogo;
                }
                if (objSettings.AppSiteFavicon.ToString() != "" && objSettings.AppSiteFavicon.ToString() != null)
                {
                    ImgFavicon.ImageUrl = objSettings.AppSiteFavicon;
                }
                //txtDatasourcePassword.Attributes.Add("value", objSettings.AppDatasourcePassword);
                txtEmailPassword.Attributes.Add("value", objEncrypt.Decrypt(objSettings.AppEmailPassword, appFunctions.strKey));
            }
            catch (Exception e)
            {
            }
        }
        objSettings = null;
    }


    protected void btnSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            DInfo.ShowMessage(" Global Settings Data Save Sucessfully.", Enums.MessageType.Successfull);
        }
        else
        {
            DInfo.ShowMessage(" Global Settings Data Not Save.", Enums.MessageType.Error);
        }
      
    }

    private bool SaveData()
    {
        objSettings = new tblSettings();
        objEncrypt = new clsEncryption();
        objSettings.Query.Top = 1;
        objSettings.Query.Load();
        if (objSettings.RowCount == 0)
        {
            objSettings = new tblSettings();
            objSettings.AddNew();
            objSettings.AppCreatedDate = DateTime.Now;
            objSettings.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();

        }
        objSettings.AppSiteName = txtSiteName.Text;
        objSettings.AppFooterText = txtFooter.Text;
        objSettings.AppIsSiteOffline = chkSiteOffline.Checked;
        objSettings.AppSiteOfflineMessage = txtSiteOfflineMessage.Text;
        objSettings.AppSiteTagLine = txtSiteTagLine.Text;
        objSettings.AppSiteDefaultListLimit = ddlPerMenu.SelectedValue;
        objSettings.AppPathToFolder = txtPathToFolder.Text;

        if (txtClientSiteUrl.Text.Contains("http://"))
        {
            objSettings.AppClientSiteURL = txtClientSiteUrl.Text;
        }
        else
        {
            objSettings.AppClientSiteURL = "http://" + txtClientSiteUrl.Text;
        }

        objSettings.AppEmailPassword = objEncrypt.Encrypt(txtEmailPassword.Text, appFunctions.strKey);
        objSettings.AppSMTP = txtSMTP.Text;
        objSettings.AppSiteEmail = txtSiteEmail.Text;
        objSettings.AppRecepientEmail = txtRecepientEmail.Text;
        objSettings.AppPortNumber = txtPortNumber.Text;
        string strFilePath = Server.MapPath("~/Uploads/ConfigurationsSetting");
        if (FileUploadOfflineImage.HasFile)
        {
            FileUploadOfflineImage.SaveAs( strFilePath + "/Offline.jpg");
            objSettings.AppSiteOfflineImage = "Uploads/ConfigurationsSetting/Offline.jpg";
        }

        if (FileUploadLogo.HasFile)
        {
            FileUploadLogo.SaveAs(strFilePath +  "/Logo.jpg");
            objSettings.AppSiteLogo = "Uploads/ConfigurationsSetting/Logo.jpg";
        }

        if (FileUploadFavicon.HasFile)
        {
            FileUploadFavicon.SaveAs(strFilePath + "/Favicon.ico");
            objSettings.AppSiteFavicon = "Uploads/ConfigurationsSetting/Favicon.ico";
        }


        //if (chkDataBaseSettings.Checked)
        //{
        //    SaveDataBaseSettings();
        //}

        objSettings.Save();
        SetUpSiteSettings();

        
        objSettings = null;
        return true;
    }
    //private void SaveDataBaseSettings()
    //{
    //    objSettings.AppDatasource = txtDatasourceName.Text;
    //    objSettings.AppDatasourceUserName = txtDatasourceUserName.Text;
    //    objSettings.AppDatabaseName = txtDataBaseName.Text;

    //    if (txtDatasourcePassword.Text != "")
    //    {
    //        objSettings.AppDatasourcePassword = txtDatasourcePassword.Text;
    //        objSettings.AppIsUseIntegratedSecurity = true;
    //    }
    //    else
    //    {
    //        objSettings.AppIsUseIntegratedSecurity = false;
    //    }

    //    try
    //    {
    //        XmlDocument xmlConfigDoc = new XmlDocument();
    //        xmlConfigDoc.Load(Server.MapPath("../web.config"));
    //        XmlNode appSettingsNode = xmlConfigDoc.SelectSingleNode("configuration/appSettings");
    //        //XmlNode childNode;
    //        string ConnectionString = null;
    //        foreach (XmlNode childNode in appSettingsNode)
    //        {
    //            if (childNode.NodeType == XmlNodeType.Comment)
    //            {
    //                if (childNode.Attributes["key"].Value.ToLower() == "dbconnection")
    //                {
    //                    ConnectionString = "Data Source=" + objSettings.AppDatasource + ";";
    //                    ConnectionString += "Initial Catalog=" + objSettings.AppDatabaseName + ";";
    //                    ConnectionString += "User ID=" + objSettings.AppDatasourceUserName + ";";
    //                    if (objSettings.AppDatasourcePassword != "")
    //                    {
    //                        ConnectionString += "Password=" + objSettings.AppDatasourcePassword + ";";
    //                    }
    //                    else
    //                    {
    //                        ConnectionString += "Integrated Security=true;";
    //                    }
    //                    childNode.Attributes["value"].Value = ConnectionString;
    //                }
    //            }
    //        }
    //        xmlConfigDoc.Save(Server.MapPath("../web.config"));
    //        SaveDataBaseSettings();

    //    }
    //    catch (Exception e)
    //    {
    //        Session[appFunctions.Session.ShowMessage.ToString()] = "You currently dont have permission to write database settings";
    //        Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Error;

    
    //    }

    //    objSettings.Save();
    //}
}