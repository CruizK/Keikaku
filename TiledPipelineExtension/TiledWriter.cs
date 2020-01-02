using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Content.Pipeline;
using Microsoft.Xna.Framework.Content.Pipeline.Serialization.Compiler;

namespace TiledPipelineExtension
{
    [ContentTypeWriter]
    public class TiledWriter : ContentTypeWriter<TileDataFile>
    {

        protected override void Write(ContentWriter output, TileDataFile value)
        {
            output.Write(value.file.Width);
            output.Write(value.file.Height);
            output.Write(value.file.TileWidth);
            output.Write(value.file.TileHeight);

            output.Write(value.file.Tilesets.Count);
            foreach(TileSet tileset in value.file.Tilesets)
            {
                output.Write(tileset.ImagePath);
                output.Write(tileset.TileWidth);
                output.Write(tileset.TileHeight);
                output.Write(tileset.TileCount);
                output.Write(tileset.GIDOffset);

            }

            output.Write(value.file.TileLayers.Count);
            foreach(TileLayer tileLayer in value.file.TileLayers)
            {
                output.Write(tileLayer.ID);
                output.Write(tileLayer.Width);
                output.Write(tileLayer.Height);
                output.Write(tileLayer.Name);

                output.Write(value.tileLayerDataPairing[tileLayer.ID].Count);
                foreach(Tuple<int,int> pair in value.tileLayerDataPairing[tileLayer.ID])
                {
                    output.Write(pair.Item1);
                    output.Write(pair.Item2);
                }
            }
        }

        public override string GetRuntimeType(TargetPlatform targetPlatform)
        {
            return "Keikaku.Tiled.Tilemap, Game";
        }

        public override string GetRuntimeReader(TargetPlatform targetPlatform)
        {
            return "Keikaku.Tiled.TiledReader, Game";
        }
    }
}
