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

        public void Resize(Point begin, Point end)
        {
            _height += end.Y - begin.Y;
            _width += end.X - begin.X;
        }

        public void Move(Point begin, Point end)
        {
            _x += end.X - begin.X;
            _y += end.Y - begin.Y;
        }

        public abstract void Draw(Graphics graphics);

        public bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= _x && point.X <= _x + _width) || (point.X >= _x + _width && point.X <= _x);
            bool isYInItem = (point.Y >= _y && point.Y <= _y + _height) || (point.Y >= _y + _height && point.Y <= _y);
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
