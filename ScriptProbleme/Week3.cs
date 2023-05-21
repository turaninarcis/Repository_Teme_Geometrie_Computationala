using System.Collections;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    internal class Week3:BaseWeek
    {
        public Week3(MainWindow mainWindow):base(mainWindow) 
        {
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem2);
        }

        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            Exercitiu_2(30);

        }
        public void Exercitiu_2(int nrLinii)
        {
            List<Helper.SegmentPointEvent> segmente = new List<Helper.SegmentPointEvent>();
            for(int i = 0;i<nrLinii;i++)
            {
                Helper.PointEvent a = new Helper.PointEvent(helper.GenerarePunctAleatoriu());
                Helper.PointEvent b = new Helper.PointEvent(helper.GenerarePunctAleatoriu());

                segmente.Add(new Helper.SegmentPointEvent(a,b));
                helper.DesenareLinie(segmente[i].a.point, segmente[i].b.point, new Pen(Brushes.Black, 3));
            }            
        }         
        
        public int PointComparer(Helper.PointEvent a, Helper.PointEvent b)
        {
            if (a.point.Y < b.point.Y)
                return 1;
            else if (a.point.Y == b.point.Y && a.point.X < b.point.X) return 1;
            return -1;
        }
    }
}
