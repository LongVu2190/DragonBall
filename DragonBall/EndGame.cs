using DragonBall.Objects;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonBall
{
    public partial class EndGame : Form
    {
        DragonBall dra = new DragonBall();
        int status;
        int stepFrame, startFrame, endFrame;

        List<string> imageMovements = new List<string>();

        public EndGame(DragonBall dra, int status)
        {
            InitializeComponent();
            this.dra = dra;
            this.status = status;
        }
        public EndGame()
        {
            InitializeComponent();
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

        private void EndGame_Load(object sender, EventArgs e)
        {
            Exit_btn.Enabled = false;
            Exit_btn.Visible = false;
            Play_btn.Enabled = false;
            Play_btn.Visible = false;
            if (status == 1) // Win
            {
                imageMovements = Directory.GetFiles("assets/win", "*.jpg").ToList();
                stepFrame = 0;
                startFrame = 0;
                endFrame = 25;
            }
            else if (status == 2) // Lose
            {
                imageMovements = Directory.GetFiles("assets/lose", "*.jpg").ToList();
                stepFrame = 0;
                startFrame = 0;
                endFrame = 7;
            }
        }

        private void Animation_Tick(object sender, EventArgs e)
        {
            if (stepFrame > endFrame || stepFrame < startFrame)
            {
                stepFrame = startFrame;
            }
            stepFrame++;

            Console.WriteLine("stepFrame" + stepFrame);
            Main_PBox.Image = Image.FromFile(imageMovements[stepFrame]);

            if (stepFrame == endFrame)
            {
                Exit_btn.Enabled = true;
                Exit_btn.Visible = true;
                Play_btn.Enabled = true;
                Play_btn.Visible = true;
                Animation.Enabled = false;
            }
        }
    }
    
}
