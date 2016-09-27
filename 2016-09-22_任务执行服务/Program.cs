using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Topshelf;

namespace _2016_09_22_任务执行服务
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // 使用 HostFactory 配置一个托管
            HostFactory.Run(x => // 使用 x 暴漏所有配置信息
            {
                // 告诉 Topself 这里有一个 TownCrier 类型的服务
                x.Service<WorkItems>(s =>
                {
                    // 如何创建服务实例
                    s.ConstructUsing(name =>SinxWorkItemsBuilder.Build());
                    // 开始服务
                    s.WhenStarted(tc => tc.Start());
                    // 关闭服务
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();
                // 设置服务的描述
                x.SetDescription("工作项大全");
                x.SetDisplayName("Sinx.WorkItems");
                x.SetServiceName("Sinx.WorkItems");

                x.SetStartTimeout(TimeSpan.FromSeconds(10));
                x.SetStopTimeout(TimeSpan.FromSeconds(10));

                x.EnableServiceRecovery(c =>
                {
                    //c.RunProgram(1, "notepad.exe"); // run a program
                    c.RestartService(1); //1分钟后尝试重启
                    c.OnCrashOnly(); //服务崩溃时才会重启    
                });
            });

        }
    }
}
