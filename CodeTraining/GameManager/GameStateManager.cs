// GameStateManager.cs
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeTraining.Game_Core;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using CodeTraining.Menu;
using CodeTraining.Scenes;

namespace CodeTraining.GameManager
{
    public enum GameState
    {
        Menu,
        Game,
        Options
    }

    public class GameStateManager : Component
    {
        private MenuScene menuScene = new MenuScene();
        private GameScene gameScene = new GameScene();

        public override void Init(ContentManager contentManager)
        {
            menuScene.Init(contentManager);
            gameScene.Init(contentManager);
        }

        public override void Update(GameTime gameTime)
        {
            switch (Data.CurrentState)
            {
                case GameState.Menu:
                    menuScene.Update(gameTime);
                    break;
                case GameState.Game:
                    gameScene.Update(gameTime);
                    // Update game state
                    break;
                case GameState.Options:
                    // Update options state
                    break;
                default:
                    break;
            }
        }

        public override void Draw(GameTime gameTime, SpriteBatch _spriteBatch)
        {
            switch (Data.CurrentState)
            {
                case GameState.Menu:
                    menuScene.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.Game:
                    gameScene.Draw(gameTime, _spriteBatch);
                    break;
                case GameState.Options:
                    break;
                default:
                    break;
            }
        }
    }
}