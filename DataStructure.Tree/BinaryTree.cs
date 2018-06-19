﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataStructure.Tree
{
    public class BinaryTree<T>
    {
        private const int _indentation = 2;
        private const int _branchLength = 1;
        private readonly BinaryTreeNode<T> _root;
        private readonly List<BinaryTreeNode<T>> _nodes;
        private int _depth;
        public BinaryTreeNode<T> Root => _root;

        public BinaryTree(List<BinaryTreeNode<T>> nodes)
        {
            if (nodes == null || nodes.Count == 0) return;
            SetDepth(nodes.Count);
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
            _root = nodes[0];
            _root.Type = BinaryTreeNodeType.Root;
            _nodes = nodes;
        }

        public void Print()
        {
            Console.WriteLine();
            var indentationAdjustment = 0;
            for (var i = 0; i < _depth; i++)
            {
                var nodes = _nodes.Where(x => x.Level == i).ToList();
                // Length of the connection lines has to decrease exponentially as we move down the tree                
                var branchLength = _branchLength * (int)Math.Pow(2, _depth - i);
                var parentBranchLength = i == 0 ? 0 : _branchLength * (int)Math.Pow(2, _depth - i + 1);
                var parentBaseLength = i == 0 ? 0 : 2 * parentBranchLength - 1;
                indentationAdjustment += parentBaseLength / 2;
                var indentation = _indentation * (int)Math.Pow(2, _depth) - indentationAdjustment;
                // Correction for cumulative error of 1 space per loop after first loop due to
                // half space in parentBaseLength calculation
                indentation -= i;
                var sb = new StringBuilder();
                for (var j = 0; j < nodes.Count; j++)
                {
                    // Must compensate for extra space(s) if value occupies more that 1 space
                    var valueLength = nodes[j].Value.ToString().Length;
                    if (j == 0)
                    {
                        sb.Append(Spaces(indentation));
                    }
                    sb.Append(nodes[j].Value);
                    if (j < nodes.Count - 1)
                    {
                        sb.Append(Spaces(parentBaseLength - (valueLength > 1 ? valueLength - 1 : 0)));
                    }
                }
                Console.WriteLine(sb.ToString());
                if (i == _depth - 1) return;
                for (var k = 1; k < branchLength; k++)
                {
                    var baseLength = 2 * k - 1;
                    sb.Clear();
                    for (var l = 0; l < nodes.Count; l++)
                    {
                        if (l == 0)
                        {
                            sb.Append(Spaces(indentation - k));
                        }
                        sb.Append(nodes[l].Left == null ? " " : "/");
                        sb.Append(Spaces(baseLength));
                        sb.Append(nodes[l].Right == null ? " " : "\\");
                        if (l < nodes.Count - 1)
                        {
                            // Subtract 1 extra space for the error in the base calculation
                            sb.Append(Spaces(parentBaseLength - baseLength - 1));
                        }
                    }
                    Console.WriteLine(sb.ToString());
                }
            }
        }

        private string Spaces(int count)
        {
            var sb = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                sb.Append(" ");
            }
            return sb.ToString();
        }

        private void SetDepth(int count)
        {
            var index = 0;
            var maxCount = 1;
            while (maxCount <= count)
            {
                index += 1;
                maxCount = (int)Math.Pow(2, index);
            }
            _depth = index;
        }
    }
}