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
    
    public partial class XK_RMBincomePayLog
    {
        public int ID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string UserReName { get; set; }
        public string UserPhone { get; set; }
        public string UserMobile { get; set; }
        public string UserQQ { get; set; }
        public string UserEmail { get; set; }
        public string UserAddress { get; set; }
        public Nullable<decimal> PayRMB { get; set; }
        public Nullable<decimal> PayCharge { get; set; }
        public Nullable<decimal> PayTotal { get; set; }
        public string PayBank { get; set; }
        public string PayCardName { get; set; }
        public string PayCardNumber { get; set; }
        public string PayIDCardNumber { get; set; }
        public string PayCardProvince { get; set; }
        public Nullable<System.DateTime> PayTime { get; set; }
        public string Remark { get; set; }
        public Nullable<byte> IsDeduction { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
    }
}