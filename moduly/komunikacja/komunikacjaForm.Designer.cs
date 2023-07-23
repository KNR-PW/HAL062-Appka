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
            this.panel1 = new System.Windows.Forms.Panel();
            this.TerminalBox = new System.Windows.Forms.ListBox();
            this.panel2 = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel6 = new System.Windows.Forms.Panel();
            this.label5 = new System.Windows.Forms.Label();
            this.panel7 = new System.Windows.Forms.Panel();
            this.label6 = new System.Windows.Forms.Label();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.panel5 = new System.Windows.Forms.Panel();
            this.label3 = new System.Windows.Forms.Label();
            this.panel4 = new System.Windows.Forms.Panel();
            this.label2 = new System.Windows.Forms.Label();
            this.ClearFilterBtn = new HAL062app.CustomControls.CustomButton();
            this.SendBtn = new HAL062app.CustomControls.CustomButton();
            this.FilterBtn = new HAL062app.CustomControls.CustomButton();
            this.UartPortCombo = new HAL062app.CustomControls.CustomComboBox();
            this.UartBaudRateCombo = new HAL062app.CustomControls.CustomComboBox();
            this.ConnectUartBtn = new HAL062app.CustomControls.CustomButton();
            this.UartRefreshBtn = new HAL062app.CustomControls.CustomButton();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel6.SuspendLayout();
            this.panel7.SuspendLayout();
            this.panel3.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.panel5.SuspendLayout();
            this.panel4.SuspendLayout();
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
            this.sendTextBox.MinimumSize = new System.Drawing.Size(200, 29);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(472, 29);
            this.sendTextBox.TabIndex = 0;
            this.sendTextBox.KeyDown += new System.Windows.Forms.KeyEventHandler(this.sendTextBox_KeyDown);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.TerminalBox, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(581, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(583, 641);
            this.tableLayoutPanel1.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.AutoSize = true;
            this.panel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel1.Controls.Add(this.ClearFilterBtn);
            this.panel1.Controls.Add(this.SendBtn);
            this.panel1.Controls.Add(this.FilterBtn);
            this.panel1.Controls.Add(this.sendTextBox);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(11, 506);
            this.panel1.Margin = new System.Windows.Forms.Padding(0);
            this.panel1.MinimumSize = new System.Drawing.Size(200, 100);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(561, 124);
            this.panel1.TabIndex = 3;
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
            this.TerminalBox.Location = new System.Drawing.Point(11, 11);
            this.TerminalBox.Margin = new System.Windows.Forms.Padding(0);
            this.TerminalBox.MinimumSize = new System.Drawing.Size(200, 200);
            this.TerminalBox.Name = "TerminalBox";
            this.TerminalBox.Size = new System.Drawing.Size(561, 494);
            this.TerminalBox.TabIndex = 5;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.label4);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(315, 10);
            this.panel2.Margin = new System.Windows.Forms.Padding(10);
            this.panel2.MinimumSize = new System.Drawing.Size(200, 100);
            this.panel2.Name = "panel2";
            this.panel2.Padding = new System.Windows.Forms.Padding(10);
            this.panel2.Size = new System.Drawing.Size(256, 621);
            this.panel2.TabIndex = 4;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(4, 5);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 24);
            this.label4.TabIndex = 1;
            this.label4.Text = "Help";
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 26.20275F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 23.71134F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panel2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel2.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1164, 641);
            this.tableLayoutPanel2.TabIndex = 5;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Controls.Add(this.tableLayoutPanel4, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 2);
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 3;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53.14136F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 46.85864F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 259F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(285, 621);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel6, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.panel7, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.panel3, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 3;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(283, 190);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // panel6
            // 
            this.panel6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel6.Controls.Add(this.UartPortCombo);
            this.panel6.Controls.Add(this.label5);
            this.panel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel6.Location = new System.Drawing.Point(10, 66);
            this.panel6.Margin = new System.Windows.Forms.Padding(10);
            this.panel6.Name = "panel6";
            this.panel6.Padding = new System.Windows.Forms.Padding(10);
            this.panel6.Size = new System.Drawing.Size(263, 47);
            this.panel6.TabIndex = 4;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label5.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label5.Location = new System.Drawing.Point(6, 10);
            this.label5.Margin = new System.Windows.Forms.Padding(0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(48, 22);
            this.label5.TabIndex = 2;
            this.label5.Text = "Port:";
            // 
            // panel7
            // 
            this.panel7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel7.Controls.Add(this.UartBaudRateCombo);
            this.panel7.Controls.Add(this.label6);
            this.panel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel7.Location = new System.Drawing.Point(10, 133);
            this.panel7.Margin = new System.Windows.Forms.Padding(10);
            this.panel7.Name = "panel7";
            this.panel7.Padding = new System.Windows.Forms.Padding(10);
            this.panel7.Size = new System.Drawing.Size(263, 47);
            this.panel7.TabIndex = 5;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.label6.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label6.Location = new System.Drawing.Point(8, 6);
            this.label6.Margin = new System.Windows.Forms.Padding(0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(46, 36);
            this.label6.TabIndex = 2;
            this.label6.Text = "Baud \r\nRate:\r\n";
            // 
            // panel3
            // 
            this.panel3.Controls.Add(this.tableLayoutPanel5);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(283, 56);
            this.panel3.TabIndex = 6;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 3;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 30F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 35F));
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.ConnectUartBtn, 2, 0);
            this.tableLayoutPanel5.Controls.Add(this.UartRefreshBtn, 1, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 1;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(283, 56);
            this.tableLayoutPanel5.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(10, 10);
            this.label1.Margin = new System.Windows.Forms.Padding(10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 36);
            this.label1.TabIndex = 0;
            this.label1.Text = "UART";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.label3);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(11, 370);
            this.panel5.Margin = new System.Windows.Forms.Padding(10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(263, 240);
            this.panel5.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(3, 5);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(91, 24);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ethernet";
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.label2);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(11, 202);
            this.panel4.Margin = new System.Windows.Forms.Padding(10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(263, 147);
            this.panel4.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(3, 5);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(104, 24);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bluetooth";
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
            this.ClearFilterBtn.Location = new System.Drawing.Point(385, 73);
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
            this.SendBtn.Location = new System.Drawing.Point(488, 10);
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
            this.FilterBtn.Location = new System.Drawing.Point(385, 42);
            this.FilterBtn.Name = "FilterBtn";
            this.FilterBtn.Size = new System.Drawing.Size(97, 30);
            this.FilterBtn.TabIndex = 2;
            this.FilterBtn.Text = "Filtruj";
            this.FilterBtn.TextColor = System.Drawing.Color.Black;
            this.FilterBtn.UseVisualStyleBackColor = false;
            // 
            // UartPortCombo
            // 
            this.UartPortCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UartPortCombo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UartPortCombo.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.UartPortCombo.BorderSize = 1;
            this.UartPortCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.UartPortCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UartPortCombo.ForeColor = System.Drawing.Color.DimGray;
            this.UartPortCombo.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.UartPortCombo.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.UartPortCombo.ListTextColor = System.Drawing.Color.DimGray;
            this.UartPortCombo.Location = new System.Drawing.Point(64, 8);
            this.UartPortCombo.Margin = new System.Windows.Forms.Padding(10);
            this.UartPortCombo.MinimumSize = new System.Drawing.Size(150, 25);
            this.UartPortCombo.Name = "UartPortCombo";
            this.UartPortCombo.Padding = new System.Windows.Forms.Padding(1);
            this.UartPortCombo.Size = new System.Drawing.Size(179, 30);
            this.UartPortCombo.TabIndex = 1;
            this.UartPortCombo.Texts = "Port";
            // 
            // UartBaudRateCombo
            // 
            this.UartBaudRateCombo.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UartBaudRateCombo.BackColor = System.Drawing.Color.WhiteSmoke;
            this.UartBaudRateCombo.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.UartBaudRateCombo.BorderSize = 1;
            this.UartBaudRateCombo.Cursor = System.Windows.Forms.Cursors.Default;
            this.UartBaudRateCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.UartBaudRateCombo.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.UartBaudRateCombo.ForeColor = System.Drawing.Color.DimGray;
            this.UartBaudRateCombo.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.UartBaudRateCombo.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.UartBaudRateCombo.ListTextColor = System.Drawing.Color.DimGray;
            this.UartBaudRateCombo.Location = new System.Drawing.Point(64, 8);
            this.UartBaudRateCombo.Margin = new System.Windows.Forms.Padding(10);
            this.UartBaudRateCombo.MinimumSize = new System.Drawing.Size(150, 25);
            this.UartBaudRateCombo.Name = "UartBaudRateCombo";
            this.UartBaudRateCombo.Padding = new System.Windows.Forms.Padding(1);
            this.UartBaudRateCombo.Size = new System.Drawing.Size(179, 30);
            this.UartBaudRateCombo.TabIndex = 1;
            this.UartBaudRateCombo.Texts = "BaudRate";
            // 
            // ConnectUartBtn
            // 
            this.ConnectUartBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ConnectUartBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ConnectUartBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ConnectUartBtn.BorderRadius = 15;
            this.ConnectUartBtn.BorderSize = 0;
            this.ConnectUartBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectUartBtn.FlatAppearance.BorderSize = 0;
            this.ConnectUartBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectUartBtn.Font = new System.Drawing.Font("Radikal WUT", 10F, System.Drawing.FontStyle.Bold);
            this.ConnectUartBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.ConnectUartBtn.Location = new System.Drawing.Point(193, 10);
            this.ConnectUartBtn.Margin = new System.Windows.Forms.Padding(10);
            this.ConnectUartBtn.Name = "ConnectUartBtn";
            this.ConnectUartBtn.Size = new System.Drawing.Size(80, 36);
            this.ConnectUartBtn.TabIndex = 3;
            this.ConnectUartBtn.Text = "Połącz";
            this.ConnectUartBtn.TextColor = System.Drawing.Color.AliceBlue;
            this.ConnectUartBtn.UseVisualStyleBackColor = false;
            this.ConnectUartBtn.Click += new System.EventHandler(this.ConnectUartBtn_Click);
            // 
            // UartRefreshBtn
            // 
            this.UartRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UartRefreshBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.UartRefreshBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.UartRefreshBtn.BorderRadius = 15;
            this.UartRefreshBtn.BorderSize = 0;
            this.UartRefreshBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UartRefreshBtn.FlatAppearance.BorderSize = 0;
            this.UartRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UartRefreshBtn.Font = new System.Drawing.Font("Radikal WUT", 10F, System.Drawing.FontStyle.Bold);
            this.UartRefreshBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.UartRefreshBtn.Location = new System.Drawing.Point(94, 10);
            this.UartRefreshBtn.Margin = new System.Windows.Forms.Padding(10);
            this.UartRefreshBtn.Name = "UartRefreshBtn";
            this.UartRefreshBtn.Size = new System.Drawing.Size(79, 36);
            this.UartRefreshBtn.TabIndex = 4;
            this.UartRefreshBtn.Text = "Odśwież";
            this.UartRefreshBtn.TextColor = System.Drawing.Color.AliceBlue;
            this.UartRefreshBtn.UseVisualStyleBackColor = false;
            this.UartRefreshBtn.Click += new System.EventHandler(this.UartRefreshBtn_click);
            // 
            // komunikacjaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tableLayoutPanel2);
            this.Name = "komunikacjaForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "komunikacjaForm";
            this.Load += new System.EventHandler(this.komunikacjaForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel6.ResumeLayout(false);
            this.panel6.PerformLayout();
            this.panel7.ResumeLayout(false);
            this.panel7.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.panel5.ResumeLayout(false);
            this.panel5.PerformLayout();
            this.panel4.ResumeLayout(false);
            this.panel4.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TextBox sendTextBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ListBox TerminalBox;
        private CustomControls.CustomButton FilterBtn;
        private CustomControls.CustomButton SendBtn;
        private CustomControls.CustomButton ClearFilterBtn;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private CustomControls.CustomButton ConnectUartBtn;
        private System.Windows.Forms.Label label5;
        private CustomControls.CustomComboBox UartPortCombo;
        private System.Windows.Forms.Panel panel6;
        private System.Windows.Forms.Panel panel7;
        private CustomControls.CustomComboBox UartBaudRateCombo;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.Panel panel3;
        private CustomControls.CustomButton UartRefreshBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
    }
}