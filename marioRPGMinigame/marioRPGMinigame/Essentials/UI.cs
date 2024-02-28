using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Essentials
{
    public class UI
    {

        public RotatedRectangle body;
        public Vector2 topLeft;
        private float width;
        private float height;


        public List<UIItem> items = new List<UIItem>();


        public UI(Vector2 origin, float w, float h)
        {
            width = w;
            height = h;
            topLeft = origin;
            body = new RotatedRectangle((int)topLeft.X, (int)topLeft.Y, width, height, 0);
        }


        public virtual void Update()
        {
            foreach (UIItem item in items)
            {
                item.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach (UIItem item in items)
            {
                item.Draw(spriteBatch);
            }


            body.Draw(spriteBatch);
        }

    }
}
