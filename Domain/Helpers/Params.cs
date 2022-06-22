using System.Collections.Generic;
using System.Linq;

namespace Domain.Helpers
{
    public static class Params
    {
        public static List<string> Get(string message)
        {
            var listParams = new List<string>();

            var listOfWords = message.Split();

            foreach (var word in listOfWords.Where( w => w.Length > 4))
            {
                if(word.Substring(0, 2) == "{{" &&
                   word.Substring(word.Length - 2, 2) == "}}")
                {
                    listParams.Add(word.Replace("{", "").Replace("}", ""));
                }
            }

            return listParams;
        }
    }
}
