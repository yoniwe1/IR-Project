using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesForRank
{
     class YahooQA
    {
        public String main_category{ get; set; }
        public String question { get; set; }
        public String[] nbestanswers { get; set; }// check if this string with id="2472154" is okay 
                                                  //with many answers in array
        public String answer { get; set; }
        public String id { get; set; }

        public static List<YahooQA> getJsonToObject()
        {
            // string link = @"C:\tmp\jsonOriginal61.json";
            string link = @"C: \\Users\\rtarabco\\Downloads\\nfL6.json\\nfL6.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(link))
            {
                string json = r.ReadToEnd();
                List<YahooQA> items = JsonConvert.DeserializeObject<List<YahooQA>>(json);
                return items;
            }
         
        }



    }



}
