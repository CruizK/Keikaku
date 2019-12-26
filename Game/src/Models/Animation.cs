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
        private TextureAtlas textureAtlas;
        
        private int currFrame = 0;
        private int startFrame = 0;
        private int endFrame = 0;
        private float durationPerFrame = 0.0f;
        private float currTime = 0.0f;
        public Animation(TextureAtlas atlas, int startFrame, int endFrame, float duration) 
        {
            this.textureAtlas = atlas;
            this.startFrame = startFrame;         
            this.endFrame = endFrame;
            currFrame = startFrame;
            durationPerFrame = duration / (endFrame - startFrame);
        }

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
    }
}
