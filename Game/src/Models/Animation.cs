using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace Keikaku.Models
{
    public class Animation
    {
        public int StartFrame = 0;
        public int EndFrame = 0;
        public float Duration = 0;
        public float DurationPerFrame = 0;
        public int CurrentFrame = 0;
        public bool ShouldLoop = true;

        public Animation(int startFrame, int frameLength, float duration, bool shouldLoop=true)
        {
            
            StartFrame = startFrame;
            CurrentFrame = StartFrame;
            EndFrame = startFrame + frameLength-1;
            Duration = duration;
            ShouldLoop = shouldLoop;

            DurationPerFrame = Duration / (frameLength);
        }

        /*

        public Rectangle playAnimation(GameTime gameTime)
        {
            currTime += (float)gameTime.ElapsedGameTime.TotalSeconds;
            if(durationPerFrame - currTime < 0)
            {
                currFrame++;
                if (currFrame > endFrame) currFrame = startFrame;
                currTime = 0;
            }

            return textureAtlas.GetSprite(currFrame);
        }
        */
    }
}
