using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Keikaku.Models;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Keikaku.Tiled
{
    public class Tilemap
    {
        public int Width, Height, TileWidth, TileHeight;


        List<Tileset> tilesets;
        List<TileLayer> tileLayers;

        public Tilemap()
        {
            tilesets = new List<Tileset>();
            tileLayers = new List<TileLayer>();
        }

        private Tileset GetTileset(int id)
        {
            foreach(Tileset tileset in tilesets)
            {
                if(id >= tileset.GIDOffset && id <= tileset.TileCount)
                {
                    return tileset;
                }
            }

            return null;
        }

        public Tile GetTile(int x, int y)
        {
            int tileX = -1;
            int tileY = -1;

            if (x < 0 || x > Width * TileWidth || y < 0 || y > Height * TileHeight)
                return null;

            if (x == 0)
                tileX = 0;
            if (y == 0)
                tileY = 0;

            if (tileX == -1)
                tileX = x / TileWidth;
            if (tileY == -1)
                tileY = y / TileHeight;

            return tileLayers.First().Tiles[tileY * Height + tileX];
        }

        public Tile GetTile(Point pos)
        {
            return GetTile(pos.X, pos.Y);
        }

        public void DrawLayers(SpriteBatch spriteBatch)
        {
            foreach(TileLayer layer in tileLayers)
            {
                for(int i = 0; i < layer.Tiles.Length; i++)
                {
                    // If tile is not a blank sprite, then draw it
                    if(layer.Tiles[i].Data != 0)
                    {
                        Tile tile = layer.Tiles[i];
                        int tileData = tile.Data;
                        Tileset tileset = GetTileset(tileData);

                        spriteBatch.Draw(tileset.Texture, new Rectangle(tile.X, tile.Y, TileWidth, TileHeight), tileset.GetTile(tileData), Color.White);
                    }
                }
            }
        }

        public void AddTileset(Tileset tileset)
        {
            tilesets.Add(tileset);
        }

        public void AddTileLayer(TileLayer layer)
        {
            tileLayers.Add(layer);
        }
    }
}
