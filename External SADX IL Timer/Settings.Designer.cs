
namespace External_SADX_IL_Timer
{
    partial class Settings
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
            this.label1 = new System.Windows.Forms.Label();
            this.backColorButton = new System.Windows.Forms.Button();
            backColorButton2 = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            fontColorButton2 = new System.Windows.Forms.Button();
            this.fontColorButton = new System.Windows.Forms.Button();
            this.label3 = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(13, 13);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(144, 20);
            this.label1.TabIndex = 0;
            this.label1.Text = "Background Color :";
            // 
            // backColorButton
            // 
            this.backColorButton.Location = new System.Drawing.Point(163, 12);
            this.backColorButton.Name = "backColorButton";
            this.backColorButton.Size = new System.Drawing.Size(38, 23);
            this.backColorButton.TabIndex = 1;
            this.backColorButton.Text = "...";
            this.backColorButton.UseVisualStyleBackColor = true;
            // 
            // backColorButton2
            // 
            backColorButton2.Enabled = false;
            backColorButton2.Location = new System.Drawing.Point(208, 12);
            backColorButton2.Name = "backColorButton2";
            backColorButton2.Size = new System.Drawing.Size(23, 23);
            backColorButton2.TabIndex = 2;
            backColorButton2.UseVisualStyleBackColor = true;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(66, 46);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(91, 20);
            this.label2.TabIndex = 3;
            this.label2.Text = "Font Color :";
            // 
            // fontColorButton2
            // 
            fontColorButton2.Enabled = false;
            fontColorButton2.Location = new System.Drawing.Point(208, 46);
            fontColorButton2.Name = "fontColorButton2";
            fontColorButton2.Size = new System.Drawing.Size(23, 23);
            fontColorButton2.TabIndex = 5;
            fontColorButton2.UseVisualStyleBackColor = true;
            // 
            // fontColorButton
            // 
            this.fontColorButton.Location = new System.Drawing.Point(163, 46);
            this.fontColorButton.Name = "fontColorButton";
            this.fontColorButton.Size = new System.Drawing.Size(38, 23);
            this.fontColorButton.TabIndex = 4;
            this.fontColorButton.Text = "...";
            this.fontColorButton.UseVisualStyleBackColor = true;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(107, 79);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 20);
            this.label3.TabIndex = 6;
            this.label3.Text = "Font :";
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(163, 79);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(68, 23);
            this.button1.TabIndex = 7;
            this.button1.Text = "...";
            this.button1.UseVisualStyleBackColor = true;
            // 
            // Settings
            // 
            this.ClientSize = new System.Drawing.Size(247, 115);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.label3);
            this.Controls.Add(fontColorButton2);
            this.Controls.Add(this.fontColorButton);
            this.Controls.Add(this.label2);
            this.Controls.Add(backColorButton2);
            this.Controls.Add(this.backColorButton);
            this.Controls.Add(this.label1);
            this.Name = "Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button backColorButton;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button fontColorButton;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private static System.Windows.Forms.Button fontColorButton2;
        private static System.Windows.Forms.Button backColorButton2;
    }
}