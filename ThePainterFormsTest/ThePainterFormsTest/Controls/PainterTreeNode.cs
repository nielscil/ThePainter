using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Controls
{
    public class PainterTreeNode : TreeNode
    {
        public DrawableItem Owner { get; private set; }

        public PainterTreeNode(DrawableItem item) : base(item.ToString())
        {
            Owner = item;
        }
    }
}
