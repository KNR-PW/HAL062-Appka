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
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TerminalBox = new System.Windows.Forms.ListBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.ClearFilterBtn = new HAL062app.CustomControls.CustomButton();
            this.SendBtn = new HAL062app.CustomControls.CustomButton();
            this.FilterBtn = new HAL062app.CustomControls.CustomButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // sendTextBox
            // 
            this.sendTextBox.AllowDrop = true;
            this.sendTextBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.sendTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.sendTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.sendTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.sendTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.sendTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sendTextBox.Location = new System.Drawing.Point(10, 10);
            this.sendTextBox.Margin = new System.Windows.Forms.Padding(10, 10, 10, 0);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(511, 29);
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
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.AddColumns;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(722, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(20);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(642, 579);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // TerminalBox
            // 
            this.TerminalBox.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.TerminalBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.TerminalBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.TerminalBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.TerminalBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.TerminalBox.FormattingEnabled = true;
            this.TerminalBox.HorizontalScrollbar = true;
            this.TerminalBox.ItemHeight = 20;
            this.TerminalBox.Location = new System.Drawing.Point(21, 21);
            this.TerminalBox.Margin = new System.Windows.Forms.Padding(0);
            this.TerminalBox.MinimumSize = new System.Drawing.Size(200, 200);
            this.TerminalBox.Name = "TerminalBox";
            this.TerminalBox.Size = new System.Drawing.Size(600, 428);
            this.TerminalBox.TabIndex = 5;
            // 
            // panel1
            // 
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ClearFilterBtn);
            this.panel1.Controls.Add(this.SendBtn);
            this.panel1.Controls.Add(this.FilterBtn);
            this.panel1.Controls.Add(this.sendTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(21, 450);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(600, 108);
            this.panel1.TabIndex = 3;
            // 
            // ClearFilterBtn
            // 
            this.ClearFilterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ClearFilterBtn.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.ClearFilterBtn.BackgroundColor = System.Drawing.Color.PaleGoldenrod;
            this.ClearFilterBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.ClearFilterBtn.BorderRadius = 10;
            this.ClearFilterBtn.BorderSize = 0;
            this.ClearFilterBtn.FlatAppearance.BorderSize = 0;
            this.ClearFilterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearFilterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ClearFilterBtn.ForeColor = System.Drawing.Color.Black;
            this.ClearFilterBtn.Location = new System.Drawing.Point(424, 73);
            this.ClearFilterBtn.Name = "ClearFilterBtn";
            this.ClearFilterBtn.Size = new System.Drawing.Size(97, 30);
            this.ClearFilterBtn.TabIndex = 4;
            this.ClearFilterBtn.Text = "Wyczyść filtr";
            this.ClearFilterBtn.TextColor = System.Drawing.Color.Black;
            this.ClearFilterBtn.UseVisualStyleBackColor = false;
            // 
            // SendBtn
            // 
            this.SendBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SendBtn.BackColor = System.Drawing.Color.Purple;
            this.SendBtn.BackgroundColor = System.Drawing.Color.Purple;
            this.SendBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.SendBtn.BorderRadius = 20;
            this.SendBtn.BorderSize = 0;
            this.SendBtn.FlatAppearance.BorderSize = 0;
            this.SendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SendBtn.ForeColor = System.Drawing.Color.White;
            this.SendBtn.Location = new System.Drawing.Point(527, 10);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(68, 93);
            this.SendBtn.TabIndex = 3;
            this.SendBtn.Text = "Wyślij";
            this.SendBtn.TextColor = System.Drawing.Color.White;
            this.SendBtn.UseVisualStyleBackColor = false;
            this.SendBtn.Click += new System.EventHandler(this.SendBtn_Click);
            // 
            // FilterBtn
            // 
            this.FilterBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.FilterBtn.BackColor = System.Drawing.Color.PaleGoldenrod;
            this.FilterBtn.BackgroundColor = System.Drawing.Color.PaleGoldenrod;
            this.FilterBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.FilterBtn.BorderRadius = 10;
            this.FilterBtn.BorderSize = 0;
            this.FilterBtn.FlatAppearance.BorderSize = 0;
            this.FilterBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FilterBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.FilterBtn.ForeColor = System.Drawing.Color.Black;
            this.FilterBtn.Location = new System.Drawing.Point(424, 42);
            this.FilterBtn.Name = "FilterBtn";
            this.FilterBtn.Size = new System.Drawing.Size(97, 30);
            this.FilterBtn.TabIndex = 2;
            this.FilterBtn.Text = "Filtruj";
            this.FilterBtn.TextColor = System.Drawing.Color.Black;
            this.FilterBtn.UseVisualStyleBackColor = false;
            // 
            // komunikacjaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1364, 579);
            this.Controls.Add(this.tableLayoutPanel1);
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
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox TerminalBox;
        private CustomControls.CustomButton FilterBtn;
        private CustomControls.CustomButton SendBtn;
        private CustomControls.CustomButton ClearFilterBtn;
    }
}