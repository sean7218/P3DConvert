using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			string currentDirectory = Directory.GetCurrentDirectory();
			DirectoryInfo directory = new DirectoryInfo(currentDirectory);
			var fileName = Path.Combine(directory.FullName, "P3D.txt");
			var fileContents = ReadTextFileToLines(fileName);

			List<Object> parsedData = ProcessTwoLines(fileContents);
			string output = JsonConvert.SerializeObject(parsedData, Formatting.Indented);

			using (StreamWriter file = File.CreateText(@"C:\Users\sean7218\Documents\Visual Studio 2017\Projects\ConsoleApp1\ConsoleApp1\output.json"))
			{
				JsonSerializer serializer = new JsonSerializer();
				serializer.Serialize(file, output);

			}

			var fileToWrite = SerializeObjectListToStringList(parsedData);
			CreateTextFileFromStringList(fileToWrite);
		}






		public static List<string> ProcessOneLine(string line)
		{
			List<string> processedLine = new List<string>();

			var twospaces = "  ";
			var fourSpaces = "    ";
			var eightSpaces = "        ";

			var first2Spaces = -1;
			var first4Spaces = -1;
			var first8Spaces = -1;
			var second2Spaces = -1;
			var second4Spaces = -1;
			var second8Spaces = -1;
			var third2Spaces = -1;
			var fourth2Spaces = -1;


			first2Spaces = line.IndexOf(twospaces);
			first4Spaces = line.IndexOf(fourSpaces);
			first8Spaces = line.IndexOf(eightSpaces);

			if (first2Spaces >= 0)
			{
				second2Spaces = line.IndexOf(twospaces, first2Spaces + 2);
			}
			else if (first4Spaces >= 0)
			{
				second2Spaces = line.IndexOf(twospaces, first2Spaces + 4);
			}
			else if (first8Spaces >= 0)
			{
				second2Spaces = line.IndexOf(twospaces, first2Spaces + 8);
			}
			else
			{
				second2Spaces = -1;
			}

			if (first2Spaces >= 0)
			{
				second4Spaces = line.IndexOf(twospaces, first2Spaces + 2);
			}
			else if (first4Spaces >= 0)
			{
				second4Spaces = line.IndexOf(twospaces, first2Spaces + 4);
			}
			else if (first8Spaces >= 0)
			{
				second4Spaces = line.IndexOf(twospaces, first2Spaces + 8);
			}
			else
			{
				second4Spaces = -1;
			}


			if (first2Spaces >= 0)
			{
				second8Spaces = line.IndexOf(twospaces, first2Spaces + 2);
			}
			else if (first4Spaces >= 0)
			{
				second8Spaces = line.IndexOf(twospaces, first2Spaces + 4);
			}
			else if (first8Spaces >= 0)
			{
				second8Spaces = line.IndexOf(twospaces, first2Spaces + 8);
			}
			else
			{
				second8Spaces = -1;
			}




			return processedLine;
		}














		// processing the list of strings with two lines at a time while using the
		// checkFormatting function to see how to arrange the actual dictionaries
		public static List<Object> ProcessTwoLines(List<string> contents)
		{
			bool[] formatInfo = new bool[2];
			var line1 = "";
			var line2 = "";

			List<Object> DataContainer = new List<Object>(); //maybe start with StringValuePair<string, object>
			int startLineIndex = 0;
			int endLineIndex = 0;
			
			for (int i = 0; i < (contents.Count-1); i++)
			{
				line1 = contents[i];
				line2 = contents[i + 1];
				formatInfo = CheckFormatting(line1, line2);
				var processedLine = ProcessOneLine(line1);
				// Line1 and Line2 has no space in front
				if (formatInfo[0] == false && formatInfo[1] == false)
				{
					
					var splitedLine = ParseLineFromResults(line1);
					splitedLine = CombinedSplitedLineTailStrings(splitedLine);
					var dataA = new DataTypeA() { Id = 1, KeyA = splitedLine[0], ValueA = (splitedLine.Length > 1) ? splitedLine[1] : "" };
					DataContainer.Add(dataA);
				}
				// Line1 has no space in front but line2 has space in front
				else if (formatInfo[0] == false && formatInfo[1] == true)
				{
					// create a dataB object
					DataTypeB objectB = new DataTypeB();

					// check following lines and stop at when line1 has space and line2 has not
					var j = i;
					while (j < contents.Count)
					{
						if (j == contents.Count - 1) {
							startLineIndex = i;
							endLineIndex = j;
							break;
						}

						formatInfo = CheckFormatting(contents[j], contents[j+1]);
						if (formatInfo[0] == true && formatInfo[1] == false)
						{
							startLineIndex = i;
							endLineIndex = j;
							break;
						}
						j++;
					}

					// count the number of lines and and convert each line to valueA
					var splitedLine = ParseLineFromResults(contents[startLineIndex]);
					splitedLine = CombinedSplitedLineTailStrings(splitedLine);
					objectB.KeyB = splitedLine[0] + " " + splitedLine[1];
					List<DataTypeA> listOfOjbectA = new List<DataTypeA>();
					for (int g = startLineIndex + 1; g <= endLineIndex; g++)
					{
						splitedLine = ParseLineFromResults(contents[g]);
						splitedLine = CombinedSplitedLineTailStrings(splitedLine);
						DataTypeA objectA = new DataTypeA() { KeyA = splitedLine[0], ValueA = (splitedLine.Length > 1) ? splitedLine[1] : "" };
						listOfOjbectA.Add(objectA);
					}

					//then added to valueB which is a list of dataA
					//then actually asigned into the dataContainer
					objectB.ValueB = listOfOjbectA;
					DataContainer.Add(objectB);
					

				}

			}

			return DataContainer;

		}
	
		// Checking two lines at a time
		// See if line1 begin with no space, and line2 begin with space
		// return true if space is found, false if space not found
		public static bool[] CheckFormatting(string line1, string line2)
		{
			bool[] formatInfo = new bool[2];

			if (!char.IsLetter(line1[0]) && char.IsLetter(line2[0]))
			{
				formatInfo[0] = true;
				formatInfo[1] = false;
			}
			if (!char.IsLetter(line1[0]) && !char.IsLetter(line2[0]))
			{
				formatInfo[0] = true;
				formatInfo[1] = true;
			}
			if (char.IsLetter(line1[0]) && !char.IsLetter(line2[0]))
			{
				formatInfo[0] = false;
				formatInfo[1] = true;
			}

			if (char.IsLetter(line1[0]) && char.IsLetter(line2[0]))
			{
				formatInfo[0] = false;
				formatInfo[1] = false;
			}

			return formatInfo;
		}
		
		//given the fullpath to the file and stored in fileName
		//Streaming out the text file into one big string
		public static string ReadFile(string fileName)
		{
			using (var reader = new StreamReader(fileName))
			{
				return reader.ReadToEnd();
			}
		}

		// Reading one line at a time from the text file and 
		// converting them into a list of strings (one line per element) with strings
		// preserved
		public static List<String> ReadTextFileToLines(string fileName)
		{

			
			var results = new List<String>();
			using (var reader = new StreamReader(fileName))
			{
				string line = "";
				while ((line = reader.ReadLine()) != null)
				{
					string values = line;
					results.Add(values);
				}

			}
			return results;

		}

		// Parsing one line by removing the white spaces and output
		// into a array of strings. 
		public static string[] ParseLineFromResults(string line)
		{
				Console.WriteLine("splitting one line into a list of strings seperated by space");
				line.TrimEnd();
				line.TrimStart();
				var splitedLine = line.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
				return splitedLine;
		}

		// Making the dictonary using the splitted line
		public static Dictionary<string, string> ParseSplitedLineToValuePairs(string[] splitedLine)
		{
			Dictionary<string, string> valuePairs = new Dictionary<string, string>();

			// Extract the first value to be the key
			int lengthOfSplitedLine = splitedLine.Length;
			if (lengthOfSplitedLine == 1)
			{
				//Check if it is the main key
				valuePairs.Add(splitedLine[0], "");
				//Check if it is a key without value
			}
			else if (lengthOfSplitedLine == 2)
			{
				//add the first as key and second as value
				valuePairs.Add(splitedLine[0], splitedLine[1]);

			}
			else if (lengthOfSplitedLine > 2)
			{
				//add the first as key and rest as value (one string)
				for (int i = 0; i < lengthOfSplitedLine; i++)
				{
					var key = "";
					var combinedValue = "";
					if (i == 0)
					{
						// do nothing since it is the key
						key = splitedLine[0];
					}
					else
					{
						combinedValue = combinedValue + "," + splitedLine[i];
					}
				}
			}
			else
			{
				Console.WriteLine("ERROR HAS OCCURRED THAT LINE HAS NOT VALUE AT ALL!");
			}


			return valuePairs;
		}

		//Checking if a line has more than two values after spliting the line
		//if it does, then combined the rest as one string
		public static string[] CombinedSplitedLineTailStrings(string[] splitedLine)
		{
			var combinedString = "";
			if (splitedLine.Length > 1)
			{
				for (int i = 1; i < splitedLine.Length; i++)
				{
					if (i == 1)
					{
						combinedString = combinedString + "" + splitedLine[i];
					}
					else
					{
						combinedString = combinedString + "," + splitedLine[i];
					}
				}
			}
			string[] output = new string[2];
			output[0] = splitedLine[0];
			output[1] = combinedString;
			return output;
		}

		// Write object into a text file
		public static List<string> SerializeObjectListToStringList(List<Object> objectList)
		{
			List<string> stringList = new List<string>();
			var line = "";

			for (int i = 0; i < objectList.Count; i++)
			{
				if (objectList[i] is DataTypeA dataA)
				{
					line = dataA.KeyA + " " + dataA.ValueA;
					stringList.Add(line);
				}
				else if (objectList[i] is DataTypeB dataB)
				{
					line = dataB.KeyB;
					foreach (var datA in dataB.ValueB)
					{
						stringList.Add("    " + datA.KeyA + "  " + datA.ValueA);
					}

				}
			}

			return stringList;

		}

		// write a list<string> into a text file
		public static void CreateTextFileFromStringList(List<string> stringList)
		{

			var filePath = @"C:\Users\sean7218\Documents\Visual Studio 2017\Projects\ConsoleApp1\ConsoleApp1\textfileOutput.txt";
			using (var writer = new StreamWriter(filePath))
			{

				writer.WriteLine("This is just a string writing to a text file");
				foreach (var line in stringList)
				{
					writer.WriteLine(line);
				}
			}

		}


	}
}
