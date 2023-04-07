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
        List<Bullets> bullets = new List<Bullets>(); // List chứa đạn
        Image player;

        List<string> playerMovements = new List<string>(); // List này chứa các hình chuyển động

        int steps = 0; // Index để thay đổi hình

        int start, end; // Khoảng hình để làm animation
        int slowDownFrameRate = 0; // Giảm FPS xuống (Di chuyển FPS cao, Hoạt ảnh FPS thấp)
        int maxSlowDownFrameRate = 6;

        int delayShoot = 0; // Thời gian giữa những lần bắn
        int score = 0; // Điểm

        bool goLeft, goRight, goUp, goDown;
        bool isLocked = false;

        int playerX; // Tọa độ X của player
        int playerY = 0; //Tọa độ Y của player

        int bulletX, bulletY;  
        int bulletWidth = 50, bulletHeight = 30;

        int form = 1; // Dạng của goku
        bool isTransform = false, isShot = false;

        static int playerHeight = 200;
        static int playerWidth = 200;
        
        static int playerSpeed = 10;
        static int bulletSpeed = 10;

        public DragonBall()
        {
            InitializeComponent();
            SetUp();
            AllocConsole();
        }

        private void SetUp()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            // Lấy hết hình trong thư mục Goku (Nằm ở thư mục debug)
            playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();

            // Lấy hình thứ 9 trong thư mục Goku
            player = Image.FromFile(playerMovements[10]);
        }
        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (delayShoot != 10) // Tăng thời gian giữa những lần bắn
            {
                delayShoot++;
            }
            if (bullets != null) // Vẽ đạn nếu list đạn không rỗng
            {
                AnimateBullet();
            }
            //// Di chuyển lên phải
            //if (goUp && goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerWidth && (playerY - playerSpeed) > 0)
            //{
            //    playerY -= playerSpeed;
            //    playerX += playerSpeed;
            //    SetStartEnd(form, Enums.Move.Right);
            //    AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            //}

            //// Di chuyển lên trái
            //else if (goUp && goLeft && (playerX - playerSpeed) > 0 && (playerX - playerSpeed) > 0 && (playerY - playerSpeed) > 0)
            //{
            //    playerY -= playerSpeed;
            //    playerX -= playerSpeed;
            //    SetStartEnd(form, Enums.Move.Left);
            //    AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            //}

            //// Di chuyển xuống phải
            //else if (goDown && goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerHeight && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            //{
            //    playerY += playerSpeed;
            //    playerX += playerSpeed;
            //    SetStartEnd(form, Enums.Move.Right);
            //    AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            //}

            //// Di chuyển xuống trái
            //else if (goDown && goLeft && (playerX - playerSpeed) > 0 && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            //{
            //    playerY += playerSpeed;
            //    playerX -= playerSpeed;
            //    SetStartEnd(form, Enums.Move.Left);
            //    AnimatePlayer(); // Lấy các hình index từ 0 -> 2 để làm hoạt ảnh cho nhân vật
            //}

            // Di chuyển lên
            if (goUp && (playerY - playerSpeed) > 0)
            {
                playerY -= playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển xuống
            if (goDown && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            {  
                playerY += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển trái
            if (goLeft && (playerX - playerSpeed) > 0)
            {
                playerX -= playerSpeed;
                SetStartEnd(form, Enums.Move.Left);
                AnimatePlayer();
            }

            // Di chuyển phải
            if (goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerWidth)
            {
                playerX += playerSpeed;
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }

            if (!isTransform && !goLeft)
            {
                SetStartEnd(form, Enums.Move.Right);
                AnimatePlayer();
            }
            else if (isTransform)
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


            if (bullets == null) return;

            // Vẽ đạn
            foreach (var bullet in bullets)
            {
                if (bullet.isMoving)
                {
                    Canvas.DrawImage(bullet.Image, bullet.X, bullet.Y, bullet.Width, bullet.Height);

                    // Tạo 1 biến picturebox tạm để xài hàm IntersectWith
                    PictureBox hit = new PictureBox();
                    hit.Location = new System.Drawing.Point(bullet.X, bullet.Y);
                    hit.Size = new System.Drawing.Size(bullet.Width, bullet.Height);

                    if (hit.Bounds.IntersectsWith(test.Bounds) && !bullet.isHit)
                    {
                        bullet.isHit = true;
                        score += 1;
                        score_lb.Text = score.ToString();
                    }

                }
            }
        }

        private void AnimateBullet()
        {
            if (bullets == null) return;

            for (int i = 0; i <  bullets.Count; i++)
            {
                if (bullets[i].X + bullets[i].Width < this.ClientSize.Width) // Bay trong màn hình
                {
                    bullets[i].X += bulletSpeed;
                    bullets[i].isMoving = true;
                }
                else // Bay hết màn hình thì loại nó ra khỏi list bullets
                {
                    bullets.RemoveAt(i);
                    i--;
                }
            }
        }
        private void AnimatePlayer()
        {           
            slowDownFrameRate += 1;
            if (slowDownFrameRate == maxSlowDownFrameRate) // Giảm FPS của hoạt ảnh nhân vật xuống
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
                isShot = false;
                isTransform = false;
                isLocked = false;
            }
            player = Image.FromFile(playerMovements[steps]);
        }
        
        // Để lựa chọn ảnh nhân vật sẽ được vẽ lên
        private void SetStartEnd(int form, Enums.Move status)
        {
            if (isShot)
            {
                start = 6;
                end = 8;
            }
            else if (form == 0 && isTransform)
            {
                start = 10;
                end = 16;
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

            else if (form == 1 && isTransform)
            {
                start = 10;
                end = 17;
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
            
            else if (form == 2 && isTransform)
            {
                start = 10;
                end = 20;
            }
            else if (form == 2 && status == Enums.Move.Right)
            {
                start = 0;
                end = 2;
            }
            else if (form == 2 && status == Enums.Move.Left)
            {
                start = 3;
                end = 5;
            }

            else if (form == 3 && isTransform)
            {
                start = 10;
                end = 27;
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

            else if (form == 4 && isTransform)
            {
                start = 10;
                end = 22;
            }
            else if (form == 4 && status == Enums.Move.Right)
            {
                start = 0;
                end = 2;
            }
            else if (form == 4 && status == Enums.Move.Left)
            {
                start = 3;
                end = 5;
            }
        }
        private void DragonBall_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLocked) return;
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
                SetNoMove(); // Khóa di chuyển và bắn trong lúc biến hình
                playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();
                isTransform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 0;
            }
            if (e.KeyCode == Keys.D2)
            {
                SetNoMove();
                playerMovements = Directory.GetFiles("Goku1", "*.png").ToList();
                isTransform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 1;
            }
            if (e.KeyCode == Keys.D3)
            {
                SetNoMove();
                playerMovements = Directory.GetFiles("Goku2", "*.png").ToList();
                isTransform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 2;
            }
            if (e.KeyCode == Keys.D4)
            {
                SetNoMove();
                playerMovements = Directory.GetFiles("Goku3", "*.png").ToList();
                isTransform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 3;
            }
            if (e.KeyCode == Keys.D5)
            {
                SetNoMove();
                playerMovements = Directory.GetFiles("Goku4", "*.png").ToList();
                isTransform = true;
                slowDownFrameRate = 0;
                steps = 0;
                form = 4;
            }
            if (e.KeyCode == Keys.Space && delayShoot == 10)
            {
                delayShoot = 0;
                bulletX = playerX + playerWidth;
                bulletY = playerY + playerHeight / 2 + 20;

                Bullets a = new Bullets();
                a.X = bulletX;
                a.Y = bulletY;
                a.Width = bulletWidth;
                a.Height = bulletHeight;
                a.Image = Image.FromFile(playerMovements[9]);
                a.isMoving = true;
                
                bullets.Add(a);

                isShot = true;
                slowDownFrameRate = 0;
                steps = 0;
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

        private void EndGame()
        {

        }
        private void SetNoMove()
        {
            isLocked = true;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
        }
    }
}
