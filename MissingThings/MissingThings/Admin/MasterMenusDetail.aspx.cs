using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class MasterMenusDetail : PageBase_Admin
{
    tblMenuType objMenuType;
    DataTable objBlocksDT;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iMenuTypeid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            SetRegulerExpression();
            lnkSaveAndAddnew.Visible = HasAdd;
            objClsCommon = new clsCommon();
            objClsCommon.FillDropDownList(ddlBlock, "tblBlock", tblBlock.ColumnNames.AppBlockName, tblBlock.ColumnNames.AppBlockId, "--Select Block--", tblBlock.ColumnNames.AppBlockId, appFunctions.Enum_SortOrderBy.Asc, "appIsShowContent = 'False'");
            objClsCommon = null;
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }

                objEncrypt = null;
                SetValuesToControls();
            }
        }
    }


    private bool SaveData()
    {
        objClsCommon = new clsCommon();
        if (objClsCommon.IsRecordExists("tblMenuType", tblMenuType.ColumnNames.AppMenuTypeName, tblMenuType.ColumnNames.AppMenuTypeId, txtMenuTypeName.Text, hdnPKID.Value))
        {
            DInfo.ShowMessage("MenuType Name already exists.", Enums.MessageType.Error);
            return false;
        }
        objClsCommon = null;

        objMenuType = new tblMenuType();

        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuType.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objMenuType.AddNew();
            objMenuType.AppCreatedDate = System.DateTime.Now;
            objMenuType.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objMenuType.AppMenuTypeName = txtMenuTypeName.Text;
        objMenuType.s_AppNoOfLevel = txtNoLevel.Text;
        objMenuType.AppIsActive = ChkIsActive.Checked;
        objMenuType.s_AppBlockId = ddlBlock.SelectedValue;
        objMenuType.Save();
        iMenuTypeid = objMenuType.AppMenuTypeId;
        objMenuType = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuType = new tblMenuType();
            if (objMenuType.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtMenuTypeName.Text = objMenuType.AppMenuTypeName;
                ddlBlock.SelectedValue = objMenuType.s_AppBlockId;

                if (objMenuType.AppIsActive)
                {
                    ChkIsActive.Checked = true;
                }
                else
                {
                    ChkIsActive.Checked = false;
                }

                txtNoLevel.Text = objMenuType.s_AppNoOfLevel;
            }
            objMenuType = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MasterMenus.aspx", true);
    }

    private void ResetControls()
    {
        txtMenuTypeName.Text = "";
        ChkIsActive.Checked = true;
        ddlBlock.SelectedIndex = 0;
        txtNoLevel.Text = "";
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("MenuType has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("MenuType has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iMenuTypeid.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "MenuType has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "MenuType has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("MasterMenus.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("MenuType has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("MenuType has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }
    public void SetRegulerExpression()
    {
        REVNoLevel.ValidationExpression = RXNumericRegularExpression;
        REVNoLevel.ErrorMessage = "Invalid No.Level (" + RXNumericRegularExpressionMsg + ")";

    }

}