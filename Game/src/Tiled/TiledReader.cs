
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Keikaku.Tiled
{
    public class TiledReader : ContentTypeReader<Tilemap>
    {
        protected override Tilemap Read(ContentReader input, Tilemap existingInstance)
        {
            Tilemap tilemap = new Tilemap();
            tilemap.Width = input.ReadInt32();
            tilemap.Height = input.ReadInt32();
            tilemap.TileWidth = input.ReadInt32();
            tilemap.TileHeight = input.ReadInt32();

            int tilesetCount = input.ReadInt32();

            for(int i = 0; i < tilesetCount; i++)
            {
                Tileset tileset = new Tileset();
                string filename = input.ReadString();
                tileset.Texture = input.ContentManager.Load<Texture2D>(filename);
                tileset.TileWidth = input.ReadInt32();
                tileset.TileHeight = input.ReadInt32();
                tileset.TileCount = input.ReadInt32();
                tileset.GIDOffset = input.ReadInt32();

                tileset.GenerateRects();

                tilemap.AddTileset(tileset);
            }

            int tileLayerCount = input.ReadInt32();

            for(int i = 0; i < tileLayerCount; i++)
            {
                TileLayer layer = new TileLayer();

                layer.ID = input.ReadInt32();
                layer.Width = input.ReadInt32();
                layer.Height = input.ReadInt32();
                layer.Name = input.ReadString();

                layer.Tiles = new Tile[layer.Width * layer.Height];

                int pairCount = input.ReadInt32();


                int tileIndex = 0;
                for(int j = 0; j < pairCount; j++)
                {
                    int count = input.ReadInt32();
                    int id = input.ReadInt32();
                    for(int k = 0; k < count; k++)
                    {
                        int y = tileIndex / layer.Width;
                        int x = (tileIndex - (layer.Width * y));

                        layer.Tiles[tileIndex] = new Tile(x * tilemap.TileWidth, y * tilemap.TileHeight, id);
                        tileIndex++;
                    }
                }

                tilemap.AddTileLayer(layer);
            }

            return tilemap;
        }
    }
}
