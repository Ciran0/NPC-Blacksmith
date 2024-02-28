using marioRPGMinigame.Essentials;
using marioRPGMinigame.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using static System.Collections.Specialized.BitVector32;

namespace marioRPGMinigame.Entities
{
    public class Player: Entity
    {

        public RotatedRectangle hitbox;
        public RotatedRectangle drawbox;
        private Texture2D _playerSprite;
        private float scale;
        public float angle;
        public bool active;
        public bool visible;
        public bool pressed = false;
        public Vector2 velocity;
        public bool isWalking=false;   
        public bool isRightKeyPressed=false;   
        public bool isLeftKeyPressed = false;


        public Player(float x, float y)
        {
            hitbox = new RotatedRectangle(x, y, 32, 32, 0);
            drawbox = new RotatedRectangle(x, y, 32, 32, 0);
            _playerSprite = Game1.assetsManager.textures["player"];
            scale = 1f;
            angle = 0;
            active = true;
            visible = true;
            faction = "allies";
        }

        public void Update()
        {
            CheckInput();
            move(velocity.X, velocity.Y);
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            
            spriteBatch.Draw(_playerSprite, drawbox.center, null, Color.White, MathHelper.ToRadians(angle), new Vector2(drawbox.width/2,drawbox.height/2), scale, SpriteEffects.None,0);
                                            // ^- where on the screen will the sprite start to be drawn            ^- which coordinate of the sprite will be put on the start point 
            hitbox.Draw(spriteBatch);
        }


        public void move(float x, float y)
        {
            hitbox.move(x, y);
            drawbox.move(x, y);
        }

        public void rotate(float Angle)
        {

            hitbox.rotate(Angle);
            drawbox.rotate(Angle);
            angle += Angle;
            
            

        }

        public void CheckInput()
        {
            KeyboardState keybState = Keyboard.GetState();
            

            if (keybState.IsKeyDown(Keys.Left) && !isLeftKeyPressed)
            {
                velocity.X += -5;
                isLeftKeyPressed = true;
            }
            if (keybState.IsKeyDown(Keys.Right) && !isRightKeyPressed)
            {
                velocity.X += 5;
                isRightKeyPressed = true;
            }
            if (keybState.IsKeyUp(Keys.Left) && isLeftKeyPressed)
            {
                velocity.X += 5;
                isLeftKeyPressed = false;
            }
            if (keybState.IsKeyUp(Keys.Right) && isRightKeyPressed)
            {
                velocity.X -= 5;
                isRightKeyPressed = false;
            }
            if (keybState.IsKeyDown(Keys.R) && pressed == false)
            {
                pressed = true;
                rotate(5);
            }

            if (keybState.IsKeyDown(Keys.A) && pressed == false)
            {
                pressed = true;
                Game1.projectiles.Add(new StandartBullet(this, hitbox.center.X, hitbox.topLeft.Y, 90, 10));
            }
            if (keybState.IsKeyUp(Keys.A)){ pressed = false; }
        }

    }
}
