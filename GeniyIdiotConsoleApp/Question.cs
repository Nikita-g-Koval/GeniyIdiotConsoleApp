using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniyIdiotConsoleApp
{
    internal class Question
    {
        public string text { get; }
        public int answer { get; }

        public Question(string text, int answer)
        {
            this.text = text;
            this.answer = answer;
        }
    }
}
