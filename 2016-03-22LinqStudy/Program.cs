using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_03_22LinqStudy
{
	class Program
	{
		static void Main(string[] args)
		{
			var db = new dbDataContext();
			IQueryable<TblSurvey> result = from m in db.TblSurvey
						 select m;
			Console.WriteLine(result);
		}
	}
}
