/*
 * Created by SharpDevelop.
 * User: Romain GABEL
 * Date: 26/05/2014
 * Time: 16:55
 * 
 * To change this template use Tools | Options | Coding | Edit Standard Headers.
 */
using System;
using System.Collections;
using nurl.parse;
using CommandLine;
using CommandLine.Text;

namespace nurl
{
	class Program
	{
		public static void Main(string[] args)
		{
			myParser parse = new myParser(args);
			parse.parseArgs();
			
			Console.WriteLine("Url Donnée : {0}", parse.inputUrl);
			Console.WriteLine("File Donnée : {0}\n\n", parse.outputFile);
	
			Console.Write("Press any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}