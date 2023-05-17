using System;
using System.Collections.Generic;
using System.Drawing;
using Drawing = System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala
{
    public class Helper
    {

        #region Declarari
        Bitmap BitmapToBeUsed;
        Graphics graphics;
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        BaseWeek baseWeek;
        int indexPunct = 0;
        Drawing.Point[] puncte;
        #endregion

        public Helper(BaseWeek baseWeek)
        {
            this.baseWeek = baseWeek;
            BitmapToBeUsed = baseWeek.bitmap;
            this.graphics = baseWeek.graphics;
            timer.Interval = TimeSpan.FromMilliseconds(70);
            timer.Tick += timer_Tick;
        }


        void timer_Tick(object sender, EventArgs e)
        {
            if (indexPunct < puncte.Length - 1)
            {
                graphics.DrawLine(new Drawing.Pen(Drawing.Color.Black, 2), puncte[indexPunct], puncte[indexPunct + 1]);
                indexPunct++;
            }
            else
            {
                graphics.DrawLine(new Drawing.Pen(Drawing.Color.Black, 2), puncte[0], puncte[puncte.Length - 1]);
                indexPunct = 0;
                timer.Stop();
            }
            baseWeek.AssignImage();
        }


        #region Generare_Puncte

        public Drawing.Point[] GenerarePuncteAleatorii(int numarPuncte)
        {
            Random random = new Random();
            Drawing.Point[] puncte = new Drawing.Point[numarPuncte];
            int x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(0, BitmapToBeUsed.Width - 10);
                y = random.Next(0, BitmapToBeUsed.Height - 10);
                puncte[i] = new Drawing.Point(x, y);
            }
            return puncte;
        }
        public Drawing.Point[] GenerarePuncteAleatoriiConstrained(int numarPuncte, int startingWidth,int maxWidth,int startingHeight,int maxHeight)
        {
            Random random = new Random();
            Drawing.Point[] puncte = new Drawing.Point[numarPuncte];
            int x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(startingWidth, maxWidth);
                y = random.Next(startingHeight, maxHeight);
                puncte[i] = new Drawing.Point(x, y);
            }
            return puncte;
        }
        public Drawing.Point GenerarePunctAleatoriu()
        {
            int indentation = 10;
            int x = random.Next(0, BitmapToBeUsed.Width - indentation);
            int y = random.Next(0, BitmapToBeUsed.Height - indentation);
            Drawing.Point punct = new Drawing.Point(x, y);

            return punct;
        }

        #endregion

        #region Math_Region
        public double DistantaIntreDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double distanta = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distanta;
        }
        public double DistantaIntreDouaPuncte(Drawing.Point p1, Drawing.Point p2)
        {
            double distanta = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            return distanta;
        }
        public double AriaUnuiTriunghi(Drawing.Point p1, Drawing.Point p2, Drawing.Point p3)
        {
            double aria;
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double semiperimetru = (a + b + c) / 2;
            aria = Math.Sqrt(semiperimetru * (semiperimetru - a) * (semiperimetru - b) * (semiperimetru - c));
            return aria;
        }
        public double PerimetrulUnuiTriunghi(Drawing.Point p1, Drawing.Point p2, Drawing.Point p3)
        {
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double perimetru = (a + b + c);
            return perimetru;
        }
        public void SortarePuncte(Drawing.Point[] puncte)
        {
            bool ok = true;
            Drawing.Point aux;
            do
            {
                ok = true;
                for (int i = 0; i < puncte.Length - 1; i++)
                {
                    if (puncte[i].X > puncte[i + 1].X)
                    {
                        ok = false;
                        aux = puncte[i];
                        puncte[i] = puncte[i + 1];
                        puncte[i + 1] = aux;
                    }
                    else if (puncte[i].X == puncte[i + 1].X && puncte[i].Y < puncte[i + 1].Y)
                    {
                        ok = false;
                        aux = puncte[i];
                        puncte[i] = puncte[i + 1];
                        puncte[i + 1] = aux;
                    }
                }
            } while (ok == false);
        }

        public static void Swap<T>(ref T a, ref T b)
        {
            T temp = a;
            a = b;
            b = temp;    
        }

        #region MinimumEnclosingCircle
        private static Point[] TransformPointsToHelperPoints(Drawing.Point[] points)
        {
            Point[] helperPoints = new Point[points.Length];
            for(int i= 0;i<points.Length;i++)
            {
                helperPoints[i] = new Point(points[i].X, points[i].Y);
            }
            return helperPoints;
        }
        public static Circle MakeCircle(Drawing.Point[] inputPoints)
        {

            Point[] points = TransformPointsToHelperPoints(inputPoints);

            // Clone list to preserve the caller's data, do Durstenfeld shuffle
            List<Point> shuffled = new List<Point>(points);
            Random rand = new Random();
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                Point temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            // Progressively add points to circle or recompute circle
            Circle c = Circle.INVALID;
            for (int i = 0; i < shuffled.Count; i++)
            {
                Point p = shuffled[i];
                if (c.radius < 0 || !c.Contains(p))
                    c = MakeCircleOnePoint(shuffled.GetRange(0, i + 1), p);
            }
            return c;
        }
        private static Circle MakeCircleOnePoint(List<Point> points, Point p)
        {
            Circle c = new Circle(p, 0);
            for (int i = 0; i < points.Count; i++)
            {
                Point q = points[i];
                if (!c.Contains(q))
                {
                    if (c.radius == 0)
                        c = MakeDiameter(p, q);
                    else
                        c = MakeCircleTwoPoints(points.GetRange(0, i + 1), p, q);
                }
            }
            return c;
        }

        private static Circle MakeCircleTwoPoints(List<Point> points, Point p, Point q)
        {
            Circle circ = MakeDiameter(p, q);
            Circle left = Circle.INVALID;
            Circle right = Circle.INVALID;

            // For each point not in the two-point circle
            Point pq = q.Subtract(p);
            foreach (Point r in points)
            {
                if (circ.Contains(r))
                    continue;

                // Form a circumcircle and classify it on left or right side
                double cross = pq.Cross(r.Subtract(p));
                Circle c = MakeCircumcircle(p, q, r);
                if (c.radius < 0)
                    continue;
                else if (cross > 0 && (left.radius < 0 || pq.Cross(c.center.Subtract(p)) > pq.Cross(left.center.Subtract(p))))
                    left = c;
                else if (cross < 0 && (right.radius < 0 || pq.Cross(c.center.Subtract(p)) < pq.Cross(right.center.Subtract(p))))
                    right = c;
            }

            // Select which circle to return
            if (left.radius < 0 && right.radius < 0)
                return circ;
            else if (left.radius < 0)
                return right;
            else if (right.radius < 0)
                return left;
            else
                return left.radius <= right.radius ? left : right;
        }


        public static Circle MakeDiameter(Point a, Point b)
        {
            Point c = new Point((a.x + b.x) / 2, (a.y + b.y) / 2);
            return new Circle(c, Math.Max(c.Distance(a), c.Distance(b)));
        }


        public static Circle MakeCircumcircle(Point a, Point b, Point c)
        {
            // Mathematical algorithm from Wikipedia: Circumscribed circle
            double ox = (Math.Min(Math.Min(a.x, b.x), c.x) + Math.Max(Math.Max(a.x, b.x), c.x)) / 2;
            double oy = (Math.Min(Math.Min(a.y, b.y), c.y) + Math.Max(Math.Max(a.y, b.y), c.y)) / 2;
            double ax = a.x - ox, ay = a.y - oy;
            double bx = b.x - ox, by = b.y - oy;
            double cx = c.x - ox, cy = c.y - oy;
            double d = (ax * (by - cy) + bx * (cy - ay) + cx * (ay - by)) * 2;
            if (d == 0)
                return Circle.INVALID;
            double x = ((ax * ax + ay * ay) * (by - cy) + (bx * bx + by * by) * (cy - ay) + (cx * cx + cy * cy) * (ay - by)) / d;
            double y = ((ax * ax + ay * ay) * (cx - bx) + (bx * bx + by * by) * (ax - cx) + (cx * cx + cy * cy) * (bx - ax)) / d;
            Point p = new Point(ox + x, oy + y);
            double r = Math.Max(Math.Max(p.Distance(a), p.Distance(b)), p.Distance(c));
            return new Circle(p, r);
        }

        #endregion
        public Drawing.Rectangle FormareDreptunghiCuDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double latimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x2, y1);
            double inaltimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x1, y2);
            Rectangle dreptunghi = new Rectangle(x1, y1, (int)latimeDreptunghi, (int)inaltimeDreptunghi);
            return dreptunghi;
        }
        public Directie GetDirection(Drawing.Point a, Drawing.Point b, Drawing.Point c)
        {
            int aux1 = a.X * b.Y + b.X * c.Y + a.Y * c.X;
            int aux2 = c.X * b.Y + c.Y * a.X + b.X * a.Y;
            int rezultat = aux1 - aux2;
            if (rezultat < 0)
            {
                return Directie.Stanga;
            }
            else if (rezultat > 0)
            {
                return Directie.Dreapta;
            }
            else return Directie.Linie;
        }

        #endregion

        #region Desenare
        public void DesenareLiniiIntrePuncte(Drawing.Point[] puncte)
        {
            this.puncte = puncte;
            timer.Start();
        }
        public void DesenarePunctPeFormular(Drawing.Point punct, Drawing.Pen pen, int marimePunct)
        {
            graphics.DrawEllipse(pen, (float)(punct.X - marimePunct / 2), (float)(punct.Y - marimePunct / 2), marimePunct, marimePunct);

        }
        public void DesenarePunctePeFormular(Drawing.Point[] puncte, System.Drawing.Pen pen, int marimePunct)
        {
            foreach (Drawing.Point punct in puncte)
            {
                graphics.DrawEllipse(pen, (float)(punct.X - marimePunct / 2), (float)(punct.Y - marimePunct / 2), marimePunct, marimePunct);
            }
        }
        public void DesenareTriunghi(Drawing.Point[] puncte, Drawing.Pen penLinie) 
        {
            graphics.DrawLine(penLinie, puncte[0], puncte[1]);
            graphics.DrawLine(penLinie, puncte[1], puncte[2]);
            graphics.DrawLine(penLinie, puncte[2], puncte[0]);
        }
        public void DesenareNumarLangaPuncte(Drawing.Point[] puncte)
        {
            for (int i = 0; i < puncte.Length; i++)
            {
                graphics.DrawString((i + 1).ToString(), new Font("Arial", 10), Drawing.Brushes.Black, puncte[i]); ;
            }
        }

        #endregion

        #region Structuri_Custom
        public struct Circle
        { 
            public static readonly Circle INVALID = new Circle(new Point(0, 0), -1);
            private const double MULTIPLICATIVE_EPSILON = 1 + 1e-14;
            public Point center;   // Center
            public double radius;  // Radius
            public Circle(Point center, double radius)
            {
                this.center = center;
                this.radius = radius;
            }
            public bool Contains(Point p)
            {
                return center.Distance(p) <= radius * MULTIPLICATIVE_EPSILON;
            }
            public bool Contains(ICollection<Point> ps)
            {
                foreach (Point p in ps)
                {
                    if (!Contains(p))
                        return false;
                }
                return true;
            }
        }

        public struct Point
        {

            public double x;
            public double y;
            public Point(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public Point Subtract(Point p)
            {
                return new Point(x - p.x, y - p.y);
            }
            public double Distance(Point p)
            {
                double dx = x - p.x;
                double dy = y - p.y;
                return Math.Sqrt(dx * dx + dy * dy);
            }


            // Signed area / determinant thing
            public double Cross(Point p)
            {
                return x * p.y - y * p.x;
            }

        }

        public enum Directie { Stanga, Linie, Dreapta }
        #endregion
    }
}
