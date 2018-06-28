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
   
    
 
	 public static String Parse_Data(String sent)
	 {// this function is out first step ( 2.1 in our paper)
	  // this function get as input user query and output the meaningful of the query
	 LexicalizedParser lp = LexicalizedParser.loadModel(); //uploading model of stanford
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
	     
	     Tree parse = (Tree) lp.apply(toks); // finally, we actually get to parse something
	     
	     // Get words, stemmed words and POS tags
	     
	    ArrayList<WordQuery> words = new ArrayList();/*original words*/ 
	 
	     ArrayList<String> stems = new ArrayList(); 
	     ArrayList<String> tags = new ArrayList(); 
	     
	     // Get words and Tags 
	     for (TaggedWord tw : parse.taggedYield())
	     { 
	    	 
	   
	     tags.add(tw.tag());
	     }

	     // Get stems     
	     for (TaggedWord tw : parse.taggedYield())
	     {
	     stems.add(tw.word()); 
	     }
	         
	     
	     // Get dependency tree
	     GrammaticalStructureFactory gsf = tlp.grammaticalStructureFactory();      
	     GrammaticalStructure gs = gsf.newGrammaticalStructure(parse);      
	     Collection<TypedDependency> tdl = gs.typedDependenciesCollapsed();
	     
	     for (String tw : stems)
	     {
	    	  words.add(new WordQuery(tw,false));//original words wihout stemming
	     }
	     System.out.println("typedDependencies: "+tdl);  // print dependencies words
	     
		Object[] TDS1 = tdl.toArray();
	     for(int k=0;k<TDS1.length;k++)
	     {
	     System.out.println(TDS1[k]);
	     }
	     ArrayList<String> typeDepweWant = new ArrayList();
	     typeDepweWant.add("nmod"); typeDepweWant.add("case"); // set which dependencies are important to us to save for the function's output
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
	     return newQuery.toString(); // the output
	 }
	 
	 public static void updateToTrue(String w,ArrayList<WordQuery> al) {
	     for(WordQuery p : al) {
		        if(p.getWord().equals(w))
		           p.setToUse(true);
		     }
	 	}
}
