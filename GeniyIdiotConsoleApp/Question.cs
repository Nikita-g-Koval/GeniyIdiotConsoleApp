using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeniyIdiotConsoleApp
{
    internal class Question
    {
        public string Text { get; }
        public int Answer { get; }

        public Question(string text, int answer)
        {
            Text = text;
            Answer = answer;
        }
    }
}
