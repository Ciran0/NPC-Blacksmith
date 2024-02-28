using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Essentials
{
    public class Menu
    {

        public RotatedRectangle body;
        public Vector2 topLeft;

        public List<MenuItem> buttons = new List<MenuItem>();


        public Menu(Vector2 origin)
        {
            topLeft = origin;
            body = new RotatedRectangle((int)topLeft.X, (int)topLeft.Y, 200, 200,0);
        }


        public virtual void Update()
        {
            foreach(MenuItem button in buttons)
            {
                button.Update();
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {

            foreach(MenuItem button in buttons)
            {
                button.Draw(spriteBatch);
            }


            body.Draw(spriteBatch);
        }


    }
}
