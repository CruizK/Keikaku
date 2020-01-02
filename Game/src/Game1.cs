using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keikaku.Tiled;

namespace Keikaku
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;

        Camera cam;
        int cameraSpeed = 5;
        float cameraZoom = 0.5f;

        static Texture2D pixel;
        Player player;

        Vector2 mouseCoords;

        Scene scene;
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            scene = new Scene();
            player = new Player();
            this.IsMouseVisible = true;
            Console.WriteLine(typeof(Tilemap).AssemblyQualifiedName);
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            
            base.Initialize();

            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;

            cam = new Camera(width, height);
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });



        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        ///
        Tilemap map;
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Tilemap>("testMap");
            //for(int i = 0; i < map.)

            scene.LoadContent(Content);

            // TODO: use this.Content to load your game content here

            //grassTexture = Content.Load<Texture2D>("grass");

            player.LoadContent(Content, GraphicsDevice);
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
            scene.UnloadContent(Content);
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();


            Logger.Update(gameTime);

            //cam.zoomCamera(cameraZoom);
            InputManager.UpdateInput();


            if (this.IsActive)
            {
                cam.Update(gameTime);
                cam.setCameraPosition(new Vector2((int)player.GetOrigin().X, (int)player.GetOrigin().Y));

                mouseCoords = InputManager.GetMousePos().ToVector2();

                //mouseCoords = cam.ScreenToWorldCoord(InputManager.GetMousePos().ToVector2());

                //Console.WriteLine(mouseCoords);

                if (InputManager.IsKeyDown(Keys.E))
                    cam.zoomCamera(0.1f);
                else if (InputManager.IsKeyDown(Keys.Q))
                    cam.zoomCamera(-0.1f);

                player.Update(gameTime);
            }



            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Black);
            
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState.PointClamp, null, null, null, cam.transformMatrix);
            map.DrawLayers(spriteBatch);
            Game1.DrawBorder(spriteBatch, cam.viewBounds, 10, Color.Red);
            //scene.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void DrawBorder(SpriteBatch spriteBatch, Rectangle rectangleToDraw, int thicknessOfBorder, Color borderColor)
        {
            // Draw top line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, rectangleToDraw.Width, thicknessOfBorder), borderColor);

            // Draw left line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X, rectangleToDraw.Y, thicknessOfBorder, rectangleToDraw.Height), borderColor);

            // Draw right line
            spriteBatch.Draw(pixel, new Rectangle((rectangleToDraw.X + rectangleToDraw.Width - thicknessOfBorder),
                                            rectangleToDraw.Y,
                                            thicknessOfBorder,
                                            rectangleToDraw.Height), borderColor);
            // Draw bottom line
            spriteBatch.Draw(pixel, new Rectangle(rectangleToDraw.X,
                                            rectangleToDraw.Y + rectangleToDraw.Height - thicknessOfBorder,
                                            rectangleToDraw.Width,
                                            thicknessOfBorder), borderColor);
        }
    }
}
