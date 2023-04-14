using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DragonBall.Objects
{
    internal class C_Boss : A_Object
    {
        public bool isHit;
        public int Direction;
        public bool isDead;
        public C_Boss()
        {
            Height = 300;
            Width = 300;
            Speed = 7;
            Health = 20;
            form = 0;
            Direction = 1;
            stepFrame = 0;
            slowDownFPS = 0;
            maxSlowDownFPS = 3;
            isHit = false;
            isDead = false;
            imageMovements = Directory.GetFiles("assets/enemy4", "*.png").ToList();
            Image = Image.FromFile(imageMovements[0]);
        }
    }
}
