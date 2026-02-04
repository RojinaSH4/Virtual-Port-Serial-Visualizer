namespace VPSG
{
    partial class dataVision
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(dataVision));
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            chartVision = new System.Windows.Forms.DataVisualization.Charting.Chart();
            btnRun = new Button();
            cmbSender = new ComboBox();
            cmbReceiver = new ComboBox();
            label1 = new Label();
            label2 = new Label();
            lblWarning = new Label();
            ((System.ComponentModel.ISupportInitialize)chartVision).BeginInit();
            SuspendLayout();
            // 
            // chartVision
            // 
            chartVision.BackImageTransparentColor = Color.White;
            chartVision.BackSecondaryColor = Color.Transparent;
            chartArea1.BackColor = Color.Transparent;
            chartArea1.BackImageTransparentColor = Color.Transparent;
            chartArea1.BackSecondaryColor = Color.Transparent;
            chartArea1.BorderColor = Color.White;
            chartArea1.Name = "ChartArea1";
            chartArea1.ShadowColor = Color.Transparent;
            chartVision.ChartAreas.Add(chartArea1);
            resources.ApplyResources(chartVision, "chartVision");
            chartVision.Name = "chartVision";
            chartVision.Palette = System.Windows.Forms.DataVisualization.Charting.ChartColorPalette.Berry;
            series1.BorderColor = Color.White;
            series1.ChartArea = "ChartArea1";
            series1.Color = Color.MediumSlateBlue;
            series1.LabelBackColor = Color.Red;
            series1.MarkerBorderColor = Color.Red;
            series1.Name = "Series1";
            series1.ShadowColor = Color.White;
            chartVision.Series.Add(series1);
            // 
            // btnRun
            // 
            btnRun.BackColor = Color.White;
            resources.ApplyResources(btnRun, "btnRun");
            btnRun.Name = "btnRun";
            btnRun.UseVisualStyleBackColor = false;
            btnRun.Click += btnRun_Click;
            // 
            // cmbSender
            // 
            cmbSender.FormattingEnabled = true;
            resources.ApplyResources(cmbSender, "cmbSender");
            cmbSender.Name = "cmbSender";
            cmbSender.SelectedIndexChanged += cmbSender_SelectedIndexChanged;
            // 
            // cmbReceiver
            // 
            cmbReceiver.FormattingEnabled = true;
            resources.ApplyResources(cmbReceiver, "cmbReceiver");
            cmbReceiver.Name = "cmbReceiver";
            cmbReceiver.SelectedIndexChanged += cmbReceiver_SelectedIndexChanged;
            // 
            // label1
            // 
            resources.ApplyResources(label1, "label1");
            label1.ForeColor = SystemColors.ControlLightLight;
            label1.Name = "label1";
            // 
            // label2
            // 
            resources.ApplyResources(label2, "label2");
            label2.ForeColor = SystemColors.ControlLightLight;
            label2.Name = "label2";
            // 
            // lblWarning
            // 
            resources.ApplyResources(lblWarning, "lblWarning");
            lblWarning.ForeColor = SystemColors.ControlLightLight;
            lblWarning.Name = "lblWarning";
            // 
            // dataVision
            // 
            resources.ApplyResources(this, "$this");
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.MediumSlateBlue;
            Controls.Add(lblWarning);
            Controls.Add(label2);
            Controls.Add(label1);
            Controls.Add(cmbReceiver);
            Controls.Add(cmbSender);
            Controls.Add(btnRun);
            Controls.Add(chartVision);
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "dataVision";
            SizeGripStyle = SizeGripStyle.Hide;
            FormClosed += dataVision_FormClosed;
            ((System.ComponentModel.ISupportInitialize)chartVision).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.DataVisualization.Charting.Chart chartVision;
        private Button btnRun;
        private ComboBox cmbSender;
        private ComboBox cmbReceiver;
        private Label label1;
        private Label label2;
        private Label lblWarning;
    }
}