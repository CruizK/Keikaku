using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku
{
    public class TextureAtlas
    {
        Rectangle[] rectangles;
        public int TextureCount { get; private set; }

        public TextureAtlas()
        {

        }
        public TextureAtlas(Texture2D texture, int perTextureWidth, int perTextureHeight)
        {
            Load(texture, perTextureWidth, perTextureHeight);
        }

        public void Load(Texture2D texture, int perTextureWidth, int perTextureHeight)
        {

            int cols = (int)(texture.Width / perTextureWidth);
            int rows = (int)(texture.Height / perTextureHeight);

            rectangles = new Rectangle[cols * rows];
            TextureCount = rectangles.Length;

            // Make sure the texture is evenly divisable by the per Texture given
            //Debug.Assert((texture.Width % perTextureWidth == 0) && (texture.Height % perTextureHeight == 0));

            for(int i = 0; i < rows; i ++)
            {
                for(int j = 0; j < cols; j++)
                {
                    rectangles[j + (cols * i)] = new Rectangle(j * perTextureWidth, i * perTextureHeight, perTextureWidth, perTextureHeight);
                }
            }
        }

        public Rectangle GetSprite(int index)
        {
            return rectangles[index];
        }
    }
}
