using marioRPGMinigame.Essentials;
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
    public class UIItem_text: UIItem
    {
        private Vector2 topLeft;
        public string text;




        public UIItem_text(Vector2 origin, UI parent, int width, int height, float angle, string Text): base(origin, parent, width, height, angle)
        {
            topLeft = origin;
            body = new RotatedRectangle((int)(topLeft.X + parent.body.topLeft.X), (int)(topLeft.Y + parent.body.topLeft.Y), width, height, angle);
            text = Text;
        }


        public override void Update()
        {
            
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            //body.Draw(spriteBatch);
            spriteBatch.DrawString(font, text, topLeft, Color.Black);
        }


    }
}
