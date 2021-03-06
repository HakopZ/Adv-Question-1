﻿using System;
using System.IO;
using System.Linq;

namespace Advance_Quesiton_1
{
    class Program
    {
        static void Main(string[] args)
        {
            var words = File.ReadAllLines(@"C:\Users\hakop\source\repos\Advance Quesiton 1\Advance Quesiton 1\words.txt");
            SpellCheck spCheck = new SpellCheck(words.ToList());

            while(true)
            {
                Console.WriteLine("Type prefix");
                var prefix = Console.ReadLine();
                if(prefix.Length < 3)
                {
                    Console.WriteLine("Too short");
                    continue;
                }
                else
                {
                    var temp = spCheck.CheckWord(prefix);
                    temp.ForEach(x => Console.WriteLine(x));
                }
            }


        }
    }
}
