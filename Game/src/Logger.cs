using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;

namespace Keikaku
{
    public class Logger
    {

        static float currTime;

        public static void Update(GameTime gameTime)
        {
            currTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
        }

        public static void LogOnInterval(int time, string message)
        {
            if (currTime - time > 0)
            {
                Console.WriteLine(message);
                currTime = 0;
            }
                
        }
    }
}
