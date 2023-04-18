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
        List<string> avatars = new List<string>();
        List<string> maps = new List<string>();

        static int labelSize = 150; // Vùng để chứa thanh máu

        C_Boss boss;
        private List<C_Bullet> bossBullets;

        C_Player player;
        List<C_Bullet> bullets;
        List<C_Bullet> bulletsToRemove;

        List<C_Enemy> enemies;
        List<C_Enemy> enemiesToRemove;

        bool isStart, isEnd, isPause, isLocked;

        int delayShoot, delaySpamEneny, maxDelaySpamEneny;
        int score;
        int numEnemies;
        int bulletSpeed;
        int status;

        bool goLeft, goRight, goUp, goDown;

        bool isTransform, isShot, isBoss;

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
        public void StartGame()
        {
            avatars = Directory.GetFiles("assets/avatars", "*.png").ToList();
            maps = Directory.GetFiles("assets/maps", "*.jpg").ToList();
            Avatar.Image = Image.FromFile(avatars[0]);

            player = new C_Player();
            player.Y = labelSize;
            bullets = new List<C_Bullet>();
            bulletsToRemove = new List<C_Bullet>();
            enemies = new List<C_Enemy>();
            enemiesToRemove = new List<C_Enemy>();

            isStart = true;
            isEnd = false;
            isPause = false;
            isLocked = false;
            isBoss = false;

            delayShoot = 0;
            score = 0;
            numEnemies = 10;
            delaySpamEneny = 0;
            maxDelaySpamEneny = 20;

            isTransform = false;
            isShot = false;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.DoubleBuffered = true;

            Moving_Timer.Enabled = true;
            Level_Timer.Enabled = true;
            Enemy_Timer.Enabled = true;

            Enemy_Progress.Maximum = 2;

            for (int a = 0; a < 4; a++)
            {
                C_Enemy enemy = new C_Enemy();
                enemy.form = player.form;
                enemy.SetFrame();

                enemy.X = this.Width + enemy.X;
                enemy.Y = labelSize + (enemy.Height + 40) * a;
                enemies.Add(enemy);
            }
            Player_Progress.SetState(2);
            Enemy_Progress.SetState(2);
        }
        private void EndGame()
        {
            Moving_Timer.Enabled = false;
            Level_Timer.Enabled = false;
            Enemy_Timer.Enabled = false;
            Boss_Timer.Enabled = false;

            Enemy_Progress.Value = 0;
            score = 0;
            Level_Progress.Value = 1;
            isLocked = true;
            isEnd = true;
            if (isBoss)
                bossBullets.Clear();

            Invalidate();

            if (player.Health == 0)
                status = 2;
            else
                status = 1;

            EndGame end = new EndGame(this, status);
            end.ShowDialog();
        }
        private void PauseGame()
        {
            if (!isPause)
            {
                Moving_Timer.Enabled = false;
                Level_Timer.Enabled = false;
                Enemy_Timer.Enabled = false;
                Boss_Timer.Enabled = false;
                isPause = true;
                return;
            }
            Moving_Timer.Enabled = true;
            Level_Timer.Enabled = true;
            Enemy_Timer.Enabled = true;
            if (isBoss)
                Boss_Timer.Enabled = true;
            isPause = false;
        }
        private void DragonBall_Paint(object sender, PaintEventArgs e)
        {
            Graphics Canvas = e.Graphics;

            if (!isStart || isEnd)
            {
                return;
            }
            
            Canvas.DrawImage(player.Image, player.X, player.Y, player.Width, player.Height);
            if (isBoss)
            {
                Canvas.DrawImage(boss.Image, boss.X, boss.Y, boss.Width, boss.Height);
            }

            if (bullets == null) return;

            // Vẽ đạn
            foreach (var bullet in bullets)
            {
                if (bullet.X + bullet.Width >= this.ClientSize.Width)
                {
                    bulletsToRemove.Add(bullet);
                }
                Canvas.DrawImage(bullet.Image, bullet.X, bullet.Y, bullet.Width, bullet.Height);
                PictureBox bulletHit = new PictureBox()
                {
                    Location = new System.Drawing.Point(bullet.X, bullet.Y),
                    Size = new System.Drawing.Size(bullet.Width, bullet.Height)
                };
                if (isBoss)
                {
                    PictureBox bossHit = new PictureBox()
                    {
                        Location = new System.Drawing.Point(boss.X, boss.Y),
                        Size = new System.Drawing.Size(boss.Width, boss.Height)
                    };
                    if (bulletHit.Bounds.IntersectsWith(bossHit.Bounds))
                    {
                        boss.Health--;
                        Enemy_Progress.Value = boss.Health;
                        bulletsToRemove.Add(bullet);
                        score++;
                    }
                }
                // Tạo 1 biến picturebox tạm để xài hàm IntersectWith
                foreach (C_Enemy enemy in enemies)
                {
                    if (enemy != null)
                    {
                        PictureBox enemyHit = new PictureBox()
                        {
                            Location = new System.Drawing.Point(enemy.X, enemy.Y),
                            Size = new System.Drawing.Size(enemy.Width, enemy.Height)
                        };
                        if (bulletHit.Bounds.IntersectsWith(enemyHit.Bounds))
                        {
                            enemy.Health--;
                            bulletsToRemove.Add(bullet);
                            if (enemy.Health == 0)
                            {
                                enemiesToRemove.Add(enemy);
                                score += 1;
                                if (Level_Progress.Value != 5)
                                {
                                    Level_Progress.Value++;
                                }
                            }
                        }
                        if (enemy.Health >= 0)
                            Enemy_Progress.Value = enemy.Health;
                    }

                }
            }

            if (bossBullets != null)
            {
                foreach (var bullet in bossBullets)
                {
                    Canvas.DrawImage(bullet.Image, bullet.X, bullet.Y, bullet.Width, bullet.Height);

                    PictureBox bulletHit = new PictureBox()
                    {
                        Location = new System.Drawing.Point(bullet.X, bullet.Y),
                        Size = new System.Drawing.Size(bullet.Width, bullet.Height)
                    };

                    PictureBox playerHit = new PictureBox()
                    {
                        Location = new Point(player.X, player.Y),
                        Size = new Size(player.Width, player.Height)
                    };
                    if (bulletHit.Bounds.IntersectsWith(playerHit.Bounds) && !bullet.isHit)
                    {
                        bullet.isHit = true;
                        bulletsToRemove.Add(bullet);
                        if (player.Health > 0)
                        {
                            player.Health--;
                            Player_Progress.Value--;
                        }
                            
                    }
                }

            }

            // Enemy và va chạm với player
            foreach (C_Enemy enemy in enemies)
            {
                Canvas.DrawImage(enemy.Image, enemy.X, enemy.Y, enemy.Width, enemy.Height);
                if (enemy.Image != null)
                {
                    PictureBox enemyHit = new PictureBox()
                    {
                        Location = new Point(enemy.X, enemy.Y),
                        Size = new Size(enemy.Width, enemy.Height)
                    };

                    PictureBox playerHit = new PictureBox()
                    {
                        Location = new Point(player.X, player.Y),
                        Size = new Size(player.Width, player.Height)
                    };

                    if (enemyHit.Bounds.IntersectsWith(playerHit.Bounds) && !enemy.isHit)
                    {
                        enemy.isHit = true;
                        player.Health--;
                        Player_Progress.Value = player.Health;
                        enemiesToRemove.Add(enemy);
                    }
                }
            }

            //Xóa đạn đã bắn dính
            foreach (var bulletToRemove in bulletsToRemove)
            {
                bullets.Remove(bulletToRemove);
                if (isBoss && bossBullets.Count != 0)
                    bossBullets.Remove(bulletToRemove);
            }

            foreach (C_Enemy enemyToRemove in enemiesToRemove)
            {
                enemies.Remove(enemyToRemove);
            }

        }

        // Nếu đủ score thì biến hình lên cấp
        private void Level_Timer_Tick(object sender, EventArgs e)
        {
            if (isEnd || !isStart) return;

            if (isBoss && Enemy_Progress.Value == 0)
            {
                isEnd = true;
                EndGame();
            }

            if (player.Health == 0 && isStart)
            {
                if (!player.firstLife && isBoss)
                {
                    player.Health = 20;
                    Player_Progress.Maximum = 20;
                    Player_Progress.Value = 20;
                    Transformation(4, 8, 22);
                    player.firstLife = true;
                }
                else
                {
                    isEnd = true;
                    EndGame();
                }
                
            }

            if (score == 0)
            {
                Transformation(0, 3, 30);
                Level_Progress.Value = 1;
                score++;
            }
            else if (score == 5)
            {
                Transformation(1, 4, 28);
                Level_Progress.Value = 1;
                score++;
            }
            else if (score == 10)
            {
                Transformation(2, 5, 26);
                Level_Progress.Value = 1;
                score++;
            }
            else if (score == 15)
            {
                Transformation(3, 6, 24);
                Level_Progress.Value = 1;
                score++;
            }
            else if (score == 20)
            {
                Level_Progress.Value = 1;
                CreateBoss();
                score++;
            }
        }
        private void Moving_Timer_Tick(object sender, EventArgs e)
        {
            if (isEnd || !isStart) return;

            if (delayShoot != player.delayShootTime) // Tăng thời gian giữa những lần bắn
            {
                delayShoot++;
            }

            if (bullets != null) // Vẽ đạn nếu list đạn không rỗng
            {
                AnimatePlayerBullet();
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
            if (goUp && (player.Y - player.Speed) > labelSize)
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
            if (goLeft)
            {
                if (player.X - player.Speed > 0)
                {
                    player.X -= player.Speed;
                }
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
        private void Enemy_Timer_Tick(object sender, EventArgs e)
        {
            if (isTransform || isEnd || !isStart || isBoss) return;

            CreateEnemy();

            foreach (C_Enemy enemy in enemies)
            {
                enemy.X -= enemy.Speed;

                if (enemy.X + enemy.Width < 0)
                {
                    enemiesToRemove.Add(enemy);
                }
                enemy.SetFrame();
                AnimateEnemy();
                this.Invalidate();
            }
        }
        private void Boss_Timer_Tick(object sender, EventArgs e)
        {
            if (isTransform) return;
            // reverse direction if the boss reaches top or bottom of the screen
            boss.Y += boss.Direction * 10;

            if (boss.Y < labelSize || boss.Y > this.Height - boss.Height - 20) boss.Direction *= -1;

            if (new Random().Next(0, 100) < 15)
            {
                BossShooting();
            }
            AnimateBossBullet();
        }

        private void AnimatePlayerBullet()
        {
            if (bullets != null)
            {
                for (int i = 0; i < bullets.Count; i++)
                {
                    if (bullets[i].X + bullets[i].Width < this.ClientSize.Width) // Bay trong màn hình
                    {
                        bullets[i].X += bullets[i].speed;
                    }
                }
            }
        }
        private void AnimateBossBullet()
        {
            if (bossBullets != null)
            {
                for (int i = 0; i < bossBullets.Count; i++)
                {
                    if (bossBullets[i].X + bossBullets[i].Width < this.ClientSize.Width)
                    {
                        bossBullets[i].X -= bossBullets[i].speed;
                    }
                }
            }
        }
        private void AnimatePlayer()
        {
            player.slowDownFPS += 1;
            if (player.slowDownFPS == player.maxSlowDownFPS) // Giảm FPS của hoạt ảnh nhân vật xuống
            {
                if (isBoss)
                {
                    boss.stepFrame++;
                }
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

            if (isBoss && boss.imageMovements.Count != 0)
            {
                if (boss.stepFrame > 2 || boss.stepFrame < 0) // Đảm bảo lấy hình trong khoảng index từ player.startFrame -> player.endFrame
                {
                    boss.stepFrame = 0;
                }
                boss.Image = Image.FromFile(boss.imageMovements[boss.stepFrame]);
            }
        }
        private void AnimateEnemy()
        {
            foreach (C_Enemy enemy in enemies)
            {
                if (enemy == null) return;

                enemy.slowDownFPS += 1;

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

                if (enemy.stepFrame > enemy.endFrame || enemy.stepFrame < enemy.startFrame)
                {
                    enemy.stepFrame = enemy.startFrame;
                }
                if (enemy != null)
                    enemy.Image = Image.FromFile(enemy.imageMovements[enemy.stepFrame]);
            }
        }

        private void CreateEnemy()
        {
            if (delaySpamEneny != maxDelaySpamEneny)
            {
                delaySpamEneny++;
                return;
            }
            delaySpamEneny = 0;

            if (enemies.Count < numEnemies)
            {
                C_Enemy enemy = new C_Enemy();
                enemy.form = player.form;
                enemy.Health = player.form + 2;
                enemy.SetFrame();

                enemy.X = this.Width + enemy.X;
                enemy.Y = new Random().Next(labelSize, this.Height - enemy.Height - 40);
                enemies.Add(enemy);
            }
        }
        private void CreateBoss()
        {
            player.Health = 2;
            Player_Progress.Value = 2;
            Boss_PBox.Visible = true;
            Enemy_Progress.Size = new Size(300, 29);
            Enemy_Progress.Location = new Point(700, 128);
            isBoss = true;
            enemies.Clear();
            boss = new C_Boss();
            bossBullets = new List<C_Bullet>();

            boss.X = this.Width - boss.Width;
            boss.Y = labelSize;
            Boss_Timer.Enabled = true;

            Enemy_Progress.Maximum = boss.Health;
            Enemy_Progress.Value = boss.Health;

            Invalidate();
        }

        private void Transformation(int form, int bulletSpeed, int delayShootTime)
        {
            SetNoMove();
            bullets.Clear();
            isTransform = true;
            player.slowDownFPS = 0;
            player.stepFrame = -1;
            delayShoot = 0;
            
            player.form = form;
           
            if (isBoss && !player.firstLife)
            {
                Enemy_Progress.Maximum = boss.Health;
            }
            else
            {
                Enemy_Progress.Maximum = player.form + 2;
            }

            player.delayShootTime = delayShootTime;
            this.bulletSpeed = bulletSpeed;

            Avatar.Image = Image.FromFile(avatars[player.form]);
            this.BackgroundImage = Image.FromFile(maps[player.form]);
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

            C_Bullet a = new C_Bullet(player.X + player.Width,
                                    player.Y + player.Height / 2 + 20,
                                    bulletSpeed,
                                    true); ;

            a.Image = Image.FromFile(player.imageMovements[9]); // Hình đạn
            bullets.Add(a);

            isShot = true;
            player.slowDownFPS = 0;
            player.stepFrame = 0;
        }
        private void BossShooting()
        {
            C_Bullet a = new C_Bullet(boss.X,
                                   boss.Y + 150,
                                   bulletSpeed,
                                   true); ;

            a.Image = Image.FromFile(boss.imageMovements[3]); // Hình đạn
            a.speed = 20;
            bossBullets.Add(a);
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
            if (e.KeyCode == Keys.C && !isTransform)
            {
                PauseGame();
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

    }
}
