using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    public class AddGroup : ICommand
    {
        private Group _group;
        private int _index;

        public AddGroup(IList<DrawableItem> items, int index)
        {
            _group = new Group(items.ToList());
            _index = index;
        }

        public void Execute(Canvas canvas)
        {
            canvas.AddGroup(_group,_index);
        }

        public void Undo(Canvas canvas)
        {
            canvas.RemoveGroup(_group);
        }
    }
}
