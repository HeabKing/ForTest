//author:WangYY
//Date:2013.1.25
//Update:2013.4.12
//---------------------------
//评价统计查询  (旧)
function IndexCount() {
    $("#AppraiseListAll").html("正在加载...");
    $.ajax({
        type: 'get',
        url: 'http://comment.zxxk.com/AppraiseQueryList.ashx',
        dataType: 'jsonp',
        data: 'action=count&InfoGuID=' + oInfoGuID + '&t=' + Math.random(),
        success: function (oJson) {
            var oMsg = oJson["msg"];
            if (oMsg == "ok") {
                var oFiveCount = parseInt(oJson["five"]);     //五星
                var oFourCount = parseInt(oJson["four"]);     //四星
                var oThreeCount = parseInt(oJson["three"]);   //三星
                var oTwoCount = parseInt(oJson["two"]);       //两星
                var oOneCount = parseInt(oJson["one"]);       //一星

                var oAllCount = oFiveCount + oFourCount + oThreeCount + oTwoCount + oOneCount;  //所有评价数
                var oAllScore = oFiveCount * 5 + oFourCount * 4 + oThreeCount * 3 + oTwoCount * 2 + oOneCount * 1; //所有分数
                var oGoodScore = oFiveCount * 5 + oFourCount * 4;                                                  //好评分数
                var oAverageScore = oThreeCount * 3 + oTwoCount * 2;                                               //中评分数
                var oPoorScore = oOneCount;                                                                        //差评分数
                var oGoodCount = oFiveCount + oFourCount; //好评数
                var oAverageCount = oThreeCount + oTwoCount; //中评数
                $("#TopAllCount").html("<font color='red'>" + oAllCount + "</font>");   //总的评价数
                $("#AllCount").html(oAllCount);
                $("#GoodCount").html(oGoodCount < 1 ? "0" : oGoodCount);                //好评数
                $("#AverageCount").html(oAverageCount < 1 ? "0" : oAverageCount);       //中评数
                $("#PoorCount").html(oOneCount < 1 ? "0" : oOneCount);                  //差评数
                if (oAllCount > 4) { 
                    var oGoodRatio = (oGoodScore * 100 / oAllScore); //好评所占比例
                    var oAverageRatio = oAverageScore * 100 / oAllScore; //中评所占比例
                    var oPoorRatio = 100 - oGoodRatio - oAverageRatio; //差评所占比例
                    
                    $("#GoodRatioImg").css({ width: oGoodRatio + "%" });          //好评进度条显示O
                    $("#AverageRatioImg").css({ width: oAverageRatio + "%" });    //中评进度条显示
                    $("#PoorRatioImg").css({ width: oPoorRatio + "%" });                 //差评进度条显示

                    $("#AllRatio").html(parseInt(oGoodRatio) + "%");                          //总体评价百分比
                    $("#GoodRatio").html("好评(" + parseInt(oGoodRatio) + "%)");              //好评百分比
                    $("#AverageRatio").html("中评(" + parseInt(oAverageRatio) + "%)");        //中评百分比
                    $("#PoorRatio").html("差评(" + parseInt(oPoorRatio) + "%)");              //差评百分比
                }            
                TabAppraiseBox('All', 1);
                GetUserList();
            } else {
                //$("#ShowCountInfo").html("").css({ height: "15px" });
                $(".score_info li").attr("onclick", " ");
                TabAppraiseBox('All', 0);
                $("#ShowUserList").html("<li>暂无精品评价</li>");
                $("#AppraiseListAll").html("<div style=\"padding:10px;color:#555;\">该资料暂时还没网友发表评价<a href=\"http://comment.zxxk.com/AddAppraise.aspx?SoftID=" + oInfoID + "\" style=\"color:#ff0000;\" target=\"_blank\">[发表评价]</a></div>");

            }
        }, error: function () {
            $("#AppraiseListAll").html("加载失败！");
            TabAppraiseBox('All', 0);
        }
    });
}
//生成列表页
function pageList(currentPage, t) {
    var root = $("#HidAppraiseBox");
    var $content = $("#AppraiseList" + t);
    $content.html("<div style=\"text-align:center;padding:50px 0;\"><img src=\"/Images/loading.gif\" alt=\"loading\" /></div>");
    var template = root.val();
    var traverse = function (data, tmplate) {
        return tmplate.replace(/{(\w+)}/g, function ($0, $1) {
            var column = $1.toLowerCase();
            if (column == 'isessence') {
                if (data[column] == "1") {
                    return "<em class=\"pl_good\"></em>";
                } else {
                    return "";
                }
            }
            if (column == "userface") {
                if (isNumber(data["userid"])) {
                    return "http://user.zxxk.com/UserFace/" + data["userid"].substring(0, 2) + "/" + data["userid"] + ".jpg";
                    //var oImg="<img src=\"/img/loadphoto.gif\" uid=\"{userid}\" alt="" title="" />"
                }
            }
            else {
                return data[column];
            }
        });
    };
    $.ajax({
        type: 'get',
        url: 'http://comment.zxxk.com/AppraiseQueryList.ashx',
        dataType: 'jsonp',
        data: 'action=list&InfoGuID=' + oInfoGuID + '&Type=' + t + '&Page=' + currentPage + '&t=' + Math.random(),
        success: function (oJson) {
            if (oJson["infolist"].length > 0) {
                var oPageCount = oJson["pagecount"];
                var content = "";
                var top = oJson["infolist"].length;
                for (var i = 0; i < top; i++) {
                    content += traverse(oJson["infolist"][i], template);
                }
                $content.html(template.replace(template, content));
                EachReplyList(); //遍历回复列表
                createPage(oPageCount, 6, currentPage, "pageList", t); //生成翻页Html
                //$content.loadUserFace(); //替换用户头像
            } else {
                $content.html('暂无评价！');
            }
        }, error: function () { $content.html('读取失败！'); }
    });
}
//获取回复列表
function EachReplyList() {
    $(".maincom .reply").each(function () {
        var appID = parseInt($(this).attr("appid"));
        var oCount = 0;
        var PostData = "action=reply&AppraiseID=" + appID + '&t=' + Math.random();
        $.ajax({
            type: "get",
            url: "http://comment.zxxk.com/AppraiseQueryList.ashx",
            data: PostData,
            dataType: 'jsonp',
            success: function (oJson) {
                if (oJson["infolist"].length > 0) {
                    var num = oJson["infolist"].length;
                    oCount = num;
                    var oContent = "";
                    for (var i = 0; i < num; i++) {
                        var oDr = oJson["infolist"][i];
                        oContent += "<p class=\"r\"><span><b>" + oCount + "F</b><a style=\"color:#066;\">" + oDr["username"] + "</a><a style=\"color:#666\">回复说：</a></span><a style=\"float:right; color:#ccc\">" + oDr["addtime"] + "</a></p>";
                        oContent += "<p class=\"con\">" + oDr["content"] + "</p>";
                        oCount--;
                    }
                    $(".maincom p[appid='" + appID + "']").html(oContent).show();
                } else {
                    $(".maincom p[appid='" + appID + "']").hide();
                }
            }, error: function () { $(".maincom p[appid='" + appID + "']").hide(); }
        })
    })
}
//评价列表用户弹出框
function ShowUserInfoBox(th,oID,oUserName) {
    $(th).find(".info").show();
}
//评价列表隐藏用户弹出框
function HideUserInfoBox() {    
    $(".info").hide();
}
//获取加精评价用户
function GetUserList() {
    var PostData = "action=user&InfoGuid=" + oInfoGuID + '&t=' + Math.random();
    $.ajax({
        type: "get",
        url: "http://comment.zxxk.com/AppraiseQueryList.ashx",
        data: PostData,
        dataType: 'jsonp',
        success: function (oJson) {
            if (oJson["infolist"].length > 0) {
                var num = oJson["infolist"].length;
                var iCount = 0;
                var oContent = "";
                for (var i = 0; i < num; i++) {
                    var oDr = oJson["infolist"][i];
                    iCount++;
                    oContent += "<li><span id=\"no" + iCount + "\"></span>" + oDr["username"] + "</li>";
                }
                $("#ShowUserList").html(oContent);
            } else {
                $("#ShowUserList").html("<li>暂无精品评价</li>");
            }
        },
        error: function () { $("#ShowUserList").html("<li>加载失败！</li>") }
    });
}
//是否为数字
function isNumber(name) {
    if (name.length == 0)
    { return false; }
    for (i = 0; i < name.length; i++) {
        if (name.charAt(i) < "0" || name.charAt(i) > "9")
        { return false; }
    }
    return true;
}
//切换评价选项卡显示
function TabAppraiseBox(t, n) {
    $("li[id^='Tab']").removeClass("current");
    $("div[id^='TabBox']").hide();
    $("#Tab" + t).addClass("current");
    $("#TabBox" + t).show();
    if (n == 1) {
        var oHtml = $("#AppraiseList" + t).html();
        if ($.trim(oHtml).length < 20) {
            pageList(1, t);
        }
    }
}
var sCaCheName = "ZxxkLocalCache";
//支持反对投票
function SetSupport(th, t, id) {
    var sName = "ZxxkAppraiseSupport";
    var IsHas = false;
    var oAppraiseInfo = Storage.Get(sCaCheName, sName);
    var oNewAppraiseInfo = "";
    if (oAppraiseInfo != "") {
        var oIndex = 100;
        var ArrHistory = oAppraiseInfo.split('|');
        var oLength = ArrHistory.length;
        if (oLength > 100) {
            oIndex = oLength - 100;
            for (var i = 0; i < ArrHistory.length; i++) {
                if (id.toString() == ArrHistory[i]) {
                    IsHas = true;
                }
                if (i > oIndex) {
                    oNewAppraiseInfo += ArrHistory[i].toString() + "|";
                }
            }
        } else {
            for (var i = 0; i < ArrHistory.length; i++) {
                if (id.toString() == ArrHistory[i]) {
                    IsHas = true;
                }
                oNewAppraiseInfo += ArrHistory[i].toString() + "|";
            }
        }   
   }
   if (IsHas == true) {
       AlertMsg('notice', '您已投过票了，请不要重复投票', 2000);
       return;
   }
    var oCount = $(th).find('font').html();
    var PostData = "action=support&Type=" + t + "&AppraiseID=" + id + "&t=" + Math.random();
    $(th).find('font').html(parseInt(oCount) + 1);
    $.ajax({
        type: "get",
        url: "http://comment.zxxk.com/AddAppraise.aspx",
        data: PostData,
        dataType: 'jsonp',
        success: function (data) {
            if (data.msg == 'ok') {
                if (oAppraiseInfo != "") {
                    oNewAppraiseInfo += id.toString();
                } else {
                    oNewAppraiseInfo = id.toString();
                }
                Storage.Set(sCaCheName, sName, oNewAppraiseInfo);
                $(th).attr("onclick", "").css({ color: '#999', cursor: 'text' });
            }
            else {
                $(th).find('font').html(oCount);
            }
        }, error: function () {
            $(th).find('font').html(oCount);
        }, timeout: 8000
    });

}
//-begin 2014-5-21 (评论星级)
function SetCommentStar() {
    var PostData = "action=count&InfoID=" + oInfoID + "&ChannelID=" + oChannelID + "&t=" + Math.random();
    $.ajax({
        type: "post",
        url: "http://comment.zxxk.com/AddAppraise.aspx",
        data: PostData,
        dataType: 'jsonp',
        success: function (data) {
            var datas = eval(data);
            var arrnum = datas.msg.split('|');
            SetStarAndCommentNumber(arrnum);
        }, error: function () {
        }
    });
}
function SetStarAndCommentNumber(arrnum) {
    var number = arrnum[0];
    var star = arrnum[1];
    //如果无人评价
    if (star.toString() == '0' && number.toString() == '0') {
        star = 60;
    }
    //$("#star").attr("class", "star s" + star);
    $("#stares").attr("style", "width:" + star + "%;");
    $("#TopAllCount").html(number);
}
//-end 2014-5-21
//资料顶踩
function SetCount(th, t, id) {
//    alert("捕获到了鲜花和鸡蛋");
    var sName = "ZxxkSoftSupport";
    var IsHas = false;
    var oSoftIDInfo = Storage.Get(sCaCheName, sName);//所有已投票的资料ID
    var oNewSoftIDInfo = "";
    if (oSoftIDInfo != "") {
        var oIndex = 100;
        var ArrSoftID = oSoftIDInfo.split('|');
        var oLength = ArrSoftID.length;       
        if (oLength > 100) {
            oIndex = oLength - 100;
            for (var i = 0; i < ArrSoftID.length; i++) {
                if (id.toString() == ArrSoftID[i]) {
                    IsHas = true;
                }
                if (i > oLength) {
                    oNewSoftIDInfo += ArrSoftID[i].toString() + "|";
                }
            }
        } else {
            for (var i = 0; i < ArrSoftID.length; i++) {
                if (id.toString() == ArrSoftID[i]) {
                    IsHas = true;
                }
                oNewSoftIDInfo += ArrSoftID[i].toString() + "|";
            }
        } 
    }
    if (IsHas == true) {
        AlertMsg('notice', '您已投过票了，请不要重复投票', 2000);
        return;
    }
    var ObjParent = $(th).parent().parent();  //点击a标签最外层标签
    var oDingCount = ObjParent.find('cite:first').html(); //获取顶的数量
    var oCaiCount = ObjParent.find('cite:last').html();   //获取踩的数量
    if (isNumber(oDingCount) == false || isNumber(oCaiCount) == false) {
        return;
    }
    oDingCount = parseInt(oDingCount);
    oCaiCount = parseInt(oCaiCount);
    if (t == "no") {
        oCaiCount++;
    } else {
        oDingCount++
    }
    var oCount = $(th).find('cite').html();
    var PostData = "action=ding&Type=" + t + "&SoftID=" + id + "&t=" + Math.random();
    $.ajax({
        type: "get",
        url: "http://comment.zxxk.com/AddAppraise.aspx",
        data: PostData,
        dataType: 'jsonp',
        success: function (data) {
            if (data.msg == 'ok') {
                if (oSoftIDInfo == "") {
                    oNewSoftIDInfo = id.toString();
                } else {
                    oNewSoftIDInfo += id.toString();
                }
                Storage.Set(sCaCheName, sName, oNewSoftIDInfo);
                var oAll = oDingCount + oCaiCount;  //总的票数
                var oDImg = parseInt(parseInt(oDingCount) * 100 / oAll); //顶所占百分比
                var oCImg = 100 - oDImg;
                ObjParent.find("em:first").css({ width: oDImg + "%" });
                ObjParent.find("em:last").css({ width: oCImg + "%" });
                ObjParent.find("tt:first").html(oDImg + "%");
                ObjParent.find("tt:last").html(oCImg + "%");

                $(th).find('cite').html((parseInt(oCount) + 1));
                ObjParent.find('span').unbind("click");
            }
            else
            { AlertMsg('notice', data.msg, 3000); }
        }, error: function (XMLHttpRequest, textStatus, errorThrown) {
            AlertMsg('error', '投票失败', 3000);
            //alert("XMLHttpRequest.status=" + XMLHttpRequest.status + ",XMLHttpRequest.readyState=" + XMLHttpRequest.readyState + ",textStatus=" + textStatus + ",errorthrown=" + errorThrown + ",postData=" + PostData);
        }, timeout: 8000
    });    
}
//显示回复框
function OpenReplayBox(th, id) {
    if ($(th).parent().parent().find(".replybox").length > 0) {
        $(th).html("回复");
        $(".replybox").remove(); return;
    }
    if ($(".replybox").length > 0) {
        if ($(".replybox").find('textarea').val() != '') {
            if (confirm("确定要放弃正在编辑的内容吗？") == false) {
                $(".replybox").find('textarea').focus();
                return;
            }
        }
        $(".replybox").prev().find("a:last").html("回复");
        $(".replybox").remove();
    }
    $(th).html("收起");
    var oBoxHTML = "<div class=\"replybox\">";
    oBoxHTML += "<textarea id=\"ReplyContent\" cols=\"20\" rows=\"2\"></textarea>";
    oBoxHTML += "<div style=\"text-align:right;\">";
    oBoxHTML += "<input type=\"button\" value=\"回复\" onclick=\"AddReply(this," + id + ")\" class=\"btn\" />";
    oBoxHTML += "</div></div>";

    $(th).parent().after(oBoxHTML);
}
//发表回复
function AddReply(th, appid) {
    if (isLogined() == false) {
        AlertMsg('notice', "未登录，请登录后发表回复！", 3000);
        return;
    }
    var oContent = $("#ReplyContent").val();
    if (oContent == "") {
        AlertMsg('notice', "请输入回复内容", 3000);
        $("#ReplyContent").focus(); return;
    }
    var PostData = "action=reply&Content=" + escape(oContent) + "&AppraiseID=" + appid + "&t=" + Math.random();
    $(th).val("提交中");
    $.ajax({
        type: "get",
        url: "http://comment.zxxk.com/AddAppraise.aspx",
        data: PostData,
        dataType: 'jsonp',
        success: function (data) {
            if (data.msg == 'ok') {
                $(".replybox").prev().find("a:last").html("回复");
                $(".replybox").remove();
                var oHtml = "";
                var oOldHtml = "";
                var oLouNum = 1;
                if ($("#ReplyList" + appid).find("p").length > 0) {
                    oOldHtml = $("#ReplyList" + appid).html();
                    var oAllCount = $("#ReplyList" + appid).find("p").length;
                    oLouNum = parseInt(oAllCount / 2) + 1;
                }
                oHtml += "<p class=\"r\"><span><b>" + oLouNum + "F</b><a style=\"color:#066;\">" + data.name + "</a><a style=\"color:#666\">回复说：</a></span><a style=\"float:right; color:#ccc\">刚刚</a><font color='#ff0000'>正在审核...</font></p>";
                oHtml += "<p class=\"con\">" + oContent + "</p>";

                $("p[appid='" + appid + "']").html(oHtml + oOldHtml).show();
            }
            else {
                AlertMsg('notice', data.msg, 3000);
                $(".replybox").prev().find("a:last").html("回复");
                $(".replybox").remove();
            }
        }, error: function () {
            AlertMsg('error', '回复失败', 3000);
            $(".replybox").prev().find("a:last").html("回复");
            $(".replybox").remove();
        }, timeout: 8000
    });
}

//生成翻页HTML
function createPage(totalPageCount, pageSize, currentPageIndex, jsName, type) {
    var proPage = currentPageIndex - 1;
    var nextPage = proPage + 2;
    if (proPage < 1) {
        proPage = 1;
    }
    if (nextPage > totalPageCount) {
        nextPage = totalPageCount;
    }
    if (totalPageCount < 1 || pageSize < 1) {
        return;
    }
    $("#ShowPage").html("");
    var start = currentPageIndex - (Math.ceil(pageSize / 2) - 1);
    if (pageSize < totalPageCount) {
        if (start < 1) {
            start = 1;
        }
        else if (start + pageSize > totalPageCount) {
            start = totalPageCount - pageSize + 1;
        }
    }
    else {
        start = 1;
    }
    var end = start + pageSize - 1;
    if (end > totalPageCount) {
        end = totalPageCount;
    }
    var newNumberStr = "";

    if (currentPageIndex <= 1) {
        newNumberStr += "<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">首页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">上一页</a>";
    } else {
        newNumberStr += "<a href=\"javascript:" + jsName + "('1','" + type + "')\">首页</a><a href=\"javascript:" + jsName + "('" + proPage + "','" + type + "')\">上一页</a>";
    }
    for (var i = start; i <= end; i++) {

        if (i == currentPageIndex) {
            newNumberStr += "<a href=\"javascript:void(0);\" class=\"current\">" + i + "</a>";
        }
        else {
            newNumberStr += "<a href=\"javascript:" + jsName + "('" + i + "','" + type + "')\">" + i + "</a>";
        }
    }
    if (currentPageIndex == totalPageCount) {
        newNumberStr += "<a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">下一页</a><a href=\"javascript:void(0);\" disabled=\"disabled\" class=\"disabled\">末页</a>";
    }
    else {
        newNumberStr += "<a href=\"javascript:void(0);\" onclick=\"" + jsName + "('" + nextPage + "','" + type + "')\">下一页</a><a href=\"javascript:void(0);\" onclick=\"" + jsName + "('" + totalPageCount + "','" + type + "')\">末页</a>";
    }
    newNumberStr += "<a href=\"javascript:void(0);\" class=\"ln\">共 " + totalPageCount + " 页</a>";
    if (totalPageCount > 1) {
        $("#ShowPage" + type).html(newNumberStr).show();
    }
}


