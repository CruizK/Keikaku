using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku.UI
{
    public class ScrollingContainer : UIElement
    {


        private Rectangle clippingRect;

        public override void Init()
        {
            base.Init();
            clippingRect = new Rectangle(Position.ToPoint(), Size.ToPoint());
        }


        public override void Draw(SpriteBatch spriteBatch)
        {
            Rectangle tmp = spriteBatch.GraphicsDevice.ScissorRectangle;
            spriteBatch.GraphicsDevice.ScissorRectangle = clippingRect;

            base.Draw(spriteBatch);

            spriteBatch.GraphicsDevice.ScissorRectangle = tmp;
        }
    }
}
