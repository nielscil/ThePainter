using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Controls
{
    /// <summary>
    /// Painter TreeNode
    /// </summary>
    public class PainterTreeNode : TreeNode
    {
        public DrawableItem Owner { get; private set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="item">item for node</param>
        public PainterTreeNode(DrawableItem item) : base(item.ToString())
        {
            Owner = item;
        }
    }
}
