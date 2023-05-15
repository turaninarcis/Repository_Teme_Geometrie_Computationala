using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Repository_Teme_Geometrie_Computationala
{
    internal class ProblemButton:Button
    {
        Problem problem;
        int index;
        public ProblemButton(Problem problem,int index) 
        {
            this.problem = problem;
            this.index = index;
            Init();

        }
        private void Init()
        {
            Name = "buttonProblem" + index;
            Content = "Problem " + index;
            FontSize= 16;
            Height = 70;
        }
        public string ShowProblemDetails()
        {
           return problem.ToString();
        }

    }
}
