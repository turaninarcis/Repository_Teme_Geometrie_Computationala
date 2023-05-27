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
        List<Brush> brushes = new List<Brush>()
        {
            Brushes.Blue, Brushes.Green,Brushes.DarkTurquoise,Brushes.Brown,Brushes.DeepPink,Brushes.Purple,Brushes.Gray
        };
        List<Poligon> poligoane = new List<Poligon>();
        List<Helper.Segment> diagonaleCurente = new List<Helper.Segment>();
        List<List<Segment>> listaDiagonale = new List<List<Segment>>();
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
            listaDiagonale.Clear();
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
                        int index = sortedPoints.IndexOf(points[iNormalised]) - 1;
                        do
                        {
                            if (index < 0)
                                break;

                            ok = true;
                            segment = new Segment(points[iNormalised], sortedPoints[index]);

                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else { ok = false; index--; }
                        } while (ok == false);

                    }
                    if (points[i - 1].Y < points[iNormalised].Y && points[excessOne].Y < points[iNormalised].Y)
                    {
                        int index = sortedPoints.IndexOf(points[iNormalised]) + 1;
                        do
                        {
                            if (index >= points.Count)
                                break;
                            ok = true;
                            segment = new Segment(points[iNormalised], sortedPoints[index]);

                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else
                            {
                                ok = false; index++;
                            }
                        } while (ok == false);
                    }
                }
            }

            listaDiagonale.Add(diagonale);
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
            List<Point> lant1 = new List<Point>();
            List<Point> lant2 = new List<Point>();
            Queue<Point> queue = new Queue<Point>();
            foreach (Poligon poligon in poligoane)
            {
                poligon.SeteazaPerimetruPoligon();
                List<Point> sortedPoints = GetSortedPoints(poligon.points);
                lant1.Clear();
                lant2.Clear();
                queue.Clear();
                stack.Clear();
                int indexCapatLant1 = poligon.points.IndexOf(sortedPoints[0]);
                int indexCapatLant2 = poligon.points.IndexOf(sortedPoints[sortedPoints.Count - 1]);
                int max = Math.Max(indexCapatLant1,indexCapatLant2);
                int min = Math.Min(indexCapatLant1, indexCapatLant2);
                foreach (Point p in poligon.points)
                    lant1.Add(p);
                for (int i = min; i <= max; i++)
                    lant2.Add(poligon.points[i]);
                for(int i = min+1;i< max; i++)
                {
                    lant1.Remove(poligon.points[i]);
                }

                stack.Push(poligon.points[0]);
                stack.Push(poligon.points[1]);

                for (int j = 2; j < poligon.points.Count - 1; j++)
                {
                    if (PuncteleFacParteDePeLanturiDiferite(lant1, lant2, poligon.points[j],stack.Peek()))
                    {
                        //stack.Pop();
                        while (stack.Count >1)
                        {

                            segment = new Segment(poligon.points[j], stack.Pop());
                            poligon.diagonale.Add(segment);
                        }
                        stack.Clear();
                        stack.Push(poligon.points[j-1]);
                        stack.Push(poligon.points[j]);
                    }
                    else
                    {
                        Point ultimulElementSters = stack.Pop();
                        while (stack.Count > 0)
                        {
                            segment = new Segment(poligon.points[j], stack.Peek());
                            if (!Helper.IntersecteazaOricareLatura(segment, poligon.laturiPoligon) &&
                            !Helper.IntersecteazaOricareDiagonala(segment, poligon.diagonale) &&
                            SeAflaInInterior(poligon.points, j, poligon.points.IndexOf(stack.Peek()), poligon.points.Count))
                            {
                                poligon.diagonale.Add(segment);
                                ultimulElementSters = stack.Pop();
                            }
                            else queue.Enqueue(stack.Pop());

                        }
                        while(queue.Count>0) stack.Push(queue.Dequeue());

                        stack.Push(ultimulElementSters);
                        stack.Push(poligon.points[j]);

                    }
                    
                    
                    
                }

                stack.Pop();
                while (stack.Count > 1)
                    poligon.diagonale.Add(new Segment(poligon.points[poligon.points.Count - 1], stack.Pop()));

                listaDiagonale.Add(poligon.diagonale);
            }
            PlayAnimation();
        }

        public bool PuncteleFacParteDePeLanturiDiferite(List<Point> lant1, List<Point> lant2, Point a, Point b)
        {
            if (lant1.Contains(a) && lant1.Contains(b) || lant2.Contains(a) && lant2.Contains(b)) return false;
            if (((lant1.Contains(a) ==true && lant2.Contains(b)==true)
                || (lant1.Contains(b) == true && lant2.Contains(a) == true)))
                return true;
            return false;
        }
        public bool DiagonalaExistaDeja(Segment segment,List<Segment> lista)
        {
            for (int i = 0; i < lista.Count; i++)
            {
                if (lista[i].a == segment.a && lista[i].b == segment.b || lista[i].b == segment.a && lista[i].a == segment.b)
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
                int indexBrush=0;
            foreach (List<Segment> lista in listaDiagonale)
            {
                while (indexTimer < lista.Count)
                {
                    helper.DesenareLinie(lista[indexTimer].a, lista[indexTimer].b, new Pen(brushes[indexBrush], 2));
                    indexTimer++;
                }
                indexBrush++;
                indexTimer = 0;
            }
            timer.Stop();
        }
        private void PlayAnimation()
        {
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
