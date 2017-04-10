using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Controls;

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

        public override void Deselect()
        {
            base.Deselect();
            
            foreach(var item in _subItems)
            {
                item.Deselect();
            }
        }

        public override void Select()
        {
            base.Select();

            foreach(var item in _subItems)
            {
                item.Select();
            }

            CalculatePositions();
        }

        public override void Draw(Graphics graphics)
        {
            foreach(var item in _subItems)
            {
                item.Draw(graphics);
            }
        }

        public override void Move(int x, int y)
        {
            int xDiff = x - X;
            int yDiff = y - Y;
            foreach (var item in _subItems)
            {
                item.Move(item.X + xDiff, item.Y + yDiff);
            }

            CalculatePositions();
        }

        public override void Move(Point begin, Point end)
        {
            foreach(var item in _subItems)
            {
                item.Move(begin, end);
            }

            CalculatePositions();
        }

        public override void Resize(int width, int height)
        {
            int widthDiff = width - Width;
            int heightDiff = height - Height;
            foreach (var item in _subItems)
            {
                item.Resize(item.Width + widthDiff, item.Height + heightDiff);
            }

            CalculatePositions();
        }

        public override void Resize(Point begin, Point end)
        {
            foreach (var item in _subItems)
            {
                item.Resize(begin, end);
            }

            CalculatePositions();
        }

        public override string Serialize(string prefix)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{prefix}group {_subItems.Count}");

            prefix += "\t";

            foreach(var item in _subItems)
            {
                sb.AppendLine(item.Serialize(prefix));
            }

            return sb.ToString().TrimEnd();
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

        private void CalculatePositions()
        {
            int x = int.MaxValue;
            int y = int.MaxValue;
            int width = 0;
            int heigth = 0;

            foreach(var item in _subItems)
            {
                x = item.X < x ? item.X : x;
                y = item.Y < y ? item.Y : y;

                width = (item.X + item.Width) > width ? item.X + item.Width : width;
                heigth = (item.Y + item.Height) > heigth ? item.Y + item.Height : heigth;
            }

            _x = x;
            _y = y;
            _width = width;
            _height = heigth;
        }
    }
}
