using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Essentials
{
    public class UIItem
    {
        private Vector2 topLeft;
        public RotatedRectangle body;
        public SpriteFont font = Game1.assetsManager.fonts["myFont"];




        public UIItem(Vector2 origin, UI parent, int width, int height, float angle)
        {
            topLeft = origin;
            body = new RotatedRectangle((int)(topLeft.X + parent.body.topLeft.X), (int)(topLeft.Y + parent.body.topLeft.Y), width, height, angle);
        }


        public virtual void Update()
        {
            
        }

        public virtual void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
        }


    }
}
