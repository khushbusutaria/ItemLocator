using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Data.SqlClient;
using MyGeneration.dOOdads;
using System.Net.Mail;
using BusinessLayer;
using System.Web.UI.WebControls;

public class appFunctions
{
    #region "Enumaration"
    public static int intMaxImgFileSize = 99999;

    public enum Session
    {
        UserID,

        IsSuperAdmin,
        RoleID,
        UserName,
        ShowMessage,
        ShowMessageType,
        DataTable,
        SelectedFields,
        LoadMenu,
        QueryString,
        PageName,
        FromPage,
        EmployeeName,
        ClientUserID,
        ClientUserName,

        MemberID,
        MemberUsername,

        SearchWhere,
        CurrencyID,
        CurrencyImage,
        CurrencyName,
        CurrencyInRupee,
        DefaultCurrencyCode,
        DefaultCurrencyImage,
        LeadDataTable,
        UserLogID,

        PaymetnOrderId,
        PaymenthasId,
        PaymnetTransactionId,
        PaymentEmailString,

        Search,
        LeadFiled,
        DesignationID,
        //UnderEmployeeId,
        //FirstLeveEmployeeId,
        LevelEmployeeId,
        ISAdvanceSearch,
        ProductID,
        ProductColorId,
        Cart
    }

    public enum Enum_SortOrderBy
    {
        Asc,
        Desc
    }

    #endregion


    public static string strKey = "r0b1nr0y";
    public static bool AddProduct = false;
    public static string Extension = ".jpeg,.gif,.png,.jpg,.tiff,.tif,.bmp";
    public string RXEmailRegularExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    //"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
    public string RXPhoneRegularExpression = "[0-9]{10}";
    public string RXEmailRegularExpressionMsg = "Ex:abc@xyz.com";
    public string RXPhoneRegularExpressionMsg = "Ex:1234567890";

    public string RXURLRegularExpressionMsg = "Ex:http://www.xyz.com";

    public string RXURLRegularExpression = "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";

    public void FillQuantity(int intQuantityID, DropDownList SizeDropDownList, DropDownList QuantityDropDownList)
    {
        QuantityDropDownList.Items.Clear();
        if (SizeDropDownList.Items.Count != 1)
        {
            if (intQuantityID >= 10)
            {
                for (int i = 1; i <= 10; i++)
                {
                    QuantityDropDownList.Items.Add(i.ToString());
                }
            }
            else
            {
                for (int i = 1; i <= intQuantityID; i++)
                {
                    QuantityDropDownList.Items.Add(i + "");
                }
            }
        }
    }
}

