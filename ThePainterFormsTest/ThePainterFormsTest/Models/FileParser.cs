using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ThePainterFormsTest.Visitors;
using ThePainterFormsTest.Strategy;
using ThePainterFormsTest.States;

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

            Line = 1;

            string[] lines = File.ReadAllLines(path);

            return Parser(lines);
        }


        private List<DrawableItem> Parser(string[] lines)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            while(Line < lines.Length)
            {
                try
                {
                    items.AddRange(GetItems(lines));
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, "The Painter", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                Line++;
            }

            return items;
        }

        private List<DrawableItem> GetItems(string[] lines)
        {
            string line = lines[Line].Trim();
            string itemName = GetItemName(line);

            DrawableItem item = null;
            switch (itemName)
            {
                case GROUP:
                    item = GroupParser(lines, GetGroupItemCount(line));
                    break;
                case ORNAMENT:
                    return OrnamentParser(lines, line);
                case ELLIPSE:
                    item = EllipseParser(line);
                    break;
                case RECTANGLE:
                    item = RectangleParser(line);
                    break;
                default:
                    throw new Exception("Not Parsed");
            }

            return new List<DrawableItem>() { item };
        }

        private List<DrawableItem> OrnamentParser(string[] lines, string line)
        {
            List<string> newOrnaments = new List<string>();

            while(IsOrnament(line) && Line < lines.Length)
            {
                newOrnaments.Add(line);
                Line++;
                line = lines[Line].Trim();
            }

            List<DrawableItem> items = GetItems(lines);

            newOrnaments.Reverse();
                
            foreach(var l in newOrnaments)
            {
                items.Insert(0, GetOrnament(l, items.First()));
            }

            return items;
        }

        private Ornament GetOrnament(string line, DrawableItem child)
        {
            string[] splittedLine = line.Split(' ');
            if (child != null && splittedLine.Length == 3)
            {

                IOrnamentState state = GetOrnamentState(splittedLine[1]);

                return new Ornament(splittedLine[2].Trim('"'), child, state);
            }

            throw new Exception($"Line[{Line}]: Could not parse Ornament");
        }

        private IOrnamentState GetOrnamentState(string stateString)
        {
            IOrnamentState state;

            switch (stateString)
            {
                case "top":
                    state = OrnamentTop.Instance;
                    break;
                case "bottom":
                    state = OrnamentBottom.Instance;
                    break;
                case "left":
                    state = OrnamentLeft.Instance;
                    break;
                case "right":
                    state = OrnamentRight.Instance;
                    break;
                default:
                    throw new Exception($"Line[{Line}]: Could not parse Ornament");
            }

            return state;
        }

        private bool IsOrnament(string line)
        {
            return GetItemName(line) == ORNAMENT;
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

        private Group GroupParser(string[] lines, int count)
        {
            List<DrawableItem> items = new List<DrawableItem>();

            for (int i = 0; i < count;)
            {
                Line++;

                List<DrawableItem> newItems = GetItems(lines);
                items.AddRange(newItems);

                foreach(var item in newItems)
                {
                    if (!(item is Ornament)) //ornament does not count as figure!
                    {
                        i++;
                    }
                }
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

        private BasicFigure RectangleParser(string line)
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
                    return new BasicFigure(x, y, width, height, RectangleStrategyObject.Instance);
                }
            }

            throw new Exception($"Line[{Line}]: Could not parse Rectangle");
        }

        private BasicFigure EllipseParser(string line)
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
                    return new BasicFigure(x, y, width, height, EllipseStrategyObject.Instance);
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
            Group group = new Group(items);

            FileVisitor visitor = new FileVisitor();

            group.Accept(visitor);

            return visitor.StringBuilder.ToString();
        }

        #endregion

    }
}
