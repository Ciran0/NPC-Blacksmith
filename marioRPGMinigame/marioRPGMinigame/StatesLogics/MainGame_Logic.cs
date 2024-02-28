using marioRPGMinigame.Entities;
using marioRPGMinigame.Projectiles;
using marioRPGMinigame.UIs;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.StatesLogics
{
    public class MainGame_Logic: StateLogic
    {

        private float playerInitialX;
        private float playerInitialY;
        private int timer;
        public static int score;
        

        public MainGame_Logic(float x, float y)
        {
            playerInitialX = x;
            playerInitialY = y;
        }

        public override void Setup()
        {
            if (Game1.player == null) { Game1.player = new Player(playerInitialX, playerInitialY); }
            else
            {
                Game1.player.active = true;
                Game1.player.visible = true;
            }

            Game1.UIs.Add(new MainUI(new Vector2(0, 0), Game1.screenWidth, Game1.screenHeight));
            


        }

        public override void Clear()
        {
            //Game1.player.move(-Game1.player.hitbox.topLeft.X, -Game1.player.hitbox.topLeft.Y);
            Game1.player.active = false;
            Game1.player.visible = false;
            Game1.projectiles.Clear();
            Game1.enemies.Clear();
            Game1.UIs.Clear();

            score = 0;
        }
        
        public override void Update()
        {

            KeyboardState keybState = Keyboard.GetState();
            if (keybState.IsKeyDown(Keys.Enter) || keybState.IsKeyDown(Keys.Space))
            {
                Game1.changeState("mainMenu");
            }

            timer++;

            if (timer%20 == 0)
            {

                Game1.enemies.Add(new Enemy(50 + Game1.rand.Next(-30,Game1.screenWidth-50), 10)); 
            }

            


            for (int i = Game1.enemies.Count - 1; i >= 0; i--)
            {

                if (Game1.enemies[i].hitbox.topLeft.X > Game1.screenWidth)
                {
                    Game1.enemies[i].Remove();
                };
            }

            for (int i = Game1.projectiles.Count - 1; i >= 0; i--)
            {
                Game1.projectiles[i].rotate(5);
            }


        }



    }
}
