using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Controls;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    /// <summary>
    /// Group item class
    /// </summary>
    public class Group : DrawableItem
    {
        public List<DrawableItem> Items
        {
            get
            {
                return new List<DrawableItem>(_subItems);
            }
        }

        public override PainterTreeNode Node
        {
            get
            {
                if(_node == null)
                {
                    _node = new PainterTreeNode(this);

                    foreach (var item in _subItems)
                    {
                        _node.Nodes.Add(item.Node);
                    }
                }
                return _node;
            }
        }

        public int Count
        {
            get
            {
                int i = 0;

                foreach (var item in Items)
                {
                    if (!(item is Ornament))
                        i++;
                }

                return i;
            }
        }

        private List<DrawableItem> _subItems = new List<DrawableItem>();        

        /// <summary>
        /// Constructor
        /// </summary>
        public Group()
        {
            _subItems = new List<DrawableItem>();
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">subitems</param>
        public Group(List<DrawableItem> items)
        {
            _subItems = new List<DrawableItem>(items);

            foreach(var item in items)
            {
                item.Parent = this;
            }

            CalculatePositions();
        }

        /// <summary>
        /// Add item to group
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="index">index</param>
        public void AddItem(DrawableItem item, int index)
        {
            _subItems.Insert(index, item);

            Node.Nodes.Insert(index, item.Node);

            item.Parent = this;

            CalculatePositions();
        }

        /// <summary>
        /// Removes item from group
        /// </summary>
        /// <param name="item">item</param>
        public void RemoveItem(DrawableItem item)
        {
            _subItems.Remove(item);

            Node.Nodes.Remove(item.Node);

            CalculatePositions();
        }

        /// <summary>
        /// Notify position changed override
        /// </summary>
        public override void NotifyPositionChangeToParent()
        {
            CalculatePositions();
            base.NotifyPositionChangeToParent();
        }

        /// <summary>
        /// clone item
        /// </summary>
        /// <returns>clone</returns>
        public override DrawableItem Clone()
        {
            List<DrawableItem> items = new List<DrawableItem>();
            foreach(var item in _subItems)
            {
                items.Add(item.Clone());
            }

            Group group = new Group(items);
            group.Color = Color;
            return group;
        }

        /// <summary>
        /// Calculate position of group using the child positions
        /// </summary>
        public void CalculatePositions()
        {
            int x = int.MaxValue;
            int y = int.MaxValue;
            int farWidth = 0;
            int farHeight = 0;

            foreach(var item in _subItems)
            {
                x = item.X < x ? item.X : x;
                y = item.Y < y ? item.Y : y;

                farWidth = (item.X + item.Width) > farWidth ? item.X + item.Width : farWidth;
                farHeight = (item.Y + item.Height) > farHeight ? item.Y + item.Height : farHeight;
            }

            X = x;
            Y = y;
            Width = farWidth - X;
            Height = farHeight - Y;
        }

        /// <summary>
        /// Accept a visitor
        /// </summary>
        /// <param name="visitor">visitor</param>
        public override void Accept(IVisitor visitor)
        {
            visitor.BeforeGroup(this);

            foreach (var item in Items)
            {
                item.Accept(visitor);
            }

            visitor.AfterGroup(this);
        }

        /// <summary>
        /// To string override to give group name
        /// </summary>
        /// <returns>name</returns>
        public override string ToString()
        {
            return "group";
        }
    }
}
