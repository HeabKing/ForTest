using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace _2016_09_22_任务执行服务
{
    public interface IWorkItem
    {
        Task RunAsync();
    }

    public class WorkItems : List<IWorkItem>
    {
        public void Start()
        {
            ForEach(item => item.RunAsync());
        }

        public void Stop() { }    //TODO 如何停止一个Task
    }

    public class IntervalWorkItem : IWorkItem
    {
        /// <summary>
        /// 创建一个在指定时间之后执行一次的工作项
        /// </summary>
        /// <param name="build">要执行的任务</param>
        /// <param name="interval">间隔的时间</param>
        /// <param name="isRunAtBegin">是否在程序初始化运行的时候执行一次</param>
        public IntervalWorkItem(Action build, TimeSpan interval, bool isRunAtBegin = true)
        {
            Interval = interval;
            _build = build;
            IsRunAtBegin = isRunAtBegin;
        }
        /// <summary>
        /// 任务执行的时间间隔
        /// </summary>
        private TimeSpan Interval { get; set; }

        /// <summary>
        /// 要执行的任务
        /// </summary>
        private readonly Action _build;

        /// <summary>
        /// 在程序初始化的时候是否执行一次
        /// </summary>
        private bool IsRunAtBegin { get; set; }

        private bool _isInitRun = true;

        // TODO 将这里的死循环换成事件
        public async Task RunAsync()
        {
            while (true)
            {
                if (IsRunAtBegin && _isInitRun)
                {
                    _build();
                    _isInitRun = false;
                }
                await Task.Delay(Interval);
                _build();
            }
        }
    }

    public class AnchorTimeWorkItem : IWorkItem
    {
        /// <summary>
        /// 创建一个在固定时间运行的工作项
        /// </summary>
        /// <param name="build">要执行的任务</param>
        /// <param name="excuteTime">要执行的时间集合</param>
        public AnchorTimeWorkItem(Action build, params DateTime[] excuteTime)
        {
            ExcuteTime = excuteTime;
            if (!ExcuteTime.Any())
            {
                throw new Exception("最少指定一个固定工作项的执行时间");
            }
            _build = build;
        }

        private TimeSpan _checkInterval = TimeSpan.FromMilliseconds(TimeSpan.FromMinutes(1).Milliseconds);
        /// <summary>
        /// 检查是否执行的过程的时间间隔
        /// </summary>
        public TimeSpan CheckInterval
        {
            get { return _checkInterval; }
            set { _checkInterval = value; }
        }

        /// <summary>
        /// 执行任务的时间
        /// </summary>
        private IEnumerable<DateTime> ExcuteTime { get; set; }

        private readonly Action _build;

        // TODO 将这里的死循环换成事件
        public async Task RunAsync()
        {
            while (true)
            {
                await Task.Delay(_checkInterval);  // 检查是否执行的时间间隔
                var now = DateTime.Now;
                var isRun = ExcuteTime.Any(time => 0 <= (now - time).TotalMilliseconds
                                                   && (now - time).TotalMilliseconds < _checkInterval.TotalMilliseconds);
                if (isRun)
                {
                    _build();
                }
            }
        }
    }
}
