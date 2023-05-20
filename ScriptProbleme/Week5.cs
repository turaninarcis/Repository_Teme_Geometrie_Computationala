using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week5:BaseWeek
    {
        public Week5(MainWindow mainWindow):base(mainWindow) 
        {
            ProblemMethodsList.Add(AssignProblem1);
        }

        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            Problem1();
        }

        public void Problem1()
        {
            int padding=30;
            Point[] points = helper.GenerarePuncteAleatoriiConstrained(100,padding,(int)mainWindow.canvas.ActualWidth-padding,padding,(int)mainWindow.canvas.ActualHeight-padding);
            int marimeBrush = 4;
            helper.DesenarePunctePeFormular(points,new Pen(Brushes.Black,marimeBrush));
            Point minPoint = points[0];
            for(int i = 1;i<points.Length;i++)
            {
                if (points[i].Y> minPoint.Y) 
                {
                    minPoint = points[i];
                }
                else if (points[i].Y == minPoint.Y && points[i].X < minPoint.X)
                {
                    minPoint = points[i];
                }
            }
            helper.DesenarePunctPeFormular(minPoint, new Pen(Brushes.Red, marimeBrush));

            List<Point> toR= new List<Point>();
            bool valid = true;
            Point on_hull = minPoint;
            Point nextPoint;
            while(valid)
            {
                toR.Add(on_hull);
                nextPoint = points[0];
                for(int i = 0;i<points.Length;i++) 
                {
                    if(nextPoint == on_hull || Helper.GetDirection(on_hull, nextPoint, points[i])==Helper.Directie.Dreapta)
                        nextPoint = points[i];
                }
                on_hull= nextPoint;
                if (on_hull == toR[0])
                    break;
            }

            helper.DesenareLiniiIntrePuncte(toR.ToArray());
        }
    }
}
