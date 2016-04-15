// 何士雄 2016-04-15
// HeabSplider使用说明:
// 将项目中一个元素的Id设置为 SliderLoadMore , 然后本JS脚本将会以此为[锚点], 每次滚动条的滚动或者此元素的点击将会去后台拿取数据, 拿取的数据放在次[锚点]元素的上方
// 参数设置:
//		1. 必须参数:	Url: 去后台拿数据的地址;	
//		2. 可选参数:	Data: 后台的数据, 如: Splider.Data = { PageIndex : 1, PageSize : 10 };
//						SuccessTimes: 每次成功的调用都会 + 1, 在分页应用中, 可以当成PageIndex且可以直接赋值n作为从n页开始 使用: Splider.SuccessTimes = PageIndex
//						NoDataText: 当后台返回不是200且不是error的时候[锚点]显示的文本, 默认为	"没有更多数据了"
//						ErrorText: 当拿取数据异常时[alert]出来的报错文本, 默认是 "服务器开小差了"
//						ContentType: 发送給服务器的数据, 默认为 "application/x-www-form-urlencoded; charset=UTF-8"(如果想传入Json请赋值为"application/json; charset=utf-8")
// 何士雄 2016-04-15

Splider = {
	Url: "",				// 去后台拿数据的地址 !必须参数!
	Data: "undefined",		// 发送给后台的数据 使用: Splider.Data = { PageIndex:1, PageSize:10 }
	SuccessTimes: 0,		// 每次成功的调用都会 + 1, 在分页应用中, 可以当成PageIndex且可以直接赋值n作为从n页开始 使用: Splider.SuccessTimes = PageIndex
	OnSuccess: "undefined",	// 成功调用以后触发的事件 使用: Splider.OnSuccess = function(SuccessTimes){}
	NoDataText: "没有更多数据了",		// 没有数据后显示的数据
	ErrorText: "服务器开小差了",		// 发生错误时[alert]出来的数据
	ContentType: "application/x-www-form-urlencoded; charset=utf-8" // "application/json; charset=utf-8"
};

$(function () {
	BindScrollAndClickEvent();
});

// 绑定滑轮滚动事件和加载更多按钮点击事件
function BindScrollAndClickEvent() {
	// 是否正在下载 [全局变量]
	isLoading = false;
	// 绑定滑轮滚动事件
	$(window).scroll(function (e) {
		if (isLoading) { return; }
		var sliderLoadMore = $("#SliderLoadMore");
		var isSliderLoadMoreInScreen = (sliderLoadMore.offset().top - $(this).scrollTop())	// 加载更多按钮距离网页上方的距离
			<= $(window).height();	// 滚动条滚动的距离+ sliderLoadMore.outerHeight(true)
		if (sliderLoadMore.length === 1 && isSliderLoadMoreInScreen) {
			sliderLoadMore.click();
		}
	});
	// 绑定加载更多按钮点击事件
	$("#SliderLoadMore").on("click", function (e) {
		isLoading = true;
		var data = {}
		if (Splider.Data !== "undefined") {
			data = Splider.Data;
		}
		if (Splider.ContentType.toLowerCase() === "application/json; charset=utf-8" ||
			Splider.ContentType.toLowerCase() === "application/json;charset=utf-8") {
			data = JSON.stringify(data);
		}
		$.ajax({
			type: "POST",
			url: Splider.Url, // 请求地址
			data: data,
			contentType: Splider.ContentType, // 数据类型 只有写成json后台才能有自动绑定
			//dataType: 'json', // 返回数据类型 jsonp - 跨域
			success: function (response, status) { // 200 ok status === "success"
				if (status === "success") {
					$("#SliderLoadMore").before(response);
					if (Splider.OnSuccess !== "undefined") {
						Splider.OnSuccess(++Splider.SuccessTimes);
					}
				} else {
					$("#SliderLoadMore").html(Splider.NoDataText);
				}
				isLoading = false;
			},
			error: function (xmlHttpRequest, textStatus, errorThrown) {
				alert(Splider.ErrorText);
				isLoading = false;
			}
		});
		e.preventDefault(); // 防止添加的HTML被消除掉
	});
}