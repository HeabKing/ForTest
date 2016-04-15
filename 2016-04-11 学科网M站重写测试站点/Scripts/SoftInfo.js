//资料顶踩
function SetCount(t, id) {
	var sCaCheName = "ZxxkLocalCache";
	var sName = "ZxxkSoftSupport";
	var isHas = false;
	var oSoftIdInfo = Storage.Get(sCaCheName, sName); // 所有已投票的资料ID
	var oNewSoftIdInfo = "";
	if (oSoftIdInfo !== "") {
		var oIndex = 100;
		var arrSoftId = oSoftIdInfo.split('|');
		var oLength = arrSoftId.length;
		if (oLength > 100) {
			oIndex = oLength - 100;
			for (var i = 0; i < arrSoftId.length; i++) {
				if (id.toString() === arrSoftId[i]) {
					isHas = true;
				}
				if (i > oLength) {
					oNewSoftIdInfo += arrSoftId[i].toString() + "|";
				}
			}
		} else {
			for (var i = 0; i < arrSoftId.length; i++) {
				if (id.toString() === arrSoftId[i]) {
					isHas = true;
				}
				oNewSoftIdInfo += arrSoftId[i].toString() + "|";
			}
		}
	}
	if (isHas === true) {
		alert('您已投过票了，请不要重复投票');
		return;
	}
	var postData = "action=ding&Type=" + t + "&SoftID=" + id + "&t=" + Math.random();
	$.ajax({
		type: "get",
		url: "http://comment.zxxk.com/AddAppraise.aspx",
		data: postData,
		dataType: 'jsonp',
		success: function (data) {
			if (data.msg === 'ok') {
				if (oSoftIdInfo === "") {
					oNewSoftIdInfo = id.toString();
				} else {
					oNewSoftIdInfo += id.toString();
				}
				Storage.Set(sCaCheName, sName, oNewSoftIdInfo);
				var dingcount = $("#dingcount").text();
				dingcount++;
				$("#dingcount").text(dingcount);
			} else {
				alert(data.msg);
			}
		},
		error: function (xmlHttpRequest, textStatus, errorThrown) {
			alert("投票失败!");
		},
		timeout: 8000
	});
}