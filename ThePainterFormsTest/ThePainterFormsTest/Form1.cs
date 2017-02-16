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
        public Panel Canvas
        {
            get
            {
                return canvas;
            }
        }

        public Button RectangleButton
        {
            get
            {
                return button1;
            }
        }

        public Button Ellipse
        {
            get
            {
                return button2;
            }
        }

        public Form1()
        {
            InitializeComponent();
        }
    }
}
