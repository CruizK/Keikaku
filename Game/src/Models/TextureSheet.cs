using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku.Models
{
    public class TextureSheet
    {
        List<Rectangle> rectangles;

        public TextureSheet(Texture2D texture, int textureWidth, int textureHeight)
        {
            rectangles = new List<Rectangle>();

            int cols = texture.Width / textureWidth;
            int rows= texture.Height / textureHeight;

            for(int y = 0; y < rows; y++)
            {
                for(int x = 0; x < cols; x++)
                {
                    rectangles.Add(new Rectangle(x * textureWidth, y * textureHeight, textureWidth, textureHeight));
                }
            }
        }

        public Rectangle getTextureRect(int index)
        {
            return rectangles[index];
        }
    }
}
