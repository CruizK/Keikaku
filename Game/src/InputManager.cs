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

        static KeyboardState currentState;
        static KeyboardState previousState;

        public static void UpdateInput()
        {
            previousState = currentState;

            mousePos = Mouse.GetState().Position;
            
            currentState = Keyboard.GetState();
        }

        public static Point GetMousePos()
        {
            return mousePos;
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
    }
}
