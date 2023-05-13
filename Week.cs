using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository_Teme_Geometrie_Computationala
{
    internal class Week
    {
        string path;
        Problem[] problems;
        int index;
        public Week(int index,MainWindow mainWindow) 
        {
           path = $"..\\..\\..\\Probleme\\Saptamana{index}.txt";
           this.index = index;
           ExtractProblems();
        }
        public Problem[] GetProblems()
        {
            return problems;
        }
        public int GetNumberOfProblems() { return problems.Length; }
        private void ExtractProblems()
        {
            string buffer;
            string[] bufferText=new string[2];
            int indexProblem = 0;
            int indexText = 0;
            using(StreamReader sr = new StreamReader(path)) 
            {
                problems= new Problem[int.Parse(sr.ReadLine())];
                while((buffer=sr.ReadLine())!=null)
                {
                    if (buffer == "#") 
                    {
                        indexText++;
                    }
                    else if(buffer=="*")
                    {
                        problems[indexProblem] = new Problem(bufferText[0], bufferText[1]);
                        bufferText[0] = "";
                        bufferText[1] = "";
                        indexText= 0;
                        indexProblem++;
                    }
                    else
                        bufferText[indexText] += buffer+"\n\r";
                }
            }
        }



    }
}
