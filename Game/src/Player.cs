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


namespace Keikaku
{
    public class Player : Entity
    {
        Transform transform;
        SpriteComponent sprite;
        TextureSheet sheet;
        AnimationComponent<PlayerState> animation;

        Rectangle[] terrain;
        Rectangle spriteBounds;
        Rectangle collisionBounds;
        Rectangle floorBounds;
        Point colliderDisplacement;

        Tilemap map;

        Song narutoSong;

        Texture2D groundTexture;
        bool onGround = true;

        SpriteEffects effect;

        enum PlayerState
        {
            STANDING,
            WALKING,
            JUMPING,
            FALLING
        }

        PlayerState currentState;

        public Player(Tilemap map)
        {
            this.map = map;
            transform = new Transform();
            sprite = new SpriteComponent();
            currentState = PlayerState.STANDING;
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
            MediaPlayer.Volume = .1f;
            //MediaPlayer.Play(narutoSong);
            
            animation = new AnimationComponent<PlayerState>();

            animation.Animations.Add(PlayerState.STANDING, new Animation(0, 4, 0.85f));
            animation.Animations.Add(PlayerState.WALKING, new Animation(8, 6, 0.75f));
            animation.Animations.Add(PlayerState.JUMPING, new Animation(14, 9, MAX_JUMP/25f * 1/60f, false));
            animation.Animations.Add(PlayerState.FALLING, new Animation(22, 2, 0.2f));

            animation.SetAnimation(PlayerState.STANDING);

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


        const int MAX_SPEED = 500;

        Vector2 speed;
        const int ACCELERATION_SPEED = 250;
        const int JUMP_SPEED = 50;

        


        private void UpdateMovement(GameTime gameTime)
        {
            
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            //Console.WriteLine(1.0f / dt);


            switch (currentState)
            {
                case PlayerState.STANDING:
                    {
                        speed = Vector2.Zero;
                        if (animation.currentState != currentState)
                            animation.SetAnimation(currentState);
                        if (InputManager.IsKeyDown(Keys.A) != InputManager.IsKeyDown(Keys.D))
                        {
                            currentState = PlayerState.WALKING;
                            break;
                        }
                        if(InputManager.IsKeyDown(Keys.Space))
                        {
                            speed.Y -= MAX_JUMP;
                            currentState = PlayerState.JUMPING;
                            break;
                        }
                        if (!onGround)
                        {
                            currentState = PlayerState.JUMPING;
                            break;
                        }

                        break;
                    }
                case PlayerState.WALKING:
                    {

                        if (animation.currentState != currentState)
                            animation.SetAnimation(currentState);
                        if (InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D))
                        {
                            currentState = PlayerState.STANDING;
                            speed.X = 0;
                            break;
                        }
                        else if (InputManager.IsKeyDown(Keys.A))
                        {
                            speed.X = -ACCELERATION_SPEED;
                        }
                        else if (InputManager.IsKeyDown(Keys.D))
                        {
                            speed.X = ACCELERATION_SPEED;
                        }

                        if (InputManager.IsKeyDown(Keys.Space))
                        {
                            speed.Y -= MAX_JUMP;
                            currentState = PlayerState.JUMPING;
                            break;
                        }
                        else if(!onGround)
                        {
                            currentState = PlayerState.JUMPING;
                            break;
                        }

                        break;
                    }
                case PlayerState.JUMPING:
                    {
                        if(animation.currentState != currentState)
                            animation.SetAnimation(currentState);
                        if (InputManager.IsKeyDown(Keys.Space))
                        {
                            speed.Y += 15f;
                        }
                        else 
                            speed.Y += 25f;// * dt;

                        //Console.WriteLine(speed.Y);
                        
                        //speed.Y = Math.Min(Math.Abs(speed.Y), 300);

                        if(speed.Y > 0)
                        {
                            currentState = PlayerState.FALLING;
                            break;
                        }
                        

                        if(InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D))
                        {
                            speed.X = 0;
                        }
                        else if(InputManager.IsKeyDown(Keys.A))
                        {
                            speed.X = -ACCELERATION_SPEED;
                        }
                        else if(InputManager.IsKeyDown(Keys.D))
                        {
                            speed.X = ACCELERATION_SPEED;
                        }
                        else
                        {
                            speed.X = 0;
                        }

                        if(onGround)
                        {
                            if(InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D))
                            {
                                currentState = PlayerState.STANDING;
                                speed = Vector2.Zero;
                            }
                            else
                            {
                                currentState = PlayerState.WALKING;
                                speed.Y = 0;
                            }
                        }

                        break;
                    }
                case PlayerState.FALLING:
                    {
                        if (animation.currentState != currentState)
                            animation.SetAnimation(currentState);
                        speed.Y += 25f;// * dt;

                        if (!InputManager.IsKeyDown(Keys.Space) && speed.Y > 0.0f)
                        {
                            speed.Y = Math.Min(speed.Y, 1200);
                        }

                        if (InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D))
                        {
                            speed.X = 0;
                        }
                        else if (InputManager.IsKeyDown(Keys.A))
                        {
                            speed.X = -ACCELERATION_SPEED;
                        }
                        else if (InputManager.IsKeyDown(Keys.D))
                        {
                            speed.X = ACCELERATION_SPEED;
                        }
                        else
                        {
                            speed.X = 0;
                        }

                        if (onGround)
                        {
                            if (InputManager.IsKeyDown(Keys.A) == InputManager.IsKeyDown(Keys.D))
                            {
                                currentState = PlayerState.STANDING;
                                speed = Vector2.Zero;
                            }
                            else
                            {
                                currentState = PlayerState.WALKING;
                                speed.Y = 0;
                            }
                        }

                        break;
                    }
            }
            //Console.WriteLine(currentState);

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

            //oldPosition = transform.Position;
            //oldSpeed = speed;
        }

        private void CollisionDetection(float dt)
        {
            collisionBounds = new Rectangle((int)(transform.Position.X + speed.X * dt), (int)(transform.Position.Y + speed.Y * dt), collisionBounds.Width, collisionBounds.Height);

            Tile yTile = map.GetTile((int)transform.Position.X, collisionBounds.Bottom);
            if(yTile != null && yTile.Data != 0)
            {
                Console.WriteLine(yTile.X + "-" + yTile.Y);
                speed.Y = 0;
                transform.Position.Y = yTile.Y - collisionBounds.Height;
                onGround = true;
            }
            else
            {
                onGround = false;
                Tile y2Tile = map.GetTile((int)transform.Position.X, collisionBounds.Top);
                if(y2Tile != null && y2Tile.Data != 0)
                {
                    speed.Y *= -.15f;
                    transform.Position.Y = y2Tile.Y + map.TileHeight;
                }
            }

            Tile xTile = map.GetTile(collisionBounds.Left, (int)transform.Position.Y);
            if (xTile != null && xTile.Data != 0)
            {
                speed.X = 0;
                transform.Position.X = xTile.X + map.TileWidth;
            }
            else 
            {
                Tile x2Tile = map.GetTile(collisionBounds.Right, (int)transform.Position.Y);
                if (x2Tile != null && x2Tile.Data != 0)
                {
                    speed.X = 0;
                    transform.Position.X = x2Tile.X - collisionBounds.Width;
                }
            }
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateMovement(gameTime);
            animation.Update(dt);
            spriteBounds = sheet.getTextureRect(animation.currentAnimation.CurrentFrame);

        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, transform.Position, spriteBounds,
                sprite.Color, transform.Rotation, new Vector2(colliderDisplacement.X, colliderDisplacement.Y), transform.Scale,
                effect, 0.0f);

            //Game1.DrawBorder(spriteBatch, collisionBounds, 1, Color.Red);
            //Game1.DrawBorder(spriteBatch, floorBounds, 1, Color.Green);

        }
    }
}
