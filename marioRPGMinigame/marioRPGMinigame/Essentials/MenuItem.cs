using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Essentials
{
    public class MenuItem
    {

        private Vector2 topLeft;
        public RotatedRectangle body;
        public List<RotatedRectangle> buttons = new List<RotatedRectangle>();
        public bool pressed = false;


        public MenuItem(Vector2 origin, Menu parent, int width, int height, float angle)
        {
            topLeft = origin;
            body = new RotatedRectangle((int)(topLeft.X + parent.body.topLeft.X), (int)(topLeft.Y + parent.body.topLeft.Y), width, height,angle);
        }


        public void Update()
        {
            CheckForClick();
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            body.Draw(spriteBatch);
        }

        public void CheckForClick()
        {
            MouseState mState = Mouse.GetState();

            if (mState.LeftButton == ButtonState.Pressed)
            {
                OnClick(mState);
            }
            else
            {
                OnRelease(mState);
            }
            
        }

        public void OnClick(MouseState mState)
        {
            if (body.checkCollision(mState.Position.ToVector2()))
            {
                pressed = true;
            }
        }

        public void OnRelease(MouseState mState)
        {
            pressed = false;
        }




    }
}
