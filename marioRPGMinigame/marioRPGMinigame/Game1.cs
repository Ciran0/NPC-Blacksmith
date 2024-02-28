using marioRPGMinigame.Essentials;
using marioRPGMinigame.Menus;
using marioRPGMinigame.Entities;
using marioRPGMinigame.StatesLogics;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System.Collections.Generic;
using System.Diagnostics;
using marioRPGMinigame.Projectiles;
using System;
using marioRPGMinigame.Essentials;

namespace marioRPGMinigame
{
    public class Game1 : Game
    {
        public static AssetsManager assetsManager;
        private GraphicsDeviceManager _graphics;
        private GraphicsDevice _device;
        private SpriteBatch _spriteBatch;
        public static DrawManager drawManager;

        public static int screenWidth;
        public static int screenHeight;


        public static List<RotatedRectangle> rectangles = new List<RotatedRectangle>();

        public static List<Menu> menus = new List<Menu>();

        public static List<UI> UIs = new List<UI>();

        public static List<Enemy> enemies = new List<Enemy>();

        public static List<Projectile> projectiles = new List<Projectile>();

        

        public static Player player;


        public static string state;
        private static bool stateChanged = false;

        public static Random rand = new Random();


        #region states
        public static StateLogic stateLogic = new StateLogic();
        public MainMenu_Logic mainMenu_Logic;
        public MainGame_Logic mainGame_Logic;


        #endregion states

        #region gameplayValues

        public static float gravity = 0.2f;

        #endregion gameplayValues


        public Game1()
        {
            _graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            state = "starting";

            base.Initialize();
        }

        protected override void LoadContent()
        {
            _spriteBatch = new SpriteBatch(GraphicsDevice);
            _device = _graphics.GraphicsDevice;
            drawManager = new DrawManager(GraphicsDevice);
            screenWidth = _device.PresentationParameters.BackBufferWidth;
            screenHeight = _device.PresentationParameters.BackBufferHeight;
            assetsManager = new AssetsManager(Content);

            

            changeState("mainMenu");

        }

        protected override void Update(GameTime gameTime)
        {
            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Keys.Escape))
                Exit();

            if (stateChanged)
            {
                setupNewState();
            }

            stateLogic.Update();

            foreach (var menu in menus)
            {
                menu.Update();
            }
            foreach (UI ui in UIs)
            {
                ui.Update();
            }


            for (int i = enemies.Count - 1; i >= 0; i--) //count from last to first, so the loop doesnt get broken if the current instance gets deleted during the loop
            {
                enemies[i].Update();
            }

            for (int i = projectiles.Count-1; i >= 0; i--)
            {
                projectiles[i].Update();
            }


            if (player != null && player.active)
            { 
                player.Update();
            }

            

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.CornflowerBlue);

            _spriteBatch.Begin();

            foreach(Menu menu in menus)
            {
                menu.Draw(_spriteBatch);
            }
            foreach (UI ui in UIs)
            {
                ui.Draw(_spriteBatch);
            }
            foreach (var enemy in enemies)
            {
                
                enemy.Draw(_spriteBatch);
            }
            foreach (Projectile projectile in projectiles)
            {
                projectile.Draw(_spriteBatch);
            }
            foreach (RotatedRectangle rect in rectangles)
            {
                rect.Draw(_spriteBatch);
            }
            if (player != null && player.visible) //difference between "&" and "&&"-> & tries all verifications, && stops after the first false
            {
                player.Draw(_spriteBatch);
            }
            _spriteBatch.End();

            base.Draw(gameTime);
        }

        public static void changeState(string newState)
        {
            stateChanged = true;
            state = newState;
        }

        private void setupNewState()
        {
            switch (state)
            {
                case ("mainMenu"):
                    stateLogic.Clear();
                    if (mainMenu_Logic == null) { mainMenu_Logic = new MainMenu_Logic(screenWidth, screenHeight); }
                    stateLogic = mainMenu_Logic;
                    stateLogic.Setup();
                    break;
                case ("mainGame"):
                    stateLogic.Clear();
                    if (mainGame_Logic == null) { mainGame_Logic = new MainGame_Logic(screenWidth / 2, screenHeight - 50); }
                    stateLogic = mainGame_Logic;
                    stateLogic.Setup();
                    break;
            }
            stateChanged = false;
        }

        public static void Remove(Enemy enemy)
        {
            enemies.Remove(enemy);
        }
        public static void Remove(Projectile enemy)
        {
            projectiles.Remove(enemy);
        }
    }
}