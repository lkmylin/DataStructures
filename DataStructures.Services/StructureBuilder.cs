using System;
using System.Collections.Generic;
using DataStructures.Models;

namespace DataStructures.Services
{
    public interface IStructureBuilder<T>
    {
        IBinaryTree<T> BuildTree(List<IBinaryTreeNode<T>> nodes);
    }

    public class StructureBuilder<T> : IStructureBuilder<T>
    {
        public IBinaryTree<T> BuildTree(List<IBinaryTreeNode<T>> nodes)
        {
            if (nodes == null || nodes.Count == 0) return null;
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
            var root = nodes[0];
            root.Type = BinaryTreeNodeType.Root;
            return new BinaryTree<T>(root, nodes.Count, GetDepth(nodes.Count));
        }

        private int GetDepth(int nodeCount)
        {
            var index = 0;
            var maxCount = 1;
            while (maxCount <= nodeCount)
            {
                index += 1;
                maxCount = (int)Math.Pow(2, index);
            }
            return index;
        }
    }
}