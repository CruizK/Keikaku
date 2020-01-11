using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Keikaku.Tiled
{
    public class Tile
    {
        public int X { get; private set; }
        public int Y { get; private set; }
        public int Data { get; private set; }

        public Color color = Color.White;
        public Tile(int x, int y, int data)
        {
            X = x;
            Y = y;
            Data = data;
        }
    }
}
