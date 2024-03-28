using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using System.Text;
using System.Threading.Tasks;

namespace try4
{
    public class Lokasyon
    {
        public int X { get; private set; }
        public int Y { get; private set; }

        public Lokasyon(int x, int y)
        {
            X = x;
            Y = y;
        }
    }
}
