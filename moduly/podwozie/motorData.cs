using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HAL062app.moduly.podwozie
{
    public class motorData
    {
       public int LF = 0;
       public int LM = 0;
       public int LB = 0;
       
       public int RF = 0;
       public int RM = 0;
       public int RB = 0;


        public motorData(int rf, int rm, int rb, int lf, int lm, int lb)
        {
            LF = lf; LM = lm; LB = lb; RF = rf; RM = rm; RB = rb;


        }
        
    }
}
