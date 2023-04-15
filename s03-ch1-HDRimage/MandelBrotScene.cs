using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Util;

namespace rt004
{
  internal static class MandelBrotScene
  {
    public static FloatImage GenerateImage(FloatImage image, double xMin, double yMin, double xMax, double yMax, int iterLimit)
    {
      for (int py = 0; py < image.Height; py++)
      {
        for (int px = 0; px < image.Width; px++)
        {
          double x = xMin + ( ( xMax - xMin ) * px ) / ( image.Height - 1 );
          double y = yMin + ( ( yMax - yMin ) * py ) / ( image.Width - 1 );

          double a = x, b = y;
          int iter = 0;
          do
          {
            (a, b) = (a * a - b * b + x, 2 * a * b + y);
            iter++;
          } while (iter <= iterLimit && a * a + b * b < 4);
          if (iter > iterLimit)
          {
            image.PutPixel(px, py, Color.GetBlack());
          }
          else
          {
            float[] color;
            if(iter % 2 == 0)
            {
              color = new float[] { 252, 243, 0 };
            }
            else
            {
              color = new float[] { 252, 0, 0 };
            }
            image.PutPixel(px, py, color);
          }
        }
      }
      return image;
    }
  }
}
