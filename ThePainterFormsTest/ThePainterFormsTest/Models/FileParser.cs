using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    public class FileParser
    {
        private FileParser _instance;
        public FileParser Instance
        {
            get
            {
                if(Instance == null)
                {
                    _instance = new FileParser();
                }
                return _instance;
            }
        }

        private FileParser() { }

       
    }
}
