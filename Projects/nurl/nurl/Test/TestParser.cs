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
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using NUnit.Framework;

namespace nurl.parse
{
	[TestFixture]
	public class TestParser
	{
		public TestParser()
		{
			
		}
				
		[TestCase("get -url http://google.fr", true , 1)]
		[TestCase("test -url http://google.fr", true, 2)]
		[TestCase("autre -url http://google.fr", false, 3)]
		[TestCase("get -autre http://google.fr", false, 4)]
		[TestCase("test -autre http://google.fr", false , 5)]
		[TestCase("test -autre http://abc", false , 6)]
		[TestCase("test -url http://google.fr -save c:\abc.json", true , 7)]
		[TestCase("test -url http://google.fr -autre c:\abc.json ", false , 8)]
		[TestCase("test -url http://google.fr -time 5", true , 9)]
		[TestCase("test -url http://google.fr -autre 5", false, 10)]
		[TestCase("test -url http://google.fr -time", false , 11)]
		[TestCase("test -url http://google.fr -save", false , 12)]
		public void validArgsTest(string  a, bool result , int id)
		{
		  	
			
			String [] test = a.Split(' ');
			Parser myParser = new Parser(test);
			
			Console.WriteLine("-----------------------------------------------------");
			Console.WriteLine("ID test : {0}  lenght : {1}", id , test.Length);
			
			Assert.IsTrue(myParser.validArgs() == result);
			
		}
		
		
		[TestCase("get -url http://google.fr", "http://google.fr", 1)]
		[TestCase("get -url http://nunit.org/index.php?p=values&r=2.5", "http://nunit.org/index.php?p=values&r=2.5", 2)]
		public void validUrl(string a , string result , int id)
		{
			String [] test = a.Split(' ');
			Parser myParser = new Parser(test);
			myParser.validArgs();
			Assert.AreEqual(myParser.inputUrl , result);
		}
		
//		[TestCase("get -url http://google.fr -save c:/text.html ", "c:/text.html", 1)]
		[TestCase("get -url http://nunit.org/ -save c:/abc.json", "c:/abc.json", 2)]
		public void validFile(string a , string result , int id)
		{
			String [] test = a.Split(' ');
			Parser myParser = new Parser(test);
			myParser.validArgs();
			
			
			Console.WriteLine("-----------\n{0} Result  : {0} \n Test : {1}", result, myParser.outputFile);
			
			Assert.AreEqual(myParser.outputFile , result);
		}
		
	}

	public class Parser
	{
		// Argument
		
		public string [] args ; 
		public string outputFile;
		public string inputUrl;
		
		public Parser(string[] _args)
		{	
			args = _args;	
		}
		
		// Méthodes
	
		public bool validArgs(){
			
			
			Console.WriteLine("ValidArgs -----------");
			
			if(!args[0].Equals("test") && !args[0].Equals("get"))
			{
				Console.WriteLine("Param get or test missing");
				return false;
			}
				
			
			if( !args[1].Equals("-url") || args.Length <1 )
			{
				Console.WriteLine("Param -url missing");
				return false;
			}
				
			//Console.WriteLine("arg[2] : {0}", (args[2].Split(':'))[0]);
			/*	(args[2].Split(':'))[0].Equals("http") 
					|| 
					(args[2].Split(':'))[0].Equals("https")
					) */
			if( args[1].Equals("-url") && !isAnUrl(args[2]))
			{
				
				Console.WriteLine("Le paramètre 2 n'est pas une URL");
				return false ; 
			}
			else{
			
				inputUrl = args[2];				
			}
			
			
			if(args.Length > 3)
			{
				//Console.WriteLine("Fuck arg[4] = {0} ", args[4]);
				if( !args[3].Equals("-save") && !args[3].Equals("-time") )
				{
					
					Console.WriteLine("Param 4 must be -save or -time");
					Console.WriteLine("args[3] : {0}", args[3]);
					return false;
				}
				
				
				if( args.Length < 5 )
				{
					Console.WriteLine("Il manque le param 5");
					return false;
				}
				
				if(args[3].Equals("-time") && isInt(args[4])){
				
					Console.WriteLine("Avec -time -> le param 5 doit être un int");
				}
				
				
				Console.WriteLine("arg3 {0}", args[3]);
				Console.WriteLine("arg4 {0}", args[4]);
				if(args[3].Equals("-save") ){
					outputFile = args[4];
				}
				
				
			}
			return true;
			
		}
		
		public bool isInt(string valeur)
		{
			    bool result = false;
			
			    try {
			        int.Parse(valeur);
			        result = true;
			    }
			    catch {}
			
			    return result;
		}
		
		private bool isAnUrl(string urlTest)
        {
            try
            {
                var url = urlTest ; 
            
                if( string.IsNullOrEmpty(url))
                    return false;
                
                //Creating the HttpWebRequest
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                //Setting the Request method HEAD, you can also use GET too.
                request.Method = "HEAD";
                
                //Getting the Web Response.
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
                //Returns TRUE if the Status code == 200
                return (response.StatusCode == HttpStatusCode.OK);
            }
            catch
            {
                
                return false;
            }
        }
	}



}


