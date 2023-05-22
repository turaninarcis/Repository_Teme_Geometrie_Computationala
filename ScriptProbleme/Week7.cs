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
            points = new List<Point>();

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
            rightMouseEventHandler = TriangularePoligonSimpluOnClick;
            mainWindow.canvas.MouseRightButtonDown+= rightMouseEventHandler;
        }

            List<Helper.Segment> diagonale = new List<Helper.Segment>();
        public void TriangularePoligonSimpluOnClick(object sender, MouseButtonEventArgs e)
        {
            helper.CreateLineBetweenLastPoints(sender, e);
            CreazaLaturiDinPuncte();
            Helper.Segment segment;

            for(int i = 1;i<=points.Count-3;i++) 
            {
                for(int j = i+2;j<=points.Count-1;j++) 
                {
                    //if(i==1&&j==points.Count-1) { continue; }
                    segment = new Helper.Segment(points[i], points[j]);
                    if (!IntersecteazaOricareLatura(segment) && !IntersecteazaOricareDiagonala(segment) && SeAflaInInterior(i, j))
                        diagonale.Add(segment);
                    if (diagonale.Count == points.Count - 3)
                    {
                        helper.DesenareSegmentePeFormular(diagonale);
                        return;
                    }
                }
            }
        }

        private bool SeAflaInInterior(int i, int j)
        {
            Helper.Directie Varf;
            Helper.Directie primaDirectie;
            Helper.Directie aDouaDirectie;
            Varf = Helper.GetDirection(points[i - 1], points[i], points[i + 1]);
            primaDirectie = Helper.GetDirection(points[i], points[j], points[i + 1]);
            aDouaDirectie = Helper.GetDirection(points[i], points[i - 1], points[j]);

            if (Varf == Helper.Directie.Dreapta)
            {
                if ((primaDirectie == aDouaDirectie && primaDirectie == Helper.Directie.Stanga))
                    return true;
            }
            else if (Varf == Helper.Directie.Stanga)
            {
                if (primaDirectie == aDouaDirectie && aDouaDirectie == Helper.Directie.Dreapta)
                    return false;
            }
            return true;
        }
        private bool IntersecteazaOricareLatura(Helper.Segment segment)
        {
            for (int k = 0; k < laturiPoligon.Count; k++)
            {
                if (Helper.IsIntersection(segment, laturiPoligon[k]))
                { return true ; }
            }
            return false;
        }
        private bool IntersecteazaOricareDiagonala(Helper.Segment segment)
        {
            for (int k = 0; k < diagonale.Count; k++)
            {
                if (Helper.IsIntersection(segment, diagonale[k])) { return true; }
            }
            return false;
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
