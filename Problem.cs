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
        public Problem(string problemDescription)
        {
            this.problemDescription = problemDescription;
        }
        public string ToString()
        {
            return problemDescription;
        }

    }
}
