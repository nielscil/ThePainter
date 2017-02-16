using System;
using System.Collections.Generic;
using System.Drawing;
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
            _form.Ellipse.Click += Ellipse_Click;
            _form.Canvas.MouseMove += Canvas_MouseMove;
            Application.Run(_form);
        }

        private void Ellipse_Click(object sender, EventArgs e)
        {
            _canvas.DrawingMode = Canvas.Mode.Ellipse;
        }

        private void RectangleButton_Click(object sender, EventArgs e)
        {
            _canvas.DrawingMode = Canvas.Mode.Rectange;
        }

        public void SetButtonCLickedColors(Color rectangleColor, Color ellipseColor)
        {
            _form.Ellipse.BackColor = ellipseColor;
            _form.RectangleButton.BackColor = rectangleColor;
        }


        public void InvalidateCanvas()
        {
            _form.Canvas.Invalidate();
        }

        private void Canvas_Paint(object sender, PaintEventArgs e)
        {
            _canvas.Draw(e.Graphics);
            e.Graphics.Dispose();
        }


        private Point _begin = Point.Empty;
        private Point _deltaBegin = Point.Empty;

        private void Canvas_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && !_canvas.HasSelected)
            {
                Point end = e.Location;

                _canvas.CreateOrChangeItem(_begin, end);
                _begin = Point.Empty;
            }
            
            if(e.Button == MouseButtons.Left)
            {
                _canvas.SelectItem(e.Location);
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
