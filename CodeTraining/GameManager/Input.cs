// Input.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
namespace CodeTraining.GameManager
{
    internal class Input
    {
        public MouseState mouseState, oldmouseState;
        public Rectangle mouseRectangle;
        private KeyboardState currentKeyboardState;
        private KeyboardState previousKeyboardState;
        public void UpdateMouse(GameTime gameTime)
        {
            oldmouseState = mouseState;
            mouseState = Mouse.GetState();
            mouseRectangle = new Rectangle(mouseState.X, mouseState.Y, 1, 1);

        }
        public void UpdateKeyboard(GameTime gameTime)
        {
            previousKeyboardState = currentKeyboardState;
            currentKeyboardState = Keyboard.GetState();
        }
        public bool IsKeyDown(Keys key)
        {
            return currentKeyboardState.IsKeyDown(key);
        }

        public bool IsKeyUp(Keys key)
        {
            return currentKeyboardState.IsKeyUp(key);
        }

        public bool WasKeyPressed(Keys key)
        {
            return IsKeyDown(key) && previousKeyboardState.IsKeyUp(key);
        }
    }
}
