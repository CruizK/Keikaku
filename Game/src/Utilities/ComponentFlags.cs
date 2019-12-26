using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Keikaku.Utilities
{
    public class ComponentFlags
    {
        const uint bitSize = (sizeof(uint) * 8 )- 1;

        uint[] flags;

        public ComponentFlags()
        {
            // 16 uints, 32 flags in each uint
            flags = new uint[1];
        }
    }
}
