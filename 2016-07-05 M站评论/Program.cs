using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using _2016_07_05_M站评论;

namespace _2016_07_05_M站评论
{
    /// <summary>
    /// 评论视图模型
    /// </summary>
    /// <remarks>何士雄 2016-07-05</remarks>
    // ReSharper disable InconsistentNaming
    public class GetCommentList
    {
        public int statuscode { get; set; }
        public int totalcount { get; set; }
        public string message { get; set; }
        public List<CommentViewModel> result { get; set; }
    }

    public class CommentViewModel
    {
        public string CommentID { get; set; }
        public int InfoID { get; set; }
        public int SourceID { get; set; }
        public string Contents { get; set; }
        public int Score { get; set; }
        public int PraiseCount { get; set; }
        public int ReplyCount { get; set; }
        public string IP { get; set; }
        public bool IsHot { get; set; }
        public int CommentUserID { get; set; }
        public string CommentUserName { get; set; }
        public DateTime CommentTime { get; set; }
        public bool Passed { get; set; }
        public int LastCensorID { get; set; }
        public string ExamineReason { get; set; }
        public string ExamineTime { get; set; }
        public List<ReplyComment> ReplyList { get; set; }
        public DateTime AfterCommentTime { get; set; }
        public string InfoIDs { get; set; }
        public string ScoreType { get; set; }
        public bool IsScore { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string OrderStr { get; set; }
    }
    public class ReplyComment
    {
        public int ReplyID { get; set; }
        public int CommentID { get; set; }
        public int ParentID { get; set; }
        public string Contents { get; set; }
        public int ReplyCount { get; set; }
        public int PraiseCount { get; set; }
        public string IP { get; set; }
        public int ReplyUserID { get; set; }
        public string ReplyUserName { get; set; }
        public DateTime ReplyTime { get; set; }
        public int ReplySourceID { get; set; }
        public bool IsDelete { get; set; }
        public string RRName { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public string OrderStr { get; set; }
    }
}


class Program
{
    static void Main(string[] args)
    {
        //http://comment.49105.zxxk.com:8424/Comment/CommentApi.asmx/GetCommentList?callback=jQuery110204364940873347223_1467696310766&jsonStr={"InfoId":694082,"SourceID":2,"ProductID":1,"PageIndex":1,"PageSize":15,"OrderStr":"[IsHot] desc,[PraiseCount] desc,[CommentTime]"}&_=1467696310767
        string getSoftCommentsUrl = "http://comment.49105.zxxk.com:8424/Comment/CommentApi.asmx/GetCommentList?callback=jQuery110204364940873347223_1467696310766&jsonStr=%7B%22InfoId%22%3A694082%2C%22SourceID%22%3A2%2C%22ProductID%22%3A1%2C%22PageIndex%22%3A1%2C%22PageSize%22%3A15%2C%22OrderStr%22%3A%22%5BIsHot%5D+desc%2C%5BPraiseCount%5D+desc%2C%5BCommentTime%5D%22%7D&_=1467696310767";
        //http://comment.49105.zxxk.com:8424/Comment/CommentApi.asmx/GetComment?commentId=766
        HttpClient client = new HttpClient();
        var task = client.GetStringAsync(getSoftCommentsUrl);
        var result = task.Result;
        var r = result.Substring(result.IndexOf('(')).Trim().Trim('(').Trim(')').Trim('[').Trim(']');
        var commentList = Newtonsoft.Json.JsonConvert.DeserializeObject<GetCommentList>(r);

        GetReplyComment(1684);

        AddComment();
    }

    public static void GetReplyComment(int commentId)
    {
        string getReplyCommentUrl = "http://api.comment.zxxk.com/Reply/ReplyApi.asmx/GetReplyByComment?jsonStr={\"CommentID\":" + commentId + ",\"PageIndex\":1,\"PageSize\":15}";
        HttpClient client = new HttpClient();
        var task = client.GetStringAsync(getReplyCommentUrl);
        var result = task.Result;
        var r = result.Substring(result.IndexOf('(')).Trim().Trim('(').Trim(')').Trim('[').Trim(']');
    }

    public static void AddComment()
    {
        int infoId = 695166;
        string contents = "为了看日出我常常早起";
        int score = 5;
        int commentUserId = 23264097;
        string commentUserName = "HeabKing";
        int sourceId = 2;
        int productId = 1;
        string a = $"http://a";
        string addCommentUrl = "http://api.comment.zxxk.com/Comment/CommentApi.asmx/AddComment?callback=jQuery110205584461810067296_1467705133505&jsonStr={%22InfoId%22:"+infoId+",%22contents%22:%22"+contents+"%22,%22Score%22:"+score+",%22CommentUserID%22:"+commentUserId+",%22CommentUserName%22:%22"+commentUserName+"%22,%22SourceID%22:"+sourceId+",%22ProductID%22:"+productId+"}&_=1467705133507";
        HttpClient client = new HttpClient();
        var r = client.GetStringAsync(addCommentUrl).Result;
    }
}
