using System;
using System.Linq;
using System.IO;
using System.Collections.Generic;

namespace Riddle_generator
{
    public class StartUp
    {
        private static string path = @"../../../text.txt";
     
        
        private static StreamReader reader;
        private static StreamWriter writer;
        static void Main(string[] args)
        {
            var list = new List<string>();
            
            var random = new Random();
            var usedIndexes = new List<int>();
            reader = new StreamReader(path);
            using (reader)
            {
                var lines = reader.ReadToEnd();
                list = lines.Split("\r\n").ToList();
            }
            for (int i = 0; i < list.Count; i++)
            {
                if (string.IsNullOrEmpty(list[i]))
                {
                    list.Remove(list[i]);
                }
            }
            Console.WriteLine(ShowMenu());
            while (true)
            {
                int cmd = int.Parse(Console.ReadLine());
                if (cmd == 0)
                {
                    Console.WriteLine(ShowMenu());
                }
                else if (cmd == 1)
                {
                    while (true)
                    {
                        Console.Write("Please enter your riddle(one at a time): ");
                        string riddle = Console.ReadLine();
                        if (!string.IsNullOrWhiteSpace(riddle))
                        {
                            while (true)
                            {
                                Console.Write("Please enter the answer(required field): ");
                                string answer = Console.ReadLine();
                                if (!string.IsNullOrWhiteSpace(answer))
                                {
                                    AddRiddle(riddle, answer,list);
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("You have not entered anything in the answer field!");
                                }
                            }
                            break;
                        }
                        else
                        {
                            Console.WriteLine("You have not entered anything in the riddle field!");
                        }
                    }
                }
                else if (cmd == 2)
                {
                    var isExist = false;
                    var currIndex = -1; 
                    for (int i = 0; i < list.Count; i++)
                    {
                        int index = random.Next(0, list.Count);
                        if (!usedIndexes.Contains(index))
                        {
                            isExist = true;
                            currIndex = index;
                            var currRiddle = list[index].Split('/')[0];
                            Console.WriteLine("Here is your riddle: ");
                            Console.WriteLine(currRiddle);
                            usedIndexes.Add(index);
                            break;
                        }
                        else
                        {
                            if (usedIndexes.Count == list.Count)
                            {
                                break;
                            }
                            i--;
                        }
                        
                    }
                    if (!isExist)
                    {
                        Console.WriteLine("There is no more riddles that you haven't answered.");
                    }
                    else
                    {
                        Console.Write("Your answer: ");
                        string answer = Console.ReadLine();
                        if (answer == list[currIndex].Split('/')[1])
                        {
                            Console.WriteLine("Congratulations, you are smart.");
                        }
                        else
                        {
                            Console.WriteLine("Sorry, it's wrong.");
                            Console.WriteLine("The riddle may fall on you again, so be ready!");
                            usedIndexes.Remove(currIndex);
                        }
                    }  
                }
                else if (cmd == 3)
                {
                    Console.WriteLine($"At the moment there are {list.Count} riddles.");
                }
                else if (cmd == 4)
                {
                    list.Clear();
                    Console.WriteLine("All the riddles are cleared.");
                }
                else if (cmd == 5)
                {
                    Console.WriteLine("Bye :)");
                    writer = new StreamWriter(path);
                    using (writer)
                    {
                        for (int i = 0; i < list.Count; i++)
                        {
                            writer.WriteLine(list[i]);
                        }
                    }
                    return;
                }
                else
                {
                    Console.WriteLine("Wrong input try with number from 1 to 5!");
                }
            }
        }
        static string ShowMenu()
        {
            return "--------------Menu--------------"
                + "\r\n" + "|[0] click to show the menu    |"
                + "\r\n" + "|[1] click to add a new riddle |"
                + "\r\n" + "|[2] click to recieve a riddle |"
                + "\r\n" + "|[3] click to recieve the count|"
                + "\r\n" + "|[4] click to clear the riddles|"
                + "\r\n" + "|[5] click to stop the program |"
                + "\r\n" + "--------------------------------";
        }
        static void AddRiddle(string riddle, string answer,List<string> list)
        {
            string fullRiddle = riddle + "/" + answer;
            list.Add(fullRiddle);  
        }
    }
}
