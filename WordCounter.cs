using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WordCounter
{
    internal class WordCounter
    {
        private Dictionary<string,int> countResult;

        private void AddCount(string key)
        {
            if (countResult.ContainsKey(key))
            {
                countResult[key]++;
            }
            else
            {
                countResult.Add(key, 1);
            }
        }

        public WordCounter()
        {
            countResult = new Dictionary<string, int>();
        }

        public void CountEnglishWord(string text)
        {
            countResult.Clear();

            string currentWord = "";

            for(int i = 0; i < text.Length; i++)
            {
                if (Char.IsLetter(text[i]))
                {
                    currentWord += Char.ToLower(text[i]);
                }
                else if(currentWord.Length>0)
                {
                    AddCount(currentWord);
                    currentWord = "";
                }
            }
        }

        public void CountChar(string text)
        {
            countResult.Clear();

            foreach(char c in text)
            {
                if (Char.IsLetter(c))
                {
                    AddCount(Char.ToLower(c).ToString());
                }
                else if (isChineseChar(c))
                {
                    AddCount(c.ToString());
                }
            }
        }

        public string GetSummary(int showCount=30)
        {
            string summary = 
                $"{new string('-', 20)} SUMMARY {new string('-', 20)}\n";
            summary += string.Format(
                "|{0,-15}|{1,-15}|{2,-15}|\n", "WORD", "COUNT", "FREQ(%)");
            summary += new string('-', 49) + '\n';

            var sortedCountResult = GetSortedCountResult();

            int totalCount = 0;
            foreach(var i in sortedCountResult)
            {
                totalCount += i.Value;
            }

            showCount = Math.Min(showCount, sortedCountResult.Count);

            for(int i=0;i<showCount;i++)
            {
                summary += String.Format(
                    "|{0,-15}|{1,-15}|{2,-15:F2}|\n",
                    sortedCountResult[i].Key,
                    sortedCountResult[i].Value,
                    100 * ((double)sortedCountResult[i].Value / totalCount));
            }

            summary += new string('-', 49) + '\n';

            return summary;
        }

        public string GetSummaryExcelFormat(int showCount = 20)
        {
            string summary = "";

            var sortedCountResult = GetSortedCountResult();

            int totalCount = 0;
            foreach (var i in sortedCountResult)
            {
                totalCount += i.Value;
            }

            showCount = Math.Min(showCount, sortedCountResult.Count);

            for (int i = 0; i < showCount; i++)
            {
                summary += String.Format(
                    "{0} {1:F2}\n",
                    sortedCountResult[i].Key,
                    100 * ((double)sortedCountResult[i].Value / totalCount));
            }

            return summary;
        }

        public string GetSummaryTikzFormat(int showCount = 20)
        {
            string summary = "";

            var sortedCountResult = GetSortedCountResult();

            int totalCount = 0;
            foreach (var i in sortedCountResult)
            {
                totalCount += i.Value;
            }

            string xLabels = "";

            showCount = Math.Min(showCount, sortedCountResult.Count);

            for (int i = 0; i < showCount; i++)
            {
                xLabels += sortedCountResult[i].Key + ",";
                summary += String.Format(
                    "({0},{1:F2})\n",
                    sortedCountResult[i].Key,
                    100 * ((double)sortedCountResult[i].Value / totalCount));
            }

            return xLabels + '\n' + summary;
        }

        private List<KeyValuePair<string,int>> GetSortedCountResult()
        {
            var sortedCountResult = countResult.ToList();
            sortedCountResult.Sort((x, y) =>
            {
                return y.Value.CompareTo(x.Value);
            });

            return sortedCountResult;
        }

        private bool isChineseChar(char c)
        {
            return 0x4e00 <= c && c <= 0x9fa5;
        }
    }
}
