using System;
using System.Collections.Generic;
using DataStructures.Models;

namespace DataStructures.Services
{
    public interface IStructureBuilder<T> where T : IComparable
    {
        IBinaryTree<T> BuildTree(List<T> nodeValues, BinaryTreeType type);
    }

    public class StructureBuilder<T> : IStructureBuilder<T> where T : IComparable
    {
        private readonly INodeProvider<T> _nodeProvider;

        public StructureBuilder(INodeProvider<T> nodeProvider)
        {
            _nodeProvider = nodeProvider;
        }

        public IBinaryTree<T> BuildTree(List<T> nodeValues, BinaryTreeType type)
        {
            if (nodeValues == null || nodeValues.Count == 0) return null;
            var nodes = new List<IBinaryTreeNode<T>>();
            var depth = 0;
            foreach (var value in nodeValues)
            {
                nodes.Add(_nodeProvider.GetBinaryTreeNode(value));
            }
            IBinaryTreeNode<T> root = null;
            switch (type)
            {
                case BinaryTreeType.Complete:
                    {
                        root = BuildCompleteTree(nodes);
                        break;
                    }
                case BinaryTreeType.Search:
                    {
                        root = BuildSearchTree(nodes);
                        break;
                    }
            }
            root.Type = BinaryTreeNodeType.Root;
            GetDepth(root, ref depth);
            return new BinaryTree<T>(root, nodes.Count, depth, type);
        }

        private IBinaryTreeNode<T> BuildCompleteTree(List<IBinaryTreeNode<T>> nodes)
        {
            for (var i = 0; i < nodes.Count; i++)
            {
                var leftChildIndex = 2 * i + 1;
                var rightChildIndex = 2 * i + 2;
                if (leftChildIndex < nodes.Count)
                {
                    nodes[i].Left = nodes[leftChildIndex];
                    nodes[i].Left.Parent = nodes[i];
                    nodes[i].Left.Type = BinaryTreeNodeType.Left;
                }
                if (rightChildIndex < nodes.Count)
                {
                    nodes[i].Right = nodes[rightChildIndex];
                    nodes[i].Right.Parent = nodes[i];
                    nodes[i].Right.Type = BinaryTreeNodeType.Right;
                }
            }
            return nodes[0];
        }

        private IBinaryTreeNode<T> BuildSearchTree(List<IBinaryTreeNode<T>> nodes)
        {
            var root = nodes[0];
            foreach(var node in nodes)
            {
                root = AddNodeToSearchTree(root, node);
            }
            return root;
        }

        private IBinaryTreeNode<T> AddNodeToSearchTree(IBinaryTreeNode<T> root, IBinaryTreeNode<T> node)
        {
            if (node == null && root == null) return null;
            if (node == null) return root;
            if (root == null || root.Value == null) return node;
            var compareResult = node.Value.CompareTo(root.Value);
            if (compareResult == 0)
            {
                return root;
            }
            node.Parent = root;
            if (compareResult < 0)
            {
                node.Type = BinaryTreeNodeType.Left;
                root.Left = AddNodeToSearchTree(root.Left, node);
            }
            else
            {
                node.Type = BinaryTreeNodeType.Right;
                root.Right = AddNodeToSearchTree(root.Right, node);
            }
            return root;
        }

        private void GetDepth(IBinaryTreeNode<T> root, ref int depth)
        {
            if (root == null) return;
            var currentDepth = root.Level + 1;
            if (currentDepth > depth) depth = currentDepth;
            GetDepth(root.Left, ref depth);
            GetDepth(root.Right, ref depth);
        }
    }
}