using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _2016_09_22_任务执行服务.SinxWorkItems;

namespace _2016_09_22_任务执行服务
{
    public static class SinxWorkItemsBuilder
    {
        public static WorkItems Build()
        {
            var workItems = new WorkItems
            {
                // 每天去OA爬积分, 积累到一定的积分給指定邮箱发信息
                new AnchorTimeWorkItem(() =>
                    OaSpider.Begin("何士雄", "he394899990", "394899990@qq.com"),
                    DateTime.Today.AddHours(18))
                    {
                        CheckInterval = TimeSpan.FromMinutes(1)
                    }  // TODO 如果能过忽略日期直接根据时间更好
            };
            return workItems;
        }
    }
}
