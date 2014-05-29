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
using CommandLine;
using CommandLine.Text;

namespace nurl
{
	class Program
	{
		public static void Main(string[] args)
		{
			Parser p = new Parser(args);
			string valid = p.validArgument();
			
			if (valid.Equals("Commande Valide"))
			{
				Console.WriteLine(valid);
				Console.WriteLine("Url ={0} \n", p.inputUrl);
				
				Processing process = new Processing(p);
				process.processNurl();
				
			}
			else
			{
			
				Console.WriteLine(valid);
			
			}
			
	
			Console.Write("\n\nPress any key to continue . . . ");
			Console.ReadKey(true);
		}
	}
}