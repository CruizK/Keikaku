using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Keikaku.Tiled;
using Keikaku.Character;
using Keikaku.UI;

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

        public static Texture2D pixel;
        Player player;

        Vector2 mouseCoords;

        Scene scene;

        Tilemap map;
        SpriteFont font;
        Panel panel;
        Label label2;
        RasterizerState UIRasterize;
        VerticalContainer verti;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            scene = new Scene();
            this.IsMouseVisible = true;
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        /// 
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            verti = new VerticalContainer();

            panel = new Panel();
            panel.Size = new Vector2(300, 150);
            panel.PanelColor = new Color(0, 0, 0, 0.5f);
            panel.TitleColor = new Color(0, 0, 0.7f, 0.5f);
            panel.Title = "Test Panel";
            panel.Name = "Panel 1";

            panel.AddChild(verti);


            int width = graphics.GraphicsDevice.Viewport.Width;
            int height = graphics.GraphicsDevice.Viewport.Height;

            cam = new Camera(width, height);
            pixel = new Texture2D(GraphicsDevice, 1, 1, false, SurfaceFormat.Color);
            pixel.SetData(new[] { Color.White });

            panel.Init();

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        ///

        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);

            map = Content.Load<Tilemap>("testMap");
            font = Content.Load<SpriteFont>("font");

            //panel.SetPosition(new Vector2(100, 100));

            //for(int i = 0; i < map.)

            scene.LoadContent(Content);
            player = new Player(map);
            // TODO: use this.Content to load your game content here

            //grassTexture = Content.Load<Texture2D>("grass")
            player.LoadContent(Content, GraphicsDevice);

            panel.LoadContent(Content);

            UIRasterize = new RasterizerState();
            UIRasterize.ScissorTestEnable = true;

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

                

                mouseCoords = cam.ScreenToWorldCoord(InputManager.GetMousePos().ToVector2());

                //Console.WriteLine(mouseCoords);

                if (InputManager.IsKeyDown(Keys.E))
                    cam.zoomCamera(0.1f);
                else if (InputManager.IsKeyDown(Keys.Q))
                    cam.zoomCamera(-0.1f);

                player.Update(gameTime, ref player);
            }

            panel.Update(gameTime);

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>


   

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.Gray);
           
            spriteBatch.Begin(SpriteSortMode.Texture, null, SamplerState.PointClamp, null, null, null, cam.transformMatrix);
            Ray2D ray = new Ray2D(player.GetCollisionBounds().Center.ToVector2(), Vector2.Normalize(mouseCoords- player.GetCollisionBounds().Center.ToVector2()));
            //Ray2D ray = new Ray2D(player.GetOrigin(), Vector2.One);
            Tile tile = map.RaycastTile(ray);
            //Console.WriteLine(ray.Direction);
            if (tile != null)
            {
                DrawLine(spriteBatch, ray.Position, new Vector2(tile.X, tile.Y), Color.Red);
                tile.color = Color.Red;
            }
                

            map.DrawLayers(spriteBatch);

            if (tile != null)
            {

                tile.color = Color.White;
            }

            //tile.color = Color.White;

            //Game1.DrawBorder(spriteBatch, cam.viewBounds, 10, Color.Red);
            //scene.Draw(spriteBatch);
            player.Draw(spriteBatch);
            spriteBatch.End();


            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.AlphaBlend, null, null, UIRasterize);
            //label.Draw(spriteBatch);
            panel.Draw(spriteBatch);
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

        public static void DrawRay(SpriteBatch spriteBatch, Ray2D ray, Color color)
        {
            //float rot = Math.Atan2(ray.Position)
            Vector2 endPos = ray.Position + ray.Direction * 5000;
            DrawLine(spriteBatch, ray.Position, endPos, color);
            

            //Console.WriteLine(MathHelper.ToDegrees(rot));
        }

        public static void DrawLine(SpriteBatch spriteBatch, Vector2 startPos, Vector2 endPos, Color color)
        {
            //Console.WriteLine(startPos + "-" + endPos);
            float rot = (float)Math.Atan2(endPos.Y - startPos.Y, endPos.X - startPos.X);
            // arctan endpos.x-startpos.x, endpos.y-startpos.y??
            Vector2 width = endPos - startPos;
            spriteBatch.Draw(pixel, new Rectangle((int)startPos.X, (int)startPos.Y, (int)width.Length(), 1), 
                null, color, rot, Vector2.Zero, SpriteEffects.None, 0f);

            //Console.WriteLine(MathHelper.ToDegrees(rot));
        }
    }
}
