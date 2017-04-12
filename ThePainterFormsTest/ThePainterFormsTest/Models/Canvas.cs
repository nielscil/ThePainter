using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Commands;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Visitors;

namespace ThePainterFormsTest.Models
{
    public class Canvas
    {
        private Controller Controller
        {
            get
            {
                return Controller.Instance;
            }
        }

        public enum Mode { Rectange, Ellipse };

        public List<DrawableItem> Items { get; private set; } = new List<DrawableItem>();

        private DrawableItem _selectedItem = null;
        public DrawableItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                Controller.EnableRemoveGroupButton(false);

                if (value == null)
                {
                    
                    _tempItem = null;
                }

                //Controller.SelectNode(_selectedItem, false);

                _selectedItem = value;

                //Controller.SelectNode(_selectedItem, true);

                if (_selectedItem != null)
                {
                    _tempItem = _selectedItem.Clone();
                    
                    if (_selectedItem is Group)
                    {
                        Controller.EnableRemoveGroupButton(true);
                    }
                }
            }
        }

        public bool HasSelected
        {
            get
            {
                return SelectedItem != null;
            }
        }

        private Mode _drawingMode;
        public Mode DrawingMode
        {
            get
            {
                return _drawingMode;
            }
            set
            {
                _drawingMode = value;
                _tempItem = null;
                SetButtonClicked();
            }
        }

        public Canvas()
        {
            DrawingMode = Mode.Rectange;
        }

        public void AddItem(DrawableItem item)
        {
            Items.Add(item);
            Controller.AddNode(item);
            item.Parent = null;
        }

        public void AddGroup(Group group, int index)
        {
            if(group.Parent == null)
            {
                foreach (var item in group.Items)
                {
                    RemoveItem(item);
                }

                AddItem(group, index);
            }

            else if(group.Parent is Group)
            {
                Group parent = group.Parent as Group;
                foreach (var item in group.Items)
                {
                    parent.RemoveItem(item);
                }

                parent.AddItem(group, index);
            }
            
        }

        public void AddItem(DrawableItem item, int index)
        {
            Items.Insert(index, item);
            Controller.AddNode(item, index);
            item.Parent = null;
        }

        public void AddRange(List<DrawableItem> items)
        {
            Items.AddRange(items);
            Controller.AddNode(items);
        }

        public void RemoveItem(DrawableItem item)
        {
            Items.Remove(item);
            Controller.RemoveNode(item);
        }

        public int RemoveGroup(Group group)
        {
            int index;
            if (group.Parent == null)
            {
                index = Items.IndexOf(group);
                RemoveItem(group as DrawableItem);

                List<DrawableItem> items = group.Items;
                items.Reverse();

                foreach (var item in items)
                {
                    AddItem(item, index);
                }
            }
            else
            {
                Group parent = group.Parent as Group;
                index = parent.Items.IndexOf(group);
                parent.RemoveItem(group);

                List<DrawableItem> items = group.Items;
                items.Reverse();

                foreach (var item in items)
                {
                    parent.AddItem(item, index);
                }
            }

            return index;
        }

        public void Draw(Graphics graphics)
        {
            IVisitor drawVisitor = new DrawVisitor(graphics);
            foreach(var item in Items)
            {
                item.Accept(drawVisitor);
            }
            _tempItem?.Accept(drawVisitor);
        }

        #region For Showing animation while changing

        private DrawableItem _tempItem = null;
        public void IsCreating(Point begin, Point end)
        {
            if(_tempItem == null)
            {
                if (DrawingMode == Mode.Rectange)
                {
                    _tempItem = new Rectangle(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y);
                }
                else
                {
                    _tempItem = new Ellipse(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y);
                }
                _tempItem.Color = Color.Gray;
            }
            else
            {
                _tempItem.Accept(new TempResizeVisitor(begin, end));
            }

            Controller.InvalidateCanvas();
        }

        public void IsMoving(Point begin, Point end)
        {
            if(HasSelected)
            {
                _tempItem.Accept(new TempMoveVisitor(begin, end));

                Controller.InvalidateCanvas();
            }
        }

        public void IsResizing(Point begin, Point end)
        {
            if (HasSelected)
            {
                _tempItem.Accept(new TempResizeVisitor(begin, end));

                Controller.InvalidateCanvas();
            }            
        }

        public void ClearTempItem()
        {
            _tempItem = null;
        }

        #endregion

        public void OpenFile(string path)
        {
            List<DrawableItem> readedItems = FileParser.Instance.ReadFile(path);

            if(readedItems != null)
            {
                Items.Clear();
                Controller.ClearTree();

                AddRange(readedItems);
                
                Controller.InvalidateCanvas();
            }
        }

        public void SaveFile(string path)
        {
            if(FileParser.Instance.WriteFile(path, Items))
            {
                if(MessageBox.Show("Bestand succesvol opgeslagen\n Wilt u het canvas legen?", "The Painter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CommandExecuter.Execute(new ClearCanvas());
                }
            }
        }

        public void SetTempData(List<DrawableItem> items, DrawableItem selectedItem)
        {
            Items = items;
            _selectedItem = selectedItem;

            Controller.ClearTree();
            Controller.AddNode(Items);

            Controller.InvalidateCanvas();
        }

        public void ClearCanvas()
        {
            Items.Clear();
            _selectedItem = null;
            _tempItem = null;

            CommandExecuter.Clear();

            Controller.ClearTree();
            Controller.InvalidateCanvas();
        }

        private void SetButtonClicked()
        {
            Color rectangleColor = DrawingMode == Mode.Rectange ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);
            Color ellipseColor = DrawingMode == Mode.Ellipse ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);

            Controller.SetButtonCLickedColors(rectangleColor, ellipseColor);
        }

    }
}
