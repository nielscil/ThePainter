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
using ThePainterFormsTest.Strategy;

namespace ThePainterFormsTest.Models
{
    /// <summary>
    /// Canvas class
    /// </summary>
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
                    Controller.EnableAddOrnamentButton(false);
                    _tempItem = null;
                }

                _selectedItem = value;

                if (_selectedItem != null)
                {
                    _tempItem = _selectedItem.Clone();
                    
                    if (_selectedItem is Group)
                    {
                        Controller.EnableRemoveGroupButton(true);
                    }

                    if(!(_selectedItem is Ornament))
                    {
                        Controller.EnableAddOrnamentButton(true);
                    }
                    else
                    {
                        Controller.EnableAddOrnamentButton(false);
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

        /// <summary>
        /// Constructor
        /// </summary>
        public Canvas()
        {
            DrawingMode = Mode.Rectange;
        }

        /// <summary>
        /// Add item
        /// </summary>
        /// <param name="item">item</param>
        public void AddItem(DrawableItem item)
        {
            Items.Add(item);
            Controller.AddNode(item);
            item.Parent = null;
        }

        /// <summary>
        /// Add group
        /// </summary>
        /// <param name="group">group</param>
        /// <param name="index">index</param>
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

        /// <summary>
        /// Adds ornament
        /// </summary>
        /// <param name="ornament">ornament</param>
        /// <param name="index">index</param>
        public void AddOrnament(Ornament ornament,int index)
        {
            if (ornament.Parent == null)
            {
                AddItem(ornament, index);
            }
            else if (ornament.Parent is Group)
            {
                (ornament.Parent as Group).AddItem(ornament, index);
            }
        }

        /// <summary>
        /// Adds item on index
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="index">index</param>
        public void AddItem(DrawableItem item, int index)
        {
            Items.Insert(index, item);
            Controller.AddNode(item, index);
            item.Parent = null;
        }

        /// <summary>
        /// Adds range
        /// </summary>
        /// <param name="items">items</param>
        public void AddRange(List<DrawableItem> items)
        {
            Items.AddRange(items);
            Controller.AddNode(items);
        }

        /// <summary>
        /// Remove item
        /// </summary>
        /// <param name="item">item</param>
        public void RemoveItem(DrawableItem item)
        {
            Items.Remove(item);
            Controller.RemoveNode(item);
        }

        /// <summary>
        /// Remove group
        /// </summary>
        /// <param name="group">remove</param>
        /// <returns>index</returns>
        public int RemoveGroup(Group group)
        {
            int index;
            if (group.Parent == null)
            {
                index = Items.IndexOf(group);
                RemoveItem(group);

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

        /// <summary>
        /// Remove ornament
        /// </summary>
        /// <param name="ornament">ornament</param>
        /// <returns>index</returns>
        public int RemoveOrnament(Ornament ornament)
        {
            int index;
            if (ornament.Parent == null)
            {
                index = Items.IndexOf(ornament);
                RemoveItem(ornament);
            }
            else
            {
                Group parent = ornament.Parent as Group;
                index = parent.Items.IndexOf(ornament);
                parent.RemoveItem(ornament);
            }

            return index;
        }

        /// <summary>
        /// Draws all items
        /// </summary>
        /// <param name="graphics">graphics</param>
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
        /// <summary>
        /// Showing the creation animation
        /// </summary>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public void IsCreating(Point begin, Point end)
        {
            if(_tempItem == null)
            {
                if (DrawingMode == Mode.Rectange)
                {
                    _tempItem = new BasicFigure(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y, RectangleStrategyObject.Instance);
                }
                else
                {
                    _tempItem = new BasicFigure(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y, EllipseStrategyObject.Instance);
                }
                _tempItem.Color = Color.Gray;
            }
            else
            {
                _tempItem.Accept(new TempResizeVisitor(begin, end));
            }

            Controller.InvalidateCanvas();
        }

        /// <summary>
        /// Is moving animation
        /// </summary>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public void IsMoving(Point begin, Point end)
        {
            if(HasSelected)
            {
                _tempItem.Accept(new TempMoveVisitor(begin, end));

                Controller.InvalidateCanvas();
            }
        }

        /// <summary>
        /// Resizing animation
        /// </summary>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public void IsResizing(Point begin, Point end)
        {
            if (HasSelected)
            {
                _tempItem.Accept(new TempResizeVisitor(begin, end));

                Controller.InvalidateCanvas();
            }            
        }

        /// <summary>
        /// Clears the temp item
        /// </summary>
        public void ClearTempItem()
        {
            _tempItem = null;
        }

        #endregion

        /// <summary>
        /// Open file
        /// </summary>
        /// <param name="path">path to file</param>
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

        /// <summary>
        /// Saves to file
        /// </summary>
        /// <param name="path">path to file</param>
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

        /// <summary>
        /// Set temp data
        /// </summary>
        /// <param name="items">items</param>
        /// <param name="selectedItem">selected item</param>
        public void SetTempData(List<DrawableItem> items, DrawableItem selectedItem)
        {
            Items = items;
            _selectedItem = selectedItem;

            Controller.ClearTree();
            Controller.AddNode(Items);

            Controller.InvalidateCanvas();
        }

        /// <summary>
        /// Clear canvas
        /// </summary>
        public void ClearCanvas()
        {
            Items.Clear();
            _selectedItem = null;
            _tempItem = null;

            CommandExecuter.Clear();

            Controller.ClearTree();
            Controller.InvalidateCanvas();
        }

        /// <summary>
        /// Set button clicked
        /// </summary>
        private void SetButtonClicked()
        {
            Color rectangleColor = DrawingMode == Mode.Rectange ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);
            Color ellipseColor = DrawingMode == Mode.Ellipse ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);

            Controller.SetButtonCLickedColors(rectangleColor, ellipseColor);
        }

    }
}
