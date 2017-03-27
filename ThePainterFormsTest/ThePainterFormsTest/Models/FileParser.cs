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

        public List<DrawableItem> ReadFile(string path)
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

        private List<DrawableItem> Parser(StreamReader reader)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            if(!reader.EndOfStream)
            {
                reader.ReadLine();
            }

            while(!reader.EndOfStream)
            {
                try
                {
                    items.Add(GetItem(reader));
                }
                catch(Exception e)
                {
                    MessageBox.Show(e.Message, "The Painter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }

            return items;
        }

        private DrawableItem GetItem(StreamReader reader)
        {
            Line++;
            string line = reader.ReadLine();
            string itemName = GetItemName(line);

            DrawableItem item = null;
            switch (itemName)
            {
                case GROUP:
                    item = GroupParser(reader, GetGroupItemCount(line));
                    break;
                case ORNAMENT:
                    //TODO: add text
                    break;
                case ELLIPSE:
                    item = EllipseParser(line);
                    break;
                case RECTANGLE:
                    item = RectangleParser(line);
                    break;
                default:
                    throw new Exception("Not Parsed");
            }

            return item;
        }

        private void OrnamentParser(string line)
        {

        }

        private int GetGroupItemCount(string line)
        {
            string[] splitted = line.Split(' ');

            int count = 0;

            if(splitted.Length == 2)
            {
                int.TryParse(splitted[1], out count);
            }

            return count;
        }

        private Group GroupParser(StreamReader reader, int count)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            for(int i = 0; i < count;)
            {
                DrawableItem item = GetItem(reader);
                items.Add(item);

                //if(!(item is Ornament)) //ornament does not count as figure!
                //{
                i++;
                //}
            }

            return new Group(items);
        }

        private int CountTabs(string line)
        {
            int count = 0;
            foreach(char c in line)
            {
                if (char.IsLetterOrDigit(c))
                    break;
                if (c == '\t')
                    count++;
            }
            return count;
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

        private string GetItemName(string line)
        {
            string[] lineArray = line.Split(' ');
            
            if(lineArray.Length > 0)
            {
                return lineArray[0].Trim();
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

        public bool WriteFile(string path, List<DrawableItem> items)
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

        private string SerializeItems(List<DrawableItem> items)
        {
            string serialized = "";

            Group group = new Group(items);
            serialized = group.Serialize(string.Empty);
                
            return serialized;
        }

        #endregion

    }
}
