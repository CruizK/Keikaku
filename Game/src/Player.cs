using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

using Keikaku.Models;

namespace Keikaku
{
    public class Player
    {
        
        public Vector2 Position { get; private set; }
        public Rectangle Bounds { get; private set; }

        private Texture2D sprite;
        private Rectangle currAnimationFrame;
        private Animation idleAnimation;
        private TextureAtlas atlas;


        public Player()
        {

        }

        public void LoadContent(ContentManager content)
        {
            sprite = content.Load<Texture2D>("sprite_sheet");
            atlas = new TextureAtlas(sprite, 50, 37);

            idleAnimation = new Animation(atlas, 0, 3, 0.5f);
            
        }

        public void Update(GameTime gameTime)
        {
            KeyboardState state = Keyboard.GetState();
            Vector2 vel = Vector2.Zero;

            if(state.IsKeyDown(Keys.A))
            {
                vel.X--;    
            }
            if(state.IsKeyDown(Keys.D))
            {
                vel.X++;
            }
            
           


            Position += vel * 200 * (float)gameTime.ElapsedGameTime.TotalSeconds;

            currAnimationFrame = idleAnimation.playAnimation(gameTime);
            Bounds = new Rectangle((int)Position.X + 8, (int)Position.Y, 32, 38);
        }

        public void ApplyGravity(GameTime gameTime)
        {
            Position = new Vector2(Position.X, Position.Y + 9.8f * (float)gameTime.ElapsedGameTime.TotalSeconds);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite, Position, currAnimationFrame, Color.White);
        }
    }
}
