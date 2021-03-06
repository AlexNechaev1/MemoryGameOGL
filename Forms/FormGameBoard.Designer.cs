namespace myOpenGL.Forms
{
    partial class FormGameBoard
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
            this.components = new System.ComponentModel.Container();
            this.panel1 = new System.Windows.Forms.Panel();
            this.secondPlayerInfoLabel = new System.Windows.Forms.Label();
            this.firstPlayerInfoLabel = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.numericUpDown3 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown2 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown1 = new System.Windows.Forms.NumericUpDown();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.numericUpDown4 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown5 = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown6 = new System.Windows.Forms.NumericUpDown();
            this.secretBoxElevationTimer = new System.Windows.Forms.Timer(this.components);
            this.currentPlayersPointsPanel = new System.Windows.Forms.Panel();
            this.currentPlayerInfoLabel = new System.Windows.Forms.Label();
            this.computerPlayerPointsPanel = new System.Windows.Forms.Panel();
            this.humanPlayerPointsPanel = new System.Windows.Forms.Panel();
            this.bottomPanel = new System.Windows.Forms.Panel();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).BeginInit();
            this.groupBox2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).BeginInit();
            this.currentPlayersPointsPanel.SuspendLayout();
            this.computerPlayerPointsPanel.SuspendLayout();
            this.humanPlayerPointsPanel.SuspendLayout();
            this.bottomPanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.SystemColors.ButtonFace;
            this.panel1.Location = new System.Drawing.Point(12, 16);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(503, 491);
            this.panel1.TabIndex = 6;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            this.panel1.Resize += new System.EventHandler(this.panel1_Resize);
            // 
            // secondPlayerInfoLabel
            // 
            this.secondPlayerInfoLabel.AutoSize = true;
            this.secondPlayerInfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.secondPlayerInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.secondPlayerInfoLabel.Name = "secondPlayerInfoLabel";
            this.secondPlayerInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.secondPlayerInfoLabel.TabIndex = 22;
            this.secondPlayerInfoLabel.Text = "a";
            // 
            // firstPlayerInfoLabel
            // 
            this.firstPlayerInfoLabel.AutoSize = true;
            this.firstPlayerInfoLabel.Dock = System.Windows.Forms.DockStyle.Left;
            this.firstPlayerInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.firstPlayerInfoLabel.Name = "firstPlayerInfoLabel";
            this.firstPlayerInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.firstPlayerInfoLabel.TabIndex = 21;
            this.firstPlayerInfoLabel.Text = "a";
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.numericUpDown3);
            this.groupBox1.Controls.Add(this.numericUpDown2);
            this.groupBox1.Controls.Add(this.numericUpDown1);
            this.groupBox1.Location = new System.Drawing.Point(521, 11);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(118, 66);
            this.groupBox1.TabIndex = 15;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Translate";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(88, 44);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(12, 13);
            this.label3.TabIndex = 5;
            this.label3.Text = "z";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(52, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(12, 13);
            this.label2.TabIndex = 4;
            this.label2.Text = "y";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 44);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(12, 13);
            this.label1.TabIndex = 3;
            this.label1.Text = "x";
            // 
            // numericUpDown3
            // 
            this.numericUpDown3.Location = new System.Drawing.Point(84, 21);
            this.numericUpDown3.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown3.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown3.Name = "numericUpDown3";
            this.numericUpDown3.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown3.TabIndex = 2;
            this.numericUpDown3.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // numericUpDown2
            // 
            this.numericUpDown2.Location = new System.Drawing.Point(48, 21);
            this.numericUpDown2.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown2.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown2.Name = "numericUpDown2";
            this.numericUpDown2.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown2.TabIndex = 1;
            this.numericUpDown2.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // numericUpDown1
            // 
            this.numericUpDown1.Location = new System.Drawing.Point(10, 21);
            this.numericUpDown1.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown1.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown1.Name = "numericUpDown1";
            this.numericUpDown1.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown1.TabIndex = 0;
            this.numericUpDown1.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.label5);
            this.groupBox2.Controls.Add(this.label6);
            this.groupBox2.Controls.Add(this.numericUpDown4);
            this.groupBox2.Controls.Add(this.numericUpDown5);
            this.groupBox2.Controls.Add(this.numericUpDown6);
            this.groupBox2.Location = new System.Drawing.Point(521, 93);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(118, 66);
            this.groupBox2.TabIndex = 16;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rotate";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(88, 44);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(12, 13);
            this.label4.TabIndex = 5;
            this.label4.Text = "z";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(52, 44);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(12, 13);
            this.label5.TabIndex = 4;
            this.label5.Text = "y";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(13, 44);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(12, 13);
            this.label6.TabIndex = 3;
            this.label6.Text = "x";
            // 
            // numericUpDown4
            // 
            this.numericUpDown4.Location = new System.Drawing.Point(10, 21);
            this.numericUpDown4.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown4.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown4.Name = "numericUpDown4";
            this.numericUpDown4.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown4.TabIndex = 2;
            this.numericUpDown4.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // numericUpDown5
            // 
            this.numericUpDown5.Location = new System.Drawing.Point(48, 21);
            this.numericUpDown5.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown5.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown5.Name = "numericUpDown5";
            this.numericUpDown5.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown5.TabIndex = 1;
            this.numericUpDown5.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // numericUpDown6
            // 
            this.numericUpDown6.Location = new System.Drawing.Point(84, 21);
            this.numericUpDown6.Maximum = new decimal(new int[] {
            10000,
            0,
            0,
            0});
            this.numericUpDown6.Minimum = new decimal(new int[] {
            10000,
            0,
            0,
            -2147483648});
            this.numericUpDown6.Name = "numericUpDown6";
            this.numericUpDown6.Size = new System.Drawing.Size(19, 20);
            this.numericUpDown6.TabIndex = 0;
            this.numericUpDown6.ValueChanged += new System.EventHandler(this.numericUpDownValueChanged);
            // 
            // secretBoxElevationTimer
            // 
            this.secretBoxElevationTimer.Enabled = true;
            this.secretBoxElevationTimer.Tick += new System.EventHandler(this.secretBoxElevationTimer_Tick);
            // 
            // currentPlayersPointsPanel
            // 
            this.currentPlayersPointsPanel.BackColor = System.Drawing.Color.Crimson;
            this.currentPlayersPointsPanel.Controls.Add(this.currentPlayerInfoLabel);
            this.currentPlayersPointsPanel.Dock = System.Windows.Forms.DockStyle.Left;
            this.currentPlayersPointsPanel.Location = new System.Drawing.Point(0, 0);
            this.currentPlayersPointsPanel.Name = "currentPlayersPointsPanel";
            this.currentPlayersPointsPanel.Size = new System.Drawing.Size(221, 15);
            this.currentPlayersPointsPanel.TabIndex = 17;
            // 
            // currentPlayerInfoLabel
            // 
            this.currentPlayerInfoLabel.AutoSize = true;
            this.currentPlayerInfoLabel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.currentPlayerInfoLabel.Location = new System.Drawing.Point(0, 0);
            this.currentPlayerInfoLabel.Name = "currentPlayerInfoLabel";
            this.currentPlayerInfoLabel.Size = new System.Drawing.Size(13, 13);
            this.currentPlayerInfoLabel.TabIndex = 20;
            this.currentPlayerInfoLabel.Text = "a";
            // 
            // computerPlayerPointsPanel
            // 
            this.computerPlayerPointsPanel.BackColor = System.Drawing.Color.Crimson;
            this.computerPlayerPointsPanel.Controls.Add(this.secondPlayerInfoLabel);
            this.computerPlayerPointsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.computerPlayerPointsPanel.Location = new System.Drawing.Point(221, 0);
            this.computerPlayerPointsPanel.Name = "computerPlayerPointsPanel";
            this.computerPlayerPointsPanel.Size = new System.Drawing.Size(432, 15);
            this.computerPlayerPointsPanel.TabIndex = 19;
            // 
            // humanPlayerPointsPanel
            // 
            this.humanPlayerPointsPanel.BackColor = System.Drawing.Color.Crimson;
            this.humanPlayerPointsPanel.Controls.Add(this.firstPlayerInfoLabel);
            this.humanPlayerPointsPanel.Dock = System.Windows.Forms.DockStyle.Right;
            this.humanPlayerPointsPanel.Location = new System.Drawing.Point(441, 0);
            this.humanPlayerPointsPanel.Name = "humanPlayerPointsPanel";
            this.humanPlayerPointsPanel.Size = new System.Drawing.Size(212, 15);
            this.humanPlayerPointsPanel.TabIndex = 20;
            // 
            // bottomPanel
            // 
            this.bottomPanel.Controls.Add(this.humanPlayerPointsPanel);
            this.bottomPanel.Controls.Add(this.computerPlayerPointsPanel);
            this.bottomPanel.Controls.Add(this.currentPlayersPointsPanel);
            this.bottomPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.bottomPanel.Location = new System.Drawing.Point(0, 513);
            this.bottomPanel.Name = "bottomPanel";
            this.bottomPanel.Size = new System.Drawing.Size(653, 15);
            this.bottomPanel.TabIndex = 21;
            // 
            // FormGameBoard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(653, 528);
            this.Controls.Add(this.bottomPanel);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.panel1);
            this.KeyPreview = true;
            this.Name = "FormGameBoard";
            this.Text = "Memory Game";
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.FormGameBoard_KeyDown);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.FormGameBoard_KeyPress);
            this.Resize += new System.EventHandler(this.FormGameBoard_Resize);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown6)).EndInit();
            this.currentPlayersPointsPanel.ResumeLayout(false);
            this.currentPlayersPointsPanel.PerformLayout();
            this.computerPlayerPointsPanel.ResumeLayout(false);
            this.computerPlayerPointsPanel.PerformLayout();
            this.humanPlayerPointsPanel.ResumeLayout(false);
            this.humanPlayerPointsPanel.PerformLayout();
            this.bottomPanel.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.NumericUpDown numericUpDown1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown numericUpDown3;
        private System.Windows.Forms.NumericUpDown numericUpDown2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.NumericUpDown numericUpDown4;
        private System.Windows.Forms.NumericUpDown numericUpDown5;
        private System.Windows.Forms.NumericUpDown numericUpDown6;
        private System.Windows.Forms.Timer secretBoxElevationTimer;
        private System.Windows.Forms.Panel currentPlayersPointsPanel;
        private System.Windows.Forms.Label secondPlayerInfoLabel;
        private System.Windows.Forms.Label firstPlayerInfoLabel;
        private System.Windows.Forms.Label currentPlayerInfoLabel;
        private System.Windows.Forms.Panel computerPlayerPointsPanel;
        private System.Windows.Forms.Panel humanPlayerPointsPanel;
        private System.Windows.Forms.Panel bottomPanel;
    }
}