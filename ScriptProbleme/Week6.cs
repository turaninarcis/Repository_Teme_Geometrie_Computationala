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
        bool Enter_Pressed;
        List<Point> points= new List<Point>();
        public Week6(MainWindow mainWindow) : base(mainWindow)
        {
            mouseEventHandler = MouseButtonPressed;
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem2);

        }

        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            Problema_2();
        }


        public void Problema_2()
        {
            mainWindow.canvas.MouseLeftButtonDown += MouseButtonPressed;

        }
        private void Window_Key_Pressed(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter) 
            {
                Enter_Pressed = true;
            }
           
        }
        private void MouseButtonPressed(object sender, MouseButtonEventArgs e)
        {
            
                var position = Mouse.GetPosition(mainWindow.canvas);
                Point point = new Point((int)position.X, (int)position.Y);
                points.Add(point);
                helper.DesenarePunctPeFormular(point, new Pen(Brushes.Black, 4));
            
        }
    }
}
