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
        bool isStart = false, isEnd = false;
        List<Bullets> bullets = new List<Bullets>(); // List chứa đạn
        Image player;

        List<string> playerMovements = new List<string>(); // List này chứa các hình chuyển động

        int stepFrame = 0; // Index để thay đổi hình
        int startFrame, endFrame; // Khoảng hình để làm animation
        int slowDownFrameRate = 0; // Giảm FPS xuống (Di chuyển FPS cao, Hoạt ảnh FPS thấp)
        int maxSlowDownFrameRate = 6; // Giảm FPS xuống 6 lần

        int delayShoot = 0, delayShootTime = 15; // Thời gian giữa những lần bắn
        int score = 0; // Điểm

        bool goLeft, goRight, goUp, goDown;
        bool isLocked = false; // Khóa di chuyển và bắn

        int playerX; // Tọa độ X của player
        int playerY = 0; //Tọa độ Y của player

        int form = 1; // Dạng của goku
        bool isTransform = false, isShot = false;

        static int playerHeight = 200, playerWidth = 200, playerSpeed = 10;
        static int bulletHeight = 30, bulletWidth = 50, bulletSpeed = 10;

        public DragonBall()
        {
            InitializeComponent();
            AllocConsole();
            StartGame();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();
        private void DragonBall_Paint(object sender, PaintEventArgs e)
        {
            if (!isStart) return;
            if (isEnd)
            {
                EndGame();
            }
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

        // Nếu đủ score thì biến hình lên cấp
        private void Level_Tick(object sender, EventArgs e)
        {
            if (!isStart) return;

            if (score == 0)
            {
                playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();
                Transformation(0, 15);
                score++;
            }
            else if (score == 10)
            {
                playerMovements = Directory.GetFiles("Goku1", "*.png").ToList();
                Transformation(1, 13);
                score++;
            }
            else if (score == 20)
            {
                playerMovements = Directory.GetFiles("Goku2", "*.png").ToList();
                Transformation(2, 10);
                score++;
            }
            else if (score == 30)
            {
                playerMovements = Directory.GetFiles("Goku3", "*.png").ToList();
                Transformation(3, 8);
                score++;
            }
            else if (score == 40)
            {
                playerMovements = Directory.GetFiles("Goku4", "*.png").ToList();
                Transformation(4, 6);
                score++;
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (delayShoot != delayShootTime) // Tăng thời gian giữa những lần bắn
            {
                delayShoot++;
            }

            if (bullets != null) // Vẽ đạn nếu list đạn không rỗng
            {
                AnimateBullet();
            }

            if (isTransform)
            {
                SetFramePlayer(form, Enums.Move.Right);
                AnimatePlayer();
            }
            else
            {
                SetFramePlayer(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển lên
            if (goUp && (playerY - playerSpeed) > 0)
            {
                playerY -= playerSpeed;
                SetFramePlayer(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển xuống
            if (goDown && (playerY + playerSpeed) < this.ClientSize.Height - playerHeight)
            {
                playerY += playerSpeed;
                SetFramePlayer(form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển trái
            if (goLeft && (playerX - playerSpeed) > 0)
            {
                playerX -= playerSpeed;
                SetFramePlayer(form, Enums.Move.Left);
                AnimatePlayer();
            }

            // Di chuyển phải
            if (goRight && (playerX + playerSpeed) < this.ClientSize.Width - playerWidth)
            {
                playerX += playerSpeed;
                SetFramePlayer(form, Enums.Move.Right);
                AnimatePlayer();
            }

            this.Invalidate();
        }

        private void AnimateBullet()
        {
            if (bullets == null) return;

            for (int i = 0; i < bullets.Count; i++)
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
                stepFrame++;
                slowDownFrameRate = 0;
            }
            if (stepFrame > endFrame || stepFrame < startFrame) // Đảm bảo lấy hình trong khoảng index từ startFrame -> endFrame
            {
                stepFrame = startFrame;
            }
            if (stepFrame == endFrame)
            {
                isShot = false;
                isTransform = false;
                isLocked = false;
            }
            player = Image.FromFile(playerMovements[stepFrame]);
        }

        // Để lựa chọn ảnh nhân vật sẽ được vẽ lên
        private void SetFramePlayer(int form, Enums.Move status)
        {
            if (isShot)
            {
                startFrame = 6;
                endFrame = 8;
            }
            else if (form == 0 && isTransform)
            {
                startFrame = 10;
                endFrame = 16;
            }
            else if (form == 0 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 0 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 1 && isTransform)
            {
                startFrame = 10;
                endFrame = 17;
            }
            else if (form == 1 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 1 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 2 && isTransform)
            {
                startFrame = 10;
                endFrame = 20;
            }
            else if (form == 2 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 2 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 3 && isTransform)
            {
                startFrame = 10;
                endFrame = 27;
            }
            else if (form == 3 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 3 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (form == 4 && isTransform)
            {
                startFrame = 10;
                endFrame = 22;
            }
            else if (form == 4 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (form == 4 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }
        }
        private void DragonBall_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.H)
            {
                isEnd = true;
            }

            if (isLocked || !isStart) return;
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
            if (e.KeyCode == Keys.Space && !isTransform)
            {
                Shooting();
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

        private void StartGame()
        {
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            // Lấy hết hình trong thư mục Goku (Nằm ở thư mục debug)
            playerMovements = Directory.GetFiles("Goku0", "*.png").ToList();

            // Lấy hình thứ 10 trong thư mục Goku
            player = Image.FromFile(playerMovements[10]);

            isStart = true;
        }
        private void EndGame()
        {
            Application.Exit();
        }
        private void Transformation(int form, int delayShootTime)
        {
            SetNoMove(); // Khóa di chuyển lúc biến hình
            isTransform = true;
            slowDownFrameRate = 0;
            stepFrame = -1;
            delayShoot = 0;
            this.form = form;
            this.delayShootTime = delayShootTime;
        }
        private void SetNoMove()
        {
            isLocked = true;
            isShot = false;
            goUp = false;
            goDown = false;
            goLeft = false;
            goRight = false;
        }
        private void Shooting()
        {
            if (delayShoot != delayShootTime) return;
            delayShoot = 0;

            Bullets a = new Bullets(playerX + playerWidth,
                                    playerY + playerHeight / 2 + 20,
                                    bulletWidth,
                                    bulletHeight,
                                    true);
            a.Image = Image.FromFile(playerMovements[9]);
            bullets.Add(a);

            isShot = true;
            slowDownFrameRate = 0;
            stepFrame = 0;
        }
    }
}
