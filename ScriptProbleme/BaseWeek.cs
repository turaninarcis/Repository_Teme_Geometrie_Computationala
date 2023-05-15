using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;

namespace Repository_Teme_Geometrie_Computationala
{
    public class BaseWeek
    {
        internal MainWindow mainWindow;
        internal List<Action> ProblemMethodsList = new List<Action>();

        internal Bitmap bitmap;
        internal Helper helper;
        internal Graphics graphics;
        public BaseWeek(MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            System.Windows.Controls.Image image = mainWindow.image;
            bitmap = new Bitmap(200,200);   
            helper = new Helper(bitmap);
            graphics = Graphics.FromImage(bitmap);
        }
        public Action GetMethod(int ProblemNumber)
        {
            return ProblemMethodsList[ProblemNumber];
        }
    }
}
