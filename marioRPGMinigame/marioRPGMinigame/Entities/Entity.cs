using marioRPGMinigame.Essentials;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Entities
{
    public class Entity
    {
        public string faction;
        public RotatedRectangle hitbox;
        public RotatedRectangle drawbox;
        public float scale;
        public float angle;
        public bool active;
        public bool visible;
        public bool dead = false;
        public Vector2 velocity;


    }
}
