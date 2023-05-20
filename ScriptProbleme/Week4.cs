using System.Collections.Generic;
using System.Windows;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    class Week4:BaseWeek
    {


        public Week4(MainWindow mainWindow):base(mainWindow) {
            ProblemMethodsList.Add(AssignProblem1);

        }
        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            int padding = 30;
            Point[] puncte = helper.GenerarePuncteAleatoriiConstrained(100, padding, (int)mainWindow.canvas.ActualWidth - padding, padding, (int)mainWindow.canvas.ActualHeight - padding);
            helper.DesenarePunctePeFormular(puncte,new Pen(Brushes.Black,4));
            Helper.SortarePuncte(puncte);
            Point[] invelitoare = GetPerimeter(puncte);
            helper.DesenarePunctPeFormular(invelitoare[0], new Pen(Brushes.Red,4));
            helper.DesenareLiniiIntrePuncte(invelitoare);
        }

        private Point[] GetPerimeter(Point[] puncte)
        {
            List<Point> sup = new List<Point>();
            List<Point> inf = new List<Point>();
            Point[] final;

            sup.Add(puncte[0]);
            sup.Add(puncte[1]);

            for (int i = 2; i < puncte.Length; i++)
            {
                sup.Add(puncte[i]);
                while (sup.Count > 2 && Helper.GetDirection(sup[sup.Count - 1], sup[sup.Count - 2], sup[sup.Count - 3]) != Helper.Directie.Dreapta)
                {
                    sup.Remove(sup[sup.Count - 2]);
                }
            }

            inf.Add(puncte[puncte.Length - 1]);
            inf.Add(puncte[puncte.Length - 2]);
            for (int i = puncte.Length - 3; i >= 0; i--)
            {
                inf.Add(puncte[i]);
                while (inf.Count > 2 && Helper.GetDirection(inf[inf.Count - 1], inf[inf.Count - 2], inf[inf.Count - 3]) != Helper.Directie.Dreapta)
                {
                    inf.Remove(inf[inf.Count - 2]);
                }
            }

            inf.Remove(inf[0]);
            inf.Remove(inf[inf.Count - 1]);
            final = new Point[inf.Count + sup.Count];
            int indexIntrodus = 0;
            foreach (Point p in inf)
            {
                final[indexIntrodus] = p;
                indexIntrodus++;
            }
            foreach (Point p in sup)
            {
                final[indexIntrodus] = p;
                indexIntrodus++;
            }
            return final;
        }
    }
}
