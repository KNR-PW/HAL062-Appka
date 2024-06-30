using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app
{
    public sealed class EventAggregator
    {
        private static readonly Lazy<EventAggregator> lazy = new Lazy<EventAggregator>(() => new EventAggregator());

        public static EventAggregator Instance { get { return lazy.Value; } }

        private EventAggregator() { }

        public event EventHandler TimerStartRequested;
        public event EventHandler TimerStopRequested;

        public void RaiseTimerStartRequested()
        {
            TimerStartRequested?.Invoke(this, EventArgs.Empty);
        }
        public void RaiseTimerStopRequested()
        {
            TimerStopRequested?.Invoke(this, EventArgs.Empty);
        }
    }
}
