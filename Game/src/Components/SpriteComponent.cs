using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku.Components
{
    
    public class SpriteComponent : Component
    {
        // Should load in default texture
        public Texture2D Texture = null;
        public string TexturePath = "";
        public Color Color = Color.White;
        public bool IsVisible = true;
    }
}
