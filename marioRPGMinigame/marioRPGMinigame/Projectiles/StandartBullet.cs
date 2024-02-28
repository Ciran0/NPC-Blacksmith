using marioRPGMinigame.Entities;
using marioRPGMinigame.Essentials;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Diagnostics;

namespace marioRPGMinigame.Projectiles
{
    public class StandartBullet: Projectile
    {

        public RotatedRectangle hitbox;
        private Texture2D _projectileSprite;
        private float scale;
        public float angle;
        public bool active;
        public bool visible;
        public bool dead = false;
        private int timer = 0;
        public int duration; // -1 = infinite duration
        public int multiplier;

        public StandartBullet(Entity entity, float x, float y, float angle, float speed, string faction = null, int duration = -1, int multiplier = 1) : base(entity,  x,  y, angle, speed, faction)
        {
            hitbox = new RotatedRectangle(x, y, 16, 16, 0);
            _projectileSprite = Game1.assetsManager.textures["projectile"];
            scale = hitbox.width / _projectileSprite.Width;
            angle = 0;
            active = true;
            visible = true;
            this.duration = duration;
            this.multiplier = multiplier;
        }

        public override void Update()
        {
            timer++;
            move(velocity.X, velocity.Y);

            if (new[] { "allies", "alone" }.Contains(faction))
            {
                foreach (Enemy enemy in Game1.enemies)
                {
                    if (hitbox.checkCollision(enemy.hitbox))
                    {
                        Debug.WriteLine("touche");
                        enemy.kill(this);
                        this.dead = true;
                    }
                }
            }


            if (new[] {"enemies", "alone"}.Contains(faction))
            {
                if (hitbox.checkCollision(Game1.player.hitbox))
                {
                    Game1.player.active = false;
                    Game1.player.visible = false;
                    this.dead = true;
                }
            }
            


            if (duration != -1 && timer > duration)
            {
                Debug.WriteLine("dead");
                dead = true;
            }
            if (dead)
            {
                Game1.projectiles.Remove(this);
            }
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {

            spriteBatch.Draw(_projectileSprite, new Vector2(hitbox.topLeft.X, hitbox.topLeft.Y), null, Color.White, MathHelper.ToRadians(angle), Vector2.Zero, scale, SpriteEffects.None, 0);
            hitbox.Draw(spriteBatch);

        }

        public override void move(float x, float y)
        {
            hitbox.move(x, y);
        }

        public override void rotate(float Angle)
        {
            angle += Angle;
            hitbox.rotate(Angle);


        }

    }
}
