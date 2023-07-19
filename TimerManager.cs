using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace HAL062app
{
    public class TimerManager
    {
        private Timer mainTimer;

        public void StartTimer()
        {
            mainTimer = new Timer(MainTimer_Elapsed, null, 0, 1000); // Interwał 1000ms (1 sekunda)
        }

        private void MainTimer_Elapsed(object state)
        {
            // Wykonaj swoje operacje, które mają być wykonywane co sekundę
        }

    }
}
