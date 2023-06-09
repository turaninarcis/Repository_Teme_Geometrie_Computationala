﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Windows.Threading;
using static Repository_Teme_Geometrie_Computationala.Helper;

namespace Repository_Teme_Geometrie_Computationala.ScriptProbleme
{
    public class Week9 : BaseWeek
    {
        List<Point> points;
        List<Point> sortedPoints;
        List<Helper.Segment> laturiPoligon;
        List<Helper.Segment> diagonale = new List<Helper.Segment>();
        DispatcherTimer timer = new DispatcherTimer();
        Ellipse ellipse = new Ellipse();

        int indexTimer = 0;



        public Week9(MainWindow mainWindow) : base(mainWindow)
        {
            ProblemMethodsList.Add(Problema1);
            points = new List<Point>();
            sortedPoints = new List<Point>();
            timer.Interval = TimeSpan.FromMilliseconds(50);
            timer.Tick += timer_Tick;
        }

        public void Problema1(object obj, RoutedEventArgs e)
        {
            HandlePointOnClick(points);
        }




        public void HandlePointOnClick(List<Point> pointList)
        {
            pointList.Clear();
            helper.AssignPointList(pointList);
            ResetMouseClicks();
            leftMouseEventHandler = helper.CreatePoligonOnClick;
            mainWindow.canvas.MouseLeftButtonDown += leftMouseEventHandler;
            rightMouseEventHandler = PartitionarePoligon;
            mainWindow.canvas.MouseRightButtonDown += rightMouseEventHandler;
        }


        public void PartitionarePoligon(object sender, MouseButtonEventArgs e)
        {
            diagonale.Clear();
            CopyPoints();
            helper.CreateLineBetweenLastPoints(sender, e);
            laturiPoligon = Helper.CreazaLaturiDinPuncte(points);
            Helper.Segment segment;
            for (int i = 1; i <= points.Count; i++)
            {
                int iNormalised = i % points.Count;
                int excessOne = (i + 1) % points.Count;
                if (Helper.GetDirection(points[i-1],points[iNormalised], points[excessOne])==Directie.Stanga)
                {
                    bool ok;
                    if (points[i - 1].Y > points[iNormalised].Y && points[excessOne].Y > points[iNormalised].Y) 
                    {
                        int index = sortedPoints.IndexOf(points[iNormalised])-1;
                        do
                        {
                            if (index < 0)
                                break;

                            ok = true;
                            segment = new Segment(points[iNormalised], sortedPoints[index]);

                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else { ok = false; index--;}
                        } while (ok == false);
                        
                    }
                    if (points[i - 1].Y < points[iNormalised].Y && points[excessOne].Y < points[iNormalised].Y)
                    {
                        int index = sortedPoints.IndexOf(points[iNormalised])+1;
                        do
                        {
                            if(index>=points.Count)
                                break;
                            ok = true;
                            segment = new Segment(points[iNormalised], sortedPoints[index]);

                            if (!Helper.IntersecteazaOricareLatura(segment, laturiPoligon) && SeAflaInInterior(points, iNormalised, index, points.Count))
                            {
                                diagonale.Add(segment);
                            }
                            else { ok = false; index++; 
                            }
                        } while (ok == false);
                    }
                }
            }

            PlayAnimation();
        }

        public bool DiagonalaExistaDeja(Segment segment)
        {
            for(int i = 0;i<diagonale.Count;i++)
            {
                if (diagonale[i].a == segment.a && diagonale[i].b == segment.b || diagonale[i].b == segment.a && diagonale[i].a == segment.b)
                    return true;
            }
            return false;
        }
        public void CopyPoints()
        {
            sortedPoints.Clear();
            foreach (Point point in points) 
            {
                sortedPoints.Add(point);
            }
            Helper.SortarePuncteY(sortedPoints);
        }

        void timer_Tick(object sender, EventArgs e)
        {
            if (indexTimer < diagonale.Count)
            {
                helper.DesenareLinie(diagonale[indexTimer].a, diagonale[indexTimer].b, new Pen(Brushes.Red, 2));
                indexTimer++;
            }
            else
                timer.Stop();
        }
        private void PlayAnimation()
        {

            indexTimer = 0;
            timer.Start();
        }

        public bool SeAflaInInterior(List<Point> points, int i, int j, int n)
        {
            Directie Varf;
            Directie primaDirectie;
            Directie aDouaDirectie;
            int pointPlusUnu = (i + 1) % (n);
            int pointMinusUnu; 
            if(i-1<0)
                pointMinusUnu = n-1;
            else pointMinusUnu=i-1;
            Varf = GetDirection(points[pointMinusUnu], points[i], points[pointPlusUnu]);
            primaDirectie = GetDirection(points[i], points[j], points[pointPlusUnu]);
            aDouaDirectie = GetDirection(points[i], points[pointMinusUnu], points[j]);

            if (Varf == Directie.Dreapta)
            {
                if (!(primaDirectie == Directie.Stanga && aDouaDirectie == Directie.Stanga))
                    return false;
            }
            else if (Varf == Directie.Stanga)
            {
                if (primaDirectie == Directie.Dreapta && aDouaDirectie == Directie.Dreapta)
                    return false;
            }
            return true;
        }
    }
}
