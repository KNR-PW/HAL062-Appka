namespace HAL062app.moduly.komunikacja
{
    partial class komunikacjaForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.button1 = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.customProgressBar1 = new HAL062app.CustomControls.CustomProgressBar();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.SendBtn = new System.Windows.Forms.Button();
            this.TerminalBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(73, 154);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 0;
            this.button1.Text = "button1";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(73, 225);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(100, 20);
            this.textBox1.TabIndex = 1;
            // 
            // customProgressBar1
            // 
            this.customProgressBar1.ChannelColor = System.Drawing.Color.LightSteelBlue;
            this.customProgressBar1.ChannelHeight = 6;
            this.customProgressBar1.ForeBackColor = System.Drawing.Color.RoyalBlue;
            this.customProgressBar1.ForeColor = System.Drawing.Color.White;
            this.customProgressBar1.Location = new System.Drawing.Point(431, 101);
            this.customProgressBar1.Name = "customProgressBar1";
            this.customProgressBar1.ShowMaximun = false;
            this.customProgressBar1.ShowValue = HAL062app.CustomControls.TextPosition.Right;
            this.customProgressBar1.Size = new System.Drawing.Size(166, 23);
            this.customProgressBar1.SliderColor = System.Drawing.Color.RoyalBlue;
            this.customProgressBar1.SliderHeight = 6;
            this.customProgressBar1.SymbolAfter = "";
            this.customProgressBar1.SymbolBefore = "";
            this.customProgressBar1.TabIndex = 4;
            // 
            // sendTextBox
            // 
            this.sendTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.sendTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.sendTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.sendTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.sendTextBox.Location = new System.Drawing.Point(10, 10);
            this.sendTextBox.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(287, 29);
            this.sendTextBox.TabIndex = 0;
            this.sendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendTextBox_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TerminalBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(946, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0, 0, 0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(418, 579);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.SendBtn);
            this.panel1.Controls.Add(this.sendTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(21, 450);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(376, 108);
            this.panel1.TabIndex = 3;
            // 
            // SendBtn
            // 
            this.SendBtn.BackColor = System.Drawing.Color.Silver;
            this.SendBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.SendBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.SendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.SendBtn.Location = new System.Drawing.Point(307, 0);
            this.SendBtn.Margin = new System.Windows.Forms.Padding(0, 5, 0, 5);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(67, 106);
            this.SendBtn.TabIndex = 1;
            this.SendBtn.Text = "Send";
            this.SendBtn.UseVisualStyleBackColor = false;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // TerminalBox
            // 
            this.TerminalBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.TerminalBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TerminalBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.TerminalBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TerminalBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.TerminalBox.FormattingEnabled = true;
            this.TerminalBox.HorizontalScrollbar = true;
            this.TerminalBox.ItemHeight = 20;
            this.TerminalBox.Location = new System.Drawing.Point(21, 21);
            this.TerminalBox.Margin = new System.Windows.Forms.Padding(0);
            this.TerminalBox.Name = "TerminalBox";
            this.TerminalBox.Size = new System.Drawing.Size(376, 428);
            this.TerminalBox.TabIndex = 5;
            // 
            // komunikacjaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1364, 579);
            this.Controls.Add(this.customProgressBar1);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.button1);
            this.Name = "komunikacjaForm";
            this.Text = "komunikacjaForm";
            this.Load += new System.EventHandler(this.komunikacjaForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox textBox1;
        private CustomControls.CustomProgressBar customProgressBar1;
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button SendBtn;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox TerminalBox;
    }
}