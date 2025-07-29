using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;


namespace HAL062app.CustomControls
{


    public class CustomButton : Button
    {
        private int borderSize = 0;
        private int borderRadius = 20;
        private Color borderColor = Color.FromArgb(0, 155, 154, 159);
        private ButtonStyles buttonStyle = ButtonStyles.Default;

        public enum ButtonStyles
        {
            Default, //Niebieski
            Primary, //szary
            Green,
            Red,
            Off, //ciemny szary
            Functional_blue,
            Functional_orange,
            Functional_purple,

        }


        public CustomButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.Size = new Size(150, 40);
            this.ApplyStyle();
            this.Resize += new EventHandler(Button_Resize);

        }
        private void Button_Resize(object sender, EventArgs e)
        {
            if (borderRadius > this.Height)
                borderRadius = this.Height;
        }
        public int BorderSize
        {
            get { return borderSize; }
            set
            {
                borderSize = value;
                this.Invalidate();
            }
        }

        public int BorderRadius
        {
            get { return borderRadius; }
            set
            {
                borderRadius = value;
                this.Invalidate();
            }
        }

        public Color BorderColor
        {
            get { return borderColor; }
            set
            {
                borderColor = value;
                this.Invalidate();
            }
        }

        public Color BackgroundColor
        {
            get { return this.BackColor; }
            set { this.BackColor = value; }
        }
        [Category("CustomControls")]
        public Color TextColor
        {
            get { return this.ForeColor; }
            set { this.ForeColor = value; }
        }
        public ButtonStyles ButtonStyle
        {
            get => buttonStyle;
            set
            {
                buttonStyle = value;
                ApplyStyle();
            }
        }

        private GraphicsPath GetFigurePath(Rectangle rect, float radius)
        {
            GraphicsPath path = new GraphicsPath();
            float curveSize = radius * 2F;
            path.StartFigure();
            path.AddArc(rect.X, rect.Y, curveSize, curveSize, 180, 90);
            path.AddArc(rect.Right - curveSize, rect.Y, curveSize, curveSize, 270, 90);
            path.AddArc(rect.Right - curveSize, rect.Bottom - curveSize, curveSize, curveSize, 0, 90);
            path.AddArc(rect.X, rect.Bottom - curveSize, curveSize, curveSize, 90, 90);
            path.CloseFigure();
            return path;
        }

        protected override void OnPaint(PaintEventArgs pevent)
        {
            base.OnPaint(pevent);
            Rectangle rectSurface = this.ClientRectangle;
            Rectangle rectBorder = Rectangle.Inflate(rectSurface, -borderSize, -borderSize);
            int smoothSize = 2;
            if (borderSize > 0)
                smoothSize = borderSize;
            if (borderRadius > 2) //Rounded button
            {
                using (GraphicsPath pathSurface = GetFigurePath(rectSurface, borderRadius))
                using (GraphicsPath pathBorder = GetFigurePath(rectBorder, borderRadius - borderSize))
                using (Pen penSurface = new Pen(this.Parent.BackColor, smoothSize))
                using (Pen penBorder = new Pen(borderColor, borderSize))
                {
                    pevent.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    //Button surface
                    this.Region = new Region(pathSurface);
                    //Draw surface border for HD result
                    pevent.Graphics.DrawPath(penSurface, pathSurface);
                    //Button border                    
                    if (borderSize >= 1)
                        //Draw control border
                        pevent.Graphics.DrawPath(penBorder, pathBorder);
                }
            }
            else //Normal button
            {
                pevent.Graphics.SmoothingMode = SmoothingMode.None;
                //Button surface
                this.Region = new Region(rectSurface);
                //Button border
                if (borderSize >= 1)
                {
                    using (Pen penBorder = new Pen(borderColor, borderSize))
                    {
                        penBorder.Alignment = PenAlignment.Inset;
                        pevent.Graphics.DrawRectangle(penBorder, 0, 0, this.Width - 1, this.Height - 1);
                    }
                }
            }
        }
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            this.Parent.BackColorChanged += new EventHandler(Container_BackColorChanged);
        }
        private void Container_BackColorChanged(object sender, EventArgs e)
        {
            this.Invalidate();
        }


        private void ApplyStyle()
        {
            switch (buttonStyle)
            {
                case ButtonStyles.Primary:
                    this.BackColor = Color.FromArgb(0, 120, 215); // niebieski
                    this.ForeColor = Color.White;
                    break;
                case ButtonStyles.Green:
                    this.BackColor = Color.FromArgb(40, 167, 69); // zielony
                    this.ForeColor = Color.White;
                    break;
                case ButtonStyles.Red:
                    this.BackColor = Color.FromArgb(220, 53, 69); // czerwony
                    this.ForeColor = Color.White;
                    break;
                case ButtonStyles.Off:
                    this.BackColor = Color.FromArgb(100, 100, 100); 
                    this.ForeColor = Color.Black;
                    break;
                default:
                    this.BackColor = Color.FromArgb(120, 120, 120); // szary
                    this.ForeColor = Color.White;
                    break;
            }

            Color mouseColor = this.BackColor;
            mouseColor = Color.FromArgb(255, (int)(mouseColor.R * 0.8), (int)(mouseColor.G * 0.8), (int)(mouseColor.B * 0.8));
            this.FlatAppearance.MouseDownBackColor = mouseColor;
            this.FlatAppearance.MouseOverBackColor = mouseColor;
        }
    }

}
