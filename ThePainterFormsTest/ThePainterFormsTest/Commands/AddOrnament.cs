using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Controllers;
using ThePainterFormsTest.Models;
using ThePainterFormsTest.States;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Add ornament commandclass
    /// </summary>
    public class AddOrnament : ICommand
    {
        private DrawableItem _selectedItem;
        private Ornament _ornament;
        private int _index;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="selectedItem">child item</param>
        /// <param name="text">text</param>
        /// <param name="state">state of ornament</param>
        /// <param name="index">index to be placed</param>
        public AddOrnament(DrawableItem selectedItem, string text, IOrnamentState state, int index)
        {
            _index = index;
            _selectedItem = selectedItem;
            _ornament = new Ornament(text, selectedItem, state);
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.AddOrnament(_ornament, _index);

            Controller.Instance.InvalidateCanvas();
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            canvas.RemoveOrnament(_ornament);

            Controller.Instance.InvalidateCanvas();
        }
    }
}
