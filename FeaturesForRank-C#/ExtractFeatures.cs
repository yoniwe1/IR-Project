using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace FeaturesForRank
{
    class ExtractFeatures
    {
        public static string RemoveSpecialCharacters(string str)
        {// removing special chars which might be harden on URL browser
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((c >= '0' && c <= '9') || (c >= 'A' && c <= 'Z') || (c >= 'a' && c <= 'z') || c == '.' || c == '_'
                    || c == ' ' || c == '-')
                    sb.Append(str[i]);
            }

            return sb.ToString();
        }
        public static List<Feature> getFeaturesFromWeb(List<YahooQA> blockQAsList)
        {// get features values of each Q&A pair from solr by HttpWebResponse

            edu.stanford.nlp.pipeline.StanfordCoreNLP pipe = stanfordForOverlapping.loadPipeline();
            List<Feature> FilteredfeatureObj = new List<Feature>();// to rankLib
            int qid = 1;
            foreach (YahooQA queryObj in blockQAsList)//for each pair Q&A
            {
                try
                {
                    string html = string.Empty;
                    // removing special chars which might be harden on URL browser
                    queryObj.question = RemoveSpecialCharacters(queryObj.question);
                    // / build grammatical structure of the the question
                    string outputStanfordUnFiltered = stanfordForOverlapping.Parse_Data(queryObj.question, pipe);
                    //extract meaningful words from user query
                    List<string> dominantWordsInQuestion = stanfordForOverlapping.ExtracterWords(outputStanfordUnFiltered);

                    string questionToSolr = queryObj.question.Replace(" ", "%2B");

                    if (dominantWordsInQuestion.Count == 0)
                        dominantWordsInQuestion.Add("nowordtofill");//after that-there is 1 for sure
                    if (dominantWordsInQuestion.Count == 1)
                        dominantWordsInQuestion.Add("nowordtofill");//after that-there is 2 for sure
                    if (dominantWordsInQuestion.Count == 2)
                        dominantWordsInQuestion.Add("nowordtofill");//after that-there is 3 for sure

                    //URL for HTTP which represents the results of solr with the feature values
                    string url = string.Format("{0}localhost:8983/solr/techproducts/query?q=nbestanswers:{1}&fl=id,answer,nbestanswers,[features%20store=my_efi_feature_store%20efi.text_a={2}%20efi.text_b={3}%20efi.text_c={4}]&start=0&rows=1000", "http://", questionToSolr, dominantWordsInQuestion[0], dominantWordsInQuestion[1], dominantWordsInQuestion[2]);

                    HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                    request.AutomaticDecompression = DecompressionMethods.GZip;

                    using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                    using (Stream stream = response.GetResponseStream())
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        html = reader.ReadToEnd();
                    }
                    // now we parser the output of html to extract feature value like BM25,overlapping words and etc
                    string[] words = html.Split(new string[] { "\"docs\":[\n" }, StringSplitOptions.None);

                    String jsonFeatures = words[1].Remove(words[1].Length - 7);
                    jsonFeatures = jsonFeatures.Replace("[", "").Replace("]", "");
                    jsonFeatures = "[" + jsonFeatures + "]";
                    List<Feature> featureObjUnFiltered = JsonConvert.DeserializeObject<List<Feature>>(jsonFeatures);//for 1 question

                    foreach (Feature fUnfiltered in featureObjUnFiltered)//block of mixed results
                    {// mixed resuls because solr show answers which belong to other question block ofcourse
                        if (fUnfiltered.id != queryObj.id)// if this specific result is not in the query Id block - continue
                            continue;

                        //now the nbestanswers of the queryObj question
                        String[] allFeatures = fUnfiltered.features.Split(',');
                        Feature filtered = new Feature();// this feature is for rankLib 

                        filtered.answer = fUnfiltered.answer;
                        filtered.nbestanswers = fUnfiltered.nbestanswers;
                        filtered.id = fUnfiltered.id;
                        filtered.qid = qid.ToString();
                        if (fUnfiltered.answer == fUnfiltered.nbestanswers)// meanwhile with just put relevance label- we will change it later.
                            filtered.relevance = "4";
                        else
                            filtered.relevance = "1";

                        foreach (var featurefile in allFeatures)// for exmple: feature="BM25=3.22"
                        {// finally- extracting the values
                            string[] valueF = featurefile.Split('=');
                            switch (valueF[0])//BM25 or OriginalScore and etc
                            {
                                case "overlapping":
                                    filtered.overlapping = valueF[1];
                                    break;
                                case "originalScore":
                                    filtered.originalScore = valueF[1];
                                    break;
                                case "questionLength":
                                    filtered.questionLength = valueF[1];
                                    break;
                                case "docLength":
                                    filtered.docLength = valueF[1];
                                    break;
                                default:
                                    Console.Write("default");
                                    break;
                            }

                        }
                        FilteredfeatureObj.Add(filtered);// attach to the list of the output of this function
                    }
                    Console.WriteLine("my qid" + qid);
                    qid++;
                }
                catch (Exception e)
                {
                    Console.WriteLine("my msg" + e);
                }
            }


            return FilteredfeatureObj;

        }


        public static void computeRelevance(List<Feature> featuresToRankLib)
        {// to compute relevance label for each line in formula to rankLib
            int it_pre = 0; int it_runForQid;// we run in form of " go a few steps till qid changes and catch up the position with it_pre"
            double maxoriginalScore = 0, minoriginalScore = 0;
            string current_qid;
            while (it_pre <= featuresToRankLib.Count - 1)
            {
                current_qid = featuresToRankLib[it_pre].qid;// get current qid
                it_runForQid = it_pre;
                maxoriginalScore = Convert.ToDouble(featuresToRankLib[it_pre].originalScore);
                minoriginalScore = Convert.ToDouble(featuresToRankLib[it_pre].originalScore);
                it_runForQid++;
                while (it_runForQid <= featuresToRankLib.Count - 1 && current_qid == featuresToRankLib[it_runForQid].qid)
                {// just compute min and max originalScore
                    if (Convert.ToDouble(featuresToRankLib[it_runForQid].originalScore) > maxoriginalScore)
                        maxoriginalScore = Convert.ToDouble(featuresToRankLib[it_runForQid].originalScore);
                    if (Convert.ToDouble(featuresToRankLib[it_runForQid].originalScore) < minoriginalScore)
                        minoriginalScore = Convert.ToDouble(featuresToRankLib[it_runForQid].originalScore);
                    it_runForQid++;
                }
                while (it_pre <= featuresToRankLib.Count - 1 && current_qid == featuresToRankLib[it_pre].qid)
                {//  compute originalScore for each obj in list
                    double thisPairBM25 = Convert.ToDouble(featuresToRankLib[it_pre].originalScore);
                    double current_relevanceDouble = (((3 * (thisPairBM25 - minoriginalScore)) / (maxoriginalScore - minoriginalScore)) + 1);
                    string current_relevanceStr = Math.Round(current_relevanceDouble).ToString();

                    featuresToRankLib[it_pre].relevance = current_relevanceStr;
                    it_pre++;
                    Console.WriteLine(it_pre);
                }
                maxoriginalScore = 0; minoriginalScore = 0;

            }
        }

    }
}
