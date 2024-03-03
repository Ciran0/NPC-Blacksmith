using marioRPGMinigame.Essentials;
using marioRPGMinigame.StatesLogics;
using marioRPGMinigame.Projectiles;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace marioRPGMinigame.Entities
{
    public class Enemy: Entity
    {

        
        private Texture2D _enemySprite;
        public float maxSpeedY = 40;


        public Enemy(float x, float y)
        {
            hitbox = new RotatedRectangle(x, y, 32, 32, 0);
            _enemySprite = Game1.assetsManager.textures["enemy"];
            scale = hitbox.width / _enemySprite.Width;
            angle = 0;
            active = true;
            visible = true;
            velocity.X = 5;
            faction = "enemies";
        }

        public void Update()
        {
            move(velocity.X, velocity.Y);

            if (hitbox.checkCollision(Game1.player.hitbox))
            {
                Game1.player.active = false;
                Game1.player.visible = false;
            }

            if (this.hitbox.bottomLeft.Y + Game1.gravity >= Game1.screenHeight && velocity.Y > 0)
            {
                velocity.Y -= 1;
                velocity.Y *= -1;
            }
            if (this.hitbox.bottomRight.X > Game1.screenWidth)
            {
                velocity.X *= -1;
            }
            if (this.hitbox.bottomLeft.X <= 0)
            {
                velocity.X *= -1;
            }



            if (velocity.Y < maxSpeedY && hitbox.bottomLeft.Y  < Game1.screenHeight)
            {
                velocity.Y += Game1.gravity;
            }
            

            


            if (dead)
            {
                this.Remove();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_enemySprite, new Vector2(hitbox.topLeft.X, hitbox.topLeft.Y), null, Color.White, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None,0);
            hitbox.Draw(spriteBatch);
        }

        public void move(float x, float y)
        {
            hitbox.move(x, y);
        }

        public void rotate(float Angle)
        {
            angle += Angle;
            hitbox.rotate(Angle);
            

        }

        public void Remove()
        {
            Game1.enemies.Remove(this);
        }

        public void kill(StandartBullet killer = null)
        {
            if (killer != null)
            {
                dead = true;
                MainGame_Logic.score += 1 * killer.multiplier;
                Game1.projectiles.Add(new StandartBullet(this, hitbox.center.X, hitbox.center.Y, Game1.rand.Next(-180, 180), 2, "alone", 20, killer.multiplier +1));
                Game1.projectiles.Add(new StandartBullet(this, hitbox.center.X, hitbox.center.Y, Game1.rand.Next(-180, 180), 2, "alone", 20, killer.multiplier + 1));
                Game1.projectiles.Add(new StandartBullet(this, hitbox.center.X, hitbox.center.Y, Game1.rand.Next(-180, 180), 2, "alone", 20, killer.multiplier + 1));
                Game1.projectiles.Add(new StandartBullet(this, hitbox.center.X, hitbox.center.Y, Game1.rand.Next(-180, 180), 2, "alone", 20, killer.multiplier + 1));
            }
        }

    }
}
