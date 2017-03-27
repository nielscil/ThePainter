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
            SetForm();

            SetCanvas();

            SetButtonEventListeners();

            SetListBoxEventListeners();
              
            Application.Run(_form);
        }

        private void SetForm()
        {
            _form = new Form1();
            _form.KeyPreview = true;
            _form.KeyDown += _form_KeyDown;
        }

        private void SetCanvas()
        {
            _canvas = new Canvas();
            _form.Canvas.Paint += Canvas_Paint;
            _form.Canvas.MouseDown += Canvas_MouseDown;
            _form.Canvas.MouseUp += Canvas_MouseUp;
            _form.Canvas.MouseMove += Canvas_MouseMove;
        }

        private void SetButtonEventListeners()
        {
            _form.RectangleButton.Click += RectangleButton_Click;
            _form.EllipseButton.Click += Ellipse_Click;

            _form.OpenFileButton.Click += OpenFileButton_Click;
            _form.SaveFileButton.Click += SaveFileButton_Click;

            _form.ClearCanvasButton.Click += ClearCanvasButton_Click;
            _form.AddGroupButton.Click += AddGroupButton_Click;
            _form.RemoveGroupButton.Click += RemoveGroupButton_Click;
        }

        private void RemoveGroupButton_Click(object sender, EventArgs e)
        {
            _canvas.RemoveGroup();
        }

        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            foreach(var item in _form.ListBox.SelectedItems)
            {
                items.Add((DrawableItem)item);
            }

            _canvas.AddGroup(items);   
        }

        private bool _listBoxHasFocus = false;
        private void SetListBoxEventListeners()
        {
            _form.ListBox.GotFocus += (s, e) => { _listBoxHasFocus = true; };
            _form.ListBox.LostFocus += (s, e) => { _listBoxHasFocus = false; };
            _form.ListBox.SelectedValueChanged += ListBox_SelectedValueChanged;
        }

        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {   
            if(_listBoxHasFocus)
            {
                ListBox lb = (ListBox)sender;

                if (lb.SelectedItems.Count == 1)
                {
                    _canvas.SelectItemWithDeselect((DrawableItem)lb.SelectedItems[0]);
                }

                if (lb.SelectedItems.Count == 0 || lb.SelectedItems.Count > 1)
                {
                    _canvas.DeSelect();
                }
            }
        }

        private void ClearCanvasButton_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Weet u zeker dat u het canvas wil legen?","The Painter",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                _canvas.Clear();
            }
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            _canvas.Draw(e.Graphics);
        }

        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.Filter = "ThePainter files (*.pntr)|*.pntr";
            dialog.Title = "Save File";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                _canvas.SaveCanvasToFile(dialog.FileName);
            }

        }

        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            dialog.Filter = "ThePainter files (*.pntr)|*.pntr";
            dialog.Title = "Open File";

            if(dialog.ShowDialog() == DialogResult.OK && File.Exists(dialog.FileName))
            {
                _canvas.OpenFileToCanvas(dialog.FileName);
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

        public void AddToListBox(DrawableItem item, int index)
        {
            ListBox listBox = _form.ListBox;
            listBox.BeginUpdate();

            listBox.Items.Insert(index, item);

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

        public void SelectInListBox(DrawableItem item, bool value)
        {
            if (item == null)
                return;

            bool oldValue = _listBoxHasFocus;
            _listBoxHasFocus = false;


            int index = _form.ListBox.Items.IndexOf(item);
            if(index >= 0)
            {
                if(value)
                {
                    _form.ListBox.SelectedItems.Add(item);
                }
                else if(_form.ListBox.SelectedItems.Count == 1)
                {
                    _form.ListBox.SelectedItems.Remove(item);
                }
            }

            _listBoxHasFocus = oldValue;
        }

        public void ClearListBox()
        {
            _form.ListBox.Items.Clear();
        }

        public void EnableRemoveGroupButton(bool enabled)
        {
            _form.RemoveGroupButton.Enabled = enabled;
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
