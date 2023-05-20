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
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem2);
        }

        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            pointList.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePointOnClick;
            rightMouseEventHandler = helper.CreateLineBetweenLastPoints;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }
    



    }
}
