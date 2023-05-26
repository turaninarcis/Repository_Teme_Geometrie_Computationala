using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using static Repository_Teme_Geometrie_Computationala.Helper;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    public class Week10 : BaseWeek
    {
        List<Point> points;
        List<Point> sortedPoints;
        List<Helper.Segment> laturiPoligon;
        List<Helper.Segment> diagonale = new List<Helper.Segment>();
        List<Poligon> poligoane = new List<Poligon>();
        List<Helper.Segment> diagonaleCurente = new List<Helper.Segment>();

        DispatcherTimer timer = new DispatcherTimer();
        int indexTimer = 0;



        public Week10(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(Problema1);
            points = new List<Point>();
            sortedPoints = new List<Point>();

        }

        public void Problema1(object obj, RoutedEventArgs e)
        {
            HandlePointOnClick(points);
        }




        public void HandlePointOnClick(List<Point> pointList)
        {
            pointList.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePoligonOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            rightMouseEventHandler = PartitionarePoligon;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }


        public void PartitionarePoligon(object sender, MouseButtonEventArgs e)
        {
            diagonale.Clear();
            sortedPoints = GetSortedPoints(points);
            helper.CreateLineBetweenLastPoints(sender, e);
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            Helper.Segment segment;
            for (int i = 1; i <= points.Count; i++)
            {
                int iNormalised = i % points.Count;
                int excessOne = (i + 1) % points.Count;
                if (Helper.GetDirection(points[i - 1], points[iNormalised], points[excessOne]) == Directie.Stanga)
                {
                    bool ok;
                    if (points[i - 1].Y > points[iNormalised].Y && points[excessOne].Y > points[iNormalised].Y)
                    {
                        int index = sortedPoints.IndexOf(points[iNormalised]);

                        do
                        {
                            ok = true;
                            if (index - 1 < 0) index = points.Count - 1;
                            segment = new Segment(points[iNormalised], sortedPoints[index - 1]);
                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index - 1, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else { ok = false; index--; }
                        } while (ok == false);

                    }
                    if (points[i - 1].Y < points[iNormalised].Y && points[excessOne].Y < points[iNormalised].Y)
                    {

                        int index = sortedPoints.IndexOf(points[iNormalised]);
                        do
                        {
                            ok = true;

                            segment = new Segment(points[iNormalised], sortedPoints[index + 1]);
                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index + 1, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else { ok = false; index++; }
                        } while (ok == false);
                    }
                }

            }

            PlayAnimation(diagonale);
            List<Point> aux = new List<Point>();
            poligoane.Clear();
            poligoane.Add(new Poligon(points));
            int index1;
            int index2;
            int min;
            int max;
            for (int i = 0; i < diagonale.Count; i++)
            {
                for (int k = 0; k < poligoane.Count; k++)
                {
                    index1 = poligoane[k].points.IndexOf(diagonale[i].a);
                    index2 = poligoane[k].points.IndexOf(diagonale[i].b);
                    if (index1 >= 0 && index2 >= 0)
                    {
                        aux.Clear();

                        min = Math.Min(index1, index2);
                        max = Math.Max(index1, index2);
                        for (int z = min; z <= max; z++)
                        {
                            aux.Add(poligoane[k].points[z]);
                        }
                        poligoane.Add(new Poligon(aux));
                        aux.Clear();
                        for (int z = min + 1; z < max; z++)
                        {
                            aux.Add(poligoane[k].points[z]);
                        }
                        foreach (Point p in aux)
                            poligoane[k].points.Remove(p);
                        break;
                    }
                }
            }

            TriangulatePoligons();

        }

        private void TriangulatePoligons()
        {
            Segment segment = new Segment();
            Stack<Point> stack = new Stack<Point>();

            foreach (Poligon poligon in poligoane)
            {
                poligon.SeteazaPerimetruPoligon();
                List<Point> sortedPoints = GetSortedPoints(poligon.points);
                List<Point> lant1 = new List<Point>();
                List<Point> lant2 = new List<Point>();
                Queue<Point> queue = new Queue<Point>();
                int indexCapatLant1 = poligon.points.IndexOf(sortedPoints[0]);
                int indexCapatLant2 = poligon.points.IndexOf(sortedPoints[sortedPoints.Count - 1]);
                for (int i = indexCapatLant1; i <= indexCapatLant2; i++)
                    lant1.Add(poligon.points[i]);
                int lungimeLant2 = poligon.points.Count - lant1.Count + 1;
                for (int i = 0; i <= lungimeLant2; i++)
                    lant2.Add(poligon.points[(indexCapatLant2 + i) % poligon.points.Count]);
                stack.Clear();
                stack.Push(poligon.points[0]);
                stack.Push(poligon.points[1]);

                for (int j = 2; j < poligon.points.Count - 1; j++)
                {
                    if (lant1.Contains(poligon.points[j]) && lant2.Contains(stack.Peek()))
                    {
                        while (stack.Count > 1)
                        {
                            poligon.diagonale.Add(new Segment(poligon.points[j], stack.Pop()));
                        }
                        stack.Clear();
                        stack.Push(poligon.points[j - 1]);
                        stack.Push(poligon.points[j]);
                    }
                    else
                    {
                        Point ultimulElementSters = stack.Pop();
                        segment = new Segment(poligon.points[j], stack.Peek());
                        while (stack.Count > 0)
                        {
                            if (!Helper.IntersecteazaOricareLatura(segment, poligon.laturiPoligon) &&
                            !Helper.IntersecteazaOricareDiagonala(segment, poligon.diagonale) &&
                            SeAflaInInterior(poligon.points, j, poligon.points.IndexOf(stack.Peek()),poligon.points.Count))
                            {
                                poligon.diagonale.Add(segment);
                                stack.Pop();
                            }
                            else queue.Enqueue(stack.Pop());

                        }
                        foreach (Point p in queue) stack.Push(p);

                        stack.Push(ultimulElementSters);
                        stack.Push(poligon.points[j]);

                    }

                }
                stack.Pop();
                while (stack.Count > 1)
                {
                    segment = new Segment(poligon.points[poligon.points.Count - 1], stack.Pop());
                    poligon.diagonale.Add(segment);
                }


                PlayAnimation(poligon.diagonale);

            }
        }

        public bool DiagonalaExistaDeja(Segment segment)
        {
            for (int i = 0; i < diagonale.Count; i++)
            {
                if (diagonale[i].a == segment.a && diagonale[i].b == segment.b || diagonale[i].b == segment.a && diagonale[i].a == segment.b)
                    return true;
            }
            return false;
        }

        public List<Point> GetSortedPoints(List<Point> points)
        {
            List<Point> sortedPoints = new List<Point>();
            sortedPoints.Clear();
            foreach (Point point in points)
            {
                sortedPoints.Add(point);
            }
            Helper.SortarePuncteY(sortedPoints);
            return sortedPoints;
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (indexTimer < diagonaleCurente.Count)
            
            {
                helper.DesenareLinie(diagonaleCurente[indexTimer].a, diagonaleCurente[indexTimer].b, new Pen(Brushes.Red, 2));
                indexTimer++;
            }
            else
                timer.Stop();
        }
        private void PlayAnimation(List<Segment> diagnoale)
        {
            this.diagonaleCurente = diagonale;
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += timer_Tick;
            indexTimer = 0;
            timer.Start();
        }

        public bool SeAflaInInterior(List<Point> points, int i, int j, int n)
        {
            Directie Varf;
            Directie primaDirectie;
            Directie aDouaDirectie;
            int pointPlusUnu = (i + 1) % (n);
            int pointMinusUnu;
            if (i - 1 < 0)
                pointMinusUnu = n - 1;
            else pointMinusUnu = i - 1;
            Varf = GetDirection(points[pointMinusUnu], points[i], points[pointPlusUnu]);
            primaDirectie = GetDirection(points[i], points[j], points[pointPlusUnu]);
            aDouaDirectie = GetDirection(points[i], points[pointMinusUnu], points[j]);

            if (Varf == Directie.Dreapta)
            {
                if (!(primaDirectie == Directie.Stanga && aDouaDirectie == Directie.Stanga))
                    return false;
            }
            else if (Varf == Directie.Stanga)
            {
                if (primaDirectie == Directie.Dreapta && aDouaDirectie == Directie.Dreapta)
                    return false;
            }
            return true;
        }


        public class Poligon
        {
            public List<Point> points = new List<Point>();
            public List<Segment> laturiPoligon = new List<Segment>();
            public List<Segment> diagonale = new List<Segment>();

            public void AddPointToPoligon(Point point)
            {
                this.points.Add(point);
            }
            public void RemovePointFromPoligon(Point point)
            {
                this.points.Remove(point);
            }
            public Poligon(List<Point> points)
            {
                this.points.Clear();
                AssignPointsToPoligon(points);
            }

            public void SeteazaPerimetruPoligon()
            {
                laturiPoligon = Helper.CreazaLaturiDinPuncte(this.points);
            }

            private void AssignPointsToPoligon(List<Point> points)
            {
                foreach (Point point in points) { this.points.Add(point); }
            }
        }
    }
}
