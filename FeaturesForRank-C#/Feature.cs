using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeaturesForRank
{
    class Feature
    {
        public String id { get; set; }
       public String features { get; set; }
        public String overlapping { get; set; }
        public String originalScore { get; set; }
        public String questionLength { get; set; }
        public String docLength { get; set; }
        public String answer { get; set; }
        public String nbestanswers { get; set; }
        public String qid { get; set; }
        public String relevance { get; set; }
    }
}
