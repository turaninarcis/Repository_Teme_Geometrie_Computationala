using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Repository_Teme_Geometrie_Computationala
{
    public class Helper
    {
        Bitmap BitmapToBeUsed;
        public Helper(Bitmap bitMapToBeUsed)
        {
            BitmapToBeUsed = bitMapToBeUsed;
        }

        public Point[] GenerarePuncteAleatorii(int numarPuncte)
        {
            Random random = new Random();
            Point[] puncte = new Point[numarPuncte];
            int x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(0, BitmapToBeUsed.Width - 10);
                y = random.Next(0, BitmapToBeUsed.Height - 10);
                puncte[i] = new Point(x, y);
            }
            return puncte;
        }

        public void DesenarePunctePeFormular(Point[] puncte, Graphics graphics, System.Drawing.Pen pen, int marimePunct)
        {
            foreach (Point punct in puncte)
            {
                graphics.DrawEllipse(pen, (float)(punct.X - marimePunct / 2), (float)(punct.Y - marimePunct / 2), marimePunct, marimePunct);
            }
        }

        public Rectangle FormareDreptunghiCuDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double latimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x2, y1);
            double inaltimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x1, y2);
            Rectangle dreptunghi = new Rectangle(x1, y1, (int)latimeDreptunghi, (int)inaltimeDreptunghi);
            return dreptunghi;
        }

        public double DistantaIntreDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double distanta = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distanta;
        }
        public double DistantaIntreDouaPuncte(Point p1, Point p2)
        {
            double distanta = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            return distanta;
        }
    }
}
