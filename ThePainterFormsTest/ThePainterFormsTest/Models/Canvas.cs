using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;

namespace ThePainterFormsTest.Models
{
    public class Canvas
    {
        public enum Mode { Rectange, Ellipse };

        private List<DrawableItem> _items = new List<DrawableItem>();
        private DrawableItem _selectedItem = null;

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
                _isCreatingItem = null;
                SetButtonClicked();
            }
        }

        public Canvas()
        {
            DrawingMode = Mode.Rectange;
        }

        public void SelectItem(Point location)
        {
            _selectedItem?.Deselect();
            _selectedItem = null;

            foreach (var item in _items)
            {
                if (item.IsOnLocation(location))
                {
                    item.Select();
                    _selectedItem = item;
                    break;
                }
            }

            Controller.Instance.InvalidateCanvas();
        }

        public void Draw(Graphics graphics)
        {
            foreach(var item in _items)
            {
                item.Draw(graphics);
            }
            _isCreatingItem?.Draw(graphics);
        }

        private DrawableItem _isCreatingItem = null;
        public void IsCreating(Point begin, Point end)
        {
            if(_selectedItem != null)
            {
                _isCreatingItem = _selectedItem;
            }

            if(_isCreatingItem == null)
            {
                _isCreatingItem = CreateItem(begin, end);
                _isCreatingItem.Color = Color.Gray;
            }
            else
            {
                ChangeItem(_isCreatingItem, begin, end);
            }
            Controller.Instance.InvalidateCanvas();
        }

        public void CreateOrChangeItem(Point begin, Point end)
        {
            if (end != begin)
            {

                if (_selectedItem == null)
                {
                    _items.Add(CreateItem(begin, end));
                }
                else
                {
                    ChangeItem(_selectedItem, begin, end);
                }
                _isCreatingItem = null;
                Controller.Instance.InvalidateCanvas();
            }
        }

        private DrawableItem CreateItem(Point begin, Point end)
        {
            DrawableItem item = null;

            if (DrawingMode == Mode.Rectange)
            {
                item = new Rectangle(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y);
            }
            else
            {
                item = new Ellipse(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y);
            }
            return item;
        }

        private void ChangeItem(DrawableItem item, Point begin, Point end)
        {
            item.Move(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y);
        }

        private void SetButtonClicked()
        {
            Color rectangleColor = DrawingMode == Mode.Rectange ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);
            Color ellipseColor = DrawingMode == Mode.Ellipse ? Color.DarkGray : Color.FromKnownColor(KnownColor.Control);

            Controller.Instance.SetButtonCLickedColors(rectangleColor, ellipseColor);
        }

    }
}
