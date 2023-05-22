// See https://aka.ms/new-console-template for more information
using System;
using System.Diagnostics;
using System.Diagnostics.Tracing;
using System.Reflection;
using System.Runtime.CompilerServices;

public class Lab1
{
    static int count = 0;

    public static void Main()
    {
        while (true) {
          

            try
            {
                    Console.WriteLine("Choose an option:");
                    Console.WriteLine("1 - Import Words from File");
                    Console.WriteLine("2 - Bubble Sort words");
                    Console.WriteLine("3 - LINQ/Lambda sort words");
                    Console.WriteLine("4 - Count the Distinct Words");
                    Console.WriteLine("5 - Take the last 50 words");
                    Console.WriteLine("6 - Reverse print the words");
                    Console.WriteLine("7 - Get and display words that end with 'd' and display the count");
                    Console.Write("8 - Get and display words that start with 'r' and display the count\n9 - Get and display words that are more than 3 characters long and include the letter 'a', and display the count\nx - Exit\n\nSelect an option: ");
                    string option = Console.ReadLine();

                    switch (option)
                    {
                        case "1":
                        IList<string> words = readFile();
                        
                            foreach (string word in words)
                            {
                                count++;
                            }
                           if(count != 0)
                            Console.WriteLine($"Word count is {count}");
                            break;
                        case "2":
                        if (count != 0)
                        {
                            Stopwatch timer = new Stopwatch();
                            timer.Start();
                            IList<string> bubble = bubbleSortArray(readFile());
                            timer.Stop();
                            Console.WriteLine($"Elapsed time: {timer.ElapsedMilliseconds} ms");
                            foreach (string word in bubble)
                            {
                                Console.WriteLine($"{word}");
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                            break;
                        case "3":
                        if (count != 0)
                        {
                            Stopwatch timer2 = new Stopwatch();
                            timer2.Start();
                            IList<string> linkSort= LINQSort(readFile());  
                            timer2.Stop();
                            Console.WriteLine($"Elapsed time: {timer2.ElapsedMilliseconds} ms");
                            foreach (string word in linkSort)
                            {
                                Console.WriteLine($"{word}");
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                            break;
                        case "4":
                        if (count != 0)
                        {
                            Console.WriteLine($"Number of distinct words: {readFile().Distinct().Count()}");
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                        break;
                        case "5":
                        if (count != 0)
                        {
                            IList<string> lastFifty = readFile().TakeLast(50).ToList();
                            foreach (string word in lastFifty)
                        {
                            Console.WriteLine($"{word}");
                        }
                }
                        else { Console.WriteLine("Please Load words first!!!"); }
                break;
                        case "6":
                        if (count != 0)
                        {
                            for (int i = count - 1; i >= 0; i--)
                            {
                                Console.WriteLine(readFile()[i]);
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                        break;
                        case "7":
                        if (count != 0)
                        {
                            List<string> dWords= readFile().Where(word => word.EndsWith("d", StringComparison.OrdinalIgnoreCase)).ToList();   
                            Console.WriteLine($"The {dWords.Count} words that end with d are:");
                            foreach(string word in dWords)
                            {
                                Console.WriteLine(word);
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                        break;
                        case "8":
                        if (count != 0)
                        {
                            List<string> rWords = readFile().Where(word => word.StartsWith("r", StringComparison.OrdinalIgnoreCase)).ToList();
                            Console.WriteLine($"The {rWords.Count} words that end with r are:");
                            foreach (string word in rWords)
                            {
                                Console.WriteLine(word);
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                        break;
                        case "9":
                        if (count != 0)
                        {
                            List<string> multiWords = readFile().Where(word => word.Length > 3 && word.Contains("a", StringComparison.OrdinalIgnoreCase)).ToList();
                            Console.WriteLine($"The {multiWords.Count} words that end with d are:");
                            foreach (string word in multiWords)
                            {
                                Console.WriteLine(word);
                            }
                        }
                        else { Console.WriteLine("Please Load words first!!!"); }
                        break;
                        case "x":
                            Environment.Exit(0);
                            break;
                        default:
                        Console.WriteLine("Invalid Option");
                            break;
                    }
                }
                catch (IOException ex)
                {
                    Console.WriteLine("An I/O error occurred: " + ex.Message);
                }
                catch (OutOfMemoryException ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
                catch (ArgumentOutOfRangeException ex)
                {
                    Console.WriteLine("An error occurred: " + ex.Message);
                }
            }
        }
    

    public static IList<string> readFile()
    {
        try
        {
            return File.ReadAllLines("Words.txt").ToList();
        }
        catch (FileNotFoundException)
        {
            Console.WriteLine("File not found.");
        }
        catch (IOException)
        {
            Console.WriteLine("Error reading the file.");
        }
        return new List<string> { };
}
    public static IList<string> bubbleSortArray(IList<string> words)
    {
        int n = count;
        bool swapped;

        for (int i = 0; i < n - 1; i++)
        {
            swapped = false;

            for (int j = 0; j < n - i - 1; j++)
            {
  if(String.Compare(words[j], words[j + 1]) > 0)
                {           
                    string temp = words[j];
                    words[j] = words[j + 1];
                    words[j + 1] = temp;

                    swapped = true;
                }
            }
            if (!swapped)
                break;
        }
        return words;
    }
    public static IList<string> LINQSort(IList<string> words)
    {
        return words.OrderBy(word => word).ToList();
    }
}