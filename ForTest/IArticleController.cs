using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ForTest
{
	public class E_Article
	{
	};

	public class E_Soft
	{
	};

	// ========================= 业务层 ======================
	public interface IArticleController
	{
		IEnumerable<E_Article> List(/*条件类*/);
		E_Article Detail(int articleId);
	}

	public interface IT_Article
	{
		IEnumerable<E_Article> GetArticleList(/*条件类*/);
		E_Article GetArticleInfo(int articleId);
	}

	public interface IT_Soft
	{
		E_Soft GetSoftInfo(int softId);
	}

	// ========================= 数据访问层 ======================
	public interface ID_Article
	{
		IEnumerable<E_Article> GetArticleList(/*条件类*/);
		E_Article GetArticleInfo(int articleId);
	}

	public interface ID_Soft
	{
		E_Article GetSoftInfo(int softId);
	}
}
