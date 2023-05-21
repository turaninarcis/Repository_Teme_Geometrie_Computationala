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
        public static MouseButtonEventHandler leftMouseEventHandler;
        public static MouseButtonEventHandler rightMouseEventHandler;

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
            ResetHelper();
        }
        public void ResetHelper()
        {
            helper = new Helper(this);
            ResetMouseClicks();
            mainWindow.canvas.Children.Clear();
        }
        public void ResetMouseClicks()
        {
            try
            {
                mainWindow.canvas.MouseLeftButtonDown -= leftMouseEventHandler;
                mainWindow.canvas.MouseRightButtonDown -= rightMouseEventHandler;
            }
            catch (System.Exception)
            {
            }
        }

    }
}
