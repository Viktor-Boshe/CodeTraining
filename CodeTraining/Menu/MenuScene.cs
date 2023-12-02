// MenuScene.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTraining.Game_Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CodeTraining.GameManager;

namespace CodeTraining.Menu
{
    internal class MenuScene : Component
    {
        private Input input;
        public static Texture2D[] buttons = new Texture2D[3];
        public Rectangle[] buttonRectangles = new Rectangle[buttons.Length];

        private const int xSpacing = 5;
        private const int ySpacing = 150;
        private const int ButtonSpacing = 100;

        public override void Init(ContentManager contentManager)
        {
            input = new Input();
            for (int i = 0; i < buttons.Length; i++)
            {
                buttons[i] = contentManager.Load<Texture2D>($"Textures/button_{i}");
                buttonRectangles[i] = new Rectangle(xSpacing, ySpacing + ButtonSpacing * i, buttons[i].Width, buttons[i].Height);
            }
        }

        public override void Update(GameTime gameTime)
        {
            input.UpdateMouse(gameTime);

            if (input.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed && input.mouseRectangle.Intersects(buttonRectangles[0]))
            {
                Data.CurrentState = GameState.Game;
            }
            if (input.mouseRectangle.Intersects(buttonRectangles[1]) && input.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Data.CurrentState = GameState.Options;
            }
            if (input.mouseRectangle.Intersects(buttonRectangles[2]) && input.mouseState.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
            {
                Data.Exit = true;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            for (int i = 0; i < buttons.Length; i++)
            {
                _spriteBatch.Draw(buttons[i], buttonRectangles[i], Color.Black);
                if (input.mouseRectangle.Intersects(buttonRectangles[i]))
                {
                    _spriteBatch.Draw(buttons[i], buttonRectangles[i], Color.White);
                }
            }
        }
    }
}