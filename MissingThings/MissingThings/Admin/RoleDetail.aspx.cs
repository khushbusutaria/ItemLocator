using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Web.UI.HtmlControls;

public partial class RoleDetail : PageBase_Admin
{
    DataTable objParentTabDT = new DataTable();
    DataTable objPageDT = new DataTable();
    DataTable objPermissionDT = new DataTable();
    tblPermission objPermision = new tblPermission();
    tblRole objRole = new tblRole();
    //'Dim da As SqlDataAdapter
    //'Dim cn As New SqlConnection


    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!IsPostBack)
        {
            //'cn = New SqlConnection("Server=dbserver;Database=key_CarCrox;Uid=sa;Password=123456;")
            //'cn.Open()
            btnSaveAndAddnew.Visible = HasAdd;
            btnClear.Visible = HasAdd;

            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();

                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    noIdFoundRedirect("Role.aspx");
                }

                objEncrypt = null;
                SetValuesToControls();
            }
            else
            {
                SetValuesToControls(true);
            }

        }

    }

    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        Response.Redirect("Role.aspx");
    }


    protected void btnSaveAndClose_Click(object sender, System.EventArgs e)
    {

        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Role has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Role has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }

            Response.Redirect("Role.aspx");
        }

    }


    protected void btnSaveAndAddnew_Click(object sender, System.EventArgs e)
    {

        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Role has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Role has been updated successfully", Enums.MessageType.Successfull);
            }

            hdnPKID.Value = "";
            SetValuesToControls(true);
        }

    }

    protected void btnClear_Click(object sender, System.EventArgs e)
    {
        hdnPKID.Value = "";
        SetValuesToControls(true);
    }


    private void SetValuesToControls(bool resetControls = false)
    {
        DataTable dt = new DataTable();
        if (string.IsNullOrEmpty(hdnPKID.Value))
        {
            txtRoleName.Text = "";
            txtDescription.Text = "";
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
            {
                dt = objRole.LoadPermissionTab(0.ToString(), 0.ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
            }
            else
            {
                dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), 0.ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
            }

        }
        else
        {
            objRole = new tblRole();
            objRole.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }

        if (resetControls == false)
        {
            txtRoleName.Text = objRole.AppRoleName;
            txtDescription.Text = objRole.AppDescription;
            if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
            {
                dt = objRole.LoadPermissionTab(hdnPKID.Value.ToString(), 0.ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
            }
            else
            {
                dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), 0.ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()], hdnPKID.Value.ToString());
            }


        }
        dlMain.DataSource = dt;
        dlMain.DataBind();
    }

    protected void dlMain_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox ChkIsAdd = (CheckBox)e.Item.FindControl("ChkIsAdd");
            HtmlTableCell objIsadd = (HtmlTableCell)e.Item.FindControl("tdIsAdd");
            if (ChkIsAdd.Visible == false)
            {
                objIsadd.InnerHtml = "NA";
            }

            CheckBox ChkIsEdit = (CheckBox)e.Item.FindControl("ChkIsEdit");
            HtmlTableCell objIsEdit = (HtmlTableCell)e.Item.FindControl("tdIsEdit");
            if (ChkIsEdit.Visible == false)
            {
                objIsEdit.InnerHtml = "NA";
            }

            CheckBox ChkIsDelete = (CheckBox)e.Item.FindControl("ChkIsDelete");
            HtmlTableCell objIsDelete = (HtmlTableCell)e.Item.FindControl("tdIsDelete");
            if (ChkIsDelete.Visible == false)
            {
                objIsDelete.InnerHtml = "NA";
            }

            DataList dl = (DataList)e.Item.FindControl("dlChild");

            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
                {
                    dt = objRole.LoadPermissionTab(0.ToString(), dlMain.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }
                else
                {
                    dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), dlMain.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }

            }
            else
            {
                if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
                {
                    dt = objRole.LoadPermissionTab(hdnPKID.Value, dlMain.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }
                else
                {
                    dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), dlMain.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()], hdnPKID.Value);
                }

            }
            dl.DataSource = dt;
            dl.DataBind();

        }
    }


    protected void dlChild_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {

        if (e.Item.ItemType == ListItemType.Item | e.Item.ItemType == ListItemType.AlternatingItem)
        {
            CheckBox ChkIsAdd = (CheckBox)e.Item.FindControl("ChkIsAdd");
            HtmlTableCell objIsadd = (HtmlTableCell)e.Item.FindControl("tdIsAdd");
            if (ChkIsAdd.Visible == false)
            {
                objIsadd.InnerHtml = "NA";
            }

            CheckBox ChkIsEdit = (CheckBox)e.Item.FindControl("ChkIsEdit");
            HtmlTableCell objIsEdit = (HtmlTableCell)e.Item.FindControl("tdIsEdit");
            if (ChkIsEdit.Visible == false)
            {
                objIsEdit.InnerHtml = "NA";
            }

            CheckBox ChkIsDelete = (CheckBox)e.Item.FindControl("ChkIsDelete");
            HtmlTableCell objIsDelete = (HtmlTableCell)e.Item.FindControl("tdIsDelete");
            if (ChkIsDelete.Visible == false)
            {
                objIsDelete.InnerHtml = "NA";
            }

            DataList dlChild = (DataList)e.Item.Parent;
            DataList dl = (DataList)e.Item.FindControl("dlChild2");
            DataTable dt = new DataTable();
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
                {
                    dt = objRole.LoadPermissionTab(0.ToString(), dlChild.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }
                else
                {
                    dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), dlChild.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }

            }
            else
            {
                if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
                {
                    dt = objRole.LoadPermissionTab(hdnPKID.Value, dlChild.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]);
                }
                else
                {
                    dt = objRole.LoadPermissionTab(Session[appFunctions.Session.RoleID.ToString()].ToString(), dlChild.DataKeys[e.Item.ItemIndex].ToString(), "", (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()], hdnPKID.Value);
                }

            }

            dl.DataSource = dt;
            dl.DataBind();
        }
    }
    protected void dlChild2_ItemDataBound(object sender, System.Web.UI.WebControls.DataListItemEventArgs e)
    {
        CheckBox ChkIsAdd = (CheckBox)e.Item.FindControl("ChkIsAdd");
        HtmlTableCell objIsadd = (HtmlTableCell)e.Item.FindControl("tdIsAdd");
        if (ChkIsAdd.Visible == false)
        {
            objIsadd.InnerHtml = "NA";
        }

        CheckBox ChkIsEdit = (CheckBox)e.Item.FindControl("ChkIsEdit");
        HtmlTableCell objIsEdit = (HtmlTableCell)e.Item.FindControl("tdIsEdit");
        if (ChkIsEdit.Visible == false)
        {
            objIsEdit.InnerHtml = "NA";
        }

        CheckBox ChkIsDelete = (CheckBox)e.Item.FindControl("ChkIsDelete");
        HtmlTableCell objIsDelete = (HtmlTableCell)e.Item.FindControl("tdIsDelete");
        if (ChkIsDelete.Visible == false)
        {
            objIsDelete.InnerHtml = "NA";
        }
    }

    private bool SaveData()
    {
        bool functionReturnValue = false;
        int intRoleId = 0;

        objRole = new tblRole();
        objRole.Where.AppRoleName.Value = txtRoleName.Text;
        objRole.Query.AddResultColumn(tblRole.ColumnNames.AppRoleID);
        objRole.Query.Load();


        if (objRole.RowCount > 0)
        {
            if (objRole.AppRoleID.ToString() != hdnPKID.Value.ToString())
            {
                DInfo.ShowMessage("Role Name Already Exist", Enums.MessageType.Error);
                return functionReturnValue;
            }

        }

        objRole = new tblRole();

        if (string.IsNullOrEmpty(hdnPKID.Value))
        {
            objRole.AddNew();
            objRole.AppCreatedDate = DateTime.Now;
            objRole.AppCreatedBy = (int)Session[appFunctions.Session.UserID.ToString()];
        }
        else
        {
            objRole.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }

        objRole.AppRoleName = txtRoleName.Text;
        objRole.AppDescription = txtDescription.Text;
        objRole.Save();

        intRoleId = objRole.AppRoleID;

        tblPermission objPermission = new tblPermission();

        DataList dlChild = default(DataList);
        DataList dlChild2 = default(DataList);




        foreach (DataListItem diMain in dlMain.Items)
        {
            //' Do diMain


            Permission(intRoleId, dlMain, diMain);

            dlChild = (DataList)diMain.FindControl("dlChild");



            foreach (DataListItem diChild in dlChild.Items)
            {
                //' Do diChild

                Permission(intRoleId, dlChild, diChild);
                dlChild2 = (DataList)diChild.FindControl("dlChild2");


                foreach (DataListItem diChild2 in dlChild2.Items)
                {
                    //' Do diChil2

                    Permission(intRoleId, dlChild2, diChild2);
                }

            }

        }
        return true;
    }
    private bool Permission(int intRoleID, DataList dl, DataListItem di)
    {
        dynamic objPermission = new tblPermission();
        int intTabID = 0;
        intTabID = (int)dl.DataKeys[di.ItemIndex];
        objPermission.Where.AppTabID.Value = intTabID;
        objPermission.Where.AppRoleID.Value = intRoleID;
        objPermission.Query.Load();
        if (objPermission.RowCount <= 0)
        {
            objPermission.AddNew();
            objPermission.AppCreatedDate = DateTime.Now;
            objPermission.AppCreatedBy = (int)Session[appFunctions.Session.UserID.ToString()];
        }
        objPermission.AppRoleID = intRoleID;


        if (di.FindControl("chkIsAdd") == null)
        {
            objPermission.AppIsAdd = false;
        }
        else
        {
            objPermission.AppIsAdd = ((CheckBox)di.FindControl("chkIsAdd")).Checked;

        }


        if (di.FindControl("chkIsEdit") == null)
        {
            objPermission.AppIsEdit = false;
        }
        else
        {
            objPermission.AppIsEdit = ((CheckBox)di.FindControl("chkIsEdit")).Checked;

        }

        if (di.FindControl("chkIsDelete") == null)
        {
            objPermission.AppIsDelete = false;
        }
        else
        {
            objPermission.AppIsDelete = ((CheckBox)di.FindControl("chkIsDelete")).Checked;

        }


        objPermission.AppIsView = ((CheckBox)di.FindControl("chkIsView")).Checked;
        objPermission.AppTabID = intTabID;
        objPermission.Save();

        return true;
    }
}