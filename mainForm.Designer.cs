namespace HAL062app
{
    partial class mainForm
    {
        /// <summary>
        /// Wymagana zmienna projektanta.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Wyczyść wszystkie używane zasoby.
        /// </summary>
        /// <param name="disposing">prawda, jeżeli zarządzane zasoby powinny zostać zlikwidowane; Fałsz w przeciwnym wypadku.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Kod generowany przez Projektanta formularzy systemu Windows

        /// <summary>
        /// Metoda wymagana do obsługi projektanta — nie należy modyfikować
        /// jej zawartości w edytorze kodu.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            this.ContextPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel4 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel5 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GamePadStatusLabel = new System.Windows.Forms.Label();
            this.tableLayoutPanel6 = new System.Windows.Forms.TableLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.LogoPictureBox = new System.Windows.Forms.PictureBox();
            this.SidePanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel7 = new System.Windows.Forms.TableLayoutPanel();
            this.SandboxBtn = new HAL062app.CustomControls.CustomButton();
            this.LaboratoryBtn = new HAL062app.CustomControls.CustomButton();
            this.ManipulatorBtn = new HAL062app.CustomControls.CustomButton();
            this.DriveBtn = new HAL062app.CustomControls.CustomButton();
            this.CommunicationBtn = new HAL062app.CustomControls.CustomButton();
            this.NightModeToggle = new HAL062app.CustomControls.CustomToggleButton();
            this.UnpinBtn = new HAL062app.CustomControls.CustomButton();
            this.ResetViewBtn = new HAL062app.CustomControls.CustomButton();
            this.tableLayoutPanel2.SuspendLayout();
            this.tableLayoutPanel4.SuspendLayout();
            this.tableLayoutPanel5.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel6.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).BeginInit();
            this.SidePanel.SuspendLayout();
            this.tableLayoutPanel7.SuspendLayout();
            this.SuspendLayout();
            // 
            // ContextPanel
            // 
            this.ContextPanel.AutoSize = true;
            this.ContextPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ContextPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ContextPanel.Location = new System.Drawing.Point(360, 10);
            this.ContextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContextPanel.Name = "ContextPanel";
            this.ContextPanel.Size = new System.Drawing.Size(1214, 841);
            this.ContextPanel.TabIndex = 1;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel4, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.tableLayoutPanel5, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.LogoPictureBox, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 59.36235F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 40.63765F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(350, 841);
            this.tableLayoutPanel2.TabIndex = 0;
            // 
            // tableLayoutPanel4
            // 
            this.tableLayoutPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel4.ColumnCount = 1;
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel4.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel4.Controls.Add(this.SandboxBtn, 0, 4);
            this.tableLayoutPanel4.Controls.Add(this.LaboratoryBtn, 0, 3);
            this.tableLayoutPanel4.Controls.Add(this.ManipulatorBtn, 0, 2);
            this.tableLayoutPanel4.Controls.Add(this.DriveBtn, 0, 1);
            this.tableLayoutPanel4.Controls.Add(this.CommunicationBtn, 0, 0);
            this.tableLayoutPanel4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel4.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel4.Name = "tableLayoutPanel4";
            this.tableLayoutPanel4.RowCount = 5;
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 20F));
            this.tableLayoutPanel4.Size = new System.Drawing.Size(344, 433);
            this.tableLayoutPanel4.TabIndex = 1;
            // 
            // tableLayoutPanel5
            // 
            this.tableLayoutPanel5.ColumnCount = 1;
            this.tableLayoutPanel5.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel1, 0, 1);
            this.tableLayoutPanel5.Controls.Add(this.tableLayoutPanel3, 0, 0);
            this.tableLayoutPanel5.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel5.Location = new System.Drawing.Point(0, 539);
            this.tableLayoutPanel5.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel5.Name = "tableLayoutPanel5";
            this.tableLayoutPanel5.RowCount = 2;
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 37.91822F));
            this.tableLayoutPanel5.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 62.08178F));
            this.tableLayoutPanel5.Size = new System.Drawing.Size(350, 302);
            this.tableLayoutPanel5.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GamePadStatusLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel6, 1, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 114);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(350, 188);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GamePadStatusLabel
            // 
            this.GamePadStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GamePadStatusLabel.AutoSize = true;
            this.GamePadStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GamePadStatusLabel.ForeColor = System.Drawing.Color.White;
            this.GamePadStatusLabel.Location = new System.Drawing.Point(9, 32);
            this.GamePadStatusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.GamePadStatusLabel.Name = "GamePadStatusLabel";
            this.GamePadStatusLabel.Size = new System.Drawing.Size(157, 29);
            this.GamePadStatusLabel.TabIndex = 0;
            this.GamePadStatusLabel.Text = "Gamepad: off";
            // 
            // tableLayoutPanel6
            // 
            this.tableLayoutPanel6.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel6.ColumnCount = 1;
            this.tableLayoutPanel6.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Controls.Add(this.NightModeToggle, 0, 1);
            this.tableLayoutPanel6.Controls.Add(this.label1, 0, 0);
            this.tableLayoutPanel6.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tableLayoutPanel6.Location = new System.Drawing.Point(178, 3);
            this.tableLayoutPanel6.Name = "tableLayoutPanel6";
            this.tableLayoutPanel6.RowCount = 2;
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel6.Size = new System.Drawing.Size(169, 88);
            this.tableLayoutPanel6.TabIndex = 2;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.Transparent;
            this.label1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlLightLight;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(163, 44);
            this.label1.TabIndex = 1;
            this.label1.Text = "Tryb nocny";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel3.ColumnCount = 2;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 49.99999F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50.00001F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.UnpinBtn, 1, 0);
            this.tableLayoutPanel3.Controls.Add(this.ResetViewBtn, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 3);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 1;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(344, 108);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // LogoPictureBox
            // 
            this.LogoPictureBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LogoPictureBox.Image = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.Image")));
            this.LogoPictureBox.InitialImage = ((System.Drawing.Image)(resources.GetObject("LogoPictureBox.InitialImage")));
            this.LogoPictureBox.Location = new System.Drawing.Point(10, 10);
            this.LogoPictureBox.Margin = new System.Windows.Forms.Padding(10);
            this.LogoPictureBox.Name = "LogoPictureBox";
            this.LogoPictureBox.Size = new System.Drawing.Size(330, 80);
            this.LogoPictureBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.LogoPictureBox.TabIndex = 3;
            this.LogoPictureBox.TabStop = false;
            this.LogoPictureBox.WaitOnLoad = true;
            // 
            // SidePanel
            // 
            this.SidePanel.AutoSize = true;
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.SidePanel.Controls.Add(this.tableLayoutPanel2);
            this.SidePanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SidePanel.Location = new System.Drawing.Point(10, 10);
            this.SidePanel.Margin = new System.Windows.Forms.Padding(0);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(350, 841);
            this.SidePanel.TabIndex = 0;
            // 
            // tableLayoutPanel7
            // 
            this.tableLayoutPanel7.ColumnCount = 2;
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 350F));
            this.tableLayoutPanel7.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Controls.Add(this.ContextPanel, 1, 0);
            this.tableLayoutPanel7.Controls.Add(this.SidePanel, 0, 0);
            this.tableLayoutPanel7.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel7.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel7.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel7.Name = "tableLayoutPanel7";
            this.tableLayoutPanel7.Padding = new System.Windows.Forms.Padding(10);
            this.tableLayoutPanel7.RowCount = 1;
            this.tableLayoutPanel7.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel7.Size = new System.Drawing.Size(1584, 861);
            this.tableLayoutPanel7.TabIndex = 2;
            // 
            // SandboxBtn
            // 
            this.SandboxBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.SandboxBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.SandboxBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.SandboxBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.SandboxBtn.BorderRadius = 15;
            this.SandboxBtn.BorderSize = 0;
            this.SandboxBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Default;
            this.SandboxBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.SandboxBtn.FlatAppearance.BorderSize = 0;
            this.SandboxBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.SandboxBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.SandboxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.SandboxBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.SandboxBtn.ForeColor = System.Drawing.Color.White;
            this.SandboxBtn.Location = new System.Drawing.Point(10, 354);
            this.SandboxBtn.Margin = new System.Windows.Forms.Padding(10);
            this.SandboxBtn.Name = "SandboxBtn";
            this.SandboxBtn.Size = new System.Drawing.Size(324, 69);
            this.SandboxBtn.TabIndex = 4;
            this.SandboxBtn.Text = "Tryb testowy";
            this.SandboxBtn.TextColor = System.Drawing.Color.White;
            this.SandboxBtn.UseVisualStyleBackColor = false;
            this.SandboxBtn.Click += new System.EventHandler(this.SandboxBtn_Click);
            // 
            // LaboratoryBtn
            // 
            this.LaboratoryBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.LaboratoryBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.LaboratoryBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.LaboratoryBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.LaboratoryBtn.BorderRadius = 15;
            this.LaboratoryBtn.BorderSize = 0;
            this.LaboratoryBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Default;
            this.LaboratoryBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.LaboratoryBtn.FlatAppearance.BorderSize = 0;
            this.LaboratoryBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.LaboratoryBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.LaboratoryBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaboratoryBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.LaboratoryBtn.ForeColor = System.Drawing.Color.White;
            this.LaboratoryBtn.Location = new System.Drawing.Point(10, 268);
            this.LaboratoryBtn.Margin = new System.Windows.Forms.Padding(10);
            this.LaboratoryBtn.Name = "LaboratoryBtn";
            this.LaboratoryBtn.Size = new System.Drawing.Size(324, 66);
            this.LaboratoryBtn.TabIndex = 3;
            this.LaboratoryBtn.Text = "Laboratorium";
            this.LaboratoryBtn.TextColor = System.Drawing.Color.White;
            this.LaboratoryBtn.UseVisualStyleBackColor = false;
            this.LaboratoryBtn.Click += new System.EventHandler(this.LaboratoryBtn_Click);
            // 
            // ManipulatorBtn
            // 
            this.ManipulatorBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ManipulatorBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.ManipulatorBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.ManipulatorBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.ManipulatorBtn.BorderRadius = 15;
            this.ManipulatorBtn.BorderSize = 0;
            this.ManipulatorBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Default;
            this.ManipulatorBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ManipulatorBtn.FlatAppearance.BorderSize = 0;
            this.ManipulatorBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ManipulatorBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.ManipulatorBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManipulatorBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ManipulatorBtn.ForeColor = System.Drawing.Color.White;
            this.ManipulatorBtn.Location = new System.Drawing.Point(10, 182);
            this.ManipulatorBtn.Margin = new System.Windows.Forms.Padding(10);
            this.ManipulatorBtn.Name = "ManipulatorBtn";
            this.ManipulatorBtn.Size = new System.Drawing.Size(324, 66);
            this.ManipulatorBtn.TabIndex = 2;
            this.ManipulatorBtn.Text = "Manipulator";
            this.ManipulatorBtn.TextColor = System.Drawing.Color.White;
            this.ManipulatorBtn.UseVisualStyleBackColor = false;
            this.ManipulatorBtn.Click += new System.EventHandler(this.ManipulatorBtn_Click);
            // 
            // DriveBtn
            // 
            this.DriveBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.DriveBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(120)))), ((int)(((byte)(120)))), ((int)(((byte)(120)))));
            this.DriveBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.DriveBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.DriveBtn.BorderRadius = 15;
            this.DriveBtn.BorderSize = 0;
            this.DriveBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Default;
            this.DriveBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DriveBtn.FlatAppearance.BorderSize = 0;
            this.DriveBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.DriveBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(96)))), ((int)(((byte)(96)))), ((int)(((byte)(96)))));
            this.DriveBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.DriveBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.DriveBtn.ForeColor = System.Drawing.Color.White;
            this.DriveBtn.Location = new System.Drawing.Point(10, 96);
            this.DriveBtn.Margin = new System.Windows.Forms.Padding(10);
            this.DriveBtn.Name = "DriveBtn";
            this.DriveBtn.Size = new System.Drawing.Size(324, 66);
            this.DriveBtn.TabIndex = 1;
            this.DriveBtn.Text = "Platforma";
            this.DriveBtn.TextColor = System.Drawing.Color.White;
            this.DriveBtn.UseVisualStyleBackColor = false;
            this.DriveBtn.Click += new System.EventHandler(this.DriveBtn_Click);
            // 
            // CommunicationBtn
            // 
            this.CommunicationBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.CommunicationBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.CommunicationBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.CommunicationBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.CommunicationBtn.BorderRadius = 15;
            this.CommunicationBtn.BorderSize = 0;
            this.CommunicationBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Primary;
            this.CommunicationBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.CommunicationBtn.FlatAppearance.BorderSize = 0;
            this.CommunicationBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.CommunicationBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.CommunicationBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommunicationBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.CommunicationBtn.ForeColor = System.Drawing.Color.White;
            this.CommunicationBtn.Location = new System.Drawing.Point(10, 10);
            this.CommunicationBtn.Margin = new System.Windows.Forms.Padding(10);
            this.CommunicationBtn.Name = "CommunicationBtn";
            this.CommunicationBtn.Size = new System.Drawing.Size(324, 66);
            this.CommunicationBtn.TabIndex = 0;
            this.CommunicationBtn.Text = "Komunikacja";
            this.CommunicationBtn.TextColor = System.Drawing.Color.White;
            this.CommunicationBtn.UseVisualStyleBackColor = false;
            this.CommunicationBtn.Click += new System.EventHandler(this.CommunicationBtn_Click);
            // 
            // NightModeToggle
            // 
            this.NightModeToggle.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.NightModeToggle.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.NightModeToggle.Checked = true;
            this.NightModeToggle.CheckState = System.Windows.Forms.CheckState.Checked;
            this.NightModeToggle.FlatAppearance.BorderSize = 0;
            this.NightModeToggle.ForeColor = System.Drawing.Color.Transparent;
            this.NightModeToggle.Location = new System.Drawing.Point(10, 54);
            this.NightModeToggle.Margin = new System.Windows.Forms.Padding(10);
            this.NightModeToggle.MinimumSize = new System.Drawing.Size(45, 22);
            this.NightModeToggle.Name = "NightModeToggle";
            this.NightModeToggle.Size = new System.Drawing.Size(98, 24);
            this.NightModeToggle.TabIndex = 0;
            this.NightModeToggle.Text = "customToggleButton1";
            this.NightModeToggle.UseVisualStyleBackColor = true;
            this.NightModeToggle.CheckedChanged += new System.EventHandler(this.NightModeToggle_CheckedChanged);
            // 
            // UnpinBtn
            // 
            this.UnpinBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.UnpinBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.UnpinBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.UnpinBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.UnpinBtn.BorderRadius = 15;
            this.UnpinBtn.BorderSize = 0;
            this.UnpinBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Primary;
            this.UnpinBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.UnpinBtn.FlatAppearance.BorderSize = 0;
            this.UnpinBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.UnpinBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.UnpinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnpinBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.UnpinBtn.ForeColor = System.Drawing.Color.White;
            this.UnpinBtn.Location = new System.Drawing.Point(181, 20);
            this.UnpinBtn.Margin = new System.Windows.Forms.Padding(10, 20, 10, 20);
            this.UnpinBtn.Name = "UnpinBtn";
            this.UnpinBtn.Size = new System.Drawing.Size(153, 68);
            this.UnpinBtn.TabIndex = 1;
            this.UnpinBtn.Text = "Odepnij kartę";
            this.UnpinBtn.TextColor = System.Drawing.Color.White;
            this.UnpinBtn.UseVisualStyleBackColor = false;
            this.UnpinBtn.Click += new System.EventHandler(this.UnpinBtn_Click);
            // 
            // ResetViewBtn
            // 
            this.ResetViewBtn.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.ResetViewBtn.BackgroundColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(120)))), ((int)(((byte)(215)))));
            this.ResetViewBtn.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(155)))), ((int)(((byte)(154)))), ((int)(((byte)(159)))));
            this.ResetViewBtn.BorderModeSelect = HAL062app.CustomControls.CustomButton.BorderMode.Off;
            this.ResetViewBtn.BorderRadius = 15;
            this.ResetViewBtn.BorderSize = 0;
            this.ResetViewBtn.ButtonStyle = HAL062app.CustomControls.CustomButton.ButtonStyles.Primary;
            this.ResetViewBtn.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ResetViewBtn.FlatAppearance.BorderSize = 0;
            this.ResetViewBtn.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.ResetViewBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(96)))), ((int)(((byte)(172)))));
            this.ResetViewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetViewBtn.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.ResetViewBtn.ForeColor = System.Drawing.Color.White;
            this.ResetViewBtn.Location = new System.Drawing.Point(10, 20);
            this.ResetViewBtn.Margin = new System.Windows.Forms.Padding(10, 20, 10, 20);
            this.ResetViewBtn.Name = "ResetViewBtn";
            this.ResetViewBtn.Size = new System.Drawing.Size(151, 68);
            this.ResetViewBtn.TabIndex = 0;
            this.ResetViewBtn.Text = "Zresetuj widok";
            this.ResetViewBtn.TextColor = System.Drawing.Color.White;
            this.ResetViewBtn.UseVisualStyleBackColor = false;
            this.ResetViewBtn.Click += new System.EventHandler(this.ResetViewBtn_Click);
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.tableLayoutPanel7);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KNR HAL-062 Command Center";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.mainForm_FormClosing);
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.tableLayoutPanel4.ResumeLayout(false);
            this.tableLayoutPanel5.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel6.ResumeLayout(false);
            this.tableLayoutPanel6.PerformLayout();
            this.tableLayoutPanel3.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LogoPictureBox)).EndInit();
            this.SidePanel.ResumeLayout(false);
            this.tableLayoutPanel7.ResumeLayout(false);
            this.tableLayoutPanel7.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Panel ContextPanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label GamePadStatusLabel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
      
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel4;

        private CustomControls.CustomButton CommunicationButton;
        private CustomControls.CustomButton LaboButton;
        private CustomControls.CustomButton ChassisButton;
        private CustomControls.CustomButton ManipulatorButton;
        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel5;
        private System.Windows.Forms.PictureBox LogoPictureBox;
        private CustomControls.CustomButton CommunicationBtn;
        private CustomControls.CustomButton SandboxBtn;
        private CustomControls.CustomButton LaboratoryBtn;
        private CustomControls.CustomButton ManipulatorBtn;
        private CustomControls.CustomButton DriveBtn;
        private CustomControls.CustomButton UnpinBtn;
        private CustomControls.CustomButton ResetViewBtn;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel7;
        private CustomControls.CustomToggleButton NightModeToggle;
    }
}

