using edu.stanford.nlp.pipeline;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
namespace FeaturesForRank
{
    class Program
    {
        static void Main(string[] args)
        {
            // ***** Important Remark: *****
            //please watch only "ExtractFeatures.cs" and "stanfordForOverlapping.cs" files.
            // all the rest are not so important for understanding the program. 

            //get json with nbestanswers as array from file
            List<YahooQA> originalQAs=YahooQA.getJsonToObject(); 

            //extract feature values of each <question,a answer> pair by HTTPrequest
            List<Feature> featuresToRankLib = ExtractFeatures.getFeaturesFromWeb(originalQAs);

            // compute relevance label to RankLib feature File Format
            ExtractFeatures.computeRelevance(featuresToRankLib);
            String formatToRankLib = "";

            foreach (Feature f in featuresToRankLib)// prepare file format to ranklib
            {
                formatToRankLib += f.relevance + " " + "qid:" + f.qid + " 1:" + f.overlapping + " 2:" +
                   f.originalScore + " 3:" + f.questionLength + " 4:" + f.docLength + "\n";
            }

            System.IO.File.WriteAllText(@"C:\tmp\formatToRankLib.json", formatToRankLib);






        }
    }
}
