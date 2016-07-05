/*评论工具类*/

/**
 * 描述：获取跟节点
 */
function commentroot() {
    return $("#commentroot");
}

/**
 * 描述：获取当前时间  
 * 格式：1970-01-01 01:01:59
 */
function getcurrenttime() {
    var d = new Date()
    var vYear = d.getFullYear()
    var vMon = d.getMonth() + 1
    var vDay = d.getDate()
    var h = d.getHours();
    var m = d.getMinutes();
    var se = d.getSeconds();
    s = vYear + "-" + (vMon < 10 ? "0" + vMon : vMon) + "-" + (vDay < 10 ? "0" + vDay : vDay) + " " + (h < 10 ? "0" + h : h) + ":" + (m < 10 ? "0" + m : m) + ":" + (se < 10 ? "0" + se : se);
    return s;
}

/**
 * 描述：为评论点赞设置本地存储
 * 参数说明：
 *      userid：用户id
 *      commentid：评论id
 */
function setCommentStorage(userid, commentid) {
    var name = userid + "_" + commentid;
    window.localStorage.setItem(name, name);
}

/**
 * 描述：为回复点赞设置本地存储
 * 参数说明：
 *      userid：用户id
 *      sourceid：平台1代表WEB平台
 *      commentid：评论id
 *      replyid：回复id
 */
function setReplyStorage(userid, sourceid, commentid, replyid) {
    var name = userid + "_" + sourceid + "_" + commentid + "_" + replyid;
    window.localStorage.setItem(name, name);
}
