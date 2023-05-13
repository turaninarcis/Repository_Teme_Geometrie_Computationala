using System;
using System.Collections.Generic;
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
        string path;
        Week week;
        StackPanel stackPanelProblems;
        MainWindow mainWindow;
        static int NumberOfButtons=0;
        public WeekButton(string path,MainWindow mainWindow)
        {
            this.path = path;
            week = new Week(path,NumberOfButtons);
            week.SetBaseWeek(mainWindow.GetWeek(NumberOfButtons));
            NumberOfButtons++;
            this.mainWindow = mainWindow;
            stackPanelProblems = mainWindow.SetProblemsPanel;
            Click += AssignProblemButtons;
        }
       
        public void AssignProblemButtons(object obj,RoutedEventArgs e)
        {
            stackPanelProblems.Children.Clear();
            int nrOfButtons = week.GetNumberOfProblems();
            Problem[] problems = week.GetProblems();
            for (int i = 1; i <= nrOfButtons; i++)
            {
                ProblemButton button = new ProblemButton(problems[i-1],i);
                button.Click += AssignTextToTextBlock;
                stackPanelProblems.Children.Add(button);
            }
        }

        public void AssignTextToTextBlock(object obj, RoutedEventArgs e)
        {
            mainWindow.ProblemDetailsText.Text = (obj as ProblemButton).ShowProblemDetails();
            
        }
    }
}
