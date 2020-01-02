using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Keikaku.Tiled
{
    public class Tileset
    {
        public int TileWidth;
        public int TileHeight;
        public int TileCount;
        public int GIDOffset;
        public Texture2D Texture;

        private List<Rectangle> tileRects = new List<Rectangle>();

        public Tileset()
        {

        }

        public void GenerateRects()
        {
            int rows = Texture.Height / TileWidth;
            int cols = Texture.Width / TileHeight;

            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < cols; x++)
                {
                    tileRects.Add(new Rectangle(x * TileWidth, y * TileHeight, TileWidth, TileHeight));
                }
            }
        }

        public Rectangle GetTile(int index)
        {
            return tileRects[index-1];
        }
    }
}
