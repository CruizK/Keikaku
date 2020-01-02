using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Keikaku.Tiled
{
    public class TileLayer
    {
        public int ID;
        public string Name;
        public int Width;
        public int Height;
        public Tile[] Tiles;

        public TileLayer()
        {

        }
    }
}
