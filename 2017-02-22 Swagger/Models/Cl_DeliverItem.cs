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
    
    public partial class Cl_DeliverItem
    {
        public int DeliverID { get; set; }
        public Nullable<int> OrderID { get; set; }
        public Nullable<int> UserID { get; set; }
        public string UserName { get; set; }
        public string ConSignee { get; set; }
        public Nullable<int> Direction { get; set; }
        public string Handler { get; set; }
        public string DeliverCompany { get; set; }
        public string DeliverSn { get; set; }
        public string Inputer { get; set; }
        public string Remark { get; set; }
        public bool Received { get; set; }
        public Nullable<System.DateTime> DeliverTime { get; set; }
    }
}
