//------------------------------------------------------------------------------
// <auto-generated>
//     此代码已从模板生成。
//
//     手动更改此文件可能导致应用程序出现意外的行为。
//     如果重新生成代码，将覆盖对此文件的手动更改。
// </auto-generated>
//------------------------------------------------------------------------------

namespace _2017_02_22_Swagger.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Cl_BankrollItem
    {
        public int ItemID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public Nullable<decimal> PayMoney { get; set; }
        public Nullable<decimal> TrueMoney { get; set; }
        public Nullable<int> PayType { get; set; }
        public Nullable<int> CurrencyType { get; set; }
        public string BankName { get; set; }
        public Nullable<int> ItemType { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> PaymentID { get; set; }
        public string Remark { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}
