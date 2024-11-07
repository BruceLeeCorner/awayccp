using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwayCCP.Services
{
    public class LineController
    {
        private string newLine;

        public List<string> Words { get; set; }

        private int currWordIndex = 0;
        private int currCharIndex = 0;

        public string NewLine
        {
            get => newLine; set
            {
                newLine = value;
                Words.Clear();
                var wrods = newLine.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                Words.AddRange(wrods);
            }
        }

        public LineController( string newLine)
        {
            Words = new List<string>();
            this.newLine = newLine;
        }

        public bool IsEnd {  get; set; }

        public bool IsMatch(char @char, out string done, out string doing)
        {
            done = null;
            doing = null;

            if (IsEnd)
            {
                done = String.Join(" ", Words);
                doing = string.Empty;
                return true;
            }

            if (@char == Words[currWordIndex][currCharIndex])
            {
                var a1 = Words[currWordIndex].Take(currCharIndex + 1);

                done = string.Join(' ', Words.ToArray(), currWordIndex,1) + " " + a1;

                

                doing = string.Join(" ", Words, currWordIndex);


                currCharIndex++;
                if (currCharIndex == Words[currWordIndex].Length - 1)
                {
                    currCharIndex = 0;
                    currWordIndex++;
                }
                if(currWordIndex == Words.Count - 1)
                {
                    IsEnd = true;
                }
            }
            return false;
        }
    }
}
