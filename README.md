# IR-Project

IR-Project
please edit it:

OurStanfordParser.java is for step 2.1 in our paper ( extracting meaningful words from user query)

FeaturesForRank-C# is for step 3 in our papaer( extracting feature values from solr and compute format file for Ranklib)

REFERENCES ( from our paper) [1] Khvalchik M., Kulkarni A. (2017) Open-Domain Non-factoid Question Answering. In: Ekštein K., Matoušek V. (eds) Text, Speech, and Dialogue. TSD 2017. Lecture Notes in Computer Science, vol 10415. Springer, Cham. L

Link to the paper! "based paper.pdf"

[2] Reference to the dataset: https://ciir.cs.umass.edu/downloads/nfL6/

[3] Stanford Dependency Parser https://stanfordnlp.github.io/CoreNLP/download.html

[4] Schema File : managed-schema

[5] Solr's feature file. myFeatures.json

[6]Script to extract feature results FeaturesForRank-C#/ ExtractFeatures.cs

[7] Script to get feature values from html. FeaturesForRank-C#/ ExtractFeatures.cs

[8]RankLib feature file. formatToRankLib2.json

[9] Xml to JSON python converter. convert_lambdamart_xml_to_json.py

remarks: formatToRankLib4.json - file format to ranklib with 4 features 
formatToRankLib2.json - file format to ranklib with 2 features . worked better than the one with 4 features 
mymodelFormat.json - the output of ranklib - the model which uploaded to solr
