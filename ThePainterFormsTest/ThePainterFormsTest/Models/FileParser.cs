using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ThePainterFormsTest.Models
{
    public class FileParser
    {

        private const string GROUP = "group";
        private const string RECTANGLE = "rectangle";
        private const string ELLIPSE = "ellipse";
        private const string ORNAMENT = "ornament";
        private const string TAB = "\t";

        private int Line { get; set; }

        private static FileParser _instance;
        public static FileParser Instance
        {
            get
            {
                if(_instance == null)
                {
                    _instance = new FileParser();
                }
                return _instance;
            }
        }

        private FileParser() { }

        #region Parser

        public List<ICanvasItem> ReadFile(string path)
        {
            if (!File.Exists(path))
            {
                MessageBox.Show("File not found!", "The Painter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }

            Line = 0;

            using (StreamReader stream = File.OpenText(path))
            {
                return Parser(stream);
            }
        }

        private List<ICanvasItem> Parser(StreamReader reader)
        {
            List<ICanvasItem> items = new List<ICanvasItem>();

            while(!reader.EndOfStream)
            {
                try
                {
                    Line++;
                    string line = reader.ReadLine();
                    string itemName = GetItem(line);

                    switch (itemName)
                    {
                        case GROUP:
                            //TODO: add group
                            break;
                        case ORNAMENT:
                            //TODO: add text
                            break;
                        case ELLIPSE:
                            items.Add(EllipseParser(line));
                            break;
                        case RECTANGLE:
                            items.Add(RectangleParser(line));
                            break;
                    }
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "The Painter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return items;
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
            string[] options = GetOptions(line);
            int x, y, width, height;

            if(options.Length == 4)
            {
                bool canParseX = int.TryParse(options[0], out x);
                bool canParseY = int.TryParse(options[1], out y);
                bool canParseWidth = int.TryParse(options[2], out width);
                bool canParseHeight = int.TryParse(options[3], out height);

                if (canParseX && canParseY && canParseWidth && canParseHeight)
                {
                    return new Rectangle(x, y, width, height);
                }
            }

            throw new Exception($"Line[{Line}]: Could not parse Rectangle");
        }

        private Ellipse EllipseParser(string line)
        {
            string[] options = GetOptions(line);
            int x, y, width, height;

            if (options.Length == 4)
            {
                bool canParseX = int.TryParse(options[0], out x);
                bool canParseY = int.TryParse(options[1], out y);
                bool canParseWidth = int.TryParse(options[2], out width);
                bool canParseHeight = int.TryParse(options[3], out height);

                if (canParseX && canParseY && canParseWidth && canParseHeight)
                {
                    return new Ellipse(x, y, width, height);
                }
            }

            throw new Exception($"Line[{Line}]: Could not parse Ellipse");
        }

        private string GetItem(string line)
        {
            string[] lineArray = line.Split(' ');
            
            if(lineArray.Length > 0)
            {
                return lineArray[0];
            }

            throw new Exception($"Line[{Line}]: Item not found");
        }

        private string[] GetOptions(string line)
        {
            string[] lineArray = line.Split(' ');

            if(lineArray.Length > 0)
            {
                return lineArray.Skip(1).ToArray();
            }

            throw new Exception("No options found");
        }

        #endregion

        #region Writer

        public bool WriteFile(string path, List<ICanvasItem> items)
        {
            try
            {
                using (var streamWriter = File.CreateText(path))
                {
                    streamWriter.Write(SerializeItems(items));
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message, "The Painter", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private string SerializeItems(List<ICanvasItem> items)
        {
            string serialized = "";

            foreach(var item in items)
            {
                serialized += GetSerializedItem(item);
            }

            return serialized;
        }

        private string GetSerializedItem(ICanvasItem item, string prefix = "")
        {
            switch (item.ToString())
            {
                case RECTANGLE:
                    return prefix + Serialize(item) + Environment.NewLine;
                case ELLIPSE:
                    return prefix + Serialize(item) + Environment.NewLine;
                case GROUP:
                    return prefix + SerializeGroup(item, prefix) + Environment.NewLine;
                case ORNAMENT:
                    return prefix + Serialize(item) + Environment.NewLine;
            }

            throw new Exception("No object found");
        }

        private string SerializeGroup(ICanvasItem item, string prefix = "")
        {
            string serialized = "";

            //add group
            serialized += Serialize(item) + Environment.NewLine;
            prefix += Environment.NewLine;

            //add sub items with tab in front!

            return serialized;
        }

        private string Serialize(ICanvasItem item)
        {
            return item.Serialize();
        }

        #endregion

    }
}
