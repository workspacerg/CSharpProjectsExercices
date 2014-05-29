/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 28/05/2014
 * Time: 14:30
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework;
using nurl;

namespace nurl.Test
{
	[TestFixture]
	public class TestParser
	{
		public TestParser()
		{
			
		}	
		[TestCase(1 , "get -url http://google.fr", "Commande Valide" )]
		[TestCase(2 , "autre", "Le premier paramètre doit être get ou test" )]
		[TestCase(3 , "get", "Il manque un argument après get ou post" )]
		[TestCase(4 , "get -url", "Il manque l'Url" )]
		[TestCase(5 , "get -url http://fake", "L'url n'est pas valide" )]
		[TestCase(6 , "get -url http://www.google.fr/uy", "L'url n'est pas valide" )] // 404
		[TestCase(7 , "get -url http://www.google.fr/ -autre", "L'option n'est pas valide" )]
		[TestCase(8 , "get -url http://www.google.fr/ -save", "Fichier abscent" )]
		[TestCase(9 ,@"get -url http://www.google.fr/ -save C:\Users\Public", "Ce n'est pas un fichier" )]
		[TestCase(10,@"get -url http://www.google.fr/ -save C:\Users\Public\file.txt", "Commande Valide" )]
		[TestCase(11,@"test -url http://www.google.fr/", "Il manque -time x")]
		[TestCase(12,@"test -url http://www.google.fr/ -autre", "L'option n'est pas valide")]
		[TestCase(13,@"test -url http://www.google.fr/ -time", "Pas un nombre ou abscent")]
		[TestCase(14,@"test -url http://www.google.fr/ -time 5", "Commande Valide")]
		[TestCase(15,@"test -url http://www.google.fr/ -time 5 -avg", "Commande Valide")]
		[TestCase(16,@"test -url http://www.google.fr/ -time 5 -autre", "Param 6 non valide")]
		[TestCase(17,@"test -url http://www.google.fr/ -time x", "Pas un nombre ou abscent")]
		public void validArgumentTest( int id, string  args, string result)
		{
							
			String [] test = args.Split(' ');
			Parser myParser = new Parser(test);
			
			Console.Write("len : {0}", test.Length);
			Console.WriteLine(myParser.validArgument());
			
			Assert.IsTrue(result.Equals(myParser.validArgument()));
			
		}
		
		[TestCase("get -url http://google.fr", "http://google.fr", 1)]
		[TestCase("get -url http://nunit.org/index.php?p=values&r=2.5", "http://nunit.org/index.php?p=values&r=2.5", 2)]
		public void validUrl(string a , string result , int id)
		{
			String [] test = a.Split(' ');
			Parser myParser = new Parser(test);
			myParser.validArgument();
			Assert.AreEqual(myParser.inputUrl , result);
		}
		
		[TestCase("get -url http://google.fr -save c:/text.html", "c:/text.html", 1)]
		[TestCase("get -url http://nunit.org/ -save c:/abc.json", "c:/abc.json", 2)]
		public void validFile(string a , string result , int id)
		{
			String [] test = a.Split(' ');
			Parser myParser = new Parser(test);
			myParser.validArgument();
			
			
			Console.WriteLine("-----------\n{0} Result  : {0} \n Test : {1}", result, myParser.outputFile);
			
			Assert.AreEqual(myParser.outputFile , result);
		}
		
		[TestCase("http://google.fr", true)]
		[TestCase("Blabla",false)]
		public void validUrlFormat(string a , bool result)
		{			
			Assert.IsTrue( Parser.isAnUrl(a) == result);
		}
		
	}

//	public class Parser
//	{
//		// Argument
//		
//		public string [] args ; 
//		public string outputFile;
//		public string inputUrl;
//		public modeOfprocessing mode ; 
//		public int iterator; 
//		
//		public Parser(string[] _args)
//		{	
//			args = _args;	
//		}
//		
//		// Méthodes
//	
//		public string validArgument(){
//			
//			string result = "" ;
//			
//			if (args.Length == 0) 
//			{
//				return "Aucun paramètre en argument";
//			}
//			
//			
//			if(!args[0].Equals("test") && !args[0].Equals("get"))
//			{
//				return "Le premier paramètre doit être get ou test";
//			}
//			
//			if (args.Length < 2)
//			{
//				return "Il manque un argument après get ou post";
//			}
//			
//			if (args[0].Equals("test"))
//			{
//				result = validArgsTest();
//			}
//			else
//			{
//				result = validArgsGet();
//			}	
//			
//			return result;
//		}
//		
//		public string validArgsGet()
//		{
//			if (args[1].Equals("-url") && args.Length <3 )
//			{
//				return "Il manque l'Url" ;
//			}
//			
//			
//			if (args[1].Equals("-url"))
//			{
//			
//				if( !isAnUrl(args[2]) )
//				{
//					return ("L'url n'est pas valide");	
//				}
//				
//				inputUrl = args[2];
//				
//				if ( args.Length < 4 )
//				{
//					mode = modeOfprocessing.getUrl ;
//					return "Commande Valide" ; 
//				}
//				
//				if (args.Length >= 4)
//				{
//					
//					if (!args[3].Equals("-save"))
//					{	
//						return "L'option n'est pas valide";
//					}
//					Console.WriteLine("TEst directory {0} //",Directory.Exists(args[4]));
//					if(args.Length == 5 )
//					{
//						
//						if (!Directory.Exists(args[4]))
//						{
//						 	mode = modeOfprocessing.getUrlSave ;
//							outputFile = args[4];
//							return "Commande Valide" ; 
//						}
//						else{
//						    return "Ce n'est pas un fichier";
//						}
//					}
//					else{
//						 return "Fichier abscent";
//					}
//					
//					//if ()
//					
//				}
//				
//				
//				
//			}
//			return "Commande Valide" ; 
//		}
//		
//		public string validArgsTest()
//		{
//			if (args[1].Equals("-url") && args.Length <3 )
//			{
//				return "Il manque l'Url" ;
//			}
//			
//			
//			if (args[1].Equals("-url"))
//			{
//			
//				if( !isAnUrl(args[2]) )
//				{
//					return ("L'url n'est pas valide");	
//				}
//				
//				inputUrl = args[2];
//				
//				if ( args.Length <= 3 )
//				{
//					return "Il manque -time x";
//				}
//				
//				if (args.Length >= 4)
//				{
//					
//					if (!args[3].Equals("-time"))
//					{	
//						return "L'option n'est pas valide";
//					}
//
//					if(args.Length >= 5 && isInt(args[4]))
//					{
//						iterator = int.Parse(args[4]);
//						
//						if (args.Length == 6)
//						{
//							if(args[5].Equals("-avg")){
//							
//								mode = modeOfprocessing.testUrlTimeAvg ;
//								return "Commande Valide" ; 
//							}
//							else
//								return "Param 6 non valide";
//						}
//						
//							mode = modeOfprocessing.testUrlTime ;
//							return "Commande Valide" ; 
//						
//					}
//					else{
//						 return "Pas un nombre ou abscent";
//					}
//					
//				}
//			
//			}
//			return "Commande Valide" ; 
//		}
		
//		public bool validArgs(){
//			
//			if (args.Length == 0 ){
//				Console.WriteLine("Aucun paramètre en argument");
//				return false;
//			}
//			
//			
//			if(!args[0].Equals("test") && !args[0].Equals("get"))
//			{
//				Console.WriteLine("Param get or test missing");
//				return false;
//			}
//			
//			return true;
//		}
		
//		public bool isInt(string valeur)
//		{
//			    bool result = false;
//			
//			    try {
//			        int.Parse(valeur);
//			        result = true;
//			    }
//			    catch {}
//			
//			    return result;
//		}
//		
//		public static bool isAnUrl(string urlTest)
//        {
//            try
//            {
//                var url = urlTest ; 
//            
//                if( string.IsNullOrEmpty(url))
//                    return false;
//                
//                //Creating the HttpWebRequest
//                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
//                //Setting the Request method HEAD, you can also use GET too.
//                request.Method = "HEAD";
//                
//                //Getting the Web Response.
//                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
//                //Returns TRUE if the Status code == 200
//                return (response.StatusCode == HttpStatusCode.OK);
//            }
//            catch
//            {
//                
//                return false;
//            }
//        }
//	}



}