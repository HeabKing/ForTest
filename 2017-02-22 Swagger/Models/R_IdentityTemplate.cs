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
    
    public partial class R_IdentityTemplate
    {
        public int IdentityTemplateID { get; set; }
        public Nullable<int> IdentityID { get; set; }
        public Nullable<int> FlowStepOperID { get; set; }
        public Nullable<int> InfoID { get; set; }
        public Nullable<int> TypeFlag { get; set; }
    
        public virtual CID_UserIdentity CID_UserIdentity { get; set; }
    }
}
