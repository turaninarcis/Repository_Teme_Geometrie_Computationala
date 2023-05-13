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
        Week week;
        StackPanel stackPanelProblems;
        MainWindow mainWindow;
        public WeekButton(int index,MainWindow mainWindow)
        {
            this.mainWindow = mainWindow;
            Name = "buttonWeek" + index;
            Content = "Week " + index;
            Height = 50;
            week = new Week(index,mainWindow);
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
