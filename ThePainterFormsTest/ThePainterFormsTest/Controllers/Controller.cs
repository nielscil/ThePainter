using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Controllers
{

    public class Controller
    {

        private static Controller _instance;
        public static Controller Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new Controller();
                }
                return _instance;
            }
        }

        private Form1 _form;
        private Canvas _canvas;

        private Controller() { }

        public void LoadApplication()
        {
            _form = new Form1();
            _canvas = new Canvas();

            _form.Canvas.Paint += Canvas_Paint;
            _form.Canvas.MouseDown += Canvas_MouseDown;
            _form.Canvas.MouseUp += Canvas_MouseUp;
            _form.RectangleButton.Click += RectangleButton_Click;
            _form.EllipseButton.Click += Ellipse_Click;
            _form.Canvas.MouseMove += Canvas_MouseMove;
            _form.KeyPreview = true;
            _form.KeyDown += _form_KeyDown;
            _form.OpenFileButton.Click += OpenFileButton_Click;
            _form.SaveFileButton.Click += SaveFileButton_Click;
            _form.ListBox.SelectionMode = SelectionMode.MultiExtended;
            Application.Run(_form);
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            _canvas.Draw(e.Graphics);
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            
        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Filter = "ThePainter files (*.pntr)|*.pntr";
            dialog.Title = "Selecteer Bestand";
            if(dialog.ShowDialog() == DialogResult.OK && File.Exists(dialog.FileName))
            {
                FileParser.Instance.ReadFile(dialog.FileName);
            }
        }

        private void _form_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Z)
            {
                _canvas.Undo();
            }
            else if(e.Control && e.KeyCode == Keys.Y)
            {
                _canvas.Redo();
            }
        }

        private void Ellipse_Click(object sender, EventArgs e)
        {
            _canvas.SetDrawingMode(Canvas.Mode.Ellipse);
        }

        private void RectangleButton_Click(object sender, EventArgs e)
        {
            _canvas.SetDrawingMode(Canvas.Mode.Rectange);
        }

        public void SetButtonCLickedColors(Color rectangleColor, Color ellipseColor)
        {
            _form.EllipseButton.BackColor = ellipseColor;
            _form.RectangleButton.BackColor = rectangleColor;
        }


        public void InvalidateCanvas()
        {
            _form.Canvas.Invalidate();
        }

        public void AddToListBox(DrawableItem item)
        {
            ListBox listBox = _form.ListBox;
            listBox.BeginUpdate();

            listBox.Items.Add(item);

            listBox.EndUpdate();
        }

        public void AddToListBox(List<DrawableItem> items)
        {
            ListBox listBox = _form.ListBox;
            listBox.BeginUpdate();

            foreach(var item in items)
            {
                listBox.Items.Add(item);
            }

            listBox.EndUpdate();
        }

        public void RemoveFromListBox(DrawableItem item)
        {
            ListBox listBox = _form.ListBox;
            listBox.BeginUpdate();

            listBox.Items.Remove(item);

            listBox.EndUpdate();
        }

        public void RemoveFromListBox(List<DrawableItem> items)
        {
            ListBox listBox = _form.ListBox;
            listBox.BeginUpdate();

            foreach (var item in items)
            {
                listBox.Items.Remove(item);
            }

            listBox.EndUpdate();
        }

        public void ClearListBox()
        {
            _form.ListBox.Items.Clear();
        }

        private Point _begin = Point.Empty;
        private Point _deltaBegin = Point.Empty;

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !_canvas.HasSelected)
            {
                _canvas.CreateItem(_begin, e.Location);
                _begin = Point.Empty;
            }

            if(e.Button == MouseButtons.Right && _canvas.HasSelected)
            {
                _canvas.ResizeItem(_begin, e.Location);
                _begin = Point.Empty;
            }
            
            if(e.Button == MouseButtons.Left)
            {
                if(e.Location == _begin)
                {
                    _canvas.SelectItemWithDeselect(e.Location);
                }
                else
                {
                    _canvas.MoveItem(_begin, e.Location);
                }
                
            }
        }

        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _begin = e.Location;
            _deltaBegin = e.Location;
        }

        private void Canvas_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !_canvas.HasSelected)
            {
                //adding
                if (!_deltaBegin.IsEmpty)
                {
                    _canvas.IsCreating(_deltaBegin, e.Location);
                    _deltaBegin = e.Location;
                }
            }

            if (e.Button == MouseButtons.Right && _canvas.HasSelected)
            {
                //resizing
                if (!_deltaBegin.IsEmpty)
                {
                    _canvas.IsResizing(_deltaBegin, e.Location);
                    _deltaBegin = e.Location;
                }
            }

            else if (e.Button == MouseButtons.Left && _canvas.HasSelected)
            {
                //moving
                if (!_deltaBegin.IsEmpty)
                {
                    _canvas.IsMoving(_deltaBegin, e.Location);
                    _deltaBegin = e.Location;
                }
            }


        }
    }
}
