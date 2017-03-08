using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest
{
    public partial class Form1 : Form
    {
        public PictureBox Canvas
        {
            get
            {
                return pictureBox1;
            }
        }

        public Button RectangleButton
        {
            get
            {
                return button1;
            }
        }

        public Button EllipseButton
        {
            get
            {
                return button2;
            }
        }

        public Button OpenFileButton
        {
            get
            {
                return button3;
            }
        }

        public Button SaveFileButton
        {
            get
            {
                return button4;
            }
        }

        public ListBox ListBox
        {
            get
            {
                return listBox1;
            }
        }

        public Form1()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }
    }
}
