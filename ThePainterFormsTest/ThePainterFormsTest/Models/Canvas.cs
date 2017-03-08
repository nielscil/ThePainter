using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Commands;
using ThePainterFormsTest.Controllers;

namespace ThePainterFormsTest.Models
{
    public class Canvas
    {
        public enum Mode { Rectange, Ellipse };

        private List<DrawableItem> _items = new List<DrawableItem>();

        private DrawableItem _selectedItem = null;
        public DrawableItem SelectedItem
        {
            get
            {
                return _selectedItem;
            }
            set
            {
                if(value == null)
                {
                    _tempItem = null;
                }

                _selectedItem = value;

                if(_selectedItem != null)
                {
                    _tempItem = _selectedItem.Clone();
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

        public void SetDrawingMode(Mode mode)
        {
            PushHistory(new ChangeDrawingMode(mode));
        }

        public void AddItem(DrawableItem item)
        {
            _items.Add(item);
            Controller.Instance.AddToListBox(item);
        }

        public void RemoveItem(DrawableItem item)
        {
            _items.Remove(item);
            Controller.Instance.RemoveFromListBox(item);
        }

        public void SelectItemWithDeselect(Point location)
        {
            foreach (var item in _items)
            {
                if (item.IsOnLocation(location))
                {
                    PushHistory(new SelectItemWithDeselect(item));
                    return;
                }
            }

            if (SelectedItem != null)
            {
                PushHistory(new DeselectItem(SelectedItem));
            }
        }

        public void Draw(Graphics graphics)
        {
            foreach(var item in _items)
            {
                item.Draw(graphics);
            }
            _tempItem?.Draw(graphics);
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
                _tempItem.Resize(begin, end);
            }

            Controller.Instance.InvalidateCanvas();
        }

        public void IsMoving(Point begin, Point end)
        {
            if(HasSelected)
            {
                _tempItem.Move(begin, end);

                Controller.Instance.InvalidateCanvas();
            }
        }

        public void IsResizing(Point begin, Point end)
        {
            if (HasSelected)
            {
                _tempItem.Resize(begin, end);

                Controller.Instance.InvalidateCanvas();
            }            
        }

        #endregion

        public void CreateItem(Point begin, Point end)
        {
            _tempItem = null;

            if (end != begin)
            {
                if(!HasSelected)
                {
                    if (DrawingMode == Mode.Rectange)
                    {
                        PushHistory(new AddRectangle(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y));
                    }
                    else
                    {
                        PushHistory(new AddEllipse(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y));
                    }
                }
                else
                {
                    ResizeItem(begin, end);
                }            
            }
        }

        public void ResizeItem(Point begin, Point end)
        {
            if(HasSelected)
            {
                PushHistory(new ResizeItem(SelectedItem, begin, end));
            }
        }

        public void MoveItem(Point begin, Point end)
        {
            if (HasSelected)
            {
                PushHistory(new MoveItem(SelectedItem, begin, end));
            }
        }

        public void OpenFile(string path)
        {
            List<DrawableItem> readedItems = FileParser.Instance.ReadFile(path);

            if(readedItems != null)
            {
                _items.Clear();
                Controller.Instance.ClearListBox();
                _items.AddRange(readedItems);
                Controller.Instance.AddToListBox(_items);
            }
        }

        public void SaveFile(string path)
        {
            if(FileParser.Instance.WriteFile(path, _items))
            {
                if(MessageBox.Show("Bestand succesvol opgeslagen\n Wilt u het canvas legen?", "The Painter", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    _items.Clear();
                    Controller.Instance.ClearListBox();
                }
            }
        }

        private void SetButtonClicked()
        {
            Color rectangleColor = DrawingMode == Mode.Rectange ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);
            Color ellipseColor = DrawingMode == Mode.Ellipse ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);

            Controller.Instance.SetButtonCLickedColors(rectangleColor, ellipseColor);
        }

        #region History

        private Stack<ICommand> _history = new Stack<ICommand>();
        private Stack<ICommand> _redoHistory = new Stack<ICommand>();

        private void PushHistory(ICommand command)
        {
            if(command != null)
            {
                command.Execute(this);
                _history.Push(command);
            }
            _redoHistory.Clear();
        }

        private ICommand PopHistory()
        {
            if (_history.Count == 0)
            {
                return null;
            }
                
            ICommand command = _history.Pop();
            command.Undo(this);

            return command;
        }

        public void Redo()
        {
            if(_redoHistory.Count > 0)
            {
                ICommand command = _redoHistory.Pop();
                command.Execute(this);
                _history.Push(command);
            }
        }

        public void Undo()
        {
            ICommand command = PopHistory();

            if(command != null)
            {
                _redoHistory.Push(command);
            }
        }

        #endregion

    }
}
