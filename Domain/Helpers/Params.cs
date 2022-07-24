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
                if(word.Contains("{{") &&
                   word.Contains("}}")
                   )
                {
                    var key = word.Replace("{", " ").Replace("}", " ").Split();
                    listParams.Add(key.Where(k => k.Length > 0).FirstOrDefault());
                }
            }

            return listParams;
        }
    }
}
