﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DragonBall
{
    public class Bullets
    {
        public Bullets()
        {

        }
        public Bullets(int x, int y, int width, int height, bool isMoving)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            this.isMoving = isMoving;
        }

        public Image Image { get; set; }
        public int X { get; set; }
        public int Y { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public bool isMoving { get; set; } = false;
        public bool isHit { get; set; } = false;
    }
}
