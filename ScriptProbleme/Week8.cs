using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static Repository_Teme_Geometrie_Computationala.Helper;
using System.Windows.Media;
using System.Windows.Threading;
using System.Windows.Controls;
using System.Runtime.InteropServices;
using System.Windows.Media.Effects;
using System.Runtime.Intrinsics.X86;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week8 : BaseWeek
    {
        List<Point> points;
        List<Helper.Segment> laturiPoligon;
        List<TriColoringPoint> triColors = new List<TriColoringPoint>();
        DispatcherTimer timer = new DispatcherTimer();
        List<TriColoringTriangle> triColoringTriangles = new List<TriColoringTriangle>();
        int indexTimer = 0;
        public static Color red = Colors.Red;
        public static Color green = Colors.Green;
        public static Color blue = Colors.Blue;
        public Week8(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(Problema1);
            points = new List<Point>();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += timer_Tick;

        }

        public void Problema1(object obj, RoutedEventArgs e)
        {
            HandlePointOnClick(points);
        }




        public void HandlePointOnClick(List<Point> pointList)
        {
            pointList.Clear();
            diagonale.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePoligonOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            rightMouseEventHandler = TriangularePoligonOtectomieOnClick;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }



        List<Helper.Segment> diagonale = new List<Helper.Segment>();
        public void TriangularePoligonOtectomieOnClick(object sender, MouseButtonEventArgs e)
        {
            TextBox textBox = new TextBox();
            triColors.Clear();
            textBox.Text = CalculeazaAria(points).ToString();
            Canvas.SetTop(textBox, 0);
            Canvas.SetLeft(textBox, 0);
            mainWindow.canvas.Children.Add(textBox);
            indexTimer = 0;
            helper.CreateLineBetweenLastPoints(sender, e);
            TransformPointsToTricoloringPoints();
            triColors[1].SetColor(red);
            triColors[1].RGB.Clear();
            triColors[1].RGB.Add(red);
            helper.DesenarePunctPeFormular(triColors[1].p, new Pen(new SolidColorBrush(triColors[1].color), 10));
            triColors[2].SetColor(green);
            triColors[2].RGB.Clear();
            triColors[2].RGB.Add(green);
            helper.DesenarePunctPeFormular(triColors[2].p, new Pen(new SolidColorBrush(triColors[2].color), 10));
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            Helper.Segment segment;
            int n = points.Count - 1;
            while (n >= 3)
            {
                for (int i = 1; i < n; i++)
                {

                    int excessOne = (i + 1) % (n + 1);
                    int excessTwo = (i + 2) % (n + 1);
                    segment = new Helper.Segment(points[i], points[excessTwo]);

                    if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, i, excessTwo, n))
                    {
                        triColoringTriangles.Add(new TriColoringTriangle(triColors[i], triColors[excessOne], triColors[excessTwo]));
                        diagonale.Add(segment);
                        triColors.RemoveAt(excessOne);
                        points.RemoveAt(excessOne);
                        n--;
                        break;
                    }
                }

            }
            triColoringTriangles.Add(new TriColoringTriangle(triColors[0], triColors[1], triColors[2]));
            bool ok;
            do
            {
                ok = true;
                for (int i = 0; i < triColoringTriangles.Count; i++)
                {
                    if (triColoringTriangles[i].tricoloredPoints == 2)
                    {
                        ok = false;
                        TriColoringPoint p = triColoringTriangles[i].ColorNonColoredPoint();
                        helper.DesenarePunctPeFormular(p.p, new Pen(new SolidColorBrush(p.color), 10));
                        triColoringTriangles.RemoveAt(i);
                    }
                }

            } while (ok == false);

            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if (indexTimer < diagonale.Count)
            {
                helper.DesenareLinie(diagonale[indexTimer].a, diagonale[indexTimer].b, new Pen(Brushes.Red, 2));
                indexTimer++;
            }
            else
                timer.Stop();
        }

        private double CalculeazaAria(List<Point> pointList)
        {
            double toR = 0;
            int n = pointList.Count;
            for (int i = 0; i < n; i++)
            {
                toR += (pointList[i].X * pointList[(i + 1) % n].Y) - (pointList[(i + 1) % n].X * pointList[i].Y);
            }
            return toR / 2000;
        }

        private void TransformPointsToTricoloringPoints()
        {
            for (int i = 0; i < points.Count; i++)
            {
                triColors.Add(new TriColoringPoint(points[i]));
            }
        }
        public bool SeAflaInInterior(List<Point> points, int i, int j, int n)
        {
            Directie Varf;
            Directie primaDirectie;
            Directie aDouaDirectie;
            int pointPlusUnu = (i + 1) % (n + 1);
            Varf = GetDirection(points[i - 1], points[i], points[pointPlusUnu]);
            primaDirectie = GetDirection(points[i], points[j], points[pointPlusUnu]);
            aDouaDirectie = GetDirection(points[i], points[i - 1], points[j]);

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
        public class TriColoringPoint
        {
            public Point p;
            public Color color;
            public List<TriColoringPoint> neighbours = new List<TriColoringPoint>();
            public List<Color> RGB = new List<Color>() { red, green, blue };
            public TriColoringPoint(Point p)
            {
                this.p = p;
            }
            public void SetColor(Color color)
            {
                this.color = color;
            }
            public void AddNeighbour(TriColoringPoint neighbour)
            {
                neighbours.Add(neighbour);
            }
            public void SetColorFinal()
            {

                for (int i = 0; i < RGB.Count; i++)
                {
                    for (int j = 0; j < neighbours.Count; j++)
                        if (RGB[i].Equals(neighbours[j].color))
                        {
                            RGB.RemoveAt(i); i = -1;
                            if (RGB.Count == 1)
                            {
                                color = RGB[0];
                                return;
                            }
                            break;
                        }
                }


            }
        }

        private class TriColoringTriangle
        {
            TriColoringPoint[] points = new TriColoringPoint[3];

            public int tricoloredPoints
            {
                get
                {
                    int aux = 0;
                    for (int i = 0; i < points.Length; i++)
                    {
                        if (points[i].color.R != 0 || points[i].color.G != 0 || points[i].color.B != 0) aux++;
                    }

                    return aux;
                }
            }
            public TriColoringTriangle(TriColoringPoint a, TriColoringPoint b, TriColoringPoint c)
            {
                this.points = new TriColoringPoint[3] { a, b, c };
                SetNeighbours();
            }
            public void SetNeighbours()
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (!points[i].neighbours.Contains(points[(i + 1) % points.Length]))
                        points[i].neighbours.Add(points[(i + 1) % points.Length]);
                    if (!points[i].neighbours.Contains(points[(i + 2) % points.Length]))
                        points[i].neighbours.Add(points[(i + 2) % points.Length]);
                }

            }
            public TriColoringPoint ColorNonColoredPoint()
            {
                TriColoringPoint nonColored = GetNonColoredPoint();
                nonColored.SetColorFinal();
                return nonColored;
            }
            public TriColoringPoint GetNonColoredPoint()
            {
                for (int i = 0; i < points.Length; i++)
                {
                    if (points[i].color.R == 0 && points[i].color.G == 0 && points[i].color.B == 0) return points[i];
                }
                return points[0];
            }
        }
    }
}
