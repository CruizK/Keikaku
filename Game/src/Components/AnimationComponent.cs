using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

using Keikaku.Models;

namespace Keikaku.Components
{
    public class AnimationComponent : Component
    {
        public Dictionary<string, Animation> animations;
        public Animation currentAnimation;
    }
}
