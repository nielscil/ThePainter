using System.Drawing;
using ThePainterFormsTest.Controls;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    /// <summary>
    /// Abstract base class for item
    /// </summary>
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

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="x">x-coordinate</param>
        /// <param name="y">y-coordinate</param>
        /// <param name="width">width of item</param>
        /// <param name="height">height of item</param>
        public DrawableItem(int x, int y, int width, int height)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public DrawableItem()
        {}

        /// <summary>
        /// Is on location
        /// </summary>
        /// <param name="point">point</param>
        /// <returns>true if point is in item, otherwise false</returns>
        public virtual bool IsOnLocation(Point point)
        {
            bool isXInItem = (point.X >= X && point.X <= X + Width) || (point.X >= X + Width && point.X <= X);
            bool isYInItem = (point.Y >= Y && point.Y <= Y + Height) || (point.Y >= Y + Height && point.Y <= Y);
            return isXInItem && isYInItem;
        }

        /// <summary>
        /// Notify position change to parent
        /// </summary>
        public virtual void NotifyPositionChangeToParent()
        {
            if(Parent != null)
            {
                Parent.NotifyPositionChangeToParent();
            }
        }

        /// <summary>
        /// Accepts the visitor
        /// </summary>
        /// <param name="visitor"></param>
        public abstract void Accept(IVisitor visitor);

        /// <summary>
        /// Clones the item
        /// </summary>
        /// <returns>clone</returns>
        public abstract DrawableItem Clone();

    }
}
