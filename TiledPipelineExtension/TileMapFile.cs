using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPipelineExtension
{
    public class TileMapFile
    {
        public int Width;
        public int Height;
        public int TileWidth;
        public int TileHeight;

        public List<TileSet> Tilesets = new List<TileSet>();
        public List<TileLayer> TileLayers = new List<TileLayer>();
    }

    public class TileSet
    {
        public int TileWidth;
        public int TileHeight;
        public int TileCount;
        public int GIDOffset;
        public string ImagePath;
    }

    public class TileLayer
    {
        public int ID;
        public string Name;
        public int Width;
        public int Height;
        public int[] Tiles;
    }

}
