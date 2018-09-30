using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.IO;
using System.Drawing;


public partial class MenuItemTypeDetail : PageBase_Admin
{
    tblMenuItemType objMenuItemType;
    clsEncryption objEncrypt;
    clsCommon objClsCommon;
    int iMenuItemTypeid = 0;
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            lnkSaveAndAddnew.Visible = HasAdd;
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
        if (objClsCommon.IsRecordExists("tblMenuItemType", tblMenuItemType.ColumnNames.AppMenuItemType, tblMenuItemType.ColumnNames.AppMenuItemTypeID, txtMenuItemType.Text.Trim(), hdnPKID.Value))
        {
            DInfo.ShowMessage("Menu iten type is already exists", Enums.MessageType.Warning);
            return false;
        }
      

        objMenuItemType = new tblMenuItemType();
       
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuItemType.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value));
        }
        else
        {
            objMenuItemType.AddNew();
            objMenuItemType.AppDisplayOrder = objClsCommon.GetNextDisplayOrder("tblMenuItemType", tblMenuItemType.ColumnNames.AppDisplayOrder);
            objMenuItemType.AppCreateDate = System.DateTime.Now;
            objMenuItemType.AppCreateBy = Convert.ToInt32(Session[appFunctions.Session.UserID.ToString()]);
        }
        objMenuItemType.s_AppMenuItemType = txtMenuItemType.Text;
        objMenuItemType.AppIsActive = chkIsActive.Checked;
        objMenuItemType.Save();
        iMenuItemTypeid = objMenuItemType.AppMenuItemTypeID;
        objMenuItemType = null;
        objClsCommon = null;
        return true;
    }

    private void SetValuesToControls()
    {
        if (!string.IsNullOrEmpty(hdnPKID.Value) && hdnPKID.Value != "")
        {
            objMenuItemType = new tblMenuItemType();
            if (objMenuItemType.LoadByPrimaryKey(Convert.ToInt32(hdnPKID.Value)))
            {
                txtMenuItemType.Text = objMenuItemType.s_AppMenuItemType;
               
                if (objMenuItemType.AppIsActive)
                {
                     chkIsActive.Checked =true;
                }
                else
                {
                     chkIsActive.Checked =false ;
                }
            }
            objMenuItemType = null;
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("MenuItemType.aspx", true);
    }

    private void ResetControls()
    {

        txtMenuItemType.Text = "";
        chkIsActive.Checked = true;
        hdnPKID.Value = "";
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Menu item type has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Menu item type has been updated successfully", Enums.MessageType.Successfull);
            }
            hdnPKID.Value = iMenuItemTypeid.ToString();
            SetValuesToControls();
        }
    }
    protected void lnkSaveAndClose_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Menu item type has been added successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            else
            {
                Session[appFunctions.Session.ShowMessage.ToString()] = "Menu item type has been updated successfully";
                Session[appFunctions.Session.ShowMessageType.ToString()] = Enums.MessageType.Successfull;
            }
            Response.Redirect("MenuItemType.aspx");
        }
    }

    protected void lnkSaveAndAddnew_Click(object sender, EventArgs e)
    {
        if (SaveData())
        {
            if (string.IsNullOrEmpty(hdnPKID.Value))
            {
                DInfo.ShowMessage("Menu item type has been added successfully", Enums.MessageType.Successfull);
            }
            else
            {
                DInfo.ShowMessage("Menu item type has been updated successfully", Enums.MessageType.Successfull);
            }
            ResetControls();
        }
    }

}