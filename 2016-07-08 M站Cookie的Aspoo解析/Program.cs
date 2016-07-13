using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace _2016_07_08_M站Cookie的Aspoo解析
{
    class Program
    {
        static void Main(string[] args)
        {
            DownloadSoft(null);
        }
        public static void DownloadSoft(IEnumerable<int> softId)
        {
            // 登录判断
            var cookie = "guide_dialog=1; guide_xk_dialog=1; guide_my_dialog=1; softCartList=; Aspoo=UserID=23264097&UserPassword=d676923a50ba668fe4f2d93b4949f0ca&UserGroupID=4&UserName=HeabKing; Token=Value=613D3E0E-35DE-4EFA-BF2F-9B747B19ECF5; Hm_lvt_0e522924b4bbb2ce3f663e505b2f1f9c=1467863507,1467880969,1467880977,1467941038; Hm_lpvt_0e522924b4bbb2ce3f663e505b2f1f9c=1467941038; Hm_lvt_b2d26de9325966c888c352837df61fec=1467876544,1467878645,1467883333,1467941048; Hm_lpvt_b2d26de9325966c888c352837df61fec=1467941048; cn_e016df0f58546c3fa10b_dplus=%7B%22distinct_id%22%3A%20%221558043c471479-0f5b61f5d-19586864-4a640-1558043c472804%22%2C%22%24_sessionid%22%3A%200%2C%22%24_sessionTime%22%3A%201467886517%2C%22%24dp%22%3A%200%2C%22%24_sessionPVTime%22%3A%201467886517%2C%22%24initial_time%22%3A%20%221466733175%22%2C%22%24initial_referrer%22%3A%20%22http%3A%2F%2Fm.49105.zxxk.com%3A8015%2FUser%2FUserAsset%22%2C%22%24initial_referring_domain%22%3A%20%22m.49105.zxxk.com%3A8015%22%2C%22%24recent_outside_referrer%22%3A%20%22%24direct%22%7D; CNZZDATA1259100373=773323365-1467609350-http%253A%252F%252Fdownload.49105.zxxk.com%253A8090%252F%7C1467938927";
            var userId = GetUserId(cookie);
            // 获取资料类型
            // 进行校验
            // 扣费平台
            // 返回下载地址
        }

        private static int GetUserId(string cookie)
        {
            Func<string, string, string> getFiled = (aspooo, filed) =>
            {
                var filedInfo = Regex.Match(aspooo, filed + "=.+?(&|;|\\s|$)", RegexOptions.IgnoreCase).Value;
                if (string.IsNullOrWhiteSpace(filedInfo)) { return ""; }
                var info = Regex.Replace(filedInfo, "(&|;)", "").Trim().Split('=');
                return info.Length == 2 ? info[1] : "";
            };
            var aspoo = Regex.Match(cookie, "Aspoo=.+?(;|\\s|$)", RegexOptions.IgnoreCase).Value;
            if (!string.IsNullOrWhiteSpace(aspoo))
            {
                var userId = Convert.ToInt32(getFiled(aspoo, "UserId"));
                var userPassword = getFiled(aspoo, "UserPassword");
                // TODO 密码校验
                return userId;
            }
            return 0;
        }
    }
}
