using System;
using System.Text.RegularExpressions;
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
            container.AddSingleton<INodeProvider<int>, NodeProvider<int>>();
            var resolver = container.BuildServiceProvider();

            var builder = resolver.GetService<IStructureBuilder<int>>();
            var stringifier = resolver.GetService<IStringificationService<int>>();
            var regex = new Regex("^(-?[\\d]+,)+-?[\\d]+$");
            var input = string.Empty;
            var size = 15;            

            while (input != "exit")
            {
                Console.Write("Enter a positive integer n to create a complete binary tree with n nodes, or a comma-delimited list of integers to create a binary search tree. For best results, avoid generating trees of more than 5 levels (doing so will result in distorted images on most screens). Enter \"exit\" to quit: ");
                input = Console.ReadLine();
                if (int.TryParse(input, out size))
                {
                    var nodeValues = new List<int>();
                    for (var i = 1; i <= size; i++)
                    {
                        nodeValues.Add(i);
                    }
                    var tree = builder.BuildTree(nodeValues, BinaryTreeType.Complete);
                    Console.WriteLine(stringifier.Stringify(tree));
                    Console.WriteLine();
                }
                else if (regex.IsMatch(input))
                {
                    var split = input.Split(',');
                    var nodeValues = new List<int>();
                    foreach(var strNodeValue in split)
                    {
                        if (int.TryParse(strNodeValue, out int iNodeValue))
                        {
                            nodeValues.Add(iNodeValue);
                        }
                    }
                    var tree = builder.BuildTree(nodeValues, BinaryTreeType.Search);
                    Console.WriteLine(stringifier.Stringify(tree));
                    Console.WriteLine();
                }
                else if (input.ToLower() != "exit")
                {
                    Console.WriteLine();
                    Console.WriteLine("Please enter a valid input, e.g. 15 or 6,1,4,9,7,21");
                    Console.WriteLine();
                }
            }
        }
    }}