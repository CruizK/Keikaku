using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keikaku.Components;
using Keikaku.Tiled;
using Keikaku.Models;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Media;


namespace Keikaku.Character
{
    public class Player : Entity
    {
        public Transform transform;
        public SpriteComponent sprite;
        public TextureSheet sheet;
        public AnimationComponent<string> animation;

        Rectangle[] terrain;
        Rectangle spriteBounds;
        Rectangle collisionBounds;
        Rectangle floorBounds;
        Point colliderDisplacement;

        Tilemap map;

        Song narutoSong;

        Texture2D groundTexture;
        public bool onGround = false;
        public bool collidingLeft = false;
        public bool collidingRight = false;
        public bool collidingX = false;

        SpriteEffects effect;

        Stack<IPlayerState> stateStack = new Stack<IPlayerState>();

        public Player(Tilemap map)
        {
            this.map = map;
            transform = new Transform();
            sprite = new SpriteComponent();
        }

        public Vector2 GetPosition() { return transform.Position; }

        public Vector2 GetOrigin()
        {
            return new Vector2(transform.Position.X + spriteBounds.Width/2-10, transform.Position.Y + spriteBounds.Height/2);
        }

        const int MAX_JUMP = 500;

        public void LoadContent(ContentManager Content, GraphicsDevice device)
        {
            sprite.Texture = Content.Load<Texture2D>("sprite_sheet");
            sheet = new TextureSheet(sprite.Texture, 50, 37);
           

            // COPY RIGHT INFRINGEMENT
            narutoSong = Content.Load<Song>("naruto_run");
            MediaPlayer.Volume = 20f/100f;
            MediaPlayer.Play(narutoSong);
            MediaPlayer.IsRepeating = true;

            stateStack.Push(IPlayerState.OnGroundState);

            animation = new AnimationComponent<string>();

            animation.Animations.Add("standing", new Animation(0, 4, 0.85f));
            animation.Animations.Add("walking", new Animation(8, 6, 0.75f));
            animation.Animations.Add("jumping", new Animation(14, 9, MAX_JUMP/25f * 1/60f, false));
            animation.Animations.Add("falling", new Animation(22, 2, 0.2f));

            animation.SetAnimation("standing");

            spriteBounds = sheet.getTextureRect(animation.currentAnimation.CurrentFrame);

            colliderDisplacement = new Point(20, 8);

            transform.Position = new Vector2(161, 2835);

            effect = SpriteEffects.None;
            transform.Scale = 1.5f;
            collisionBounds = new Rectangle((int)transform.Position.X, (int)transform.Position.Y, (int)((spriteBounds.Width - 40) * transform.Scale), (int)((spriteBounds.Height-colliderDisplacement.Y) * transform.Scale));
            floorBounds = collisionBounds;
            floorBounds.Height += 10;
            onGround = false;
        }


        const int MAX_SPEED = 300;

        public Vector2 speed;
        public int acceleration = 50;
        const int JUMP_SPEED = 50;


        public void PopState()
        {
            stateStack.Pop();
        }

        public void PushState(IPlayerState state)
        {
            stateStack.Push(state);
        }

        public void ChangeState(IPlayerState state)
        {
            while (stateStack.Count > 0)
                stateStack.Pop();

            PushState(state);
        }

        public void ChangeAnimation(string animKey)
        {
            animation.SetAnimation(animKey);
        }

        private void UpdateMovement(GameTime gameTime, ref Player player)
        {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine(1.0f / dt);

            if (InputManager.IsKeyDown(Keys.A))
            {
                speed.X -= acceleration;
            }
            else if (InputManager.IsKeyDown(Keys.D))
            {
                speed.X += acceleration;
            }
            else
                speed.X = speed.X /(1 + 200);

            speed.X = MathHelper.Clamp(speed.X, -MAX_SPEED, MAX_SPEED);
            float absSpeed = Math.Abs(speed.X);
            if (absSpeed > 0 && absSpeed < 1)
                speed.X = 0;

            stateStack.Peek().Update(ref player);

            if (speed.X < 0)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            else if (speed.X > 0)
            {
                effect = SpriteEffects.None;
            }


            CollisionDetection(dt);

            transform.Position += speed * dt;


            collisionBounds = new Rectangle((int)transform.Position.X, (int)transform.Position.Y, collisionBounds.Width, collisionBounds.Height);
            //oldPosition = transform.Position;
            //oldSpeed = speed;
        }

        private void CollisionDetection(float dt)
        {
            collisionBounds = new Rectangle((int)(transform.Position.X + speed.X * dt), (int)(transform.Position.Y + speed.Y * dt), collisionBounds.Width, collisionBounds.Height);

            Tile xTile = map.GetTile(new Rectangle(collisionBounds.X, (int)transform.Position.Y, collisionBounds.Width, collisionBounds.Height-1));
            if (xTile != null)
            {
                speed.X = 0;

                if (xTile.X <= collisionBounds.Left)
                {
                    transform.Position.X = xTile.X + map.TileWidth;
                    collidingLeft = true;
                }
                else if(xTile.X >= collisionBounds.Right)
                {
                    collidingRight = true;
                    transform.Position.X = xTile.X - collisionBounds.Width;
                }
                Console.WriteLine(xTile.X + map.TileWidth + "-" + collisionBounds.Left);
            }
            else
            {
                collidingRight = false;
                collidingLeft = false;
            }



            Tile yTile = map.GetTile(new Rectangle((int)transform.Position.X, collisionBounds.Y, collisionBounds.Width-1, collisionBounds.Height));
            if(yTile != null)
            {
                //Console.WriteLine(yTile.X + "-" + yTile.Y);

                if(yTile.Y + map.TileHeight > collisionBounds.Bottom)
                {
                    transform.Position.Y = yTile.Y - collisionBounds.Height;
                    speed.Y = 0;
                    onGround = true;
                }
                else
                {
                    onGround = false;
                    transform.Position.Y = yTile.Y + map.TileHeight;
                    speed.Y = 0;
                }

            }
            else
            {
                onGround = false;
            }

            //Console.WriteLine

            collidingX = collidingLeft || collidingRight;
        }

        public void Update(GameTime gameTime, ref Player player)
        {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateMovement(gameTime, ref player);
            animation.Update(dt);
            spriteBounds = sheet.getTextureRect(animation.currentAnimation.CurrentFrame);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, transform.Position, spriteBounds,
                sprite.Color, transform.Rotation, new Vector2(colliderDisplacement.X, colliderDisplacement.Y), transform.Scale,
                effect, 0.0f);

            Game1.DrawBorder(spriteBatch, collisionBounds, 1, Color.Red);
            //Game1.DrawBorder(spriteBatch, floorBounds, 1, Color.Green);

        }
    }
}
