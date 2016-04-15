//var http_url = "http://localhost:6239/";
var http_url="http://api.comment.zxxk.com/"

//模板
var templete_type = 1;

/*初始化入口*/
$(function () {
    //  commentroot().html("<div style=\"margin-left: auto;margin-right: auto;width:200px\"><span style=\" float:left\">正在加载评论...</span><i class=\"comment_loading\"></i></div>");
    $('head').append('<link href="' + http_url + '/Web/css/CommentMain(' + templete_type + ').css" rel="stylesheet" type="text/css" />');

    comment_onload(function () {
        if (comment_pagetype == 1) {            //加载评论列表页
            commentlistonload();

        } else if (comment_pagetype == 2) {     //加载评论详情页
            // alert("评论详情");
            commentinfoonload();
        } else if (comment_pagetype == 3) {     //加载我的评论
            //我的评论
            mycommentlistonload();
        }


    })

})
/*加载评论列表*/
function commentlistonload() {
    var htm = "";
    var msg = "说点什么...不要超过180字";
    var dis = "";
    if (typeof (is_comment) != "undefined") {
        if (is_comment == 0) {//当评论权限不够时；
            msg = comment_msg;
        }
    }
    if (parseInt(comment_userid) <= 0 || comment_username == "") {
        msg = "评论前请先登录";
        dis = "onfocus=\"comment_login()\""
    }

    htm += templete.addcomment.replace("{disabled}", dis).replace("{placeholder}", msg);
    commentroot().append(htm);
    comment.getcommentlist(comment_infoid, 15, 1, '"[IsHot] desc,[PraiseCount] desc,[CommentTime]"', function (j) {
        var htm = "";

        htm += templete.commentlisttitle.replace("{totalcount}", j.totalcount);

        htm += Mustache.render(templete.commentlist, j);
        if (j.result && j.result.length > 14) {
            var loadmore = '{"InfoID":' + comment_infoid + ',"PageIndex":' + (parseInt(1) + 1) + '}';
            htm += Mustache.render(templete.comment_loadmore, $.parseJSON(loadmore));
        }
        if (comment_host != "m.zxxk.com" && htm.match("http://m.zxxk.com/comment/commentinfo")) {
        	htm = htm.replace("http://m.zxxk.com/comment/commentinfo", "http://" + comment_host + "/comment/commentinfo");
        }
        commentroot().append(htm);
    });
}
/*评论详情*/
function commentinfoonload() {
    comment.commentinfo(comment_commentid, function (j) {

        var htm = Mustache.render(templete.commentinfo, j.result);
        commentroot().append(htm);

        comment.getreplylist(comment_commentid, 1, function (j) {

            var htm = Mustache.render(templete.replylist, j);

            if (j.result && j.result.length > 14) {
                var loadmore = '{"CommentID":' + comment_commentid + ',"PageIndex":' + (parseInt(1) + 1) + '}';
                htm += Mustache.render(templete.reply_loadmore, $.parseJSON(loadmore));
            }
            commentroot().append(htm);
        });

    });
}
/*我的评论列表*/
function mycommentlistonload() {
    comment.getcommentlistbyuserid(1, function (j) {
        var htm = Mustache.render(templete.mycommentlist, j);
        if (j.result && j.result.length > 14) {
            var loadmore = '{"PageIndex":' + (parseInt(pageindex) + 1) + '}';
            htm += Mustache.render(templete.mycomment_loadmore, $.parseJSON(loadmore));
        }
        commentroot().append(htm);
    });
}
/*所有评论文件加载完毕*/
function comment_onload(callback) {
   
        onscriptload(http_url + "/Web/Js/comment.js", function () {
            onscriptload(http_url + "/Web/Js/commenttools.js", function () {
                onscriptload(http_url + "/Web/Js/Comment(" + templete_type + ").js", function () {
                    if (typeof callback == "function") {
                        callback();
                    }

                })
     
            })
        })
  
}
/* 下载JS文件*/
function onscriptload(path,callback) {
    $.ajax({
        url: path,
        dataType: "script",
        cache: true,
        success: function () {
            if (typeof callback == "function") {
                callback();
            }
        },
        error: function (XMLHttpRequest, textStatus, errorThrown) {
            alert(path + "加载失败！");
      //      alert(XMLHttpRequest.status);
      //      alert(XMLHttpRequest.readyState);
      //      alert(textStatus);  
        }
    })
}
/*
描述：发表评论
*/
function addcomment() {
    if (parseInt(comment_userid) <= 0) {
        comment_login(); return;
    }
    if (typeof (is_comment) != "undefined") {
        if (is_comment == 0) {//当评论权限不够时；
            alert(comment_msg);
            return;
        }
    }
    var contents = $("[commenttype='" + comment_content + "']").val();
    contents = $.trim(contents).replace(/</ig, '').replace(/>/ig, "").replace(/[\r\n]|[\r]|[\n]/ig, "").replace(/"/ig, "").replace(/[\\]/ig, "");
    if (contents.length > 180) {//评论字数过长时；
        alert("超过180个字了");
        return;
    }
    var oscore = $("[commenttype='" + comment_set_score + "']");
    var set_score = 5;
    if (oscore.length > 0)//当页面中存在打分的Dom元素时；
    {
        set_score = parseInt(oscore.val());
        if (set_score == 0) {
            alert("还没打分哦~~");
            return;
        }
    }
    if (contents == '') {//评论框内容为空时
        alert("请说点什么吧！！");
        return;
    }
    comment.addcomment(comment_infoid, contents, set_score, function (j) {
        var p = '{"CommentUserName":"' + comment_username + '","CommentTime":"' + getcurrenttime() + '","Contents":"' + contents + '","CommentID":' + comment_userid  + ',"PraiseCount":0,"ReplyCount":0}';
        var ping = Mustache.render(templete.newcomment, $.parseJSON(p));
        $("[commenttype='comment_add']").after(ping);

    });
    $("[commenttype='" + comment_content + "']").val('');
    if (oscore.length > 0) {
        $("[commenttype='" + comment_set_score + "']").val(''); //清空打分
    }
}
/*
描述：评论点赞
参数说明：
t:当前节点对象
commentid：评论ID
*/
function comment_praise(t, commentid) {
    if ($(t).attr("tag") == "true") {
        return;
    }
    var c_name = comment_userid + "_" + commentid;
    if (window.localStorage[c_name]) {
        return;
    }
    $(t).attr("tag", "true");
    setCommentStorage(comment_userid, commentid);
    var praise = $("[commenttype='" + comment_praise_count + "_" + commentid + "']");
    //点赞数+1
    var praisecount = parseInt(praise.text()) + 1;
    praise.text(praisecount);
    $(t).find("span").addClass("comment-ck");
    //提交点赞
    comment.commnetpraise(commentid);
}

/*
描述：回复点赞
参数说明：
t:当前节点对象
replyid：回复ID
*/
function reply_praise(t, replyid) {
    if ($(t).attr("tag") == "true") {
        return;
    }
    var c_name = comment_userid + "_" + comment_sourceid + "_" + comment_commentid + "_" + replyid;
    if (window.localStorage[c_name]) {
        return;
    }
    else {
        $(t).attr("tag", "true");
        setReplyStorage(comment_userid, comment_sourceid, comment_commentid, replyid);
        var praise = $("[replytype='" + reply_praise_count + "_" + replyid + "']");
        //点赞数+1
        var praisecount = parseInt(praise.text()) + 1;
        praise.text(praisecount);
        $(t).find("span").addClass("comment-ck");
        //提交点赞
        comment.replypraise(replyid);

        return false;
    }
}

/*
描述：回复按钮 切换事件
参数说明：
commentid：评论ID
*/
function comment_reply(commentid) {

    var node = $("[commenttype='" + comment_reply_item + "_" + commentid + "']");
    if (node.css("display") == "block") {
        node.css("display", "none");
    } else {
        node.css("display", "block");
    }
}
/*
描述：回复的回复
参数说明：
replyid：回复ID
*/
function reply_reply(replyid) {
    var node = $("[replytype='" + reply_reply_item + "_" + replyid + "']");
    if (node.css("display") == "block") {
        node.css("display", "none");
    } else {
        node.css("display", "block");
    }
}
/*
描述：评论回复 回复的回复
参数说明：
t：当前节点对象
commentid：评论ID
parentid：父级ID
*/
function reply_add(t, commentid, parentid) {
    var content = $(t).prev().val();
    content = content.trim().replace(/</ig, "").replace(/>/ig, "").replace(/[\r\n]|[\r]|[\n]/ig, "").replace(/"/ig, "").replace(/[\\]/ig, "");
    if (content.length > 180) {
        alert("超过180个字了");
        return;
    }
    if (content == '') {//评论框内容为空时
        alert("请再说点什么吧！！");
        return;
    }

    comment.addreply(commentid, parentid, content, function (j) {
        //如果是回复的回复
        var json = '{"RRName":"' + $(t).attr("username") + '","ReplyTime":"' + getcurrenttime() + '","Contents":"' + content + '","ReplyUserID":' + comment_userid + ',"ReplyUserName":"' + comment_username + '"}';
        var htm = Mustache.render(templete.replyinfo, $.parseJSON(json));

        if (parentid == 0) { //对评论进行回复
            $("[commenttype='" + comment_item + "_" + commentid + "']").append(htm);
            $("[commenttype='" + comment_reply_content + "_" + commentid + "']").val(""); //清空文本框

            var reply = $("[commenttype='" + comment_reply_count + "_" + commentid + "']");
            var replycount = parseInt(reply.text()) + 1;
            reply.text(replycount); //回复数+1

            $("[commenttype='" + comment_reply_item + "_" + commentid + "']").css("display", "none");
        }
        else {
            $("[replytype='" + reply_item + "_" + parentid + "']").append(htm);
            $("[replytype='" + reply_reply_content + "_" + parentid + "']").val(""); //清空文本框

            var reply = $("[replytype='" + reply_reply_count + "_" + parentid + "']");
            var replycount = parseInt(reply.text()) + 1;
            reply.text(replycount); //回复数+1

            $("[replytype='" + reply_reply_item + "_" + parentid + "']").css("display", "none");
        }

    });

}
/*
描述：评论加载更多
参数说明：
t:当前节点对象
infoid:内容ID
pageindex:页码
*/
function comment_loadmore(t, infoid, pageindex) {
    $(t).remove();
    comment.getcommentlist(infoid, 15, pageindex, null, function (j) {
        var htm = "";
        htm += Mustache.render(templete.commentlist, j);
        if (j.result && j.result.length > 14) {
            var loadmore = '{"InfoID":' + infoid + ',"PageIndex":' + (parseInt(pageindex) + 1) + '}';
            htm += Mustache.render(templete.comment_loadmore, $.parseJSON(loadmore));
        }
        commentroot().append(htm);
    });
}
/*
描述：评论加载更多
参数说明：
t:当前节点对象
pageindex：页码
*/
function mycomment_loadmore(t, pageindex) {
    $(t).remove();
    comment.getcommentlistbyuserid(pageindex, function (j) {
        var htm = Mustache.render(templete.mycommentlist, j);
        if (j.result && j.result.length > 14) {
            var loadmore = '{"PageIndex":' + (parseInt(pageindex) + 1) + '}';
            htm += Mustache.render(templete.mycomment_loadmore, $.parseJSON(loadmore));
        }
        commentroot().append(htm);
    });
}
/*
描述：回复加载更多
参数说明：
t：当前节点对象
commentid：评论ID
pageindex：页码
*/
function reply_loadmore(t, commentid, pageindex) {
    $(t).remove();
    comment.getreplylist(commentid, pageindex, function (j) {

        var htm = Mustache.render(templete.replylist, j);

        if (j.result && j.result.length > 14) {
            var loadmore = '{"CommentID":' + commentid + ',"PageIndex":' + (parseInt(pageindex) + 1) + '}';
            htm += Mustache.render(templete.reply_loadmore, $.parseJSON(loadmore));
        }
        commentroot().append(htm);
    });
}
/*
描述：举报
参数说明：
t：当前节点对象
reportinfoid：举报内容ID（评论ID或回复ID）
reporttypeid：举报类型(0,评论，1是回复)
*/
function report_add(t, reportinfoid, reporttypeid) {
    if ($(t).attr("tag") == "true") {
    }
    else {

        var reason = "评论举报";
        if (reporttypeid == 1) {    //举报类型为举报回复
            var reason = "回复举报";
        }

        comment.addreport(reportinfoid, reporttypeid, reason, function (j) {

            //   alert(j.message);
            $(t).attr("tag", "true");
            $(t).html(templete.report);
            $(t).find("span").addClass("comment-ck");
        });
    }
}

/*评论登陆*/
function comment_login() {
    if (typeof (targetlogin) != "undefined") {
        window.location.href = targetlogin + window.location.pathname;
    }
}