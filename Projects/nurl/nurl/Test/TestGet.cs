/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 28/05/2014
 * Time: 18:22
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Net;
using System.Security.Cryptography;
using NUnit.Framework;
using nurl;

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
		
		
		[TestCase(1 , "http://webbdoger93.free.fr/testNurl/hello.html", modeOfprocessing.testUrlTime, 5 )]
		[TestCase(2 , "http://webbdoger93.free.fr/testNurl/hello.html", modeOfprocessing.testUrlTimeAvg, 5 )]
		public void TestGetTimer(int id, string _url ,modeOfprocessing _mode ,int _iterator)
		{
			
			Processing process = new Processing(_url, "" , _mode);			
			TimeSpan resultContent = process.getTimeSpent(_iterator);
	
			Assert.IsTrue(TimeSpan.Zero != resultContent);
	
		}
		
		[TestCase(1 , new double[] {1.5, 2.3 , 1.6 , 2.4}, 1.95 ,  4 )]
		public void TestGetTimerAvg(int id, double[] list , double test ,int _iterator)
		{
			
			Processing process = new Processing("", "");			
			double res = process.avgTime(list , 4);
	
			Console.WriteLine("Res : {0}  , test  : {1}", res , test );
			
			Assert.IsTrue( (float)res == (float)test);
	
		}
		
		
		
	}

//		public  enum modeOfprocessing {
//			
//			getUrl,
//			getUrlSave,
//			testUrlTime,
//			testUrlTimeAvg,
//			
//		};

	
//	public class Processing
//	{	
//			
//		public  modeOfprocessing mode;
//		public string inputUrl ; 
//		public string outputFile ; 
//		
//	
//		public Processing(string _Url, string _file , modeOfprocessing _mode ){
//			
//			inputUrl = _Url ; 
//			outputFile = _file ; 
//			mode = _mode ;
//		}
//		
//		public Processing(string _Url, string _file ){
//			
//			inputUrl = _Url ; 
//			outputFile = _file ; 
//		}
//		
//		public Processing(Parser p)
//		{
//			mode = p.mode ; 
//		
//			if (!string.IsNullOrEmpty(inputUrl))
//			{
//				inputUrl = p.inputUrl ; 
//			}	
//			
//			if (!string.IsNullOrEmpty(outputFile))
//			{
//				outputFile= p.outputFile ; 
//			}
//	
//			
//		}
//		
//		public string  getContentOfUrl(){
//		
//			var webRequest = WebRequest.Create(inputUrl);
//			
//			using (var response = webRequest.GetResponse())
//			using(var content = response.GetResponseStream())
//			using(var reader = new System.IO.StreamReader(content)){
//			    var strContent = reader.ReadToEnd();
//				
//				
//				if (string.IsNullOrEmpty(strContent))
//				{
//					return "Emplty file" ;
//				}	
//			
//				return strContent; 
//			}
//			
//		}
//		
//		public TimeSpan getTimeSpent(int iterator)
//		{
//			
//			
//			TimeSpan ts = TimeSpan.Zero; 
//			List<double> list = new List<double>();
//			
//			for (int i = 1; i <= iterator; i++)
//	        {
//	            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest) WebRequest.Create(inputUrl);
//				DateTime oldDate = DateTime.Now;
//				var response = myHttpWebRequest1.GetResponse();
//				DateTime newDate = DateTime.Now;
//				ts = newDate - oldDate;
//				Console.WriteLine("Total time: {0} ms", ts.Milliseconds);
//				
//				response.GetResponseStream().Close();
//				
//				list.Add(ts.Milliseconds);
//				
//	        }
//			
//			if (mode == modeOfprocessing.testUrlTimeAvg)
//			{
//				double [] arg = list.ToArray() ;
//				Console.WriteLine("Moyenne : {0}", avgTime(arg , iterator));
//			}
//			
//			return ts;	
//			
//		}
//		
//		 public double avgTime(double[] t, int iterator)
//		{
//				double result, somme = 0;
//				
//				for(int i=0; i<t.Length; i++){
//					somme=somme+t[i];
//				}
//				
//				result=somme/iterator;
//				
//				return result;
//			}
//					
//		public bool saveContentInFile(string _content)
//		{      
//			try
//			{
//				System.IO.StreamWriter file = new System.IO.StreamWriter(outputFile);
//				file.WriteLine(_content);
//				
//				file.Close();
//				return true;
//			}
//			catch(InvalidOperationException e)
//			{
//				Console.WriteLine(e);
//				return false; 
//			}
//	        
//		}
//		
//		public void displayString(string src){
//			
//			Console.WriteLine(src);
//		
//		}
//		
//	}
	
	
}
