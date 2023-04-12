using System;
using System.Collections.Generic;
using System.Drawing;
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
            form = 1;
            stepFrame = 0;
            slowDownFPS = 0;
            maxSlowDownFPS = 3;
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
                startFrame = 0;
                endFrame = 2;
            }
        }
        ~Enemy() { }
    }
}
