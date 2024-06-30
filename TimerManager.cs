using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app
{
    public class TimerManager
    {
        private  Timer _timer;
        private int elapsedTime;
        private int readInterval;

        public TimerManager(int interval, int readInterval)
        {
            _timer = new Timer();
            _timer.Interval = interval;

            _timer.Tick += Timer_Tick;
            elapsedTime = 0;
            _timer.Start();
            

        }

        private void MainTimer_Elapsed(object state)
        {
            
        }
        public event EventHandler TimerIntervalService;

        protected virtual void OnTimerIntervalService(EventArgs e)
        {
            TimerIntervalService?.Invoke(this, e);
        }
        private async void Timer_Tick(object sender, EventArgs e)
        {
            elapsedTime += _timer.Interval;

            if (elapsedTime >= readInterval)
            {
                elapsedTime = 0;
               await Task.Run((() => OnTimerIntervalService(EventArgs.Empty)));
            }
        }
        public void Start()
        {
            if (!_timer.Enabled)
            {
                elapsedTime = 0;
                _timer.Start();
            }
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
