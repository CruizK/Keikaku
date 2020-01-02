using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TiledPipelineExtension
{
    public class TileDataFile
    {
        public Dictionary<int, List<Tuple<int, int>>> tileLayerDataPairing = new Dictionary<int, List<Tuple<int, int>>>();
        public TileMapFile file;

    }
}
