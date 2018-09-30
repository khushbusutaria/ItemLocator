using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using BusinessLayer;
public partial class UserControls_ExportFile : System.Web.UI.UserControl
{
    DataTable objExportData;
    DataTable objListData;
    string strFileName;
    public event EventHandler btnClick;

    public void SetListBoxData(DataTable objTable)
    {
        objListData = new DataTable();
        objListData = objTable;
    }

    public void SetExportData(DataTable objTable)
    {
        objExportData = new DataTable();
        objExportData = objTable;
    }

    public void SetFileName(string  name)
    {
        strFileName = name;
    }
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    public void Show()
    {
        lstSelectedColumn.DataTextField = "Text";
        lstSelectedColumn.DataValueField = "ColumnName";
        lstSelectedColumn.DataSource = objListData;
        lstSelectedColumn.DataBind();
        lstAllColumn.Items.Clear();
        mpeExport.Show();
    }

    protected void lnkAllRight_Click(object sender, EventArgs e)
    {
        foreach (System.Web.UI.WebControls.ListItem lstItem in lstAllColumn.Items)
        {
            lstSelectedColumn.Items.Add(lstItem);
        }
        lstAllColumn.Items.Clear();
    }

    protected void lnkRight_Click(object sender, EventArgs e)
    {
        foreach (System.Web.UI.WebControls.ListItem listitem in lstAllColumn.Items)
        {
            if (listitem.Selected == true)
            {
                lstSelectedColumn.Items.Add(listitem);
            }
        }

        foreach (System.Web.UI.WebControls.ListItem listitem in lstSelectedColumn.Items)
        {
            if (listitem.Selected == true)
            {
                lstAllColumn.Items.Remove(listitem);
            }
        }
        lstSelectedColumn.ClearSelection();
    }
    protected void lnkLeft_Click(object sender, EventArgs e)
    {
        foreach (System.Web.UI.WebControls.ListItem listitem in lstSelectedColumn.Items)
        {
            if (listitem.Selected == true)
            {
                lstAllColumn.Items.Add(listitem);
            }
        }

        foreach (System.Web.UI.WebControls.ListItem listitem in lstAllColumn.Items)
        {
            if (listitem.Selected == true)
            {
                lstSelectedColumn.Items.Remove(listitem);
            }
        }

        lstAllColumn.ClearSelection();
    }

    protected void lnkAllLeft_Click(object sender, EventArgs e)
    {
        foreach (System.Web.UI.WebControls.ListItem lstItem in lstSelectedColumn.Items)
        {
            lstAllColumn.Items.Add(lstItem);
        }
        lstSelectedColumn.Items.Clear();

    }

    protected void lnkExcel_Click(object sender, EventArgs e)
    {
        if (lstSelectedColumn.Items.Count > 0)
        {

            btnClick(this, e);

            if (objExportData != null && objExportData.Rows.Count > 0)
            {

                clsCommon objCommon = new clsCommon();
                string strDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                if (objCommon.ExportToExcel(objExportData, lstSelectedColumn, strFileName + strDate))
                {
                    //  DInfo.ShowMessage("Export File.", Enums.MessageType.Error);
                }
                objCommon = null;
            }
        }
        else
        {
            DInfoExport.ShowMessage("Select column for Export.", Enums.MessageType.Error);
        }

    }

    protected void lnkCsv_Click(object sender, EventArgs e)
    {
        if (lstSelectedColumn.Items.Count > 0)
        {
            btnClick(this, e);
            if (objExportData != null && objExportData.Rows.Count > 0)
            {
                clsCommon objCommon = new clsCommon();
                string strDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
                if (objCommon.ExportToCsv(objExportData, lstSelectedColumn, strFileName + strDate))
                {
                    //  DInfo.ShowMessage("Export to .Pdf File.", Enums.MessageType.Error);
                }
                objCommon = null;
            }
        }
        else
        {
            DInfoExport.ShowMessage("Select column for Export.", Enums.MessageType.Error);
        }

    }

    protected void lnkPdf_Click(object sender, EventArgs e)
    {
        //if (lstSelectedColumn.Items.Count > 0)
        //{
        //    btnClick(this, e);
        //    if (objExportData != null && objExportData.Rows.Count > 0)
        //    {
        //        clsCommon objCommon = new clsCommon();
        //        string strDate = DateTime.Now.Day.ToString() + DateTime.Now.Month.ToString() + DateTime.Now.Year.ToString() + DateTime.Now.Hour.ToString() + DateTime.Now.Minute.ToString() + DateTime.Now.Second.ToString() + DateTime.Now.Millisecond.ToString();
        //        if (objCommon.ExportToPdf(objExportData, lstSelectedColumn, strFileName + strDate))
        //        {
        //            //DInfo.ShowMessage("Export to .Pdf File.", Enums.MessageType.Error);
        //        }
        //        objCommon = null;
        //    }
        //}
        //else
        //{
        //    DInfoExport.ShowMessage("Select column for Export.", Enums.MessageType.Error);
        //}

    }

 
}