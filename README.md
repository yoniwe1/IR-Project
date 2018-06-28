# IR-Project

* The following is a list of the files and links included in this repository and related to it, with their increamented number in the *references* list in the ProjectSummary.pdf file:

  * [1] based paper.pdf - The main paper we implemented on our project.
  
  * [2] https://ciir.cs.umass.edu/downloads/nfL6/ - The dataset.
  
  * [3] https://stanfordnlp.github.io/CoreNLP/download.html - Stanford Dependency Parser.
  
  * [4] managed-schema - Our schema file.
  
  * [5] myFeatures.json - Solr's feature file.
  
  * [6] FeaturesForRank-C#/ExtractFeatures.cs - Script to extract feature results and values from HTML.
  
  * [7] formatToRankLib2.json - RankLib feature file.
  
  * [8] convert_lambdamart_xml_to_json.py - Xml to JSON python converter.
  
* Other files in the repository:

  * OurStanfordParser.java - Our implementation of the Stanford Parser.
  
  * mymodelFormat.json - The model as was given to us by the RankLib library. This is the model uploaded to Solr.
  
  * formatToRankLib4.json - file format to ranklib with 4 features. The algorithm worked better for us with 2 features instead of 4.
  
  * FeaturesForRank-C#/stanfordForOverlapping.cs - Script to extract words to be used as parameters for the "number of overlapping terms" feature. 
  
Remark: All other files in the FeaturesForRank-C# directory are not mandatory to read in order to understand the flow of the program. Mostly consists of entities of classes.
