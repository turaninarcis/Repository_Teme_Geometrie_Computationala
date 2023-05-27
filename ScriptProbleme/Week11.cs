using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Repository_Teme_Geometrie_Computationala.Helper;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    public class Week11 : BaseWeek
    {
        List<Point> points;
        List<Helper.Segment> laturiPoligon;
        List<Segment> segmenteNeimportante = new List<Helper.Segment>();
        List<Segment> segmenteImportante= new List<Helper.Segment>();
        List<Poligon> poligoane = new List<Poligon>();
        public Week11(MainWindow mainWindow) : base(mainWindow)
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
            poligoane.Clear();
            pointList.Clear();
            diagonale.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePoligonOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            rightMouseEventHandler = OnClickEvent;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }

        List<Helper.Segment> diagonale = new List<Helper.Segment>();
        public void OnClickEvent(object sender, MouseButtonEventArgs e)
        {
            helper.CreateLineBetweenLastPoints(sender, e);
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            TriangularePoligon();
            CreatePoligoaneConvexe();
            helper.DesenareSegmentePeFormular(diagonale);
        }

        private void TriangularePoligon()
        {
            Helper.Segment segment;

            for (int i = 0; i <= points.Count - 3; i++)
            {
                for (int j = i + 2; j <= points.Count - 2; j++)
                {
                    //if(i==1&&j==points.Count-1) { continue; }
                    segment = new Helper.Segment(points[i], points[j]);
                    if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && !Helper.IntersecteazaOricareDiagonala(segment, diagonale) && Helper.SeAflaInInterior(points, j, i))
                        diagonale.Add(segment);
                    if (diagonale.Count == points.Count - 3)
                    {
                        return;
                    }
                }
            }
        }
        private void CreatePoligoaneConvexe()
        {

        }

        public Segment CreateSegment(Point point)
        {
            int pointPlusUnu;
            int pointMinusUnu;
            pointPlusUnu = (points.IndexOf(point) + 1) % (points.Count);
            if (points.IndexOf(point) - 1 < 0)
                pointMinusUnu = points.Count - 1;
            else pointMinusUnu = points.IndexOf(point) - 1;
            return new Segment(points[pointPlusUnu], points[pointMinusUnu]);
        }

        public static bool IntersecteazaOricareDiagonala(Segment segment, List<Segment> diagonale)
        {
            for (int k = 0; k < diagonale.Count; k++)
            {
                if (IsIntersection(segment, diagonale[k])) 
                { return true; }
            }
            return false;
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



