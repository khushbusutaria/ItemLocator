using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;
using System.Web.Routing;


public partial class PageDetail : PageBase_Admin
{
    tblPage objpage;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iPageid = 0;
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

            objClsCommon = new clsCommon();
            objClsCommon.FillDropDownList(ddlPageFormats, "tblPageFormat", tblPageFormat.ColumnNames.AppPageFormatName, tblPageFormat.ColumnNames.AppPageFormatId, "-- Select Page Format --", tblPageFormat.ColumnNames.AppPageFormatId, appFunctions.Enum_SortOrderBy.Asc, " appIsActive = 'true' ");
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
            else
            {
                ChkIsLink.Checked = false;

                chkIsStatic.Checked = true;
            }
            CheckLink(ChkIsLink.Checked);
            SetRegulerExpression();
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
    private bool SaveData()
    {

        objpage = new tblPage();

        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objpage.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objpage.AddNew();
            objpage.AppCreatedDate = System.DateTime.Now;
            objpage.AppCreatedBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objpage.AppPageName = txtPageName.Text;

        objpage.AppPageTitle = txtPageTitle.Text;

        objpage.AppSEOWord = txtSEOWord.Text;
        objpage.AppSEODescription = txtSEODescription.Text;
        objpage.AppPageContent = ckePageContent.Text;
        if (chkIsStatic.Checked)
        {
            objpage.AppAlias = txtPageAlias.Text;
        }
        else
        {
            objpage.AppAlias = txtPageAlias.Text + "/{*name}";
        }
        if (txtPageAlias.Text != "")
        {
            tblPage objCheckAliasExist = new tblPage();
            objCheckAliasExist.Where.AppAlias.Value = objpage.AppAlias;
            objCheckAliasExist.Query.AddResultColumn(tblPage.ColumnNames.AppPageId);
            objCheckAliasExist.Query.Load();
            if (objCheckAliasExist.RowCount > 0)
            {
                if (objCheckAliasExist.AppPageId.ToString() != hdnPKID.Value.ToString())
                {
                    DInfo.ShowMessage("The Page alias already exists!!", Enums.MessageType.Error);
                    return false;
                }
            }
            objCheckAliasExist = null;
        }
        objpage.AppPageHeading = txtPageHeader.Text;

        if (chkIsDefault.Checked)
        {
            tblPage objPageTemp = new tblPage();
            objPageTemp.ResetDefaultPage();
            objPageTemp = null;
            objpage.AppIsDefault = chkIsDefault.Checked;
        }
        else
        {
            objpage.AppIsDefault = false;
        }
        objpage.AppIsLink = ChkIsLink.Checked;
        if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()] == true)
        {
            objpage.s_AppDynamicParameters = CkEditorDynParameters.Text;
        }
        if (ChkIsLink.Checked)
        {
            objpage.AppPageFormatId = 0;
            objpage.AppIsOpenInNewTab = chkIsOpenInNewTab.Checked;
            objpage.AppLinkURL = txtLinkURL.Text;
        }
        else
        {
            objpage.s_AppPageFormatId = ddlPageFormats.SelectedValue;
            objpage.AppIsOpenInNewTab = false;
            objpage.s_AppLinkURL = "";
        }


        objpage.Save();
        iPageid = objpage.AppPageId;
        objpage = null;

        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objpage = new tblPage();
            if (objpage.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtPageName.Text = objpage.AppPageName;
                txtLinkURL.Text = objpage.AppLinkURL;
                chkIsOpenInNewTab.Checked = objpage.AppIsOpenInNewTab;

                if (objpage.s_AppAlias.Contains("/"))
                {
                    chkIsStatic.Checked = false;
                    txtPageAlias.Text = objpage.s_AppAlias.Split('/')[0];
                }
                else
                {
                    txtPageAlias.Text = objpage.s_AppAlias;
                    chkIsStatic.Checked = true;
                }
                txtPageHeader.Text = objpage.AppPageHeading;
                txtPageTitle.Text = objpage.AppPageTitle;
                txtSEODescription.Text = objpage.AppSEODescription;
                txtSEOWord.Text = objpage.AppSEOWord;
                ckePageContent.Text = objpage.AppPageContent;
                ddlPageFormats.SelectedValue = objpage.s_AppPageFormatId;
                ddlPageFormats_SelectedIndexChanged(null, null);

                if (objpage.AppIsDefault)
                {
                    chkIsDefault.Checked = objpage.AppIsDefault;
                }
                else
                {
                    chkIsDefault.Checked = false;
                }
                if (chkIsDefault.Checked)
                {
                    chkIsDefault.Enabled = false;
                }
                if (objpage.AppIsLink)
                {
                    ChkIsLink.Checked = true;
                }
                else
                {
                    ChkIsLink.Checked = false;
                }
                //if ((bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
                //{
                //    if (objpage.s_AppDynamicParameters != "")
                //    {
                //        trdynParameters.Visible = true;

                //        CkEditorDynParameters.Visible = false;
                //    }
                //    else
                //    {
                //        trdynParameters.Visible = false;
                //    }
                //}
                //else
                //{
                //    trdynParameters.Visible = true;

                //    CkEditorDynParameters.Visible = true;
                //    CkEditorDynParameters.Text = objpage.s_AppDynamicParameters;
                //}
            }
            objpage = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Page.aspx", true);
    }

    private void ResetControls()
    {
        txtPageName.Text = "";
        txtLinkURL.Text = "";
        txtPageAlias.Text = "";
        txtPageHeader.Text = "";
        txtPageTitle.Text = "";
        txtSEODescription.Text = "";
        txtSEOWord.Text = "";
        ckePageContent.Text = "";
        if (!(bool)Session[appFunctions.Session.IsSuperAdmin.ToString()])
        {
            trdynParameters.Visible = false;
        }
        else
        {
            trdynParameters.Visible = true;

            CkEditorDynParameters.Visible = true;
            CkEditorDynParameters.Text = "";
        }
        ChkIsLink.Checked = true;
        chkIsDefault.Checked = false;
        chkIsDefault.Enabled = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Page has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Page has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iPageid.ToString();
            SetValuesToControls();
            PageBase objpageBase = new PageBase();
            objpageBase.RegisterRoute(RouteTable.Routes);
            objpageBase = null;
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Page has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Page has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            PageBase objpageBase = new PageBase();
            objpageBase.RegisterRoute(RouteTable.Routes);
            objpageBase = null;
            Response.Redirect("Page.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Page has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Page has been updated successfully", Enums.MessageType.Successfull);
            }
            PageBase objpageBase = new PageBase();
            objpageBase.RegisterRoute(RouteTable.Routes);
            objpageBase = null;
            ResetControls();
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
    protected void btnAutoAlias_Click(object sender, EventArgs e)
    {
        string strAlias = "";
        strAlias = generateAlias();
        if (strAlias != ".html" && strAlias != "")
        {
            txtPageAlias.Text = strAlias;
        }
    }
    public string generateAlias()
    {
        string strAlias = "";
        if (txtPageName.Text != "")
        {
            strAlias = txtPageName.Text.Replace(" ", "-");
            strAlias = strAlias.Replace("_", "-");
        }
        else
        {
            DInfo.ShowMessage("Please enter page name to generate alias", Enums.MessageType.Warning);
        }
        return strAlias;
    }
    protected void ChkIsLink_CheckedChanged(object sender, EventArgs e)
    {
        CheckLink(ChkIsLink.Checked);
    }
    protected void chkIsStatic_CheckedChanged(object sender, EventArgs e)
    {
        SetRegulerExpression();
    }
}