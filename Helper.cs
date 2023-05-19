using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace Repository_Teme_Geometrie_Computationala
{
    public class Helper
    {

        #region Declarari
        Random random = new Random();
        DispatcherTimer timer = new DispatcherTimer();
        BaseWeek baseWeek;
        int indexPunct = 0;
        Point[] puncte;
        #endregion

        public Helper(BaseWeek baseWeek)
        {
            this.baseWeek = baseWeek;
            timer.Interval = TimeSpan.FromMilliseconds(70);
            timer.Tick += timer_Tick;
        }


        void timer_Tick(object sender, EventArgs e)
        {
            if (indexPunct < puncte.Length - 1)
            {
                DesenareLinie(puncte[indexPunct], puncte[indexPunct + 1], new Pen(Brushes.Red,4));
                indexPunct++;
            }
            else
            {
                DesenareLinie(puncte[0], puncte[indexPunct], new Pen(Brushes.Red, 4));
                indexPunct = 0;
                timer.Stop();
            }
        }


        #region Generare_Puncte

        public Point[] GenerarePuncteAleatorii(int numarPuncte)
        {
            Random random = new Random();
            Point[] puncte = new Point[numarPuncte];
            double x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                
                puncte[i] = GenerarePunctAleatoriu();
            }
            return puncte;
        }
        public Point[] GenerarePuncteAleatoriiConstrained(int numarPuncte, int startingWidth,int maxWidth,int startingHeight,int maxHeight)
        {
            Point[] puncte = new Point[numarPuncte];
            for (int i = 0; i < numarPuncte; i++)
            {
                int x = random.Next(startingWidth,maxWidth);
                int y = random.Next(startingHeight,maxHeight);
                puncte[i] = new Point(x,y);
            }
            return puncte;
        }
        public Point GenerarePunctAleatoriu()
        {
            Random random = new Random();
            int x, y;
            x = random.Next(0, (int)baseWeek.mainWindow.canvas.ActualWidth - 10);
            y = random.Next(0, (int)baseWeek.mainWindow.canvas.ActualHeight - 10);
            Point punct = new Point(x, y);

            return punct;
        }

        #endregion

        #region Math_Region
        public double DistantaIntreDouaPuncte(double x1, double y1, double x2, double y2)
        {
            double distanta = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distanta;
        }
        public double DistantaIntreDouaPuncte(Point p1, Point p2)
        {
            double distanta = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            return distanta;
        }
        public double AriaUnuiTriunghi(Point p1, Point p2, Point p3)
        {
            double aria;
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double semiperimetru = (a + b + c) / 2;
            aria = Math.Sqrt(semiperimetru * (semiperimetru - a) * (semiperimetru - b) * (semiperimetru - c));
            return aria;
        }
        public double PerimetrulUnuiTriunghi(Point p1, Point p2, Point p3)
        {
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double perimetru = (a + b + c);
            return perimetru;
        }
        public void SortarePuncte(Point[] puncte)
        {
            bool ok = true;
            Point aux;
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
        private static PointCustom[] TransformPointsToHelperPoints(Point[] points)
        {
            PointCustom[] helperPoints = new PointCustom[points.Length];
            for(int i= 0;i<points.Length;i++)
            {
                helperPoints[i] = new PointCustom(points[i].X, points[i].Y);
            }
            return helperPoints;
        }
        public static Circle MakeCircle(Point[] inputPoints)
        {

            PointCustom[] points = TransformPointsToHelperPoints(inputPoints);

            // Clone list to preserve the caller's data, do Durstenfeld shuffle
            List<PointCustom> shuffled = new List<PointCustom>(points);
            Random rand = new Random();
            for (int i = shuffled.Count - 1; i > 0; i--)
            {
                int j = rand.Next(i + 1);
                PointCustom temp = shuffled[i];
                shuffled[i] = shuffled[j];
                shuffled[j] = temp;
            }

            // Progressively add points to circle or recompute circle
            Circle c = Circle.INVALID;
            for (int i = 0; i < shuffled.Count; i++)
            {
                PointCustom p = shuffled[i];
                if (c.radius < 0 || !c.Contains(p))
                    c = MakeCircleOnePoint(shuffled.GetRange(0, i + 1), p);
            }
            return c;
        }
        private static Circle MakeCircleOnePoint(List<PointCustom> points, PointCustom p)
        {
            Circle c = new Circle(p, 0);
            for (int i = 0; i < points.Count; i++)
            {
                PointCustom q = points[i];
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

        private static Circle MakeCircleTwoPoints(List<PointCustom> points, PointCustom p, PointCustom q)
        {
            Circle circ = MakeDiameter(p, q);
            Circle left = Circle.INVALID;
            Circle right = Circle.INVALID;

            // For each point not in the two-point circle
            PointCustom pq = q.Subtract(p);
            foreach (PointCustom r in points)
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


        public static Circle MakeDiameter(PointCustom a, PointCustom b)
        {
            PointCustom c = new PointCustom((a.x + b.x) / 2, (a.y + b.y) / 2);
            return new Circle(c, Math.Max(c.Distance(a), c.Distance(b)));
        }


        public static Circle MakeCircumcircle(PointCustom a, PointCustom b, PointCustom c)
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
            PointCustom p = new PointCustom(ox + x, oy + y);
            double r = Math.Max(Math.Max(p.Distance(a), p.Distance(b)), p.Distance(c));
            return new Circle(p, r);
        }

        #endregion



        #endregion

        #region Desenare
        public void DesenareLiniiIntrePuncte(Point[] puncte)
        {
            this.puncte = puncte;
            timer.Start();
        }
        public void DesenarePunctPeFormular(Point punct, Pen pen)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = pen.Thickness,
                Width = pen.Thickness,
                Fill = pen.Brush,
                StrokeThickness = 3,
                Stroke = pen.Brush,
            };
            Canvas.SetLeft(ellipse, punct.X-pen.Thickness /2);
            Canvas.SetTop(ellipse, punct.Y-pen.Thickness /2);
            baseWeek.mainWindow.canvas.Children.Add(ellipse);
        }
        public void DesenarePunctePeFormular(Point[] puncte, Pen pen)
        {
            foreach (Point punct in puncte)
            {
                DesenarePunctPeFormular(punct, pen);
            }
        }

        public void DesenareLinie(Point a, Point b,Pen penLinie)
        {
            Line line = new Line()
            {
                Fill = Brushes.Black,
                StrokeThickness = 2,
                Stroke = Brushes.Black,
            };
            line.X1 = a.X;
            line.Y1 = a.Y;
            line.X2 = b.X;
            line.Y2 = b.Y;
            baseWeek.mainWindow.canvas.Children.Add(line);
        }
        public void DesenareTriunghi(Point[] puncte, Pen penLinie) 
        {
            DesenareLinie(puncte[0], puncte[1],penLinie);
            DesenareLinie(puncte[1], puncte[2], penLinie);
            DesenareLinie(puncte[0], puncte[2], penLinie);
        }

        public void DesenareDreptunghi(double x1, double y1, double x2, double y2)
        {
            double latimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x2, y1);
            double inaltimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x1, y2);

            Rectangle dreptunghi = new Rectangle()
            {
                Width = latimeDreptunghi,
                Height = inaltimeDreptunghi,
                StrokeThickness = 3,
                Stroke = Brushes.Red,
            };
            Canvas.SetLeft(dreptunghi, x1);
            Canvas.SetTop(dreptunghi, y1);
            baseWeek.mainWindow.canvas.Children.Add(dreptunghi);
        }
        public void DesenareCerc(Point point, double raza, Pen pen)
        {
            Ellipse ellipse = new Ellipse()
            {
                Height = raza * 2,
                Width = raza * 2,
                StrokeThickness = 3,
                Stroke = pen.Brush,
            };
            Canvas.SetLeft(ellipse, point.X - raza);
            Canvas.SetTop(ellipse, point.Y - raza);

            baseWeek.mainWindow.canvas.Children.Add(ellipse);
        }
        #endregion

        #region Structuri_Custom
        public struct Circle
        { 
            public static readonly Circle INVALID = new Circle(new PointCustom(0, 0), -1);
            private const double MULTIPLICATIVE_EPSILON = 1 + 1e-14;
            public PointCustom center;   // Center
            public double radius;  // Radius
            public Circle(PointCustom center, double radius)
            {
                this.center = center;
                this.radius = radius;
            }
            public bool Contains(PointCustom p)
            {
                return center.Distance(p) <= radius * MULTIPLICATIVE_EPSILON;
            }
            public bool Contains(ICollection<PointCustom> ps)
            {
                foreach (PointCustom p in ps)
                {
                    if (!Contains(p))
                        return false;
                }
                return true;
            }
        }

        public struct PointCustom
        {

            public double x;
            public double y;
            public PointCustom(double x, double y)
            {
                this.x = x;
                this.y = y;
            }
            public PointCustom Subtract(PointCustom p)
            {
                return new PointCustom(x - p.x, y - p.y);
            }
            public double Distance(PointCustom p)
            {
                double dx = x - p.x;
                double dy = y - p.y;
                return Math.Sqrt(dx * dx + dy * dy);
            }


            // Signed area / determinant thing
            public double Cross(PointCustom p)
            {
                return x * p.y - y * p.x;
            }

        }

        public struct Segment
        {
            public PointEvent a;
            public PointEvent b;
            public Segment(PointEvent a, PointEvent b)
            {
                this.a = a;
                this.b = b;
            }

        }
        public struct PointEvent
        {
            public Point point;
            public List<Segment> SegmentsForWhichPointIsLower;
            public List<Segment> SegmentsForWhichPointIsUpper;
            public List<Segment> SegmentsForWhichPointIsInterior;
            public PointEvent(Point point)
            {
                this.point = point;
            }
            public bool isBegin(PointEvent other)
            {
                if (this.point.Y < other.point.Y)
                    return true;
                else if(this.point.Y ==other.point.Y &&this.point.X<other.point.X) return true;
                return false;
            }
        }
        public enum Directie { Stanga, Linie, Dreapta }

        public Directie GetDirection(Point a, Point b, Point c)
        {
            double aux1 = a.X * b.Y + b.X * c.Y + a.Y * c.X;
            double aux2 = c.X * b.Y + c.Y * a.X + b.X * a.Y;

            double rezultat = aux1 - aux2;
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
    }
}
