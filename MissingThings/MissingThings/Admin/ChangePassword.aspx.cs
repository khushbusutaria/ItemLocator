using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class ChangePassword : PageBase_Admin
{
    tblUser objUser;

    protected void Page_Load1(object sender, System.EventArgs e)
    {
    }

    protected void btnSaveAndClose_Click(object sender, System.EventArgs e)
    {
        if (SaveData())
        {
            DInfo.ShowMessage("Password has been updated successfully", Enums.MessageType.Successfull);
        }
    }

    private bool SaveData()
    {
        objUser = new tblUser();
        objUser.LoadByPrimaryKey((int)Session[appFunctions.Session.UserID.ToString()]);
        objEncrypt = new clsEncryption();
        if (string.Compare(objEncrypt.Decrypt(objUser.AppPassword, appFunctions.strKey), txtOldPassword.Text) == 0)
        {
            objUser.AppPassword = objEncrypt.Encrypt(txtNewPassword.Text, appFunctions.strKey);
            objUser.Save();
            return true;
        }

        DInfo.ShowMessage("Old Password is incorrect", Enums.MessageType.Error);
        return false;
    }
}