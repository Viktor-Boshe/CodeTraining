//Game1.cs
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using CodeTraining.Game_Core;
using CodeTraining.GameManager;

namespace CodeTraining
{
    public class Game1 : Game
    {
        private GameStateManager GameStateManager = new GameStateManager();
        private GraphicsDeviceManager _graphics;
        private SpriteBatch _spriteBatch;

        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            _graphics.PreferredBackBufferWidth = Data.ScreenWidth;
            _graphics.PreferredBackBufferHeight = Data.ScreenHeight;
            _graphics.ApplyChanges();

            Window.Title = "Code Tester";
            Window.AllowUserResizing = false;

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);

            // TODO: use this.Content to load your game content here
            GameStateManager.Init(Content);

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            // TODO: Add your update logic here
            GameStateManager.Update(gameTime);
            if (Data.Exit)
            {
                this.Exit();
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.BurlyWood);

            // TODO: Add your drawing code here
            _spriteBatch.Begin();

            GameStateManager.Draw(gameTime, _spriteBatch);

            _spriteBatch.End();


            base.Draw(gameTime);
        }
    }
}
