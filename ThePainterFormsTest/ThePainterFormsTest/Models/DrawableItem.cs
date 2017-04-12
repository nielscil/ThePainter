using System.Drawing;
using ThePainterFormsTest.Controls;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    public abstract class DrawableItem
    {
        protected PainterTreeNode _node;
        public virtual PainterTreeNode Node
        {
            get
            {
                if(_node == null)
                {
                    _node = new PainterTreeNode(this);
                }
                return _node;
            }
        }

        public int X { get; set; }

        public int Y { get; set; }

        public int Height { get; set; }

        public int Width { get; set; }

        public Color Color { get; set; } = Color.Black;

        public DrawableItem Parent { get; set; }

        public DrawableItem(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        public DrawableItem()
        {}

        public virtual bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= X && point.X <= X + Width) || (point.X >= X + Width && point.X <= X);
            bool isYInItem = (point.Y >= Y && point.Y <= Y + Height) || (point.Y >= Y + Height && point.Y <= Y);
            return isXInItem && isYInItem;
        }

        public virtual void NotifyPositionChangeToParent()
        {
            if(Parent != null)
            {
                Parent.NotifyPositionChangeToParent();
            }
        }

        public abstract void Accept(IVisitor visitor);

        public abstract DrawableItem Clone();

    }
}
