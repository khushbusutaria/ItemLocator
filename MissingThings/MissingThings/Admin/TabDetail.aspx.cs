using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;

public partial class TabDetail : PageBase_Admin
{

    string fileAppend = DateTime.Now.Date.Day.ToString() + "_" + DateTime.Now.Date.Month.ToString() + DateTime.Now.Date.Year.ToString() + DateTime.Now.TimeOfDay.Hours.ToString() + "_" + DateTime.Now.TimeOfDay.Minutes.ToString() + DateTime.Now.TimeOfDay.Seconds.ToString() + "_";
    string strImagePath = "../Admin/Uploads/TabImages/";
    tblTab objTab = new tblTab();
    clsCommon objClsCommon = new clsCommon();
    int intParentId;

    protected void Page_Load(object sender, System.EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            btnSaveAndAddnew.Visible = HasAdd;
            btnClear.Visible = HasAdd;

            SetUpDrowDown();
            objEncrypt = new clsEncryption();


            if ((Request.QueryString.Get("ID") != null))
            {
                try
                {
                    hdnPKID.Value = objEncrypt.Decrypt(Request.QueryString.Get("ID"), appFunctions.strKey);
                }
                catch (Exception ex)
                {
                    noIdFoundRedirect("Tab.aspx");
                }

                objEncrypt = null;
                SetValuesToControls();

            }


            if ((Request.QueryString.Get("PID") != null))
            {

                try
                {
                    intParentId = Convert.ToInt32(objEncrypt.Decrypt(Request.QueryString.Get("PID"), appFunctions.strKey));
                }
                catch (Exception ex)
                {
                    noIdFoundRedirect("Tab.aspx");
                }

                ddlParent.SelectedValue = intParentId.ToString();

                objTab = new tblTab();
                objTab.LoadByPrimaryKey(intParentId);
                objTab.Query.AddResultColumn(tblTab.ColumnNames.AppAddPage);
                objTab.Query.Load();

                ////Set Value To Controls
                //txtAddPage.Text = objTab.AppAddPage;

                objTab.Where.WhereClauseReset();
                objTab = null;
            }
        }

    }

    private void SetUpDrowDown()
    {
        objClsCommon.FillDropDownList(ddlParent, "tblTab", tblTab.ColumnNames.AppTabName, tblTab.ColumnNames.AppTabID, "No Parent", tblTab.ColumnNames.AppTabID, appFunctions.Enum_SortOrderBy.Asc);
    }

    protected void btnBack_Click(object sender, System.EventArgs e)
    {
        // Response.Redirect("TabsList.aspx")
        redirectToListing();
    }

    protected void btnSaveAndClose_Click(object sender, System.EventArgs e)
    {

        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Tab has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Tab has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }

            //Response.Redirect("Tab.aspx");
            redirectToListing();
        }
    }

    private bool SaveData()
    {

        objTab = new tblTab();

        if (!string.IsNullOrEmpty(hdnPKID.Value))
        {
            objTab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objTab.AddNew();
            objTab.AppCreatedDate = System.DateTime.Now;
            objTab.AppCreatedBy = (int)Session[appFunctions.Session.UserID.ToString()];
        }

        objTab.AppTabName = txtTabName.Text;

        if (objTab.s_AppParentID != ddlParent.SelectedValue.ToString())
        {

            objTab.AppDisplayOrder = objClsCommon.GetNextDisplayOrder("tblTab", tblTab.ColumnNames.AppDisplayOrder, " appParentID = " + ddlParent.SelectedValue);

        }
        objTab.AppParentID = Convert.ToInt32(ddlParent.SelectedValue);

        if (!string.IsNullOrEmpty(txtWebPageName.Text) & txtWebPageName.Text != "#")
        {
            tblTab objTemp = new tblTab();
            objTemp.Where.AppWebPageName.Value = txtWebPageName.Text;
            objTemp.Query.AddResultColumn(tblTab.ColumnNames.AppTabID);
            objTemp.Query.Load();

            if (objTemp.RowCount > 0)
            {
                if (objTemp.AppTabID != Convert.ToInt32(hdnPKID.Value))
                {
                    DInfo.ShowMessage("The Specified Web Page is already allocated to another tab", Enums.MessageType.Error);
                }
            }

            objTab.AppWebPageName = txtWebPageName.Text;
        }
        else
        {
            objTab.AppWebPageName = "#";
        }


        objTab.AppIsActive = ChkIsActive.Checked;
        objTab.AppIsAdd = chkIsAdd.Checked;
        objTab.AppIsEdit = chkIsEdit.Checked;
        objTab.AppIsDelete = chkIsDelete.Checked;

        if (chkHasAddOption.Checked == true)
        {
            objTab.AppAddPage = txtAddPage.Text;
        }
        else
        {
            objTab.AppAddPage = "";
        }

        if (chkIsShowOnDashboard.Checked == true)
        {
            objTab.AppIsShowOnDashboard = true;

            if ((FileUploadIcon.HasFile))
            {
                //string fileName = System.IO.Path.GetFileNameWithoutExtension(FileUploadIcon.FileName).ToString();
                //string extension = System.IO.Path.GetExtension(FileUploadIcon.FileName).ToString();
                //FileUploadIcon.SaveAs(Server.MapPath(strImagePath) + fileName + fileAppend + extension);


                //try
                //{
                //    System.IO.File.Delete(Server.MapPath(".") + Server.MapPath(strImagePath) + objTab.AppIconPath);

                //}
                //catch (Exception ex)
                //{
                //}

                //objTab.AppIconPath = fileName + fileAppend + extension;
                string strError = "";
                string Time = Convert.ToString(DateTime.Now.Month) + Convert.ToString(DateTime.Now.Day) + Convert.ToString(DateTime.Now.Year) + Convert.ToString(DateTime.Now.Hour) + Convert.ToString(DateTime.Now.Minute) + Convert.ToString(DateTime.Now.Second);
                string strPath = objClsCommon.FileUpload_Images(FileUploadIcon.PostedFile, txtTabName.Text.Trim().Replace(" ", "_") + "_" + Time, "Uploads/TabImages/", ref strError, 0, objTab.AppIconPath);
                if (strError == "")
                {
                    objTab.AppIconPath = strPath;
                }
                else
                {
                    DInfo.ShowMessage(strError, Enums.MessageType.Error);
                    return false;
                }


            }

        }
        else
        {
            objTab.AppIsShowOnDashboard = false;
            objTab.AppIconPath = "";
        }

        ////If IsMenu Is True 
        if (chkIsMenu.Checked == true)
        {
            ////Make All Others IsMenu To False
            tblTab objTemp = new tblTab();
            objTemp.updateIsMenu();

            objTab.AppIsMenu = true;
        }
        else
        {
            objTab.AppIsMenu = false;
        }

        objTab.Save();
        objTab = null;

        return true;
    }

    protected void btnSaveAndAddnew_Click(object sender, System.EventArgs e)
    {

        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Tab has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Tab has been updated successfully", Enums.MessageType.Successfull);
            }

            Resetcontrols();
            hdnPKID.Value = "";
        }
    }

    private void Resetcontrols()
    {
        txtTabName.Text = "";
        txtAddPage.Text = "";
        txtWebPageName.Text = "";
        ChkIsActive.Checked = true;
        chkIsAdd.Checked = true;
        chkIsDelete.Checked = true;
        chkIsEdit.Checked = true;
        chkHasAddOption.Checked = true;
        ddlParent.SelectedIndex = 0;
    }

    protected void btnClear_Click(object sender, System.EventArgs e)
    {
        Resetcontrols();
        hdnPKID.Value = "";
    }

    private void SetValuesToControls()
    {
        objTab = new tblTab();
        objTab.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));

        if (objTab.RowCount > 0)
        {
            txtTabName.Text = objTab.AppTabName;
            ddlParent.SelectedValue = objTab.AppParentID.ToString();

            if (objTab.AppWebPageName != "#")
            {
                txtWebPageName.Text = objTab.AppWebPageName;
            }
            else
            {
                txtWebPageName.Text = "";
            }

            ChkIsActive.Checked = objTab.AppIsActive;
            chkIsAdd.Checked = objTab.AppIsAdd;
            chkIsEdit.Checked = objTab.AppIsEdit;
            chkIsDelete.Checked = objTab.AppIsDelete;
            chkIsMenu.Checked = objTab.AppIsMenu;

            if (objTab.AppIsShowOnDashboard == true)
            {
                chkIsShowOnDashboard.Checked = true;

                if ((objTab.AppIconPath.ToString() != null))
                {
                    imgIconPreview.ImageUrl = (strImagePath) + objTab.AppIconPath;
                }

                RegexpImageValid.Enabled = false;
                reImage.Enabled = false;
            }
            else
            {
                chkIsShowOnDashboard.Checked = false;
                RegexpImageValid.Enabled = false;
                reImage.Enabled = true;
            }

            if (!string.IsNullOrEmpty(objTab.AppAddPage))
            {
                txtAddPage.Text = objTab.AppAddPage;
                chkHasAddOption.Checked = true;
            }
            else
            {
                txtAddPage.Text = objTab.AppAddPage;
                chkHasAddOption.Checked = false;
            }

        }

    }

    public void redirectToListing()
    {
        objEncrypt = new clsEncryption();

        if (ddlParent.SelectedValue != "0")
        {
            Response.Redirect("Tab.aspx?ID=" + objEncrypt.Encrypt(ddlParent.SelectedValue, appFunctions.strKey));
        }
        else
        {
            Response.Redirect("Tab.aspx");
        }

    }

}