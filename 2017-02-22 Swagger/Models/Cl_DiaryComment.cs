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
    
    public partial class Cl_DiaryComment
    {
        public int CommentID { get; set; }
        public Nullable<int> DiaryID { get; set; }
        public Nullable<int> AuthorUserID { get; set; }
        public string AuthorUserName { get; set; }
        public Nullable<short> AuthorViewStatus { get; set; }
        public Nullable<int> CommentUserID { get; set; }
        public string CommentUserName { get; set; }
        public string Content { get; set; }
        public Nullable<System.DateTime> CreateTime { get; set; }
        public Nullable<int> ReplyCount { get; set; }
        public Nullable<int> UnReadReplyCount { get; set; }
    }
}
