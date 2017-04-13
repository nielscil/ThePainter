using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Commands;
using ThePainterFormsTest.Controls;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.States;

namespace ThePainterFormsTest.Controllers
{

    /// <summary>
    /// Controller class of the application
    /// </summary>
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

        /// <summary>
        /// Loads the application
        /// </summary>
        public void LoadApplication()
        {
            SetForm();

            SetCanvas();

            SetButtonEventListeners();

            SetListBoxEventListeners();
              
            Application.Run(_form);
        }

        /// <summary>
        /// Sets form settings
        /// </summary>
        private void SetForm()
        {
            _form = new Form1();
            _form.KeyPreview = true;
            _form.KeyDown += _form_KeyDown;
        }

        /// <summary>
        /// Sets canvas on form settings
        /// </summary>
        private void SetCanvas()
        {
            _canvas = new Canvas();
            _form.Canvas.Paint += Canvas_Paint;
            _form.Canvas.MouseDown += Canvas_MouseDown;
            _form.Canvas.MouseUp += Canvas_MouseUp;
            _form.Canvas.MouseMove += Canvas_MouseMove;

            CommandExecuter.Init(_canvas);
        }

        /// <summary>
        /// Sets button event listeners
        /// </summary>
        private void SetButtonEventListeners()
        {
            _form.RectangleButton.Click += RectangleButton_Click;
            _form.EllipseButton.Click += Ellipse_Click;

            _form.OpenFileButton.Click += OpenFileButton_Click;
            _form.SaveFileButton.Click += SaveFileButton_Click;

            _form.ClearCanvasButton.Click += ClearCanvasButton_Click;
            _form.AddGroupButton.Click += AddGroupButton_Click;
            _form.RemoveGroupButton.Click += RemoveGroupButton_Click;
            _form.AddOrnamentButton.Click += AddOrnament_Click;
        }

        /// <summary>
        /// Handles click event from ornament button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddOrnament_Click(object sender, EventArgs e)
        {
            using (var form = new OrnamentForm())
            {
                var result = form.ShowDialog();

                if(result == DialogResult.OK)
                {
                    string text = form.OrnamentText;
                    IOrnamentState state = form.State;
                    int index = GetIndex(_canvas.SelectedItem);

                    if (index != -1 && state != null)
                    {
                        CommandExecuter.Execute(new AddOrnament(_canvas.SelectedItem, text, state, index));
                    }
                }
            }
        }

        /// <summary>
        /// Handles click event from remove group button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RemoveGroupButton_Click(object sender, EventArgs e)
        {
            if(_canvas.SelectedItem is Group)
            {
                CommandExecuter.Execute(new RemoveGroup(_canvas.SelectedItem as Group));
            }
        }

        /// <summary>
        /// Handles click event from add group button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void AddGroupButton_Click(object sender, EventArgs e)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            foreach(var item in _form.TreeView.SelectedItems)
            {
                items.Add(item.Owner);
            }

            if (items.Count > 0)
            {
                DrawableItem item;
                int index = GetLowestIndex(items, out item);

                if (index != -1 && index != int.MaxValue)
                {
                    CommandExecuter.Execute(new AddGroup(items, index, item.Parent));
                }
            } 
        }

        /// <summary>
        /// Gets the lowest index of a list of items
        /// </summary>
        /// <param name="items">items</param>
        /// <param name="parent">parent</param>
        /// <returns>lowest index</returns>
        private int GetLowestIndex(List<DrawableItem> items, out DrawableItem parent)
        {
            int low = int.MaxValue;
            parent = items[0];

            if(parent.Parent == null)
            {
                foreach (var item in items)
                {
                    int newLow = _canvas.Items.IndexOf(item);

                    if (newLow < low)
                    {
                        low = newLow;
                        parent = item;
                    }
                }
            }
            else if(parent.Parent is Group)
            {
                Group temp = parent.Parent as Group;
                foreach (var item in items)
                {
                    int newLow = temp.Items.IndexOf(item);

                    if (newLow < low)
                    {
                        low = newLow;
                        parent = item;
                    }
                }
            }
            return low;
        }

        /// <summary>
        /// Gets the index of a item
        /// </summary>
        /// <param name="item">item</param>
        /// <returns>index</returns>
        private int GetIndex(DrawableItem item)
        {
            int index = -1;

            if(item.Parent == null)
            {
                index = _canvas.Items.IndexOf(item);
            }
            else if(item.Parent is Group)
            {
                index =(item.Parent as Group).Items.IndexOf(item);
            }

            return index;
        }

        private bool _listBoxHasFocus = false;

        /// <summary>
        /// Set listbox eventlisteners
        /// </summary>
        private void SetListBoxEventListeners()
        {
            _form.TreeView.GotFocus += (s, e) => { _listBoxHasFocus = true; };
            _form.TreeView.LostFocus += (s, e) => { _listBoxHasFocus = false; };
            _form.TreeView.SelectionChanged += ListBox_SelectedValueChanged;
        }

        /// <summary>
        /// Handles listbox selected value change event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ListBox_SelectedValueChanged(object sender, EventArgs e)
        {   
            if(_listBoxHasFocus)
            {
                PainterTreeView treeView = (PainterTreeView)sender;

                if (treeView.SelectedItems.Count == 1)
                {
                    CommandExecuter.Execute(new SelectItemWithDeselectFromTree(treeView.SelectedItems[0].Owner));
                }

                if (treeView.SelectedItems.Count == 0 || treeView.SelectedItems.Count > 1)
                {
                    CommandExecuter.Execute(new DeselectItemFromTree(_canvas.SelectedItem));
                }
            }
        }

        /// <summary>
        /// Handles click event on clear canvas button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ClearCanvasButton_Click(object sender, EventArgs e)
        {
            if(MessageBox.Show("Weet u zeker dat u het canvas wil legen?","The Painter",MessageBoxButtons.YesNo,MessageBoxIcon.Question) == DialogResult.Yes)
            {
                CommandExecuter.Execute(new ClearCanvas());
            }
        }

        /// <summary>
        /// Handles in on paint event of canvas
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            _canvas.Draw(e.Graphics);
        }

        /// <summary>
        /// Handles save button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SaveFileButton_Click(object sender, EventArgs e)
        {
            SaveFileDialog dialog = new SaveFileDialog();
            dialog.OverwritePrompt = true;
            dialog.Filter = "ThePainter files (*.pntr)|*.pntr";
            dialog.Title = "Save File";

            if(dialog.ShowDialog() == DialogResult.OK)
            {
                CommandExecuter.Execute(new SaveFile(dialog.FileName));
            }

        }

        /// <summary>
        /// Handles open file button click event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OpenFileButton_Click(object sender, EventArgs e)
        {
            OpenFileDialog dialog = new OpenFileDialog();
            dialog.CheckFileExists = true;
            dialog.Multiselect = false;
            dialog.Filter = "ThePainter files (*.pntr)|*.pntr";
            dialog.Title = "Open File";

            if(dialog.ShowDialog() == DialogResult.OK && File.Exists(dialog.FileName))
            {
                CommandExecuter.Execute(new OpenFile(dialog.FileName));
            }

        }

        /// <summary>
        /// Handles keydown event on form for undo and redo actions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void _form_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Control && e.KeyCode == Keys.Z)
            {
                CommandExecuter.Undo();
            }
            else if(e.Control && e.KeyCode == Keys.Y)
            {
                CommandExecuter.Redo();
            }
        }

        /// <summary>
        /// Handles ellipse button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Ellipse_Click(object sender, EventArgs e)
        {
            CommandExecuter.Execute(new ChangeDrawingMode(Canvas.Mode.Ellipse));
        }
        
        /// <summary>
        /// Handles rectangle button click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void RectangleButton_Click(object sender, EventArgs e)
        {
            CommandExecuter.Execute(new ChangeDrawingMode(Canvas.Mode.Rectange));
        }

        /// <summary>
        /// Sets the right color to the buttons
        /// </summary>
        /// <param name="rectangleColor">color for rectangle</param>
        /// <param name="ellipseColor">color for ellipse</param>
        public void SetButtonCLickedColors(Color rectangleColor, Color ellipseColor)
        {
            _form.EllipseButton.BackColor = ellipseColor;
            _form.RectangleButton.BackColor = rectangleColor;
        }

        /// <summary>
        /// Invalidate canvas
        /// </summary>
        public void InvalidateCanvas()
        {
            _form.Canvas.Invalidate();
        }

        #region TreeList functions
        /// <summary>
        /// Adds node to treelist
        /// </summary>
        /// <param name="drawableItem">item</param>
        public void AddNode(DrawableItem drawableItem)
        {
            _form.TreeView.BeginUpdate();
            _form.TreeView.Nodes.Add(drawableItem.Node);
            _form.TreeView.EndUpdate();
            _form.TreeView.ExpandAll();
        }

        /// <summary>
        /// Inserts node on index in treelist
        /// </summary>
        /// <param name="drawableItem">item</param>
        /// <param name="index">index</param>
        public void AddNode(DrawableItem drawableItem, int index)
        {
            _form.TreeView.BeginUpdate();
            _form.TreeView.Nodes.Insert(index, drawableItem.Node);
            _form.TreeView.EndUpdate();
            _form.TreeView.ExpandAll();
        }

        /// <summary>
        /// Add list of nodes
        /// </summary>
        /// <param name="items">items</param>
        public void AddNode(List<DrawableItem> items)
        {
            _form.TreeView.BeginUpdate();

            foreach (var item in items)
            {
                _form.TreeView.Nodes.Add(item.Node);
            }

            _form.TreeView.EndUpdate();
            _form.TreeView.ExpandAll();
        }

        /// <summary>
        /// Removes node from treelist
        /// </summary>
        /// <param name="drawableItem">item</param>
        public void RemoveNode(DrawableItem drawableItem)
        {
            _form.TreeView.BeginUpdate();

            _form.TreeView.Nodes.Remove(drawableItem.Node);

            _form.TreeView.EndUpdate();
            _form.TreeView.ExpandAll();
        }

        /// <summary>
        /// Removes list of nodes from treelist
        /// </summary>
        /// <param name="drawableItems">items</param>
        public void RemoveNode(List<DrawableItem> drawableItems)
        {
            _form.TreeView.BeginUpdate();

            foreach (var item in drawableItems)
            {
                _form.TreeView.Nodes.Remove(item.Node);
            }

            _form.TreeView.EndUpdate();
            _form.TreeView.ExpandAll();
        }

        /// <summary>
        /// Select a node
        /// </summary>
        /// <param name="item">item</param>
        /// <param name="value">select or deselect</param>
        public void SelectNode(DrawableItem item, bool value)
        {
            if (item == null)
                return;

            bool oldValue = _listBoxHasFocus;
            _listBoxHasFocus = false;

            bool isInCollection = _form.TreeView.Nodes.IsInCollection(item.Node);//Nodes.IndexOf(item.Node);
            if (isInCollection)
            {
                if (value)
                {
                    _form.TreeView.SelectedItems.Add(item.Node);
                }
                else if (_form.TreeView.SelectedItems.Count == 1)
                {
                    _form.TreeView.SelectedItems.Remove(item.Node);
                }
            }

            _listBoxHasFocus = oldValue;
        }

        /// <summary>
        /// Clear the treelist
        /// </summary>
        public void ClearTree()
        {
            _form.TreeView.Nodes.Clear();
        }

        #endregion

        /// <summary>
        /// Enables or disables remove group button
        /// </summary>
        /// <param name="enabled">enabled or disabled</param>
        public void EnableRemoveGroupButton(bool enabled)
        {
            _form.RemoveGroupButton.Enabled = enabled;
        }

        /// <summary>
        /// Enables or disables add ornament button
        /// </summary>
        /// <param name="enabled">enabled or disabled</param>
        public void EnableAddOrnamentButton(bool enabled)
        {
            _form.AddOrnamentButton.Enabled = enabled;
        }

        #region Mouse events
        private Point _begin = Point.Empty;
        private Point _deltaBegin = Point.Empty;

        /// <summary>
        /// Handles mouse up event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !_canvas.HasSelected)
            {
                CreateItem(_begin, e.Location);
                _begin = Point.Empty;
            }

            if(e.Button == MouseButtons.Right && _canvas.HasSelected)
            {
                ResizeItem(_begin, e.Location);
                _begin = Point.Empty;
            }
            
            if(e.Button == MouseButtons.Left)
            {
                if(e.Location == _begin)
                {
                    SelectItemWithDeselect(e.Location);
                    
                }
                else if(_canvas.HasSelected)
                {
                    CommandExecuter.Execute(new MoveItem(_canvas.SelectedItem, _begin, e.Location));
                }
                
            }
        }

        /// <summary>
        /// Handles mouse down event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseDown(object sender, MouseEventArgs e)
        {
            _begin = e.Location;
            _deltaBegin = e.Location;
        }

        /// <summary>
        /// Handles mouse move event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Create item
        /// </summary>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public void CreateItem(Point begin, Point end)
        {
            _canvas.ClearTempItem();

            if (end != begin)
            {
                if (!_canvas.HasSelected)
                {
                    if (_canvas.DrawingMode == Canvas.Mode.Rectange)
                    {
                        CommandExecuter.Execute(new AddRectangle(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y));
                    }
                    else
                    {
                        CommandExecuter.Execute(new AddEllipse(begin.X, begin.Y, end.X - begin.X, end.Y - begin.Y));
                    }
                }
                else
                {
                    ResizeItem(begin, end);
                }
            }
        }

        /// <summary>
        /// Resize item
        /// </summary>
        /// <param name="begin">begin point</param>
        /// <param name="end">end point</param>
        public void ResizeItem(Point begin, Point end)
        {
            if (_canvas.HasSelected)
            {
                CommandExecuter.Execute(new ResizeItem(_canvas.SelectedItem, begin, end));
            }
        }

        #endregion

        /// <summary>
        /// Selects on location
        /// </summary>
        /// <param name="location">location</param>
        public void SelectItemWithDeselect(Point location)
        {
            foreach (var item in _canvas.Items)
            {
                if (item.IsOnLocation(location))
                {
                    CommandExecuter.Execute(new SelectItemWithDeselect(item));
                    return;
                }
            }

            DeSelect();
        }

        /// <summary>
        /// Deselects
        /// </summary>
        public void DeSelect()
        {
            if (_canvas.HasSelected)
            {
                CommandExecuter.Execute(new DeselectItem(_canvas.SelectedItem));
            }
        }
    }
}
