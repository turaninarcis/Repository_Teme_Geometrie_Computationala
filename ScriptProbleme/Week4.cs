using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Drawing = System.Drawing;
using System.Windows;
using System.Windows.Threading;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    class Week4:BaseWeek
    {


        public Week4(MainWindow mainWindow):base(mainWindow) {
            ProblemMethodsList.Add(AssignProblem1);

        }
        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            Drawing.Point[] puncte = helper.GenerarePuncteAleatorii(200);
            helper.DesenarePunctePeFormular(puncte, Drawing.Pens.Red, 4);
            helper.SortarePuncte(puncte);
            Drawing.Point[] invelitoare = GetPerimeter(puncte);
            helper.DesenareLiniiIntrePuncte(invelitoare);
        }

        private Drawing.Point[] GetPerimeter(Drawing.Point[] puncte)
        {
            List<Drawing.Point> sup = new List<Drawing.Point>();
            List<Drawing.Point> inf = new List<Drawing.Point>();
            Drawing.Point[] final;

            sup.Add(puncte[0]);
            sup.Add(puncte[1]);

            for (int i = 2; i < puncte.Length; i++)
            {
                sup.Add(puncte[i]);
                while (sup.Count > 2 && helper.GetDirection(sup[sup.Count - 1], sup[sup.Count - 2], sup[sup.Count - 3]) != Helper.Directie.Dreapta)
                {
                    sup.Remove(sup[sup.Count - 2]);
                }
            }

            inf.Add(puncte[puncte.Length - 1]);
            inf.Add(puncte[puncte.Length - 2]);
            for (int i = puncte.Length - 3; i >= 0; i--)
            {
                inf.Add(puncte[i]);
                while (inf.Count > 2 && helper.GetDirection(inf[inf.Count - 1], inf[inf.Count - 2], inf[inf.Count - 3]) != Helper.Directie.Dreapta)
                {
                    inf.Remove(inf[inf.Count - 2]);
                }
            }

            inf.Remove(inf[0]);
            inf.Remove(inf[inf.Count - 1]);
            final = new Drawing.Point[inf.Count + sup.Count];
            int indexIntrodus = 0;
            foreach (Drawing.Point p in inf)
            {
                final[indexIntrodus] = p;
                indexIntrodus++;
            }
            foreach (Drawing.Point p in sup)
            {
                final[indexIntrodus] = p;
                indexIntrodus++;
            }
            return final;
        }
    }
}
