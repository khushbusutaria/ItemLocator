using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class MenuItems : PageBase_Admin
{
    tblMenuItem objMenuItem;
    public string strSiteMap = "";
    public string strSiteMapData = "";
    int intSiteMapId = 0;
    Boolean ifError = false;
    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {
            ifError = false;
            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;
            dgvGridView.Columns[5].Visible = HasEdit;

            if ((Session[appFunctions.Session.ShowMessage.ToString()] != null))
            {
                if (!string.IsNullOrEmpty(Session[appFunctions.Session.ShowMessage.ToString()].ToString()))
                {
                    DInfo.ShowMessage(Session[appFunctions.Session.ShowMessage.ToString()].ToString(), (Enums.MessageType)Session[appFunctions.Session.ShowMessageType.ToString()]);
                    Session[appFunctions.Session.ShowMessage.ToString()] = "";
                    Session[appFunctions.Session.ShowMessageType.ToString()] = "";
                }
            }
            objCommon = new clsCommon();
            objCommon.FillRecordPerPage(ref ddlPerPage);
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                    intSiteMapId = Convert.ToInt32(hdnPKID.Value);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }

                objEncrypt = null;
                if ((Request.QueryString.Get("type") != null))
                {
                    if ((Request.QueryString.Get("type") == "mtype"))
                    {
                        hdnType.Value = "mtype";
                    }
                    else
                    {
                        hdnType.Value = "";
                    }
                }
                else
                {
                    hdnType.Value = "";
                }
            }
            if (intSiteMapId != -1)
            {
                DataTable dt = new DataTable();

                objEncrypt = new clsEncryption();
                dynamic objTempMenuItem = new tblMenuItem();
                if (hdnType.Value == "")
                {
                    dt = objTempMenuItem.GetSiteMap(intSiteMapId);
                    strSiteMapData = dt.Rows[0][0].ToString();
                    dt = objTempMenuItem.GetMenuType(intSiteMapId, false);
                }
                else
                {
                    dt = objTempMenuItem.GetMenuType(intSiteMapId, true);
                }

                String[] SplitSiteMapString = strSiteMapData.Split('/');

                strSiteMap = "<a href='MasterMenus.aspx'>Menu Items</a>";

                if (SplitSiteMapString.Length > 1)
                {
                    strSiteMap += " >  <a href=MenuItems.aspx?Id=" + objEncrypt.Encrypt(dt.Rows[0]["appMenuTypeId"].ToString(), appFunctions.strKey) + "&type=mtype>" + dt.Rows[0]["appMenuTypeName"].ToString() + "</a>";
                    //strSiteMap = "<a href='MenuItems.aspx'>Parent Tabs </a>";
                }
                else
                {
                    strSiteMap += " > " + dt.Rows[0]["appMenuTypeName"].ToString();
                }

                for (int i = 0; i <= SplitSiteMapString.Length - 2; i += 1)
                {
                    String[] SplittedItems = SplitSiteMapString.GetValue(i).ToString().Split(',');
                    if (i == SplitSiteMapString.Length - 2)
                    {
                        strSiteMap += " > " + SplittedItems.GetValue(0);
                    }
                    else
                    {
                        strSiteMap += " > <a href='MenuItems.aspx?Id=" + objEncrypt.Encrypt(SplittedItems.GetValue(1).ToString(), appFunctions.strKey) + "'> " + SplittedItems.GetValue(0) + "</a>";
                    }
                }

                litSiteMap.Text = strSiteMap;

            }

            LoadDataGrid(true, false);
            txtSearch.Focus();
            objCommon = null;
        }

    }


    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objMenuItem = new tblMenuItem();
        string strWhereCondition = "";
        if (hdnPKID.Value != "" && hdnType.Value == "")
        {
            strWhereCondition = "appParentID = " + hdnPKID.Value.ToString();
        }
        else if (hdnPKID.Value != "" && hdnType.Value != "")
        {
            strWhereCondition = "appMenuTypeID = " + hdnPKID.Value.ToString() + " and appParentID = 0";
        }
        else
        {
            strWhereCondition = "appParentID = 0";
        }

        objDataTable = objMenuItem.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), strWhereCondition);

        //'Reset PageIndex of gridviews
        if (IsResetPageIndex)
        {
            if (dgvGridView.PageCount > 0)
            {
                dgvGridView.PageIndex = 0;
            }
        }

        dgvGridView.DataSource = null;
        dgvGridView.DataBind();
        lblCount.Text = 0.ToString();
        hdnSelectedIDs.Value = "";

        //'Check for data into datatable
        if (objDataTable.Rows.Count <= 0)
        {
            DInfo.ShowMessage("No data found", Enums.MessageType.Information);
            return;
        }
        else
        {
            if (ddlPerPage.SelectedItem.Text.ToLower() == "all")
            {
                dgvGridView.AllowPaging = false;
            }
            else
            {
                dgvGridView.AllowPaging = true;
                dgvGridView.PageSize = Convert.ToInt32(ddlPerPage.SelectedItem.Text);
            }

            lblCount.Text = objDataTable.Rows.Count.ToString();
            objDataTable = SortDatatable(objDataTable, ViewState["SortColumn"].ToString(), (appFunctions.Enum_SortOrderBy)ViewState["SortOrder"], IsSort);
            dgvGridView.DataSource = objDataTable;
            dgvGridView.DataBind();
        }

        objMenuItem = null;
    }

    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }

    protected void dgvGridView_RowCreated(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        switch (e.Row.RowType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");
                chk.ID = "chkSelectRow_" + strID;
                break;
        }
    }

    protected void dgvGridView_RowDataBound(object sender, System.Web.UI.WebControls.GridViewRowEventArgs e)
    {
        DataControlRowType itemType = e.Row.RowType;

        switch (itemType)
        {
            case DataControlRowType.DataRow:
                string strID = dgvGridView.DataKeys[e.Row.RowIndex].Values[0].ToString();
                CheckBox chk = (CheckBox)e.Row.FindControl("chkSelectRow");

                if ((chk != null))
                {
                    chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
                }
                break;
        }
    }

    protected void dgvGridView_Sorting(object sender, System.Web.UI.WebControls.GridViewSortEventArgs e)
    {
        hdnSelectedIDs.Value = "";
        ViewState["SortColumn"] = e.SortExpression;
        LoadDataGrid(false, true);
    }

    protected void ddlPerPage_SelectedIndexChanged(object sender, System.EventArgs e)
    {
        hdnSelectedIDs.Value = "";
        LoadDataGrid(true, false);
    }

    protected void btnDelete_Click(object sender, System.EventArgs e)
    {
        string[] arIDs = hdnSelectedIDs.Value.ToString().TrimEnd(',').Split(',');
        bool IsDelete = false;
        bool HasChild = false;


        for (int i = 0; i <= arIDs.Length - 1; i++)
        {

            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                objMenuItem = new tblMenuItem();
                objMenuItem.Query.AddResultColumn(tblMenuItem.ColumnNames.AppMenuItem);
                objMenuItem.Where.AppParentId.Value = arIDs.GetValue(i);
                objMenuItem.Query.Load();

                if (objMenuItem.RowCount == 0)
                {
                    if (Delete(Convert.ToInt32(arIDs.GetValue(i))))
                    {
                        IsDelete = true;
                    }
                }
                else
                {
                    HasChild = true;
                }

            }

        }

        if (HasChild)
        {
            DInfo.ShowMessage("Some Item May Not Deleted As It Contains Child . . .", Enums.MessageType.Warning);
        }
        else
        {
            DInfo.ShowMessage("MenuItem has been deleted successfully", Enums.MessageType.Successfull);
        }
        if (IsDelete || HasChild)
        {
            LoadDataGrid(false, false);
        }
        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objMenuItem = new tblMenuItem();
        if (objMenuItem.LoadByPrimaryKey(intPKID))
        {
            objMenuItem.MarkAsDeleted();
            objMenuItem.Save();
        }
        retval = true;
        objMenuItem = null;
        return retval;
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            objCommon = new clsCommon();

            if (e.CommandName == "IsActive")
            {
                objMenuItem = new tblMenuItem();
                if (objMenuItem.LoadByPrimaryKey(Convert.ToInt32(e.CommandArgument.ToString())))
                {
                    if (objMenuItem.AppIsActive == true)
                    {
                        objMenuItem.AppIsActive = false;
                    }
                    else if (objMenuItem.AppIsActive == false)
                    {
                        objMenuItem.AppIsActive = true;
                    }
                    objMenuItem.Save();
                }
                objMenuItem = null;
            }
            else if (e.CommandName == "Up")
            {
                LinkButton inkButton = (LinkButton)e.CommandSource;
                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;
                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblMenuItem", tblMenuItem.ColumnNames.AppMenuItemId, tblMenuItem.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[1]);

                    LoadDataGrid(false, false);
                    objCommon = null;
                }
            }
            else if (e.CommandName == "Down")
            {

                LinkButton lnkButton = (LinkButton)e.CommandSource;

                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;

                if (drCurrent.RowIndex < dgvGridView.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblMenuItem", tblMenuItem.ColumnNames.AppMenuItemId, tblMenuItem.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[1], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[1]);

                    LoadDataGrid(false, false);
                    objCommon = null;
                }

            }

            LoadDataGrid(false, false, "", "");
        }
    }
    protected void btnAdd_Click(object sender, EventArgs e)
    {
        if (hdnPKID.Value != "")
        {
            if (hdnType.Value == "")
            {
                Response.Redirect("MenuItemsDetail.aspx?PID=" + Request.QueryString.Get("ID"));
            }
            else if (hdnType.Value == "mtype")
            {
                Response.Redirect("MenuItemsDetail.aspx?TID=" + Request.QueryString.Get("ID"));
            }
        }
        else
        {

            Response.Redirect("MenuItemsDetail.aspx");
        }
    }
}

