using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using BusinessLayer;

public partial class Main : System.Web.UI.MasterPage
{
    public string menuString;
    //'Public menuString As String
    //'Public menuString As String
    tblMenuItem objMenuItems = new tblMenuItem();
    tblMenuType objMenuTypes = new tblMenuType();
    clsEncryption objEncrypt = new clsEncryption();
    tblMenuItem objMenuItem = new tblMenuItem();

    tblMenuType objMenuType = new tblMenuType();
    bool hasTabAdd = false;

    bool hasTabEdit = false;
    DataTable objPermissionDT;
    DataTable objMenuItemDT = new DataTable();
    DataTable objMenuTypeDT = new DataTable();

    tblRole objRole;

    protected void Page_Load(object sender, EventArgs e)
    {
        tblUser objUser = new tblUser();
        if (!IsPostBack)
        {
            SetMenu();
            SetSiteDetail();

            lblLoginUser.Text = Session[appFunctions.Session.UserName.ToString()].ToString();
            if (Session[appFunctions.Session.UserID.ToString()] != null)
            {

                objUser = new BusinessLayer.tblUser();
                if (objUser.LoadByPrimaryKey(Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()])))
                {

                    if (objUser.s_AppPhoto != "")
                    {
                        imgUserPhoto.Src = objUser.AppPhoto;
                    }
                    else
                    {
                        imgUserPhoto.Src = "Images/find_user.png";
                    }
                }
                else
                {
                    imgUserPhoto.Src = "Images/find_user.png";
                }
                objUser = null;

            }
        }
    }

    public void SetMenu()
    {
        tblPermission objPermisssion = new tblPermission();
        //Change for manu and sub menu load

        DataTable DtTab = new DataTable();
        objRole = new tblRole();

        DtTab = objRole.LoadPermissionTabMenu(Session[appFunctions.Session.RoleID.ToString()].ToString(), 0.ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);

        for (int i = 0; i <= DtTab.Rows.Count - 1; i++)
        {

            if ((bool)DtTab.Rows[i]["AppIsMenu"])
            {
                menuString += "<li><a href='#'>" + DtTab.Rows[i]["AppTabName"];
                objMenuType = new tblMenuType();
                DataTable menutypeDt = new DataTable();
                menutypeDt = objMenuType.LoadMenuTypes();

                menuString += "</a> ";
                //loadmenuChild(menutypeDt, 2, "0", true);
                menuString += "<ul>";
                for (int j = 0; j <= menutypeDt.Rows.Count - 1; j++)
                {
                    menuString += "<li><a href='MenuItems.aspx?ID=" + objEncrypt.Encrypt(menutypeDt.Rows[j]["appMenuTypeId"].ToString(), appFunctions.strKey) + "&type=mtype'>" + menutypeDt.Rows[j]["appMenuTypeName"].ToString();
                    menuString += "</a>";
                    menuString += "</li>";

                }

                menuString += "</ul>";
                menuString += "</li>";
            }
            else
            {
                DataTable DtChildTab = new DataTable();
                objRole = new tblRole();

                DtChildTab = objRole.LoadPermissionTabMenu(Session[appFunctions.Session.RoleID.ToString()].ToString(), DtTab.Rows[i]["AppTabID"].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);

                menuString += "<li><a href='" + DtTab.Rows[i]["appWebPageName"] + "'>" + DtTab.Rows[i]["AppTabName"];

                menuString += "</a> ";

                loadchildmenu(DtChildTab, 2);

                //if ((bool)DtTab.Rows[i]["AppIsMenu"] == true)
                //{

                //    loadchildmenu(DtTab.Rows(i)("AppTabID").ToString, DtTab.Rows(i)("AppIsMenu"), DtTab.Rows(i)("AppIsTabAdd"), DtTab.Rows(i)("AppIsTabEdit"));
                //}
                //else
                //{
                //    menuString += "<li><a class='menuLink' id='" + DtTab.Rows(i)("AppTabID").ToString + "' href='" + DtTab.Rows(i)("appWebPageName") + "'>" + DtTab.Rows(i)("AppTabName") + "</a>";
                //    loadchildmenu(DtTab.Rows(i)("AppTabID").ToString, DtTab.Rows(i)("AppIsMenu"), DtTab.Rows(i)("AppIsTabAdd"), DtTab.Rows(i)("AppIsTabEdit"));
                //}
                menuString += "</li>";
            }
        }

        litMainMenu.Text = menuString;
    }

    public void loadchildmenu(DataTable DtChildTab, int intLevel)
    {

        if (DtChildTab.Rows.Count > 0)
        {
            if (intLevel == 2)
            {
                menuString += "<ul>";
            }
            else if (intLevel == 3)
            {
                menuString += "<ul>";
            }

            for (int i = 0; i <= DtChildTab.Rows.Count - 1; i++)
            {
                //  If DtTab.Rows(i)("appParentId") <> 0 And DtTab.Rows(i)("appIsAdd").ToString = "False" Then

                objRole = new tblRole();
                DataTable dtSubChildTab = objRole.LoadPermissionTabMenu(Session[appFunctions.Session.RoleID.ToString()].ToString(), DtChildTab.Rows[i]["AppTabID"].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);

                menuString += "<li><a href='" + DtChildTab.Rows[i]["appWebPageName"].ToString() + "'>" + DtChildTab.Rows[i]["AppTabName"].ToString();

                menuString += "</a>";
                loadchildmenu(dtSubChildTab, intLevel + 1);

                menuString += "</li>";
                // End If
            }
            menuString += "</ul>";
        }

    }

    //public void loadmenuChild(DataTable DtChildTab, int intLevel,string  strPrentId,bool IsTopMenu)
    //{

    //    if (DtChildTab.Rows.Count > 0)
    //    {
    //        if (intLevel == 2)
    //        {
    //            menuString += "<ul class=\"nav nav-second-level\">";
    //        }
    //        else if (intLevel == 3)
    //        {
    //            menuString += "<ul class=\"nav nav-third-level\">";
    //        }

    //        for (int i = 0; i <= DtChildTab.Rows.Count - 1; i++)
    //        {
    //            //  If DtTab.Rows(i)("appParentId") <> 0 And DtTab.Rows(i)("appIsAdd").ToString = "False" Then
    //            if (IsTopMenu)
    //            {
    //                menuString += "<li><a href='MenuItems.aspx?ID=" + objEncrypt.Encrypt(DtChildTab.Rows[i]["appMenuTypeId"].ToString(), appFunctions.strKey) + "&type=mtype'>" + DtChildTab.Rows[i]["appMenuTypeName"].ToString();
    //            }
    //            else
    //            {
    //                menuString += "<li><a href='MenuItems.aspx?ID=" + objEncrypt.Encrypt(DtChildTab.Rows[i]["appMenuItemId"].ToString(), appFunctions.strKey) + "'>" + DtChildTab.Rows[i]["appMenuItem"].ToString();

    //            }
    //             objMenuType = new tblMenuType();

    //             DataTable DTsubchiledMenu = objMenuItem.LoadMenuItems(DtChildTab.Rows[i]["appMenuTypeId"].ToString(), strPrentId);
    //            if (DTsubchiledMenu.Rows.Count > 0)
    //            {
    //                menuString += " <span class=\"fa arrow\"></span> ";
    //            }

    //            menuString += "</a>";
    //            loadmenuChild(DTsubchiledMenu, 3, DtChildTab.Rows[i]["appMenuTypeId"].ToString(),false);

    //            menuString += "</li>";
    //            // End If
    //        }
    //        menuString += "</ul>";
    //    }

    //}

    //public void loadSubChildMenu(string menutypeID, int parentId, bool IsAdd, bool IsEdit)
    //{
    //    DataTable DTsubchiledMenu = new DataTable();
    //    tblPermission objPermission = new tblPermission();
    //    DataTable dt = new DataTable();
    //    if (Session(appFunctions.Session.IsSuperAdmin.ToString) == false)
    //    {
    //        dt = objPermission.SelecteMenusubTabPermission(Session(appFunctions.Session.RoleID.ToString));
    //        IsAdd = dt.Rows(0)("AppIsAdd");
    //        IsEdit = dt.Rows(0)("AppIsEdit");
    //    }


    //    tblMenuItem objMenuItem = new tblMenuItem();
    //    DTsubchiledMenu = objMenuItem.LoadMenuItems(menutypeID, parentId.ToString);

    //    if (DTsubchiledMenu.Rows.Count > 0)
    //    {
    //        menuString += "<ul>";


    //        for (i = 0; i <= DTsubchiledMenu.Rows.Count - 1; i++)
    //        {
    //            menuString += "<li><a class='menuLink' id='" + DTsubchiledMenu.Rows(i)("appMenuItem").ToString + "' href='MenuItemsList.aspx?ID=" + objEncrypt.Encrypt(DTsubchiledMenu.Rows(i)("appMenuItemId"), appFunctions.strKey) + "'>" + DTsubchiledMenu.Rows(i)("appMenuItem") + "</a>";


    //            if (IsAdd == true)
    //            {
    //                menuString += "<a class='btnLink btnAdd' id='" + DTsubchiledMenu.Rows(i)("appMenuItemId").ToString + "' href='MenuItemDetails.aspx?PID=" + objEncrypt.Encrypt(DTsubchiledMenu.Rows(i)("appMenuItemId"), appFunctions.strKey) + "'> </a>";
    //            }
    //            if (IsEdit == true)
    //            {
    //                menuString += "<a class='btnLink btnEdit' id='" + DTsubchiledMenu.Rows(i)("appMenuItemId").ToString + "' href='MenuItemDetails.aspx?ID=" + objEncrypt.Encrypt(DTsubchiledMenu.Rows(i)("appMenuItemId"), appFunctions.strKey) + "'> </a>";
    //            }

    //            loadSubChildMenu(menutypeID, DTsubchiledMenu.Rows(i)("appMenuItemId"), IsAdd, IsEdit);
    //            menuString += "</li>";
    //        }
    //        menuString += "</ul>";
    //    }
    //}

    private void SetSiteDetail()
    {
        tblSettings objSetting = new tblSettings();
        DataTable dt = new DataTable();
        dt = objSetting.SetLogo();
        if (dt.Rows.Count > 0)
        {
            linkFavIcon.Href = objSetting.AppSiteFavicon;
            SiteLogo.ImageUrl = objSetting.AppSiteLogo;
            lblSiteName.Text = objSetting.AppSiteName;
        }
    }

    protected void lnkLogout_Click(object sender, EventArgs e)
    {
        Session[appFunctions.Session.UserID.ToString()] = "";
        Session.Abandon();
        setLogOut();
        Response.Redirect("Default.aspx");
    }


    private void setLogOut()
    {
        tblUserLog objUserLog = new tblUserLog();
        objUserLog.LoadByPrimaryKey((int)Session[appFunctions.Session.UserLogID.ToString()]);
        if (objUserLog.RowCount > 0)
        {
            objUserLog.AppLogOutDate = DateTime.Now;
            objUserLog.Save();
            objUserLog = null;
        }
        Session[appFunctions.Session.UserLogID.ToString()] = "";
    }

}



