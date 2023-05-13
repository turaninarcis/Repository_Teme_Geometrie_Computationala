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
        public Problem(string problemDescription,string problemSolvingDescription)
        {
            this.problemDescription = problemDescription;
            this.problemSolvingDescription = problemSolvingDescription;
        }

        public string ToString()
        {
            return problemDescription +"\r\n" +problemSolvingDescription;
        }
    }
}
