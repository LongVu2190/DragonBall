using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall.Objects
{
    internal class C_Enemy : A_Object
    {
        public bool isHit;
        public C_Enemy()
        {
            Height = 120;
            Width = 120;
            Speed = 7;
            Health = 2;
            form = 0;
            stepFrame = 0;
            slowDownFPS = 0;
            maxSlowDownFPS = 10;
            isHit = false;
            imageMovements = Directory.GetFiles("assets/enemy0", "*.png").ToList();
            Image = Image.FromFile(imageMovements[0]);
        }

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
        ~C_Enemy() { }
    }
}
