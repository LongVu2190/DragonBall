using DragonBall.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DragonBall
{
    public partial class DragonBall : Form
    {      
        Image player;
        List<string> playerMovements = new List<string>(); // List này chứa các hình chuyển động

        int steps = 0; // Index để thay đổi hình

        int start, end;
        int slowDownFrameRate = 0; // Giảm FPS xuống (Di chuyển FPS cao, Hoạt ảnh FPS thấp)
        int MaxSlowDownFrameRate = 6;

        bool goLeft, goRight, goUp, goDown;

        int playerX; // Tọa độ X của player
        int playerY = 0; //Tọa độ Y của player

        int form = 1;
        bool transform = false, isStart = false;

        static int playerHeight = 200;
        static int playerWidth = 200;
        
        int playerSpeed = 10;

        public DragonBall()
        {
            InitializeComponent();
            SetUp();
            //AllocConsole();
        }

        private void SetUp()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            // Lấy hết hình trong thư mục Goku (Nằm ở thư mục debug)
            playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();

            // Lấy hình đầu tiên
            player = Image.FromFile(playerMovements[6]);
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void Timer_Tick(object sender, EventArgs e)
        {
            // Di chuyển lên phải
            if (goUp && goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerWidth && (playerY - playerSpeed) > 0)
            {
                playerY -= playerSpeed;
                playerX += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            }

            // Di chuyển lên trái
            else if (goUp && goLeft && (playerX - playerSpeed) > 0 && (playerX - playerSpeed) > 0 && (playerY - playerSpeed) > 0)
            {
                playerY -= playerSpeed;
                playerX -= playerSpeed;
                SetStartEnd(form, Enums.Move.Left);
                AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            }

            // Di chuyển xuống phải
            else if (goDown && goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerHeight && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            {
                playerY += playerSpeed;
                playerX += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            }

            // Di chuyển xuống trái
            else if (goDown && goLeft && (playerX - playerSpeed) > 0 && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            {
                playerY += playerSpeed;
                playerX -= playerSpeed;
                SetStartEnd(form, Enums.Move.Left);
                AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            }

            // Di chuyển lên
            else if (goUp && (playerY - playerSpeed) > 0)
            {
                playerY -= playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển xuống
            else if (goDown && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            {  
                playerY += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển trái
            else if (goLeft && (playerX - playerSpeed) > 0)
            {
                playerX -= playerSpeed;
                SetStartEnd(form, Enums.Move.Left);
                AnimatePlayer();
            }

            // Di chuyển phải
            else if (goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerWidth)
            {
                playerX += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            else if (!transform && isStart)
            {
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }
            else if (transform)
            {
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();               
            }

            this.Invalidate();
        }
        private void DragonBall_Paint(object sender, PaintEventArgs e)
        {
            // Vẽ nhân vật
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(player, playerX, playerY, playerWidth, playerHeight);
        }

        private void AnimatePlayer()
        {           
            slowDownFrameRate += 1;
            if (slowDownFrameRate == MaxSlowDownFrameRate) // Giảm FPS của hoạt ảnh nhân vật xuống 6 lần
            {
                steps++;
                slowDownFrameRate = 0;
            }
            if (steps > end || steps < start) // Đảm bảo lấy hình trong khoảng index từ start -> end
            {
                steps = start;
            }
            if (steps == end)
            {
                transform = false;
            }
            player = Image.FromFile(playerMovements[steps]);
        }
        private void SetStartEnd(int form, Enums.Move status)
        {
            if (form == 0 && transform)
            {
                start = 6;
                end = 12;
            }
            else if (form == 0 && status == Enums.Move.Right)
            {
                start = 0;
                end = 2;
            }
            else if (form == 0 && status == Enums.Move.Left)
            {
                start = 3;
                end = 5;
            }

            else if (form == 1 && transform)
            {
                start = 6;
                end = 13;
            }
            else if (form == 1 && status == Enums.Move.Right)
            {
                start = 0;
                end = 2;
            }
            else if (form == 1 && status == Enums.Move.Left)
            {
                start = 3;
                end = 5;
            }         
            
            else if (form == 2 && transform)
            {
                start = 0;
                end = 10;
            }
            else if (form == 2 && status == Enums.Move.Right)
            {
                start = 11;
                end = 13;
            }
            else if (form == 2 && status == Enums.Move.Left)
            {
                start = 14;
                end = 16;
            }

            else if (form == 3 && transform)
            {
                start = 6;
                end = 21;
            }
            else if (form == 3 && status == Enums.Move.Right)
            {
                start = 0;
                end = 2;
            }
            else if (form == 3 && status == Enums.Move.Left)
            {
                start = 3;
                end = 5;
            }
        }
        private void DragonBall_KeyDown(object sender, KeyEventArgs e)
        {
            // Nhấn phím thì là true
            if (e.KeyCode == Keys.A)
            {
                goLeft = true;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = true;
            }
            if (e.KeyCode == Keys.W)
            {
                goUp = true;
            }
            if (e.KeyCode == Keys.S)
            {
                goDown = true;
            }
            if (e.KeyCode == Keys.D1)
            {
                isStart = true;      
                playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();
                transform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 0;
            }
            if (e.KeyCode == Keys.D2)
            {
                playerMovements = Directory.GetFiles("Goku1", "*.png").ToList();
                transform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 1;
            }
            if (e.KeyCode == Keys.D3)
            {
                playerMovements = Directory.GetFiles("Goku2", "*.png").ToList();
                transform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 2;
            }
            if (e.KeyCode == Keys.D4)
            {
                playerMovements = Directory.GetFiles("Goku3", "*.png").ToList();
                transform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 3;
            }
        }
        private void DragonBall_KeyUp(object sender, KeyEventArgs e)
        {
            // Thả phím thì là false
            if (e.KeyCode == Keys.A)
            {
                goLeft = false;
            }
            if (e.KeyCode == Keys.D)
            {
                goRight = false;
            }
            if (e.KeyCode == Keys.W)
            {
                goUp = false;
            }
            if (e.KeyCode == Keys.S)
            {
                goDown = false;
            }
        }
    }
}
