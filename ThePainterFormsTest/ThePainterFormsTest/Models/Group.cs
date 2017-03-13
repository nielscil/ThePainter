using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    class Group : ICanvasItem
    {

        public int X
        {
            get
            {
                return 0; //TODO: calculate lazy x,y,width,height from subitems
            }
        }

        public int Y
        {
            get
            {
                return 0;
            }
        }

        public int Width
        {
            get
            {
                return 0;
            }
        }

        public int Height
        {
            get
            {
                return 0;
            }
        }

        public Color Color { get; set; }

        public string Name
        {
            get
            {
                return "group";
            }
        }

        private List<ICanvasItem> _subItems;

        public Group()
        {
            _subItems = new List<ICanvasItem>();
        }

        public ICanvasItem Clone()
        {
            throw new NotImplementedException();
        }

        public void Deselect()
        {
            Color = Color.Black;
        }

        public void Draw(Graphics graphics)
        {
            foreach(var item in _subItems)
            {
                item.Draw(graphics);
            }

            if(Color == Color.Red)
            {
                using (Pen p = new Pen(Color))
                {
                    graphics.DrawLine(p, X, Y, X + Width, Y);
                    graphics.DrawLine(p, X, Y, X, Y + Height);
                    graphics.DrawLine(p, X + Width, Y, X + Width, Y + Height);
                    graphics.DrawLine(p, X, Y + Height, X + Width, Y + Height);
                }
            }
        }

        public bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= X && point.X <= X + Width) || (point.X >= X + Width && point.X <= X);
            bool isYInItem = (point.Y >= Y && point.Y <= Y + Height) || (point.Y >= Y + Height && point.Y <= Y);
            return isXInItem && isYInItem;
        }

        public void Move(int x, int y)
        {
            foreach (var item in _subItems)
            {
                item.Move(x, y);
            }
        }

        public void Move(Point begin, Point end)
        {
            foreach(var item in _subItems)
            {
                item.Move(begin, end);
            }
        }

        public void Resize(int width, int height)
        {
            foreach (var item in _subItems)
            {
                item.Resize(width, height);
            }
        }

        public void Resize(Point begin, Point end)
        {
            foreach (var item in _subItems)
            {
                item.Resize(begin, end);
            }
        }

        public void Select()
        {
            Color = Color.Red;
        }

        public string Serialize()
        {
            throw new NotImplementedException();
        }
    }
}
