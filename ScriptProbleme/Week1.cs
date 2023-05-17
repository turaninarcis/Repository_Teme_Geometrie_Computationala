using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Drawing = System.Drawing;
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
            Exercitiu_3(300,new Drawing.Point(100,100));
        }
        private void Exercitiu_1(int numarPuncte)
        {
            int MultiplicatorMarime = 3;
            Drawing.Pen pen = new Drawing.Pen(Drawing.Color.Black, MultiplicatorMarime);
            Random random = new Random();

            int indentation = 30;
            int MaxSizeX = bitmap.Width - indentation;
            int MaxSizeY = bitmap.Height - indentation;
            int marimeCerc = MultiplicatorMarime;

            int xMin = bitmap.Width;
            int yMin = bitmap.Height;
            int xMax = 0;
            int yMax = 0;
            int x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(indentation, MaxSizeX);
                if (x < xMin) { xMin = x; }
                else if (x > xMax) { xMax = x; }
                y = random.Next(indentation, MaxSizeY);
                if (y < yMin) { yMin = y; }
                else if (y > yMax) { yMax = y; }

                graphics.DrawEllipse(pen, x - marimeCerc / 2, y - marimeCerc / 2, marimeCerc, marimeCerc);
            }
            Rectangle dreptunghi = helper.FormareDreptunghiCuDouaPuncte(xMin, yMin, xMax, yMax);
            pen.Color = Drawing.Color.Red;
            graphics.DrawRectangle(pen, dreptunghi);
        }

        private void Exercitiu_2(int numarPuncte1, int numarPuncte2)
        {

            int MultiplicatorMarime = 3;

            Drawing.Pen pen = new Drawing.Pen(Drawing.Color.Red, MultiplicatorMarime);
            Drawing.Point[] primaGrupa = helper.GenerarePuncteAleatorii(numarPuncte1);
            helper.DesenarePunctePeFormular(primaGrupa, pen, MultiplicatorMarime);


            pen.Color = Drawing.Color.Blue;
            Drawing.Point[] aDouaGrupa = helper.GenerarePuncteAleatorii(numarPuncte2);
            helper.DesenarePunctePeFormular(aDouaGrupa, pen, MultiplicatorMarime);

            pen.Color = Drawing.Color.Black;
            double distantaDintrePuncte;
            double distantaMinimaDintrePuncte;
            int ContorPozitie = 0;


            for (int i = 0; i < numarPuncte1; i++)
            {
                distantaMinimaDintrePuncte = Int32.MaxValue;
                for (int j = 0; j < numarPuncte2; j++)
                {
                    distantaDintrePuncte = helper.DistantaIntreDouaPuncte(primaGrupa[i], aDouaGrupa[j]);
                    if (distantaDintrePuncte < distantaMinimaDintrePuncte)
                    {
                        distantaMinimaDintrePuncte = distantaDintrePuncte;
                        ContorPozitie = j;
                    }
                }

                graphics.DrawLine(pen, primaGrupa[i], aDouaGrupa[ContorPozitie]);
            }
        }

        private void Exercitiu_3(int numarPuncte, Drawing.Point CentrulCercului)
        {
            int MultiplicatorMarime = 4;
            Drawing.Pen pen = new Drawing.Pen(Drawing.Color.Red, MultiplicatorMarime);

            Drawing.Point[] puncte = helper.GenerarePuncteAleatorii(numarPuncte);
            helper.DesenarePunctePeFormular(puncte, pen, MultiplicatorMarime);

            double razaMaxima = int.MaxValue;
            double razaCurenta;
            foreach (Drawing.Point point in puncte)
            {
                razaCurenta = helper.DistantaIntreDouaPuncte(point, CentrulCercului);
                if (razaCurenta < razaMaxima)
                    razaMaxima = razaCurenta;
            }

            pen.Color = Drawing.Color.Blue;
            graphics.DrawEllipse(pen, (float)(CentrulCercului.X - razaMaxima), (float)(CentrulCercului.Y - razaMaxima), (float)(razaMaxima * 2), (float)(razaMaxima * 2));
        }
    }
}
