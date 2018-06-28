package ourProject;

import edu.stanford.nlp.ling.*;

import edu.stanford.nlp.ling.CoreAnnotations.LemmaAnnotation;
import edu.stanford.nlp.ling.CoreAnnotations.SentencesAnnotation;
import edu.stanford.nlp.ling.CoreAnnotations.TokensAnnotation;
import edu.stanford.nlp.process.Tokenizer;
import edu.stanford.nlp.process.DocumentPreprocessor;
import edu.stanford.nlp.tagger.maxent.MaxentTagger;
import edu.stanford.nlp.trees.*;
import edu.stanford.nlp.util.CoreMap;
import entities.WordQuery;

import java.io.*;
import java.nio.channels.Pipe;
import java.util.*;


import EDU.oswego.cs.dl.util.concurrent.FJTask.Par2;
import edu.stanford.nlp.parser.lexparser.LexicalizedParser;
import edu.stanford.nlp.parser.nndep.DependencyParser;
import edu.stanford.nlp.pipeline.Annotation;
import edu.stanford.nlp.process.PTBTokenizer;




public class OurStanfordParser {
   
    
   

	 //Print all the parsed sentences-not in use
	 public static void dependency_parser_for_text_File(String SourceFile, String TargetFile)
	 {
	 LinkedList<String> Unparsed_Sentences = new LinkedList<String>();
	 LinkedList<String> Parsed_Sentences = new LinkedList<String>();
	 try
	 {
/*	 FileReader fr1 = new FileReader(SourceFile);
	 BufferedReader br1 = new BufferedReader(fr1);
	 for(String line1 = br1.readLine();line1!=null;line1=br1.readLine())
	 {
	 Unparsed_Sentences.add(line1.trim());
	 }
	 br1.close();*/
		 Unparsed_Sentences.add("What should I have in my disaster emergency kit stored outside my house?");
	 }
	 catch(Exception e)
	 {
	 e.printStackTrace();
	 }
	 
	 //Apply dependency parser 
	 String modelPath = DependencyParser.DEFAULT_MODEL;

	
	 DependencyParser parser=null;
	// MaxentTagger tagger=null;
	try{
		 // tagger = new MaxentTagger("C:\\Users\\rtarabco\\eclipse-workspace\\otherLibs\\stanford-wsj-0-18-bidirectional-nodistsim.tagger");
	
		  parser = DependencyParser.loadFromModelFile(modelPath);
	}
	catch(Exception e) {
		 System.out.println("tagger file not okaty");
	}
	 // MaxentTagger tagger = new MaxentTagger("C:\\Java_Systems\\Concept_Graph_QA\\Java_Libraries\\stanford-postagger-2013-04-04\\models\\wsj-0-18-bidirectional-nodistsim.tagger");

	 String Parsed_text = "";
	 for(int i=0;i<Unparsed_Sentences.size();i++)
	 {
	 String text = Unparsed_Sentences.get(i).toString();
	
	     //MaxentTagger tagger = new MaxentTagger("C:\Java_Systems\Concept_Graph_QA\Java_Libraries\\stanford-postagger-2014-10-26\\models\\english-left3words-distsim.tagger");
     MaxentTagger tagger = new MaxentTagger("C:\\Users\\rtarabco\\eclipse-workspace\\stanford-corenlp-full-2018-02-27\\stanford-wsj-0-18-bidirectional-nodistsim.tagger");

	 
	 DocumentPreprocessor tokenizer = new DocumentPreprocessor(new StringReader(text));
	     //ArrayList<TypedDependency> dep_list = new ArrayList<TypedDependency>();
	     for (List<HasWord> sentence : tokenizer) 
	     {
	      List<TaggedWord> tagged = tagger.tagSentence(sentence); 
	      GrammaticalStructure gs = parser.predict(tagged);
	       
	      // Print typed dependencies     
	      System.err.println(gs);
	      Parsed_text = gs.typedDependencies().toString();
	      //TypedDependenciesDemo.convert(Parsed_text);   just experience
	      ArrayList<TypedDependency> dep_list = (ArrayList<TypedDependency>) gs.typedDependencies();
	      ArrayList<TypedDependency> dep_list2=dep_list;
	      int a=2;
	     }
	     
	     System.out.println("Only Typed dependencies => "+Parsed_text);
	    // for(int j=0;j<dep_list.size();j++)
	    // {
	    // if(dep_list.size()>0)
	    // {
	      System.out.println(Parsed_text);
	      //Parsed_text = Parsed_text + "#" + dep_list.get(0);
	     Parsed_Sentences.add(Parsed_text);
	    // }

	    // } 

	 }
	 //Now write the data to  file
	 try
	 {
	 FileWriter frw1 = new FileWriter(TargetFile);
	 BufferedWriter brw1 = new BufferedWriter(frw1);
	 for(int i=0;i<Parsed_Sentences.size();i++)
	 {
	 brw1.write(Parsed_Sentences.get(i).toString());
	 brw1.newLine();
	 }
	 brw1.close();
	 }
	 catch(Exception e)
	 {
	 e.printStackTrace();
	 }
	 OurStanfordParser.Parse_Data(Parsed_text);
	 }
	 //function to parse dat into subject verb object
	 
	 public static String Parse_Data(String sent)
	 {
	 //LexicalizedParser lp = LexicalizedParser.loadModel("edu/stanford/nlp/models/lexparser/englishPCFG.ser.gz");
	 LexicalizedParser lp = LexicalizedParser.loadModel();
	// String sent = "What should I have in my disaster emergency kit stored outside my house?";
	
	 System.out.println("Sentence = "+sent);
	 TreebankLanguagePack tlp = lp.getOp().langpack();
	 Tokenizer<? extends HasWord> toke = tlp.getTokenizerFactory().getTokenizer(new StringReader(sent));
	 List<? extends HasWord> sentence = toke.tokenize();
	 lp.apply(sentence).pennPrint();
	 
	 //----------------
	 StringReader sr; // we need to re-read each line into its own reader because the tokenizer is over-complicated garbage
	     PTBTokenizer tkzr; // tokenizer object
	     WordStemmer ls = new WordStemmer(); // stemmer/lemmatizer object
	     
	     sr = new StringReader(sent);
	     tkzr = PTBTokenizer.newPTBTokenizer(sr);
	     List toks = tkzr.tokenize();
	    // System.out.println ("tokens: "+toks);
	     
	     Tree parse = (Tree) lp.apply(toks); // finally, we actually get to parse something
	     
	     // Get words, stemmed words and POS tags
	     
	    ArrayList<WordQuery> words = new ArrayList();/*original*/ 
	 
	     ArrayList<String> stems = new ArrayList(); 
	     ArrayList<String> tags = new ArrayList(); 
	     
	     // Get words and Tags 
	     for (TaggedWord tw : parse.taggedYield())
	     { 
	    	 
	   
	     tags.add(tw.tag());
	     }

	     // Get stems     
//	     ls.visitTree(parse); // apply the stemmer to the tree
	     for (TaggedWord tw : parse.taggedYield())
	     {
	     stems.add(tw.word()); 
	     }
	         
	     
	     // Get dependency tree
	     //TreebankLanguagePack tlp = new PennTreebankLanguagePack();      
	     GrammaticalStructureFactory gsf = tlp.grammaticalStructureFactory();      
	     GrammaticalStructure gs = gsf.newGrammaticalStructure(parse);      
	     Collection<TypedDependency> tdl = gs.typedDependenciesCollapsed();
	     
	     for (String tw : stems)
	     {
	    	  words.add(new WordQuery(tw,false));//original
	    
	     }
	   
	  
	     
	     
	     // And print!     
	   //  System.out.println("words: ");      
	   //  System.out.println("POStags: "+tags);  
	   //  System.out.println("stemmedWordsAndTags: "+stems);  
	    // System.out.println("typedDependencies: "+tdl); 
	     Object[] TDS1 = tdl.toArray();
	     for(int k=0;k<TDS1.length;k++)
	     {
	     System.out.println(TDS1[k]);
	     }
	     ArrayList<String> typeDepweWant = new ArrayList();
	     typeDepweWant.add("nmod"); typeDepweWant.add("case");
	     typeDepweWant.add("compound"); typeDepweWant.add("amod");
	     typeDepweWant.add("nsubj"); typeDepweWant.add("advmod");
	     
	     String w1,w2,w3,typed;
	     for (TypedDependency it : tdl) {
	    	 typed=it.reln().getShortName();
	    	 if(typeDepweWant.contains(typed)) {
	    		 w1=it.dep().value();
	    		 updateToTrue(w1,words);
	    		 w2=it.gov().value();
	    		 updateToTrue(w2,words);
	    		 if(typed.equals("nmod")&&it.reln().getSpecific()!=null)
	    		 {
		    		 w3=it.reln().getSpecific();
		    		 updateToTrue(w3,words);	    		 
	    		 }
	    	 }
	     }

	     ArrayList<String> newQuery = new ArrayList();
	  
	     for(WordQuery p : words) {
		        if(p.isToUse() == true)
		        	newQuery.add(p.getWord());
		     }
	     return newQuery.toString();
	 }
	 
	 
	 
	 
	 public static void updateToTrue(String w,ArrayList<WordQuery> al) {
	     for(WordQuery p : al) {
		        if(p.getWord().equals(w))
		           p.setToUse(true);
		     }

	 	}
}