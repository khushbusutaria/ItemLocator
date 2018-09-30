using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;

public partial class Tab : PageBase_Admin
{
    tblTab objtab;
    public string strSiteMap = "";
    public string strSiteMapData = "";
    int intSiteMapId = 0;

    bool ifError = false;



    protected void Page_Load(object sender, System.EventArgs e)
    {
        if (!IsPostBack)
        {

            ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            ViewState["SortColumn"] = "";


            ifError = false;
            btnAdd.Visible = HasAdd;
            btnDelete.Visible = HasDelete;
            dgvGridView.Columns[0].Visible = HasDelete;
            dgvGridView.Columns[1].Visible = HasEdit;
            dgvGridView.Columns[4].Visible = HasEdit;
            dgvGridView.Columns[5].Visible = HasEdit;
            dgvGridView.Columns[6].Visible = HasEdit;
            dgvGridView.Columns[7].Visible = HasEdit;
            dgvGridView.Columns[8].Visible = HasEdit;

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
            //  objCommon.FillDropDownList(ddlParents, "tblTab", tblTab.ColumnNames.AppTabName, tblTab.ColumnNames.AppTabID, "--Select Parent--", tblTab.ColumnNames.AppDisplayOrder, appFunctions.Enum_SortOrderBy.Asc, "appParentId = 0")

            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                hdnCurrentTabID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                intSiteMapId = Convert.ToInt32(hdnCurrentTabID.Value);
            }

            if (intSiteMapId != -1)
            {
                DataTable dt = new DataTable();

                objEncrypt = new clsEncryption();
                dynamic objTempTab = new tblTab();
                dt = objTempTab.GetSiteMap(intSiteMapId);
                strSiteMapData = dt.Rows[0][0].ToString();

                dt = objTempTab.GetMenuType(intSiteMapId);

                String[] SplitSiteMapString = strSiteMapData.Split('/');
                if (SplitSiteMapString.Length > 1)
                {
                    strSiteMap = "<a href='Tab.aspx'>Parent Tabs </a>";
                }
                else
                {
                    strSiteMap = "Parent Tabs";
                }

                for (int i = SplitSiteMapString.Length - 2; i >= 0; i += -1)
                {
                    String[] SplittedItems = SplitSiteMapString.GetValue(i).ToString().Split(',');

                    //If i = 0 Then
                    //    strSiteMap = "<a href=TabsList.aspx?Id=" + objEncrypt.Encrypt(dt.Rows(0).Item("appTabID"), appFunctions.strKey) + "&type=mtype>" + dt.Rows(0).Item("appTabName") + "</a>"
                    //End If
                    //If i <> SplitSiteMapString.Length - 2 Then
                    //    strSiteMap += " > "
                    //End If
                    if (i == 0)
                    {
                        strSiteMap += " > " + SplittedItems.GetValue(0);
                    }
                    else
                    {
                        strSiteMap += " > <a href='Tab.aspx?Id=" + objEncrypt.Encrypt(SplittedItems.GetValue(1).ToString(), appFunctions.strKey) + "'>" + SplittedItems.GetValue(0) + "</a>";
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
        objtab = new tblTab();

        string strWhereCondition = "";
        if (!string.IsNullOrEmpty(hdnCurrentTabID.Value))
        {
            strWhereCondition = "appParentID = " + hdnCurrentTabID.Value.ToString();
        }
        else
        {
            strWhereCondition = "appParentID = 0";
        }
        objDataTable = objtab.LoadGridData(ddlFields.SelectedValue, txtSearch.Text.Trim(), strWhereCondition);

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

        objtab = null;
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

                //if ((bool)dgvGridView.DataKeys[e.Row.RowIndex].Values[1])
                //{
                //    ImageButton imd = (ImageButton)e.Row.FindControl("imgbtnIsAdd");
                //    imd.ImageUrl = "images/right_icon.png";
                //}

                //if ((bool)dgvGridView.DataKeys[e.Row.RowIndex].Values[2])
                //{
                //    ImageButton imd = (ImageButton)e.Row.FindControl("imgbtnIsEdit");
                //    imd.ImageUrl = "images/right_icon.png";
                //}

                //if ((bool)dgvGridView.DataKeys[e.Row.RowIndex].Values[3])
                //{
                //    ImageButton imd = (ImageButton)e.Row.FindControl("imgbtnIsDelete");
                //    imd.ImageUrl = "images/right_icon.png";
                //}

                //if ((bool)dgvGridView.DataKeys[e.Row.RowIndex].Values[4])
                //{
                //    ImageButton imd = (ImageButton)e.Row.FindControl("imgbtnIsActive");
                //    imd.ImageUrl = "images/right_icon.png";
                //}

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
            if (!string.IsNullOrEmpty(arIDs.GetValue(i).ToString()))
            {
                if (Delete(Convert.ToInt32(arIDs.GetValue(i))))
                {
                    IsDelete = true;
                }
            }
        }

        if (IsDelete)
        {
            LoadDataGrid(false, false);
        }

        DInfo.ShowMessage("Tab has been deleted successfully", Enums.MessageType.Successfull);
        hdnSelectedIDs.Value = "";
    }

    private bool Delete(int intPKID)
    {
        bool retval = false;
        objtab = new tblTab();

        var _with1 = objtab;

        if (_with1.LoadByPrimaryKey(intPKID))
        {
            _with1.MarkAsDeleted();
            _with1.Save();
        }

        retval = true;
        objtab = null;
        return retval;
    }


    protected void dgvGridView_RowCommand(object sender, System.Web.UI.WebControls.GridViewCommandEventArgs e)
    {
        if (!string.IsNullOrEmpty(e.CommandArgument.ToString()))
        {
            clsEncryption objEncrypt = new clsEncryption();
            int intHiddenPKID = 0;

            if (!string.IsNullOrEmpty(hdnPKID.Value))
            {
                intHiddenPKID = Convert.ToInt32(hdnPKID.Value);

            }

            objCommon = new clsCommon();
            hdnPKID.Value = e.CommandArgument.ToString();

            if (e.CommandName == "ListChildMenus")
            {
                Response.Redirect("Tab.aspx?ID=" + objEncrypt.Encrypt(hdnPKID.Value, appFunctions.strKey), true);
            }
            //else if (e.CommandName == "Edit")
            //{
            //    Response.Redirect("TabDetail.aspx?ID=" + objEncrypt.Encrypt(hdnPKID.Value, appFunctions.strKey), true);
            //}
            else if (e.CommandName == "Up")
            {

                LinkButton inkButton = (LinkButton)e.CommandSource;

                GridViewRow drCurrent = (GridViewRow)inkButton.Parent.Parent;

                if (drCurrent.RowIndex > 0)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex - 1];
                    objCommon.SetDisplayOrder("tblTab", tblTab.ColumnNames.AppTabID, tblTab.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[5], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[5]);
                    hdnPKID.Value = intHiddenPKID.ToString();
                    LoadDataGrid(false, false);
                    objCommon = null;
                }

                hdnPKID.Value = intHiddenPKID.ToString();

            }
            else if (e.CommandName == "Down")
            {

                LinkButton lnkButton = (LinkButton)e.CommandSource;

                GridViewRow drCurrent = (GridViewRow)lnkButton.Parent.Parent;

                if (drCurrent.RowIndex < dgvGridView.Rows.Count - 1)
                {
                    GridViewRow drUp = dgvGridView.Rows[drCurrent.RowIndex + 1];
                    objCommon.SetDisplayOrder("tblTab", tblTab.ColumnNames.AppTabID, tblTab.ColumnNames.AppDisplayOrder, (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[0], (int)dgvGridView.DataKeys[drCurrent.RowIndex].Values[5], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[0], (int)dgvGridView.DataKeys[drUp.RowIndex].Values[5]);
                    hdnPKID.Value = intHiddenPKID.ToString();
                    LoadDataGrid(false, false);
                    objCommon = null;
                }

                hdnPKID.Value = intHiddenPKID.ToString();
            }
            else if (e.CommandName == "IsActive")
            {
                objtab = new tblTab();

                objtab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

                if (objtab.AppIsActive == true)
                {
                    objtab.AppIsActive = false;
                }
                else if (objtab.AppIsActive == false)
                {
                    objtab.AppIsActive = true;
                }

                objtab.Save();
                LoadDataGrid(false, false, "", "");

            }
            else if (e.CommandName == "IsAdd")
            {
                objtab = new tblTab();


                objtab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

                if (objtab.AppIsAdd == true)
                {
                    objtab.AppIsAdd = false;
                }
                else if (objtab.AppIsAdd == false)
                {
                    objtab.AppIsAdd = true;
                }

                objtab.Save();
                LoadDataGrid(false, false, "", "");

            }
            else if (e.CommandName == "IsEdit")
            {
                objtab = new tblTab();

                objtab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

                if (objtab.AppIsEdit == true)
                {
                    objtab.AppIsEdit = false;
                }
                else if (objtab.AppIsEdit == false)
                {
                    objtab.AppIsEdit = true;
                }

                objtab.Save();
                LoadDataGrid(false, false, "", "");

            }
            else if (e.CommandName == "IsDelete")
            {
                objtab = new tblTab();

                objtab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

                if (objtab.AppIsDelete == true)
                {
                    objtab.AppIsDelete = false;
                }
                else if (objtab.AppIsDelete == false)
                {
                    objtab.AppIsDelete = true;
                }

                objtab.Save();
                LoadDataGrid(false, false, "", "");

            }
            else if (e.CommandName == "IsDashbord")
            {
                objtab = new tblTab();

                objtab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

                if (objtab.AppIsShowOnDashboard == true)
                {
                    objtab.AppIsShowOnDashboard = false;
                }
                else if (objtab.AppIsShowOnDashboard == false)
                {
                    objtab.AppIsShowOnDashboard = true;
                }

                objtab.Save();
                LoadDataGrid(false, false, "", "");

            }
        }
    }

    protected void btnAdd_Click(object sender, System.EventArgs e)
    {
        if (!string.IsNullOrEmpty(hdnCurrentTabID.Value))
        {
            Response.Redirect("TabDetail.aspx?PID=" + Request.QueryString.Get("ID"));
        }
        else
        {
            Response.Redirect("TabDetail.aspx");


        }

    }
}