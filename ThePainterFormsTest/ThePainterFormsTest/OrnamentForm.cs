using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.States;

namespace ThePainterFormsTest
{
    public partial class OrnamentForm : Form
    {

        public string OrnamentText
        {
            get
            {
                return textBox1.Text;
            }
        }

        public IOrnamentState State
        {
            get
            {
                return comboBox1.SelectedItem as IOrnamentState;
            }
        }


        public OrnamentForm()
        {
            InitializeComponent();

            foreach(var item in GetStates())
            {
                comboBox1.Items.Add(item);
            }

            SetEvents();
        }


        private void SetEvents()
        {
            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
        }


        private void PrepareCloseAction()
        {
            button1.Click -= Button1_Click;
            button2.Click -= Button2_Click;
            Close();
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            PrepareCloseAction();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            PrepareCloseAction();
        }

        private IEnumerable<IOrnamentState> GetStates()
        {
            yield return OrnamentBottom.Instance;
            yield return OrnamentTop.Instance;
            yield return OrnamentLeft.Instance;
            yield return OrnamentRight.Instance;
        }
    }
}
