﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class Role : PageBase_Admin
{
    tblRole objRole;
    protected void btnGO_Click(object sender, System.EventArgs e)
    {
        LoadDataGrid(true, false);
    }

    protected void btnReset_Click(object sender, System.EventArgs e)
    {
        txtSearch.Text = "";
        LoadDataGrid(true, false);
    }

    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("RoleDetail.aspx");
    }

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {

            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";

            btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;
            //dgvGridView.Columns(0).Visible = HasEdit

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
            LoadDataGrid(true, false);
            objCommon = null;
        }

        txtSearch.Focus();
    }


    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objRole = new tblRole();

        if (!string.IsNullOrEmpty(strFieldValue) & !string.IsNullOrEmpty(strFieldName))
        {
            objDataTable = objRole.LoadGridData(strFieldName, strFieldValue, (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()], Session[appFunctions.Session.UserID.ToString()].ToString());
        }
        else
        {
            objDataTable = objRole.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()], Session[appFunctions.Session.UserID.ToString()].ToString());
        }

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

        objRole = null;

    }

    protected void dgvGridView_PageIndexChanging(object sender, System.Web.UI.WebControls.GridViewPageEventArgs e)
    {
        dgvGridView.PageIndex = e.NewPageIndex;
        LoadDataGrid(false, false);
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {

        //if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        //{
        //    clsEncryption objEncrypt = new clsEncryption();
        //    hdnPKID.Value = e.CommandArgument.ToString();

        //    if (e.CommandName == "Edit")
        //    {
        //        Response.Redirect("RoleDetail.aspx?ID=" + objEncrypt.Encrypt(hdnPKID.Value, appFunctions.strKey), true);
        //    }
        //}
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
                chk.Attributes.Add("OnClick", "javascript:SelectRow(this," + strID + ")");
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
        for (int i = 0; i <= arIDs.Length - 1; i++)
        {
            dynamic objUser = new tblUser();
            objUser.Where.AppRoleId.Value = arIDs.GetValue(i);
            objUser.Query.Load();
            if (objUser.RowCount <= 0)
            {
                if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
                {
                    if (Delete((int)arIDs.GetValue(i)))
                    {
                        IsDelete = true;
                    }
                }
                DInfo.ShowMessage("Role has been deleted successfully", Enums.MessageType.Successfull);
                hdnSelectedIDs.Value = "";
            }
            else
            {
                DInfo.ShowMessage("Role is already assaigned to Users, It can not be Delete", Enums.MessageType.Error);
            }

        }

        if (IsDelete)
        {
            LoadDataGrid(false, false);
        }
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objRole = new tblRole();
        dynamic objPermission = new tblPermission();
        objPermission.DeleteSelectedRoleTab(intPKID);
        var _with1 = objRole;
        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }

        retval = true;
        objRole = null;
        return retval;
    }
}