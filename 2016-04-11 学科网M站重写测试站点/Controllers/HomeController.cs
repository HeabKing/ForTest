using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace _2016_04_11_学科网M站重写测试站点.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

		/// <summary>
		/// 资讯详情 - 点赞
		/// </summary>
		/// <returns></returns>
		[Route("AppraiseArticle2")]
	    public ActionResult AppraiseArticle2()
	    {
		    return View("AppraiseArticle");
	    }

		/// <summary>
		/// 资讯详情 - 评论
		/// </summary>
		/// <returns></returns>
	    public ActionResult ArticleComment()
	    {
		    return View();
	    }

		/// <summary>
		/// 资讯/资料 - 评论详情
		/// </summary>
		/// <returns></returns>
		[Route("~/Comment/CommentInfo")]
	    public ActionResult ArticleCommentInfo()
	    {
		    return View();
	    }

	    /// <summary>
		/// 资料详情 - 点赞
		/// </summary>
		/// <returns></returns>
	    public ActionResult AppraiseSoft()
	    {
		    return View();
	    }

		/// <summary>
		/// 资料详情 - 评论
		/// </summary>
		/// <returns></returns>
	    public ActionResult SoftComment()
	    {
		    return View();
	    }

	    /// <summary>
		/// 资讯/资料收藏
		/// </summary>
		/// <returns></returns>
	    public ActionResult SoftFavorite()
	    {
		    return View();
	    }

		/// <summary>
		/// 根据滚动条自动加载
		/// </summary>
		/// <returns></returns>
	    public ActionResult ScrollBarAutoLoad()
		{
			return View();
		}

	    public ActionResult ScrollBarAutoLoadPartial()
	    {
		    return Content($"<h3>第{Request["PageIndex"]}页</h3><a href=\"/soft/4724146.html\" title=\"普通征集-复审推荐\" class=\"no-line\" data-ajax=\"false\" data-signnum=\"2\"><div class=\"bot-bt pad2 gm-list of-x\"><p class=\"of-x\">普通征集-复审推荐</p><p class=\"sm-gy\"><span>关键字：初审,初审</span></p><p class=\"sm-gy\"><span>下载:0次</span><span class=\"mid\">免费</span><span>发布日期：2016.4.14</span></p></div></a>");
	    }
    }
}