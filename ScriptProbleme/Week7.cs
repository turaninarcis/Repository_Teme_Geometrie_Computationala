using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week7:BaseWeek
    {
        List<Point> points;
        List<Helper.Segment> laturiPoligon;
        public Week7(MainWindow mainWindow) : base(mainWindow) 
        {
            ProblemMethodsList.Add(Problema1);
        }

        public void Problema1(object obj, RoutedEventArgs e)
        {
            points = new List<Point>();
            HandlePointOnClick(points);            
        }


        public void HandlePointOnClick(List<Point> pointList)
        {
            pointList.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePoligonOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            rightMouseEventHandler = TriangularePoligonSimpluOnClick;
            mainWindow.canvas.MouseRightButtonDown+= rightMouseEventHandler;
        }

        public void TriangularePoligonSimpluOnClick(object sender, MouseButtonEventArgs e)
        {
            helper.CreateLineBetweenLastPoints(sender, e);
            CreazaLaturiDinPuncte();
            bool ok;
            List<Helper.Segment> diagonale = new List<Helper.Segment>();
            for(int i = 0;i<=points.Count-3;i++) 
            {
                ok = true;
                for(int j = i+2;j<=points.Count-1;j++) 
                {
                    //if(i==0&&j==points.Count-1) { break; }
                    for(int k = 0;k < laturiPoligon.Count;k++)
                    {
                        if (Helper.IsIntersection(new Helper.Segment(points[i], points[j]), laturiPoligon[k]))
                        { ok = false;}
                    }
                    for(int k = 0;k<diagonale.Count;k++)
                    {
                        if (Helper.IsIntersection(new Helper.Segment(points[i], points[j]), diagonale[k])) { ok = false; }
                    }
                    if (ok) diagonale.Add(new Helper.Segment(points[i], points[j]));
                    if (diagonale.Count == points.Count - 3)
                    {
                        helper.DesenareSegmentePeFormular(diagonale);
                        return;
                    }
                }
            }
        }


        public void CreazaLaturiDinPuncte()
        {
            laturiPoligon = new List<Helper.Segment>();
            for(int i = 0;i<points.Count-1;i++)
            {
                laturiPoligon.Add(new Helper.Segment(points[i], points[i + 1]));
            }
            laturiPoligon.Add(new Helper.Segment(points[points.Count - 1], points[0]));
        }
    }
}
