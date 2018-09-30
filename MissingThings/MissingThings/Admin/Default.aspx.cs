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
    tblUser objAdminUser = new tblUser();
    tblUserLog objUserLog = new tblUserLog();
    //Dim strHostName As String = System.Net.Dns.GetHostName()

    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            dynamic objEncrypt = new clsEncryption();



            if ((Request.Cookies.Get("JetAPIAdminUsername") != null))
            {
                if (!string.IsNullOrEmpty(Request.Cookies.Get("JetAPIAdminUsername").Value))
                {
                    txtUserName.Text = objEncrypt.Decrypt(Request.Cookies.Get("JetAPIAdminUsername").Value, appFunctions.strKey);

                    if ((Request.Cookies.Get("JetAPIAdminPassword") != null))
                    {
                        if (!string.IsNullOrEmpty(Request.Cookies.Get("JetAPIAdminPassword").Value))
                        {
                            txtPassword.Attributes.Add("value", objEncrypt.Decrypt(Request.Cookies.Get("JetAPIAdminPassword").Value, appFunctions.strKey));
                        }
                    }

                    chkRemeber.Checked = true;
                }

            }

        }
    }

    protected void btnLogIn_Click(object sender, System.EventArgs e)
    {
        clsEncryption objEncrypt = new clsEncryption();
        objAdminUser = new tblUser();
        objAdminUser.Where.AppUserName .Value = txtUserName.Text;

        try
        {
            objAdminUser.Where.AppPassword.Value = objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey.ToString());

        }
        catch (Exception ex)
        {

            lblMsg.Text = "Invalid Username or Password";
            SaveLog(false);
            return;
        }

        objAdminUser.Query.Load();


        if (objAdminUser.RowCount > 0)
        {
            if (objAdminUser.AppIsActive)
            {

                if (chkRemeber.Checked)
                {
                    httpCookie = new HttpCookie("JetAPIAdminUsername", objEncrypt.Encrypt(txtUserName.Text, appFunctions.strKey));
                    httpCookie.Expires = DateTime.Today.AddDays(10);
                    Response.Cookies.Add(httpCookie);

                    httpCookie = new HttpCookie("JetAPIAdminPassword", objEncrypt.Encrypt(txtPassword.Text, appFunctions.strKey));
                    httpCookie.Expires = DateTime.Today.AddDays(10);
                    Response.Cookies.Add(httpCookie);


                }
                else
                {
                    httpCookie = new HttpCookie("JetAPIAdminUsername", "");
                    httpCookie.Expires = DateTime.Today.AddDays(0);
                    Response.Cookies.Add(httpCookie);

                    httpCookie = new HttpCookie("JetAPIAdminPassword", "");
                    httpCookie.Expires = DateTime.Today.AddDays(0);
                    Response.Cookies.Add(httpCookie);

                }

                Session[appFunctions.Session.UserID.ToString()] = objAdminUser.AppUserId;
                Session[appFunctions.Session.RoleID.ToString()] = objAdminUser.AppRoleId;
                Session[appFunctions.Session.UserName.ToString()] = objAdminUser.AppUserName;
                Session[appFunctions.Session.EmployeeName.ToString()] = objAdminUser.AppFullName;
                Session[appFunctions.Session.IsSuperAdmin.ToString()] = objAdminUser.AppIsSuperAdmin;

               
                objAdminUser.AppLastLoginTime = DateTime.Now;
                objAdminUser.Save();
                SaveLog(true);
                
                if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == false)
                {
                    tblPermission objPermission = new tblPermission();

                    bool hasDashboardPermission = objPermission.CheckDeshbordTab(Session[appFunctions.Session.RoleID.ToString()].ToString());  //objPermission.CheckDeshbordTab(Session[appFunctions.Session.RoleID.ToString()].ToString());
                    if (hasDashboardPermission)
                    {
                        Response.Redirect("Dashboard.aspx");
                    }
                    else
                    {
                        Response.Redirect("UserPanel.aspx");
                    }
                }
                else
                {
                    Response.Redirect("Dashboard.aspx");
                }



            }
            else
            {
                lblMsg.Text = "Your account is disabled, Contact to administrator.";
                SaveLog(false);
            }
        }
        else
        {
            lblMsg.Text = "Invalid Username or Password";
            SaveLog(false);
        }


    }




    protected void lnkForgotPassword1_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("ForgotPassword.aspx", true);
    }

    //protected void btnCancel_Click(object sender, System.EventArgs e)
    //{
    //    Response.Redirect("Default.aspx");
    //}


    protected void SaveLog(bool IsSuccess)
    {
        objUserLog = new tblUserLog();
        objUserLog.AddNew();

        if (Session[appFunctions.Session.UserID.ToString()] != null)
            objUserLog.AppUserID = (int)Session[appFunctions.Session.UserID.ToString()];

        objUserLog.AppLogInDate = DateTime.Now;
        objUserLog.AppIPAddress = Request.UserHostAddress.ToString();
        objUserLog.AppIsSuccess = IsSuccess;
        objUserLog.AppLogOutDate = DateTime.Now;
        objUserLog.Save();
        Session[appFunctions.Session.UserLogID.ToString()] = objUserLog.AppUserLogID;
        objUserLog = null;

    }
}