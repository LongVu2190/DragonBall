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
            this.Timer = new System.Windows.Forms.Timer(this.components);
            this.test = new System.Windows.Forms.PictureBox();
            this.score_lb = new System.Windows.Forms.Label();
            this.Level = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.test)).BeginInit();
            this.SuspendLayout();
            // 
            // Timer
            // 
            this.Timer.Enabled = true;
            this.Timer.Interval = 10;
            this.Timer.Tick += new System.EventHandler(this.Timer_Tick);
            // 
            // test
            // 
            this.test.Location = new System.Drawing.Point(710, 145);
            this.test.Name = "test";
            this.test.Size = new System.Drawing.Size(100, 144);
            this.test.TabIndex = 0;
            this.test.TabStop = false;
            // 
            // score_lb
            // 
            this.score_lb.AutoSize = true;
            this.score_lb.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.score_lb.Location = new System.Drawing.Point(309, 65);
            this.score_lb.Name = "score_lb";
            this.score_lb.Size = new System.Drawing.Size(70, 25);
            this.score_lb.TabIndex = 1;
            this.score_lb.Text = "label1";
            // 
            // Level
            // 
            this.Level.Enabled = true;
            this.Level.Interval = 20;
            this.Level.Tick += new System.EventHandler(this.Level_Tick);
            // 
            // DragonBall
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundImage = global::DragonBall.Properties.Resources._1940_7864_6519v2;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(884, 461);
            this.Controls.Add(this.score_lb);
            this.Controls.Add(this.test);
            this.Name = "DragonBall";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Ball";
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.DragonBall_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.DragonBall_KeyUp);
            ((System.ComponentModel.ISupportInitialize)(this.test)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Timer Timer;
        private System.Windows.Forms.PictureBox test;
        private System.Windows.Forms.Label score_lb;
        private System.Windows.Forms.Timer Level;
    }
}

