using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HAL062app.moduly.laboratorium
{
    public partial class laboratoriumForm : Form
    {
        public event Action<Message> SendFrame_Action;

        public laboratoriumForm()
        {
            InitializeComponent();
            Rewolwer_panel.Paint += Rewolwer_panel_Paint;
        }

        private void ModulUpBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(50);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ModulStopBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(0);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }

        private void ModulDownBtn_Click(object sender, EventArgs e)
        {
            Message frame = new Message();
            frame.buffer[0] = (byte)('#');
            frame.buffer[1] = (byte)(194);
            frame.buffer[2] = (byte)(3);
            frame.buffer[3] = (byte)(50);
            frame.buffer[4] = (byte)(0);
            frame.buffer[5] = (byte)(0);
            frame.buffer[6] = (byte)(0);
            frame.buffer[7] = (byte)(0);
            frame.buffer[8] = (byte)(0);
            frame.buffer[9] = (byte)(0);
            frame.text = new string(frame.encodeMessage());
            SendFrame_Action(frame);
        }


        private void laboratoriumForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            e.Cancel = true;
        }

        private void Labo_visualizationPanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void Rewolwer_panel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            int centerX = Rewolwer_panel.Width / 2;
            int centerY = Rewolwer_panel.Height / 2;
            int radius = 120;
            int slotCount = 8;
            int slotSize = 50;

            // Rysowanie probówek w okręgu
            for (int i = 0; i < slotCount; i++)
            {
                double angle = 2 * Math.PI * i / slotCount;
                int x = centerX + (int)(radius * Math.Cos(angle));
                int y = centerY + (int)(radius * Math.Sin(angle));

                Rectangle slotRect = new Rectangle(x - slotSize / 2, y - slotSize / 2, slotSize, slotSize);

                // Próbówki – kolory przykładowe
                Brush fill = Brushes.LightBlue;
                g.FillEllipse(fill, slotRect);
                g.DrawEllipse(Pens.Black, slotRect);

                // Numer slotu
                var sf = new StringFormat() { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center };
                g.DrawString((i + 1).ToString(), this.Font, Brushes.Black, slotRect, sf);
            }

            // Środek rewolweru
            int hubSize = 40;
            g.FillEllipse(Brushes.Gray, centerX - hubSize / 2, centerY - hubSize / 2, hubSize, hubSize);
            g.DrawEllipse(Pens.Black, centerX - hubSize / 2, centerY - hubSize / 2, hubSize, hubSize);

            // Spektrometr – lewy prostokąt
            Rectangle spektrometr = new Rectangle(20, centerY - 30, 60, 60);
            g.FillRectangle(Brushes.Purple, spektrometr);
            g.DrawRectangle(Pens.Black, spektrometr);
            g.DrawString("Spektro", this.Font, Brushes.White, spektrometr, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            // Mieszadło – dół
            Rectangle mieszadlo = new Rectangle(centerX - 30, Rewolwer_panel.Height - 70, 60, 40);
            g.FillRectangle(Brushes.Orange, mieszadlo);
            g.DrawRectangle(Pens.Black, mieszadlo);
            g.DrawString("Mieszadło", new Font(this.Font.FontFamily, 7), Brushes.Black, mieszadlo, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });

            // Lejek z glebą – prawa strona
            Point[] lejek = new Point[]
            {
        new Point(Rewolwer_panel.Width - 70, centerY - 20),
        new Point(Rewolwer_panel.Width - 30, centerY),
        new Point(Rewolwer_panel.Width - 70, centerY + 20)
            };
            g.FillPolygon(Brushes.SaddleBrown, lejek); // gleba
            g.DrawPolygon(Pens.Black, lejek);
            g.DrawString("Gleba", this.Font, Brushes.Black, Rewolwer_panel.Width - 75, centerY - 40);

            // Odczynniki – góra
            Rectangle odczynniki = new Rectangle(centerX - 30, 10, 60, 30);
            g.FillRectangle(Brushes.LightGreen, odczynniki);
            g.DrawRectangle(Pens.Black, odczynniki);
            g.DrawString("Odczynniki", new Font(this.Font.FontFamily, 7), Brushes.Black, odczynniki, new StringFormat { Alignment = StringAlignment.Center, LineAlignment = StringAlignment.Center });
        }

        private void Rewolwer_panel_Resize(object sender, EventArgs e)
        {
            Rewolwer_panel.Resize += (s, args) => Rewolwer_panel.Invalidate();

        }
    }
}
