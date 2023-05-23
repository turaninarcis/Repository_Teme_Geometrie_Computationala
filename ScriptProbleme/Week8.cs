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

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week8:BaseWeek
    {
        List<Point> points;
        List<Helper.Segment> laturiPoligon;
        DispatcherTimer timer = new DispatcherTimer();
        int indexTimer = 0;

        public Week8(MainWindow mainWindow):base(mainWindow)
        {
            ProblemMethodsList.Add(Problema1);
            points = new List<Point>();
            timer.Interval = TimeSpan.FromMilliseconds(300);
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
            indexTimer = 0;
            helper.CreateLineBetweenLastPoints(sender, e);
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

                        points.RemoveAt(i+1);
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


        public static bool SeAflaInInterior(List<Point> points, int i, int j,int n)
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
                if ((primaDirectie == Directie.Stanga && aDouaDirectie == Directie.Stanga))
                    return true;
                else return false;
            }
            else if (Varf == Directie.Stanga)
            {
                if (primaDirectie == Directie.Dreapta && aDouaDirectie == Directie.Dreapta)
                    return false;
            }
            return true;
        }

    }
}
