using System;
using System.Collections.Generic;
using System.Drawing;
using Drawing = System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Threading;
using System.Windows;

namespace Repository_Teme_Geometrie_Computationala
{
    public class Helper
    {
        Bitmap BitmapToBeUsed;
        Random random = new Random();
        int marimePen = 3;
        Graphics graphics;
        int indexPunct = 0;
        Drawing.Point[] puncte;
        DispatcherTimer timer = new DispatcherTimer();
        BaseWeek baseWeek;
        public Helper(BaseWeek baseWeek)
        {
            this.baseWeek = baseWeek;
            BitmapToBeUsed = baseWeek.bitmap;
            this.graphics = baseWeek.graphics;
            timer.Interval = TimeSpan.FromMilliseconds(70);
            timer.Tick += timer_Tick;
        }


        public void DrawConnectingLines(Drawing.Point[] puncte)
        {
            this.puncte = puncte;
            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (indexPunct < puncte.Length - 1)
            {
                graphics.DrawLine(new Drawing.Pen(Drawing.Color.Black, 2), puncte[indexPunct], puncte[indexPunct + 1]);
                indexPunct++;
            }
            else
            {
                graphics.DrawLine(new Drawing.Pen(Drawing.Color.Black, 2), puncte[0], puncte[puncte.Length - 1]);
                indexPunct = 0;
                timer.Stop();
            }
            baseWeek.AssignImage();
        }

        public Drawing.Point[] GenerarePuncteAleatorii(int numarPuncte)
        {
            Random random = new Random();
            Drawing.Point[] puncte = new Drawing.Point[numarPuncte];
            int x, y;
            for (int i = 0; i < numarPuncte; i++)
            {
                x = random.Next(0, BitmapToBeUsed.Width - 10);
                y = random.Next(0, BitmapToBeUsed.Height - 10);
                puncte[i] = new Drawing.Point(x, y);
            }
            return puncte;
        }
        public Drawing.Point GenerarePunctAleatoriu()
        {
            int indentation = 10;
            int x = random.Next(0, BitmapToBeUsed.Width - indentation);
            int y = random.Next(0, BitmapToBeUsed.Height - indentation);
            Drawing.Point punct = new Drawing.Point(x, y);

            return punct;
        }
        public void DesenarePunctPeFormular(Drawing.Point punct, Drawing.Pen pen, int marimePunct)
        {
            graphics.DrawEllipse(pen, (float)(punct.X - marimePunct / 2), (float)(punct.Y - marimePunct / 2), marimePunct, marimePunct);

        }
        public void DesenarePunctePeFormular(Drawing.Point[] puncte, System.Drawing.Pen pen, int marimePunct)
        {
            foreach (Drawing.Point punct in puncte)
            {
                graphics.DrawEllipse(pen, (float)(punct.X - marimePunct / 2), (float)(punct.Y - marimePunct / 2), marimePunct, marimePunct);
            }
        }

        public Rectangle FormareDreptunghiCuDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double latimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x2, y1);
            double inaltimeDreptunghi = DistantaIntreDouaPuncte(x1, y1, x1, y2);
            Rectangle dreptunghi = new Rectangle(x1, y1, (int)latimeDreptunghi, (int)inaltimeDreptunghi);
            return dreptunghi;
        }

        public double DistantaIntreDouaPuncte(int x1, int y1, int x2, int y2)
        {
            double distanta = Math.Sqrt(Math.Pow(x1 - x2, 2) + Math.Pow(y1 - y2, 2));
            return distanta;
        }
        public double DistantaIntreDouaPuncte(Drawing.Point p1, Drawing.Point p2)
        {
            double distanta = Math.Sqrt(Math.Pow(p1.X - p2.X, 2) + Math.Pow(p1.Y - p2.Y, 2));
            return distanta;
        }
        public double AriaUnuiTriunghi(Drawing.Point p1, Drawing.Point p2, Drawing.Point p3)
        {
            double aria;
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double semiperimetru = (a + b + c) / 2;
            aria = Math.Sqrt(semiperimetru * (semiperimetru - a) * (semiperimetru - b) * (semiperimetru - c));
            return aria;
        }
        public double PerimetrulUnuiTriunghi(Drawing.Point p1, Drawing.Point p2, Drawing.Point p3)
        {
            double a = DistantaIntreDouaPuncte(p1, p2);
            double b = DistantaIntreDouaPuncte(p2, p3);
            double c = DistantaIntreDouaPuncte(p1, p3);
            double perimetru = (a + b + c);
            return perimetru;
        }


        public void SortarePuncte(Drawing.Point[] puncte)
        {
            bool ok = true;
            Drawing.Point aux;
            do
            {
                ok = true;
                for (int i = 0; i < puncte.Length - 1; i++)
                {
                    if (puncte[i].X > puncte[i + 1].X)
                    {
                        ok = false;
                        aux = puncte[i];
                        puncte[i] = puncte[i + 1];
                        puncte[i + 1] = aux;
                    }
                    else if (puncte[i].X == puncte[i + 1].X && puncte[i].Y < puncte[i + 1].Y)
                    {
                        ok = false;
                        aux = puncte[i];
                        puncte[i] = puncte[i + 1];
                        puncte[i + 1] = aux;
                    }
                }
            } while (ok == false);
        }

        public void GenerareNumarLangaPuncte(Drawing.Point[] puncte)
        {
            for (int i = 0; i < puncte.Length; i++)
            {
                graphics.DrawString((i + 1).ToString(), new Font("Arial", 10), Brushes.Black, puncte[i]); ;
            }
        }

        public Directie GetDirection(Drawing.Point a, Drawing.Point b, Drawing.Point c)
        {
            int aux1 = a.X * b.Y + b.X * c.Y + a.Y * c.X;
            int aux2 = c.X * b.Y + c.Y * a.X + b.X * a.Y;
            int rezultat = aux1 - aux2;
            if (rezultat < 0)
            {
                return Directie.Stanga;
            }
            else if (rezultat > 0)
            {
                return Directie.Dreapta;
            }
            else return Directie.Linie;
        }
        public enum Directie { Stanga, Linie, Dreapta }

    }
}
