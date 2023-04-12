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
using DragonBall.Enums;

namespace DragonBall
{
    public partial class DragonBall : Form
    {
        Player player;
        Enemy enemy;

        List<Bullet> bullets;
        List<Bullet> bulletsToRemove;

        bool isStart, isEnd, isLocked;

        int delayShoot;
        int score;

        bool goLeft, goRight, goUp, goDown;

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
            isStart = false;
            isEnd = false;

            delayShoot = 0;
            score = 0;
            isLocked = false;

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

            Graphics Canvas = e.Graphics;
            Canvas.DrawImage(player.Image, player.X, player.Y, player.Width, player.Height);


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
                player.imageMovements = Directory.GetFiles("assets/player0", "*.png").ToList();
                Transformation(0, 15);
                score++;
            }
            else if (score == 10)
            {
                player.imageMovements = Directory.GetFiles("assets/player1", "*.png").ToList();
                Transformation(1, 13);
                score++;
            }
            else if (score == 20)
            {
                player.imageMovements = Directory.GetFiles("assets/player2", "*.png").ToList();
                Transformation(2, 10);
                score++;
            }
            else if (score == 30)
            {
                player.imageMovements = Directory.GetFiles("assets/player3", "*.png").ToList();
                Transformation(3, 8);
                score++;
            }
            else if (score == 40)
            {
                player.imageMovements = Directory.GetFiles("assets/player4", "*.png").ToList();
                Transformation(4, 6);
                score++;
            }

        }
        private void Timer_Tick(object sender, EventArgs e)
        {
            if (player == null) return;

            if (delayShoot != player.delayShootTime) // Tăng thời gian giữa những lần bắn
            {
                delayShoot++;
            }

            if (bullets != null) // Vẽ đạn nếu list đạn không rỗng
            {
                AnimateBullet();
            }

            if (isTransform)
            {
                player.SetFrame(isShot, isTransform, Enums.Move.Right);
                AnimatePlayer();
            }
            else if (!goLeft)
            {
                player.SetFrame(isShot, isTransform, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển lên
            if (goUp && (player.Y - player.Speed) > 0)
            {
                player.Y -= player.Speed;
                player.SetFrame(isShot, isTransform, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển xuống
            if (goDown && (player.Y + player.Speed) < this.ClientSize.Height - player.Height)
            {
                player.Y += player.Speed;
                player.SetFrame(isShot, isTransform, Enums.Move.Right);
                AnimatePlayer();
            }

            // Di chuyển trái
            if (goLeft && (player.X - player.Speed) > 0)
            {
                player.X -= player.Speed;
                player.SetFrame(isShot, isTransform, Enums.Move.Left);
                AnimatePlayer();
            }

            // Di chuyển phải
            if (goRight && (player.X + player.Speed) < this.ClientSize.Width - player.Width)
            {
                player.X += player.Speed;
                player.SetFrame(isShot, isTransform, Enums.Move.Right);
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

            enemy.SetFrame();
            AnimateEnemy();
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
            player.slowDownFPS += 1;
            if (player.slowDownFPS == player.maxSlowDownFPS) // Giảm FPS của hoạt ảnh nhân vật xuống
            {
                player.stepFrame++;
                player.slowDownFPS = 0;
            }
            if (player.stepFrame > player.endFrame || player.stepFrame < player.startFrame) // Đảm bảo lấy hình trong khoảng index từ player.startFrame -> player.endFrame
            {
                player.stepFrame = player.startFrame;
            }
            if (player.stepFrame == player.endFrame)
            {
                isShot = false;
                isTransform = false;
                isLocked = false;
            }
            if (player.imageMovements.Count != 0)
                player.Image = Image.FromFile(player.imageMovements[player.stepFrame]);
        }
        private void AnimateEnemy()
        {
            if (enemy == null) return;

            enemy.slowDownFPS += 1;
            Console.WriteLine("SlowDownFPS: " + enemy.slowDownFPS);

            if (enemy.slowDownFPS == enemy.maxSlowDownFPS)
            {
                enemy.stepFrame++;
                enemy.slowDownFPS = 0;
            }

            if (enemy.slowDownFPS == enemy.maxSlowDownFPS)
            {
                enemy.stepFrame++;
                enemy.slowDownFPS = 0;
            }

            Console.WriteLine("Enemystep: " + enemy.stepFrame);
            if (enemy.stepFrame > enemy.endFrame || enemy.stepFrame < enemy.startFrame)
            {
                enemy.stepFrame = enemy.startFrame;
            }
            if (enemy != null)
                enemy.Image = Image.FromFile(enemy.imageMovements[enemy.stepFrame]);
        }
        private void CreateEnemy()
        {
            enemy.imageMovements = Directory.GetFiles("assets/enemy0", "*.png").ToList();
            enemy.Image = Image.FromFile(enemy.imageMovements[0]);

            enemy.X = this.Width + 50;
            enemy.Y = new Random().Next(0, this.Height - 250);
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
            player = new Player();
            enemy = new Enemy();
            bullets = new List<Bullet>();
            bulletsToRemove = new List<Bullet>();

            InitValue();
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

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
            player.slowDownFPS = 0;
            player.stepFrame = -1;
            delayShoot = 0;
            player.form = form;
            player.delayShootTime = delayShootTime;
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
            if (delayShoot != player.delayShootTime) return; // Tăng thời gian giữa những lần bắn

            delayShoot = 0;

            Bullet a = new Bullet(player.X + player.Width,
                                    player.Y + player.Height / 2 + 20,
                                    true);

            a.Image = Image.FromFile(player.imageMovements[9]); // Hình đạn
            bullets.Add(a);

            isShot = true;
            player.slowDownFPS = 0;
            player.stepFrame = 0;
        }
    }
}
