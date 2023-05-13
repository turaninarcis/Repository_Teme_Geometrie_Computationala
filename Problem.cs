using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Repository_Teme_Geometrie_Computationala
{
    internal class Problem
    {
        
        string problemDescription;
        string problemSolvingDescription;
        Action problemAction;
        public Problem(string problemDescription,string problemSolvingDescription,Action problem)
        {
            this.problemDescription = problemDescription;
            this.problemSolvingDescription = problemSolvingDescription;
            problemAction= problem;

        }

        private void UseProblem(object obj, RoutedEventArgs e)
        {
            problemAction();
        }

        public string ToString()
        {
            return problemDescription +"\r\n" +problemSolvingDescription;
        }
    }
}
