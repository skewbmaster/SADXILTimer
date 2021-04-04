namespace External_SADX_IL_Timer
{
    partial class ILTimer
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
            this.minutesLabel = new System.Windows.Forms.Label();
            this.separator1 = new System.Windows.Forms.Label();
            this.secondsLabel = new System.Windows.Forms.Label();
            this.separator2 = new System.Windows.Forms.Label();
            this.framesLabel = new System.Windows.Forms.Label();
            this.backgroundTiming = new System.ComponentModel.BackgroundWorker();
            this.SuspendLayout();
            // 
            // minutesLabel
            // 
            this.minutesLabel.AutoSize = true;
            this.minutesLabel.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.minutesLabel.Location = new System.Drawing.Point(38, 46);
            this.minutesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.minutesLabel.Name = "minutesLabel";
            this.minutesLabel.Size = new System.Drawing.Size(42, 49);
            this.minutesLabel.TabIndex = 0;
            this.minutesLabel.Text = "9";
            this.minutesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // separator1
            // 
            this.separator1.AutoSize = true;
            this.separator1.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.separator1.Location = new System.Drawing.Point(80, 46);
            this.separator1.Margin = new System.Windows.Forms.Padding(0);
            this.separator1.Name = "separator1";
            this.separator1.Size = new System.Drawing.Size(33, 49);
            this.separator1.TabIndex = 1;
            this.separator1.Text = ":";
            // 
            // secondsLabel
            // 
            this.secondsLabel.AutoSize = true;
            this.secondsLabel.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.secondsLabel.Location = new System.Drawing.Point(113, 46);
            this.secondsLabel.Margin = new System.Windows.Forms.Padding(0);
            this.secondsLabel.Name = "secondsLabel";
            this.secondsLabel.Size = new System.Drawing.Size(62, 49);
            this.secondsLabel.TabIndex = 2;
            this.secondsLabel.Text = "59";
            this.secondsLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // separator2
            // 
            this.separator2.AutoSize = true;
            this.separator2.BackColor = System.Drawing.SystemColors.Control;
            this.separator2.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.separator2.Location = new System.Drawing.Point(172, 46);
            this.separator2.Margin = new System.Windows.Forms.Padding(0);
            this.separator2.Name = "separator2";
            this.separator2.Size = new System.Drawing.Size(33, 49);
            this.separator2.TabIndex = 3;
            this.separator2.Text = ".";
            // 
            // framesLabel
            // 
            this.framesLabel.AutoSize = true;
            this.framesLabel.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.framesLabel.Location = new System.Drawing.Point(194, 46);
            this.framesLabel.Margin = new System.Windows.Forms.Padding(0);
            this.framesLabel.Name = "framesLabel";
            this.framesLabel.Size = new System.Drawing.Size(62, 49);
            this.framesLabel.TabIndex = 4;
            this.framesLabel.Text = "99";
            this.framesLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // backgroundTiming
            // 
            this.backgroundTiming.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundTiming_DoWork);
            // 
            // ILTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 141);
            this.Controls.Add(this.framesLabel);
            this.Controls.Add(this.secondsLabel);
            this.Controls.Add(this.separator2);
            this.Controls.Add(this.separator1);
            this.Controls.Add(this.minutesLabel);
            this.Font = new System.Drawing.Font("Calibri", 11F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(320, 180);
            this.MinimumSize = new System.Drawing.Size(255, 135);
            this.Name = "ILTimer";
            this.ShowIcon = false;
            this.Text = "IL Timer";
            this.Resize += new System.EventHandler(this.ILTimer_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label minutesLabel;
        private System.Windows.Forms.Label separator1;
        private System.Windows.Forms.Label secondsLabel;
        private System.Windows.Forms.Label separator2;
        private System.Windows.Forms.Label framesLabel;
        private System.ComponentModel.BackgroundWorker backgroundTiming;
    }
}

