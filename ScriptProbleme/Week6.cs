using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week6:BaseWeek
    {
        List<Point> pointList= new List<Point>();
        public Week6(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(AssignProblem1);
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem3);
        }


        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            int nrPuncte = 24;
            pointList.Clear();
            for(int i = 0;i<nrPuncte;i++)
            {
                pointList.Add(helper.GenerarePunctAleatoriu());
            }
            helper.DesenarePunctePeFormular(pointList.ToArray(), new Pen(Brushes.Black, 5));
            List<Helper.Segment> segmentList = new List<Helper.Segment>();
            for(int i = 0;i< nrPuncte-1;i++) 
            {
                for(int j = i+1;j<nrPuncte;j++)
                {
                    segmentList.Add(new Helper.Segment(pointList[i], pointList[j]));
                }
            }
            segmentList.Sort(Helper.SegmentComparer);

            List<Helper.Segment> triangulare = new List<Helper.Segment>();
            triangulare.Add(segmentList[0]);
            bool ok;
            for(int i = 1;i<segmentList.Count;i++) 
            {
                ok = true;
                for(int j = 0;j<triangulare.Count;j++) 
                {
                    if (Helper.IsIntersection(segmentList[i], triangulare[j])) ok = false;
                }
                if(ok) { triangulare.Add(segmentList[i]); }
            }
            for(int i = 0;i<triangulare.Count;i++)
            {
                helper.DesenareLinie(triangulare[i].a, triangulare[i].b,new Pen(Brushes.Red,3));
            }
        }



        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            HandlePointOnClick();
            rightMouseEventHandler = helper.CreateLineBetweenLastPoints;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }
    
        public void AssignProblem3(object obj, RoutedEventArgs e)
        {
            HandlePointOnClick();
            rightMouseEventHandler = helper.TriangularePoligonConvex;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }


        public void HandlePointOnClick()
        {
            pointList.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePointOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
        }

    }
}
