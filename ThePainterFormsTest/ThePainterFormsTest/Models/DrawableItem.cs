using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Models
{
    public abstract class DrawableItem
    {
        protected int _x;
        public virtual int X
        {
            get
            {
                return _x;
            }
        }

        protected int _y;
        public virtual int Y
        {
            get
            {
                return _y;
            }
        }
        protected int _height;
        public virtual int Height
        {
            get
            {
                return _height;
            }
        }
        protected int _width;
        public virtual int Width
        {
            get
            {
                return _width;
            }
        }

        public Color Color { get; set; } = Color.Black;

        public abstract string Name { get; }

        public DrawableItem(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }

        public DrawableItem() { }

        public virtual void Resize(Point begin, Point end)
        {
            _height += end.Y - begin.Y;
            _width += end.X - begin.X;
        }

        public virtual void Resize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public virtual void Move(Point begin, Point end)
        {
            _x += end.X - begin.X;
            _y += end.Y - begin.Y;
        }

        public virtual void Move(int x, int y)
        {
            _x = x;
            _y = y;
        }

        public abstract void Draw(Graphics graphics);

        public virtual bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= _x && point.X <= _x + _width) || (point.X >= _x + _width && point.X <= _x);
            bool isYInItem = (point.Y >= _y && point.Y <= _y + _height) || (point.Y >= _y + _height && point.Y <= _y);
            return isXInItem && isYInItem;
        }

        public virtual void Select()
        {
            Color = Color.Red;
        }

        public virtual void Deselect()
        {
            Color = Color.Black;
        }

        public virtual string Serialize(string prefix)
        {
            return $"{prefix}{Name} {_x} {_y} {_width} {_height}";
        }

        public abstract DrawableItem Clone();

        public override string ToString()
        {
            return Name;
        }
    }
}
