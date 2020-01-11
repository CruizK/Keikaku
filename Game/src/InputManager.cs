using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;

namespace Keikaku
{
    public class InputManager
    {

        static Point mousePos;
        static Point mouseDelta;

        static KeyboardState currentState;
        static KeyboardState previousState;

        static MouseState mouseState;
        static MouseState prevMouseState;

        public static void UpdateInput()
        {
            previousState = currentState;
            prevMouseState = mouseState;

            mouseState = Mouse.GetState();
            currentState = Keyboard.GetState();

            mousePos = mouseState.Position;
            mouseDelta = mouseState.Position - prevMouseState.Position;
        }

        public static bool IsMouseMoving()
        {
            return prevMouseState.Position != mouseState.Position;
        }

        public static Point GetMousePos()
        {
            return mousePos;
        }

        public static Point GetMouseDelta()
        {
            return mouseDelta;
        }

        public static bool IsLeftDown()
        {
            return mouseState.LeftButton == ButtonState.Pressed;
        }

        public static bool IsClicked()
        {
            return prevMouseState.LeftButton == ButtonState.Pressed && mouseState.LeftButton == ButtonState.Released;
        }


        public static bool IsKeyDown(Keys[] keys)
        {
            bool isDown = true;
            foreach (Keys key in keys)
                isDown = IsKeyDown(key);

            return isDown;
        }

        public static bool IsKeyDown(Keys key)
        {
            return currentState.IsKeyDown(key);
        }

        public static bool KeyPressed(Keys key)
        {
            return currentState.IsKeyUp(key) && previousState.IsKeyDown(key);
        }

        public static bool KeyReleased(Keys key)
        {
            return previousState.IsKeyDown(key) && currentState.IsKeyUp(key);
        }
    }
}
