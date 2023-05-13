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
        BaseWeek baseWeek;
        int index;
        public Week(string path,int index) 
        {
           this.path = path;
           this.index = index;
           ExtractProblems();
        }
        public void SetBaseWeek(BaseWeek baseWeek)
        {
           this.baseWeek = baseWeek;
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
                        problems[indexProblem] = new Problem(bufferText[0], bufferText[1],baseWeek.GetMethod(index));
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
