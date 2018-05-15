using System;
using System.IO;
using System.Text.RegularExpressions;
namespace CaesarCipher
{
    class Cipher
    {
        public static void Main()
        {	
			//input from file
			string phrase = File.ReadAllText(Path.Combine(Environment.CurrentDirectory, "caesarShiftEncoded.txt"));
			
			//other variables
			int answer;
			int shift;
            
			try
			{
				//user choice
				Console.WriteLine("\nDo you want to:\n\n1. Decrypt File\nor\n2. Custom Shift?");
				answer = Convert.ToInt32(Console.ReadLine());
				
				switch (answer)
				{
					//Decryption
					case 1:
					
					//applies methods
					phrase = removeExtra(phrase);
					shift = 0;
					
					//displays each shift for user
					Console.WriteLine("\nShift\tPhrase");
					
					for (int ctr = 1; ctr < 27; ctr++)
					{
						shift = 1;	
						phrase = cShift(phrase, shift);			
						Console.WriteLine(" {0}\t{1}\n", ctr, phrase);
					}
					
					//find out the correct shift
					shift = 0;
					Console.WriteLine("Which shift is correct?");
					shift = Convert.ToInt32(Console.ReadLine());
					
					//displays and prints correct phrase
					phrase = cShift(phrase, shift);
					Console.ForegroundColor = ConsoleColor.Green;
					Console.WriteLine("\nYou selected shift {0}. The following message is being sent to output file:\n{1}",shift, phrase);
					File.WriteAllText(Path.Combine(Environment.CurrentDirectory, "caesarShiftPlainText.txt"), phrase);
					Console.ForegroundColor = ConsoleColor.White;
					
					//debug
					Console.WriteLine("\n\nPlease press any key to exit...");
					Console.ReadKey();	
					break;
				
					// Encryption
					case 2:
					
					//gets phrase
					Console.WriteLine("\nPlease enter the message you would like shifting:");
					phrase = removeExtra(Console.ReadLine());	
					
					//gets shift
					Console.WriteLine("\nHow many characters would you like to shift by?");
					shift = Convert.ToInt32(Console.ReadLine());
					
					//applies methods
					phrase = cShift(phrase, shift);	

					//displays output
					Console.ForegroundColor = ConsoleColor.Green;
					Console.Write("\nYour new phrase is:\n{0}", phrase);
					Console.ForegroundColor = ConsoleColor.White;

					//debug
					Console.WriteLine("\n\nPlease press any key to exit...");
					Console.ReadKey();					
					break;

					//invalid inputs
					default:
					Console.ForegroundColor = ConsoleColor.Red;
					Console.WriteLine("Invalid input. Press any key to exit...");
					Console.ReadKey();
					Console.ForegroundColor = ConsoleColor.White;
					break;
				}
			}
			
			//will show any unexpected errors
			catch (Exception e)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine("The program will not work because:\n{0}", e.Message);
				Console.ForegroundColor = ConsoleColor.White;
			}		
		}
		
		public static string removeExtra(string phrase)
        {
			//converts to uppercase and removes spaces, newlines and tabs			
			phrase = phrase.ToUpper();
			string newPhrase = Regex.Replace(phrase, @"[^A-Z]", "");
			
			return newPhrase;
        }
		
		public static string cShift(string phrase, int shift)
		{	
			string output = "";
			char newC;
			
			foreach (char c in phrase)
			{
				//converting
				int asciiValue = c;
			
				//ascii plus shift
				asciiValue += shift;
			
				//Keeps the characters with A-Z
				if (asciiValue > 90)
				{
					asciiValue -= 26;
				}
			
				//converts back to characters
				newC = Convert.ToChar(asciiValue);
				
				//converts back to string
				output += newC;	
			}	
			return output;
		}
	}	
}