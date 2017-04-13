using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ThePainterFormsTest.Models;

namespace ThePainterFormsTest.States
{
    /// <summary>
    /// Ornament Bottom State
    /// </summary>
    public class OrnamentBottom : IOrnamentState
    {
        private static OrnamentBottom _instance;
        public static OrnamentBottom Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new OrnamentBottom();
                }

                return _instance;
            }
        }

        private OrnamentBottom() { }

        public string State
        {
            get
            {
                return "bottom";
            }
        }

        /// <summary>
        /// Gets drawing position
        /// </summary>
        /// <param name="ornament">ornament</param>
        /// <returns>position</returns>
        public PointF GetPosition(Ornament ornament)
        {
            int x = ornament.Width / 2 + ornament.X - 30;
            int y = ornament.Y + ornament.Height + 20;
            return new PointF(x, y);
        }

        public override string ToString()
        {
            return State;
        }
    }
}
