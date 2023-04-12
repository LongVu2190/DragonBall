using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace DragonBall.Objects
{
    public abstract class A_Object
    {
        public A_Object() { }

        public int X { get; set; }
        public int Y { get; set; }
        public int Height { get; set; }
        public int Width { get; set; }
        public int Speed { get; set; }
        public int stepFrame { get; set; }
        public int startFrame { get; set; }
        public int endFrame { get; set; }
        public int slowDownFPS { get; set; }
        public int maxSlowDownFPS { get; set; }
        public int form { get; set; }
        public int Health { get; set; }
        public Image Image { get; set; }
        public List<string> imageMovements { get; set; }
        ~A_Object() { }
    }
}
