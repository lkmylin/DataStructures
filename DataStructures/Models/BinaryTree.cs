using System;

namespace DataStructures.Models
{
    public interface IBinaryTree<T> where T : IComparable
    {
        IBinaryTreeNode<T> Root { get; }
        int NodeCount { get; }
        int Depth { get; }
        BinaryTreeType Type { get; }
    }

    public class BinaryTree<T> : IBinaryTree<T> where T : IComparable
    {
        public IBinaryTreeNode<T> Root { get; }
        public int NodeCount { get; }
        public int Depth { get; }
        public BinaryTreeType Type { get; }

        internal BinaryTree(IBinaryTreeNode<T> root, int nodeCount, int depth, BinaryTreeType type)
        {
            Root = root;
            NodeCount = nodeCount;
            Depth = depth;
            Type = type;
        }
    }

    public enum BinaryTreeType
    {
        Complete,
        Search
    }
}