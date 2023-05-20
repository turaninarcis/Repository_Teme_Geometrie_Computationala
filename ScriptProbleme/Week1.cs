using System;
using System.Windows;
using System.Windows.Media;
namespace Repository_Teme_Geometrie_Computationala
{
    internal class Week1:BaseWeek
    {

        public Week1(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(AssignProblem1);
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem3);
        }
        public void AssignProblem1(object obj, RoutedEventArgs e) 
        {
            Exercitiu_1(200);

        }
        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            Exercitiu_2(200,200);

        }
        public void AssignProblem3(object obj, RoutedEventArgs e)
        {
            Exercitiu_3(300,new Point(100,100));
        }
        private void Exercitiu_1(int numarPuncte)
        {
            int MultiplicatorMarime = 3;
            Random random = new Random();

            int indentation = 30;
            double MaxSizeX = mainWindow.canvas.ActualWidth - indentation;
            double MaxSizeY = mainWindow.canvas.ActualHeight - indentation;
            double marimeCerc = MultiplicatorMarime;

            double xMin = mainWindow.canvas.ActualWidth;
            double yMin = mainWindow.canvas.ActualHeight;
            double xMax = 0;
            double yMax = 0;
            double x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(indentation, (int)MaxSizeX);
                if (x < xMin) { xMin = x; }
                else if (x > xMax) { xMax = x; }
                y = random.Next(indentation, (int)MaxSizeY);
                if (y < yMin) { yMin = y; }
                else if (y > yMax) { yMax = y; }
                helper.DesenareCerc(new Point(x, y), marimeCerc/2, commonPointPen);
            }
            commonPointPen.Brush = Brushes.Red;
            helper.DesenareDreptunghi(xMin, yMin, xMax, yMax);
        }

        private void Exercitiu_2(int numarPuncte1, int numarPuncte2)
        {
            commonPointPen.Brush = Brushes.Red;
            Point[] primaGrupa = helper.GenerarePuncteAleatorii(numarPuncte1);
            helper.DesenarePunctePeFormular(primaGrupa, commonPointPen);


            commonPointPen.Brush = Brushes.Blue;
            Point[] aDouaGrupa = helper.GenerarePuncteAleatorii(numarPuncte2);
            helper.DesenarePunctePeFormular(aDouaGrupa, commonPointPen);

            commonPointPen.Brush = Brushes.Black;
            double distantaDintrePuncte;
            double distantaMinimaDintrePuncte;
            int ContorPozitie = 0;


            for (int i = 0; i < numarPuncte1; i++)
            {
                distantaMinimaDintrePuncte = Int32.MaxValue;
                for (int j = 0; j < numarPuncte2; j++)
                {
                    distantaDintrePuncte = Helper.DistantaIntreDouaPuncte(primaGrupa[i], aDouaGrupa[j]);
                    if (distantaDintrePuncte < distantaMinimaDintrePuncte)
                    {
                        distantaMinimaDintrePuncte = distantaDintrePuncte;
                        ContorPozitie = j;
                    }
                }
                helper.DesenareLinie(primaGrupa[i], aDouaGrupa[ContorPozitie], commonPointPen);
            }
        }

        private void Exercitiu_3(int numarPuncte, Point CentrulCercului)
        {
            int MultiplicatorMarime = 4;
            Pen pen = new Pen(Brushes.Black, MultiplicatorMarime);

            Point[] puncte = helper.GenerarePuncteAleatorii(numarPuncte);
            helper.DesenarePunctePeFormular(puncte, pen);

            double razaMaxima = int.MaxValue;
            double razaCurenta;
            foreach (Point point in puncte)
            {
                razaCurenta = Helper.DistantaIntreDouaPuncte(point, CentrulCercului);
                if (razaCurenta < razaMaxima)
                    razaMaxima = razaCurenta;
            }

            pen.Brush = Brushes.Red;
            helper.DesenareCerc(CentrulCercului, razaMaxima, pen);
        }
    }
}
