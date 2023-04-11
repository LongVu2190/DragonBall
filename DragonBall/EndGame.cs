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
    public partial class EndGame : Form
    {
        DragonBall dra = new DragonBall();
        public EndGame(DragonBall dra)
        {
            InitializeComponent();
            this.dra = dra;
        }

        private void Exit_btn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void Play_btn_Click(object sender, EventArgs e)
        {
            this.Hide();
            dra.StartGame();         
        }
    }
}
