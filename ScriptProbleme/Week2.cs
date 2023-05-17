using System;
using System.Collections.Generic;
using Drawing = System.Drawing;
using System.Drawing;

using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Security.Cryptography;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    public class Week2:BaseWeek
    {
        public Week2(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(AssignProblem1);
            ProblemMethodsList.Add(AssignProblem2);
            ProblemMethodsList.Add(AssignProblem3);
        }
        public void AssignProblem1(object obj, RoutedEventArgs e)
        {
            Exercitiu_1(200, 100);

        }
        public void AssignProblem2(object obj, RoutedEventArgs e)
        {
            Exercitiu_2(200);

        }
        public void AssignProblem3(object obj, RoutedEventArgs e)
        {
            Exercitiu_3(200);
        }
        private void Exercitiu_1(int nrPuncte, int distanta)
        {
            Drawing.Point punctPrincipal = helper.GenerarePunctAleatoriu();
            Drawing.Point[] puncteSecundare = helper.GenerarePuncteAleatorii(nrPuncte);
            double auxDistanta;
            int razaMaxima = distanta;
            int marimePunct = 4;
            Pen penPunctPrincipal = new Pen(Color.Blue, marimePunct);
            Pen penPunctApropiat = new Pen(Color.Red, marimePunct);
            Pen penPunctDepartat = new Pen(Color.Black, marimePunct);
            foreach (Drawing.Point point in puncteSecundare)
            {
                auxDistanta = helper.DistantaIntreDouaPuncte(punctPrincipal, point);
                if (auxDistanta <= distanta)
                {
                    helper.DesenarePunctPeFormular(point,penPunctApropiat, marimePunct);
                }
                else
                {
                    helper.DesenarePunctPeFormular(point, penPunctDepartat, marimePunct);
                }
            }
            graphics.DrawEllipse(penPunctPrincipal, (float)(punctPrincipal.X - razaMaxima), (float)(punctPrincipal.Y - razaMaxima), (float)(razaMaxima * 2), (float)(razaMaxima * 2));

            helper.DesenarePunctPeFormular(punctPrincipal, penPunctPrincipal, marimePunct);
        }

        private void Exercitiu_2(int numarPuncte)
        {

            Drawing.Point[] puncte = helper.GenerarePuncteAleatorii(numarPuncte);
            helper.DesenarePunctePeFormular(puncte, new Pen(Color.Black, 2), 2);
            Drawing.Point[] puncteArieMinima = new Drawing.Point[3];
            double aria = int.MaxValue;
            double ariaAux;
            double perimetru = int.MaxValue;
            double perimetruAux;
            for (int i = 0; i < numarPuncte; i++)
            {
                for (int j = i + 1; j < numarPuncte; j++)
                {
                    for (int k = j + 1; k < numarPuncte; k++)
                    {
                        //ariaAux = helper.AriaUnuiTriunghi(puncte[i], puncte[j], puncte[k]);
                        perimetruAux = helper.PerimetrulUnuiTriunghi(puncte[i], puncte[j], puncte[k]);
                        if (perimetruAux < perimetru)
                        {
                            //aria = ariaAux;
                            perimetru = perimetruAux;
                            puncteArieMinima[0] = puncte[i];
                            puncteArieMinima[1] = puncte[j];
                            puncteArieMinima[2] = puncte[k];
                        }
                    }
                }
            }
            Pen penLinie = new Pen(Color.Red, 2);
            helper.DesenareTriunghi(puncteArieMinima, penLinie);
        }

        private void Exercitiu_3(int numarPuncte)
        {
            Drawing.Point[] puncte = helper.GenerarePuncteAleatoriiConstrained(numarPuncte, 100, 400, 100, 300);
            Drawing.Pen pen = new Drawing.Pen(Color.Black, 3);
            helper.DesenarePunctePeFormular(puncte,pen,2);
            Helper.Circle circle= new Helper.Circle();
            circle = Helper.MakeCircle(puncte);

            graphics.DrawEllipse(Drawing.Pens.Red, (int)circle.center.x-(int)circle.radius, (int)circle.center.y-(int)circle.radius, (int)circle.radius*2, (int)circle.radius*2);
        }
    }
}
