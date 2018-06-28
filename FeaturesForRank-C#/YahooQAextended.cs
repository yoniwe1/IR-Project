using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesForRank
{

    class YahooQAextended
    {    // just convert nbestanswers to string  for each answer and not array.
        // not really important. just for comfortable settings
        public String identityF { get; set; }
        public String main_category { get; set; }
        public String question { get; set; }
        public String nbestanswers { get; set; }
        public String answer { get; set; }
        public String id { get; set; }


    }
}
