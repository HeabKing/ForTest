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
    
    public partial class Cl_Comment
    {
        public int CommentID { get; set; }
        public int ChannelID { get; set; }
        public int ClassID { get; set; }
        public int InfoID { get; set; }
        public Nullable<int> UserLevel { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string IP { get; set; }
        public Nullable<System.DateTime> WriteTime { get; set; }
        public Nullable<int> Score { get; set; }
        public string Content { get; set; }
        public string ReplyName { get; set; }
        public string ReplyContent { get; set; }
        public Nullable<System.DateTime> ReplyTime { get; set; }
        public bool Passed { get; set; }
        public Nullable<int> Status { get; set; }
    }
}
