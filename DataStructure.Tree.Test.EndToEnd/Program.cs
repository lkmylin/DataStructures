using System;
using System.Collections.Generic;

namespace DataStructure.Tree.Test.EndToEnd
{
    class Program
    {
        static void Main(string[] args)
        {
            var size = 15;
            var nodes = new List<BinaryTreeNode<int>>();
            for (var i = 1; i <= size; i++)
            {
                nodes.Add(new BinaryTreeNode<int>(i));
            }
            var tree = new BinaryTree<int>(nodes);
            tree.Print();
            Console.Read();
        }
    }
}