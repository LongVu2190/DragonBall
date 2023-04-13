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
            isMoving = false;
            speed = 10;
        }
        public C_Bullet(int x, int y, bool isMoving)
        {
            X = x;
            Y = y;
            Width = 30;
            Height = 14;
            speed = 8;
            this.isMoving = isMoving;
        }

        public Image Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isMoving { get; set; }
        public int speed { get; set; }
        ~C_Bullet() { }
    }
}
