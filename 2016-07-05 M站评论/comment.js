(function defineMustache(global, factory) { if (typeof exports === "object" && exports) { factory(exports) } else if (typeof define === "function" && define.amd) { define(["exports"], factory) } else { global.Mustache = {}; factory(Mustache) } })(this, function mustacheFactory(mustache) { var objectToString = Object.prototype.toString; var isArray = Array.isArray || function isArrayPolyfill(object) { return objectToString.call(object) === "[object Array]" }; function isFunction(object) { return typeof object === "function" } function escapeRegExp(string) { return string.replace(/[\-\[\]{}()*+?.,\\\^$|#\s]/g, "\\$&") } function hasProperty(obj, propName) { return obj != null && typeof obj === "object" && propName in obj } var regExpTest = RegExp.prototype.test; function testRegExp(re, string) { return regExpTest.call(re, string) } var nonSpaceRe = /\S/; function isWhitespace(string) { return !testRegExp(nonSpaceRe, string) } var entityMap = { "&": "&amp;", "<": "&lt;", ">": "&gt;", '"': "&quot;", "'": "&#39;", "/": "&#x2F;" }; function escapeHtml(string) { return String(string).replace(/[&<>"'\/]/g, function fromEntityMap(s) { return entityMap[s] }) } var whiteRe = /\s*/; var spaceRe = /\s+/; var equalsRe = /\s*=/; var curlyRe = /\s*\}/; var tagRe = /#|\^|\/|>|\{|&|=|!/; function parseTemplate(template, tags) { if (!template) return []; var sections = []; var tokens = []; var spaces = []; var hasTag = false; var nonSpace = false; function stripSpace() { if (hasTag && !nonSpace) { while (spaces.length) delete tokens[spaces.pop()] } else { spaces = [] } hasTag = false; nonSpace = false } var openingTagRe, closingTagRe, closingCurlyRe; function compileTags(tagsToCompile) { if (typeof tagsToCompile === "string") tagsToCompile = tagsToCompile.split(spaceRe, 2); if (!isArray(tagsToCompile) || tagsToCompile.length !== 2) throw new Error("Invalid tags: " + tagsToCompile); openingTagRe = new RegExp(escapeRegExp(tagsToCompile[0]) + "\\s*"); closingTagRe = new RegExp("\\s*" + escapeRegExp(tagsToCompile[1])); closingCurlyRe = new RegExp("\\s*" + escapeRegExp("}" + tagsToCompile[1])) } compileTags(tags || mustache.tags); var scanner = new Scanner(template); var start, type, value, chr, token, openSection; while (!scanner.eos()) { start = scanner.pos; value = scanner.scanUntil(openingTagRe); if (value) { for (var i = 0, valueLength = value.length; i < valueLength; ++i) { chr = value.charAt(i); if (isWhitespace(chr)) { spaces.push(tokens.length) } else { nonSpace = true } tokens.push(["text", chr, start, start + 1]); start += 1; if (chr === "\n") stripSpace() } } if (!scanner.scan(openingTagRe)) break; hasTag = true; type = scanner.scan(tagRe) || "name"; scanner.scan(whiteRe); if (type === "=") { value = scanner.scanUntil(equalsRe); scanner.scan(equalsRe); scanner.scanUntil(closingTagRe) } else if (type === "{") { value = scanner.scanUntil(closingCurlyRe); scanner.scan(curlyRe); scanner.scanUntil(closingTagRe); type = "&" } else { value = scanner.scanUntil(closingTagRe) } if (!scanner.scan(closingTagRe)) throw new Error("Unclosed tag at " + scanner.pos); token = [type, value, start, scanner.pos]; tokens.push(token); if (type === "#" || type === "^") { sections.push(token) } else if (type === "/") { openSection = sections.pop(); if (!openSection) throw new Error('Unopened section "' + value + '" at ' + start); if (openSection[1] !== value) throw new Error('Unclosed section "' + openSection[1] + '" at ' + start) } else if (type === "name" || type === "{" || type === "&") { nonSpace = true } else if (type === "=") { compileTags(value) } } openSection = sections.pop(); if (openSection) throw new Error('Unclosed section "' + openSection[1] + '" at ' + scanner.pos); return nestTokens(squashTokens(tokens)) } function squashTokens(tokens) { var squashedTokens = []; var token, lastToken; for (var i = 0, numTokens = tokens.length; i < numTokens; ++i) { token = tokens[i]; if (token) { if (token[0] === "text" && lastToken && lastToken[0] === "text") { lastToken[1] += token[1]; lastToken[3] = token[3] } else { squashedTokens.push(token); lastToken = token } } } return squashedTokens } function nestTokens(tokens) { var nestedTokens = []; var collector = nestedTokens; var sections = []; var token, section; for (var i = 0, numTokens = tokens.length; i < numTokens; ++i) { token = tokens[i]; switch (token[0]) { case "#": case "^": collector.push(token); sections.push(token); collector = token[4] = []; break; case "/": section = sections.pop(); section[5] = token[2]; collector = sections.length > 0 ? sections[sections.length - 1][4] : nestedTokens; break; default: collector.push(token) } } return nestedTokens } function Scanner(string) { this.string = string; this.tail = string; this.pos = 0 } Scanner.prototype.eos = function eos() { return this.tail === "" }; Scanner.prototype.scan = function scan(re) { var match = this.tail.match(re); if (!match || match.index !== 0) return ""; var string = match[0]; this.tail = this.tail.substring(string.length); this.pos += string.length; return string }; Scanner.prototype.scanUntil = function scanUntil(re) { var index = this.tail.search(re), match; switch (index) { case -1: match = this.tail; this.tail = ""; break; case 0: match = ""; break; default: match = this.tail.substring(0, index); this.tail = this.tail.substring(index) } this.pos += match.length; return match }; function Context(view, parentContext) { this.view = view; this.cache = { ".": this.view }; this.parent = parentContext } Context.prototype.push = function push(view) { return new Context(view, this) }; Context.prototype.lookup = function lookup(name) { var cache = this.cache; var value; if (cache.hasOwnProperty(name)) { value = cache[name] } else { var context = this, names, index, lookupHit = false; while (context) { if (name.indexOf(".") > 0) { value = context.view; names = name.split("."); index = 0; while (value != null && index < names.length) { if (index === names.length - 1) lookupHit = hasProperty(value, names[index]); value = value[names[index++]] } } else { value = context.view[name]; lookupHit = hasProperty(context.view, name) } if (lookupHit) break; context = context.parent } cache[name] = value } if (isFunction(value)) value = value.call(this.view); return value }; function Writer() { this.cache = {} } Writer.prototype.clearCache = function clearCache() { this.cache = {} }; Writer.prototype.parse = function parse(template, tags) { var cache = this.cache; var tokens = cache[template]; if (tokens == null) tokens = cache[template] = parseTemplate(template, tags); return tokens }; Writer.prototype.render = function render(template, view, partials) { var tokens = this.parse(template); var context = view instanceof Context ? view : new Context(view); return this.renderTokens(tokens, context, partials, template) }; Writer.prototype.renderTokens = function renderTokens(tokens, context, partials, originalTemplate) { var buffer = ""; var token, symbol, value; for (var i = 0, numTokens = tokens.length; i < numTokens; ++i) { value = undefined; token = tokens[i]; symbol = token[0]; if (symbol === "#") value = this.renderSection(token, context, partials, originalTemplate); else if (symbol === "^") value = this.renderInverted(token, context, partials, originalTemplate); else if (symbol === ">") value = this.renderPartial(token, context, partials, originalTemplate); else if (symbol === "&") value = this.unescapedValue(token, context); else if (symbol === "name") value = this.escapedValue(token, context); else if (symbol === "text") value = this.rawValue(token); if (value !== undefined) buffer += value } return buffer }; Writer.prototype.renderSection = function renderSection(token, context, partials, originalTemplate) { var self = this; var buffer = ""; var value = context.lookup(token[1]); function subRender(template) { return self.render(template, context, partials) } if (!value) return; if (isArray(value)) { for (var j = 0, valueLength = value.length; j < valueLength; ++j) { buffer += this.renderTokens(token[4], context.push(value[j]), partials, originalTemplate) } } else if (typeof value === "object" || typeof value === "string" || typeof value === "number") { buffer += this.renderTokens(token[4], context.push(value), partials, originalTemplate) } else if (isFunction(value)) { if (typeof originalTemplate !== "string") throw new Error("Cannot use higher-order sections without the original template"); value = value.call(context.view, originalTemplate.slice(token[3], token[5]), subRender); if (value != null) buffer += value } else { buffer += this.renderTokens(token[4], context, partials, originalTemplate) } return buffer }; Writer.prototype.renderInverted = function renderInverted(token, context, partials, originalTemplate) { var value = context.lookup(token[1]); if (!value || isArray(value) && value.length === 0) return this.renderTokens(token[4], context, partials, originalTemplate) }; Writer.prototype.renderPartial = function renderPartial(token, context, partials) { if (!partials) return; var value = isFunction(partials) ? partials(token[1]) : partials[token[1]]; if (value != null) return this.renderTokens(this.parse(value), context, partials, value) }; Writer.prototype.unescapedValue = function unescapedValue(token, context) { var value = context.lookup(token[1]); if (value != null) return value }; Writer.prototype.escapedValue = function escapedValue(token, context) { var value = context.lookup(token[1]); if (value != null) return mustache.escape(value) }; Writer.prototype.rawValue = function rawValue(token) { return token[1] }; mustache.name = "mustache.js"; mustache.version = "2.1.2"; mustache.tags = ["{{", "}}"]; var defaultWriter = new Writer; mustache.clearCache = function clearCache() { return defaultWriter.clearCache() }; mustache.parse = function parse(template, tags) { return defaultWriter.parse(template, tags) }; mustache.render = function render(template, view, partials) { return defaultWriter.render(template, view, partials) }; mustache.to_html = function to_html(template, view, partials, send) { var result = mustache.render(template, view, partials); if (isFunction(send)) { send(result) } else { return result } }; mustache.escape = escapeHtml; mustache.Scanner = Scanner; mustache.Context = Context; mustache.Writer = Writer });

//类对象
var comment = {
    /*
    描述：评论列表
    参数说明：
    infoid：内容ID
    pagesize：页面大小
    pageindex：页码
    orderstr：排序字段
    */
    getcommentlist: function (infoid, pagesize, pageindex, orderstr, callback) {
        var param = '{"InfoId":' + infoid + ',"SourceID":' + comment_sourceid + ',"ProductID":' + comment_platform + ',"PageIndex":' + pageindex + ',"PageSize":' + pagesize + ',"OrderStr":' + orderstr + '}';
        $.ajax({
            type: "POST",
            url: http_url + "Comment/CommentApi.asmx/GetCommentList",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }
                } else {
                    alert(j.message);
                }
            },
            error: function (XMLHttpRequest, textStatus, errorThrown) {

            }
        });
    },
    /*
    描述：我的评论
    参数说明：
    pageindex：页码
    */
    getcommentlistbyuserid: function (pageindex, callback) {
        if (parseInt(comment_userid) <= 0 || comment_username == "") {
            alert("该操作需要登录呦~~~"); return;
        }
        var param = '{"CommentUserID":' + comment_userid + ',"SourceID":' + comment_sourceid + ',"PageIndex":' + pageindex + ',"PageSize":15}';
        $.ajax({
            type: "POST",
            url: http_url + "Comment/CommentApi.asmx/GetCommentListByUserId",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {

                var j = data[0];

                if (j.statuscode == "200") {

                    if (typeof callback == "function") {
                        callback(j);
                    }

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：添加评论
    参数说明：
    infoid：内容ID
    contents：评论内容
    score：打分
    */
    addcomment: function (infoid, contents, score, callback) {
        if (parseInt(comment_userid) <= 0) {
            alert("该操作需要登录呦~~~"); return;
        }

        var param = '{"InfoId":' + infoid + ',"contents":"' + contents + '","Score":' + score + ',"CommentUserID":' + comment_userid + ',"CommentUserName":"' + comment_username + '","SourceID":' + comment_sourceid + ',"ProductID":' + comment_platform + '}';
        $.ajax({
            type: "POST",
            url: http_url + "Comment/CommentApi.asmx/AddComment",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {

                    if (typeof callback == "function") {
                        callback(j);
                    }

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：评论点赞
    参数说明：
    commentid：评论ID
    */
    commnetpraise: function (commentid, callback) {
        $.ajax({
            type: "POST",
            url: http_url + "Comment/CommentApi.asmx/CommentPraise",
            data: { "commentId": commentid },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {

                    if (typeof callback == "function") {
                        callback(j);
                    }

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：根据评论id获得回复列表
    参数说明：
    commentid：评论ID
    pageindex：页码
    */
    getreplylist: function (commentid, pageindex, callback) {
        var param = '{"CommentID":' + commentid + ',"PageIndex":' + pageindex + ',"PageSize":15}';
        $.ajax({
            type: "POST",
            url: http_url + "Reply/ReplyApi.asmx/GetReplyByComment",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];

                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：添加回复
    参数说明：
    commentid：评论ID
    parentid：父级ID
    contents：回复内容
    t：当前节点对象
    */
    addreply: function (commentid, parentid, contents, callback) {
        if (parseInt(comment_userid) <= 0 || comment_username == "") {
            alert("该操作需要登录呦~~~"); return;
        }
        var param = '{"CommentID":' + commentid + ',"ParentID":' + parentid + ',"contents":"' + contents + '","ReplyUserID":' + comment_userid + ',"ReplyUserName":"' + comment_username + '"}';
        $.ajax({
            type: "POST",
            url: http_url + "Reply/ReplyApi.asmx/AddReply",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }
                  
                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：回复点赞
    参数说明：
    replyid：回复ID
    */
    replypraise: function (replyid, callback) {
        $.ajax({
            type: "POST",
            url: http_url + "Reply/ReplyApi.asmx/ReplyPraise",
            data: { "ReplyID": replyid },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：举报
    参数说明：
    reportinfoid：举报内容ID（评论ID或者回复ID）
    reporttypeid：举报类型（0评论，1回复）
    contents：举报理由
    */
    addreport: function (reportinfoid, reporttypeid, contents, callback) {
        if (parseInt(comment_userid) <= 0 || comment_username == "") {
            alert("该操作需要登录呦~~~"); return;
        }

        var param = '{"ReportInfoID":' + reportinfoid + ',"ReportTypeID":' + reporttypeid + ',"Reason":"' + contents + '","ReportUserID":' + comment_userid + ',"ReportUserName":"' + comment_username + '"}';
        $.ajax({
            type: "POST",
            url: http_url + "Report/ReportApi.asmx/AddReport",
            data: { "jsonStr": param },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }
                 

                } else {
                    alert(j.message);
                }
            }
        });
    },
    /*
    描述：评论详细
    参数说明：
    commentid：评论ID
    */
    commentinfo: function (commentid, callback) {
        $.ajax({
            type: "POST",
            url: http_url + "Comment/CommentApi.asmx/GetComment",
            data: { "commentId": commentid },
            dataType: "jsonp",
            jsonp: "callback", //服务端用于接收callback调用的function名的参数
            success: function (data) {
                var j = data[0];
                if (j.statuscode == "200") {
                    if (typeof callback == "function") {
                        callback(j);
                    }
                 

                } else {
                    alert(j.message);
                }
            }
        });
    }
}
