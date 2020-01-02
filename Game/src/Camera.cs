using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace Keikaku
{
    public class Camera
    {
        
        public Matrix transformMatrix { get; private set; }

        private Vector2 position;
        private float zoom;
        private float rotation;

        private float screenWidth;
        private float screenHeight;

        public Rectangle viewBounds;

        public Camera(int screenWidth, int screenHeight)
        {
            this.screenWidth = screenWidth;
            this.screenHeight = screenHeight;

            zoom = 1.0f;
            rotation = 0.0f;
            position = new Vector2(0, 0);
        }

        public void setCameraPosition(Vector2 newPosition)
        {
            position = newPosition;
        }

        public Vector2 ScreenToWorldCoord(Vector2 screenCoords)
        {
            return Vector2.Transform(screenCoords, Matrix.Invert(transformMatrix));
        }

        public void moveCamera(Vector2 movePosition)
        {
            position += movePosition;
        }

        public void zoomCamera(float cameraZoom)
        {
            zoom += zoom * cameraZoom;
        }

        public void Update(GameTime gameTime)
        {
            
            transformMatrix = Matrix.CreateTranslation(-position.X, -position.Y, 0) *
                Matrix.CreateScale(zoom) *
                Matrix.CreateRotationZ(rotation) *
                Matrix.CreateTranslation(new Vector3(screenWidth * 0.5f, screenHeight * 0.5f,0));

            Vector2 Origin = new Vector2((screenWidth / 2) / zoom, (screenHeight / 2) / zoom);
            Console.WriteLine("Origin: " + Origin.ToString() + ", Zoom: " + zoom);
            
            viewBounds = new Rectangle((int)(position.X - Origin.X), (int)(position.Y - Origin.Y), (int)(Origin.X*2), (int)(Origin.Y*2));
        }

        public bool IsInView(Rectangle rect)
        {
            return viewBounds.Intersects(rect);
        }

    }
}
