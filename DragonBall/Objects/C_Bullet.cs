using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Objects.DragonBall
{
    internal class C_Bullet
    {
        public C_Bullet()
        {
            speed = 10;
        }
        public C_Bullet(int x, int y, int speed, bool isMoving)
        {
            X = x;
            Y = y;
            Width = 30;
            Height = 14;
            this.speed = speed;
            isHit = false;
        }

        public Image Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int speed { get; set; }
        public bool isHit { get; set; }
        ~C_Bullet() { }
    }
}
