using System;
using System.Collections;
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
            set
            {
                _node = value;
            }
        }

        private int _x;
        public int X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                _SizeChanged?.Invoke();
            }
        }

        private int _y;
        public int Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                _SizeChanged?.Invoke();
            }
        }

        private int _height;
        public int Height
        {
            get
            {
                return _height;
            }
            set
            {
                _height = value;
                _SizeChanged?.Invoke();
            }
        }

        private int _width;
        public int Width
        {
            get
            {
                return _width;
            }
            set
            {
                _width = value;
                _SizeChanged?.Invoke();
            }
        }

        public Color Color { get; set; } = Color.Black;

        public DrawableItem Parent { get; set; }

        public delegate void OnSizeChanged();
        private event OnSizeChanged _SizeChanged;
        public event OnSizeChanged SizeChanged
        {
            add
            {
                _SizeChanged += value;
            }
            remove
            {
                _SizeChanged -= value;
            }
        }

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

        ///// <summary>
        ///// Notify position change to parent
        ///// </summary>
        //public virtual void NotifyPositionChangeToParent()
        //{
        //    if(Parent != null)
        //    {
        //        Parent.NotifyPositionChangeToParent();
        //    }
        //}

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

        public override bool Equals(object obj)
        {
            DrawableItem drawableObj = obj as DrawableItem;

            if (drawableObj != null)
            {
                bool hasSameXY = X == drawableObj.X && Y == drawableObj.Y;
                bool hasSameWH = Width == drawableObj.Width && Height == drawableObj.Height;
                bool hasSameName = ToString() == drawableObj.ToString();
                return hasSameWH && hasSameXY && hasSameName && Color == drawableObj.Color;
            }

            return false;
        }
    }
}
