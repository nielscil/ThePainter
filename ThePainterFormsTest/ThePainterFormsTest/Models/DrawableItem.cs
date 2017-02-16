using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    public abstract class DrawableItem
    {
        protected int _x;
        protected int _y;
        protected int _height;
        protected int _width;

        public Color Color { get; set; } = Color.Black;

        public DrawableItem(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _height = height;
            _width = width;
        }

        public void Move(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _height = height;
            _width = width;
        }

        public abstract void Draw(Graphics graphics);

        public bool IsOnLocation(Point point)
        {
            bool isXInItem = point.X >= _x && point.X <= _x + _width;
            bool isYInItem = point.Y >= _y && point.Y <= _y + _height;
            return isXInItem && isYInItem;
        }

        public void Select()
        {
            Color = Color.Red;
        }

        public void Deselect()
        {
            Color = Color.Black;
        }
    }
}
