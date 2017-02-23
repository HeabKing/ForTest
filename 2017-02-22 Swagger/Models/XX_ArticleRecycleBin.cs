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
    
    public partial class XX_ArticleRecycleBin
    {
        public int ArticleID { get; set; }
        public int ChannelID { get; set; }
        public int ClassID { get; set; }
        public int SpecialID { get; set; }
        public Nullable<int> FeatureID { get; set; }
        public Nullable<int> DepartmentID { get; set; }
        public string Prefixion { get; set; }
        public string Title { get; set; }
        public string FontColor { get; set; }
        public int FontType { get; set; }
        public string TitleIntact { get; set; }
        public string Keyword { get; set; }
        public int AreaID { get; set; }
        public string Author { get; set; }
        public int AuthorID { get; set; }
        public string CopyFrom { get; set; }
        public string ArticleLevel { get; set; }
        public decimal ArticlePoint { get; set; }
        public decimal ArticleMoney { get; set; }
        public int Stars { get; set; }
        public int Hits { get; set; }
        public int DayHits { get; set; }
        public int WeekHits { get; set; }
        public int MonthHits { get; set; }
        public bool OnTop { get; set; }
        public bool Elite { get; set; }
        public bool Passed { get; set; }
        public Nullable<int> IncludePic { get; set; }
        public string PicUrl { get; set; }
        public string Intro { get; set; }
        public string Content { get; set; }
        public bool IsLink { get; set; }
        public string HtmlFileUrl { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string Censor { get; set; }
        public Nullable<System.DateTime> CensorTime { get; set; }
        public System.DateTime UpdateTime { get; set; }
        public System.DateTime AddTime { get; set; }
        public System.DateTime LastHitTime { get; set; }
        public Nullable<int> GoodCount { get; set; }
        public Nullable<int> PoorCount { get; set; }
        public System.Guid InfoGuid { get; set; }
    }
}
