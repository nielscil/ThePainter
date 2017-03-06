using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ThePainterFormsTest.Models
{
    public class FileParser
    {

        private const string GROUP = "group";
        private const string RECTANGLE = "rectangle";
        private const string ELLIPSE = "ellipse";
        private const string ORNAMENT = "ornament";
        private const string POSITION = "position";


        private static FileParser _instance;
        public static FileParser Instance
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

        public Canvas ReadFile(string path)
        {
            if(!File.Exists(path))
            {
                return null;
            }

            using (StreamReader stream = File.OpenText(path))
            {
                
            }
            return null;
        }

        public bool WriteFile(string path, List<DrawableItem> items)
        {
            return true;
        }

        private List<DrawableItem> Parser(StreamReader reader)
        {
            List<DrawableItem> item = new List<DrawableItem>();

            while(!reader.EndOfStream)
            {
                string line = reader.ReadLine();
                string[] array = line.Split(' ');

                if(array.Length > 0)
                {
                    switch(array[0])
                    {
                        case GROUP:

                            break;
                        case ORNAMENT:

                            break;
                        case ELLIPSE:
                            
                            break;
                        case RECTANGLE:

                            break;
                    }
                }
            }

            return item;
        }

        private void OrnamentParser(string line)
        {

        }

        private void GroupParser(StreamReader reader)
        {
            //do stuff
        }

        private Rectangle RectangleParser(string line)
        {
            string[] stringArray = line.Split(' ');
            int x, y, width, height;

            Rectangle rectangle = null;

            if(stringArray.Length == 5)
            {
                bool canParseX = int.TryParse(stringArray[1], out x);
                bool canParseY = int.TryParse(stringArray[2], out y);
                bool canParseWidth = int.TryParse(stringArray[3], out width);
                bool canParseHeight = int.TryParse(stringArray[4], out height);

                if (canParseX && canParseY && canParseWidth && canParseHeight)
                {
                    rectangle = new Rectangle(x, y, width, height);
                }
            }

            return rectangle;
        }

        private Ellipse EllipseParser(string line)
        {
            string[] stringArray = line.Split(' ');
            int x, y, width, height;

            Ellipse ellipse = null;

            if (stringArray.Length == 5)
            {
                bool canParseX = int.TryParse(stringArray[1], out x);
                bool canParseY = int.TryParse(stringArray[2], out y);
                bool canParseWidth = int.TryParse(stringArray[3], out width);
                bool canParseHeight = int.TryParse(stringArray[4], out height);

                if (canParseX && canParseY && canParseWidth && canParseHeight)
                {
                    ellipse = new Ellipse(x, y, width, height);
                }
            }

            return ellipse;
        }

    }
}
