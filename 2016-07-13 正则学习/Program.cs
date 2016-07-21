using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace _2016_07_13_正则学习
{
	class Program
	{
		static void Main(string[] args)
		{
			string input = "Aspoo=UserID=28129796&UserPassword=f379eaf3c831b04de153469d1bec345e&UserGroupID=4&UserName=admin%4066666; Token=Value=E0127416-6B63-44A1-B421-8099180EAB6B; Hm_lvt_0e522924b4bbb2ce3f663e505b2f1f9c=1467863507,1467880969,1467880977,1468284725; Hm_lvt_b2d26de9325966c888c352837df61fec=1467883333,1467941048,1468286470,1468401044; Hm_lpvt_b2d26de9325966c888c352837df61fec=1468403002; cn_e016df0f58546c3fa10b_dplus=%7B%22distinct_id%22%3A%20%221558043c471479-0f5b61f5d-19586864-4a640-1558043c472804%22%2C%22%24_sessionid%22%3A%200%2C%22%24_sessionTime%22%3A%201468402376%2C%22%24dp%22%3A%200%2C%22%24_sessionPVTime%22%3A%201468402376%2C%22%24initial_time%22%3A%20%221466733175%22%2C%22%24initial_referrer%22%3A%20%22http%3A%2F%2Fm.49105.zxxk.com%3A8015%2FUser%2FUserAsset%22%2C%22%24initial_referring_domain%22%3A%20%22m.49105.zxxk.com%3A8015%22%2C%22%24recent_outside_referrer%22%3A%20%22%24direct%22%2C%22initial_view_time%22%3A%20%221468219946%22%2C%22initial_referrer%22%3A%20%22%24direct%22%2C%22initial_referrer_domain%22%3A%20%22%24direct%22%7D; CNZZDATA1259100373=773323365-1467609350-http%253A%252F%252Fdownload.49105.zxxk.com%253A8090%252F%7C1468398241";
			var result = Regex.Replace(input, @"(?:(?:^|.*;\s*)Aspoo\s*\=\s*([^;]*).*$)|^.*$", "$1");
			Console.WriteLine(result);
		}
	}
}
