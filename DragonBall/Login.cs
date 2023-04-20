using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonBall
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
            this.BackgroundImage = Properties.Resources.Wall1;
        }

        private void Start_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            new DragonBall {}.ShowDialog();
            base.Close();
        }
        private void Tutorial_btn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Wall2;
        }
        private void About_btn_Click(object sender, EventArgs e)
        {
            this.BackgroundImage = Properties.Resources.Wall3;
        }
      
    }
}
