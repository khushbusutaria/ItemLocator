
// Generated by MyGeneration Version # (1.3.0.3)

using System;
using System.Data;
namespace BusinessLayer
{
    public class tblMenuItemType : _tblMenuItemType
    {
        public tblMenuItemType()
        {

        }
        public DataTable LoadMenuItemTypeGridData(string strFieldName, string strSearchText)
        {

            string StrQuery = "select * from tblMenuItemType where 1 = 1";

            if (strSearchText != "")
            {
                StrQuery += "and " + strFieldName + " like '%" + strSearchText + "%'";
            }
            StrQuery += " order by appDisplayOrder";
            base.LoadFromRawSql(StrQuery);
            return base.DefaultView.Table;
        }

    }
}
