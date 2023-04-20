namespace DragonBall
{
    partial class Login
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Login));
            this.Start_btn = new System.Windows.Forms.Button();
            this.Tutorial_btn = new System.Windows.Forms.Button();
            this.About_btn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // Start_btn
            // 
            this.Start_btn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Start_btn.Location = new System.Drawing.Point(122, 12);
            this.Start_btn.Name = "Start_btn";
            this.Start_btn.Size = new System.Drawing.Size(98, 59);
            this.Start_btn.TabIndex = 0;
            this.Start_btn.Text = "Start";
            this.Start_btn.UseVisualStyleBackColor = true;
            this.Start_btn.Click += new System.EventHandler(this.Start_btn_Click);
            // 
            // Tutorial_btn
            // 
            this.Tutorial_btn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Tutorial_btn.Location = new System.Drawing.Point(284, 12);
            this.Tutorial_btn.Name = "Tutorial_btn";
            this.Tutorial_btn.Size = new System.Drawing.Size(98, 59);
            this.Tutorial_btn.TabIndex = 1;
            this.Tutorial_btn.Text = "Tutorial";
            this.Tutorial_btn.UseVisualStyleBackColor = true;
            this.Tutorial_btn.Click += new System.EventHandler(this.Tutorial_btn_Click);
            // 
            // About_btn
            // 
            this.About_btn.Font = new System.Drawing.Font("Segoe UI", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.About_btn.Location = new System.Drawing.Point(451, 12);
            this.About_btn.Name = "About_btn";
            this.About_btn.Size = new System.Drawing.Size(98, 59);
            this.About_btn.TabIndex = 2;
            this.About_btn.Text = "About";
            this.About_btn.UseVisualStyleBackColor = true;
            this.About_btn.Click += new System.EventHandler(this.About_btn_Click);
            // 
            // Login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(684, 361);
            this.Controls.Add(this.About_btn);
            this.Controls.Add(this.Tutorial_btn);
            this.Controls.Add(this.Start_btn);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "Login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Dragon Ball";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button Start_btn;
        private System.Windows.Forms.Button Tutorial_btn;
        private System.Windows.Forms.Button About_btn;
    }
}