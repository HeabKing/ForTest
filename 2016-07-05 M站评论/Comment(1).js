
//发表评论内容
var comment_content = "comment_content";
//评论块
var comment_item = "comment_item";
//评论的回复内容
var comment_reply_content = "comment_reply_content";
//评论回复数
var comment_reply_count = "comment_reply_count";
//评论点赞数
var comment_praise_count = "comment_praise_count";
//回复点赞数
var reply_praise_count = "reply_praise_count";
//评论下面的回复框
var comment_reply_item = "comment_reply_item";
//回复下面的回复框
var reply_reply_item = "reply_reply_item";
//回复块
var reply_item = "reply_item";
//回复下面的回复内容
var reply_reply_content = "reply_reply_content";
//回复的回复数
var reply_reply_count = "reply_reply_count";

//打分块
var comment_set_score = "comment_set_score";
//得分块
var comment_get_score = "comment_get_score";

//站点登陆地址
var targetlogin = "http://m.zxxk.com/user/index.html?ReturnUrl=";
//评论详情地址
var http_commentinfourl = "http://m.zxxk.com/comment/commentinfo?commentid=";

var templete = {
    //评论列表
    commentlist: "{{#result}}"
                        + "<div class=\"comment-pl-det\" commenttype=\"" + comment_item + "_{{CommentID}}\">"
            	            + "<div class=\"comment-clearfix comment-per-area\" >"
                	            + "<span class=\"comment-fl\">{{CommentUserName}}</span>"
                                + "<span class=\"comment-fr comment-time\">{{CommentTime}}</span>"
                            + "</div>"
                            + "<div class=\"comment-pl-cont\">{{Contents}}</div>"
                            + "<div class=\"comment-clearfix comment-pl-obt\">"
                	            + "<div class=\"comment-fl comment-clearfix comment-obt-btns\">"
                    	           + " <a href=\"javascript:void(0)\" onclick=\"comment_praise(this,{{CommentID}})\" praise=\"{{PraiseCount}}\"><span class=\"comment-zan\"><i></i>(<c commenttype=\"" + comment_praise_count + "_{{CommentID}}\">{{PraiseCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"comment_reply({{CommentID}})\" ><span class=\"comment-huifu\"><i></i>(<c commenttype=\"" + comment_reply_count + "_{{CommentID}}\">{{ReplyCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"report_add(this,{{CommentID}},0)\" ><span class=\"comment-jubao\"><i></i>举报</span></a>"
                                + "</div>"
                                + "<a class=\"comment-fr comment-ck-btn\" href=\""+http_commentinfourl+"{{CommentID}}\">查看详情</a>"
                            + "</div>"
                              + " <div class=\"comment-sld-pl\" commenttype=\"" + comment_reply_item + "_{{CommentID}}\">"
                                    + " <div class=\"comment-clearfix comment-pop-pl\" >"
                	                + " <input class=\"comment-fl\" type=\"text\" placeholder=\"回复 {{CommentUserName}}:\" commenttype=\"" + comment_reply_content + "_{{CommentID}}\" />"
                                    + " <a class=\"comment-fr comment-fb-btn\" username=\"{{CommentUserName}}\" onclick=\"reply_add(this,{{CommentID}},0)\" >发表</a>"
                                    + " </div>"
                                + "</div>"
                        + "</div>"
                + "{{/result}}",

    //我的评论
    mycommentlist: "{{#result}}" +
                    "<div class=\"comment-pl-mod comment-mypl-list\">" +
                        "<div class=\"pl-det\">" +
                            "<div class=\"comment-pl-cont\">" +
                                "{{Contents}}" +
                            "</div>" +
                            "<div class=\"comment-clearfix\">" +
                                "<span class=\"comment-fl comment-time\">{{CommentTime}}</span><a class=\"comment-fr comment-ck-btn\" href=\""+http_commentinfourl+"{{CommentID}}\">查看详情</a>" +
                            "</div>" +
                        "</div>" +
                     "</div>" +
                   "{{/result}}",
    //列表页追加评论内容
    newcomment: "<div class=\"comment-pl-det\">"
            	            + "<div class=\"comment-clearfix comment-per-area\">"
                	            + "<span class=\"comment-fl\">{{CommentUserName}}</span>"
                                + "<span class=\"comment-fr comment-time\">{{CommentTime}}</span>"
                            + "</div>"
                            + "<div class=\"comment-pl-cont\">{{Contents}}</div>"
                            + "<div class=\"comment-clearfix comment-pl-obt\">"
                	            + "<div class=\"comment-fl comment-clearfix comment-obt-btns\">"
                                + "</div>"
                            + "</div>"
                        + "</div>",
    //评论详情
    commentinfo: "<div class=\"comment-pl-det\" commenttype=\"" + comment_item + "_{{CommentID}}\">"
            	            + "<div class=\"comment-clearfix comment-per-area\">"
                	            + "<span class=\"comment-fl\">{{CommentUserName}}</span>"
                                + "<span class=\"comment-fr comment-time\">{{CommentTime}}</span>"
                            + "</div>"
                            + "<div class=\"comment-pl-cont\">{{Contents}}</div>"
                            + "<div class=\"comment-clearfix comment-pl-obt\">"
                	            + "<div class=\"comment-fl comment-clearfix comment-obt-btns\">"
                    	           + " <a href=\"javascript:void(0)\" onclick=\"comment_praise(this,{{CommentID}})\" praise=\"{{PraiseCount}}\"><span class=\"comment-zan\"><i></i>(<c commenttype=\"" + comment_praise_count + "_{{CommentID}}\">{{PraiseCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"comment_reply({{CommentID}})\" ><span class=\"comment-huifu\"><i></i>(<c commenttype=\"" + comment_reply_count + "_{{CommentID}}\">{{ReplyCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"report_add(this,{{CommentID}},0)\" ><span class=\"comment-jubao\"><i></i>举报</span></a>"
                                + "</div>"
                            + "</div>"
                              + " <div class=\"comment-sld-pl\" commenttype=\"" + comment_reply_item + "_{{CommentID}}\">"
                                    + " <div class=\"comment-clearfix comment-pop-pl\" >"
                	                + " <input class=\"comment-fl\" type=\"text\" placeholder=\"回复 {{CommentUserName}}:\" commenttype=\"" + comment_reply_content + "_{{CommentID}}\" />"
                                    + " <a class=\"comment-fr comment-fb-btn\" username=\"{{CommentUserName}}\" onclick=\"reply_add(this,{{CommentID}},0)\" >发表</a>"
                                    + " </div>"
                                + "</div>"
                        + "</div>",
    //回复列表
    replylist: "{{#result}}"
                        + "<div class=\"comment-pl-det comment-sub-pldet\" replytype=\"" + reply_item + "_{{ReplyID}}\">"
            	            + "<div class=\"comment-clearfix comment-per-area\">"
                	            + "<div class=\"comment-fl comment-sub-bl\"><span>{{ReplyUserName}}</span>回复<span>{{RRName}}</span></div>"
                                + "<span class=\"comment-fr comment-time\">{{ReplyTime}}</span>"
                            + "</div>"
                            + "<div class=\"comment-pl-cont\">{{Contents}}</div>"
                            + "<div class=\"comment-clearfix comment-pl-obt\">"
                	            + "<div class=\"comment-fl comment-clearfix comment-obt-btns\">"
                    	           + " <a href=\"javascript:void(0)\" onclick=\"reply_praise(this,{{ReplyID}})\" praise=\"{{PraiseCount}}\"><span class=\"comment-zan\"><i></i>(<c replytype=\"" + reply_praise_count + "_{{ReplyID}}\">{{PraiseCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"reply_reply({{ReplyID}})\" ><span replycount=\"{{ReplyCount}}\" class=\"comment-huifu\"><i></i>(<c replytype=\"" + reply_reply_count + "_{{ReplyID}}\">{{ReplyCount}}</c>)</span></a>"
                                   + " <a href=\"javascript:void(0)\" onclick=\"report_add(this,{{ReplyID}},1)\" ><span class=\"comment-jubao\"><i></i>举报</span></a>"
                                + "</div>"

                            + "</div>"
                              + " <div class=\"comment-sld-pl\" replytype=\"" + reply_reply_item + "_{{ReplyID}}\" >"
                                    + " <div class=\"comment-clearfix comment-pop-pl\" >"
                	                + " <input class=\"comment-fl\" type=\"text\" placeholder=\"回复 {{ReplyUserName}}:\" replytype=\"" + reply_reply_content + "_{{ReplyID}}\" />"
                                    + " <a class=\"comment-fr comment-fb-btn\" username=\"{{ReplyUserName}}\" onclick=\"reply_add(this,{{CommentID}},{{ReplyID}})\" >发表</a>"
                                    + " </div>"
                                + "</div>"
                        + "</div>"
                        + "{{/result}}",
    //追加回复内容
    replyinfo: "<div class=\"comment-pl-det comment-sub-pldet\">"
            	            + "<div class=\"comment-clearfix comment-per-area\">"
                	            + "<div class=\"comment-fl comment-sub-bl\"><span>{{ReplyUserName}}</span>回复<span>{{RRName}}</span></div>"
                                + "<span class=\"comment-fr comment-time\">{{ReplyTime}}</span>"
                            + "</div>"
                            + "<div class=\"comment-pl-cont\">{{Contents}}</div>"
                            + "<div class=\"comment-clearfix comment-pl-obt\">"
                	            + "<div class=\"comment-fl comment-clearfix comment-obt-btns\">"
                                + "</div>"
                            + "</div>"
                        + "</div>",
    /*添加评论*/
    addcomment: "<section class=\"comment-plarea\"><div class=\"comment-int-area\"><textarea class=\"comment-lt-grey\" id=\"plint\" commenttype=\"" + comment_content + "\" cols=\"37\" rows=\"12\" {disabled} placeholder=\"{placeholder}\" maxlength=\"180\" onchange=\"this.value=this.value.substring(0, 180)\" onkeydown=\"this.value=this.value.substring(0, 180)\" onkeyup=\"this.value=this.value.substring(0, 180)\" ></textarea></div>"
                     + "<div class=\"comment-clearfix comment-cz-brns\">"
                         + "<a class=\"comment-fb\" href=\"javascript:addcomment();\">发表</a>"
                     + "</div></section>",
 /*列表页title*/
    commentlisttitle:"<div class=\"comment-pl-sep\"  commenttype=\"comment_add\">评论列表<span>{totalcount}条评论</span></div>",
    //回复加载更多
    reply_loadmore: "<a href=\"javascript:void(0)\" class=\"comment-pl-loadmore\" onclick=\"reply_loadmore(this,{{CommentID}},{{PageIndex}})\">加载更多</a>",
    //评论加载更多
    comment_loadmore: "<a href=\"javascript:void(0)\" class=\"comment-pl-loadmore\" onclick=\"comment_loadmore(this,{{InfoID}},{{PageIndex}})\">加载更多</a>",
    //我的评论加载更多
    mycomment_loadmore: "<a href=\"javascript:void(0)\" class=\"comment-pl-loadmore\" onclick=\"mycomment_loadmore(this,{{PageIndex}})\">加载更多</a>",
    report: "<span class=\"comment-jubao\" style=\"color:Red\"><i></i>已举报</span>"
}