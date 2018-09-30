using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Web.Routing;
using BusinessLayer;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.UI;
using System.Text.RegularExpressions;


public class PageBase : System.Web.UI.Page
{


    public string strClientImagePath = "admin/";
    public string strInputDateFormat = System.Configuration.ConfigurationManager.AppSettings["InputDateFormat"];
    public string strOutputDateFormat = System.Configuration.ConfigurationManager.AppSettings["OutputDateFormat"];

    public string strMMddyyyy = "MM/dd/yyyy";
    public string strUserName = "";
    public int strUserID = 0;
    public string strBranchID = "";
    public string strBranchCode = "";
    public string strCurrentYearID = "";
    public bool IsSuperAdmin = false;
    public string strPageName = "";

    public int intCurrentLanguageID = 0;
    public bool HasAdd = false;
    public bool HasEdit = false;

    public bool HasDelete = false;

    public clsCommon objCommon;
    public DataTable objDataTable;

    public clsEncryption objEncrypt = new clsEncryption();

    public appFunctions objAppFunctions;

    public string RXGeneralRegularExpression = "^[\\sA-Za-z0-9&@_-[,]]*$";
    public string RXDescriptionRegularExpression = "[^'\"<>]*";
    public string RXEmailRegularExpression = "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*";
    //"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}"
    public string RXPhoneRegularExpression = "[0-9]{10}";
    //Public RXDateRegularExpression As String = "^(((0[1-9]|[12]\d|3[01])\-(0[13578]|1[02])\-((19|[2-9]\d)\d{2}))|((0[1-9]|[12]\d|30)\/(0[13456789]|1[012])\/((19|[2-9]\d)\d{2}))|((0[1-9]|1\d|2[0-8])\/02\/((19|[2-9]\d)\d{2}))|(29\/02\/((1[6-9]|[2-9]\d)(0[48]|[2468][048]|[13579][26])|((16|[2468][048]|[3579][26])00))))$"

    public string RXDateRegularExpression = "^(((((0[1-9])|(1\\d)|(2[0-8]))-((0[1-9])|(1[0-2])))|((31-((0[13578])|(1[02])))|((29|30)-((0[1,3-9])|(1[0-2])))))-((19[0-9][0-9])|(20[0-9][0-9]))|(29-02-20(([02468][048])|([13579][26]))))$";
    public string RXPinRegularExpression = "\\d{6}";
    public string RXUNCPathRegularExpression = "^\\\\\\\\\\w+\\\\\\w+";
    public string RXPathRegularExpression = "\\b[a-zA-Z]:\\\\[^/:*?\"<>|\\r\\n]*";
    public string RXFileExtensionExpression = "^[.][\\sA-Za-z0-9]*$";
    public string RXNumericRegularExpression = "[0-9]*";
    public string RXDecimalRegularExpression = "^\\d*[0-9]\\d*(\\.\\d+)?$";
    public string RXFileNameRegularExpression = "[^/:*?\"<>|\\r\\n]*";
    public string RXURLRegularExpression = "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?";
    public string RXURLWithoutWWWRegularExpression = "^((ftp|http|https):\\/\\/)?([a-zA-Z0-9]+(\\.[a-zA-Z0-9]+)+.*)$";
    public string RXIpAddressRegularExpression = "\\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\\b";
    public string RXPasswordRegularExpression = ".{8}.*";
    public string RXDateDDMMMYYYYRegularExpression = "^(0?[1-9]|[12][0-9]|3[01])-(Jan|Feb|Mar|Apr|May|Jun|Jul|Aug|Sep|Oct|Nov|Dec)-(19|20)\\d\\d$";
    public string RXSQlColumnNameRegularExpression = "^[a-zA-z]+$";
    public string RXIsStaticPageAlias = "^([a-z A-Z_\\/.\\-0-9]+)$";

    public string RXIsNotStaticPageAlias = "^([a-z A-Z_.\\-0-9]+)$";


    public string RXGeneralRegularExpressionMsg = "Enter aA-zZ 0-9@ _and-";
    public string RXDescriptionRegularExpressionMsg = "Enter aA-zZ 0-9@ _and-";
    public string RXEmailRegularExpressionMsg = "Ex:abc@xyz.com";
    public string RXPhoneRegularExpressionMsg = "Ex:1234567890";
    public string RXDateRegularExpressionMsg = "Ex:31-01-2011";
    public string RXPinRegularExpressionMsg = "Ex:123456";
    public string RXUNCPathRegularExpressionMsg = "Ex:\\\\ServerName\\sharedFoldername.";
    public string RXPathRegularExpressionMsg = "Ex:C:\\Dir_Name";
    public string RXFileExtensionExpressionMsg = "Ex:.txt[only a-zA-Z0-9 valid]";
    public string RXNumericRegularExpressionMsg = "Enter 0-9";
    public string RXDecimalRegularExpressionMsg = "Ex. 5 or 5.5";
    public string RXURLRegularExpressionMsg = "Ex:http://www.xyz.com";
    public string RXURLWithoutWWWRegularExpressionMsg = "Ex:http://xyz.com";
    public string RXPasswordRegularExpressionMsg = "Minimum password length is 8";
    public string RXDateDDMMMYYYYRegularExpressionMsg = "Ex:31-Dec-2011";
    public string RXSQlColumnNameRegularExpressionMsg = "Column Name should contain special character or numeric Values ";
    public string RXIsStaticPageAliasMsg = "Invalid PageAlias : Only Alphabet ,Dash ,slash & Underscore Allowed";
    public string RXIsNotStaticPageAliasMsg = "Invalid PageAlias : Only Alphabet ,Dash & Underscore Allowed";

    public static string GetServerURL()
    {
        string ServerName = HttpContext.Current.Request.ServerVariables["HTTP_HOST"];
        string sAppPath = HttpContext.Current.Request.ApplicationPath;

        //Dim strServerPort As String = HttpContext.Current.Request.ServerVariables("SERVER_PORT")
        string SURL = "";
        string Protocol = null;
        //= HttpContext.Current.Request.ServerVariables("SERVER_PROTOCOL")

        if (HttpContext.Current.Request.ServerVariables["SERVER_PORT_SECURE"].ToLower() == "0")
        {
            Protocol = "http://";
        }
        else
        {
            Protocol = "https://";
        }

        //Dim arrPro As String() = Protocol.Split("/")
        //Protocol = arrPro(0) + "://"
        if (sAppPath == "/")
        {
            SURL = Protocol + ServerName;
        }
        else
        {
            SURL = Protocol + ServerName + sAppPath;
        }

        return SURL;
    }

    public System.Data.DataTable SortDatatable(System.Data.DataTable objDT, string strColumnName, appFunctions.Enum_SortOrderBy objSortOrder, bool IsSort)
    {
        if (IsSort)
        {
            if (objSortOrder == appFunctions.Enum_SortOrderBy.Asc)
            {
                ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Desc;
            }
            else
            {
                ViewState["SortOrder"] = appFunctions.Enum_SortOrderBy.Asc;
            }
            objDT.DefaultView.Sort = string.Format("{0} {1}", strColumnName, ViewState["SortOrder"]);
        }
        else
        {
            if (ViewState["SortColumn"] != null & ViewState["SortColumn"] != "" & (ViewState["SortOrder"] != null))
            {
                objDT.DefaultView.Sort = string.Format("{0} {1}", ViewState["SortColumn"], ViewState["SortOrder"]);
            }
        }
        return objDT.DefaultView.Table;
    }

    public string GetCurrentDateTime()
    {
        return DateTime.UtcNow.AddHours(5.5).ToString();
    }

    public DateTime  GetDateTime()
    {
        return DateTime.UtcNow.AddHours(5.5);
    }
    // Public Function GetCurrentDateTime() As String
    //    Return DateTime.UtcNow.AddHours(5.5).ToString
    //End Function

    public System.DateTime FormatDate(string strDate, string strInputFormat = "", string strOutputFromat = "")
    {
        string strOF = "";

        try
        {
            strDate = strDate.Split(' ')[0];

            //strDate = strDate.Replace("/", "-");

            if (string.IsNullOrEmpty(strInputFormat))
            {
                strInputFormat = strInputDateFormat;
            }

            if (string.IsNullOrEmpty(strOutputFromat))
            {
                strOutputFromat = strOutputDateFormat;
            }

            string[] arDate = strDate.Split('/');
            string[] arInputFormat = strInputFormat.Split('/');
            strOF = strOutputFromat;
            System.DateTime d = new System.DateTime();

            strOF = strOF.Replace(arInputFormat[0], arDate[0]);
            strOF = strOF.Replace(arInputFormat[1], arDate[1]);
            strOF = strOF.Replace(arInputFormat[2], arDate[2]);
            return Convert.ToDateTime(strOF);

        }
        catch (Exception ex)
        {
        }
        return Convert.ToDateTime(strDate);
    }


    public string FormatDateString(string strDate, string strInputFormat = "", string strOutputFromat = "")
    {
        string strOF = "";
        try
        {
            strDate = strDate.Split(' ')[0];

            //strDate = strDate.Replace("/", "-");

            if (string.IsNullOrEmpty(strInputFormat))
            {
                strInputFormat = strInputDateFormat;
            }

            if (string.IsNullOrEmpty(strOutputFromat))
            {
                strOutputFromat = strOutputDateFormat;
            }

            string[] arDate = strDate.Split('/');
            string[] arInputFormat = strInputFormat.Split('/');
            strOF = strOutputFromat;
            System.DateTime d = new System.DateTime();

            strOF = strOF.Replace(arInputFormat[0], arDate[0]);
            strOF = strOF.Replace(arInputFormat[1], arDate[1]);
            strOF = strOF.Replace(arInputFormat[2], arDate[2]);

            return strOF;

        }
        catch (Exception ex)
        {
        }
        return strDate;
    }


    public void SetSelectedRowInGrid(GridView objGridView, GridViewRow objRow)
    {
        for (int i = 0; i <= objGridView.Rows.Count - 1; i++)
        {
            objGridView.Rows[i].CssClass = "gridrow";
            if (objGridView.Rows[i].CssClass.ToLower() == "gridrowselected")
            {
                break; // TODO: might not be correct. Was : Exit For
            }
        }
        objRow.CssClass = "gridrowselected";
    }

    public void Addtocart(int intProductID, string ProductName, string Qty, string Price)
    {
        System.Data.DataRow objDataRow = null;
        bool IsUpadate = false;

        if (Session["Cart"] == null)
        {
            objDataTable = new DataTable();
            objDataTable.Columns.Add("appProductID");
            objDataTable.Columns.Add("appProductName");
            objDataTable.Columns.Add("appQty");
            objDataTable.Columns.Add("appPrice");
            objDataRow = objDataTable.NewRow();
        }
        else
        {
            objDataTable = (DataTable)Session["Cart"];
            DataRow[] objdatarow_temp = null;
            objdatarow_temp = objDataTable.Select("appproductid='" + intProductID + "'");
            if (objdatarow_temp.Length > 0)
            {
                objDataRow = objdatarow_temp[0];
            }
            if ((objDataRow != null))
            {
                int OldQty = (int)objDataRow["appQty"];
                Qty = Qty + OldQty;
                IsUpadate = true;
            }
            else
            {
                objDataRow = objDataTable.NewRow();
            }
        }

        objDataRow["appProductID"] = intProductID;
        objDataRow["appProductName"] = ProductName;
        objDataRow["appQty"] = Qty;
        objDataRow["appPrice"] = Price;
        if (!IsUpadate)
        {
            objDataTable.Rows.Add(objDataRow);
        }
        IsUpadate = false;
        Session["Cart"] = objDataTable;
    }

    public void UpdateCart()
    {
        HtmlControl divCart = (HtmlControl)Page.Master.FindControl("divCart");
        HtmlControl divEmptyCart = (HtmlControl)Page.Master.FindControl("divEmptyCart");
        DataList dlSmallCart = (DataList)Page.Master.FindControl("dlSmallCart");
        Label lblTotalAmount = (Label)Page.Master.FindControl("lblTotalAmount");
        if ((Session["Cart"] != null))
        {
            System.Data.DataTable objDataTable = (DataTable)Session["Cart"];
            if (objDataTable.Rows.Count > 0)
            {
                divCart.Visible = true;
                divEmptyCart.Visible = false;
                ViewState["TotalAmount"] = 0;
                lblTotalAmount.Text = 0.ToString();
                dlSmallCart.DataSource = objDataTable;
                dlSmallCart.DataBind();
                //'lblTotalAmount.Text = ViewState("TotalAmount")
            }
            else
            {
                divCart.Visible = false;
                divEmptyCart.Visible = true;
            }
        }
        else
        {
            divCart.Visible = false;
            divEmptyCart.Visible = true;
        }

        UpdatePanel up = (UpdatePanel)Page.Master.FindControl("upSmallCart");
        up.Update();
    }

    //    If Session("Cart") Is Nothing Then
    //        objDataTable = New DataTable
    //        objDataTable.Columns.Add("appProductID")
    //        objDataTable.Columns.Add("appProductName")
    //        objDataTable.Columns.Add("appQty")
    //        objDataTable.Columns.Add("appPrice")
    //Dim objDataRow As Data.DataRow
    //        objDataRow = objDataTable.NewRow
    //        objDataRow("appProductID") = IntParentID
    //        objDataRow("appProductName") = ProductName
    //        objDataRow("appQty") = Qty
    //        objDataRow("appPrice") = Price
    //        objDataTable.Rows.Add(objDataRow)
    //        Session("Cart") = objDataTable
    //    Else
    //        objDataTable = Session("Cart")
    //Dim objdatarow_temp As DataRow()
    //        objdatarow_temp = objDataTable.Select("appproductid='" & IntParentID & "'")
    //Dim objRow As DataRow
    //        If objdatarow_temp.Count > 0 Then
    //            objRow = objdatarow_temp(0)
    //        End If
    //        If Not objRow Is Nothing Then
    //Dim objdatarow As DataRow()
    //            objdatarow = objDataTable.Select("appproductid='" & IntParentID & "'")
    //Dim objDatatableRow As DataRow
    //            If objdatarow.Count > 0 Then
    //                objDatatableRow = objdatarow(0)
    //            End If
    //Dim OldQty As Integer = objDatatableRow("appQty")
    //Dim NewQty As Integer = Qty
    //Dim TotalQty As Integer = OldQty + NewQty
    //            objDatatableRow("appQty") = TotalQty
    //        Else
    //Dim objDataRow As Data.DataRow
    //            objDataRow = objDataTable.NewRow
    //            objDataRow("appProductID") = IntParentID
    //            objDataRow("appProductName") = ProductName
    //            objDataRow("appQty") = Qty
    //            objDataRow("appPrice") = Price
    //            objDataTable.Rows.Add(objDataRow)
    //            Session("Cart") = objDataTable
    //        End If

    //    End If
    private void Page_PreInit(object sender, System.EventArgs e)
    {
        if (Request.QueryString["for"] == "add")
        {
            this.MasterPageFile = "Blank.master";
            //Dim btn As Button = Me.FindControl("btnBack")
            //btn.Visible = False
        }
    }

    public void RegisterRoute(RouteCollection routes)
    {
        RouteTable.Routes.Clear();
        DataTable objDataTable = new DataTable();
        tblPage objPage = new tblPage();
        objDataTable = objPage.LoadPageNameAndAlias();
        try
        {
            for (int i = 0; i <= objDataTable.Rows.Count - 1; i++)
            {
                routes.MapPageRoute(objDataTable.Rows[i]["appAlias"].ToString(), objDataTable.Rows[i]["appAlias"].ToString(), "~/" + objDataTable.Rows[i]["appLinkURL"].ToString());
            }

        }
        catch (Exception ex)
        {
        }
    }
    public string generateUrl(string str)
    {
        string a = str.Replace(" ", "-");
        return a;
    }

    public string GetAlias(string strString, bool GetOnlyAlias = false)
    {
        tblPage objPage = new tblPage();
        DataTable objDataTable = new DataTable();
        objDataTable = objPage.LoadPageNameAndAlias();
        DataRow[] dr = null;
        dr = objDataTable.Select("appLinkURL = '" + strString + "'");
        if (dr.Length > 0)
        {
            if (GetOnlyAlias == false)
            {
                if (dr[0]["appAlias"].ToString().Contains("/{*name}"))
                {

                    //return GetServerURL() + "/" + dr[0]["appAlias"].ToString().Split("/{*name}".ToCharArray())[0] + "/";
                    string[] stringSeparators = new string[] { "/{*name}" };
                    return GetServerURL() + "/" + dr[0]["appAlias"].ToString().Split(stringSeparators, StringSplitOptions.None)[0] + "/";

                }
                else
                {
                    return GetServerURL() + "/" + dr[0]["appAlias"];
                }
            }
            else
            {
                if (dr[0]["appAlias"].ToString().Contains("/p/"))
                {
                    //return dr[0]["appAlias"].ToString().Split("/p/".ToCharArray())[0];
                    string[] stringSeparators = new string[] { "/p/" };
                    return dr[0]["appAlias"].ToString().Split(stringSeparators, StringSplitOptions.None)[0];
                }
            }
        }
        else
        {
            return "";
        }
        return "";
    }

    public PageBase()
    {
        PreInit += Page_PreInit;
    }
}
