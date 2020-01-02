using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Keikaku.Components;
using Keikaku.Utils;
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

        public Player()
        {
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
            MediaPlayer.Play(narutoSong);
            
            animation = new AnimationComponent<PlayerState>();

            animation.Animations.Add(PlayerState.STANDING, new Animation(0, 4, 0.85f));
            animation.Animations.Add(PlayerState.WALKING, new Animation(8, 6, 0.75f));
            animation.Animations.Add(PlayerState.JUMPING, new Animation(14, 9, MAX_JUMP/25f * 1/60f, false));
            animation.Animations.Add(PlayerState.FALLING, new Animation(22, 2, 0.2f));

            animation.SetAnimation(PlayerState.STANDING);

            spriteBounds = sheet.getTextureRect(animation.currentAnimation.CurrentFrame);

            terrain = new[] {
                new Rectangle(-300, 450, 50, 100),
                new Rectangle(300, 450, 50, 100),
                new Rectangle(150, 400, 100, 50),
                new Rectangle(-5000, 500, 10000, 50)
            };
            groundTexture = new Texture2D(device, 1, 1);
            groundTexture.SetData(new[] { Color.Aqua });

            colliderDisplacement = new Point(20, 8);


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

            bool collidingGround = false;
            //Console.WriteLine(currentState);

            //Console.WriteLine(transform.Position);
            foreach (Rectangle rect in terrain)
            {
                if(collisionBounds.Intersects(rect))
                {
                    //Console.WriteLine("INTERSECTION");
                    //Collision below player
                    if(rect == terrain[2])
                    {
                        //Console.WriteLine((int)transform.Position.Y + spriteBounds.Height);
                        //Console.WriteLine(terrain[0].Y);
                        //Console.WriteLine(collisionBounds);
                    }

                    //Console.WriteLine((int)transform.Position.Y + spriteBounds.Height);



                    if ((int)transform.Position.X >= rect.Right)
                    {
                        speed.X = 0;
                        transform.Position.X = rect.Right;
                    }
                    else if((int)transform.Position.X + collisionBounds.Width <= rect.Left)
                    {
                        speed.X = 0;
                        transform.Position.X = rect.Left - collisionBounds.Width;
                    }

                    else if ((int)transform.Position.Y + spriteBounds.Height <= rect.Y)
                    {
                        if (rect == terrain[2])
                        {
                            Console.WriteLine((int)transform.Position.Y + spriteBounds.Height);
                            Console.WriteLine(collisionBounds.Bottom);
                        }

                        speed.Y = 0;
                        //Console.WriteLine("COLLISION ABOVE");
                        transform.Position.Y = rect.Top - collisionBounds.Height+1;
                        collidingGround = true;
                    }
                    else if ((int)transform.Position.Y >= rect.Bottom)
                    {
                        speed.Y *= -.15f;
                        //Console.WriteLine(currentState);
                        transform.Position.Y = rect.Bottom+1;
                    }
                  
                }
            }

            onGround = collidingGround;
            //Console.WriteLine(onGou);
            //onGround = collidingGround;
        }

        public void Update(GameTime gameTime)
        {
            float dt = (float)gameTime.ElapsedGameTime.TotalSeconds;
            UpdateMovement(gameTime);
            animation.Update(dt);
            spriteBounds = sheet.getTextureRect(animation.currentAnimation.CurrentFrame);

            /*Vector2 velocity = Vector2.Zero;
            Vector2 newPosition = transform.Position;

            if(InputManager.IsKeyDown(Keys.A))
            {
                acceleration.X -= 50;
                moving = true;
            }
            if(InputManager.IsKeyDown(Keys.D))
            {
                acceleration.X += 50;
                moving = true;
            }
            if(!InputManager.IsKeyDown(Keys.A) && !InputManager.IsKeyDown(Keys.D))
            {
                moving = false;
            }
            if (Math.Abs(acceleration.X) > MAX_SPEED)
                acceleration.X = MAX_SPEED * Math.Sign(acceleration.X);
            if(onGround && InputManager.IsKeyDown(Keys.Space))
            {
                acceleration.Y += -500;
            }
            if(InputManager.KeyPressed(Keys.Z))
            {
                sprite.Texture.CurrentRectangle++;
                if (sprite.Texture.CurrentRectangle >= sprite.Texture.Rects.Count)
                    sprite.Texture.CurrentRectangle = 0;
            }

            if(!onGround)
            {
                acceleration.Y += 10;
            }

            // Check For Collisions

            velocity.X += acceleration.X * dt;
            if(acceleration.X != 0 && !moving)
                acceleration.X *= .85f;
            velocity.Y += acceleration.Y * dt;


            //int x5 = Math.Max(collisionBounds.X)

            //foreach(Rectangle rect in terrain)

            bool meme = false;
            foreach (Rectangle rect in terrain)
            {
                collisionBounds.X = (int)transform.Position.X;
                collisionBounds.Y = (int)(transform.Position.Y + velocity.Y);
                floorBounds.X = collisionBounds.X;
                floorBounds.Y = collisionBounds.Y + 10;

                if (collisionBounds.Intersects(rect))
                {
                    // Bottom Collision
                    if(collisionBounds.Y < rect.Y)
                    {
                        //Console.WriteLine("Bottom Collision");
                        acceleration.Y = 0;
                        transform.Position.Y = rect.Top - collisionBounds.Height;
                        //onGround = true;
                    }
                    else
                    {
                        acceleration.Y = 0;
                        transform.Position.Y = rect.Bottom;
                    }
                    velocity.Y = 0;
                }

                if(floorBounds.Intersects(rect))
                {
                    if (floorBounds.Y < rect.Y)
                    {
                        meme = true;
                    }
                }



                collisionBounds.Y = (int)transform.Position.Y;
                collisionBounds.X = (int)(transform.Position.X + velocity.X);

                if (collisionBounds.Intersects(rect))
                {
                    // Left collision
                    if (transform.Position.X < rect.X)
                    {
                        acceleration.X = 0;
                        transform.Position.X = rect.Left - collisionBounds.Width;
                    }
                    else
                    {
                        acceleration.X = 0;
                        transform.Position.X = rect.Right;
                    }
                        
                    velocity.X = 0;
                }
            }

            Console.WriteLine(acceleration.ToString() + "-" + velocity.ToString() + "-" + transform.Position.ToString());
            if (Math.Abs(velocity.X) < 1) velocity.X = 0;

            onGround = meme;

            if(acceleration.Y != 0)
            {
                onGround = false;
            }

            if (velocity.X < 0)
            {
                effect = SpriteEffects.FlipHorizontally;
            }
            if (velocity.X > 0)
            {
                effect = SpriteEffects.None;
            }


            transform.Position += velocity;
            */
            //collisionBounds = new Rectangle((int)(transform.Position.X), (int)(transform.Position.Y), collisionBounds.Width, collisionBounds.Height);
        }


        public void Draw(SpriteBatch spriteBatch)
        {
            spriteBatch.Draw(sprite.Texture, transform.Position, spriteBounds,
                sprite.Color, transform.Rotation, new Vector2(colliderDisplacement.X, colliderDisplacement.Y), transform.Scale,
                effect, 0.0f);
            
            foreach(Rectangle rec in terrain)
            {
                spriteBatch.Draw(groundTexture, rec, Color.White);
            }

            //Game1.DrawBorder(spriteBatch, collisionBounds, 1, Color.Red);
            //Game1.DrawBorder(spriteBatch, floorBounds, 1, Color.Green);

        }
    }
}
