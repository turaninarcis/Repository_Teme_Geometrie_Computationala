using System.Windows;
using System.Windows.Media;

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
            Point punctPrincipal = helper.GenerarePunctAleatoriu();
            Point[] puncteSecundare = helper.GenerarePuncteAleatorii(nrPuncte);
            double auxDistanta;
            int razaMaxima = distanta;
            int marimePunct = 4;
            Pen penPunctPrincipal = new Pen(Brushes.Blue, marimePunct);
            Pen penPunctApropiat = new Pen(Brushes.Red, marimePunct);
            Pen penPunctDepartat = new Pen(Brushes.Black, marimePunct);
            foreach (Point point in puncteSecundare)
            {
                auxDistanta = Helper.DistantaIntreDouaPuncte(punctPrincipal, point);
                if (auxDistanta <= distanta)
                {
                    helper.DesenarePunctPeFormular(point,penPunctApropiat);
                }
                else
                {
                    helper.DesenarePunctPeFormular(point, penPunctDepartat);
                }
            }
            helper.DesenareCerc(new Point(punctPrincipal.X, punctPrincipal.Y), razaMaxima, penPunctPrincipal);

            helper.DesenarePunctPeFormular(punctPrincipal, penPunctPrincipal);
        }

        private void Exercitiu_2(int numarPuncte)
        {

            Point[] puncte = helper.GenerarePuncteAleatorii(numarPuncte);
            helper.DesenarePunctePeFormular(puncte, commonPointPen);
            Point[] puncteArieMinima = new Point[3];
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
                        perimetruAux = Helper.PerimetrulUnuiTriunghi(puncte[i], puncte[j], puncte[k]);
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
            Pen penLinie = new Pen(Brushes.Red, 2);
            helper.DesenareTriunghi(puncteArieMinima, penLinie);
        }

        private void Exercitiu_3(int numarPuncte)
        {
            int Width = (int)mainWindow.canvas.ActualWidth/2;
            int Height = (int)mainWindow.canvas.ActualHeight/2;
            Point[] puncte = helper.GenerarePuncteAleatoriiConstrained(numarPuncte, Width-100, Width+100, Height-100, Height+100);
            helper.DesenarePunctePeFormular(puncte,commonPointPen);
            Helper.Circle circle= new Helper.Circle();
            circle = Helper.MakeCircle(puncte);
            helper.DesenareCerc(new Point(circle.center.x, circle.center.y), circle.radius, new Pen(Brushes.Red, 1));
        }
    }
}
