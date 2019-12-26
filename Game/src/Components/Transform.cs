using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;

namespace Keikaku.Components
{
    
    public class Transform : Component
    {
        public Vector2 Position = Vector2.Zero;
        public float Rotation = 0.0f;
        public float Scale = 1.0f;
    }
}
