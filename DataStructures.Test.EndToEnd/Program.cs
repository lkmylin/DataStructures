using System;
using System.Collections.Generic;
using DataStructures.Models;
using DataStructures.Services;
using Microsoft.Extensions.DependencyInjection;

namespace DataStructures.Test.EndToEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var container = new ServiceCollection();
            container.AddSingleton<IStructureBuilder<int>, StructureBuilder<int>>();
            container.AddSingleton<IStringificationService<int>, StringificationService<int>>();
            var resolver = container.BuildServiceProvider();

            var builder = resolver.GetService<IStructureBuilder<int>>();
            var stringifier = resolver.GetService<IStringificationService<int>>();
            var strSize = string.Empty;
            var size = 15;

            while (strSize != "exit")
            {
                Console.Write("Enter the number of nodes (1 < n < 64), or enter \"exit\" to quit: ");
                strSize = Console.ReadLine();
                if (int.TryParse(strSize, out size) && size > 1 && size < 64)
                {
                    var nodes = new List<IBinaryTreeNode<int>>();
                    for (var i = 1; i <= size; i++)
                    {
                        nodes.Add(new BinaryTreeNode<int>(i));
                    }
                    var tree = builder.BuildTree(nodes);
                    Console.WriteLine(stringifier.Stringify(tree));
                    Console.WriteLine();
                }
                else if (strSize.ToLower() != "exit")
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid number of nodes, e.g. 15");
                    Console.WriteLine();
                }
            }
        }
    }
}