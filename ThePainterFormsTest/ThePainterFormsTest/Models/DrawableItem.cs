using System.Drawing;
using ThePainterFormsTest.Controls;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    public abstract class DrawableItem
    {

        public virtual PainterTreeNode Node { get; protected set; }

        public int X { get; set; }

        public virtual int Y { get; set; }

        public virtual int Height { get; set; }

        public virtual int Width { get; set; }

        public Color Color { get; set; } = Color.Black;

        public DrawableItem Parent { get; set; }

        public abstract string Name { get; }

        public DrawableItem(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Node = new PainterTreeNode(this);
        }

        public DrawableItem()
        {}

        public virtual void Resize(Point begin, Point end)
        {
            Height += end.Y - begin.Y;
            Width += end.X - begin.X;
        }

        public virtual void Resize(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public virtual void Move(Point begin, Point end)
        {
            X += end.X - begin.X;
            Y += end.Y - begin.Y;
        }

        public virtual void Move(int x, int y)
        {
            X = x;
            Y = y;
        }

        public abstract void Draw(Graphics graphics);

        public virtual bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= X && point.X <= X + Width) || (point.X >= X + Width && point.X <= X);
            bool isYInItem = (point.Y >= Y && point.Y <= Y + Height) || (point.Y >= Y + Height && point.Y <= Y);
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
            return $"{prefix}{Name} {X} {Y} {Width} {Height}";
        }

        public abstract void Accept(IVisitor visitor);

        public abstract DrawableItem Clone();

        public override string ToString()
        {
            return Name;
        }
    }
}
