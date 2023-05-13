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
        public ProblemButton(Problem problem) 
        {
            this.problem = problem;
        }

        public string ShowProblemDetails()
        {
           return problem.ToString();
        }
    }
}
