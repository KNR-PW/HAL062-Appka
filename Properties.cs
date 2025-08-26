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
        public Color SidePanel { get; set; }
        public Color Background { get; set; }
        public Color Foreground { get; set; }
        public Color Accent { get; set; }
        public Color Frame { get; set; }
        public Color Text { get; set; }
        public Theme(String background, String foreground, String frame, String accent, String text, String sidePanel)
        {
            SidePanel = ColorTranslator.FromHtml(sidePanel);
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

            sidePanel: "#F1DEDE",
            background: "#EBEBEB",
            foreground: "#FFFFFF",
            frame: "#393D3F",
            accent: "#8D9F87",
            text: "#3C3C4C"

            );



        private Theme Dark = new Theme(

            sidePanel: "#11151C",
            background: "#212D40",
            foreground: "#212D40",
            frame: "#364156",
            accent: "#7D4E57",
            text: "#FFFFFF"

            );

        public void ApplyTheme(Control ctrl, AppTheme ThemeName)
        {
            Theme theme = ThemeName == AppTheme.Light ? lightTheme : Dark;

         
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
                if (ctrl.Name == "SidePanel")
                {
                    ctrl.BackColor = theme.SidePanel;
                }
                else if(ctrl.BackColor != Color.Transparent)
                {
                    ctrl.BackColor = theme.Background;
                }
            }
            if(ctrl is FlowLayoutPanel)
            {
            
                if (ctrl.BackColor != Color.Transparent)
                    ctrl.BackColor = theme.Background;
                
            }
            if(ctrl is TableLayoutPanel)
            {
             
                if (ctrl.BackColor != Color.Transparent)
                    ctrl.BackColor = theme.Background;
                
            }
            if(ctrl is Label)
                {
                ctrl.ForeColor = theme.Text;
      
            }

            if(ctrl is CustomButton btn)
            {
                if (ThemeName == AppTheme.Light)
                btn.BorderModeSelect = CustomButton.BorderMode.On;
                else
                btn.BorderModeSelect = CustomButton.BorderMode.Off;



            }

            foreach (Control child in ctrl.Controls)
            {
                ApplyTheme(child, ThemeName);
            }

            



        }




    }


   
}