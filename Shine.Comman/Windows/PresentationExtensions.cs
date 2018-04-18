using System;
using System.Windows.Threading;

namespace Shine.Comman.Windows
{
    /// <summary>
    /// WPF相关扩展
    /// </summary>
    public static class PresentationExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static DispatcherOperation BeginInvoke(this Dispatcher dispatcher, Action action)
        {
            return dispatcher.BeginInvoke(new Action(action));
        }
    }
}
