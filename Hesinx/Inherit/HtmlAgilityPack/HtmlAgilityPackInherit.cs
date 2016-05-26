namespace HtmlAgilityPack
{
	/// <summary>
	/// 对HtmlAgilityPack进行拓展
	/// </summary>
	/// <remarks>Hesinx 2016-05-26</remarks>
	public class HtmlAgilityPackInherit : HtmlDocument
	{
		public HtmlAgilityPackInherit(string html)
		{
			this.LoadHtml(html);
		}

		
	}
}
