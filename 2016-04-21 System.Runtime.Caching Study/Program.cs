using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Runtime.Caching;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace _2016_04_21_System.Runtime.Caching_Study
{
	class Program
	{
		/// <summary>
		/// 1. obj.GetType().FullName是否能保证在相同签名的lambda不同的返回类型中得到不同的值, 不能, 所有的lambda名字都一样
		/// 2. fun.ToString() 虽然能, 但是相同的lambda也一样了
		/// </summary>
		/// <param name="args"></param>
		static void Main(string[] args)
		{
			// 第一个问题
			var s1 = TypeFullName(GetDateTime);
			var a = 11;
			var s2 = TypeFullName(() => GetDateTime(a));
			var s3 = TypeFullName(() => GetDateTime(a));
			var s4 = TypeFullName(() => GetDateTime(a, 2));

			CacheHelper.Get(GetDateTime);
		}

		public static T TypeFullName<T>(Func<T> fun, string key = null) where T: class
		{
			if (key == null)
			{
				key = typeof (T).FullName;
			}
			RuntimeHelpers.GetHashCode(fun);
			Debug.WriteLine(fun.GetHashCode());
			return fun();
		}

		public static string GetDateTime()
		{
			return DateTime.Now.ToString(CultureInfo.CurrentCulture);
		}
		public static string GetDateTime(int a, int b = 1)
		{
			return DateTime.Now.ToString(CultureInfo.CurrentCulture);
		}
	}

	public class CacheHelper
	{
		/// <summary>
		/// 缓存
		/// </summary>
		private static readonly ObjectCache ObjectCache = MemoryCache.Default;
		public static T Get<T>(Func<T> func, string key = null) where T : class
		{
			if (key == null)
			{
				key = typeof (T).FullName;
			}
			var chche = ObjectCache[key] as T;

			if (chche == null)
			{
				ObjectCache.Set(key, func(), new CacheItemPolicy
				{
					//SlidingExpiration = TimeSpan.FromHours(1),          // 一个小时不访问就过期
					AbsoluteExpiration = DateTimeOffset.Now.AddDays(1)  // 一天过后就过期
				});
			}

			return ObjectCache[key] as T;
		}
	}
}
