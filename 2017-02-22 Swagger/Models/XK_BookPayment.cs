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
    
    public partial class XK_BookPayment
    {
        public int PaymentID { get; set; }
        public string PaymentNum { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public Nullable<int> eBankID { get; set; }
        public Nullable<decimal> PayMoney { get; set; }
        public Nullable<decimal> TrueMoney { get; set; }
        public Nullable<System.DateTime> PayTime { get; set; }
        public Nullable<System.DateTime> ReceiveTime { get; set; }
        public Nullable<int> Status { get; set; }
        public string eBankInfo { get; set; }
        public string Remark { get; set; }
        public string BookTitle { get; set; }
        public string ComputerSn { get; set; }
        public string UserReName { get; set; }
        public string LinkQQ { get; set; }
        public string LinkPhone { get; set; }
        public string LinkEmail { get; set; }
        public string LinkAddress { get; set; }
    }
}
