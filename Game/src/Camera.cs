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
            
        }

    }
}
