namespace HAL062app.moduly.komunikacja
{
    partial class CommunicationForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CommunicationForm));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.TerminalBox = new System.Windows.Forms.ListBox();
            this.tableLayoutPanel9 = new System.Windows.Forms.TableLayoutPanel();
            this.SendBtn = new HAL062app.CustomControls.CustomButton();
            this.sendTextBox = new System.Windows.Forms.TextBox();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.panel4 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel8 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel10 = new System.Windows.Forms.TableLayoutPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.panel11 = new System.Windows.Forms.Panel();
            this.label10 = new System.Windows.Forms.Label();
            this.BluetoothSwitch = new HAL062app.CustomControls.CustomToggleButton();
            this.panel12 = new System.Windows.Forms.Panel();
            this.label11 = new System.Windows.Forms.Label();
            this.BluetoothDevicesComboBox = new HAL062app.CustomControls.CustomComboBox();
            this.tableLayoutPanel11 = new System.Windows.Forms.TableLayoutPanel();
            this.BluetoothRefreshBtn = new HAL062app.CustomControls.CustomButton();
            this.ConnectBluetoothBtn = new HAL062app.CustomControls.CustomButton();
            this.panel5 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.EthernetConnectBtn = new HAL062app.CustomControls.CustomButton();
            this.panel10 = new System.Windows.Forms.Panel();
            this.EthernetPort = new System.Windows.Forms.NumericUpDown();
            this.label9 = new System.Windows.Forms.Label();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.panel14 = new System.Windows.Forms.Panel();
            this.label13 = new System.Windows.Forms.Label();
            this.EthernetSwitch = new HAL062app.CustomControls.CustomToggleButton();
            this.label3 = new System.Windows.Forms.Label();
            this.panel9 = new System.Windows.Forms.Panel();
            this.IPtextbox = new System.Windows.Forms.TextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.panel2 = new System.Windows.Forms.Panel();
            this.tableLayoutPanel14 = new System.Windows.Forms.TableLayoutPanel();
            this.richTextBox1 = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel15 = new System.Windows.Forms.TableLayoutPanel();
            this.label4 = new System.Windows.Forms.Label();
            this.ClearNotebookBtn = new HAL062app.CustomControls.CustomButton();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.Buffor_Label = new System.Windows.Forms.Label();
            this.ReceivedMsg_label = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.SentMsg_label = new System.Windows.Forms.Label();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel9.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panel4.SuspendLayout();
            this.tableLayoutPanel8.SuspendLayout();
            this.tableLayoutPanel10.SuspendLayout();
            this.panel11.SuspendLayout();
            this.panel12.SuspendLayout();
            this.tableLayoutPanel11.SuspendLayout();
            this.panel5.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.panel10.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EthernetPort)).BeginInit();
            this.tableLayoutPanel7.SuspendLayout();
            this.panel14.SuspendLayout();
            this.panel9.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.panel2.SuspendLayout();
            this.tableLayoutPanel14.SuspendLayout();
            this.tableLayoutPanel15.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.TerminalBox, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel9, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(620, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 70F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(544, 641);
            this.tableLayoutPanel1.TabIndex = 3;
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
            this.TerminalBox.Size = new System.Drawing.Size(522, 548);
            this.TerminalBox.TabIndex = 5;
            this.TerminalBox.SelectedIndexChanged += new System.EventHandler(this.TerminalBox_SelectedIndexChanged);
            // 
            // tableLayoutPanel9
            // 
            this.tableLayoutPanel9.ColumnCount = 2;
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 80F));
            this.tableLayoutPanel9.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel9.Controls.Add(this.SendBtn, 1, 0);
            this.tableLayoutPanel9.Controls.Add(this.sendTextBox, 0, 0);
            this.tableLayoutPanel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel9.Location = new System.Drawing.Point(14, 563);
            this.tableLayoutPanel9.Name = "tableLayoutPanel9";
            this.tableLayoutPanel9.RowCount = 1;
            this.tableLayoutPanel9.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel9.Size = new System.Drawing.Size(516, 64);
            this.tableLayoutPanel9.TabIndex = 8;
            // 
            // SendBtn
            // 
            this.SendBtn.BackColor = System.Drawing.Color.Purple;
            this.SendBtn.BackgroundColor = System.Drawing.Color.Purple;
            this.SendBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.SendBtn.BorderRadius = 10;
            this.SendBtn.BorderSize = 0;
            this.SendBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SendBtn.FlatAppearance.BorderSize = 0;
            this.SendBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SendBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SendBtn.ForeColor = System.Drawing.Color.White;
            this.SendBtn.Location = new System.Drawing.Point(415, 3);
            this.SendBtn.Name = "SendBtn";
            this.SendBtn.Size = new System.Drawing.Size(98, 58);
            this.SendBtn.TabIndex = 7;
            this.SendBtn.Text = "Wyślij";
            this.SendBtn.TextColor = System.Drawing.Color.White;
            this.SendBtn.UseVisualStyleBackColor = false;
            this.SendBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.SendBtn_Click);
            // 
            // sendTextBox
            // 
            this.sendTextBox.AllowDrop = true;
            this.sendTextBox.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.sendTextBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.sendTextBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.sendTextBox.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.sendTextBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.sendTextBox.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.sendTextBox.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.sendTextBox.Location = new System.Drawing.Point(3, 17);
            this.sendTextBox.MinimumSize = new System.Drawing.Size(200, 29);
            this.sendTextBox.Name = "sendTextBox";
            this.sendTextBox.Size = new System.Drawing.Size(406, 29);
            this.sendTextBox.TabIndex = 6;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 3;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 310F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel1, 2, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 1, 0);
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
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.panel4, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panel5, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(10, 10);
            this.tableLayoutPanel3.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 53F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 47F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(290, 621);
            this.tableLayoutPanel3.TabIndex = 5;
            // 
            // panel4
            // 
            this.panel4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel4.Controls.Add(this.tableLayoutPanel8);
            this.panel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel4.Location = new System.Drawing.Point(11, 339);
            this.panel4.Margin = new System.Windows.Forms.Padding(10);
            this.panel4.Name = "panel4";
            this.panel4.Size = new System.Drawing.Size(268, 271);
            this.panel4.TabIndex = 1;
            // 
            // tableLayoutPanel8
            // 
            this.tableLayoutPanel8.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel8.ColumnCount = 1;
            this.tableLayoutPanel8.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel10, 0, 0);
            this.tableLayoutPanel8.Controls.Add(this.panel12, 0, 1);
            this.tableLayoutPanel8.Controls.Add(this.tableLayoutPanel11, 0, 2);
            this.tableLayoutPanel8.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel8.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel8.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel8.Name = "tableLayoutPanel8";
            this.tableLayoutPanel8.RowCount = 3;
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel8.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel8.Size = new System.Drawing.Size(266, 269);
            this.tableLayoutPanel8.TabIndex = 7;
            // 
            // tableLayoutPanel10
            // 
            this.tableLayoutPanel10.ColumnCount = 2;
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel10.Controls.Add(this.label2, 0, 0);
            this.tableLayoutPanel10.Controls.Add(this.panel11, 1, 0);
            this.tableLayoutPanel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel10.Location = new System.Drawing.Point(4, 4);
            this.tableLayoutPanel10.Name = "tableLayoutPanel10";
            this.tableLayoutPanel10.RowCount = 1;
            this.tableLayoutPanel10.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel10.Size = new System.Drawing.Size(258, 46);
            this.tableLayoutPanel10.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label2.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Margin = new System.Windows.Forms.Padding(0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(129, 52);
            this.label2.TabIndex = 0;
            this.label2.Text = "Bluetooth";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel11
            // 
            this.panel11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel11.Controls.Add(this.label10);
            this.panel11.Controls.Add(this.BluetoothSwitch);
            this.panel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel11.Location = new System.Drawing.Point(129, 0);
            this.panel11.Margin = new System.Windows.Forms.Padding(0);
            this.panel11.Name = "panel11";
            this.panel11.Size = new System.Drawing.Size(129, 52);
            this.panel11.TabIndex = 0;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label10.Location = new System.Drawing.Point(80, 14);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(30, 20);
            this.label10.TabIndex = 3;
            this.label10.Text = "On";
            // 
            // BluetoothSwitch
            // 
            this.BluetoothSwitch.Location = new System.Drawing.Point(12, 12);
            this.BluetoothSwitch.Margin = new System.Windows.Forms.Padding(10);
            this.BluetoothSwitch.MinimumSize = new System.Drawing.Size(45, 22);
            this.BluetoothSwitch.Name = "BluetoothSwitch";
            this.BluetoothSwitch.Size = new System.Drawing.Size(55, 22);
            this.BluetoothSwitch.TabIndex = 1;
            this.BluetoothSwitch.Text = "customToggleButton1";
            this.BluetoothSwitch.UseVisualStyleBackColor = true;
            this.BluetoothSwitch.CheckedChanged += new System.EventHandler(this.BluetoothSwitch_CheckedChanged);
            // 
            // panel12
            // 
            this.panel12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel12.Controls.Add(this.label11);
            this.panel12.Controls.Add(this.BluetoothDevicesComboBox);
            this.panel12.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel12.Location = new System.Drawing.Point(11, 64);
            this.panel12.Margin = new System.Windows.Forms.Padding(10);
            this.panel12.Name = "panel12";
            this.panel12.Padding = new System.Windows.Forms.Padding(10);
            this.panel12.Size = new System.Drawing.Size(244, 52);
            this.panel12.TabIndex = 8;
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label11.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label11.Location = new System.Drawing.Point(11, 14);
            this.label11.Margin = new System.Windows.Forms.Padding(10);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(48, 22);
            this.label11.TabIndex = 7;
            this.label11.Text = "Port:";
            // 
            // BluetoothDevicesComboBox
            // 
            this.BluetoothDevicesComboBox.BackColor = System.Drawing.Color.WhiteSmoke;
            this.BluetoothDevicesComboBox.BorderColor = System.Drawing.Color.MediumSlateBlue;
            this.BluetoothDevicesComboBox.BorderSize = 1;
            this.BluetoothDevicesComboBox.Dock = System.Windows.Forms.DockStyle.Right;
            this.BluetoothDevicesComboBox.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDown;
            this.BluetoothDevicesComboBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F);
            this.BluetoothDevicesComboBox.ForeColor = System.Drawing.Color.DimGray;
            this.BluetoothDevicesComboBox.IconColor = System.Drawing.Color.MediumSlateBlue;
            this.BluetoothDevicesComboBox.ListBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(230)))), ((int)(((byte)(228)))), ((int)(((byte)(245)))));
            this.BluetoothDevicesComboBox.ListTextColor = System.Drawing.Color.DimGray;
            this.BluetoothDevicesComboBox.Location = new System.Drawing.Point(62, 10);
            this.BluetoothDevicesComboBox.Margin = new System.Windows.Forms.Padding(0);
            this.BluetoothDevicesComboBox.MinimumSize = new System.Drawing.Size(150, 30);
            this.BluetoothDevicesComboBox.Name = "BluetoothDevicesComboBox";
            this.BluetoothDevicesComboBox.Padding = new System.Windows.Forms.Padding(1);
            this.BluetoothDevicesComboBox.Size = new System.Drawing.Size(170, 30);
            this.BluetoothDevicesComboBox.TabIndex = 6;
            this.BluetoothDevicesComboBox.Texts = "";
            // 
            // tableLayoutPanel11
            // 
            this.tableLayoutPanel11.ColumnCount = 2;
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel11.Controls.Add(this.BluetoothRefreshBtn, 0, 0);
            this.tableLayoutPanel11.Controls.Add(this.ConnectBluetoothBtn, 1, 0);
            this.tableLayoutPanel11.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel11.Location = new System.Drawing.Point(1, 127);
            this.tableLayoutPanel11.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel11.Name = "tableLayoutPanel11";
            this.tableLayoutPanel11.RowCount = 1;
            this.tableLayoutPanel11.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel11.Size = new System.Drawing.Size(264, 141);
            this.tableLayoutPanel11.TabIndex = 10;
            // 
            // BluetoothRefreshBtn
            // 
            this.BluetoothRefreshBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BluetoothRefreshBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(192)))), ((int)(((byte)(0)))), ((int)(((byte)(0)))));
            this.BluetoothRefreshBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.BluetoothRefreshBtn.BorderRadius = 10;
            this.BluetoothRefreshBtn.BorderSize = 0;
            this.BluetoothRefreshBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.BluetoothRefreshBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.BluetoothRefreshBtn.FlatAppearance.BorderSize = 0;
            this.BluetoothRefreshBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.BluetoothRefreshBtn.Font = new System.Drawing.Font("Radikal WUT", 10F, System.Drawing.FontStyle.Bold);
            this.BluetoothRefreshBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.BluetoothRefreshBtn.Location = new System.Drawing.Point(4, 10);
            this.BluetoothRefreshBtn.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.BluetoothRefreshBtn.Name = "BluetoothRefreshBtn";
            this.BluetoothRefreshBtn.Size = new System.Drawing.Size(124, 121);
            this.BluetoothRefreshBtn.TabIndex = 5;
            this.BluetoothRefreshBtn.Text = "Odśwież";
            this.BluetoothRefreshBtn.TextColor = System.Drawing.Color.AliceBlue;
            this.BluetoothRefreshBtn.UseVisualStyleBackColor = false;
            this.BluetoothRefreshBtn.Click += new System.EventHandler(this.BluetoothRefreshBtn_Click);
            // 
            // ConnectBluetoothBtn
            // 
            this.ConnectBluetoothBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ConnectBluetoothBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.ConnectBluetoothBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.ConnectBluetoothBtn.BorderRadius = 10;
            this.ConnectBluetoothBtn.BorderSize = 0;
            this.ConnectBluetoothBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ConnectBluetoothBtn.FlatAppearance.BorderSize = 0;
            this.ConnectBluetoothBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ConnectBluetoothBtn.Font = new System.Drawing.Font("Radikal WUT", 10F, System.Drawing.FontStyle.Bold);
            this.ConnectBluetoothBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.ConnectBluetoothBtn.Location = new System.Drawing.Point(136, 10);
            this.ConnectBluetoothBtn.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.ConnectBluetoothBtn.Name = "ConnectBluetoothBtn";
            this.ConnectBluetoothBtn.Size = new System.Drawing.Size(124, 121);
            this.ConnectBluetoothBtn.TabIndex = 4;
            this.ConnectBluetoothBtn.Text = "Połącz";
            this.ConnectBluetoothBtn.TextColor = System.Drawing.Color.AliceBlue;
            this.ConnectBluetoothBtn.UseVisualStyleBackColor = false;
            this.ConnectBluetoothBtn.Click += new System.EventHandler(this.ConnectBluetoothBtn_Click);
            // 
            // panel5
            // 
            this.panel5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel5.Controls.Add(this.tableLayoutPanel6);
            this.panel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel5.Location = new System.Drawing.Point(11, 11);
            this.panel5.Margin = new System.Windows.Forms.Padding(10);
            this.panel5.Name = "panel5";
            this.panel5.Size = new System.Drawing.Size(268, 307);
            this.panel5.TabIndex = 2;
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel6.Controls.Add(this.EthernetConnectBtn, 0, 3);
            this.tableLayoutPanel6.Controls.Add(this.panel10, 0, 2);
            this.tableLayoutPanel6.Controls.Add(this.tableLayoutPanel7, 0, 0);
            this.tableLayoutPanel6.Controls.Add(this.panel9, 0, 1);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel6.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 4;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 52F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 72F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(266, 305);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // EthernetConnectBtn
            // 
            this.EthernetConnectBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.EthernetConnectBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(192)))), ((int)(((byte)(0)))));
            this.EthernetConnectBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(255)))), ((int)(((byte)(128)))));
            this.EthernetConnectBtn.BorderRadius = 10;
            this.EthernetConnectBtn.BorderSize = 0;
            this.EthernetConnectBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.EthernetConnectBtn.FlatAppearance.BorderSize = 0;
            this.EthernetConnectBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.EthernetConnectBtn.Font = new System.Drawing.Font("Radikal WUT", 10F, System.Drawing.FontStyle.Bold);
            this.EthernetConnectBtn.ForeColor = System.Drawing.Color.AliceBlue;
            this.EthernetConnectBtn.Location = new System.Drawing.Point(5, 210);
            this.EthernetConnectBtn.Margin = new System.Windows.Forms.Padding(4, 10, 4, 10);
            this.EthernetConnectBtn.Name = "EthernetConnectBtn";
            this.EthernetConnectBtn.Size = new System.Drawing.Size(256, 84);
            this.EthernetConnectBtn.TabIndex = 5;
            this.EthernetConnectBtn.Text = "Połącz";
            this.EthernetConnectBtn.TextColor = System.Drawing.Color.AliceBlue;
            this.EthernetConnectBtn.UseVisualStyleBackColor = false;
            this.EthernetConnectBtn.MouseClick += new System.Windows.Forms.MouseEventHandler(this.EthernetConnectBtn_Click);
            // 
            // panel10
            // 
            this.panel10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel10.Controls.Add(this.EthernetPort);
            this.panel10.Controls.Add(this.label9);
            this.panel10.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel10.Location = new System.Drawing.Point(11, 137);
            this.panel10.Margin = new System.Windows.Forms.Padding(10);
            this.panel10.Name = "panel10";
            this.panel10.Padding = new System.Windows.Forms.Padding(10);
            this.panel10.Size = new System.Drawing.Size(244, 52);
            this.panel10.TabIndex = 2;
            // 
            // EthernetPort
            // 
            this.EthernetPort.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.EthernetPort.Location = new System.Drawing.Point(72, 11);
            this.EthernetPort.Margin = new System.Windows.Forms.Padding(0);
            this.EthernetPort.Maximum = new decimal(new int[] {
            1000000000,
            0,
            0,
            0});
            this.EthernetPort.Name = "EthernetPort";
            this.EthernetPort.Size = new System.Drawing.Size(162, 29);
            this.EthernetPort.TabIndex = 5;
            this.EthernetPort.Value = new decimal(new int[] {
            8888,
            0,
            0,
            0});
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label9.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label9.Location = new System.Drawing.Point(17, 14);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(48, 22);
            this.label9.TabIndex = 0;
            this.label9.Text = "Port:";
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel7.Controls.Add(this.panel14, 0, 0);
            this.tableLayoutPanel7.Controls.Add(this.label3, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(264, 52);
            this.tableLayoutPanel7.TabIndex = 0;
            // 
            // panel14
            // 
            this.panel14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.panel14.Controls.Add(this.label13);
            this.panel14.Controls.Add(this.EthernetSwitch);
            this.panel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel14.Location = new System.Drawing.Point(132, 0);
            this.panel14.Margin = new System.Windows.Forms.Padding(0);
            this.panel14.Name = "panel14";
            this.panel14.Size = new System.Drawing.Size(132, 52);
            this.panel14.TabIndex = 1;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label13.Location = new System.Drawing.Point(80, 14);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(30, 20);
            this.label13.TabIndex = 3;
            this.label13.Text = "On";
            // 
            // EthernetSwitch
            // 
            this.EthernetSwitch.Location = new System.Drawing.Point(12, 12);
            this.EthernetSwitch.Margin = new System.Windows.Forms.Padding(10);
            this.EthernetSwitch.MinimumSize = new System.Drawing.Size(45, 22);
            this.EthernetSwitch.Name = "EthernetSwitch";
            this.EthernetSwitch.Size = new System.Drawing.Size(55, 22);
            this.EthernetSwitch.TabIndex = 1;
            this.EthernetSwitch.Text = "customToggleButton1";
            this.EthernetSwitch.UseVisualStyleBackColor = true;
            this.EthernetSwitch.CheckedChanged += new System.EventHandler(this.EthernetSwitch_CheckedChanged);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label3.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label3.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label3.Location = new System.Drawing.Point(2, 10);
            this.label3.Margin = new System.Windows.Forms.Padding(2, 10, 2, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(128, 32);
            this.label3.TabIndex = 0;
            this.label3.Text = "Ethernet";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // panel9
            // 
            this.panel9.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel9.Controls.Add(this.IPtextbox);
            this.panel9.Controls.Add(this.label8);
            this.panel9.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel9.Location = new System.Drawing.Point(11, 64);
            this.panel9.Margin = new System.Windows.Forms.Padding(10);
            this.panel9.Name = "panel9";
            this.panel9.Padding = new System.Windows.Forms.Padding(10);
            this.panel9.Size = new System.Drawing.Size(244, 52);
            this.panel9.TabIndex = 1;
            // 
            // IPtextbox
            // 
            this.IPtextbox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.IPtextbox.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F);
            this.IPtextbox.Location = new System.Drawing.Point(72, 10);
            this.IPtextbox.Margin = new System.Windows.Forms.Padding(0);
            this.IPtextbox.Name = "IPtextbox";
            this.IPtextbox.Size = new System.Drawing.Size(160, 29);
            this.IPtextbox.TabIndex = 1;
            this.IPtextbox.Text = "192.168.1.98";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.label8.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label8.Location = new System.Drawing.Point(17, 14);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(31, 22);
            this.label8.TabIndex = 0;
            this.label8.Text = "IP:";
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.Controls.Add(this.panel2, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.tableLayoutPanel5, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(320, 10);
            this.tableLayoutPanel4.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 2;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 38.3871F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 61.6129F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(290, 621);
            this.tableLayoutPanel4.TabIndex = 6;
            // 
            // panel2
            // 
            this.panel2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel2.Controls.Add(this.tableLayoutPanel14);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(11, 249);
            this.panel2.Margin = new System.Windows.Forms.Padding(10);
            this.panel2.MinimumSize = new System.Drawing.Size(200, 100);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(268, 361);
            this.panel2.TabIndex = 4;
            // 
            // tableLayoutPanel14
            // 
            this.tableLayoutPanel14.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel14.ColumnCount = 1;
            this.tableLayoutPanel14.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Controls.Add(this.richTextBox1, 0, 1);
            this.tableLayoutPanel14.Controls.Add(this.tableLayoutPanel15, 0, 0);
            this.tableLayoutPanel14.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel14.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel14.Name = "tableLayoutPanel14";
            this.tableLayoutPanel14.RowCount = 2;
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 42F));
            this.tableLayoutPanel14.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel14.Size = new System.Drawing.Size(266, 359);
            this.tableLayoutPanel14.TabIndex = 0;
            // 
            // richTextBox1
            // 
            this.richTextBox1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.richTextBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.richTextBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.richTextBox1.ForeColor = System.Drawing.Color.WhiteSmoke;
            this.richTextBox1.Location = new System.Drawing.Point(1, 44);
            this.richTextBox1.Margin = new System.Windows.Forms.Padding(0);
            this.richTextBox1.Name = "richTextBox1";
            this.richTextBox1.Size = new System.Drawing.Size(264, 314);
            this.richTextBox1.TabIndex = 2;
            this.richTextBox1.Text = resources.GetString("richTextBox1.Text");
            // 
            // tableLayoutPanel15
            // 
            this.tableLayoutPanel15.ColumnCount = 2;
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel15.Controls.Add(this.label4, 0, 0);
            this.tableLayoutPanel15.Controls.Add(this.ClearNotebookBtn, 1, 0);
            this.tableLayoutPanel15.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel15.Location = new System.Drawing.Point(1, 1);
            this.tableLayoutPanel15.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel15.Name = "tableLayoutPanel15";
            this.tableLayoutPanel15.RowCount = 1;
            this.tableLayoutPanel15.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel15.Size = new System.Drawing.Size(264, 42);
            this.tableLayoutPanel15.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label4.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label4.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label4.Location = new System.Drawing.Point(0, 0);
            this.label4.Margin = new System.Windows.Forms.Padding(0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(132, 42);
            this.label4.TabIndex = 1;
            this.label4.Text = "Notatnik";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // ClearNotebookBtn
            // 
            this.ClearNotebookBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(94)))), ((int)(((byte)(254)))));
            this.ClearNotebookBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(94)))), ((int)(((byte)(254)))));
            this.ClearNotebookBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.ClearNotebookBtn.BorderRadius = 5;
            this.ClearNotebookBtn.BorderSize = 0;
            this.ClearNotebookBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ClearNotebookBtn.FlatAppearance.BorderSize = 0;
            this.ClearNotebookBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ClearNotebookBtn.Font = new System.Drawing.Font("Radikal WUT", 11F, System.Drawing.FontStyle.Bold);
            this.ClearNotebookBtn.ForeColor = System.Drawing.Color.White;
            this.ClearNotebookBtn.Location = new System.Drawing.Point(132, 0);
            this.ClearNotebookBtn.Margin = new System.Windows.Forms.Padding(0);
            this.ClearNotebookBtn.Name = "ClearNotebookBtn";
            this.ClearNotebookBtn.Size = new System.Drawing.Size(132, 42);
            this.ClearNotebookBtn.TabIndex = 2;
            this.ClearNotebookBtn.Text = "Clear";
            this.ClearNotebookBtn.TextColor = System.Drawing.Color.White;
            this.ClearNotebookBtn.UseVisualStyleBackColor = false;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.CellBorderStyle = System.Windows.Forms.TableLayoutPanelCellBorderStyle.Single;
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel5.Controls.Add(this.Buffor_Label, 0, 3);
            this.tableLayoutPanel5.Controls.Add(this.ReceivedMsg_label, 0, 2);
            this.tableLayoutPanel5.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel5.Controls.Add(this.SentMsg_label, 0, 1);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(11, 11);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 4;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 33.33333F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(268, 217);
            this.tableLayoutPanel5.TabIndex = 5;
            // 
            // Buffor_Label
            // 
            this.Buffor_Label.AutoSize = true;
            this.Buffor_Label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.Buffor_Label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.Buffor_Label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.Buffor_Label.Location = new System.Drawing.Point(4, 162);
            this.Buffor_Label.Name = "Buffor_Label";
            this.Buffor_Label.Size = new System.Drawing.Size(260, 54);
            this.Buffor_Label.TabIndex = 4;
            this.Buffor_Label.Text = "Zapełnienie buffora: 0";
            this.Buffor_Label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ReceivedMsg_label
            // 
            this.ReceivedMsg_label.AutoSize = true;
            this.ReceivedMsg_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ReceivedMsg_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.ReceivedMsg_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.ReceivedMsg_label.Location = new System.Drawing.Point(4, 107);
            this.ReceivedMsg_label.Name = "ReceivedMsg_label";
            this.ReceivedMsg_label.Size = new System.Drawing.Size(260, 54);
            this.ReceivedMsg_label.TabIndex = 3;
            this.ReceivedMsg_label.Text = "Odebrano: 0 ramek";
            this.ReceivedMsg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Radikal WUT", 15F, System.Drawing.FontStyle.Bold);
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(3, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 10, 2, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(262, 30);
            this.label1.TabIndex = 1;
            this.label1.Text = "Statystyki";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SentMsg_label
            // 
            this.SentMsg_label.AutoSize = true;
            this.SentMsg_label.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SentMsg_label.Font = new System.Drawing.Font("Microsoft Sans Serif", 13F);
            this.SentMsg_label.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.SentMsg_label.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.SentMsg_label.Location = new System.Drawing.Point(4, 52);
            this.SentMsg_label.Name = "SentMsg_label";
            this.SentMsg_label.Size = new System.Drawing.Size(260, 54);
            this.SentMsg_label.TabIndex = 2;
            this.SentMsg_label.Text = "Wysłano: 0 ramek";
            this.SentMsg_label.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // KomunikacjaForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tableLayoutPanel2);
            this.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Name = "KomunikacjaForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.Text = "komunikacjaForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.komunikacjaForm_FormClosing);
            this.Load += new System.EventHandler(this.komunikacjaForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel9.ResumeLayout(false);
            this.tableLayoutPanel9.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel2.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panel4.ResumeLayout(false);
            this.tableLayoutPanel8.ResumeLayout(false);
            this.tableLayoutPanel10.ResumeLayout(false);
            this.tableLayoutPanel10.PerformLayout();
            this.panel11.ResumeLayout(false);
            this.panel11.PerformLayout();
            this.panel12.ResumeLayout(false);
            this.panel12.PerformLayout();
            this.tableLayoutPanel11.ResumeLayout(false);
            this.panel5.ResumeLayout(false);
            this.tableLayoutPanel6.ResumeLayout(false);
            this.panel10.ResumeLayout(false);
            this.panel10.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.EthernetPort)).EndInit();
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.panel14.ResumeLayout(false);
            this.panel14.PerformLayout();
            this.panel9.ResumeLayout(false);
            this.panel9.PerformLayout();
            this.tableLayoutPanel4.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.tableLayoutPanel14.ResumeLayout(false);
            this.tableLayoutPanel15.ResumeLayout(false);
            this.tableLayoutPanel15.PerformLayout();
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel5.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.ListBox TerminalBox;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.Panel panel5;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Panel panel4;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private System.Windows.Forms.Panel panel10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Panel panel9;
        private System.Windows.Forms.TextBox IPtextbox;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label10;
        private CustomControls.CustomToggleButton BluetoothSwitch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel8;
        private CustomControls.CustomComboBox BluetoothDevicesComboBox;
        private System.Windows.Forms.Panel panel11;
        private CustomControls.CustomButton ConnectBluetoothBtn;
        private System.Windows.Forms.Panel panel12;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Panel panel14;
        private System.Windows.Forms.Label label13;
        private CustomControls.CustomToggleButton EthernetSwitch;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel11;
        private CustomControls.CustomButton BluetoothRefreshBtn;
        private System.Windows.Forms.NumericUpDown EthernetPort;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel14;
        private System.Windows.Forms.RichTextBox richTextBox1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel15;
        private CustomControls.CustomButton ClearNotebookBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label Buffor_Label;
        private System.Windows.Forms.Label ReceivedMsg_label;
        private System.Windows.Forms.Label SentMsg_label;
        private CustomControls.CustomButton EthernetConnectBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel9;
        private CustomControls.CustomButton SendBtn;
        private System.Windows.Forms.TextBox sendTextBox;
    }
}