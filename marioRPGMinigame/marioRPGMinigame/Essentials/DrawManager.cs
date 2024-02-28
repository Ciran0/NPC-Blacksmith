using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace marioRPGMinigame.Essentials
{
    public class DrawManager
    {

        private Texture2D pixel;
        public DrawManager(GraphicsDevice graphicsDevice)
        {
            pixel = new Texture2D(graphicsDevice, 1, 1);
            pixel.SetData(new[] { Color.White }); ;
        }


        public void DrawLine(SpriteBatch spriteBatch, Vector2 point1, Vector2 point2, Color color, float thickness = 1f)
        {
            var distance = Vector2.Distance(point1, point2);
            float angle = (float)Math.Atan2(point2.Y - point1.Y, point2.X - point1.X);
            angle = MathHelper.ToDegrees(angle);
            DrawLine(spriteBatch, point1, distance, angle, color, thickness);
        }

        public void DrawLine(SpriteBatch spriteBatch, Vector2 point, float length, float angle, Color color, float thickness = 1f)
        {
            // Line with no angle and an origin of 0,0 would be horizontal and have its origin in top left corner
            Vector2 origin;


            //Little origin manipulation to make angled lines and straight lines look smoother when used together 
            if (angle % 90 == 0)
            {
                origin = new Vector2(0f, 0f);
            }
            else
            {
                origin = new Vector2(0f, 0.5f);
            }


            angle = MathHelper.ToRadians(angle);
            var scale = new Vector2(length, thickness);
            spriteBatch.Draw(pixel, point, null, color, angle, origin, scale, SpriteEffects.None, 0);
        }
        //


        public void DrawRectangle(SpriteBatch spriteBatch, Vector2 topLeft, int width, int height, Color color, float angle = 0f, float thickness = 1f)
        {
            angle = MathHelper.ToRadians(angle);
            Vector2 topRight = new Vector2(topLeft.X + (float)Math.Cos(angle) * (width), topLeft.Y + (float)Math.Sin(angle) * (width));
            Vector2 bottomRight = new Vector2(topRight.X + (float)Math.Cos((angle + 90)) * (height), topRight.Y + (float)Math.Sin((angle + 90)) * (height));
            Vector2 bottomLeft = new Vector2(bottomRight.X + (float)Math.Cos((angle + 180)) * (width), bottomRight.Y + (float)Math.Sin((angle + 180)) * (width));

            DrawRectangle(spriteBatch, topLeft, topRight, bottomRight, bottomLeft, color, thickness);
        }

        public void DrawRectangle(SpriteBatch spriteBatch, Vector2 topLeft, Vector2 topRight, Vector2 bottomRight, Vector2 bottomLeft, Color color, float thickness = 1f)
        {
            DrawLine(spriteBatch, topLeft, topRight, color, thickness);
            DrawLine(spriteBatch, Vector2.Round(topRight), Vector2.Round(bottomRight), color, thickness);
            DrawLine(spriteBatch, Vector2.Round(bottomRight), Vector2.Round(bottomLeft), color, thickness);
            DrawLine(spriteBatch, Vector2.Round(bottomLeft), Vector2.Round(topLeft), color, thickness);
        }
    }
}
