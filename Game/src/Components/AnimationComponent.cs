using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Keikaku.Models;

namespace Keikaku.Components
{
    public class AnimationComponent<T> : Component
    {
        public Dictionary<T, Animation> Animations = new Dictionary<T, Animation>();
        public Animation currentAnimation { get; private set; }
        public T currentState;

        private float timeElapsed = 0f;

        public AnimationComponent()
        {
            currentAnimation = null;
        }

        public void Update(float dt)
        {
            if(currentAnimation != null)
            {
                timeElapsed += dt;
                if (timeElapsed >= currentAnimation.DurationPerFrame)
                {
                    currentAnimation.CurrentFrame++;
                    timeElapsed = 0;
                }

                if (currentAnimation.CurrentFrame > currentAnimation.EndFrame)
                {
                    if(currentAnimation.ShouldLoop)
                    {
                        currentAnimation.CurrentFrame = currentAnimation.StartFrame;
                    }
                    else
                    {
                        currentAnimation.CurrentFrame = currentAnimation.EndFrame;
                    }
                    
                } 
            }
        }

        public void SetAnimation(T key)
        {
            if(currentAnimation != null)
            {
                currentAnimation.CurrentFrame = currentAnimation.StartFrame;
                timeElapsed = 0;
            }
            currentAnimation = Animations[key];
            currentState = key;
            
        }

    }
}
