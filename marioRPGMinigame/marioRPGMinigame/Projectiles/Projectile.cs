using marioRPGMinigame.Entities;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Projectiles
{
    public class Projectile
    {
        public Entity owner;
        public String faction;
        public Vector2 velocity;

        public Projectile(Entity entity, float x, float y, float angle, float speed, string faction = null)
        {
            owner = entity;
            if (faction != null)
            {
                this.faction = faction;
            }
            else
            {
                this.faction = owner.faction;
            }
            
            this.velocity = new Vector2((float)Math.Cos(MathHelper.ToRadians(angle)), -(float)Math.Sin(MathHelper.ToRadians(angle))) * speed;

        }

        public virtual void Update()
        {

        }
        public virtual void Draw(SpriteBatch spriteBatch)
        {

        }

        public virtual void move(float x, float y)
        {

        }
        public virtual void rotate(float Angle)
        {

        }



    }
}
