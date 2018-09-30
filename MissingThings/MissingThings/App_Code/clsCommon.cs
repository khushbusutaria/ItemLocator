using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Net.Mail;
using System.Net.Mime;
using System.Web.UI.WebControls;
using System.IO;
using BusinessLayer;
using System.Text;
using System.Web;
using System.Configuration;
using System.Security.Cryptography;
using System.Web.UI;

public class clsCommon : MyGeneration.dOOdads.SqlClientEntity
{

    public static void CreateMessageAlert(ref System.Web.UI.Page aspxPage, string strMessage, string strKey)
    {
        string strScript = "<script language=JavaScript>alert('" + strMessage + "')</script>";

        if ((!aspxPage.ClientScript.IsStartupScriptRegistered(strKey)))
        {
            aspxPage.ClientScript.RegisterClientScriptBlock(typeof(string), strKey, strScript);
        }
    }

    public string JoinWithComma(DataTable objDT, string strColumnName, Enums.Enum_DataType objDataType)
    {
        string[] ar = { "" };
        switch (objDataType)
        {
            case Enums.Enum_DataType.iInteger:
                ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => Convert.ToString((Int32)row[strColumnName]));
                break;
            case Enums.Enum_DataType.sString:
                ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => (string)row[strColumnName]);
                break;
            case Enums.Enum_DataType.dDateTime:
                ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => Convert.ToString((DateTime)row[strColumnName]));
                break;
        }
        return String.Join(",", ar);
    }

    public bool IsRecordExists(string strTableName, string strUniqueFieldName, string strPKFieldName, string strUniqueFieldValue, string strPKFieldValue = "", string strWhereCondition = "")
    {
        string strQry = "Select " + strPKFieldName + " from " + strTableName + " where " + strUniqueFieldName + "='" + strUniqueFieldValue + "'";
        if (!string.IsNullOrEmpty(strPKFieldValue))
        {
            strQry = strQry + " and " + strPKFieldName + "<>" + strPKFieldValue;
        }
        if (!string.IsNullOrEmpty(strWhereCondition))
        {
            strQry = strQry + " and " + strWhereCondition;
        }
        base.LoadFromRawSql(strQry);
        if (base.RowCount > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //Public Sub SendMail(ByVal strTO As String, ByVal strSubject As String, ByVal strBody As String)
    //    Dim strFrom As String = ""
    //    strFrom = "webmaster@kumkumshaadi.com"

    //    Dim mailMessage As MailMessage = New MailMessage(strFrom, strTO, strSubject, strBody)
    //    'mailMessage.IsBodyHtml = True
    //    'mailMessage.Priority = MailPriority.High
    //    Dim mailSender As SmtpClient = New SmtpClient("mail.kumkumshaadi.com") ''use this if you are in the development server

    //    Dim basicAuthenticationInfo As New System.Net.NetworkCredential("webmaster@kumkumshaadi.com", "kumkum_111")
    //    mailSender.UseDefaultCredentials = False
    //    mailSender.Credentials = basicAuthenticationInfo

    //    mailSender.Send(mailMessage)

    //End Sub

    public void SendMail(string strTO, string strSubject, string strBody, FileUpload objFileUpload = null)
    {
        try
        {
            tblSettings objSettings = new tblSettings();
            objSettings.LoadAll();

            if (objSettings.RowCount > 0)
            {
                string strFrom = objSettings.AppSiteEmail;
                //"rahul.b4r097@gmail.com"
                if (strTO == "")
                {
                    strTO = objSettings.AppRecepientEmail;
                }
                MailMessage mail = new MailMessage(strFrom, strTO, strSubject, strBody);
                mail.IsBodyHtml = true;

                SmtpClient smtp = new SmtpClient();
                smtp.Host = objSettings.AppSMTP;
                //"smtp.gmail.com"

                if (!string.IsNullOrEmpty(objSettings.s_AppPortNumber))
                {
                    smtp.Port = Convert.ToInt32(objSettings.AppPortNumber.ToString());
                    //587
                    smtp.EnableSsl = true;
                }
                if(objSettings.s_AppIsSSLEnabled !="")
                {
                    smtp.EnableSsl = objSettings.AppIsSSLEnabled;
                }

                smtp.UseDefaultCredentials = false;

                clsEncryption objEncrypt = new clsEncryption();

                smtp.Credentials = new System.Net.NetworkCredential(strFrom, objEncrypt.Decrypt(objSettings.AppEmailPassword, appFunctions.strKey));
                smtp.Send(mail);

                smtp = null;
                mail = null;
            }
        }
        catch (Exception ex)
        {
            HttpContext.Current.Response.Write(ex.StackTrace);
            //msgbox(ex.innerexception.tostring())
        }
    }

    public bool SendSMS(string strSMSURL)
    {
        try
        {
            StringBuilder strURL = new StringBuilder();


            if (!string.IsNullOrEmpty(strSMSURL))
            {
                strURL.Append("" + strSMSURL + "");
            }
            System.Net.WebRequest WReq = System.Net.WebRequest.Create(strURL.ToString());
            System.Net.WebResponse WResp = WReq.GetResponse();
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public void SendNormalMail(string strTO, string strSubject, string strBody)
    {
        tblSettings objSetting = new tblSettings();
        clsEncryption objEncrypt = new clsEncryption();
        objSetting.Query.Load();
        if (objSetting.RowCount > 0)
        {
            string strFrom = objSetting.s_AppSiteEmail;


            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(strFrom);
            mail.To.Add(strTO);
            mail.Subject = strSubject;


            mail.IsBodyHtml = true;


            string[] arImages = null;

            DataTable objDT = new DataTable();
            objDT.Columns.Add("path");
            objDT.Columns.Add("contentid");

            arImages = strBody.Split(new string[] { "<img" }, StringSplitOptions.None); // Strings.Split(strBody.ToString().ToLower(), "<img");
            string[] arSrc = null;
            for (int i = 0; i <= arImages.Length - 1; i++)
            {
                if (arImages[i].Contains("src="))
                {
                    arSrc = arImages[i].Split(new string[] { "src=" }, StringSplitOptions.None);
                    if (arSrc.Length > 0)
                    {
                        string strPath = arSrc[1];
                        if (strPath.StartsWith("\""))
                        {
                            strPath = strPath.TrimStart("\"".ToCharArray()).Split("\"".ToCharArray())[0];
                        }
                        else if (strPath.StartsWith("'"))
                        {
                            strPath = strPath.TrimStart("'".ToCharArray()).Split("'".ToCharArray())[0];
                        }
                        else
                        {
                            strPath = strPath.Split(' ')[0];
                        }

                        DataRow dr = objDT.NewRow();
                        dr["path"] = HttpContext.Current.Server.MapPath(strPath.ToLower().Replace(PageBase.GetServerURL().ToLower() + "/admin/", ""));
                        dr["contentid"] = "cid_" + i;
                        objDT.Rows.Add(dr);

                        strBody = strBody.ToLower().Replace(strPath, "cid:" + "cid_" + i);
                    }
                }
            }

            AlternateView htmlView = AlternateView.CreateAlternateViewFromString(strBody, null, "text/html");

            for (int i = 0; i <= objDT.Rows.Count - 1; i++)
            {
                LinkedResource imagelink = new LinkedResource(objDT.Rows[i]["path"].ToString(), "image/jpeg");
                imagelink.ContentId = objDT.Rows[i]["contentid"].ToString();
                imagelink.TransferEncoding = System.Net.Mime.TransferEncoding.Base64;
                htmlView.LinkedResources.Add(imagelink);
            }

            mail.AlternateViews.Add(htmlView);
            mail.Body = strBody.ToString();


            SmtpClient smtp = new SmtpClient();
            smtp.Host = objSetting.s_AppSMTP;

            if (!string.IsNullOrEmpty(objSetting.s_AppPortNumber))
            {
                smtp.Port = Convert.ToInt32(objSetting.s_AppPortNumber);
            }

            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;
            smtp.Credentials = new System.Net.NetworkCredential(strFrom, objEncrypt.Decrypt(objSetting.s_AppEmailPassword, appFunctions.strKey));
            smtp.Send(mail);

            smtp = null;
            mail = null;
        }
        objSetting = null;
        objEncrypt = null;
    }

    //Public Sub SendAttachFileMail(ByVal strTO As String, ByVal strSubject As String, ByVal strBody As String, ByVal objFileUpload As FileUpload)
    //    Try
    //        Dim strFrom As String = ""
    //        strFrom = "webmaster@arihantbelting.com"

    //        Dim mailMessage As MailMessage = New MailMessage(strFrom, strTO, strSubject, strBody)
    //        mailMessage.IsBodyHtml = True
    //        mailMessage.Priority = MailPriority.High
    //        Dim mailSender As SmtpClient = New SmtpClient("mail.arihantbelting.com") ''use this if you are in the development server

    //        Dim basicAuthenticationInfo As New System.Net.NetworkCredential(strFrom, "arihant_123")
    //        mailSender.UseDefaultCredentials = False
    //        mailSender.Credentials = basicAuthenticationInfo

    //        mailSender.Send(mailMessage)
    //    Catch ex As Exception

    //    End Try
    //End Sub

    public string readFile(string strFileName)
    {
        System.IO.StreamReader streamReader = new System.IO.StreamReader(strFileName);
        string s = "";
        try
        {
            do
            {
                s += streamReader.ReadLine();
            } while (streamReader.Peek() != -1);
        }
        catch
        {
            s = "File is empty";
        }
        finally
        {
            streamReader.Close();
        }
        return s;
    }

    public void FillDropDownList(DropDownList ObjDrp, string strTableName, string strDisplayFieldName, string strValueFieldName, string strDefaultTextToDisplay, string strOrderByFieldName, appFunctions.Enum_SortOrderBy strSortOrder, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select  convert(varchar," + strDisplayFieldName + ") as " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            strQry = strQry + " Order By " + strOrderByFieldName + " " + strSortOrder.ToString();

            base.LoadFromRawSql(strQry);
            DataRow dr = null;
            dr = base.DefaultView.Table.NewRow();
            dr[strValueFieldName] = 0;
            dr[strDisplayFieldName] = strDefaultTextToDisplay;
            base.DefaultView.Table.Rows.InsertAt(dr, 0);
            ObjDrp.DataSource = null;
            ObjDrp.DataSource = base.DefaultView.Table;
            ObjDrp.DataTextField = strDisplayFieldName;
            ObjDrp.DataValueField = strValueFieldName;
            ObjDrp.DataBind();
        }
        catch (Exception ex)
        {
            // Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    public void FillDropDownListWithOutDefaultValue(DropDownList ObjDrp, string strTableName, string strDisplayFieldName, string strValueFieldName, string strOrderByFieldName, appFunctions.Enum_SortOrderBy strSortOrder, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            strQry = strQry + " Order By " + strOrderByFieldName + " " + strSortOrder.ToString();

            base.LoadFromRawSql(strQry);
            ObjDrp.DataSource = null;
            ObjDrp.DataSource = base.DefaultView.Table;
            ObjDrp.DataTextField = strDisplayFieldName;
            ObjDrp.DataValueField = strValueFieldName;
            ObjDrp.DataBind();
        }
        catch (Exception ex)
        {
            // Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    public void FillDropDownList(DropDownList ObjDrp, string strTableName, string strDisplayFieldName, string strValueFieldName, string strDefaultTextToDisplay, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            //strQry = strQry & " Order By " & strOrderByFieldName & " " & strSortOrder.ToString

            base.LoadFromRawSql(strQry);
            System.Data.DataRow dr = null;
            dr = base.DefaultView.Table.NewRow();
            dr[strValueFieldName] = 0;
            dr[strDisplayFieldName] = strDefaultTextToDisplay;
            base.DefaultView.Table.Rows.InsertAt(dr, 0);
            ObjDrp.DataSource = null;
            ObjDrp.DataSource = base.DefaultView.Table;
            ObjDrp.DataTextField = strDisplayFieldName;
            ObjDrp.DataValueField = strValueFieldName;
            ObjDrp.DataBind();
        }
        catch (Exception ex)
        {
            //  Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    public void FillDropDownListWithoutDefaultValuePass(DropDownList ObjDrp, string strTableName, string strDisplayFieldName, string strValueFieldName, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            base.LoadFromRawSql(strQry);
            ObjDrp.DataSource = null;
            ObjDrp.DataSource = base.DefaultView.Table;
            ObjDrp.DataTextField = strDisplayFieldName;
            ObjDrp.DataValueField = strValueFieldName;
            ObjDrp.DataBind();
        }
        catch (Exception ex)
        {
        }
    }

    public void FillDropDownListForNumeric(DropDownList ObjDrp, string strTableName, string strDisplayFieldName, string strValueFieldName, string strDefaultTextToDisplay, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select convert(varchar(20)," + strDisplayFieldName + ") as " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            //strQry = strQry & " Order By " & strOrderByFieldName & " " & strSortOrder.ToString

            base.LoadFromRawSql(strQry);
            System.Data.DataRow dr = null;
            dr = base.DefaultView.Table.NewRow();
            dr[strValueFieldName] = 0;
            dr[strDisplayFieldName] = strDefaultTextToDisplay;
            base.DefaultView.Table.Rows.InsertAt(dr, 0);
            ObjDrp.DataSource = null;
            ObjDrp.DataSource = base.DefaultView.Table;
            ObjDrp.DataTextField = strDisplayFieldName;
            ObjDrp.DataValueField = strValueFieldName;
            ObjDrp.DataBind();
        }
        catch (Exception ex)
        {
            //  Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    public void FillListBox(ListBox ObjListBox, string strTableName, string strDisplayFieldName, string strValueFieldName, string strOrderByFieldName, appFunctions.Enum_SortOrderBy strSortOrder, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            strQry = strQry + " Order By " + strOrderByFieldName + " " + strSortOrder.ToString();

            base.LoadFromRawSql(strQry);
            ObjListBox.DataSource = null;
            ObjListBox.DataSource = base.DefaultView.Table;
            ObjListBox.DataTextField = strDisplayFieldName;
            ObjListBox.DataValueField = strValueFieldName;
            ObjListBox.DataBind();
        }
        catch (Exception ex)
        {
            //Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    //Public Function ExecuteQuery(ByVal strQry As String) As DataTable
    //    Try
    //        MyBase.LoadFromRawSql(strQry)
    //        Return MyBase.DefaultView.Table
    //    Catch ex As Exception

    //    End Try
    //    Return Nothing
    //End Function

    public DropDownList BindEnumtoDDL(Type enumType, DropDownList ddl, string strDefaultText = "")
    {
        try
        {

            string[] enumNames = Enum.GetNames(enumType);
            foreach (string item in enumNames)
            {
                //get the enum item value
                int value = (int)Enum.Parse(enumType, item);
                System.Web.UI.WebControls.ListItem listItem = new System.Web.UI.WebControls.ListItem(item, value.ToString());
                ddl.Items.Add(listItem);
            }

            if (strDefaultText.Length > 0)
            {
                ddl.Items.Insert(0, new System.Web.UI.WebControls.ListItem(strDefaultText, 0.ToString()));
            }
            ddl.SelectedIndex = 0;

            //Array itemValues = Enum.GetValues(enumType);
            //Array itemNames = Enum.GetNames(enumType);

            //for (int i = 0; i <= itemNames.Length - 1; i++)
            //{
            //    ListItem item = new ListItem(itemNames.GetValue(i).ToString(), Convert.ToInt32(itemValues.GetValue(i).ToString()).ToString());
            //    ddl.Items.Add(item);
            //}

        }
        catch (Exception ex)
        {
        }
        return ddl;
    }

    public void SendSMS(string strMobileNos, string strSMS)
    {
        try
        {
            StringBuilder strURL = new StringBuilder();

            strURL.Append("http://203.199.114.180/operator/davinci/bulksmspush/smspush.aspx?");
            strURL.Append("phno=" + strMobileNos.TrimEnd(','));
            strURL.Append("&msg=" + strSMS);
            strURL.Append("&uid=kumkum&pwd=kumkum1&from=KUMKUM");

            System.Net.WebRequest WReq = System.Net.WebRequest.Create(strURL.ToString());
            System.Net.WebResponse WResp = WReq.GetResponse();

        }
        catch (Exception ex)
        {
        }
    }

    public void SetDisplayOrder(string strTableName, string strPkFieldName, string strDisplayOrderFieldName, int intCurruntPkID, int intCurruntDisplayOrder, int intUpdownPkID, int intUpDownDisplayOrderID)
    {
        string strQuery = "";
        strQuery = "UPDATE " + strTableName + " Set " + strDisplayOrderFieldName + " = " + intUpDownDisplayOrderID + " Where " + strPkFieldName + " = " + intCurruntPkID + " Update " + strTableName + "  Set " + strDisplayOrderFieldName + " = " + intCurruntDisplayOrder + " Where " + strPkFieldName + " = " + intUpdownPkID + "";
        base.LoadFromRawSql(strQuery);
    }

    public void SetDisplayOrderForDesiredMoving(string strTableName, string strPkFieldName, string strDisplayOrderFieldName, string currentDisplayOrder, int intPkID)
    {
        string strQry = "";
        strQry = "UPDATE " + strTableName + " Set " + strDisplayOrderFieldName + " = " + strDisplayOrderFieldName + 1 + " where " + strDisplayOrderFieldName + " >= " + currentDisplayOrder + " and " + intPkID + " <> " + intPkID + "";
        base.LoadFromRawSql(strQry);
    }

    public void MoveToDesiredOrder(string strTableName, string strDisplayOrder, int intPKID, string strFieldName, string strWhere = "")
    {
        string strQry = "";
        strQry = "update " + strTableName + " set appDisplayOrder = " + strDisplayOrder + " where " + strFieldName + " = " + intPKID;
        if (!string.IsNullOrEmpty(strWhere))
        {
            strQry += " and " + strWhere;
        }
        base.LoadFromRawSql(strQry);
    }

    public void AddDisplayOrder(string strTableName, string strFirstDisplayOrder, string strSecondDisplayOrder, int intPKID, string strFieldName, string strWhere = "")
    {
        string strQry = "";
        strQry = "update " + strTableName + " set appDisplayOrder=appDisplayOrder+1 where appDisplayOrder Between " + strFirstDisplayOrder + " and " + strSecondDisplayOrder + " and " + strFieldName + " <>" + intPKID;
        if (!string.IsNullOrEmpty(strWhere))
        {
            strQry += " and " + strWhere;
        }
        base.LoadFromRawSql(strQry);
    }

    public void MinusDisplayOrder(string strTableName, string strFirstDisplayOrder, string strSecondDisplayOrder, int intPKID, string strFieldName, string strWhere = "")
    {
        string strQry = "";
        strQry = "update " + strTableName + " set appDisplayOrder=appDisplayOrder-1 where appDisplayOrder Between " + strSecondDisplayOrder + " and " + strFirstDisplayOrder + " and " + strFieldName + " <>" + intPKID;
        if (!string.IsNullOrEmpty(strWhere))
        {
            strQry += " and " + strWhere;
        }
        base.LoadFromRawSql(strQry);
    }

    public void BindEnumIntoDropDown(DropDownList ddlDropDown, Type enumeration)
    {
        string[] names = Enum.GetNames(enumeration);
        Array values = Enum.GetValues(enumeration);
        Hashtable ht = new Hashtable();
        //ht.Add(0, "-- Select --")
        Dictionary<int, string> dic = new Dictionary<int, string>();
        dic.Add(0, "-- Select --");
        for (int i = 0; i <= names.Length - 1; i++)
        {
            //ht.Add(Convert.ToInt32(values.GetValue(i)).ToString(), names(i))
            dic.Add(Convert.ToInt32(values.GetValue(i)), names[i].ToString());
        }

        ddlDropDown.DataSource = dic;
        ddlDropDown.DataTextField = "value";
        ddlDropDown.DataValueField = "key";
        ddlDropDown.DataBind();
    }

    public void FillRecordPerPage(ref DropDownList ddl)
    {
        string[] ar = {
            2.ToString(),
            5.ToString(),
			10.ToString(),
            15.ToString(),
			20.ToString(),
            30.ToString(),
			50.ToString(),
			100.ToString(),
			"All"
		};
        ddl.DataSource = ar;
        ddl.DataBind();

        if ((PageBase_Admin.strListLimit != null))
        {
            ddl.Items.FindByText(PageBase_Admin.strListLimit).Selected = true;
        }

    }

    public int GetNextDisplayOrder(string strTableName, string strDispalyOrderFieldName, string strWhere = "")
    {
        string strQry = "";
        strQry = " Select max(" + strDispalyOrderFieldName + ") from " + strTableName;

        if (!string.IsNullOrEmpty(strWhere.Trim()))
        {
            strQry += " where " + strWhere;
        }

        base.LoadFromRawSql(strQry);

        if (string.IsNullOrEmpty(base.DefaultView.Table.Rows[0][0].ToString()))
        {
            return 1;
        }
        else
        {
            return Convert.ToInt32(base.DefaultView.Table.Rows[0][0].ToString()) + 1;
        }

    }

    public void FillNumberDropdown(DropDownList cmb, int intStart, int intEnd, string strDefaultText)
    {
        for (int i = intStart; i <= intEnd; i++)
        {
            System.Web.UI.WebControls.ListItem ilist = new System.Web.UI.WebControls.ListItem(i.ToString(), i.ToString());
            cmb.Items.Add(ilist);
        }
        cmb.Items.Insert(0, new System.Web.UI.WebControls.ListItem(strDefaultText, 0.ToString()));
        cmb.ClearSelection();
    }

    public string FileUpload_Commomn(FileUpload objFileUpload, string strFileName, string strFilePath, string strFileExt, ref string status, int strFilesize = 0, string strExitsFilePath = "", bool IsClient = false)
    {
        string Filepath = "";
        string fileName = Path.GetFileName(objFileUpload.FileName);

        if (!string.IsNullOrEmpty(fileName))
        {
            string temppath = "";
            if (IsClient == true)
            {
                temppath = "Admin/" + strFilePath;
            }
            else
            {
                temppath = strFilePath;
            }

            string fileExt = Path.GetExtension(objFileUpload.FileName);
            int fileSize = Convert.ToInt32(((objFileUpload.PostedFile.ContentLength) / 1024) / 1024);
            bool fileExtBoolen = false;
            Array strfileExtension = strFileExt.Split(',');

            for (int i = 0; i <= strfileExtension.Length - 1; i++)
            {
                if (strfileExtension.GetValue(i).ToString().ToLower() == fileExt.ToLower())
                {
                    fileExtBoolen = true;
                    break; // TODO: might not be correct. Was : Exit For
                }
            }
            if (fileExtBoolen)
            {

                if (fileSize <= strFilesize)
                {
                    //strFileName = strFileName
                    if (!string.IsNullOrEmpty(strExitsFilePath))
                    {
                        try
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strExitsFilePath)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath(strExitsFilePath));
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    Filepath = temppath + strFileName + fileExt;
                    objFileUpload.SaveAs(HttpContext.Current.Server.MapPath(Filepath));

                }
                else
                {
                    status = "File size is very large then " + strFilesize.ToString();
                    return false.ToString();
                }
            }
            else
            {
                status = "You can only upload " + strFileExt.ToString() + " file";
                return false.ToString();
            }
        }
        else
        {
            status = "Please select Upload file";
            return false.ToString();
        }
        return Filepath;
    }

    public string FileUpload_Images(HttpPostedFile objFileUpload, string strFileName, string strFilePath, ref string status, int strFileResize = 0, string strExitsFilePath = "", bool IsClient = false, int FixedHeightSize = 0, int FixedWidthSize = 0)
    {
        string Filepath = "";
        string fileName = Path.GetFileName(objFileUpload.FileName);

        if (!string.IsNullOrEmpty(fileName))
        {
            string temppath = "";
            if (IsClient == true)
            {
                temppath = "Admin/Uploads/Temp/";
            }
            else
            {
                temppath = "Uploads/Temp/";
            }

            string fileExt = Path.GetExtension(objFileUpload.FileName);
            int fileSize = Convert.ToInt32((objFileUpload.ContentLength) / 1024);
            if (fileExt.ToLower() == ".jpeg" | fileExt.ToLower() == ".gif" | fileExt.ToLower() == ".png" | fileExt.ToLower() == ".jpg" | fileExt.ToLower() == ".tiff" | fileExt.ToLower() == ".tif" | fileExt.ToLower() == ".bmp")
            {

                if (fileSize <= appFunctions.intMaxImgFileSize)
                {
                    //strFileName = strFileName
                    if (!string.IsNullOrEmpty(strExitsFilePath))
                    {
                        try
                        {
                            if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strExitsFilePath)))
                            {
                                System.IO.File.Delete(HttpContext.Current.Server.MapPath(strExitsFilePath));
                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                    string strPath = temppath + strFileName + fileExt;

                    objFileUpload.SaveAs(HttpContext.Current.Server.MapPath(strPath));
                    temppath = strPath;
                }
                else
                {
                    status = "File size is very large";
                    return false.ToString();
                }
            }
            else
            {
                status = "You can only upload .jpeg,.gif,.png,.jpg,.tiff,.tif,.bmp file";
                return false.ToString();
            }

            if (strFileResize != 0)
            {
                Filepath = ResizeUploadedFile(temppath, strFileName, strFileResize, ref status, strFilePath);
            }
            else if (FixedHeightSize != 0 & FixedWidthSize != 0)
            {
                Filepath = ResizeUploadedFile(temppath, strFileName, strFileResize, ref status, strFilePath, FixedHeightSize, FixedWidthSize);

            }
            else if (FixedHeightSize != 0 & FixedWidthSize == 0)
            {
                Filepath = ResizeUploadedFile(temppath, strFileName, strFileResize, ref status, strFilePath, FixedHeightSize);
            }
            else if (FixedHeightSize == 0 & FixedWidthSize != 0)
            {
                Filepath = ResizeUploadedFile(temppath, strFileName, strFileResize, ref status, strFilePath, 0, FixedWidthSize);
            }
            else
            {
                if (IsClient == true)
                {
                    Filepath = "Admin/" + strFilePath + strFileName + Path.GetExtension(temppath);
                }
                else
                {
                    Filepath = strFilePath + strFileName + Path.GetExtension(temppath);
                }
                File.Copy(HttpContext.Current.Server.MapPath(temppath), HttpContext.Current.Server.MapPath(Filepath));
            }


            System.IO.File.Delete(HttpContext.Current.Server.MapPath(temppath));
        }
        else
        {
            status = "Please select image file";
            return false.ToString();
        }
        strFileResize = 0;
        return Filepath;
    }

    public string ResizeUploadedFile(string strOldFilePath, string strFileName, int strFileResize, ref string status, string strNewFilePath = "", int FixedHeightSize = 0, int FixedWidthSize = 0)
    {
        string Filepath = "";
        bool newfile = false;
        if (string.IsNullOrEmpty(strNewFilePath))
        {
            newfile = true;
            Array strold = strOldFilePath.Split('/');
            for (int i = 0; i <= strold.Length - 2; i++)
            {
                strNewFilePath = strNewFilePath + strold.GetValue(i) + "/";
            }
        }
        System.Drawing.Bitmap upBmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(HttpContext.Current.Server.MapPath(strOldFilePath));


        try
        {
            int upWidth = upBmp.Width;
            int upHeight = upBmp.Height;
            int newWidth = 0;
            int newHeight = 0;

            if (FixedHeightSize != 0 & FixedWidthSize != 0)
            {
                newHeight = FixedHeightSize;
                //Dim intRatio As Decimal = FixedHeightSize / upHeight
                //newWidth = upWidth * intRatio
                newWidth = FixedWidthSize;
                Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
            }
            else if (FixedHeightSize != 0)
            {
                if (upHeight > FixedHeightSize)
                {
                    newHeight = FixedHeightSize;
                    decimal intRatio = Convert.ToDecimal(FixedHeightSize) / Convert.ToDecimal(upHeight);
                    newWidth = Convert.ToInt32(Convert.ToDecimal(upWidth) * intRatio);
                    Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                }
                else
                {
                    Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                    File.Copy(HttpContext.Current.Server.MapPath(strOldFilePath), HttpContext.Current.Server.MapPath(Filepath));
                }
            }
            else if (FixedWidthSize != 0)
            {
                if (upWidth > FixedWidthSize)
                {
                    newWidth = FixedWidthSize;
                    decimal intRatio = Convert.ToDecimal(FixedWidthSize) / Convert.ToDecimal(upWidth);
                    newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                    Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                }
                else
                {
                    Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                    File.Copy(HttpContext.Current.Server.MapPath(strOldFilePath), HttpContext.Current.Server.MapPath(Filepath));
                }
            }
            else
            {
                if (upWidth > upHeight)
                {
                    if (upWidth > strFileResize)
                    {
                        newWidth = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upWidth);
                        newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                        Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(HttpContext.Current.Server.MapPath(strOldFilePath), HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
                else if (upHeight > upWidth)
                {
                    if (upHeight > strFileResize)
                    {
                        newHeight = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upHeight);
                        newWidth = Convert.ToInt32(Convert.ToDecimal(upWidth) * intRatio);
                        Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(HttpContext.Current.Server.MapPath(strOldFilePath), HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
                else if (upWidth == upHeight)
                {
                    if (upWidth > strFileResize)
                    {
                        newWidth = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upWidth);
                        newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                        Filepath = (ResizeImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(HttpContext.Current.Server.MapPath(strOldFilePath), HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            status = ex.ToString();
            return false.ToString();
        }
        finally
        {
            upBmp.Dispose();

        }

        if (newfile)
        {
            try
            {
                if (System.IO.File.Exists(HttpContext.Current.Server.MapPath(strOldFilePath)))
                {
                    System.IO.File.Delete(HttpContext.Current.Server.MapPath(strOldFilePath));
                }
            }
            catch (Exception ex)
            {
            }
        }

        return Filepath;

    }

    public string ResizeImage(int NewWidth, int NewHeight, string TempPath, string strFilePath, string strImage = "", string strFileName = "")
    {
        string MainFilePath = HttpContext.Current.Server.MapPath(TempPath);
        string strImagePath = strFilePath + strFileName + strImage + Path.GetExtension(TempPath);
        string tmpThumbPath = HttpContext.Current.Server.MapPath(strImagePath);

        System.Drawing.Image Img = System.Drawing.Image.FromFile(MainFilePath);
        dynamic thumbnailImg = new System.Drawing.Bitmap(NewWidth, NewHeight);
        dynamic thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        dynamic imageRectangle = new System.Drawing.Rectangle(0, 0, NewWidth, NewHeight);
        thumbGraph.DrawImage(Img, imageRectangle);
        thumbnailImg.Save(tmpThumbPath, Img.RawFormat);

        thumbGraph.Dispose();
        thumbnailImg.Dispose();
        Img.Dispose();
        return strImagePath;
    }

    public bool ThumbnailCallback()
    {
        return true;
    }

    public string ResizeDirectImagesFile(string strOldFilePath, string strFileName, int strFileResize, ref string status, string strNewFilePath = "", int FixedHeightSize = 0, int FixedWidthSize = 0)
    {
        string Filepath = "";


        System.Drawing.Bitmap upBmp = (System.Drawing.Bitmap)System.Drawing.Image.FromFile(strOldFilePath);
        try
        {
            int upWidth = upBmp.Width;
            int upHeight = upBmp.Height;
            int newWidth = 0;
            int newHeight = 0;

            if (FixedHeightSize != 0 & FixedWidthSize != 0)
            {
                newHeight = FixedHeightSize;
                //Dim intRatio As Decimal = FixedHeightSize / upHeight
                //newWidth = upWidth * intRatio
                newWidth = FixedWidthSize;
                Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
            }
            else if (FixedHeightSize != 0)
            {
                if (upHeight > FixedHeightSize)
                {
                    newHeight = FixedHeightSize;
                    decimal intRatio = Convert.ToDecimal(FixedHeightSize) / Convert.ToDecimal(upHeight);
                    newWidth = Convert.ToInt32(Convert.ToDecimal(upWidth) * intRatio);
                    Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                }
                else
                {
                    Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                    File.Copy(strOldFilePath, HttpContext.Current.Server.MapPath(Filepath));
                }
            }
            else if (FixedWidthSize != 0)
            {
                if (upWidth > FixedWidthSize)
                {
                    newWidth = FixedWidthSize;
                    decimal intRatio = Convert.ToDecimal(FixedWidthSize) / Convert.ToDecimal(upWidth);
                    newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                    Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                }
                else
                {
                    Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                    File.Copy(strOldFilePath, HttpContext.Current.Server.MapPath(Filepath));
                }
            }
            else
            {
                if (upWidth > upHeight)
                {
                    if (upWidth > strFileResize)
                    {
                        newWidth = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upWidth);
                        newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                        Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(strOldFilePath, HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
                else if (upHeight > upWidth)
                {
                    if (upHeight > strFileResize)
                    {
                        newHeight = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upHeight);
                        newWidth = Convert.ToInt32(Convert.ToDecimal(upWidth) * intRatio);
                        Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(strOldFilePath, HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
                else if (upWidth == upHeight)
                {
                    if (upWidth > strFileResize)
                    {
                        newWidth = strFileResize;
                        decimal intRatio = Convert.ToDecimal(strFileResize) / Convert.ToDecimal(upWidth);
                        newHeight = Convert.ToInt32(Convert.ToDecimal(upHeight) * intRatio);
                        Filepath = (ResizeDirectImage(newWidth, newHeight, strOldFilePath, strNewFilePath, strFileName));
                    }
                    else
                    {
                        Filepath = strNewFilePath + strFileName + Path.GetExtension(strOldFilePath);
                        File.Copy(strOldFilePath, HttpContext.Current.Server.MapPath(Filepath));
                    }
                }
            }
        }
        catch (Exception ex)
        {
            status = ex.ToString();
            return false.ToString();
        }
        finally
        {
            upBmp.Dispose();

        }

        return Filepath;

    }

    public string ResizeDirectImage(int NewWidth, int NewHeight, string TempPath, string strFilePath, string strImage = "", string strFileName = "")
    {
        string MainFilePath = TempPath;
        string strImagePath = strFilePath + strFileName + strImage + Path.GetExtension(TempPath);
        string tmpThumbPath = HttpContext.Current.Server.MapPath(strImagePath);

        System.Drawing.Image Img = System.Drawing.Image.FromFile(MainFilePath);
        dynamic thumbnailImg = new System.Drawing.Bitmap(NewWidth, NewHeight);
        dynamic thumbGraph = System.Drawing.Graphics.FromImage(thumbnailImg);
        thumbGraph.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
        thumbGraph.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
        thumbGraph.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
        dynamic imageRectangle = new System.Drawing.Rectangle(0, 0, NewWidth, NewHeight);
        thumbGraph.DrawImage(Img, imageRectangle);
        thumbnailImg.Save(tmpThumbPath, Img.RawFormat);

        thumbGraph.Dispose();
        thumbnailImg.Dispose();
        Img.Dispose();
        return strImagePath;
    }

    public void FillCheckboxList(CheckBoxList ObjListBox, string strTableName, string strDisplayFieldName, string strValueFieldName, string strOrderByFieldName, appFunctions.Enum_SortOrderBy strSortOrder, string strWhreCondition = "")
    {
        try
        {
            string strQry = "Select " + strDisplayFieldName + "," + strValueFieldName + " from " + strTableName;
            if (!string.IsNullOrEmpty(strWhreCondition))
            {
                strQry = strQry + " where " + strWhreCondition;
            }
            strQry = strQry + " Order By " + strOrderByFieldName + " " + strSortOrder.ToString();

            base.LoadFromRawSql(strQry);
            ObjListBox.DataSource = null;
            ObjListBox.DataSource = base.DefaultView.Table;
            ObjListBox.DataTextField = strDisplayFieldName;
            ObjListBox.DataValueField = strValueFieldName;
            ObjListBox.DataBind();
        }
        catch (Exception ex)
        {
            //Dim objPageBase As New PageBase
            //objPageBase.LogError(ex, "appFunctions -> FillDropDownList()")
        }
    }

    public Array GetParameterInArray()
    {
        Array UrlPart = null;
        UrlPart = GetCurrentUrl(false).Split('/');
        int intSize = (UrlPart.GetValue(0).ToString().Split('+')).Length - 1;
        string[,] Namevalue = new string[UrlPart.Length, intSize + 1];
        for (int i = 0; i <= UrlPart.Length - 1; i++)
        {
            Array temUrlPart = UrlPart.GetValue(i).ToString().Split('+');
            try
            {
                for (int j = 0; j <= intSize; j++)
                {
                    Namevalue[i, j] = temUrlPart.GetValue(j).ToString();
                }

            }
            catch (Exception ex)
            {
            }

        }
        return Namevalue;
    }

    public string GetCurrentUrl(bool FullName, bool GetPageName = false)
    {
        string strRawUrl = HttpContext.Current.Request.RawUrl.ToString();
        string pageName = strRawUrl;
        if (Convert.ToBoolean(ConfigurationManager.AppSettings["IsOnServer"]) == true)
        {
            //'For Online
            pageName = pageName.TrimStart('/');
        }
        else
        {
            //'For Localhost
            for (int i = 0; i <= 1; i++)
            {
                pageName = pageName.Substring(pageName.IndexOf("/") + 1);
            }
        }
        if (GetPageName)
        {
            if (pageName.Split('/').Length > 1)
            {
                pageName = pageName.Split('/')[0] + "/{*name}";
            }
        }
        else
        {
            if (FullName)
            {
                return pageName;
            }
            else
            {
                pageName = pageName.Substring(pageName.IndexOf("/") + 1);
            }
        }
        return pageName;
    }
    //Public Function GetParameterFromUrl(ByVal intOption As Integer, Optional ByVal strParameterName As String = "", Optional ByVal intParameterNo As Integer = 0, Optional ByVal inrParameterParameterNo As Integer = 0) As String
    public string GetParameterFromUrl(Enums.Enums_URLParameterType intURLParameterType, string strParameter = "", int inrParameterParameterNo = 0)
    {
        string strUrl = GetCurrentUrl(false);
        Array UrlPart = null;
        UrlPart = strUrl.Split('/');
        //if (UrlPart.Length > 0)
        //{
        //    Array.Reverse(UrlPart);
        //}
        //else
        //{
        //    return "";
        //}
        if (UrlPart.Length <= 0)
        {
            return "";
        }

        if (intURLParameterType == Enums.Enums_URLParameterType.ByNumber)
        {
            if (!string.IsNullOrEmpty(strParameter))
            {
                int intParameterNo = Convert.ToInt32(strParameter);
                if (UrlPart.Length >= intParameterNo)
                {
                    if (UrlPart.GetValue(intParameterNo - 1).ToString().Contains("+"))
                    {
                        UrlPart = UrlPart.GetValue(intParameterNo - 1).ToString().Split('+');
                        int parameterParameterno = UrlPart.Length - 1;
                        if ((inrParameterParameterNo) != 0 & ((inrParameterParameterNo - 1) <= parameterParameterno))
                        {
                            parameterParameterno = (inrParameterParameterNo - 1);
                        }

                        return generateOrignalNameFromUrl(UrlPart.GetValue(parameterParameterno).ToString());
                    }
                    else
                    {
                        return generateOrignalNameFromUrl(UrlPart.GetValue(intParameterNo - 1).ToString());
                    }
                }
                else
                {
                    return "";
                }
            }
            return generateOrignalNameFromUrl(UrlPart.GetValue(UrlPart.Length - 1).ToString());
        }
        else if (intURLParameterType == Enums.Enums_URLParameterType.ByName)
        {
            if (!string.IsNullOrEmpty(strParameter))
            {
                for (int i = 0; i <= UrlPart.Length - 1; i++)
                {
                    try
                    {
                        if (UrlPart.GetValue(i).ToString().ToLower().Contains(strParameter.ToLower()))
                        {
                            if (UrlPart.GetValue(i).ToString().Contains("+"))
                            {
                                UrlPart = UrlPart.GetValue(i).ToString().ToString().Split('+');
                                int parameterParameterno = UrlPart.Length - 1;
                                if ((inrParameterParameterNo) != 0 & ((inrParameterParameterNo - 1) <= parameterParameterno))
                                {
                                    parameterParameterno = (inrParameterParameterNo - 1);
                                }
                                return generateOrignalNameFromUrl(UrlPart.GetValue(parameterParameterno).ToString());
                            }
                            else
                            {
                                return generateOrignalNameFromUrl(UrlPart.GetValue(i).ToString());
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        return "";
    }

    public string generateOrignalNameFromUrl(string str)
    {
        string a = str.Replace("-", " ");
        return a;
    }

    public bool ExportToExcel(DataTable objDataTable, ListBox lstbox, string strFileName)
    {
        int i = 0;
        for (i = 0; i < lstbox.Items.Count; i++)
        {
            objDataTable.Columns[lstbox.Items[i].Value.ToString()].SetOrdinal(i);
            objDataTable.Columns[i].ColumnName = lstbox.Items[i].ToString();
            objDataTable.AcceptChanges();
        }
        for (int j = objDataTable.Columns.Count - 1; j >= i; j--)
        {
            objDataTable.Columns.RemoveAt(j);
            objDataTable.AcceptChanges();
        }


        GridView GridView1 = new GridView();
        GridView1.DataSource = objDataTable;
        GridView1.DataBind();

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".xls");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/vnd.ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter hw = new HtmlTextWriter(sw);

        GridView1.AllowPaging = false;
        GridView1.DataBind();
        GridView1.HeaderRow.Style.Add("background-color", "#d0d0d0");

        GridView1.RenderControl(hw);
        string style = "<style> .textmode { mso-number-format:\\@; } </style>";

        HttpContext.Current.Response.Write(style);
        HttpContext.Current.Response.Output.Write(sw.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        return true;
    }

    public bool ExportToCsv(DataTable objDataTable, ListBox lstbox, string strFileName)
    {
        int i = 0;
        for (i = 0; i < lstbox.Items.Count; i++)
        {
            objDataTable.Columns[lstbox.Items[i].Value.ToString()].SetOrdinal(i);
            objDataTable.Columns[i].ColumnName = lstbox.Items[i].ToString();
            objDataTable.AcceptChanges();
        }
        for (int j = objDataTable.Columns.Count - 1; j >= i; j--)
        {
            objDataTable.Columns.RemoveAt(j);
            objDataTable.AcceptChanges();
        }

        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Buffer = true;

        HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".csv");
        HttpContext.Current.Response.Charset = "";
        HttpContext.Current.Response.ContentType = "application/text ";
        StringBuilder sb = new StringBuilder();


        for (int k = 0; k <= objDataTable.Columns.Count - 1; k++)
        {
            //add separator 
            sb.Append(objDataTable.Columns[k].ColumnName + ',');

        }
        sb.Append("\n");
        for (int j = 0; j <= objDataTable.Rows.Count - 1; j++)
        {
            for (int k = 0; k <= objDataTable.Columns.Count - 1; k++)
            {
                //add separator 
                sb.Append(objDataTable.Rows[j][k].ToString() + ',');
            }
            //append new line 
            sb.Append("\n");

        }

        HttpContext.Current.Response.Output.Write(sb.ToString());
        HttpContext.Current.Response.Flush();
        HttpContext.Current.Response.End();

        return true;
    }

    //public bool ExportToPdf(DataTable objDataTable, ListBox lstbox, string strFileName)
    //{
    //    int i = 0;
    //    for (i = 0; i < lstbox.Items.Count; i++)
    //    {
    //        objDataTable.Columns[lstbox.Items[i].Value.ToString()].SetOrdinal(i);
    //        objDataTable.Columns[i].ColumnName = lstbox.Items[i].ToString();
    //        objDataTable.AcceptChanges();
    //    }
    //    for (int j = objDataTable.Columns.Count - 1; j >= i; j--)
    //    {
    //        objDataTable.Columns.RemoveAt(j);
    //        objDataTable.AcceptChanges();
    //    }

    //    GridView GridView1 = new GridView();
    //    GridView1.DataSource = objDataTable;
    //    GridView1.DataBind();

    //    HttpContext.Current.Response.Clear();
    //    HttpContext.Current.Response.Buffer = true;

    //    HttpContext.Current.Response.AddHeader("content-disposition", "attachment;filename=" + strFileName + ".pdf");
    //    HttpContext.Current.Response.Charset = "";
    //    HttpContext.Current.Response.ContentType = "application/pdf";

    //    HttpContext.Current.Response.Cache.SetCacheability(HttpCacheability.NoCache);


    //    StringWriter sw = new StringWriter();
    //    HtmlTextWriter hw = new HtmlTextWriter(sw);

    //    GridView1.AllowPaging = false;
    //    GridView1.DataBind();
    //    GridView1.RenderControl(hw);

    //    StringReader sr = new StringReader(sw.ToString());
    //    Document pdfDoc = new Document(PageSize.A4, 10.0F, 10.0F, 10.0F, 0.0F);
    //    HTMLWorker htmlparser = new HTMLWorker(pdfDoc);
    //    PdfWriter.GetInstance(pdfDoc, HttpContext.Current.Response.OutputStream);

    //    pdfDoc.Open();
    //    htmlparser.Parse(sr);
    //    pdfDoc.Close();
    //    HttpContext.Current.Response.Write(pdfDoc);
    //    HttpContext.Current.Response.End();

    //    return true;
    //}

    public string GetEventParameter(string strParameter)
    {
        return "#" + strParameter + "#";
    }

    public string GenerateCaptchCode()
    {
        Random random = new Random();
        string combination = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
        StringBuilder captcha = new StringBuilder();
        for (int i = 0; i < 4; i++)
        {
            captcha.Append(combination[random.Next(combination.Length)]);

        }
        return captcha.ToString();
    }

    public string GenerateDocketNo(int iRang = 5)
    {
        Random random = new Random();
        string combination = "0123456789";
        StringBuilder captcha = new StringBuilder();
        for (int i = 0; i < iRang; i++)
        {
            captcha.Append(combination[random.Next(combination.Length)]);

        }
        return captcha.ToString();
    }

    //public void AddToCart(string strProductId, string strProductColorId, string strProductDetailID, int intQty, string strPriceDiscount)
    //{
    //    bool IsNew = true;
    //    DataTable dtCart = new DataTable();
    //    if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
    //    {
    //        dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
    //    }
    //    //dtCart = null;
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        DataRow[] dr = dtCart.Select(tblProduct.ColumnNames.AppProductID.ToString() + "=" + strProductId + " and " + tblProductColor.ColumnNames.AppProductColorID + "=" + strProductColorId + " And " + tblProductDetail.ColumnNames.AppProductDetailID + "=" + strProductDetailID);
    //        if (dr.Length > 0)
    //        {
    //            int iQty = Convert.ToInt32(dr[0]["appQty"].ToString());
    //            dr[0]["appQty"] = (iQty + intQty).ToString();
    //            if (strPriceDiscount == "0")
    //            {
    //                decimal appRealDiscountPrice = Convert.ToDecimal(dr[0]["appRealDiscountPrice"]);
    //                decimal appTotalDiscount = Convert.ToDecimal(Convert.ToDecimal(dr[0]["appQty"]) * appRealDiscountPrice);
    //                dr[0]["appDiscountPrice"] = appTotalDiscount;
    //                dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0][tblProductDetail.ColumnNames.AppPrice].ToString()) * (iQty + 1)).ToString();
    //            }
    //            else
    //            {
    //                decimal appRealDiscountPrice = Convert.ToDecimal(strPriceDiscount);
    //                decimal appTotalDiscount = Convert.ToDecimal(Convert.ToDecimal(dr[0]["appQty"]) * Convert.ToDecimal(strPriceDiscount));
    //                dr[0]["appDiscountPrice"] = appTotalDiscount;
    //                dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * (iQty + 1) - (appTotalDiscount)).ToString();
    //                dr[0]["appRealDiscountPrice"] = strPriceDiscount;
    //                //  dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0][tblProductDetail.ColumnNames.AppPrice].ToString()) * (iQty + 1)).ToString();
    //            }
    //            dtCart.AcceptChanges();
    //            HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = dtCart;
    //            IsNew = false;
    //        }

    //    }
    //    if (IsNew)
    //    {
    //        tblProductImage objImg = new tblProductImage();
    //        DataTable objDataTable = objImg.LoadProductInformation(strProductId, strProductColorId, strProductDetailID, intQty);
    //        if (objDataTable.Rows.Count > 0)
    //        {
    //            if (dtCart != null)
    //            {
    //                dtCart.Merge(objDataTable);
    //            }
    //            else
    //            {
    //                dtCart = objDataTable;
    //            }
    //            HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = dtCart;
    //            DataRow[] dr = dtCart.Select(tblProductDetail.ColumnNames.AppProductDetailID.ToString() + "=" + strProductDetailID);
    //            if (dr.Length > 0)
    //            {

    //                    tblProductDetail objProductDetail = new tblProductDetail();
    //                    if (objProductDetail.LoadByPrimaryKey(Convert.ToInt32(strProductDetailID)))
    //                    {

    //                        int iQty = intQty;

    //                        decimal appRealDiscountPrice = Convert.ToDecimal(strPriceDiscount);
    //                        decimal appTotalDiscount = Convert.ToDecimal(Convert.ToDecimal(dr[0]["appQty"]) * Convert.ToDecimal(strPriceDiscount));
    //                        dr[0]["appDiscountPrice"] = appTotalDiscount;
    //                        if (strPriceDiscount == "0")
    //                        {
    //                            dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appPrice"].ToString()) * (iQty) - (appTotalDiscount)).ToString();
    //                        }
    //                        else {
    //                            dr[0]["appTotalPrice"] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * (iQty) - (appTotalDiscount)).ToString();
    //                        }
    //                        dr[0]["appRealDiscountPrice"] = strPriceDiscount;
    //                        dtCart.AcceptChanges();
    //                    }
    //                    objProductDetail = null;
    //                }



    //        }
    //        objImg = null;


    //    }
    //}


   // public void AddToCart(string strProductId, string strProductColorId, string strProductDetailID, int intQty, string strCouponDiscount = "0")
   // {
    //    bool IsNew = true;
    //    DataTable dtCart = new DataTable();
    //    if ((HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] != null))
    //    {
    //        dtCart = (DataTable)HttpContext.Current.Session[appFunctions.Session.Cart.ToString()];
    //    }
    //    //dtCart = null;
    //    if (dtCart.Rows.Count > 0)
    //    {
    //        DataRow[] dr = dtCart.Select(tblProduct.ColumnNames.AppProductID.ToString() + "=" + strProductId + " and " + tblProductColor.ColumnNames.AppProductColorID + "=" + strProductColorId + " And " + tblProductDetail.ColumnNames.AppProductDetailID + "=" + strProductDetailID);
    //        if (dr.Length > 0)
    //        {
    //            int iQty = Convert.ToInt32(dr[0]["appQty"].ToString());

    //            dr[0]["appQty"] = (iQty + intQty).ToString();
    //            if (Convert.ToDecimal(dr[0]["appRealDiscountPrice"]) == 0)
    //            {
    //                if (strCouponDiscount != "0")
    //                {
    //                    dr[0]["appRealDiscountPrice"] = strCouponDiscount;
    //                }
    //            }
    //            decimal appRealDiscountPrice = Convert.ToDecimal(dr[0]["appRealDiscountPrice"]);
    //            decimal appTotalDiscount = Convert.ToDecimal(Convert.ToDecimal(dr[0]["appQty"]) * appRealDiscountPrice);
    //            dr[0]["appDiscountPrice"] = appTotalDiscount;
    //            dr[0]["appTotalPrice"] = ((Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * Convert.ToDecimal(dr[0]["appQty"])) - appTotalDiscount).ToString();
    //            dr[0][tblProductDetail.ColumnNames.AppPrice] = (Convert.ToDecimal(dr[0]["appRealPrice"].ToString()) * Convert.ToDecimal(dr[0]["appQty"])).ToString();

    //            dtCart.AcceptChanges();
    //            HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = dtCart;
    //            IsNew = false;
    //        }

    //    }
    //    if (IsNew)
    //    {
    //        tblProductImage objImg = new tblProductImage();
    //        DataTable objDataTable = objImg.LoadProductInformation(strProductId, strProductColorId, strProductDetailID, intQty, Math.Round(Convert.ToDecimal(strCouponDiscount), 2));
    //        if (objDataTable.Rows.Count > 0)
    //        {
    //            if (dtCart != null)
    //            {
    //                dtCart.Merge(objDataTable);
    //            }
    //            else
    //            {
    //                dtCart = objDataTable;
    //            }
    //            HttpContext.Current.Session[appFunctions.Session.Cart.ToString()] = dtCart;

    //        }
    //        objImg = null;
    //    }
    //}

    public string Generatehash512(string text)
    {
        byte[] message = Encoding.UTF8.GetBytes(text);
        UnicodeEncoding UE = new UnicodeEncoding();
        byte[] hashValue;
        SHA512Managed hashString = new SHA512Managed();
        string hex = "";
        hashValue = hashString.ComputeHash(message);
        foreach (byte x in hashValue)
        {
            hex += String.Format("{0:x2}", x);
        }
        return hex;
    }

}
