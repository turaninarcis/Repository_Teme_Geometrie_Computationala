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
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            Helper.Segment segment;

            for(int i = 0;i<=points.Count-3;i++) 
            {
                for(int j = i+2;j<=points.Count-2;j++) 
                {
                    //if(i==1&&j==points.Count-1) { continue; }
                    segment = new Helper.Segment(points[i], points[j]);
                    if (!Helper.IntersecteazaOricareLatura(segment,laturiPoligon) && !Helper.IntersecteazaOricareDiagonala(segment,diagonale) && Helper.SeAflaInInterior(points,j, i))
                        diagonale.Add(segment);
                    if (diagonale.Count == points.Count - 3)
                    {
                        helper.DesenareSegmentePeFormular(diagonale);
                        return;
                    }
                }
            }
        }


    }
}
