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
    public class Panel : UIElement
    {
        public string Title;
        public Color TitleColor;
        public Color PanelColor;

        bool isClicked = false;

        Label titleLabel;

        public Panel()
        {

        }

        public override void Init()
        {
            titleLabel = new Label();
            titleLabel.Text = Title;
            titleLabel.FontPath = "font";
            titleLabel.Name = "Panel Title";

            AddChild(titleLabel);
            base.Init();

        }

        public override void LoadContent(ContentManager content)
        {
            base.LoadContent(content);
        }

        public override void Update(GameTime gameTime)
        {
            Point pos = InputManager.GetMousePos();
            Rectangle bounds = new Rectangle(Position.ToPoint(), new Point((int)Size.X, 20));

            if (bounds.Contains(pos))
            {
                if(InputManager.IsLeftDown())
                {
                    isClicked = true;
                }
            }

            if (!InputManager.IsLeftDown())
                isClicked = false;

            if(isClicked)
            {
                Vector2 delta = InputManager.GetMouseDelta().ToVector2();
                if(delta != Vector2.Zero)
                    SetPosition(Position + delta);
            }

            base.Update(gameTime);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            Rectangle rect = spriteBatch.GraphicsDevice.ScissorRectangle;


            //spriteBatch.GraphicsDevice.ScissorRectangle = new Rectangle(GetLocalPos().ToPoint(), Size.ToPoint());

            // Draw Entire Panel
            spriteBatch.Draw(Game1.pixel, new Rectangle(GetLocalPos().ToPoint(), Size.ToPoint()), PanelColor);

            // Draw The Title
            spriteBatch.Draw(Game1.pixel, new Rectangle(GetLocalPos().ToPoint(), new Point((int)Size.X, 20)), TitleColor);

            base.Draw(spriteBatch);
        }
    }
}
