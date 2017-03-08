using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.Commands
{
    class SaveFile : ICommand
    {

        private string _filePath;

        public SaveFile(string filePath)
        {
            _filePath = filePath;
        }

        public void Execute(Canvas canvas)
        {
            canvas.SaveFile(_filePath);
        }

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
