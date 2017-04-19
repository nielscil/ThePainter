using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Add group commandclass
    /// </summary>
    public class AddGroup : ICommand
    {
        private Group _group;
        private int _index;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="items">items for group</param>
        /// <param name="index">index for group</param>
        /// <param name="parent">parent of group</param>
        public AddGroup(IList<DrawableItem> items, int index, DrawableItem parent)
        {
            _group = new Group(items.ToList());
            _group.Parent = parent;
            _index = index;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.AddGroup(_group,_index);
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.RemoveGroup(_group);
            _group.Node = null;
        }
    }
}
