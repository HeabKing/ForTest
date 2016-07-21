using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _2016_07_21_暴力麻将
{
	class Program
	{
		// c. 麻将暴力平胡判断: C142C123C93C63C33 = 3363, 3600
		static void Main(string[] args)
		{
			while (true)
			{
				var data = GetData().ToList();
				data[0] = 13;
				try
				{
					IsTrue(data);
				}
				catch (FormatException)
				{
					throw;
				}
			}
		}

		private static Random random = new Random();
		public static IEnumerable<int> GetData()
		{
			for (int i = 0; i < 14; i++)
			{
				yield return random.Next() % 14;
			}
		}

		private static int a = 0;
		private static int b = 0;
		public static void IsTrue(IList<int> list)
		{
			Console.WriteLine(a++);
			if (list.Count == 2)
			{
				if (list[0] == list[1])
				{
					Console.WriteLine("shi");
					Console.WriteLine(++b);
					throw new FormatException();
					return;
				}
				else
				{
					Console.WriteLine("bushi");
					return;
				}
			}
			else if (list.Count%3 == 2 && list.Count < 15)
			{
				var listTemp = RandGet3(list);
				foreach (var item in listTemp) 
				{
					if (IsRight3(item))
					{
						var temp = ExceptOnce(list.ToList(), item).ToList();
						IsTrue(temp);
					}
				}
			}
			else
			{
				throw new ArgumentException();
			}
		}

		public static List<int> ExceptOnce(List<int> listParam, List<int> list2Param)
		{
			var list = listParam;
			var list2 = list2Param;
			for (int i = 0; i < list.Count; i++)
			{
				for (int j = 0; j < list2.Count; j++)
				{
					if (list[i] != list2[j])
					{
						continue;
					}
					list[i] = -1;
					list2[j] = -2;
				}
			}

			return list.Where(s => s > 0).ToList();
		}

		public static Func<IList<int>, List<List<int>>> RandGet3 = list =>
	   {
		   var result = new List<List<int>>();
		   for (int x = 0; x < list.Count - 2; x++)
		   {
			   var xx = list[x];
			   for (int y = x + 1; y < list.Count - 1; y++)
			   {
				   var yy = list[y];
				   for (int z = y + 1; z < list.Count; z++)
				   {
					   var zz = list[z];
					   var ar = new[] { xx, yy, zz };
					   result.Add(ar.ToList());
				   }
			   }
		   }
		   return result;
	   };

		static readonly Func<IList<int>, bool> IsRight3 = list =>
		{
			
			if (list.Distinct().Count() != 3)
			{
				return list[0] == list[1] && list[1] == list[2];
			}
			else
			{
				var min = Math.Min(list[0], Math.Min(list[1], list[2]));
				return list.Aggregate(0, (c, i) => c += i - min) == 3;
			}
		};
	}
}
