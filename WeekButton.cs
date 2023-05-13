using Repository_Teme_Geometrie_Computationala.User_Controls;
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
        StackPanel stackPanelButoane;
        MainWindow mainWindow;
        static int NumberOfButtons=0;
        public WeekButton(string path,MainWindow mainWindow)
        {
            this.path = path;
            week = new Week(path,NumberOfButtons);
            week.SetBaseWeek(mainWindow.GetWeek(NumberOfButtons));
            NumberOfButtons++;
            this.stackPanelButoane = stackPanelButoane;
            this.mainWindow = mainWindow;
            stackPanelButoane = mainWindow.SetProblemsPanel.stackPanelButoane;
            Click += AssignProblemButtons;
        }
       
        public void AssignProblemButtons(object obj,RoutedEventArgs e)
        {
            stackPanelButoane.Children.Clear();
            int nrOfButtons = week.GetNumberOfProblems();
            Problem[] problems = week.GetProblems();
            for (int i = 1; i <= nrOfButtons; i++)
            {
                ProblemButton button = new ProblemButton(problems[i-1]);
                button.Click += AssignTextToTextBlock;
                button.Name = "buttonProblem" + i;
                button.Content = "Problem " + i;
                button.Height = 70;
                stackPanelButoane.Children.Add(button);
            }
        }

        public void AssignTextToTextBlock(object obj, RoutedEventArgs e)
        {
            mainWindow.ProblemDetailsText.Text = (obj as ProblemButton).ShowProblemDetails();
            
        }
    }
}
