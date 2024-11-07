using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwayCCP.Services
{
    public class SentenceManager : ISentenceManager
    {

        private List<string> lines = new List<string>();
        public string CurrentLine = null;
        private int currentIndex = -1;

        public void Load(string path)
        {
            if (Path.GetExtension(path).ToLower() != ".txt")
            {
                throw new ArgumentException("The file extension must be .txt");
            }
            this.lines.Clear();
            var lines = File.ReadLines(path);
            foreach (var line in lines)
            {
                if (!string.IsNullOrWhiteSpace(line))
                {
                    this.lines.Add(line.Trim());
                }
            }
        }

        public bool Next()
        {
            if (currentIndex < lines.Count - 1)
            {
                CurrentLine = lines[++currentIndex];
                return true;
            }
            return false;
        }

        public bool Previous()
        {
            if (currentIndex > 0)
            {
                CurrentLine = lines[--currentIndex];
                return true;
            }
            return false;
        }

    }
}
