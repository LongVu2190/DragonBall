using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Objects
{
    internal class Enemy
    {
        public Enemy()
        {
            Height = 200;
            Width = 200;
            Speed = 10;
            form = 0;
            stepFrame = 0;
            slowDownFPS = 0;
            maxSlowDownFPS = 3;
            imageMovements = Directory.GetFiles("assets/enemy0", "*.png").ToList();
            Image = Image.FromFile(imageMovements[0]);
        }
        
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int stepFrame { get; set; }
        public int startFrame { get; set; }
        public int endFrame { get; set; }
        public int slowDownFPS { get; set; }
        public int maxSlowDownFPS { get; set; }
        public int form { get; set; }
        public Image Image { get; set; }

        public List<string> imageMovements = new List<string>();
        public void SetFrame()
        {
            if (form == 1)
            {
                imageMovements = Directory.GetFiles("assets/enemy1", "*.png").ToList();
            }
            else if (form == 2)
            {
                imageMovements = Directory.GetFiles("assets/enemy2", "*.png").ToList();
            }
            else if (form == 3)
            {
                imageMovements = Directory.GetFiles("assets/enemy3", "*.png").ToList();
            }
            else if (form == 4)
            {
                imageMovements = Directory.GetFiles("assets/enemy4", "*.png").ToList();
            }
            startFrame = 0;
            endFrame = 2;
        }
        ~Enemy() { }
    }
}
