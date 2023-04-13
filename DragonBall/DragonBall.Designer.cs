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
            this.Moving = new System.Windows.Forms.Timer(this.components);
            this.score_lb = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.Timer(this.components);
            this.Enemy = new System.Windows.Forms.Timer(this.components);
            this.player_health = new System.Windows.Forms.ProgressBar();
            this.enemy_health = new System.Windows.Forms.ProgressBar();
            this.SuspendLayout();
            // 
            // Moving
            // 
            this.Moving.Interval = 10;
            this.Moving.Tick += new System.EventHandler(this.Moving_Tick);
            // 
            // score_lb
            // 
            this.score_lb.AutoSize = true;
            this.score_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_lb.Location = new System.Drawing.Point(529, 66);
            this.score_lb.Name = "score_lb";
            this.score_lb.Size = new System.Drawing.Size(61, 25);
            this.score_lb.TabIndex = 1;
            this.score_lb.Text = "Point";
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
            // player_health
            // 
            this.player_health.Location = new System.Drawing.Point(112, 62);
            this.player_health.Maximum = 10;
            this.player_health.Name = "player_health";
            this.player_health.Size = new System.Drawing.Size(189, 29);
            this.player_health.TabIndex = 2;
            this.player_health.Value = 10;
            // 
            // enemy_health
            // 
            this.enemy_health.Location = new System.Drawing.Point(852, 66);
            this.enemy_health.Maximum = 2;
            this.enemy_health.Name = "enemy_health";
            this.enemy_health.Size = new System.Drawing.Size(189, 29);
            this.enemy_health.TabIndex = 3;
            this.enemy_health.Value = 2;
            // 
            // DragonBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DragonBall.Properties.Resources._1940_7864_6519v2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(1148, 791);
            this.Controls.Add(this.enemy_health);
            this.Controls.Add(this.player_health);
            this.Controls.Add(this.score_lb);
            this.Name = "DragonBall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Ball";
            this.Load += new System.EventHandler(this.DragonBall_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DragonBall_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyUp);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Moving;
        private System.Windows.Forms.Label score_lb;
        private System.Windows.Forms.Timer Level;
        private System.Windows.Forms.Timer Enemy;
        private System.Windows.Forms.ProgressBar player_health;
        private System.Windows.Forms.ProgressBar enemy_health;
    }
}

