using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    /// <summary>
    /// Save file commandclass
    /// </summary>
    class SaveFile : ICommand
    {

        private string _filePath;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">path to be saved file</param>
        public SaveFile(string filePath)
        {
            _filePath = filePath;
        }

        /// <summary>
        /// Executes the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Execute(Canvas canvas)
        {
            canvas.SaveFile(_filePath);
        }

        /// <summary>
        /// Undo's the action
        /// </summary>
        /// <param name="canvas"></param>
        public void Undo(Canvas canvas)
        {
            try
            {
                File.Delete(_filePath);
            }
            catch(Exception e)
            {
                //TODO: show error ??
            }
        }
    }
}
