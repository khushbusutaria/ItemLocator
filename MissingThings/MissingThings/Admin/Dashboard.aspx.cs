using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Globalization;
using System.Text.RegularExpressions;

public partial class _Dashboard : PageBase_Admin
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            //tblUser objUser = new tblUser();

            //dgvGridView.DataSource = objUser.RecentUsers(Session[appFunctions.Session.UserID.ToString()].ToString(), Convert.ToBoolean(Session[appFunctions.Session.IsSuperAdmin.ToString()].ToString()));
            //dgvGridView.DataBind(); 
            //objUser = null;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            DataTable objPermissionDT = new DataTable();
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
            {
                tblTab objTab = new tblTab();
                objTab.Query.AddOrderBy(tblTab.ColumnNames.AppDisplayOrder, MyGeneration.dOOdads.WhereParameter.Dir.ASC);
                objTab.Query.AddOrderBy(tblTab.ColumnNames.AppParentID, MyGeneration.dOOdads.WhereParameter.Dir.ASC);
                objTab.Where.AppIsShowOnDashboard.Value = true;
                objTab.Query.Load();
                objPermissionDT = objTab.DefaultView.Table;
            }
            else
            {
                tblPermission objPermission = new tblPermission();
                try
                {
                    objPermissionDT = objPermission.LoadTabsForRole(Convert.ToInt32(Session[appFunctions.Session.RoleID.ToString()].ToString()), true);
                }
                catch
                {
                    Response.Redirect("UserPannel.aspx");
                }
            }

            rptDashBoardLinks.DataSource = objPermissionDT;
            rptDashBoardLinks.DataBind();

            //string dashBoardString = "";
            //foreach (DataRow dr in objPermissionDT.Rows)
            //{
            //    dashBoardString += "<div class='col-md-2 col-sm-3 col-xs-3' align='Center'>";
            //    dashBoardString += "<a href='" + dr["appWebPageName"].ToString() + "'>";
            //    if (dr["appIconPath"].ToString() != "")
            //    {
            //        dashBoardString += "<img src='" + dr["appIconPath"].ToString() + "' height='100px' width='100px'  alt='" + dr["appTabName"] + "'/>";
            //    }
            //    else
            //    {
            //        dashBoardString += "<img src='Images/NoImg.png' height='100px' width='100px'  alt='" + dr["appTabName"] + "'/>";
            //    }

            //    dashBoardString += "<div style='text=align:center;Width:100px;line-height:20Px;height:40Px;'>" + dr["appTabName"].ToString() + "</div>";
            //    dashBoardString += "</a>";

            //    dashBoardString += "</div>";
            //}
            //DashBord.InnerHtml = dashBoardString;
            LoadDataGridInquiry();
        }
    }
    private void LoadDataGridInquiry()
    {
        //tblInquiry objInquiry = new tblInquiry();
        //objDataTable = objInquiry.LoadDashboardDataInquiry();
        //objInquiry = null;
        //if (objDataTable.Rows.Count > 0)
        //{
        //    divInquiry.Visible = true;
        //}
        //else
        //{
        //    divInquiry.Visible = false;
        //}
        //dgvInInquiry.DataSource = objDataTable;
        //dgvInInquiry.DataBind();
    }

    protected void dgvInInquiry_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            //objCommon = new clsCommon();
            //hdnPKID.Value = e.CommandArgument.ToString();

            //if (e.CommandName == "IsRead")
            //{
            //    tblInquiry objInquiry = new tblInquiry();
            //    if (objInquiry.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            //    {
            //        if (objInquiry.AppIsRead == true)
            //        {
            //            objInquiry.AppIsRead = false;
            //        }
            //        else if (objInquiry.AppIsRead == false)
            //        {
            //            objInquiry.AppIsRead = true;
            //        }
            //        objInquiry.Save();
            //    }
            //    objInquiry = null;
            //    LoadDataGridInquiry();
            //}
        }
    }

    protected void dgvInInquiry_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvInInquiry.PageIndex = e.NewPageIndex;
        LoadDataGridInquiry();
    }

}