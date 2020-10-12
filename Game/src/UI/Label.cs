using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;


namespace Keikaku.UI
{
    public class Label : UIElement
    {
        private SpriteFont font;
        public string FontPath = "";
        public string Text = "";
        public Color FontColor = Color.White;


        public override void LoadContent(ContentManager content)
        {
            font = content.Load<SpriteFont>(FontPath);
            Size = font.MeasureString(Text);

            base.LoadContent(content);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.DrawString(font, Text, GetWorldCoords(), FontColor);

            base.Draw(spriteBatch);
        }
    }
}
