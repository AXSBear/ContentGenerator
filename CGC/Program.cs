using System;
using System.IO;

namespace CGC
 {
     internal class Program
     {
         public static void Main(string[] args)
         {
             Console.WriteLine("--- Welcome! This is content generator!");
             Console.WriteLine("--- Please, enter the path to TSV file...");
             string filePath = Console.ReadLine();
             if (string.IsNullOrWhiteSpace(filePath))
             {
                 filePath = "FileExamples/example.tsv";
             }

             while (true)
             {
                 try
                 {
                     string example =
                         SentenceGenerator.Generate(filePath);
                     Console.WriteLine(example);
                     Console.WriteLine();
                 }
                 catch (ArgumentException e)
                 {
                     Console.WriteLine("--- ERROR! File is incorrect! Plese select another file. Abort...");
                     break;
                 }
                 catch (IOException e)
                 {
                     Console.WriteLine("--- Something went wrong...");
                     Console.WriteLine(e.ToString());
                     break;
                 }

                 Console.WriteLine("--- Any button to get next sentence. Enter q or Q to exit");
                 if (Console.ReadLine()?.ToLower() == "q") break;
             }
         }
     }
 }