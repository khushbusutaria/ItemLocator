using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using BusinessLayer;

public class PageBase_Admin : PageBase
{
    public string strServerURL = PageBase.GetServerURL() + "/";
    public static string strListLimit = "15";
    private void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            string pageName = Request.Url.PathAndQuery;
            pageName = pageName.Substring(pageName.LastIndexOf("/") + 1);

            if (pageName.Contains("?"))
            {
                pageName = pageName.Substring(0, pageName.LastIndexOf("?"));
            }


            try
            {

                if ((Session[appFunctions.Session.IsSuperAdmin.ToString()] != null))
                {
                    if (Session[appFunctions.Session.IsSuperAdmin.ToString()].ToString() == "True")
                    {
                        HasAdd = true;
                        HasEdit = true;
                        HasDelete = true;
                    }
                    else
                    {
                        SetUpPermission(pageName);
                    }

                }
                else
                {
                    SetUpPermission(pageName);
                }

            }
            catch (Exception ex)
            {
                Response.Redirect("PageNotFound.aspx", true);
            }

        }

        if (Cache["ListLimit"] == null | Cache["ClientSiteUrl"] == null | Cache["FooterText"] == null)
        {
            SetUpSiteSettings();
        }

        if ((Session[appFunctions.Session.RoleID.ToString()] != null))
        {
            if (string.IsNullOrEmpty(Session[appFunctions.Session.UserID.ToString()].ToString()) | Session[appFunctions.Session.UserID.ToString()].ToString() == "0")
            {
                Response.Redirect("Default.aspx", true);
                return;
            }
            else
            {
                strUserName = Session[appFunctions.Session.UserName.ToString()].ToString();
                strUserID = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()].ToString());
            }
        }
        else
        {
            Response.Redirect("Default.aspx", true);
            return;
        }

    }

    public void noIdFoundRedirect(string strPageName)
    {
        Session[appFunctions.Session.ShowMessage.ToString()] = "No Such Id Found";
        Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Error;
        Response.Redirect(strPageName);
    }

    public void SetUpSiteSettings()
    {
        tblSettings objSettings = new tblSettings();
        objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppSiteDefaultListLimit);
        objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppFooterText);
        objSettings.Query.AddResultColumn(tblSettings.ColumnNames.AppClientSiteURL);
        objSettings.Query.Load();

        if (objSettings.RowCount > 0)
        {
            Cache["ListLimit"] = objSettings.AppSiteDefaultListLimit;
            Cache["ClientSiteUrl"] = objSettings.AppClientSiteURL;
            Cache["FooterText"] = objSettings.AppFooterText;
            strListLimit = Cache["ListLimit"].ToString();
        }
        else
        {
            Cache["ListLimit"] = "All";
            Cache["ClientSiteUrl"] = "#";
            Cache["FooterText"] = "";
            strListLimit = "All";
        }

    }

    private void SetUpPermission(string pageName)
    {
        tblTab objTab = new tblTab();
        DataTable objPermissionDt = new DataTable();
        objPermissionDt = objTab.LoadPermissionForTab(pageName, Convert.ToInt32(Session[appFunctions.Session.RoleID.ToString()]));


        if (objPermissionDt.Rows.Count > 0)
        {
            if ((bool)objPermissionDt.Rows[0]["appIsView"] == false)
            {
                Response.Redirect("PageNotFound.aspx", true);
            }
            else
            {
                if ((bool)objPermissionDt.Rows[0]["appIsAdd"] == true)
                {
                    HasAdd = true;
                }

                if ((bool)objPermissionDt.Rows[0]["appIsEdit"] == true)
                {
                    HasEdit = true;
                }

                if ((bool)objPermissionDt.Rows[0]["appIsDelete"] == true)
                {
                    HasDelete = true;
                }
            }

        }
    }
    public PageBase_Admin()
    {
        Load += Page_Load;
    }

}
