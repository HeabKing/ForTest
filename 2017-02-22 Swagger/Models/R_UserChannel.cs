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
    
    public partial class R_UserChannel
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public R_UserChannel()
        {
            this.R_UserChannelClass = new HashSet<R_UserChannelClass>();
        }
    
        public int UserChannelID { get; set; }
        public Nullable<int> UserID { get; set; }
        public Nullable<int> ChannelID { get; set; }
        public Nullable<int> IsAll { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<R_UserChannelClass> R_UserChannelClass { get; set; }
    }
}
