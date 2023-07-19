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
            this.FastOptionPanel = new System.Windows.Forms.Panel();
            this.ButtonPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.StatusPanel = new System.Windows.Forms.Panel();
            this.ContextPanel = new System.Windows.Forms.Panel();
            this.UpperPanel = new System.Windows.Forms.Panel();
            this.MainPageButton = new HAL062app.CustomControls.CustomButton();
            this.CommunicationButton = new HAL062app.CustomControls.CustomButton();
            this.ChassisButton = new HAL062app.CustomControls.CustomButton();
            this.ManipulatorButton = new HAL062app.CustomControls.CustomButton();
            this.LaboButton = new HAL062app.CustomControls.CustomButton();
            this.customButton6 = new HAL062app.CustomControls.CustomButton();
            this.SidePanel.SuspendLayout();
            this.ButtonPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // SidePanel
            // 
            this.SidePanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left)));
            this.SidePanel.AutoSize = true;
            this.SidePanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.SidePanel.Controls.Add(this.FastOptionPanel);
            this.SidePanel.Controls.Add(this.ButtonPanel);
            this.SidePanel.Controls.Add(this.StatusPanel);
            this.SidePanel.Location = new System.Drawing.Point(29, 29);
            this.SidePanel.Margin = new System.Windows.Forms.Padding(20);
            this.SidePanel.Name = "SidePanel";
            this.SidePanel.Size = new System.Drawing.Size(286, 803);
            this.SidePanel.TabIndex = 0;
            // 
            // FastOptionPanel
            // 
            this.FastOptionPanel.AutoSize = true;
            this.FastOptionPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.FastOptionPanel.Location = new System.Drawing.Point(15, 502);
            this.FastOptionPanel.Margin = new System.Windows.Forms.Padding(15);
            this.FastOptionPanel.Name = "FastOptionPanel";
            this.FastOptionPanel.Size = new System.Drawing.Size(256, 286);
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
            this.ButtonPanel.Controls.Add(this.customButton6);
            this.ButtonPanel.Location = new System.Drawing.Point(15, 115);
            this.ButtonPanel.Margin = new System.Windows.Forms.Padding(15);
            this.ButtonPanel.Name = "ButtonPanel";
            this.ButtonPanel.Size = new System.Drawing.Size(256, 357);
            this.ButtonPanel.TabIndex = 1;
            // 
            // StatusPanel
            // 
            this.StatusPanel.AutoSize = true;
            this.StatusPanel.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
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
            this.ContextPanel.Margin = new System.Windows.Forms.Padding(20);
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
            // customButton6
            // 
            this.customButton6.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.customButton6.BackgroundColor = System.Drawing.Color.MediumSlateBlue;
            this.customButton6.BorderColor = System.Drawing.Color.PaleVioletRed;
            this.customButton6.BorderRadius = 10;
            this.customButton6.BorderSize = 0;
            this.customButton6.FlatAppearance.BorderSize = 0;
            this.customButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.customButton6.ForeColor = System.Drawing.Color.White;
            this.customButton6.Location = new System.Drawing.Point(10, 310);
            this.customButton6.Margin = new System.Windows.Forms.Padding(10);
            this.customButton6.Name = "customButton6";
            this.customButton6.Size = new System.Drawing.Size(236, 40);
            this.customButton6.TabIndex = 5;
            this.customButton6.Text = "customButton6";
            this.customButton6.TextColor = System.Drawing.Color.White;
            this.customButton6.UseVisualStyleBackColor = false;
            this.customButton6.Click += new System.EventHandler(this.customButton6_Click);
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
            this.Name = "mainForm";
            this.Text = "KNR HAL-062";
            this.Load += new System.EventHandler(this.mainForm_Load);
            this.SidePanel.ResumeLayout(false);
            this.SidePanel.PerformLayout();
            this.ButtonPanel.ResumeLayout(false);
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
        private CustomControls.CustomButton customButton6;
    }
}

