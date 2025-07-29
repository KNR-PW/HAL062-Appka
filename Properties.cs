using System;
using System.Media;
using System.Windows.Forms;
using System.Drawing;
using HAL062app.Properties;
using HAL062app.CustomControls;




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
        public Color Frame { get; set; }
        public Color Text { get; set; }
        public Theme(String background, String foreground, String frame, String accent, String text)
        {
            Background = ColorTranslator.FromHtml(background);
            Foreground = ColorTranslator.FromHtml(foreground);
            Frame = ColorTranslator.FromHtml(frame);
            Accent = ColorTranslator.FromHtml(accent);
            Text = ColorTranslator.FromHtml(text);
        }
    }



    public class PropertiesManager
    {
        private Theme lightTheme = new Theme(
            
            background: "#A4B8C4",
            foreground: "#FFFFFF",
            frame: "#393D3F",
            accent: "#8D9F87",
            text: "#3C3C4C"

            );



        private Theme Dark = new Theme(

            background: "#6F9283",
            foreground: "#FFFFFF",
            frame: "#393D3F",
            accent: "#8D9F87",
            text: "#FFFFFF"

            );

        public void ApplyTheme(Control ctrl, AppTheme ThemeName)
        {
            Theme theme = ThemeName == AppTheme.Light ? lightTheme : Dark;

            ctrl.BackColor = theme.Background;
            ctrl.ForeColor = theme.Foreground;

            if (ctrl is PictureBox pictureBox && pictureBox.Name == "LogoPictureBox")
            {
                pictureBox.Image = ThemeName == AppTheme.Light 
                    ? Resources.HALCC
                    : Resources.HALCCW;
            }

            if (ctrl is Form)
            {
                ctrl.BackColor = theme.Frame;

            }
            if(ctrl is Panel)
            {
                ctrl.BackColor = theme.Background;
            }
            if(ctrl is FlowLayoutPanel)
            {
                ctrl.BackColor = theme.Background;
            }

            if(ctrl is Label)
                {
                ctrl.ForeColor = theme.Text;
      
            }

            if(ctrl is CustomButton btn)
            {
                ctrl.ForeColor = theme.Text;
                btn.UseVisualStyleBackColor = false;
            }

            foreach (Control child in ctrl.Controls)
            {
                ApplyTheme(child, ThemeName);
            }

            



        }




    }


   
}