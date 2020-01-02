using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using Microsoft.Xna.Framework.Content.Pipeline;


namespace TiledPipelineExtension
{
    /// <summary>
    /// This class will be instantiated by the XNA Framework Content Pipeline
    /// to import a file from disk into the specified type, TImport.
    ///
    /// This should be part of a Content Pipeline Extension Library project.
    ///
    /// TODO: change the ContentImporter attribute to specify the correct file
    /// extension, display name, and default processor for this importer.
    /// </summary>

    [ContentImporter(".tmx", DisplayName = "Tiled Importer", DefaultProcessor = "TiledProcessor")]
    public class TiledImporter : ContentImporter<TileMapFile>
    {

        public override TileMapFile Import(string filename, ContentImporterContext context)
        {
            context.Logger.LogMessage("Importing Tiled map file : {0}", filename);
            // TODO: process the input object, and return the modified data.
            TileMapFile file = new TileMapFile();

            XmlDocument doc = new XmlDocument();
            doc.Load(filename);

            XmlElement mapElement = doc.DocumentElement;

            file.Width = int.Parse(mapElement.GetAttribute("width"));
            file.Height = int.Parse(mapElement.GetAttribute("height"));
            file.TileWidth = int.Parse(mapElement.GetAttribute("tilewidth"));
            file.TileHeight = int.Parse(mapElement.GetAttribute("tileheight"));



            {
                XmlNodeList tilesets = mapElement.GetElementsByTagName("tileset");

                foreach(XmlNode tilesetNode in tilesets)
                {
                    XmlDocument tileDoc = new XmlDocument();
                    TileSet tileset = new TileSet();
                    tileset.GIDOffset = int.Parse(tilesetNode.Attributes["firstgid"].Value);
                    
                    tileDoc.Load(tilesetNode.Attributes["source"].Value);
                    
                    context.Logger.LogMessage("Adding Tileset");

                    XmlElement tileSheetElement = tileDoc.DocumentElement;

                    tileset.TileWidth = int.Parse(tileSheetElement.GetAttribute("tilewidth"));
                    tileset.TileHeight = int.Parse(tileSheetElement.GetAttribute("tileheight"));
                    tileset.TileCount = int.Parse(tileSheetElement.GetAttribute("tilecount"));
                    

                    XmlNode imageNode = tileSheetElement.FirstChild;

                    tileset.ImagePath = Path.GetFileNameWithoutExtension(imageNode.Attributes["source"].Value);

                    file.Tilesets.Add(tileset);
                }
            }

            {
                XmlNodeList layerList = mapElement.GetElementsByTagName("layer");

                foreach(XmlNode layer in layerList)
                {
                    context.Logger.LogMessage("Adding Tile Layer");
                    TileLayer tileLayer = new TileLayer();
                    tileLayer.ID = int.Parse(layer.Attributes["id"].Value);
                    tileLayer.Width = int.Parse(layer.Attributes["width"].Value);
                    tileLayer.Height = int.Parse(layer.Attributes["height"].Value);
                    tileLayer.Name = layer.Attributes["name"].Value;
                    tileLayer.Tiles = new int[tileLayer.Width * tileLayer.Height];


                    XmlNode dataContainer = layer.FirstChild;
                    context.Logger.LogMessage("data size: {0}", dataContainer.ChildNodes.Count);
                    int i = 0;
                    foreach(XmlNode tile in dataContainer.ChildNodes)
                    {
                        if(tile.Attributes != null && tile.Attributes["gid"] != null)
                        {
                            tileLayer.Tiles[i] = int.Parse(tile.Attributes["gid"].Value); // -1 cuz it starts at 1
                        }
                        else
                        {
                            tileLayer.Tiles[i] = 0;
                        }

                        i++;
                    }

                    context.Logger.LogMessage("test");

                    file.TileLayers.Add(tileLayer);
                }
            }
            

            return file;
        }

    }

}
