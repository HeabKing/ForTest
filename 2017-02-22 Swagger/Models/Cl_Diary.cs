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
    
    public partial class Cl_Diary
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public System.DateTime DiaryDate { get; set; }
        public string Weather { get; set; }
        public string Mood { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> AddTime { get; set; }
        public int Purview { get; set; }
        public string Friends { get; set; }
        public Nullable<int> SeeTimes { get; set; }
        public string DiaryBg { get; set; }
        public bool Deleted { get; set; }
    }
}
