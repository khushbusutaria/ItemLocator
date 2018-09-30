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
using System.Web.Routing;


public partial class MenuItemsDetail : PageBase_Admin
{

    string strSiteMap = "";
    string aliasPrefix = "";
    int strSiteMapId = 0;
    tblMenuItem objMenuItem;
    tblMenuType objMenuType;
    DataTable objMenuItemDT;
    DataTable objMenuTypeDT;
    tblPage objPage;
    clsEncryption objEncrypt;
    int iMenuItemid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
            if (!((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()]))
            {
                trDefaultPage.Visible = false;
                trIsStatic.Visible = false;
                trdynParameters.Visible = false;
            }
            hdnAliasPrefix.Value = "";
            objCommon = new clsCommon();
            objCommon.FillDropDownList(ddlMenuType, "tblMenuType", tblMenuType.ColumnNames.AppMenuTypeName, tblMenuType.ColumnNames.AppMenuTypeId, "-- Select Menu --", tblMenuType.ColumnNames.AppMenuTypeName, appFunctions.Enum_SortOrderBy.Asc);
            objCommon.FillDropDownList(ddlPages, "tblPage", tblPage.ColumnNames.AppPageName, tblPage.ColumnNames.AppPageId, "-- Select Page --", tblPage.ColumnNames.AppPageName, appFunctions.Enum_SortOrderBy.Asc);
            objCommon.FillDropDownList(ddlPageFormats, "tblPageFormat", tblPageFormat.ColumnNames.AppPageFormatName, tblPageFormat.ColumnNames.AppPageFormatId, "-- Select Page Format --", tblPageFormat.ColumnNames.AppPageFormatId, appFunctions.Enum_SortOrderBy.Asc, " appIsActive = 'true' ");
            objCommon.FillDropDownList(ddlMenuItemType, "tblMenuItemType", tblMenuItemType.ColumnNames.AppMenuItemType, tblMenuItemType.ColumnNames.AppMenuItemTypeID, "--Select Menu Item Type--");
            if (ddlMenuType.SelectedIndex == 0)
            {
                ddlParent.Items.Add(new ListItem("--Select Parent--", "0"));
            }


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
            else
            {
                chkCreatePage.Checked = true;
                chkIsLink.Checked = false;
                rdbCreateNewPage.Checked = true;
                rdbUseExistingPage.Checked = false;

                rdbUseExistingPage_CheckedChanged(null, null);
                CheckCreatePage(true);
                chkIsStatic.Checked = true;
            }
            CheckLink(chkIsLink.Checked);
            if ((Request.QueryString.Get("TID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    hdnMenuTypeId.Value = objEncrypt.Decrypt(Request.QueryString.Get("TID"), appFunctions.strKey);
                    hdnPageName.Value = Request.QueryString.Get("Page");

                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                ddlMenuType.SelectedValue = objEncrypt.Decrypt(Request.QueryString.Get("TID"), appFunctions.strKey);
                ddlMenuType_SelectedIndexChanged(null, null);
                objEncrypt = null;
            }
            else if ((Request.QueryString.Get("PID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    strSiteMapId = Convert.ToInt32(objEncrypt.Decrypt(Request.QueryString.Get("PID"), appFunctions.strKey));


                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }


                tblMenuItem objTemp = new tblMenuItem();
                objTemp.Query.AddResultColumn(tblMenuItem.ColumnNames.AppMenuTypeId);
                objTemp.Where.AppMenuItemId.Value = objEncrypt.Decrypt(Request.QueryString.Get("PID"), appFunctions.strKey);
                objTemp.Query.Load();
                if (objTemp.RowCount > 0)
                {
                    hdnMenuTypeId.Value = objTemp.s_AppMenuTypeId;
                    ddlMenuType.SelectedValue = objTemp.s_AppMenuTypeId;
                    ddlMenuType_SelectedIndexChanged(null, null);
                }
                try
                {
                    ddlParent.SelectedValue = objEncrypt.Decrypt(Request.QueryString.Get("PID"), appFunctions.strKey).ToString();
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
            }
            if ((Request.QueryString.Get("ID") != null))
            {
                objEncrypt = new clsEncryption();
                try
                {
                    strSiteMapId = Convert.ToInt32(objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey));
                }
                catch (Exception ex)
                {
                    // noIdFoundRedirect("Employee.aspx");
                }
                objEncrypt = null;
            }
            if (Request.QueryString.Get("ID") != null || Request.QueryString.Get("PID") != null || Request.QueryString.Get("TID") != null)
            {
                GenerateAlias(strSiteMapId);
            }
            SetRegulerExpression();
        }
    }

    public void SetRegulerExpression()
    {
        if (chkIsStatic.Checked)
        {
            RXPageAliasValid.ValidationExpression = RXIsStaticPageAlias;
            RXPageAliasValid.ErrorMessage = "Invalid PageAlias (" + RXIsStaticPageAliasMsg + ")";
        }
        else
        {
            RXPageAliasValid.ValidationExpression = RXIsNotStaticPageAlias;
            RXPageAliasValid.ErrorMessage = "Invalid PageAlias (" + RXIsNotStaticPageAliasMsg + ")";
        }
    }
    public void CheckLink(Boolean IsLink)
    {
        trPageFormat.Visible = !(IsLink);
        rfPageFormats.Enabled = !(IsLink);
        trPageFormatImage.Visible = !(IsLink);

        trOpenInNewTab.Visible = IsLink;
        trLinkUrl.Visible = IsLink;
        rfLinkUrl.Enabled = IsLink;
        reLinkUrl.Enabled = IsLink;

        if (IsLink)
        {
            if (txtLinkURL.Text.EndsWith(".aspx") && (bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == false)
            {
                trIsLink.Visible = false;
                trOpenInNewTab.Visible = false;
                trLinkUrl.Visible = false;
                rfLinkUrl.Enabled = false;
                reLinkUrl.Enabled = false;
            }
        }

    }

    private bool SaveData()
    {

        objMenuItem = new tblMenuItem();
        objCommon = new clsCommon();

        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuItem.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objMenuItem.AddNew();
            objMenuItem.AppCreatedDate = System.DateTime.Now;
            objMenuItem.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        if (chkCreatePage.Checked)
        {
            string strPageAlias = "";
            int intPageID = 0;
            objPage = new tblPage();
            if (rdbEditExisting.Checked)
            {
                objPage.LoadByPrimaryKey(Convert.ToInt32(hdnPageId.Value));
                strPageAlias = txtNewPageAlias.Text;
                intPageID = Convert.ToInt32(hdnPageId.Value);
            }
            else if (rdbUseExistingPage.Checked && txtMenuPageAlias.Text != "")
            {
                objPage.LoadByPrimaryKey(Convert.ToInt32(ddlPages.SelectedValue));
                strPageAlias = txtMenuPageAlias.Text;
                intPageID = Convert.ToInt32(ddlPages.SelectedValue);
            }
            else if (rdbUseExistingPage.Checked && txtMenuPageAlias.Text == "")
            {
                objMenuItem.s_AppPageId = ddlPages.SelectedValue;
                goto saveMenuItem;
            }
            else if (rdbCreateNewPage.Checked)
            {
                objPage.AddNew();
                objPage.s_AppCreatedBy = Session[appFunctions.Session.UserID.ToString()].ToString();
                strPageAlias = txtNewPageAlias.Text;
                objPage.AppCreatedDate = DateTime.Now;
            };

            if (rdbCreateNewPage.Checked || rdbEditExisting.Checked)
            {
                if (checkPage(txtPageName.Text, false, intPageID) != true)
                {
                    DInfo.ShowMessage("The Page name already exists!!", Enums.MessageType.Error);
                    return false;
                }
            }
            if (checkPage(strPageAlias, true, intPageID) != true)
            {
                DInfo.ShowMessage("The Page alias already exists!!", Enums.MessageType.Error);
                return false;
            }
            if (rdbCreateNewPage.Checked || rdbEditExisting.Checked)
            {
                if (chkIsDefault.Checked)
                {
                    tblPage objPageTemp = new tblPage();
                    objPageTemp.ResetDefaultPage();
                    objPageTemp = null;
                    objPage.AppIsDefault = chkIsDefault.Checked;
                }
                else
                {
                    objPage.AppIsDefault = false;
                }
                objPage.AppIsLink = chkIsLink.Checked;
                if (chkIsLink.Checked)
                {
                    objPage.AppLinkURL = txtLinkURL.Text;
                    objPage.AppIsOpenInNewTab = chkIsOpenInNewTab.Checked;

                    objPage.AppPageFormatId = 0;
                }
                else
                {
                    objPage.s_AppPageFormatId = ddlPageFormats.SelectedValue;

                    objPage.AppLinkURL = "";
                    objPage.AppIsOpenInNewTab = false;
                }
                objPage.AppPageContent = txtDescription.Text;
                objPage.AppPageHeading = txtPageHeader.Text;
                objPage.AppPageName = txtPageName.Text;
                objPage.AppPageTitle = txtPageTitle.Text;
                objPage.AppSEODescription = txtSEODescription.Text;
                objPage.AppSEOWord = txtSEOWord.Text;
                objPage.s_AppDynamicParameters = CkEditorDynParameters.Text;
            }
            strPageAlias = strPageAlias.Replace(" ", "-");
            if (chkIsStatic.Checked)
            {
                objPage.AppAlias = strPageAlias;
            }
            else
            {
                objPage.AppAlias = strPageAlias + "/{*name}";
            }
            objPage.Save();
            objMenuItem.AppPageId = objPage.AppPageId;
        }
        else
        {
            objMenuItem.AppPageId = 0;
        }

    saveMenuItem:
        if (ChkIsActive.Checked)
        {
            objMenuItem.AppIsActive = true;
        }
        else
        {
            objMenuItem.AppIsActive = false;
        }
        objMenuItem.s_AppMenuItemTypeID = ddlMenuItemType.SelectedValue;
        objMenuItem.s_AppMenuItem = txtMenuItemName.Text;
        objMenuItem.s_AppParentId = ddlParent.SelectedValue;
        objMenuItem.s_AppMenuTypeId = ddlMenuType.SelectedValue;
        if (hdnPKID.Value == "")
        {
            objMenuItem.AppDisplayOrder = objCommon.GetNextDisplayOrder("tblMenuItem", tblMenuItem.ColumnNames.AppDisplayOrder, "appMenuTypeId = " + objMenuItem.AppMenuTypeId.ToString() + " and appParentID = " + objMenuItem.AppParentId.ToString());
        }
        objMenuItem.Save();
        iMenuItemid = objMenuItem.AppMenuItemId;
        objMenuItem = null;
        objCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuItem = new tblMenuItem();
            if (objMenuItem.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtMenuItemName.Text = objMenuItem.AppMenuItem;
                ddlMenuType.SelectedValue = objMenuItem.s_AppMenuTypeId;
                ddlMenuType_SelectedIndexChanged(null, null);
                ddlParent.SelectedValue = objMenuItem.s_AppParentId;
                ddlMenuItemType.SelectedValue = objMenuItem.s_AppMenuItemTypeID;
                ChkIsActive.Checked = objMenuItem.AppIsActive;

                if (objMenuItem.AppPageId != 0)
                {
                    hdnPageId.Value = objMenuItem.s_AppPageId;
                    rdbEditExisting.Checked = true;
                    rdbEditExisting.Visible = true;
                    trPageAliasOptions.Visible = false;
                    chkCreatePage.Checked = true;
                    SetPageObjValues();
                }
                else
                {
                    chkCreatePage.Checked = false;
                    trPageOptions.Visible = false;
                    divSubtitle.Visible = false;
                    trPageDetails.Visible = false;
                }
            }
            objMenuItem = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        redirectToListing();

    }

    private void ResetControls()
    {
        txtMenuItemName.Text = "";
        ddlMenuType.SelectedValue = "0";
        ddlParent.SelectedValue = "0";
        chkCreatePage.Checked = true;
        rdbCreateNewPage.InputAttributes["checked"] = "True";
        rdbUseExistingPage.Checked = false;
        rdbEditExisting.Checked = false;
        ChkIsActive.Checked = true;
        ddlMenuItemType.SelectedIndex = 0;
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("MenuItem has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("MenuItem has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iMenuItemid.ToString();
            SetValuesToControls();
            PageBase objPageBase = new PageBase();
            objPageBase.RegisterRoute(RouteTable.Routes);
            objPageBase = null;
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "MenuItem has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "MenuItem has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            PageBase objPageBase = new PageBase();
            objPageBase.RegisterRoute(RouteTable.Routes);
            objPageBase = null;
            redirectToListing();
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("MenuItem has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("MenuItem has been updated successfully", Enums.MessageType.Successfull);
            }

            PageBase objPageBase = new PageBase();
            objPageBase.RegisterRoute(RouteTable.Routes);
            objPageBase = null;
            ResetControls();
        }
    }

    protected void rdbUseExistingPage_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbUseExistingPage.Checked)
        {
            ddlPages.Visible = true;
            trPageDetails.Visible = false;
            trPageAliasOptions.Visible = true;
            divSubtitle.Visible = false;
        }
        else
        {
            ddlPages.Visible = false;
            trPageAliasOptions.Visible = false;
            trPageDetails.Visible = true;
            divSubtitle.Visible = true;
            if (rdbEditExisting.Checked)
            {
                SetPageObjValues();
            }
            else
            {
                SetPageObjValues(true);
            }
        }
    }
    public void SetPageObjValues(Boolean setNull = false)
    {
        if (hdnPageId.Value != "")
        {
            objPage = new tblPage();
            if (objPage.LoadByPrimaryKey(Convert.ToInt32(hdnPageId.Value)))
            {
                if (setNull)
                {
                    txtNewPageAlias.Text = "";
                    txtNewPageAlias.Text = "";
                    txtPageHeader.Text = "";
                    txtPageName.Text = "";
                    txtPageTitle.Text = "";
                    txtLinkURL.Text = "";
                    txtSEODescription.Text = "";
                    txtSEOWord.Text = "";
                    txtDescription.Text = "";
                    CkEditorDynParameters.Text = "";
                    chkIsDefault.Checked = false;
                    chkIsDefault.Enabled = true;
                    chkIsLink.Checked = false;
                    chkIsOpenInNewTab.Checked = false;
                    ddlPageFormats.SelectedIndex = 0;
                }
                else if (objPage.RowCount > 0)
                {
                    if (objPage.s_AppAlias.Contains("/{*name}"))
                    {
                        chkIsStatic.Checked = false;
                        txtNewPageAlias.Text = objPage.s_AppAlias.Split('/')[0];
                    }
                    else
                    {
                        txtNewPageAlias.Text = objPage.s_AppAlias;
                        chkIsStatic.Checked = true;
                    }
                    txtPageHeader.Text = objPage.AppPageHeading;
                    txtPageName.Text = objPage.AppPageName;
                    txtPageTitle.Text = objPage.AppPageTitle;
                    txtLinkURL.Text = objPage.AppLinkURL;
                    txtSEODescription.Text = objPage.AppSEODescription;
                    txtSEOWord.Text = objPage.AppSEOWord;
                    txtDescription.Text = objPage.AppPageContent;
                    hdnPageId.Value = objPage.s_AppPageId;
                    CkEditorDynParameters.Text = objPage.s_AppDynamicParameters;
                    if (objPage.AppIsDefault)
                    {
                        chkIsDefault.Checked = objPage.AppIsDefault;
                    }
                    else
                    {
                        chkIsDefault.Checked = false;
                    }
                    if (chkIsDefault.Checked)
                    {
                        chkIsDefault.Enabled = false;
                    }
                    chkIsLink.Checked = objPage.AppIsLink;
                    chkIsOpenInNewTab.Checked = objPage.AppIsOpenInNewTab;
                    ddlPageFormats.SelectedValue = objPage.AppPageFormatId.ToString();
                    //Page.ClientScript.RegisterStartupScript([GetType](), "MyKey", "SEOWordCountCharacter();", True);
                }
            }
            objPage = null;
            ddlPageFormats_SelectedIndexChanged(null, null);
        }
    }
    protected void ddlPageFormats_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlPageFormats.SelectedValue != "0")
        {
            tblPageFormat objPageformat = new tblPageFormat();
            if (objPageformat.LoadByPrimaryKey(Convert.ToInt32(ddlPageFormats.SelectedValue)))
            {
                if (objPageformat.s_AppImage != "")
                {
                    imgPageFormat.ImageUrl = objPageformat.s_AppImage;
                }
                else
                {
                    imgPageFormat.ImageUrl = "";
                }
            }
            else
            {
                imgPageFormat.ImageUrl = "";
            }
        }
        else
        {
            imgPageFormat.ImageUrl = "";
        }
    }
    public void CheckCreatePage(bool IsCreatePage)
    {
        trPageOptions.Visible = IsCreatePage;
        if (rdbUseExistingPage.Checked)
        {
            trPageAliasOptions.Visible = IsCreatePage;
        }
        divSubtitle.Visible = IsCreatePage;
        trPageDetails.Visible = IsCreatePage;
    }
    protected void ddlMenuType_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlMenuType.SelectedIndex != 0)
        {
            DataTable dt = new DataTable();
            DataRow dr;
            tblMenuItem objSetMenuItem = new tblMenuItem();

            objMenuType = new tblMenuType();
            objMenuType.Where.AppMenuTypeId.Value = ddlMenuType.SelectedValue;
            objMenuType.Query.AddResultColumn(tblMenuType.ColumnNames.AppNoOfLevel);
            objMenuType.Query.Load();
            dt = objSetMenuItem.getMenuItem(Convert.ToInt32(ddlMenuType.SelectedValue), Convert.ToInt32(objMenuType.AppNoOfLevel));

            dr = dt.NewRow();
            dr["appMenuItemId"] = 0;
            dr["appMenuItem"] = "-- Select Parent --";
            dt.Rows.InsertAt(dr, 0);

            ddlParent.DataSource = dt;
            ddlParent.DataValueField = tblMenuItem.ColumnNames.AppMenuItemId;
            ddlParent.DataTextField = tblMenuItem.ColumnNames.AppMenuItem;
            ddlParent.DataBind();
            objMenuType = null;
            objSetMenuItem = null;
        }
    }
    public void GenerateAlias(int intId)
    {
        DataTable dt;
        tblMenuType objMenuType = new tblMenuType();
        objMenuItem = new tblMenuItem();
        objEncrypt = new clsEncryption();
        dt = objMenuItem.GetSiteMap(intId);
        strSiteMap = dt.Rows[0][0].ToString().Trim('/');
        if (hdnMenuTypeId.Value == "")
        {
            dt = objMenuItem.GetMenuType(intId, false);
        }
        else
        {
            dt = objMenuItem.GetMenuType(Convert.ToInt32(hdnMenuTypeId.Value), true);
        }
        string[] SplitSiteMapString = strSiteMap.Split('/');
        objMenuType.Where.AppMenuTypeId.Value = ddlMenuType.SelectedValue;
        objMenuType.Query.Load();
        strSiteMap = "<a href='MasterMenus.aspx'>Menu Items</a>";
        if (objMenuType.RowCount > 0)
        {
            strSiteMap += " >  <a href=MenuItems.aspx?Id=" + objEncrypt.Encrypt(dt.Rows[0]["appMenuTypeId"].ToString(), appFunctions.strKey) + "&type=mtype>" + dt.Rows[0]["appMenuTypeName"].ToString() + "</a>";

        }
        if (objMenuType.AppNoOfLevel > SplitSiteMapString.Length - 1)
        {
            for (int i = 0; i <= SplitSiteMapString.Length - 1; i++)
            {
                string[] SplittedItems = SplitSiteMapString[i].Split(',');
                aliasPrefix += SplittedItems[0] + "/";
                if (i == SplitSiteMapString.Length - 1)
                {
                    strSiteMap += " > " + SplittedItems[0];
                }
                else
                {
                    strSiteMap += "> <a href='MenuItems.aspx?Id=" + objEncrypt.Encrypt(SplittedItems[1].ToString(), appFunctions.strKey) + "'>" + SplittedItems[0].ToString() + "</a>";


                }

            }
            siteMapLiteral.Text = strSiteMap.ToString();
            if (aliasPrefix.Length > 0)
            {
                aliasPrefix = aliasPrefix.Substring(0, aliasPrefix.Length - 1);
                aliasPrefix = aliasPrefix.Substring(0, aliasPrefix.LastIndexOf("/") + 1);
            }
            hdnAliasPrefix.Value = aliasPrefix.Replace(" ", "-");
        }
        else
        {
            Session[appFunctions.Session.ShowMessage.ToString()] = "You Can't Add Menu Item For This Level As Maximum No. Of Level Is " + objMenuType.AppNoOfLevel.ToString();
            Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Error;

            if (Request.QueryString.Get("PID") != null)
            {
                Response.Redirect("MenuItems.aspx?Id=" + Request.QueryString.Get("PID"), true);
            }
            else if (Request.QueryString.Get("TID") != null)
            {
                Response.Redirect("MenuItems.aspx?Id=" + Request.QueryString.Get("TID") + "&type=mtype", true);
            }
            else if (hdnPKID.Value == "")
            {
                Response.Redirect("Dashboard.aspx");
            }
        }
        objMenuItem = null;
        objMenuType = null;
    }


    public bool checkPage(string strMenuAlias, bool checkAlias, int intID = 0)
    {
        tblPage objTempPage = new tblPage();
        if (checkAlias)
        {
            objTempPage.Where.AppAlias.Value = strMenuAlias;
        }
        else
        {
            objTempPage.Where.AppPageName.Value = strMenuAlias;
        }
        if (intID != 0)
        {
            objTempPage.Where.AppPageId.Value = intID;
            objTempPage.Where.AppPageId.Operator = MyGeneration.dOOdads.WhereParameter.Operand.NotEqual;
        }
        objTempPage.Query.AddResultColumn(tblPage.ColumnNames.AppPageId);
        objTempPage.Query.Load();
        if (objTempPage.RowCount > 0)
        {
            return false;
        }
        objTempPage = null;
        return true;
    }
    public void redirectToListing()
    {
        objEncrypt = new clsEncryption();
        if (hdnPageName.Value != "MasterMenusList.aspx")
        {
            if (ddlParent.SelectedValue != "0")
            {
                Response.Redirect("MenuItems.aspx?ID=" + objEncrypt.Encrypt(ddlParent.SelectedValue, appFunctions.strKey));
            }
            else
            {
                Response.Redirect("MenuItems.aspx?ID=" + objEncrypt.Encrypt(ddlMenuType.SelectedValue, appFunctions.strKey) + "&type=mtype");
            }
        }
        else
        {
            Response.Redirect("MasterMenus.aspx");
        }

    }
    protected void ddlParent_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlParent.SelectedIndex != 0)
        {
            GenerateAlias(Convert.ToInt32(ddlParent.SelectedValue));
        }
        else
        {
            hdnAliasPrefix.Value = "";
        }
    }
    protected void ChkIsLink_CheckedChanged(object sender, EventArgs e)
    {
        CheckLink(chkIsLink.Checked);
    }
    protected void chkCreatePage_CheckedChanged(object sender, EventArgs e)
    {
        CheckCreatePage(chkCreatePage.Checked);
    }
    protected void btnAutoAlias_Click(object sender, EventArgs e)
    {
        txtMenuPageAlias.Text = generatePageAlias(ddlPages.SelectedItem.Text);

    }
    public string generatePageAlias(string strPageName)
    {
        string strAlias = "";
        if (strPageName != "")
        {
            if (ddlParent.SelectedIndex > 0)
            {
                objMenuItem = new tblMenuItem();
                DataTable dtMenuItems = objMenuItem.GetSiteMapDT(Convert.ToInt32(ddlParent.SelectedValue));
                if (dtMenuItems.Rows.Count > 0)
                {
                    for (int i = 0; i <= dtMenuItems.Rows.Count - 1; i++)
                    {
                        if (dtMenuItems.Rows[i][tblPage.ColumnNames.AppAlias].ToString() != "")
                        {
                            strAlias += dtMenuItems.Rows[i][tblPage.ColumnNames.AppAlias].ToString() + "/";
                        }
                    }
                }
            }
            strAlias += strPageName.Replace(" ", "-");
            strAlias = strAlias.Replace("_", "-");
        }
        else
        {
            DInfo.ShowMessage("Please enter page name to generate alias", Enums.MessageType.Warning);
        }
        return strAlias;
    }
    protected void btnPageAutoAlias_Click(object sender, EventArgs e)
    {
        txtNewPageAlias.Text = generatePageAlias(txtPageName.Text);
    }
    protected void chkIsStatic_CheckedChanged(object sender, EventArgs e)
    {
        SetRegulerExpression();
    }
    protected void rdbEditExisting_CheckedChanged(object sender, EventArgs e)
    {

        if (rdbUseExistingPage.Checked)
        {
            ddlPages.Visible = true;
            trPageDetails.Visible = false;
            trPageAliasOptions.Visible = true;
            divSubtitle.Visible = false;
        }
        else
        {
            ddlPages.Visible = false;
            trPageAliasOptions.Visible = false;
            trPageDetails.Visible = true;
            divSubtitle.Visible = true;
            if (rdbEditExisting.Checked)
            {
                SetPageObjValues();
            }
            else
            {
                SetPageObjValues(true);
            }
        }
    }
    protected void rdbCreateNewPage_CheckedChanged(object sender, EventArgs e)
    {
        if (rdbUseExistingPage.Checked)
        {
            ddlPages.Visible = true;
            trPageDetails.Visible = false;
            trPageAliasOptions.Visible = true;
            divSubtitle.Visible = false;
        }
        else
        {
            ddlPages.Visible = false;
            trPageAliasOptions.Visible = false;
            trPageDetails.Visible = true;
            divSubtitle.Visible = true;
            if (rdbEditExisting.Checked)
            {
                SetPageObjValues();
            }
            else
            {
                SetPageObjValues(true);
            }
        }
    }
}
