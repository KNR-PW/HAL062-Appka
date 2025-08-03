namespace HAL062app.moduly.manipulator
{
    partial class manipulatorForm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.HostManipulator = new System.Windows.Forms.Integration.ElementHost();
            this.manipulatorWPF1 = new HAL062app.moduly.manipulator.manipulatorWPF();
            this.HostSterowanie = new System.Windows.Forms.Integration.ElementHost();
            this.sterowanieWPF1 = new HAL062app.moduly.manipulator.SterowanieWPF();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.HostManipulator, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.HostSterowanie, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 1;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1184, 661);
            this.tableLayoutPanel1.TabIndex = 1;
            // 
            // HostManipulator
            // 
            this.HostManipulator.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(23)))), ((int)(((byte)(23)))), ((int)(((byte)(23)))));
            this.HostManipulator.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostManipulator.Location = new System.Drawing.Point(300, 0);
            this.HostManipulator.Margin = new System.Windows.Forms.Padding(0);
            this.HostManipulator.Name = "HostManipulator";
            this.HostManipulator.Size = new System.Drawing.Size(884, 661);
            this.HostManipulator.TabIndex = 0;
            this.HostManipulator.Text = "HostManipulator";
            this.HostManipulator.Child = this.manipulatorWPF1;
            // 
            // HostSterowanie
            // 
            this.HostSterowanie.Dock = System.Windows.Forms.DockStyle.Fill;
            this.HostSterowanie.Location = new System.Drawing.Point(0, 0);
            this.HostSterowanie.Margin = new System.Windows.Forms.Padding(0);
            this.HostSterowanie.Name = "HostSterowanie";
            this.HostSterowanie.Size = new System.Drawing.Size(300, 661);
            this.HostSterowanie.TabIndex = 1;
            this.HostSterowanie.Text = "HostSterowanie";
            this.HostSterowanie.Child = this.sterowanieWPF1;
            // 
            // manipulatorForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(38)))), ((int)(((byte)(41)))), ((int)(((byte)(46)))));
            this.ClientSize = new System.Drawing.Size(1184, 661);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "manipulatorForm";
            this.Text = "manipulatorForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.manipulatorForm_FormClosing);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Integration.ElementHost HostManipulator;
        private manipulatorWPF manipulatorWPF1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Integration.ElementHost HostSterowanie;
        private SterowanieWPF sterowanieWPF1;
    }
}