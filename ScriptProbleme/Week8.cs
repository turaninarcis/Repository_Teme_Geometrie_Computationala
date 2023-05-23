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

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week8:BaseWeek
    {
        List<Point> points;
        List<Helper.Segment> laturiPoligon;
        List<TriColoringPoint> triColors = new List<TriColoringPoint>();
        DispatcherTimer timer = new DispatcherTimer();
        int indexTimer = 0;
        public Week8(MainWindow mainWindow):base(mainWindow)
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
            textBox.Text = CalculeazaAria(points).ToString();
            Canvas.SetTop(textBox, 0);
            Canvas.SetLeft(textBox, 0);
            mainWindow.canvas.Children.Add(textBox);
            indexTimer = 0;
            helper.CreateLineBetweenLastPoints(sender, e);
            TransformPointsToTricoloringPoints();
            triColors[0].SetColor(Colors.Blue);
            helper.DesenarePunctPeFormular(triColors[0].p, new Pen(new SolidColorBrush(triColors[0].color),10));
            triColors[1].SetColor(Colors.Red);
            helper.DesenarePunctPeFormular(triColors[1].p, new Pen(new SolidColorBrush(triColors[1].color), 10));
            triColors[2].SetColor(Colors.Green);
            helper.DesenarePunctPeFormular(triColors[2].p, new Pen(new SolidColorBrush(triColors[2].color), 10));
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            Helper.Segment segment;
            int n = points.Count-1;
            while(n>=3)
            {
                for (int i = 1; i <n; i++)
                {
                    int excessOne = (i + 1)%(n+1);
                    int excessTwo = (i+2)%(n+1);
                    segment = new Helper.Segment(points[i], points[excessTwo]);

                    if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points,i,excessTwo,n))
                    {
                        diagonale.Add(segment);
                        triColors[excessTwo].SetNeighbours(triColors[i], triColors[i-1]);
                        helper.DesenarePunctPeFormular(triColors[excessTwo].p, new Pen(new SolidColorBrush(triColors[excessTwo].color), 10));

                        points.RemoveAt(excessOne);
                        n--;
                        break;
                    }
                }
            }        
            timer.Start();
        }
        void timer_Tick(object sender, EventArgs e)
        {
            if(indexTimer < diagonale.Count)
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
            for(int i = 0;i < n;i++)
            {
                toR += (pointList[i].X * pointList[(i + 1) % n].Y) - (pointList[(i + 1) % n].X * pointList[i].Y);
            }
            return toR/2000;
        }

        private void TransformPointsToTricoloringPoints()
        {
            foreach (Point p in points)
                triColors.Add(new TriColoringPoint(p));
        }
        public bool SeAflaInInterior(List<Point> points, int i, int j,int n)
        {
            Directie Varf;
            Directie primaDirectie;
            Directie aDouaDirectie;
            int pointPlusUnu = (i + 1) % (n + 1);
            Varf = GetDirection(points[i - 1], points[i], points[pointPlusUnu]);
            primaDirectie = GetDirection(points[i], points[j], points[pointPlusUnu]);
            aDouaDirectie = GetDirection(points[i], points[i-1], points[j]);

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
        public struct TriColoringPoint
        {
            public Point p;
            public Color color;
            public TriColoringPoint[] neighbours = new TriColoringPoint[2];
            public List<Color> RGB = new List<Color>(){ Colors.Red,Colors.Green, Colors.Blue}; 
            public TriColoringPoint(Point p)
            {
                this.p = p;
            }
            public void SetColor(Color color)
            {
                this.color = color;
            }
            public void SetNeighbours(TriColoringPoint n1, TriColoringPoint n2)
            {
                neighbours[0] = n1;
                neighbours[1] = n2;
                for (int i = 0; i < RGB.Count; i++)
                {
                    try
                    {
                        if (RGB[i].GetNativeColorValues() == n2.color.GetNativeColorValues() || RGB[i].GetNativeColorValues() == n1.color.GetNativeColorValues()) { RGB.RemoveAt(i); i = 0; };
                    }
                    catch(Exception) { }
                }
                color = RGB[0];
            }
            public TriColoringPoint(Point p,Color color) 
            {
                this.p = p;
                this.color = color;
            }
        }
    }
}
