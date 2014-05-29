/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 29/05/2014
 * Time: 23:20
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections.Generic;
using System.Net;

namespace nurl
{
	
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
		int iterator ;
		
		
		public Processing()
		{
		}
		
		public Processing(string _Url, string _file , modeOfprocessing _mode ){
			
			inputUrl = _Url ; 
			outputFile = _file ; 
			mode = _mode ;
		}
		
		public Processing(string _Url, string _file ){
			
			inputUrl = _Url ; 
			outputFile = _file ; 
		}
		
		public Processing(Parser p)
		{
			mode = p.mode ; 
			iterator = p.iterator ;
		
			if (!string.IsNullOrEmpty(p.inputUrl))
			{
				inputUrl = p.inputUrl ; 
			}		
			if (!string.IsNullOrEmpty(p.outputFile))
			{
				outputFile= p.outputFile ; 
			}	
		}
		
		public void processNurl(){
		
			if (mode == modeOfprocessing.getUrl)
			{
				displayString(getContentOfUrl());
				
			}
			else if (mode == modeOfprocessing.getUrlSave)
			{
				string content =  getContentOfUrl();
				saveContentInFile(content);
			}
			else if(mode == modeOfprocessing.testUrlTime)
			{
				getTimeSpent(iterator);
			}
			else if(mode == modeOfprocessing.testUrlTimeAvg)
			{
				getTimeSpent(iterator);
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
		
		public TimeSpan getTimeSpent(int iterator)
		{
			
			
			TimeSpan ts = TimeSpan.Zero; 
			List<double> list = new List<double>();
			
			for (int i = 1; i <= iterator; i++)
	        {
	            HttpWebRequest myHttpWebRequest1 = (HttpWebRequest) WebRequest.Create(inputUrl);
				DateTime oldDate = DateTime.Now;
				var response = myHttpWebRequest1.GetResponse();
				DateTime newDate = DateTime.Now;
				ts = newDate - oldDate;
				Console.WriteLine("Total time: {0} ms", ts.Milliseconds);
				
				response.GetResponseStream().Close();
				
				list.Add(ts.Milliseconds);
				
	        }
			
			if (mode == modeOfprocessing.testUrlTimeAvg)
			{
				double [] arg = list.ToArray() ;
				Console.WriteLine("Moyenne : {0}", avgTime(arg , iterator));
			}
			
			return ts;	
			
		}
		
		public double avgTime(double[] t, int iterator)
		{
				double result, somme = 0;
				
				for(int i=0; i<t.Length; i++){
					somme=somme+t[i];
				}
				
				result=somme/iterator;
				
				return result;
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
