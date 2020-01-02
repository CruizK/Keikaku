using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Content.Pipeline;

namespace TiledPipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to apply custom processing to content data, converting an object of
    /// type TInput to TOutput. The input and output types may be the same if
    /// the processor wishes to alter data without changing its type.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentProcessor attribute to specify the correct
    /// display name for this processor.
    /// </summary>
    [ContentProcessor(DisplayName = "Tiled Processor - Keikaku")]
    public class TiledProcessor : ContentProcessor<TileMapFile, TileDataFile>
    {
        public override TileDataFile Process(TileMapFile input, ContentProcessorContext context)
        {
            // TODO: process the input object, and return the modified data.

            TileDataFile dataFile = new TileDataFile();

            context.Logger.LogMessage("Processing tile data file");

            //context.Logger.LogMessage("datafile layer length {0}", input.TileLayers[0].Tiles.Length);
            

            foreach(TileLayer layer in input.TileLayers)
            {
                //context.Logger.LogMessage("{0}", layer.Tiles[layer.Tiles.Length - 1100]);
                int lastID = -1;
                int tileCounter = 0;
                dataFile.tileLayerDataPairing.Add(layer.ID, new List<Tuple<int, int>>());
                for(int i = 0; i < layer.Tiles.Length; i++)
                {
                    if (lastID == -1)
                        lastID = layer.Tiles[i];
                    if (lastID != layer.Tiles[i]) // Different than the last tile
                    {
                        dataFile.tileLayerDataPairing[layer.ID].Add(Tuple.Create(tileCounter, lastID));
                        lastID = layer.Tiles[i];
                        tileCounter = 1;
                    }
                    else
                    {
                        tileCounter++;
                    }
                }
                dataFile.tileLayerDataPairing[layer.ID].Add(Tuple.Create(tileCounter, lastID));
            }



            dataFile.file = input;

            return dataFile;
        }
    }
}