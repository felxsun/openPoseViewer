using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Security;

namespace openposeImageKey
{
    public static class imageLabeler
    {
        public static Image labelBody25(Image im, openposeKeyPoint[] points)
        {
            if (im == null)
                return null;

            Graphics g = Graphics.FromImage(im);
            if (points == null || points.Length != 25)
                throw new Exception("Invalid openpose body 25 key point format");

            float pointR = 3F;
            float lineW = 2F;
            float pThr = 0.05F;

            SolidBrush b;
            Pen p;

            //right face
            b = new SolidBrush(Color.MediumVioletRed);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[0], points[15], pThr);
            drawConnect(g, p, points[17], points[15], pThr);
            drawPoint(g, b, pointR, points[15], pThr);
            drawPoint(g, b, pointR, points[17], pThr);
            
            //left face
            b = new SolidBrush(Color.Blue);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[0], points[16], pThr);
            drawConnect(g, p, points[16], points[18], pThr);
            drawPoint(g, b, pointR, points[16], pThr);
            drawPoint(g, b, pointR, points[18], pThr);

            b = new SolidBrush(Color.Green);
            p = new Pen(b, lineW);
            //draw neck
            drawConnect(g, p, points[0], points[1], pThr);
            //draw nose
            drawPoint(g, b, pointR, points[0], pThr);

            //Right shoulder / hand
            b = new SolidBrush(Color.Sienna);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[1], points[2], pThr);
            drawConnect(g, p, points[2], points[3], pThr);
            drawConnect(g, p, points[3], points[4], pThr);
            drawPoint(g, b, pointR, points[2], pThr);
            drawPoint(g, b, pointR, points[3], pThr);
            drawPoint(g, b, pointR, points[4], pThr);

            //Left shoulder / hand
            b = new SolidBrush(Color.Indigo);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[1], points[5], pThr);
            drawConnect(g, p, points[5], points[6], pThr);
            drawConnect(g, p, points[6], points[7], pThr);
            drawPoint(g, b, pointR, points[5], pThr);
            drawPoint(g, b, pointR, points[6], pThr);
            drawPoint(g, b, pointR, points[7], pThr);


            

            //right butt
            b = new SolidBrush(Color.Navy);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[8], points[9], pThr);

            //left butt
            b = new SolidBrush(Color.DarkRed);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[8], points[12], pThr);

            //center body
            b = new SolidBrush(Color.DarkGreen);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[1], points[8], pThr);
            drawPoint(g, b, pointR, points[1], pThr);
            drawPoint(g, b, pointR, points[8], pThr);


            //right foot
            b = new SolidBrush(Color.DarkOrchid);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[11], points[24], pThr);
            drawPoint(g, b, pointR, points[24], pThr);
            b = new SolidBrush(Color.DarkOliveGreen);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[11], points[22], pThr);
            drawConnect(g, p, points[22], points[23], pThr);
            drawPoint(g, b, pointR, points[22], pThr);
            drawPoint(g, b, pointR, points[23], pThr);

            //left foot
            b = new SolidBrush(Color.Teal);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[14], points[21], pThr);
            drawPoint(g, b, pointR, points[21], pThr);
            b = new SolidBrush(Color.Maroon);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[14], points[19], pThr);
            drawConnect(g, p, points[19], points[20], pThr);
            drawPoint(g, b, pointR, points[19], pThr);
            drawPoint(g, b, pointR, points[20], pThr);

            //right leg
            b = new SolidBrush(Color.Purple);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[9], points[10], pThr);
            drawConnect(g, p, points[10], points[11], pThr);
            drawPoint(g, b, pointR, points[9], pThr);
            drawPoint(g, b, pointR, points[10], pThr);
            drawPoint(g, b, pointR, points[11], pThr);

            //left leg
            b = new SolidBrush(Color.DarkSlateGray);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[12], points[13], pThr);
            drawConnect(g, p, points[13], points[14], pThr);
            drawPoint(g, b, pointR, points[12], pThr);
            drawPoint(g, b, pointR, points[13], pThr);
            drawPoint(g, b, pointR, points[14], pThr);


            return im;
        }

        public static Image labelCoco(Image im, openposeKeyPoint[] points)
        {
            if (im == null)
                return null;

            Graphics g = Graphics.FromImage(im);
            if (points == null || points.Length != 18)
                throw new Exception("Invalid openpose body 25 key point format");

            float pointR = 3F;
            float lineW = 2F;
            float pThr = 0.05F;

            SolidBrush b;
            Pen p;

            Size fSize = im.Size;

            //right face
            b = new SolidBrush(Color.MediumVioletRed);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[0], points[14], pThr,fSize.Width,fSize.Height);
            drawConnect(g, p, points[14], points[16], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[14], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[16], pThr, fSize.Width, fSize.Height);

            //left face
            b = new SolidBrush(Color.Blue);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[0], points[15], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[15], points[17], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[15], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[17], pThr, fSize.Width, fSize.Height);

            b = new SolidBrush(Color.Green);
            p = new Pen(b, lineW);
            //draw neck
            drawConnect(g, p, points[0], points[1], pThr, fSize.Width, fSize.Height);
            //draw nose
            drawPoint(g, b, pointR, points[0], pThr, fSize.Width, fSize.Height);

            //Right shoulder / hand
            b = new SolidBrush(Color.Sienna);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[1], points[2], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[2], points[3], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[3], points[4], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[2], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[3], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[4], pThr, fSize.Width, fSize.Height);

            //Left shoulder / hand
            b = new SolidBrush(Color.Indigo);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[1], points[5], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[5], points[6], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[6], points[7], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[5], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[6], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[7], pThr, fSize.Width, fSize.Height);

            //right leg
            b = new SolidBrush(Color.Purple);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[8], points[9], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[9], points[10], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[9], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[10], pThr, fSize.Width, fSize.Height);

            //left leg
            b = new SolidBrush(Color.DarkSlateGray);
            p = new Pen(b, lineW);
            drawConnect(g, p, points[11], points[12], pThr, fSize.Width, fSize.Height);
            drawConnect(g, p, points[12], points[13], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[12], pThr, fSize.Width, fSize.Height);
            drawPoint(g, b, pointR, points[13], pThr, fSize.Width, fSize.Height);

            

            //right body
            SolidBrush rbb = new SolidBrush(Color.DarkRed);
            Pen rbp = new Pen(b, lineW);
            drawConnect(g, rbp, points[1], points[11], pThr, fSize.Width, fSize.Height);

            //left body
            SolidBrush lbb = new SolidBrush(Color.DarkGreen);
            Pen lbp = new Pen(b, lineW);
            drawConnect(g, lbp, points[1], points[8], pThr, fSize.Width, fSize.Height);


            //right butt
            b = new SolidBrush(Color.Navy);
            Pen btp = new Pen(b, lineW);
            drawConnect(g, btp, points[8], points[9], pThr, fSize.Width, fSize.Height);

            drawPoint(g, b, pointR, points[1], pThr, fSize.Width, fSize.Height);
            drawPoint(g, lbb, pointR, points[11], pThr, fSize.Width, fSize.Height);
            drawPoint(g, rbb, pointR, points[8], pThr, fSize.Width, fSize.Height);

            return im;
        }
        private static void drawConnect(Graphics gp, Pen p, openposeKeyPoint pt1, openposeKeyPoint pt2, float thr=0.05F, float wd=float.NaN, float ht=float.NaN)
        {
            if (pt1.pValue > thr && pt2.pValue > thr)
            {
                if (float.IsNaN(wd))
                {
                    gp.DrawLine(p, pt1.x, pt1.y, pt2.x, pt2.y);
                }
                else
                    gp.DrawLine(p, pt1.x * wd, pt1.y * ht, pt2.x * wd, pt2.y * ht);
            }
                
        }

        //draw a point circle
        private static void drawPoint(Graphics gp, Brush bs, float radius, openposeKeyPoint pt, float thr=0.05F, float wd = float.NaN, float ht = float.NaN)
        {
            if(pt.pValue>thr)
            {
                if (float.IsNaN(wd))
                {
                    gp.FillEllipse(bs, pt.x - radius, pt.y - radius, radius + radius, radius + radius);
                }
                else
                    gp.FillEllipse(bs, (pt.x * wd) - radius, (pt.y * ht) - radius, radius + radius, radius + radius);
            }
                
        }
    }
}
