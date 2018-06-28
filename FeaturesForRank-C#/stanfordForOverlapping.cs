




using edu.stanford.nlp.pipeline;
using edu.stanford.nlp.process;
using edu.stanford.nlp.trees;
using java.io;
using java.util;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FeaturesForRank
{
    class stanfordForOverlapping
    {
        public static StanfordCoreNLP loadPipeline()
        {// generic function for every using of Stanford Lib
            // Path to the folder with models extracted from `stanford-corenlp-3.7.0-models.jar`
            var jarRoot = @"C:\\Users\\rtarabco\\eclipse-workspace\\modelsStanford";

            // Annotation pipeline configuration
            var props = new Properties();
            props.setProperty("annotators", "tokenize, ssplit, pos, lemma, ner, parse, dcoref");
            props.setProperty("ner.useSUTime", "0");

            // We should change current directory, so StanfordCoreNLP could find all the model files automatically
            var curDir = Environment.CurrentDirectory;
            Directory.SetCurrentDirectory(jarRoot);
            var pipeline1 = new edu.stanford.nlp.pipeline.StanfordCoreNLP(props);
            Directory.SetCurrentDirectory(curDir);
            return pipeline1;

        }
        public static String Parse_Data(String sent, StanfordCoreNLP pipeline1)
        {// extract meaningful words from user query
            // Text for processing
            var text = sent;
            // Annotation
            var annotation = new edu.stanford.nlp.pipeline.Annotation(text);
            pipeline1.annotate(annotation);
            // Result - Pretty Print
            string output;
            using (var stream = new ByteArrayOutputStream())
            {
                pipeline1.prettyPrint(annotation, new PrintWriter(stream));
                System.Console.WriteLine(" it's stanford time ");
                output = stream.toString();
                stream.close();
            }
            return output;
        }

        public static List<string> ExtracterWords(string document)
        {//  extract meaningful words for 'overlapping' feature
            List<string> finalWords = new List<string> { };
            string[] wordsNeeded1 = new string[] { };
            string[] wordsNeeded2 = new string[] { };
            //  string[] seperator = new string[] {};
            string[] docNeeded = document.Split(new string[] { "dependencies):" }, StringSplitOptions.None);
            string[] lines = docNeeded[1].Split(new[] { "\r\n", "\r", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                string[] checkWho = new string[] { };
                string[] lineString = new string[] { };
                string[] bracketString = new string[] { };
                string[] termsInString = new string[] { };
                lineString = line.Split('(');

                if (lineString[0].Contains(':'))//if yes - nmod or other
                {
                    checkWho = lineString[0].Split(':');
                    lineString[0] = checkWho[0];
                }
                if ((lineString[0] == "nmod"))
                {

                    string bracketStringStr = lineString[1];
                    termsInString = bracketStringStr.Split(',');
                    wordsNeeded1 = termsInString[0].Split('-');
                    wordsNeeded2 = termsInString[1].Split('-');
                    finalWords.Add(wordsNeeded1[0]);
                    finalWords.Add(wordsNeeded2[0]);
                }
                else if ((lineString[0] == "amod"))
                {

                    string bracketStringStr = lineString[1];
                    termsInString = bracketStringStr.Split(',');
                    wordsNeeded1 = termsInString[0].Split('-');
                    wordsNeeded2 = termsInString[1].Split('-');
                    finalWords.Add(wordsNeeded1[0]);
                    finalWords.Add(wordsNeeded2[0]);
                }
            }

            return finalWords;
        }
    }
}
