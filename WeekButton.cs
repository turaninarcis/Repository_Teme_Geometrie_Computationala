using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Repository_Teme_Geometrie_Computationala
{
    internal class WeekButton:Button
    {
        StackPanel stackPanelProblems;
        MainWindow mainWindow;
        Problem[] problems;
        string path;
        int index;
        public WeekButton(int index,MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            this.index = index;
            path = $"..\\..\\..\\Probleme\\Saptamana{index}.txt";
            Name = "buttonWeek" + index;
            Content = "Week " + index;
            Height = 50;
            stackPanelProblems = mainWindow.SetProblemsPanel;
            ExtractProblems();
            Click += AssignProblemButtons;
        }
        
        public void AssignProblemButtons(object obj,RoutedEventArgs e)
        {
            stackPanelProblems.Children.Clear();
            int nrOfButtons = problems.Length;
            for (int i = 1; i <= nrOfButtons; i++)
            {
                ProblemButton button = new ProblemButton(problems[i-1],i);
                button.Click += AssignTextToTextBlock;
                stackPanelProblems.Children.Add(button);
            }
        }

        private void ExtractProblems()
        {
            string buffer;
            string[] bufferText = new string[2];
            int indexProblem = 0;
            int indexText = 0;
            using (StreamReader sr = new StreamReader(path))
            {
                problems = new Problem[int.Parse(sr.ReadLine())];
                while ((buffer = sr.ReadLine()) != null)
                {
                    if (buffer == "#")
                    {
                        indexText++;
                    }
                    else if (buffer == "*")
                    {
                        problems[indexProblem] = new Problem(bufferText[0], bufferText[1]);
                        bufferText[0] = "";
                        bufferText[1] = "";
                        indexText = 0;
                        indexProblem++;
                    }
                    else
                        bufferText[indexText] += buffer + "\n\r";
                }
            }
        }      

        public void AssignTextToTextBlock(object obj, RoutedEventArgs e)
        {
            mainWindow.ProblemDetailsText.Text = (obj as ProblemButton).ShowProblemDetails();      
        }
    }
}
