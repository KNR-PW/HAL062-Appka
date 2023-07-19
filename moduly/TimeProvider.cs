using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

// ta klasa odpowiada za to, zeby klasa message mogla pobrac aktualny czas
// inne sposoby nie wiem czemu nie dzialaly, wiec trzeba bylo nowy plik tworzyc :( 


namespace HAL062app.moduly
{
    public static class TimeProvider
    {
        private static readonly DateTime startTime = DateTime.Now;
       


        public static DateTime GetCurrentTime()
        {
            return startTime.Add(DateTime.UtcNow - startTime).AddHours(2);
        }
    }
}
