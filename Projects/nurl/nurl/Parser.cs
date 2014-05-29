/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 29/05/2014
 * Time: 23:19
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Runtime.Remoting.Messaging;
using System.Runtime.Remoting.Metadata.W3cXsd2001;

namespace nurl
{
	/// <summary>
	/// Description of Parser.
	/// </summary>
	public class Parser
	{
		// Argument
		
		public string [] args ; 
		public string outputFile;
		public string inputUrl;
		public modeOfprocessing mode ; 
		public int iterator; 
		
		public Parser(string[] _args)
		{	
			args = _args;	
		}
		
		// Méthodes
	
		public string validArgument(){
			
			string result = "" ;
			
			if (args.Length == 0) 
			{
				return "Aucun paramètre en argument";
			}
			
			
			if(!args[0].Equals("test") && !args[0].Equals("get"))
			{
				return "Le premier paramètre doit être get ou test";
			}
			
			if (args.Length < 2)
			{
				return "Il manque un argument après get ou post";
			}
			
			if (args[0].Equals("test"))
			{
				result = validArgsTest();
			}
			else
			{
				result = validArgsGet();
			}	
			
			return result;
		}
		
		public string validArgsGet()
		{
			if (args[1].Equals("-url") && args.Length <3 )
			{
				return "Il manque l'Url" ;
			}
			
			
			if (args[1].Equals("-url"))
			{
			
				if( !isAnUrl(args[2]) )
				{
					return ("L'url n'est pas valide");	
				}
				
				inputUrl = args[2];
				
				if ( args.Length < 4 )
				{
					mode = modeOfprocessing.getUrl ;
					return "Commande Valide" ; 
				}
				
				if (args.Length >= 4)
				{
					
					if (!args[3].Equals("-save"))
					{	
						return "L'option n'est pas valide";
					}
					Console.WriteLine("TEst directory {0} //",Directory.Exists(args[4]));
					if(args.Length == 5 )
					{
						
						if (!Directory.Exists(args[4]))
						{
						 	mode = modeOfprocessing.getUrlSave ;
							outputFile = args[4];
							return "Commande Valide" ; 
						}
						else{
						    return "Ce n'est pas un fichier";
						}
					}
					else{
						 return "Fichier abscent";
					}
					
					//if ()
					
				}
				
				
				
			}
			return "Commande Valide" ; 
		}
		
		public string validArgsTest()
		{
			if (args[1].Equals("-url") && args.Length <3 )
			{
				return "Il manque l'Url" ;
			}
			
			
			if (args[1].Equals("-url"))
			{
			
				if( !isAnUrl(args[2]) )
				{
					return ("L'url n'est pas valide");	
				}
				
				inputUrl = args[2];
				
				if ( args.Length <= 3 )
				{
					return "Il manque -time x";
				}
				
				if (args.Length >= 4)
				{
					
					if (!args[3].Equals("-time"))
					{	
						return "L'option n'est pas valide";
					}

					if(args.Length >= 5 && isInt(args[4]))
					{
						iterator = int.Parse(args[4]);
						
						if (args.Length == 6)
						{
							if(args[5].Equals("-avg")){
							
								mode = modeOfprocessing.testUrlTimeAvg ;
								return "Commande Valide" ; 
							}
							else
								return "Param 6 non valide";
						}
						
							mode = modeOfprocessing.testUrlTime ;
							return "Commande Valide" ; 
						
					}
					else{
						 return "Pas un nombre ou abscent";
					}
					
				}
			
			}
			return "Commande Valide" ; 
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
		
		public static bool isAnUrl(string urlTest)
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
