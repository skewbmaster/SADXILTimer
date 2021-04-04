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
            timerLabel = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // timerLabel
            // 
            timerLabel.AutoSize = true;
            timerLabel.Font = new System.Drawing.Font("Calibri", 30F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            timerLabel.Location = new System.Drawing.Point(50, 43);
            timerLabel.Margin = new System.Windows.Forms.Padding(0);
            timerLabel.Name = "timerLabel";
            timerLabel.Size = new System.Drawing.Size(200, 49);
            timerLabel.TabIndex = 0;
            timerLabel.Text = "99 : 59 . 99";
            timerLabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // ILTimer
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 18F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(304, 141);
            this.Controls.Add(timerLabel);
            this.Font = new System.Drawing.Font("Calibri", 11F);
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(640, 360);
            this.MinimumSize = new System.Drawing.Size(255, 135);
            this.Name = "ILTimer";
            this.ShowIcon = false;
            this.Text = "IL Timer";
            this.Resize += new System.EventHandler(this.ILTimer_Resize);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        public static System.Windows.Forms.Label timerLabel;
    }
}

