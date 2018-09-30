using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
public partial class _Default : System.Web.UI.Page
{
    HttpCookie httpCookie;
    tblUser objUser = new tblUser();
    tblUserLog objUserLog = new tblUserLog();
    //Dim strHostName As String = System.Net.Dns.GetHostName()

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {

        }
    }


    protected void btnSend_Click(object sender, EventArgs e)
    {

        objUser = new tblUser();
        objUser.Where.AppUserName .Value = txtUserName.Text;
        objUser.Query.Load();
        if (objUser.RowCount > 0)
        {
            clsCommon objCommon = new clsCommon();
            clsEncryption objEncrypt = new clsEncryption();
            string StrBody = "";
            string strSubject = "Password Recovery Request";
            StrBody = objCommon.readFile(Server.MapPath("~/admin/EmailTemplates/ForgetPassword.html"));
            StrBody = StrBody.Replace("`email`", objUser.AppEmail);
            StrBody = StrBody.Replace("`password`", objEncrypt.Decrypt(objUser.AppPassword, appFunctions.strKey.ToString()));
            objCommon.SendMail(objUser.AppEmail, strSubject, StrBody);
            objEncrypt = null;
            objCommon = null;

            txtUserName.Text = "";
            lblMsg.Text = "Your password has been sent to your email address. Please check your email.";
            lblMsg.ForeColor = System.Drawing.Color.Green;
        }
        else
        {
            lblMsg.Text = "Invalid Your User Name.";
            lblMsg.ForeColor = System.Drawing.Color.Red;

        }
        objUser = null;
    }
}