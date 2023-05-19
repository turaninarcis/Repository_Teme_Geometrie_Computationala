using Repository_Teme_Geometrie_Computationala.Comparers;
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
            Exercitiu_2(4);

        }
        public void Exercitiu_2(int nrLinii)
        {
            Helper.Segment[] segmente = new Helper.Segment[nrLinii];
            for(int i = 0;i<nrLinii;i++)
            {
                Helper.PointEvent a = new Helper.PointEvent(helper.GenerarePunctAleatoriu());
                Helper.PointEvent b = new Helper.PointEvent(helper.GenerarePunctAleatoriu());

                segmente[i] = new Helper.Segment(a,b);
                helper.DesenareLinie(segmente[i].a.point, segmente[i].b.point, new Pen(Brushes.Black, 3));
            }
            EventPointComparer comparer = new EventPointComparer();

            SortedList<Helper.Segment, Comparer> sortedList = new SortedList<Helper.Segment, Comparer>();
            

            
        }              
    }
}
