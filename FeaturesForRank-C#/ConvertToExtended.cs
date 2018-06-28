using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesForRank
{
    // just a function to convert the regular json of Yahoo to 
    // json which in each block there is 1 "nbestanswers" and not array
    class ConvertToExtended
    {// just convert nbestanswers to string  for each answer and not array.
        // not really important. just for comfortable settings
        public static List<YahooQAextended> convertToExt(List<YahooQA> preCollection)
        {
            List<YahooQAextended> postCollection = new List<YahooQAextended>();

            foreach (YahooQA cur in preCollection)
            {
                foreach (String oneAnswerPre in cur.nbestanswers)
                {
                    YahooQAextended currentForExt = new YahooQAextended();
                    currentForExt.id=cur.id;
                    currentForExt.main_category=cur.main_category;
                    currentForExt.question=cur.question;
                    currentForExt.answer=cur.answer;
                    currentForExt.nbestanswers=oneAnswerPre;
                    postCollection.Add(currentForExt);
                }
            }
            return postCollection;
        }
    }
}
