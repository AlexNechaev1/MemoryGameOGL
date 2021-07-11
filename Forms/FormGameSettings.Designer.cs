namespace myOpenGL.Forms
{
    partial class FormGameSettings
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
            this.textBoxFirstPlayerName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.gameModeBtn = new System.Windows.Forms.Button();
            this.sceneModeBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBoxFirstPlayerName
            // 
            this.textBoxFirstPlayerName.Location = new System.Drawing.Point(143, 14);
            this.textBoxFirstPlayerName.Name = "textBoxFirstPlayerName";
            this.textBoxFirstPlayerName.Size = new System.Drawing.Size(212, 20);
            this.textBoxFirstPlayerName.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 17);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 13);
            this.label1.TabIndex = 5;
            this.label1.Text = "First Player Name:";
            // 
            // gameModeBtn
            // 
            this.gameModeBtn.Location = new System.Drawing.Point(12, 58);
            this.gameModeBtn.Name = "gameModeBtn";
            this.gameModeBtn.Size = new System.Drawing.Size(343, 23);
            this.gameModeBtn.TabIndex = 6;
            this.gameModeBtn.Text = "Game Mode";
            this.gameModeBtn.UseVisualStyleBackColor = true;
            this.gameModeBtn.Click += new System.EventHandler(this.gameModeBtn_Click);
            // 
            // sceneModeBtn
            // 
            this.sceneModeBtn.Location = new System.Drawing.Point(12, 97);
            this.sceneModeBtn.Name = "sceneModeBtn";
            this.sceneModeBtn.Size = new System.Drawing.Size(343, 23);
            this.sceneModeBtn.TabIndex = 8;
            this.sceneModeBtn.Text = "Scene Mode";
            this.sceneModeBtn.UseVisualStyleBackColor = true;
            this.sceneModeBtn.Click += new System.EventHandler(this.sceneModeBtn_Click);
            // 
            // FormGameSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(367, 130);
            this.Controls.Add(this.sceneModeBtn);
            this.Controls.Add(this.gameModeBtn);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBoxFirstPlayerName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.ImeMode = System.Windows.Forms.ImeMode.Off;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FormGameSettings";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Memory Game - Settings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox textBoxFirstPlayerName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button gameModeBtn;
        private System.Windows.Forms.Button sceneModeBtn;
    }
}