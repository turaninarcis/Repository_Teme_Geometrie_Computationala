using System;
using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week3:BaseWeek
    {
        public Week3(MainWindow mainWindow):base(mainWindow) 
        {
            ProblemMethodsList.Add(AssignProblem1);
            ProblemMethodsList.Add(AssignProblem2);
        }


        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            Exercitiu_1(20);
        }


        public void Exercitiu_1(int nrSegmente)
        {
            List<Point> points = new List<Point>();
            points = helper.GenerarePuncteAleatoriiList(2 * nrSegmente);
            helper.DesenarePunctePeFormular(points.ToArray(), new Pen(Brushes.Black, 7));
            Point[] sortedXPoints = helper.CopierePuncte(points.ToArray());
            Helper.SortarePuncteX(sortedXPoints);
            List<Pair> minimalPairing = PointPairing.GetMinimalPairing(points);
            foreach (Pair pair in minimalPairing)
            {
                helper.DesenareLinie(pair.Point1, pair.Point2, new Pen(Brushes.Red, 2));
            }
        }

            public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            Exercitiu_2(10);

        }
        public void Exercitiu_2(int nrLinii)
        {
            List<LineSegment> lines = new List<LineSegment>();
            for(int i = 0;i< nrLinii;i++) 
            {
                Point p1= helper.GenerarePunctAleatoriu();
                Point p2 = helper.GenerarePunctAleatoriu();
                lines.Add(new LineSegment(p1, p2));
                helper.DesenareLinie(p2, p1,new Pen(Brushes.Black,2));
            }
            List<Point> intersections = IntersectionFinder.FindIntersections(lines);
            foreach(Point p in intersections)
            {
                helper.DesenarePunctPeFormular(p,new Pen(Brushes.Red, 7));
            }
        }






        public class LineSegment
        {
            public Point Start { get; }
            public Point End { get; }

            public LineSegment(Point start, Point end)
            {
                Start = start;
                End = end;
            }

            public bool Intersects(LineSegment other)
            {
                return DoIntersect(Start, End, other.Start, other.End);
            }

            private static bool OnSegment(Point p, Point q, Point r)
            {
                return q.X <= Math.Max(p.X, r.X) &&
                       q.X >= Math.Min(p.X, r.X) &&
                       q.Y <= Math.Max(p.Y, r.Y) &&
                       q.Y >= Math.Min(p.Y, r.Y);
            }

            private static int Orientation(Point p, Point q, Point r)
            {
                double val = (q.Y - p.Y) * (r.X - q.X) - (q.X - p.X) * (r.Y - q.Y);

                if (Math.Abs(val) < double.Epsilon)
                    return 0;  // Collinear

                return (val > 0) ? 1 : 2; // Clockwise or Counterclockwise
            }

            private static bool DoIntersect(Point p1, Point q1, Point p2, Point q2)
            {
                int o1 = Orientation(p1, q1, p2);
                int o2 = Orientation(p1, q1, q2);
                int o3 = Orientation(p2, q2, p1);
                int o4 = Orientation(p2, q2, q1);

                if (o1 != o2 && o3 != o4)
                    return true;

                if (o1 == 0 && OnSegment(p1, p2, q1))
                    return true;

                if (o2 == 0 && OnSegment(p1, q2, q1))
                    return true;

                if (o3 == 0 && OnSegment(p2, p1, q2))
                    return true;

                if (o4 == 0 && OnSegment(p2, q1, q2))
                    return true;

                return false;
            }
        }

        public class IntersectionFinder
        {
            public static List<Point> FindIntersections(List<LineSegment> segments)
            {
                List<Point> intersections = new List<Point>();

                for (int i = 0; i < segments.Count - 1; i++)
                {
                    for (int j = i + 1; j < segments.Count; j++)
                    {
                        LineSegment segment1 = segments[i];
                        LineSegment segment2 = segments[j];

                        if (segment1.Intersects(segment2))
                        {
                            Point intersection = GetIntersectionPoint(segment1, segment2);
                            intersections.Add(intersection);
                        }
                    }
                }

                return intersections;
            }

            private static Point GetIntersectionPoint(LineSegment segment1, LineSegment segment2)
            {
                double x1 = segment1.Start.X;
                double y1 = segment1.Start.Y;
                double x2 = segment1.End.X;
                double y2 = segment1.End.Y;

                double x3 = segment2.Start.X;
                double y3 = segment2.Start.Y;
                double x4 = segment2.End.X;
                double y4 = segment2.End.Y;

                double denominator = (x1 - x2) * (y3 - y4) - (y1 - y2) * (x3 - x4);

                // Check if the line segments are parallel or coincident
                if (Math.Abs(denominator) < double.Epsilon)
                    throw new InvalidOperationException("Line segments are parallel or coincident.");

                double intersectionX = ((x1 * y2 - y1 * x2) * (x3 - x4) - (x1 - x2) * (x3 * y4 - y3 * x4)) / denominator;
                double intersectionY = ((x1 * y2 - y1 * x2) * (y3 - y4) - (y1 - y2) * (x3 * y4 - y3 * x4)) / denominator;

                return new Point(intersectionX, intersectionY);
            }
        }







        public int PointComparer(Helper.PointEvent a, Helper.PointEvent b)
        {
            if (a.point.Y < b.point.Y)
                return 1;
            else if (a.point.Y == b.point.Y && a.point.X < b.point.X) return 1;
            return -1;
        }


        public class Pair
        {
            public Point Point1 { get; }
            public Point Point2 { get; }
            public double Distance { get; }

            public Pair(Point point1, Point point2)
            {
                Point1 = point1;
                Point2 = point2;
                Distance = CalculateDistance();
            }

            private double CalculateDistance()
            {
                double dx = Point1.X - Point2.X;
                double dy = Point1.Y - Point2.Y;
                return Math.Sqrt(dx * dx + dy * dy);
            }
        }

        public class PointPairing
        {
            public static List<Pair> GetMinimalPairing(List<Point> points)
            {
                // Generate all possible pairs of points
                List<Pair> allPairs = new List<Pair>();
                for (int i = 0; i < points.Count - 1; i++)
                {
                    for (int j = i + 1; j < points.Count; j++)
                    {
                        Pair pair = new Pair(points[i], points[j]);
                        allPairs.Add(pair);
                    }
                }

                // Sort the pairs by distance in ascending order
                allPairs.Sort((a, b) => a.Distance.CompareTo(b.Distance));

                // Select pairs with minimal distance, ensuring each point is used only once
                List<Pair> minimalPairs = new List<Pair>();
                HashSet<Point> usedPoints = new HashSet<Point>();
                foreach (Pair pair in allPairs)
                {
                    if (!usedPoints.Contains(pair.Point1) && !usedPoints.Contains(pair.Point2))
                    {
                        minimalPairs.Add(pair);
                        usedPoints.Add(pair.Point1);
                        usedPoints.Add(pair.Point2);
                    }
                }

                return minimalPairs;
            }
        }

    }
}
