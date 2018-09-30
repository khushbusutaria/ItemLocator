using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace BusinessLayer
{
    public class Enums
    {

        public enum MessageType
        {
            Information = 1,
            Warning = 2,
            Error = 3,
            Successfull = 4
        }

        public enum InquiryStatus
        {
            open = 1,
            close = 2,
            remain = 3
        }

        public enum RoleType
        {
            SuperAdmin = 1,
            Admin = 2,
            User = 3
        }

        public enum Enum_SortOrderBy
        {
            Asc,
            Desc
        }
        public enum PaymentMode
        {
            COD = 1,
            PayNow = 2

        }

        public enum Enum_DataType
        {
            iInteger = 1,
            sString = 2,
            dDateTime = 3
        }

        public enum Enum_Formula
        {
            None = 1,
            Multiplication = 2,
            Division = 3
        }

        public string JoinWithComma(DataTable objDT, string strColumnName, Enum_DataType objDataType)
        {
            string[] ar = { "" };
            switch (objDataType)
            {
                case Enum_DataType.iInteger:    //By rahul prajapati 06102014
                    ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => Convert.ToString((Int32)row[strColumnName]));
                    break;
                case Enum_DataType.sString:
                    ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => (string)row[strColumnName]);
                    break;
                case Enum_DataType.dDateTime:
                    ar = Array.ConvertAll<System.Data.DataRow, string>(objDT.Select(), (System.Data.DataRow row) => Convert.ToString((DateTime)row[strColumnName]));
                    break;
            }
            return String.Join(",", ar);
        }


        public enum Enum_Specification
        {
            Select = 1,
            Text = 2,
            Boolean = 3
        }
        public enum Enum_TabID
        {
            Dashboard = 5,
            Menus = 3
        }
        public enum Enum_Gender
        {
            Male = 0,
            Female = 1
        }
        public enum Enums_URLParameterType
        {
            ByNumber = 1,
            ByName = 2
        }

        public enum Enums_MenuTypeID
        {
            TopMenu = 3,
            TopMainMenu = 4
        }

        public enum Enums_Type
        {
            Solution = 1,
            Product = 2
        }

        public enum Enums_OrderStatus
        {
            Ordered = 4,
            Confirmed = 1,
            ReadyToShip = 2,
            Shipped = 3,
            Delivered = 5,
            CancelledByCustomer = 6,
            CancelledByAdmin = 8,
            Returned = 7,
            Complete = 9,
            PaymentFail = 10
        }

        public enum Enums_ProductHeader
        {
            Product_Category,
            Product_SubCategory,
            Product_Code,
            Product_Name,
            Product_Tag,
            Description,
            Product_Estimated_Delivery_Days,
            Product_Weight,
            Meta_KeyWord,
            Meta_Description,
            Color_Name,
            Seller_Price,
            MRP,
            Price,
            Quantity,
            SKU_No,
            Size,
            Image
        }

        public enum Enum_ReviewStatus
        {
            Submitted = 1,
            Approved = 2,
            Rejected = 3
        }

        public enum Enum_CouponCodeType
        {
            Category = 1,
            SubCategory = 2,
            Product = 3,
            General = 4
        }

        public enum Enums_PaymentStatus
        {
            Pending = 1,
            success = 2,
            Failure = 3
        }
        public enum Enum_ReturnStatus
        {
            Requested = 1,
            Approved = 2,
            Dispatched = 3,
            Complete = 4,
            Reject = 5,
            Slip = 6
        }
        public enum Enum_SearchTableName
        {
            Featured_Products = 1,
            Best_Seller = 2,
            New_Products = 3,
            Deal_Products = 4

        }
    }
}
