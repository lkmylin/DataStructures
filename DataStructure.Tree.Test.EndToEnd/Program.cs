using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Test.EndToEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var strSize = string.Empty;
            var size = 15;            
            while (strSize != "exit")
            {                
                Console.Write("Enter the number of nodes (1 < n < 64), or enter \"exit\" to quit: ");
                strSize = Console.ReadLine();                
                if (int.TryParse(strSize, out size) && size > 1 && size < 64)
                {
                    var nodes = new List<BinaryTreeNode<int>>();
                    for (var i = 1; i <= size; i++)
                    {
                        nodes.Add(new BinaryTreeNode<int>(i));
                    }
                    var tree = new BinaryTree<int>(nodes);
                    tree.Print();
                    Console.WriteLine();
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