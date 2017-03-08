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
        private string _name;

        protected int _x;
        public int X
        {
            get
            {
                return _x;
            }
        }

        protected int _y;
        public int Y
        {
            get
            {
                return _y;
            }
        }
        protected int _height;
        public int Height
        {
            get
            {
                return _height;
            }
        }
        protected int _width;
        public int Width
        {
            get
            {
                return _width;
            }
        }

        public Color Color { get; set; } = Color.Black;

        public DrawableItem(int x, int y, int width, int height, string name)
        {
            _x = x;
            _y = y;
            _height = height;
            _width = width;
            _name = name;
        }

        public void Resize(Point begin, Point end)
        {
            _height += end.Y - begin.Y;
            _width += end.X - begin.X;
        }

        public void Resize(int width, int height)
        {
            _width = width;
            _height = height;
        }

        public void Move(Point begin, Point end)
        {
            _x += end.X - begin.X;
            _y += end.Y - begin.Y;
        }

        public void Move(int x, int y)
        {
            _x = x;
            _y = y;
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

        public string Serialize()
        {
            return $"{_name} {_x} {_y} {_width} {_height}";
        }

        public DrawableItem Clone()
        {
            DrawableItem item = null;

            if(this is Ellipse)
            {
                item = new Ellipse(this._x, this._y, this._width, this._height);
            }
            else
            {
                item = new Rectangle(this._x, this._y, this._width, this._height);
            }

            item.Color = this.Color;

            return item;
        }

        public override string ToString()
        {
            return _name;
        }
    }
}
