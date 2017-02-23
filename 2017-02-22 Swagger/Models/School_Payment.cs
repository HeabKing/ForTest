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
    
    public partial class School_Payment
    {
        public int PaymentId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string SchoolName { get; set; }
        public string SchoolIP { get; set; }
        public string LinkMan { get; set; }
        public string LinkPhone { get; set; }
        public int PayType { get; set; }
        public string PaymentNum { get; set; }
        public decimal PayMoney { get; set; }
        public string ProductName { get; set; }
        public int BuyYear { get; set; }
        public System.DateTime PayTime { get; set; }
        public int Status { get; set; }
        public int HandleStatus { get; set; }
        public string eBankInfo { get; set; }
        public string Remark { get; set; }
        public Nullable<int> ProvinceID { get; set; }
        public Nullable<int> CityID { get; set; }
        public Nullable<int> DistrictID { get; set; }
        public string SchoolAddress { get; set; }
        public string ZipCode { get; set; }
        public string Business { get; set; }
        public string HandleUserID { get; set; }
        public Nullable<System.DateTime> HandleTime { get; set; }
        public string HandleUserName { get; set; }
    }
}