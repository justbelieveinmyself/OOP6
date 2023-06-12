namespace TestModule
{
    partial class Form1
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
            buttonStart = new Button();
            buttonStop = new Button();
            textBoxOutput = new TextBox();
            labelMetric = new Label();
            labelImperial = new Label();
            buttonDispose = new Button();
            SuspendLayout();
            // 
            // buttonStart
            // 
            buttonStart.Location = new Point(24, 24);
            buttonStart.Name = "buttonStart";
            buttonStart.Size = new Size(130, 49);
            buttonStart.TabIndex = 1;
            buttonStart.Text = "Start Collecting";
            buttonStart.UseVisualStyleBackColor = true;
            buttonStart.Click += buttonStart_Click;
            // 
            // buttonStop
            // 
            buttonStop.Location = new Point(173, 24);
            buttonStop.Name = "buttonStop";
            buttonStop.Size = new Size(130, 49);
            buttonStop.TabIndex = 5;
            buttonStop.Text = "Stop Collecting";
            buttonStop.UseVisualStyleBackColor = true;
            buttonStop.Click += buttonStop_Click;
            // 
            // textBoxOutput
            // 
            textBoxOutput.Location = new Point(337, 12);
            textBoxOutput.Multiline = true;
            textBoxOutput.Name = "textBoxOutput";
            textBoxOutput.Size = new Size(446, 407);
            textBoxOutput.TabIndex = 7;
            // 
            // labelMetric
            // 
            labelMetric.AutoSize = true;
            labelMetric.Location = new Point(12, 368);
            labelMetric.Name = "labelMetric";
            labelMetric.Size = new Size(0, 20);
            labelMetric.TabIndex = 8;
            // 
            // labelImperial
            // 
            labelImperial.AutoSize = true;
            labelImperial.Location = new Point(12, 400);
            labelImperial.Name = "labelImperial";
            labelImperial.Size = new Size(0, 20);
            labelImperial.TabIndex = 9;
            // 
            // buttonDispose
            // 
            buttonDispose.Location = new Point(24, 90);
            buttonDispose.Name = "buttonDispose";
            buttonDispose.Size = new Size(130, 49);
            buttonDispose.TabIndex = 10;
            buttonDispose.Text = "Dispose";
            buttonDispose.UseVisualStyleBackColor = true;
            buttonDispose.Click += buttonDispose_Click;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(955, 487);
            Controls.Add(buttonDispose);
            Controls.Add(labelImperial);
            Controls.Add(labelMetric);
            Controls.Add(textBoxOutput);
            Controls.Add(buttonStop);
            Controls.Add(buttonStart);
            Name = "Form1";
            Text = "Form1";
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion
        private Button buttonStart;
        private Button buttonStop;
        private TextBox textBoxOutput;
        private Label labelMetric;
        private Label labelImperial;
        private Button buttonDispose;
    }
}