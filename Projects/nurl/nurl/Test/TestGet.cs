/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 28/05/2014
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Net;
using NUnit.Framework;

namespace nurl.Test
{
	[TestFixture]
	public class TestGet
	{
		[TestCase(1 , "http://webbdoger93.free.fr/testNurl/hello.html" , "<h1>Hello test<h1>" , true )]
		[TestCase(2 , "http://webbdoger93.free.fr/testNurl/hello.html" , "<h1>Hello test fake<h1>" , false )]
		[TestCase(3 , "http://webbdoger93.free.fr/testNurl/empty.html" , "Emplty file" , true )]
		public void Get_the_content_of_a_page(int id , string tested , string test , bool result )
		{
			Processing process = new Processing(tested, "");			
			string resultContent = process.getContentOfUrl();			
		    Assert.AreEqual(result, resultContent.Equals(test));
		}
		
		[TestCase(1 , "http://webbdoger93.free.fr/testNurl/hello.html" , @"C:\Users\Romain GABEL\Desktop\hello.html" )]
		[TestCase(2 , "https://www.linxo.com/" , @"C:\Users\Romain GABEL\Desktop\linxoIndex.html" )]
		public void Save_the_content_in_page(int id, string _url , string _file )
		{
			
			Processing process = new Processing(_url , _file);			
			string resultContent = process.getContentOfUrl();
			bool result = process.saveContentInFile(resultContent);
			
			
			Assert.IsTrue(result);	
		}
	}
	
		public  enum modeOfprocessing {
			
			getUrl,
			getUrlSave,
			testUrlTime,
			testUrlTimeAvg,
			
		};

	
	public class Processing
	{	
			
		public  modeOfprocessing mode;
		public string inputUrl ; 
		public string outputFile ; 
		
		public Processing(string _Url, string _file ){
			
			inputUrl = _Url ; 
			outputFile = _file ; 
		}
		
		public Processing(Parser p)
		{
			mode = p.mode ; 
		
			if (!string.IsNullOrEmpty(inputUrl))
			{
				inputUrl = p.inputUrl ; 
			}	
			
			if (!string.IsNullOrEmpty(outputFile))
			{
				outputFile= p.outputFile ; 
			}
	
			
		}
		
		public string  getContentOfUrl(){
		
			var webRequest = WebRequest.Create(inputUrl);
			
			using (var response = webRequest.GetResponse())
			using(var content = response.GetResponseStream())
			using(var reader = new System.IO.StreamReader(content)){
			    var strContent = reader.ReadToEnd();
				
				
				if (string.IsNullOrEmpty(strContent))
				{
					return "Emplty file" ;
				}	
			
				return strContent; 
			}
			
		}
		
		public bool saveContentInFile(string _content)
		{      
			try
			{
				System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile);
				file.WriteLine(_content);
				
				file.Close();
				return true;
			}
			catch(InvalidOperationException e)
			{
				Console.WriteLine(e);
				return false; 
			}
	        
		}
		
		public void displayString(string src){
			
			Console.WriteLine(src);
		
		}
		
	}
	
	
}
