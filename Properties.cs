using System;
using System.Media;
using System.Windows.Forms;
using System.Drawing;




namespace Hall062app
{



    public enum AppTheme
    {
        Light,
        Dark,
    }

    public class Theme
    {
     
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Color Accent { get; set; }
        public Theme(Color background, Color foreground, Color accent)
        {
            Background = background;
            Foreground = foreground;

            Accent = accent;
        }
    }



    public class Properties
    {
        

        private void ApplyTheme(Control ctrl ,Theme theme)
        {
            ctrl.BackColor = theme.Background;
            ctrl.ForeColor = theme.Foreground;


            if (ctrl is Form)
            {
                ctrl.BackColor = theme.Background;

            }
            
          





        }




    }


   
}