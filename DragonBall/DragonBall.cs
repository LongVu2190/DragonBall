using DragonBall.Enums;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using Objects.DragonBall;
using DragonBall.Objects;
using System.Text;

namespace DragonBall
{
    public partial class DragonBall : Form
    {
        Player player;
        List<Bullet> bullets;

        List<Bullet> bulletsToRemove;

        Enemy enemy;

        bool isStart, isEnd, isLocked;
        int stepFrame, startFrame, endFrame, slowDownFrameRate, maxSlowDownFrameRate;

        int delayShoot, delayShootTime; // Thời gian giữa những lần bắn
        int score;

        bool goLeft, goRight, goUp, goDown;

        int playerX, playerY;

        bool isTransform, isShot;

        public DragonBall()
        {
            InitializeComponent();
        }

        private void DragonBall_Load(object sender, EventArgs e)
        {
            AllocConsole();
            StartGame();
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool AllocConsole();

        private void InitValue()
        {
            player = new Player();
            enemy = new Enemy();
            bullets = new List<Bullet>();
            bulletsToRemove = new List<Bullet>();

            isStart = false;
            isEnd = false;

            stepFrame = 0;
            slowDownFrameRate = 0;
            maxSlowDownFrameRate = 6;

            delayShoot = 0;
            delayShootTime = 15;
            score = 0;
            isLocked = false;

            playerX = 0;
            playerY = 0;

            player.form = 1;
            isTransform = false;
            isShot = false;

            CreateEnemy();
        }

        private void DragonBall_Paint(object sender, PaintEventArgs e)
        {
            if (!isStart) return;
            if (isEnd)
            {
                EndGame();
            }
            // Vẽ nhân vật
            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(player.Image, playerX, playerY, player.Width, player.Height);


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

                    if (Enemy != null)
                    {
                        PictureBox enemyHit = new PictureBox();
                        enemyHit.Location = new System.Drawing.Point(enemy.X, enemy.Y);
                        enemyHit.Size = new System.Drawing.Size(enemy.Width, enemy.Height);
                        if (hit.Bounds.IntersectsWith(enemyHit.Bounds) && !bullet.isHit)
                        {
                            bullet.isHit = true;
                            bulletsToRemove.Add(bullet);
                            score += 1;
                            score_lb.Text = score.ToString();
                            enemy = new Enemy();
                            CreateEnemy();
                        }
                    }
                }
            }

            if (enemy.Image != null)
            {
                Canvas.DrawImage(enemy.Image, enemy.X, enemy.Y, enemy.Width, enemy.Height);
            }

            foreach (var bulletToRemove in bulletsToRemove)
            {
                bullets.Remove(bulletToRemove);
            }

        }

        // Nếu đủ score thì biến hình lên cấp
        private void Level_Tick(object sender, EventArgs e)
        {
            if (score == 50) isEnd = true;
            if (!isStart) return;

            if (score == 0)
            {
                player.imageMovements = Directory.GetFiles("Goku0", "*.png").ToList();
                Transformation(0, 15);
                score++;
            }
            else if (score == 10)
            {
                player.imageMovements = Directory.GetFiles("Goku1", "*.png").ToList();
                Transformation(1, 13);
                score++;
            }
            else if (score == 20)
            {
                player.imageMovements = Directory.GetFiles("Goku2", "*.png").ToList();
                Transformation(2, 10);
                score++;
            }
            else if (score == 30)
            {
                player.imageMovements = Directory.GetFiles("Goku3", "*.png").ToList();
                Transformation(3, 8);
                score++;
            }
            else if (score == 40)
            {
                player.imageMovements = Directory.GetFiles("Goku4", "*.png").ToList();
                Transformation(4, 6);
                score++;
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player == null) return;

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
                SetFramePlayer(player.form, Enums.Move.Right);
                AnimatePlayer();
            }
            else if (!goLeft)
            {
                SetFramePlayer(player.form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển lên
            if (goUp && (playerY - player.Speed) > 0)
            {
                playerY -= player.Speed;
                SetFramePlayer(player.form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển xuống
            if (goDown && (playerY + player.Speed) < this.ClientSize.Height - player.Height)
            {
                playerY += player.Speed;
                SetFramePlayer(player.form, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển trái
            if (goLeft && (playerX - player.Speed) > 0)
            {
                playerX -= player.Speed;
                SetFramePlayer(player.form, Enums.Move.Left);
                AnimatePlayer();
            }

            // Di chuyển phải
            if (goRight && (playerX + player.Speed) < this.ClientSize.Width - player.Width)
            {
                playerX += player.Speed;
                SetFramePlayer(player.form, Enums.Move.Right);
                AnimatePlayer();
            }

            this.Invalidate();
        }
        private void Enemy_Tick(object sender, EventArgs e)
        {
            if (player == null) return;

            enemy.X -= player.Speed;

            if (enemy.X + 200 < 0)
            {
                enemy = new Enemy();
                CreateEnemy();
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
                    bullets[i].X += bullets[i].speed;
                    bullets[i].isMoving = true;
                }
                else // Bay hết màn hình thì loại nó ra khỏi list bullets
                {
                    if (bullets[i].isHit) // Xóa bullets được bắn
                    {
                        bullets.RemoveAt(i);
                        i--;
                    }
                    else
                    {
                        bullets[i].isMoving = false;
                    }
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
            if (player.imageMovements.Count != 0)
                player.Image = Image.FromFile(player.imageMovements[stepFrame]);
        }
        private void AnimateEnemy()
        {

        }
        private void CreateEnemy()
        {
            enemy.imageMovements = Directory.GetFiles("E0", "*.png").ToList();
            enemy.Image = Image.FromFile(enemy.imageMovements[0]);

            enemy.X = this.Width + 50;
            enemy.Y = new Random().Next(0, this.Height - 200);
        }
        // Để lựa chọn ảnh nhân vật sẽ được vẽ lên
        private void SetFramePlayer(int form, Enums.Move status)
        {
            if (isShot)
            {
                startFrame = 6;
                endFrame = 8;
            }
            else if (player.form == 0 && isTransform)
            {
                startFrame = 10;
                endFrame = 16;
            }
            else if (player.form == 0 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (player.form == 0 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (player.form == 1 && isTransform)
            {
                startFrame = 10;
                endFrame = 17;
            }
            else if (player.form == 1 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (player.form == 1 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (player.form == 2 && isTransform)
            {
                startFrame = 10;
                endFrame = 20;
            }
            else if (player.form == 2 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (player.form == 2 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (player.form == 3 && isTransform)
            {
                startFrame = 10;
                endFrame = 27;
            }
            else if (player.form == 3 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (player.form == 3 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }

            else if (player.form == 4 && isTransform)
            {
                startFrame = 10;
                endFrame = 22;
            }
            else if (player.form == 4 && status == Enums.Move.Right)
            {
                startFrame = 0;
                endFrame = 2;
            }
            else if (player.form == 4 && status == Enums.Move.Left)
            {
                startFrame = 3;
                endFrame = 5;
            }
        }
        private void SetFrameEnemy(int form)
        {

        }
        private void DragonBall_KeyDown(object sender, KeyEventArgs e)
        {
            if (isLocked || !isStart) return;

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
            if (e.KeyCode == Keys.E && !isTransform)
            {
                EndGame();
            }
        }
        private void DragonBall_KeyUp(object sender, KeyEventArgs e)
        {
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

        public void StartGame()
        {
            InitValue();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            // Lấy hết hình trong thư mục Goku (Nằm ở thư mục debug)
            player.imageMovements = Directory.GetFiles("Goku0", "*.png").ToList();

            // Lấy hình thứ 10 trong thư mục Goku
            player.Image = Image.FromFile(player.imageMovements[10]);

            isStart = true;

            Timer.Enabled = true;
            Level.Enabled = true;
            Enemy.Enabled = true;
        }
        private void EndGame()
        {
            Timer.Enabled = false;
            Level.Enabled = false;
            Enemy.Enabled = false;
            isLocked = true;
            isEnd = true;
            score = 0;
            MessageBox.Show("You win", "Notification");
            EndGame end = new EndGame(this);
            end.ShowDialog();
        }
        private void Transformation(int form, int delayShootTime)
        {
            SetNoMove(); // Khóa di chuyển lúc biến hình
            isTransform = true;
            slowDownFrameRate = 0;
            stepFrame = -1;
            delayShoot = 0;
            player.form = form;
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
            if (delayShoot != delayShootTime) return; // Tăng thời gian giữa những lần bắn

            delayShoot = 0;

            Bullet a = new Bullet(playerX + player.Width,
                                    playerY + player.Height / 2 + 20,
                                    true);

            a.Image = Image.FromFile(player.imageMovements[9]); // Hình đạn
            bullets.Add(a);

            isShot = true;
            slowDownFrameRate = 0;
            stepFrame = 0;
        }
    }
}
