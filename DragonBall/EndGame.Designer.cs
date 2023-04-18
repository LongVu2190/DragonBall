namespace DragonBall
{
    partial class EndGame
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
            this.Exit_btn = new System.Windows.Forms.Button();
            this.Play_btn = new System.Windows.Forms.Button();
            this.Main_PBox = new System.Windows.Forms.PictureBox();
            this.Animation = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.Main_PBox)).BeginInit();
            this.SuspendLayout();
            // 
            // Exit_btn
            // 
            this.Exit_btn.Location = new System.Drawing.Point(419, 176);
            this.Exit_btn.Name = "Exit_btn";
            this.Exit_btn.Size = new System.Drawing.Size(49, 37);
            this.Exit_btn.TabIndex = 0;
            this.Exit_btn.Text = "Exit";
            this.Exit_btn.UseVisualStyleBackColor = true;
            this.Exit_btn.Click += new System.EventHandler(this.Exit_btn_Click);
            // 
            // Play_btn
            // 
            this.Play_btn.Location = new System.Drawing.Point(491, 176);
            this.Play_btn.Name = "Play_btn";
            this.Play_btn.Size = new System.Drawing.Size(71, 37);
            this.Play_btn.TabIndex = 1;
            this.Play_btn.Text = "Play again";
            this.Play_btn.UseVisualStyleBackColor = true;
            this.Play_btn.Click += new System.EventHandler(this.Play_btn_Click);
            // 
            // Main_PBox
            // 
            this.Main_PBox.Location = new System.Drawing.Point(0, 0);
            this.Main_PBox.Name = "Main_PBox";
            this.Main_PBox.Size = new System.Drawing.Size(654, 369);
            this.Main_PBox.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Main_PBox.TabIndex = 2;
            this.Main_PBox.TabStop = false;
            // 
            // Animation
            // 
            this.Animation.Enabled = true;
            this.Animation.Interval = 300;
            this.Animation.Tick += new System.EventHandler(this.Animation_Tick);
            // 
            // EndGame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(654, 368);
            this.Controls.Add(this.Play_btn);
            this.Controls.Add(this.Exit_btn);
            this.Controls.Add(this.Main_PBox);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "EndGame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "EndGame";
            this.Load += new System.EventHandler(this.EndGame_Load);
            ((System.ComponentModel.ISupportInitialize)(this.Main_PBox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Exit_btn;
        private System.Windows.Forms.Button Play_btn;
        private System.Windows.Forms.PictureBox Main_PBox;
        private System.Windows.Forms.Timer Animation;
    }
}