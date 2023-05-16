using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using Drawing = System.Drawing;

namespace Repository_Teme_Geometrie_Computationala
{
    public class BaseWeek
    {
        internal MainWindow mainWindow;
        public List<RoutedEventHandler> ProblemMethodsList;

        internal Bitmap bitmap;
        internal Helper helper;
        internal Graphics graphics;
 
        public BaseWeek(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            System.Windows.Controls.Image image = mainWindow.image;
            ResetBitmap();
            ProblemMethodsList = new List<RoutedEventHandler>();
        }
        public RoutedEventHandler GetMethod(int ProblemNumber)
        {
            return ProblemMethodsList[ProblemNumber];
        }

        public void ResetBitmap(object obj, RoutedEventArgs e)
        {
            bitmap = new Bitmap((int)mainWindow.Width, (int)mainWindow.Height);
            graphics = Graphics.FromImage(bitmap);
            helper = new Helper(this);
        }
        public void ResetBitmap()
        {
            bitmap = new Bitmap((int)mainWindow.Width, (int)mainWindow.Height);
            graphics = Graphics.FromImage(bitmap);
            helper = new Helper(this);
        }

        public void AssignImage(object obj, RoutedEventArgs e)
        {
            ImageSource imageSource = BitmapConverter.ImageSourceFromBitmap(bitmap);
            mainWindow.image.Source = imageSource;
        }
        public void AssignImage()
        {
            ImageSource imageSource = BitmapConverter.ImageSourceFromBitmap(bitmap);
            mainWindow.image.Source = imageSource;
        }





    }
}
