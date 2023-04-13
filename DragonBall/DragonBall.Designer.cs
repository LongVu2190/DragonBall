namespace DragonBall
{
    partial class DragonBall
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DragonBall));
            this.Moving = new System.Windows.Forms.Timer(this.components);
            this.Level = new System.Windows.Forms.Timer(this.components);
            this.Enemy = new System.Windows.Forms.Timer(this.components);
            this.Player_Progress = new System.Windows.Forms.ProgressBar();
            this.Enemy_Progress = new System.Windows.Forms.ProgressBar();
            this.Avatar = new System.Windows.Forms.PictureBox();
            this.Level_Progress = new System.Windows.Forms.ProgressBar();
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).BeginInit();
            this.SuspendLayout();
            // 
            // Moving
            // 
            this.Moving.Interval = 10;
            this.Moving.Tick += new System.EventHandler(this.Moving_Tick);
            // 
            // Level
            // 
            this.Level.Interval = 20;
            this.Level.Tick += new System.EventHandler(this.Level_Tick);
            // 
            // Enemy
            // 
            this.Enemy.Interval = 120;
            this.Enemy.Tick += new System.EventHandler(this.Enemy_Tick);
            // 
            // Player_Progress
            // 
            this.Player_Progress.Location = new System.Drawing.Point(127, 128);
            this.Player_Progress.Maximum = 10;
            this.Player_Progress.Name = "Player_Progress";
            this.Player_Progress.Size = new System.Drawing.Size(189, 29);
            this.Player_Progress.TabIndex = 2;
            this.Player_Progress.Value = 10;
            // 
            // Enemy_Progress
            // 
            this.Enemy_Progress.Location = new System.Drawing.Point(852, 66);
            this.Enemy_Progress.Maximum = 2;
            this.Enemy_Progress.Name = "Enemy_Progress";
            this.Enemy_Progress.Size = new System.Drawing.Size(189, 29);
            this.Enemy_Progress.TabIndex = 3;
            this.Enemy_Progress.Value = 2;
            // 
            // Avatar
            // 
            this.Avatar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Avatar.Image = ((System.Drawing.Image)(resources.GetObject("Avatar.Image")));
            this.Avatar.Location = new System.Drawing.Point(9, 12);
            this.Avatar.Name = "Avatar";
            this.Avatar.Size = new System.Drawing.Size(100, 145);
            this.Avatar.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Avatar.TabIndex = 4;
            this.Avatar.TabStop = false;
            // 
            // Level_Progress
            // 
            this.Level_Progress.Location = new System.Drawing.Point(127, 79);
            this.Level_Progress.Maximum = 5;
            this.Level_Progress.Name = "Level_Progress";
            this.Level_Progress.Size = new System.Drawing.Size(189, 29);
            this.Level_Progress.Step = 1;
            this.Level_Progress.TabIndex = 5;
            // 
            // DragonBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1148, 791);
            this.Controls.Add(this.Level_Progress);
            this.Controls.Add(this.Avatar);
            this.Controls.Add(this.Enemy_Progress);
            this.Controls.Add(this.Player_Progress);
            this.Name = "DragonBall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Ball";
            this.Load += new System.EventHandler(this.DragonBall_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DragonBall_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.Avatar)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer Moving;
        private System.Windows.Forms.Timer Level;
        private System.Windows.Forms.Timer Enemy;
        private System.Windows.Forms.ProgressBar Player_Progress;
        private System.Windows.Forms.ProgressBar Enemy_Progress;
        private System.Windows.Forms.PictureBox Avatar;
        private System.Windows.Forms.ProgressBar Level_Progress;
    }
}

