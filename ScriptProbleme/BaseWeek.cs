using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

namespace Repository_Teme_Geometrie_Computationala
{
    public class BaseWeek
    {
        internal MainWindow mainWindow;
        public List<RoutedEventHandler> ProblemMethodsList;
        internal static MouseButtonEventHandler mouseEventHandler;
        internal Helper helper;
        internal static int marimePen = 5;
        internal Pen commonPointPen = new Pen(Brushes.Black, marimePen);
        public BaseWeek(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            ResetHelper();
            ProblemMethodsList = new List<RoutedEventHandler>();
        }
        public RoutedEventHandler GetMethod(int ProblemNumber)
        {
            return ProblemMethodsList[ProblemNumber];
        }

        public void ResetHelper(object obj, RoutedEventArgs e)
        {
            helper = new Helper(this);
            try
            {
                mainWindow.canvas.MouseLeftButtonDown -=mouseEventHandler;
            }
            catch (System.Exception)
            {
            }

            mainWindow.canvas.Children.Clear();
        }
        public void ResetHelper()
        {
            helper = new Helper(this);
            Mouse.OverrideCursor = null; ;
            mainWindow.canvas.Children.Clear();
        }



    }
}
