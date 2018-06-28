using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FeaturesForRank
{
    class notepadExtSplit
    {
       // public String identityF { get; set; }
        public String main_category { get; set; }
        public String id { get; set; }
        public String question { get; set; }
        public String[] nbestanswers { get; set; }
        public String answer { get; set; }

        //public static void splitNodepad()
        //{
        //    string link = "C:\\tmp\\jsonExt6.json";
        //    using (System.IO.StreamReader r = new System.IO.StreamReader(link))
        //    {
        //        string json = r.ReadToEnd();
        //        List<notepadExtSplit> items = JsonConvert.DeserializeObject<List<notepadExtSplit>>(json);

        //        List<notepadExtSplit> items1 = new List<notepadExtSplit>();
        //        List<notepadExtSplit> items2 = new List<notepadExtSplit>();
        //        List<notepadExtSplit> items3 = new List<notepadExtSplit>();
        //        List<notepadExtSplit> items4 = new List<notepadExtSplit>();
        //        List<notepadExtSplit> items5 = new List<notepadExtSplit>();
        //        List<notepadExtSplit> items6 = new List<notepadExtSplit>();

        //        // split:
        //        int identityFcurrent = 0;

        //        /* run your code here */






        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent < 100000)
        //        {
        //            items1.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json1 = JsonConvert.SerializeObject(items1.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt61.json", json1);

        //        ////-----------------------------





        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent >= 100000 && identityFcurrent < 200000)
        //        {
        //            items2.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json2 = JsonConvert.SerializeObject(items2.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt62.json", json2);






        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent >= 200000 && identityFcurrent < 300000)
        //        {
        //            items3.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json3 = JsonConvert.SerializeObject(items3.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt63.json", json3);





        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent >= 300000 && identityFcurrent < 400000)
        //        {
        //            items4.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json4 = JsonConvert.SerializeObject(items4.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt64.json", json4);






        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent >= 400000 && identityFcurrent < 500000)
        //        {
        //            items5.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json5 = JsonConvert.SerializeObject(items5.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt65.json5", json5);




        //        while (Int32.Parse(items[identityFcurrent].identityF) <= items.Count - 1 && identityFcurrent >= 500000 && identityFcurrent < 600000)
        //        {
        //            items6.Add(items[identityFcurrent]);
        //            identityFcurrent++;
        //            Console.WriteLine(identityFcurrent);
        //        }
        //        string json6 = JsonConvert.SerializeObject(items6.ToArray(), Formatting.Indented);
        //        //write string to file
        //        System.IO.File.WriteAllText(@"C:\tmp\jsonExt66.json", json6);

        //    }
        //}


        public static void splitOriginal()
        {

            string link = "C:\\Users\\rtarabco\\Downloads\\nfL6.json\\nfL6.json";
            using (System.IO.StreamReader r = new System.IO.StreamReader(link))
            {
                string json = r.ReadToEnd();
                List<notepadExtSplit> items = JsonConvert.DeserializeObject<List<notepadExtSplit>>(json);

                List<notepadExtSplit> items1 = new List<notepadExtSplit>();
                List<notepadExtSplit> items2 = new List<notepadExtSplit>();


                // split:
                int qid= 0;

                /* run your code here */

                ////-----------------------------
                while (qid <= items.Count - 1 && qid < 40000)
                {
                    items1.Add(items[qid]);
                    qid++;
                    Console.WriteLine(qid);
                }
                string json1 = JsonConvert.SerializeObject(items1.ToArray(), Formatting.Indented);
                //write string to file
                System.IO.File.WriteAllText(@"C:\tmp\jsonOriginal61.json", json1);

                ////-----------------------------
                ////-----------------------------
                while (qid <= items.Count - 1 )
                {
                    items2.Add(items[qid]);
                    qid++;
                    Console.WriteLine(qid);
                }
                string json2 = JsonConvert.SerializeObject(items2.ToArray(), Formatting.Indented);
                //write string to file
                System.IO.File.WriteAllText(@"C:\tmp\jsonOriginal62.json", json2);

                ////-----------------------------
            }
        }
    }
}