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
    public class Group : DrawableItem
    {
        public override string Name
        {
            get
            {
                return "group";
            }
        }
        public List<DrawableItem> Items
        {
            get
            {
                return new List<DrawableItem>(_subItems);
            }
        }


        protected PainterTreeNode _node;
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
            protected set
            {
                _node = value;
            }
        }

        private List<DrawableItem> _subItems = new List<DrawableItem>();        

        public Group()
        {
            _subItems = new List<DrawableItem>();
        }

        public Group(List<DrawableItem> items)
        {
            _subItems = new List<DrawableItem>(items);

            foreach(var item in items)
            {
                item.Parent = this;
            }

            CalculatePositions();
        }

        public void AddItem(DrawableItem item, int index)
        {
            _subItems.Add(item);

            Node.Nodes.Insert(index, item.Node);

            item.Parent = this;

            CalculatePositions();
        }

        public void RemoveItem(DrawableItem item)
        {
            _subItems.Remove(item);

            Node.Nodes.Remove(item.Node);

            CalculatePositions();
        }

        public override void NotifyPositionChangeToParent()
        {
            CalculatePositions();
            base.NotifyPositionChangeToParent();
        }

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

        public override void Accept(IVisitor visitor)
        {
            visitor.BeforeGroup(this);

            foreach (var item in Items)
            {
                item.Accept(visitor);
            }

            visitor.AfterGroup(this);
        }
    }
}
