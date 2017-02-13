using System;
using System.Collections.Generic;

namespace LuceneConsoleApplication
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                LuceneIndexer startIndexer = new LuceneIndexer();
                Console.WriteLine("Starting Lucene index creation process for search...\r\n");
                Console.WriteLine("Lucene index has been created from the following list:");
                // This method will create Lucene indexes according to your given list items.
                startIndexer.CreateLuceneIndexes();
                Console.WriteLine("");
                // You can check CFX and CFS file in folder "..\LuceneConsoleApplication\App_Data". 
                // Open the file in Notepad or any other text editor and you can see list of items.
                Console.WriteLine("Lucene Index creation successful!\r\n");


                GetSearchLucene getSearchLucene = new GetSearchLucene();
                Console.WriteLine("Enter a name to search:");
                // Get input string from user
                string inputParam = Console.ReadLine();
                List<LuceneSearcher.LuceneData> result = getSearchLucene.GetSearchLuceneText(inputParam);
                Console.WriteLine("");
                Console.WriteLine("You searched for:");
                if (result.Count > 0)
                {
                    foreach (var item in result)
                    {
                        Console.WriteLine(item.Actor);
                    }
                }
                else
                {
                    Console.WriteLine("Sorry! No matching records found.");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
    }
}
