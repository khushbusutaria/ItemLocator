using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using BusinessLayer;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

public partial class Admin_Query : PageBase_Admin
{
    #region Declared Variables
    SqlConnection conn = new SqlConnection(ConfigurationManager.AppSettings["dbconnection"].ToString());
    SqlCommand cmd = new SqlCommand();
    SqlDataAdapter adp = new SqlDataAdapter();
    #endregion


    #region Helper Methods
    DataTable ExecuteDatatable(string strQuery, out string strResult)
    {
        try
        {
            DataTable dtData = new DataTable();
            cmd.Connection = conn;
            cmd.CommandText = strQuery;
            cmd.CommandType = CommandType.Text;
            adp.SelectCommand = cmd;
            adp.Fill(dtData);
            strResult = string.Empty;
            return dtData;
        }
        catch (Exception ex)
        {
            strResult = ex.Message;
            return null;
        }
    }

    bool ExecuteNonQuery(string strQuery, out string strResult, bool isStoreProcedure)
    {
        try
        {
            cmd.Connection = conn;
            cmd.CommandText = strQuery;
            if (isStoreProcedure == true)
            { cmd.CommandType = CommandType.StoredProcedure; }
            else { cmd.CommandType = CommandType.Text; }
            conn.Open();
            cmd.ExecuteNonQuery();
            strResult = "Query Executed Successfully...";
            return true;
        }
        catch (Exception ex)
        {
            strResult = ex.Message;
            return false;
        }
        finally
        {
            conn.Close();
        }
    }
    #endregion



    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {

        }
    }





    //private bool Execute()
    //{
    //    objBanner = new tblBanner();
    //    string strQuery = txtBannerTitle.Text.ToLower();
    //    int IndexForUpdate = strQuery.IndexOf("UPDATE".ToLower());
    //    int IndexForInsert = strQuery.IndexOf("INSERT".ToLower());
    //    int IndexForSelect = strQuery.IndexOf("Select".ToLower());

    //    if (IndexForUpdate < 0 && IndexForInsert < 0)
    //    {
    //        int index = 0;

    //        objDataTable = objBanner.ExecuteQuery(txtBannerTitle.Text);
    //        string strError = objDataTable.Rows[0][0].ToString();
    //        string[] columnNames = objDataTable.Columns.Cast<DataColumn>()
    //                                 .Select(x => x.ColumnName)
    //                                 .ToArray();
    //        index = strError.IndexOf("Exception");
    //        if (index <= 0)
    //        {
    //            if (objDataTable.Rows.Count <= 0)
    //            {
    //                DInfo.ShowMessage("No data found", Enums.MessageType.Information);
    //            }
    //            else
    //            {
    //                string html = "<table class='table table-striped table-bordered table-hover' cellspacing='0' border='1' style='width:100%;border-collapse:collapse;' rules='all'>";

    //                html += "<tr style='white-space:nowrap;'>";
    //                for (int i = 0; i < objDataTable.Columns.Count; i++)
    //                    html += "<th style='width:1%;'>" + objDataTable.Columns[i].ColumnName + "</th>";
    //                html += "</tr>";

    //                for (int i = 0; i < objDataTable.Rows.Count; i++)
    //                {
    //                    html += "<tr>";
    //                    for (int j = 0; j < objDataTable.Columns.Count; j++)
    //                        html += "<td  align='center' style='width:1%;'>" + objDataTable.Rows[i][j].ToString() + "</td>";
    //                    html += "</tr>";
    //                }
    //                html += "</table>";


    //                QueryResult.InnerHtml = html;
    //            }
    //        }
    //        else
    //        {
    //            DInfo.ShowMessage(strError, Enums.MessageType.Error);
    //        }
    //    }
    //    else
    //    {
    //        string connString = ConfigurationManager.AppSettings["dbconnection"];

    //        using (SqlConnection con = new SqlConnection(connString))
    //        {
    //            using (SqlCommand cmd = new SqlCommand(strQuery, con))
    //            {

    //                con.Open();
    //                try
    //                {
    //                    int rowsAffected = -1;
    //                    if (IndexForInsert >= 0)
    //                    {
    //                        rowsAffected = cmd.ExecuteNonQuery();
    //                        if (rowsAffected >= 0)
    //                        {
    //                            DInfo.ShowMessage(rowsAffected + " rows inserted", Enums.MessageType.Successfull);
    //                        }
    //                    }
    //                    else
    //                    {
    //                        rowsAffected = cmd.ExecuteNonQuery();
    //                        if (rowsAffected >= 0 && IndexForUpdate >= 0)
    //                        {
    //                            DInfo.ShowMessage(rowsAffected + " rows affected", Enums.MessageType.Successfull);
    //                        }
    //                    }
    //                }
    //                catch (Exception e)
    //                {

    //                    System.Console.WriteLine("READING:");
    //                    System.Console.WriteLine("  ERROR:" + e.Message);
    //                    System.Console.WriteLine("  SQL  :" + strQuery);
    //                    System.Console.WriteLine("  Conn.:" + connString);
    //                    DInfo.ShowMessage(e.ToString(), Enums.MessageType.Error);
    //                }
    //                finally
    //                {
    //                    con.Close();
    //                }
    //            }
    //        }
    //    }

    //    objBanner = null;
    //    return true;
    //}

    protected void btnBack_Click(object sender, EventArgs e)
    {
        Response.Redirect("Banner.aspx", true);
    }

    private void ResetControls()
    {
        txtBannerTitle.Text = "";

    }
    protected void btnClear_Click(object sender, EventArgs e)
    {
        ResetControls();
        QueryResult.InnerHtml = "";
    }

    protected void btnExecute_Click(object sender, EventArgs e)
    {
        string strResult = string.Empty;
        switch (txtBannerTitle.Text.Trim().Split(' ')[0].ToString().ToLower())
        {
            case "truncate":
            case "delete":
            case "insert":
            case "update":
            case "drop":
                if (ExecuteNonQuery(txtBannerTitle.Text.Trim(), out strResult, false))
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Information);
                }
                else
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Error);
                }
                break;
            case "exec":
                if (ExecuteNonQuery(txtBannerTitle.Text.Trim(), out strResult, true))
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Information);
                }
                else
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Error);
                }
                break;
            case "create":
                if (ExecuteNonQuery(txtBannerTitle.Text.Trim(), out strResult, false))
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Information);
                }
                else
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Error);
                }
                break;
            case "select":
                DataTable dt = ExecuteDatatable(txtBannerTitle.Text.Trim(), out strResult);
                if (string.IsNullOrWhiteSpace(strResult))
                {
                    gvResult.DataSource = dt;
                    gvResult.DataBind();

                }
                else
                {
                    DInfo.ShowMessage(strResult, Enums.MessageType.Error);
                    gvResult.DataSource = null;
                    gvResult.DataBind();
                }

                break;
        }

    }


}