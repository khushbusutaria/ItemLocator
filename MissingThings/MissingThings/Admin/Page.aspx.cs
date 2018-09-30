using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Page : PageBase_Admin
{
    tblPage objPage;
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
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
            {
                dgvGridView.Columns[3].Visible = HasEdit;
            }
            else
            {
                dgvGridView.Columns[3].Visible = false;
            }

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
            txtSearch.Focus();
            objCommon = null;
        }

    }


    private void LoadDataGrid(bool IsResetPageIndex, bool IsSort, string strFieldName = "", string strFieldValue = "")
    {
        objPage = new tblPage();
        objDataTable = objPage.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim());
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

        objPage = null;
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
                tblMenuItem objMenuItem = new tblMenuItem();
                objMenuItem.Query.AddResultColumn(tblMenuItem.ColumnNames.AppMenuItem);
                objMenuItem.Where.AppPageId.Value = arIDs.GetValue(i);
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
            DInfo.ShowMessage("Some Item May Not Deleted As It Is Linked With Some Menu Item . . .", Enums.MessageType.Warning);
        }
        else
        {
            DInfo.ShowMessage("Page Formate has been deleted successfully", Enums.MessageType.Successfull);
        }


        if (IsDelete || HasChild )
        {
            LoadDataGrid(false, false);
        }

        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objPage = new tblPage();
        if (objPage.LoadByPrimaryKey(intPKID))
        {
            objPage.MarkAsDeleted();
            objPage.Save();
        }
        retval = true;
        objPage = null;
        return retval;
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            hdnPKID.Value = e.CommandArgument.ToString();
            if (e.CommandName == "IsLink")
            {
                objPage = new tblPage();
                if (objPage.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
                {

                    if (objPage.AppIsLink == true)
                    {
                        objPage.AppIsLink = false;
                    }
                    else if (objPage.AppIsLink == false)
                    {
                        objPage.AppIsLink = true;
                    }
                    objPage.Save();
                }
                objPage = null;
                LoadDataGrid(false, false, "", "");
            }
            if (e.CommandName == "IsDefault")
            {
                objPage = new tblPage();
                if (objPage.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
                {
                    tblPage objPageTemp = new tblPage();
                    objPageTemp.ResetDefaultPage();
                    objPageTemp = null;
                    if (objPage.AppIsDefault == true)
                    {
                        objPage.AppIsDefault = false;
                    }
                    else if (objPage.AppIsDefault == false)
                    {
                        objPage.AppIsDefault = true;
                    }
                    objPage.Save();
                }
                objPage = null;
                LoadDataGrid(false, false, "", "");
            }
        }
    }

    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("PageDetail.aspx");

    }
}