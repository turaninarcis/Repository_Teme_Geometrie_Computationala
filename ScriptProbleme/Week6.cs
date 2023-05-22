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
            int nrPuncte = 203;
            pointList.Clear();
            for(int i = 0;i<nrPuncte;i++)
            {
                pointList.Add(helper.GenerarePunctAleatoriu());
            }
            helper.TriangularePuncteDinLista(pointList);
            helper.DesenarePunctePeFormular(pointList.ToArray(), new Pen(Brushes.Black, 7));

        }



        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            helper.HandlePointOnClick(pointList);
        }
    
        public void AssignProblem3(object obj, RoutedEventArgs e)
        {
            helper.HandlePointOnClick(pointList);
            rightMouseEventHandler = helper.TriangularePoligonConvex;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }




    }
}
