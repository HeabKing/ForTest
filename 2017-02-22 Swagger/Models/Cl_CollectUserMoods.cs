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
    
    public partial class Cl_CollectUserMoods
    {
        public int MoodsID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string YearMonth { get; set; }
        public Nullable<int> MoodsCount { get; set; }
        public Nullable<System.DateTime> LastUpdateTime { get; set; }
    }
}