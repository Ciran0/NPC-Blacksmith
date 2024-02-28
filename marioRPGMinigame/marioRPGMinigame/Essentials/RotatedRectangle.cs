using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;


namespace marioRPGMinigame.Essentials
{
    public class RotatedRectangle
    {
        public Vector2 topLeft;
        public Vector2 topRight;
        public Vector2 bottomRight;
        public Vector2 bottomLeft;
        public Vector2 center;
        public float width;
        public float height;
        public float radius;
        public float thickness;

        public float baseAngle;
        public float topLeftAngle;
        public float topRightAngle;
        public float bottomRightAngle;
        public float bottomLeftAngle;

        public float angleBetweenTLandTR;
        public float angleBetweenTLandBL;
        public float angleBetweenBRandTR;
        public float angleBetweenBRandBL;

        public float distanceCenterToTL;
        public float distanceCenterToTR;
        public float distanceCenterToBR;
        public float distanceCenterToBL;

        public Color color;

        public List<RotatedRectangle> collidingWith = new List<RotatedRectangle>();

        public RotatedRectangle(float x, float y, float Width, float Height, float angle)
        {
            float angleRad = MathHelper.ToRadians(angle);
            width = Width;
            height = Height;

            topLeft = new Vector2(x, y);
            topRight = new Vector2(topLeft.X + (float)Math.Cos(angleRad) * (width), topLeft.Y + (float)Math.Sin(angleRad) * (width));
            bottomRight = new Vector2(topRight.X + (float)Math.Cos(angleRad + Math.PI / 2) * (height), topRight.Y + (float)Math.Sin(angleRad + Math.PI / 2) * (height));
            bottomLeft = new Vector2(bottomRight.X + (float)Math.Cos(angleRad + Math.PI) * (width), bottomRight.Y + (float)Math.Sin(angleRad + Math.PI) * (width));
            baseAngle = angle;
            center = new Vector2((topLeft.X + topRight.X + bottomRight.X + bottomLeft.X) / 4, (topLeft.Y + topRight.Y + bottomRight.Y + bottomLeft.Y) / 4);
            radius = Vector2.Distance(center, new Vector2(new[] { topLeft.X, topRight.X, bottomRight.X, bottomLeft.X }.Max(), new[] { topLeft.Y, topRight.Y, bottomRight.Y, bottomLeft.Y }.Max()));

            angleBetweenTLandTR = MathHelper.ToDegrees(-(float)Math.Atan2(topRight.Y - topLeft.Y, topRight.X - topLeft.X));
            angleBetweenTLandBL = MathHelper.ToDegrees(-(float)Math.Atan2(bottomLeft.Y - topLeft.Y, bottomLeft.X - topLeft.X));

            angleBetweenBRandTR = MathHelper.ToDegrees(-(float)Math.Atan2(topRight.Y - bottomRight.Y, topRight.X - bottomRight.X));
            angleBetweenBRandBL = MathHelper.ToDegrees(-(float)Math.Atan2(bottomLeft.Y - bottomRight.Y, bottomLeft.X - bottomRight.X));

            distanceCenterToTL = Vector2.Distance(center, topLeft);
            distanceCenterToTR = Vector2.Distance(center, topRight);
            distanceCenterToBR = Vector2.Distance(center, bottomRight);
            distanceCenterToBL = Vector2.Distance(center, bottomLeft);

            topLeftAngle = MathHelper.ToDegrees((float)Math.Atan2(topLeft.Y - center.Y, topLeft.X - center.X));
            topRightAngle = MathHelper.ToDegrees((float)Math.Atan2(topRight.Y - center.Y, topRight.X - center.X));
            bottomRightAngle = MathHelper.ToDegrees((float)Math.Atan2(bottomRight.Y - center.Y, bottomRight.X - center.X));
            bottomLeftAngle = MathHelper.ToDegrees((float)Math.Atan2(bottomLeft.Y - center.Y, bottomLeft.X - center.X));

            color = Color.Black;
            thickness = 1f;

        }


        public void Update()
        {
            /*
            foreach(RotatedRectangle r in Game1.rectangles)
            {
                if (r == this) { continue; }
                if (Vector2.Distance(this.center, r.center) <= this.radius + r.radius) //dont start the detection algorythm is both rectangles arent within a good distance
                {
                    checkCollision(r);
                }
            }*/
        }

        public void move(float x, float y)
        {
            Vector2 vector = new Vector2(x, y);
            topLeft += vector;
            topRight += vector;
            bottomRight += vector;
            bottomLeft += vector;
            center += vector;
        }

        public void rotate(float angle)
        {
            rotate(center.X, center.Y, angle);
        }

        public void rotate(float x, float y, float angle)
        {




            angle = MathHelper.ToRadians(angle);

            topLeft = center + (new Vector2((float)Math.Cos(angle + MathHelper.ToRadians(topLeftAngle)) * distanceCenterToTL, (float)Math.Sin(angle + MathHelper.ToRadians(topLeftAngle)) * distanceCenterToTL));
            topRight = center + (new Vector2((float)Math.Cos(angle + MathHelper.ToRadians(topRightAngle)) * distanceCenterToTR, (float)Math.Sin(angle + MathHelper.ToRadians(topRightAngle)) * distanceCenterToTR));
            bottomRight = center + (new Vector2((float)Math.Cos(angle + MathHelper.ToRadians(bottomRightAngle)) * distanceCenterToBR, (float)Math.Sin(angle + MathHelper.ToRadians(bottomRightAngle)) * distanceCenterToBR));
            bottomLeft = center + (new Vector2((float)Math.Cos(angle + MathHelper.ToRadians(bottomLeftAngle)) * distanceCenterToBL, (float)Math.Sin(angle + MathHelper.ToRadians(bottomLeftAngle)) * distanceCenterToBL));
            angle = MathHelper.ToDegrees(angle);
            baseAngle += angle;
            topLeftAngle += angle;
            topRightAngle += angle;
            bottomRightAngle += angle;
            bottomLeftAngle += angle;
            angleBetweenTLandTR = MathHelper.ToDegrees(-(float)Math.Atan2(topRight.Y - topLeft.Y, topRight.X - topLeft.X));
            angleBetweenTLandBL = MathHelper.ToDegrees(-(float)Math.Atan2(bottomLeft.Y - topLeft.Y, bottomLeft.X - topLeft.X));

            angleBetweenBRandTR = MathHelper.ToDegrees(-(float)Math.Atan2(topRight.Y - bottomRight.Y, topRight.X - bottomRight.X));
            angleBetweenBRandBL = MathHelper.ToDegrees(-(float)Math.Atan2(bottomLeft.Y - bottomRight.Y, bottomLeft.X - bottomRight.X));
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            Game1.drawManager.DrawRectangle(spriteBatch, topLeft, topRight, bottomRight, bottomLeft, color, thickness);
        }

        public bool ccw(Vector2 A, Vector2 B, Vector2 C)
        {
            return (C.Y - A.Y) * (B.X - A.X) > (B.Y - A.Y) * (C.X - A.X);
        }



        public bool intersect(Vector2 A, Vector2 B, Vector2 C, Vector2 D)
        {
            return ccw(A, C, D) != ccw(B, C, D) && ccw(A, B, C) != ccw(A, B, D);
        }

        public bool checkCollision(RotatedRectangle rect1)
        {
            bool res;


            foreach ((Vector2, Vector2) corner in new List<(Vector2, Vector2)>() { (topLeft, topRight), (topRight, bottomRight), (bottomRight, bottomLeft), (bottomLeft, topLeft) })
            {
                foreach ((Vector2, Vector2) cornerRect1 in new List<(Vector2, Vector2)>() { (rect1.topLeft, rect1.topRight), (rect1.topRight, rect1.bottomRight), (rect1.bottomRight, rect1.bottomLeft), (rect1.bottomLeft, rect1.topLeft) })
                {
                    res = intersect(corner.Item1, corner.Item2, cornerRect1.Item1, cornerRect1.Item2);



                    if (res)
                    {
                        return true;
                    }

                }


            }
            if (rect1.width < width && rect1.height < width) //if rect1 can be contained within rect, then it could bypass the above checks, but in this case it'd always have its center within rect
            {
                checkCollision(rect1.center);
            }

            return false;
        }


        public bool old_checkCollision(RotatedRectangle rect1)
        {
            bool res1 = false;
            bool res2 = false;

            foreach (Vector2 corner in new List<Vector2>() { topLeft, topRight, bottomRight, bottomLeft })
            {

                float a1 = -(float)Math.Atan2(corner.Y - rect1.topLeft.Y, corner.X - rect1.topLeft.X) * 180 / (float)Math.PI;
                float a2 = -(float)Math.Atan2(corner.Y - rect1.bottomRight.Y, corner.X - rect1.bottomRight.X) * 180 / (float)Math.PI;

                rect1.angleBetweenTLandBL = (int)Math.Round(rect1.angleBetweenTLandBL);
                rect1.angleBetweenTLandTR = (int)Math.Round(rect1.angleBetweenTLandTR); ;
                rect1.angleBetweenBRandBL = (int)Math.Round(rect1.angleBetweenBRandBL); ;
                rect1.angleBetweenBRandTR = (int)Math.Round(rect1.angleBetweenBRandTR); ;


                res1 = (Math.Min(rect1.angleBetweenTLandBL, rect1.angleBetweenTLandTR) < a1 && a1 < Math.Max(rect1.angleBetweenTLandBL, rect1.angleBetweenTLandTR));
                res2 = (Math.Min(rect1.angleBetweenBRandBL, rect1.angleBetweenBRandTR) < a2 && a2 < Math.Max(rect1.angleBetweenBRandBL, rect1.angleBetweenBRandTR));
                //if both angles are on the left quadrants, there will be an issue when checking if the point is between the two (110° is between 100° and -170°, but not mathematically)
                if (
                    (Math.Abs((int)rect1.angleBetweenTLandBL) / 90 != 0 && Math.Abs((int)rect1.angleBetweenTLandTR) / 90 != 0)//this inverts the result in that case
                    && (!((int)rect1.angleBetweenTLandBL == -90 && (int)rect1.angleBetweenTLandTR == -180)//these are particular cases, where both angles are in the left quadrant, but the mathematical comparison works
                    && !((int)rect1.angleBetweenTLandBL == -180 && (int)rect1.angleBetweenTLandTR == -90)
                    && !((int)rect1.angleBetweenTLandBL == 180 && (int)rect1.angleBetweenTLandTR == 90)
                    && !((int)rect1.angleBetweenTLandBL == 90 && (int)rect1.angleBetweenTLandTR == 180)
                    )
                    ) { res1 = !res1; }

                if (
                    (Math.Abs((int)rect1.angleBetweenBRandBL) / 90 != 0 && Math.Abs((int)rect1.angleBetweenBRandTR) / 90 != 0)
                    && (!((int)rect1.angleBetweenBRandBL == -90 && (int)rect1.angleBetweenBRandTR == -180)
                    && !((int)rect1.angleBetweenBRandBL == -180 && (int)rect1.angleBetweenBRandTR == -90)
                    && !((int)rect1.angleBetweenBRandBL == 180 && (int)rect1.angleBetweenBRandTR == 90)
                    && !((int)rect1.angleBetweenBRandBL == 90 && (int)rect1.angleBetweenBRandTR == 180)
                    )
                    ) { res2 = !res2; }
                if (res1 && res2)
                {
                    color = Color.Red;
                    //Debug.WriteLine("Original is within rect1");
                    return true;
                }
                color = Color.Black;

            }

            foreach (Vector2 corner in new List<Vector2>() { rect1.topLeft, rect1.topRight, rect1.bottomRight, rect1.bottomLeft })
            {
                float a1 = -(float)Math.Atan2(corner.Y - topLeft.Y, corner.X - topLeft.X) * 180 / (float)Math.PI;//angle between point from rect1 and top left
                float a2 = -(float)Math.Atan2(corner.Y - bottomRight.Y, corner.X - bottomRight.X) * 180 / (float)Math.PI;//angle between point from rect1 and bottom right

                angleBetweenTLandBL = (int)Math.Round(angleBetweenTLandBL);
                angleBetweenTLandTR = (int)Math.Round(angleBetweenTLandTR); ;
                angleBetweenBRandBL = (int)Math.Round(angleBetweenBRandBL); ;
                angleBetweenBRandTR = (int)Math.Round(angleBetweenBRandTR); ;

                res1 = (Math.Min(angleBetweenTLandBL, angleBetweenTLandTR) < a1 && a1 < Math.Max(angleBetweenTLandBL, angleBetweenTLandTR));
                res2 = (Math.Min(angleBetweenBRandBL, angleBetweenBRandTR) < a2 && a2 < Math.Max(angleBetweenBRandBL, angleBetweenBRandTR));





                //if both angles are on the left quadrants, there will be an issue when checking if the point is between the two (110° is between 100° and -170°, but not mathematically)
                if (
                    (Math.Abs((int)angleBetweenTLandBL) / 90 != 0 && Math.Abs((int)angleBetweenTLandTR) / 90 != 0)//this inverts the result in that case
                    && (!((int)angleBetweenTLandBL == -90 && (int)angleBetweenTLandTR == -180)//these are particular cases, where both angles are in the left quadrant, but the mathematical comparison works
                    && !((int)angleBetweenTLandBL == -180 && (int)angleBetweenTLandTR == -90)
                    && !((int)angleBetweenTLandBL == 180 && (int)angleBetweenTLandTR == 90)
                    && !((int)angleBetweenTLandBL == 90 && (int)angleBetweenTLandTR == 180)
                    )
                    ) { res1 = !res1; }

                if (
                    (Math.Abs((int)angleBetweenBRandBL) / 90 != 0 && Math.Abs((int)angleBetweenBRandTR) / 90 != 0)
                    && (!((int)angleBetweenBRandBL == -90 && (int)angleBetweenBRandTR == -180)
                    && !((int)angleBetweenBRandBL == -180 && (int)angleBetweenBRandTR == -90)
                    && !((int)angleBetweenBRandBL == 180 && (int)angleBetweenBRandTR == 90)
                    && !((int)angleBetweenBRandBL == 90 && (int)angleBetweenBRandTR == 180)
                    )
                    ) { res2 = !res2; }


                if (res1 && res2)
                {
                    /*
                    Debug.WriteLine((

                        (Math.Abs((int)angleBetweenTLandBL) / 90 != 0 && Math.Abs((int)angleBetweenTLandTR) / 90 != 0)
                    , (!(Math.Abs((int)angleBetweenTLandBL) == -90 && Math.Abs((int)angleBetweenTLandTR) == -180)
                    , !(Math.Abs((int)angleBetweenTLandBL) == -180 && Math.Abs((int)angleBetweenTLandTR) == -90)
                    , !(Math.Abs((int)angleBetweenTLandBL) == 180 && Math.Abs((int)angleBetweenTLandTR) == 90)
                    , !(Math.Abs((int)angleBetweenTLandBL) == 90 && Math.Abs((int)angleBetweenTLandTR) == 180)
                    )


                        ));
                    Debug.WriteLine(

                        (Math.Abs((int)angleBetweenBRandBL) / 90 != 0 && Math.Abs((int)angleBetweenBRandTR) / 90 != 0)
                    && (!(Math.Abs((int)angleBetweenBRandBL) == -90 && Math.Abs((int)angleBetweenBRandTR) == -180)
                    && !(Math.Abs((int)angleBetweenBRandBL) == -180 && Math.Abs((int)angleBetweenBRandTR) == -90)
                    && !(Math.Abs((int)angleBetweenBRandBL) == 180 && Math.Abs((int)angleBetweenBRandTR) == 90)
                    && !(Math.Abs((int)angleBetweenBRandBL) == 90 && Math.Abs((int)angleBetweenBRandTR) == 180)
                    )
                    

                        );*/
                    //Debug.WriteLine((topLeft, topRight, bottomRight, bottomLeft));
                    //Debug.WriteLine((rect1.topLeft, rect1.topRight, rect1.bottomRight, rect1.bottomLeft));



                    if (res1 && res2)
                    {
                        color = Color.Red;
                        //Debug.WriteLine((a1, a2));
                        //Debug.WriteLine(((int)angleBetweenTLandBL, (int)angleBetweenTLandTR));
                        //Debug.WriteLine((angleBetweenBRandBL, angleBetweenBRandTR));
                        //Debug.WriteLine("rect1 is within Original");
                        return true;
                    }

                }
                color = Color.Black;

            }


            return false;

        }

        public bool checkCollision(Vector2 point)
        {
            bool res1 = false;
            bool res2 = false;


            float a1 = -(float)Math.Atan2(point.Y - topLeft.Y, point.X - topLeft.X) * 180 / (float)Math.PI;
            float a2 = -(float)Math.Atan2(point.Y - bottomRight.Y, point.X - bottomRight.X) * 180 / (float)Math.PI;

            res1 = (Math.Min(angleBetweenTLandBL, angleBetweenTLandTR) < a1 && a1 < Math.Max(angleBetweenTLandBL, angleBetweenTLandTR));
            res2 = (Math.Min(angleBetweenBRandBL, angleBetweenBRandTR) < a2 && a2 < Math.Max(angleBetweenBRandBL, angleBetweenBRandTR));
            if ((int)angleBetweenTLandBL / 90 != 0 && (int)angleBetweenTLandTR / 90 != 0) { res1 = !res1; }
            if ((int)angleBetweenBRandBL / 90 != 0 && (int)angleBetweenBRandTR / 90 != 0) { res2 = !res2; }


            if (res1 && res2)
            {
                return true;
            }

            return false;

        }

    }
}
