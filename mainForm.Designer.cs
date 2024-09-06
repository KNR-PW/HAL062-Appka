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
            this.SidePanel = new System.Windows.Forms.Panel();
            this.PinBtn = new HAL062app.CustomControls.CustomButton();
            this.ResetViewBtn = new HAL062app.CustomControls.CustomButton();
            this.UnpinBtn = new HAL062app.CustomControls.CustomButton();
            this.FastOptionPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.MainPageButton = new HAL062app.CustomControls.CustomButton();
            this.CommunicationButton = new HAL062app.CustomControls.CustomButton();
            this.ChassisButton = new HAL062app.CustomControls.CustomButton();
            this.ManipulatorButton = new HAL062app.CustomControls.CustomButton();
            this.LaboButton = new HAL062app.CustomControls.CustomButton();
            this.sandboxBtn = new HAL062app.CustomControls.CustomButton();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.ContextPanel = new System.Windows.Forms.Panel();
            this.UpperPanel = new System.Windows.Forms.Panel();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.GamePadStatusLabel = new System.Windows.Forms.Label();
            this.SidePanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.StatusPanel.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SidePanel.AutoSize = true;
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.SidePanel.Controls.Add(this.PinBtn);
            this.SidePanel.Controls.Add(this.ResetViewBtn);
            this.SidePanel.Controls.Add(this.UnpinBtn);
            this.SidePanel.Controls.Add(this.FastOptionPanel);
            this.SidePanel.Controls.Add(this.ButtonPanel);
            this.SidePanel.Controls.Add(this.StatusPanel);
            this.SidePanel.Location = new System.Drawing.Point(29, 29);
            this.SidePanel.Margin = new System.Windows.Forms.Padding(20);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(286, 803);
            this.SidePanel.TabIndex = 0;
            // 
            // PinBtn
            // 
            this.PinBtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.PinBtn.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.PinBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.PinBtn.BorderRadius = 10;
            this.PinBtn.BorderSize = 0;
            this.PinBtn.FlatAppearance.BorderSize = 0;
            this.PinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PinBtn.ForeColor = System.Drawing.Color.White;
            this.PinBtn.Location = new System.Drawing.Point(107, 485);
            this.PinBtn.Margin = new System.Windows.Forms.Padding(10);
            this.PinBtn.Name = "PinBtn";
            this.PinBtn.Size = new System.Drawing.Size(72, 40);
            this.PinBtn.TabIndex = 8;
            this.PinBtn.Text = "Przypnij zakładkę";
            this.PinBtn.TextColor = System.Drawing.Color.White;
            this.PinBtn.UseVisualStyleBackColor = false;
            this.PinBtn.Click += new System.EventHandler(this.PinBtn_Click);
            // 
            // ResetViewBtn
            // 
            this.ResetViewBtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.ResetViewBtn.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.ResetViewBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ResetViewBtn.BorderRadius = 10;
            this.ResetViewBtn.BorderSize = 0;
            this.ResetViewBtn.FlatAppearance.BorderSize = 0;
            this.ResetViewBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ResetViewBtn.ForeColor = System.Drawing.Color.White;
            this.ResetViewBtn.Location = new System.Drawing.Point(189, 485);
            this.ResetViewBtn.Margin = new System.Windows.Forms.Padding(10);
            this.ResetViewBtn.Name = "ResetViewBtn";
            this.ResetViewBtn.Size = new System.Drawing.Size(72, 40);
            this.ResetViewBtn.TabIndex = 7;
            this.ResetViewBtn.Text = "Zresetuj widok";
            this.ResetViewBtn.TextColor = System.Drawing.Color.White;
            this.ResetViewBtn.UseVisualStyleBackColor = false;
            this.ResetViewBtn.Click += new System.EventHandler(this.ResetViewBtn_Click);
            // 
            // UnpinBtn
            // 
            this.UnpinBtn.BackColor = System.Drawing.Color.CornflowerBlue;
            this.UnpinBtn.BackgroundColor = System.Drawing.Color.CornflowerBlue;
            this.UnpinBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.UnpinBtn.BorderRadius = 10;
            this.UnpinBtn.BorderSize = 0;
            this.UnpinBtn.FlatAppearance.BorderSize = 0;
            this.UnpinBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.UnpinBtn.ForeColor = System.Drawing.Color.White;
            this.UnpinBtn.Location = new System.Drawing.Point(25, 485);
            this.UnpinBtn.Margin = new System.Windows.Forms.Padding(10);
            this.UnpinBtn.Name = "UnpinBtn";
            this.UnpinBtn.Size = new System.Drawing.Size(72, 40);
            this.UnpinBtn.TabIndex = 6;
            this.UnpinBtn.Text = "Odepnij zakładkę";
            this.UnpinBtn.TextColor = System.Drawing.Color.White;
            this.UnpinBtn.UseVisualStyleBackColor = false;
            this.UnpinBtn.Click += new System.EventHandler(this.UnpinBtn_Click);
            // 
            // FastOptionPanel
            // 
            this.FastOptionPanel.AutoSize = true;
            this.FastOptionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.FastOptionPanel.Location = new System.Drawing.Point(15, 585);
            this.FastOptionPanel.Margin = new System.Windows.Forms.Padding(15);
            this.FastOptionPanel.Name = "FastOptionPanel";
            this.FastOptionPanel.Size = new System.Drawing.Size(256, 203);
            this.FastOptionPanel.TabIndex = 2;
            // 
            // ButtonPanel
            // 
            this.ButtonPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ButtonPanel.Controls.Add(this.MainPageButton);
            this.ButtonPanel.Controls.Add(this.CommunicationButton);
            this.ButtonPanel.Controls.Add(this.ChassisButton);
            this.ButtonPanel.Controls.Add(this.ManipulatorButton);
            this.ButtonPanel.Controls.Add(this.LaboButton);
            this.ButtonPanel.Controls.Add(this.sandboxBtn);
            this.ButtonPanel.Location = new System.Drawing.Point(15, 115);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(15);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(256, 357);
            this.ButtonPanel.TabIndex = 1;
            // 
            // MainPageButton
            // 
            this.MainPageButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.MainPageButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.MainPageButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.MainPageButton.BorderRadius = 10;
            this.MainPageButton.BorderSize = 0;
            this.MainPageButton.FlatAppearance.BorderSize = 0;
            this.MainPageButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.MainPageButton.ForeColor = System.Drawing.Color.White;
            this.MainPageButton.Location = new System.Drawing.Point(10, 10);
            this.MainPageButton.Margin = new System.Windows.Forms.Padding(10);
            this.MainPageButton.Name = "MainPageButton";
            this.MainPageButton.Size = new System.Drawing.Size(236, 40);
            this.MainPageButton.TabIndex = 0;
            this.MainPageButton.Text = "Strona startowa";
            this.MainPageButton.TextColor = System.Drawing.Color.White;
            this.MainPageButton.UseVisualStyleBackColor = false;
            this.MainPageButton.Click += new System.EventHandler(this.MainPageButton_Click);
            // 
            // CommunicationButton
            // 
            this.CommunicationButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.CommunicationButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.CommunicationButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.CommunicationButton.BorderRadius = 10;
            this.CommunicationButton.BorderSize = 0;
            this.CommunicationButton.FlatAppearance.BorderSize = 0;
            this.CommunicationButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CommunicationButton.ForeColor = System.Drawing.Color.White;
            this.CommunicationButton.Location = new System.Drawing.Point(10, 70);
            this.CommunicationButton.Margin = new System.Windows.Forms.Padding(10);
            this.CommunicationButton.Name = "CommunicationButton";
            this.CommunicationButton.Size = new System.Drawing.Size(236, 40);
            this.CommunicationButton.TabIndex = 1;
            this.CommunicationButton.Text = "Komunikacja";
            this.CommunicationButton.TextColor = System.Drawing.Color.White;
            this.CommunicationButton.UseVisualStyleBackColor = false;
            this.CommunicationButton.Click += new System.EventHandler(this.CommunicationButton_Click);
            // 
            // ChassisButton
            // 
            this.ChassisButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ChassisButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.ChassisButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ChassisButton.BorderRadius = 10;
            this.ChassisButton.BorderSize = 0;
            this.ChassisButton.FlatAppearance.BorderSize = 0;
            this.ChassisButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ChassisButton.ForeColor = System.Drawing.Color.White;
            this.ChassisButton.Location = new System.Drawing.Point(10, 130);
            this.ChassisButton.Margin = new System.Windows.Forms.Padding(10);
            this.ChassisButton.Name = "ChassisButton";
            this.ChassisButton.Size = new System.Drawing.Size(236, 40);
            this.ChassisButton.TabIndex = 2;
            this.ChassisButton.Text = "Podwozie";
            this.ChassisButton.TextColor = System.Drawing.Color.White;
            this.ChassisButton.UseVisualStyleBackColor = false;
            this.ChassisButton.Click += new System.EventHandler(this.ChassisButton_Click);
            // 
            // ManipulatorButton
            // 
            this.ManipulatorButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.ManipulatorButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.ManipulatorButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.ManipulatorButton.BorderRadius = 10;
            this.ManipulatorButton.BorderSize = 0;
            this.ManipulatorButton.FlatAppearance.BorderSize = 0;
            this.ManipulatorButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ManipulatorButton.ForeColor = System.Drawing.Color.White;
            this.ManipulatorButton.Location = new System.Drawing.Point(10, 190);
            this.ManipulatorButton.Margin = new System.Windows.Forms.Padding(10);
            this.ManipulatorButton.Name = "ManipulatorButton";
            this.ManipulatorButton.Size = new System.Drawing.Size(236, 40);
            this.ManipulatorButton.TabIndex = 3;
            this.ManipulatorButton.Text = "Manipulator";
            this.ManipulatorButton.TextColor = System.Drawing.Color.White;
            this.ManipulatorButton.UseVisualStyleBackColor = false;
            this.ManipulatorButton.Click += new System.EventHandler(this.ManipulatorButton_Click);
            // 
            // LaboButton
            // 
            this.LaboButton.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.LaboButton.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.LaboButton.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.LaboButton.BorderRadius = 10;
            this.LaboButton.BorderSize = 0;
            this.LaboButton.FlatAppearance.BorderSize = 0;
            this.LaboButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LaboButton.ForeColor = System.Drawing.Color.White;
            this.LaboButton.Location = new System.Drawing.Point(10, 250);
            this.LaboButton.Margin = new System.Windows.Forms.Padding(10);
            this.LaboButton.Name = "LaboButton";
            this.LaboButton.Size = new System.Drawing.Size(236, 40);
            this.LaboButton.TabIndex = 4;
            this.LaboButton.Text = "Laboratorium";
            this.LaboButton.TextColor = System.Drawing.Color.White;
            this.LaboButton.UseVisualStyleBackColor = false;
            this.LaboButton.Click += new System.EventHandler(this.LaboButton_Click);
            // 
            // sandboxBtn
            // 
            this.sandboxBtn.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.sandboxBtn.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.sandboxBtn.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.sandboxBtn.BorderRadius = 10;
            this.sandboxBtn.BorderSize = 0;
            this.sandboxBtn.FlatAppearance.BorderSize = 0;
            this.sandboxBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.sandboxBtn.ForeColor = System.Drawing.Color.White;
            this.sandboxBtn.Location = new System.Drawing.Point(10, 310);
            this.sandboxBtn.Margin = new System.Windows.Forms.Padding(10);
            this.sandboxBtn.Name = "sandboxBtn";
            this.sandboxBtn.Size = new System.Drawing.Size(236, 40);
            this.sandboxBtn.TabIndex = 5;
            this.sandboxBtn.Text = "Debug";
            this.sandboxBtn.TextColor = System.Drawing.Color.White;
            this.sandboxBtn.UseVisualStyleBackColor = false;
            this.sandboxBtn.Click += new System.EventHandler(this.customButton6_Click);
            // 
            // StatusPanel
            // 
            this.StatusPanel.AutoSize = true;
            this.StatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.StatusPanel.Controls.Add(this.tableLayoutPanel1);
            this.StatusPanel.Location = new System.Drawing.Point(15, 15);
            this.StatusPanel.Margin = new System.Windows.Forms.Padding(15);
            this.StatusPanel.Name = "StatusPanel";
            this.StatusPanel.Size = new System.Drawing.Size(256, 70);
            this.StatusPanel.TabIndex = 0;
            // 
            // ContextPanel
            // 
            this.ContextPanel.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.ContextPanel.AutoSize = true;
            this.ContextPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ContextPanel.Location = new System.Drawing.Point(355, 132);
            this.ContextPanel.Margin = new System.Windows.Forms.Padding(0);
            this.ContextPanel.Name = "ContextPanel";
            this.ContextPanel.Size = new System.Drawing.Size(1200, 700);
            this.ContextPanel.TabIndex = 1;
            // 
            // UpperPanel
            // 
            this.UpperPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.UpperPanel.AutoSize = true;
            this.UpperPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.UpperPanel.Location = new System.Drawing.Point(358, 29);
            this.UpperPanel.Margin = new System.Windows.Forms.Padding(15);
            this.UpperPanel.Name = "UpperPanel";
            this.UpperPanel.Size = new System.Drawing.Size(1197, 85);
            this.UpperPanel.TabIndex = 2;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.GamePadStatusLabel, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(256, 70);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // GamePadStatusLabel
            // 
            this.GamePadStatusLabel.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.GamePadStatusLabel.AutoSize = true;
            this.GamePadStatusLabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(238)));
            this.GamePadStatusLabel.ForeColor = System.Drawing.Color.MediumSlateBlue;
            this.GamePadStatusLabel.Location = new System.Drawing.Point(15, 8);
            this.GamePadStatusLabel.Margin = new System.Windows.Forms.Padding(0);
            this.GamePadStatusLabel.Name = "GamePadStatusLabel";
            this.GamePadStatusLabel.Size = new System.Drawing.Size(98, 18);
            this.GamePadStatusLabel.TabIndex = 0;
            this.GamePadStatusLabel.Text = "Gamepad: off";
            // 
            // mainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1584, 861);
            this.Controls.Add(this.UpperPanel);
            this.Controls.Add(this.ContextPanel);
            this.Controls.Add(this.SidePanel);
            this.MinimumSize = new System.Drawing.Size(1600, 900);
            this.Name = "mainForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "KNR HAL-062";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
            this.StatusPanel.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel SidePanel;
        private System.Windows.Forms.Panel StatusPanel;
        private System.Windows.Forms.Panel ContextPanel;
        private System.Windows.Forms.Panel FastOptionPanel;
        private System.Windows.Forms.FlowLayoutPanel ButtonPanel;
        private System.Windows.Forms.Panel UpperPanel;
        private CustomControls.CustomButton MainPageButton;
        private CustomControls.CustomButton CommunicationButton;
        private CustomControls.CustomButton ChassisButton;
        private CustomControls.CustomButton ManipulatorButton;
        private CustomControls.CustomButton LaboButton;
        private CustomControls.CustomButton sandboxBtn;
        private CustomControls.CustomButton ResetViewBtn;
        private CustomControls.CustomButton UnpinBtn;
        private CustomControls.CustomButton PinBtn;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Label GamePadStatusLabel;
    }
}

