using System;
using System.Linq;
using System.Text;
using DataStructures.Models;
using System.Collections.Generic;

namespace DataStructures.Services
{
    public interface IStringificationService<T> where T : IComparable
    {
        string Stringify(IBinaryTree<T> tree);
    }

    public class StringificationService<T> : IStringificationService<T> where T : IComparable
    {
        private const int _indentation = 2;
        private const int _branchLength = 1;

        public string Stringify(IBinaryTree<T> tree)
        {
            if (tree?.Root == null || tree.Root.Value == null) return string.Empty;
            var result = new StringBuilder();
            var indentationAdjustment = 0;
            var allNodes = new List<IBinaryTreeNode<T>>();
            GetAllNodes(tree.Root, ref allNodes);
            result.Append(Environment.NewLine);
            if (allNodes.Count == 1)
            {
                result.Append(Spaces(_indentation) + tree.Root.Value.ToString());
                result.Append(Environment.NewLine);
                return result.ToString();
            }
            for (var i = 0; i < tree.Depth; i++)
            {
                var nodes = allNodes.Where(x => x.Level == i).ToList();
                var nodeCount = Math.Pow(2, i);
                // Length of the connection lines has to decrease exponentially as we move down the tree                
                var branchLength = _branchLength * (int)Math.Pow(2, tree.Depth - i);
                var parentBranchLength = i == 0 ? 0 : _branchLength * (int)Math.Pow(2, tree.Depth - i + 1);
                var parentBaseLength = i == 0 ? 0 : 2 * parentBranchLength - 1;
                indentationAdjustment += parentBaseLength / 2;
                var indentation = _indentation * (int)Math.Pow(2, tree.Depth) - indentationAdjustment;
                // Correction for cumulative error of 1 space per loop after first loop due to
                // half space in parentBaseLength calculation
                indentation -= i;
                for (var j = 0; j < nodeCount; j++)
                {
                    var currentNode = nodes.FirstOrDefault(x => x.Offset == j);
                    // Must compensate for extra space(s) if value occupies more that 1 space
                    var valueLength = currentNode == null ? 1 : currentNode.Value.ToString().Length;
                    if (j == 0)
                    {
                        result.Append(Spaces(indentation));
                    }
                    result.Append(currentNode == null ? Spaces(1) : currentNode.Value.ToString());
                    if (j < nodeCount - 1)
                    {
                        result.Append(Spaces(parentBaseLength - (valueLength > 1 ? valueLength - 1 : 0)));
                    }
                }
                result.Append(Environment.NewLine);
                if (i == tree.Depth - 1) return result.ToString();
                for (var k = 1; k < branchLength; k++)
                {
                    var baseLength = 2 * k - 1;
                    for (var l = 0; l < nodeCount; l++)
                    {
                        var currentNode = nodes.FirstOrDefault(x => x.Offset == l);
                        if (l == 0)
                        {
                            result.Append(Spaces(indentation - k));
                        }
                        result.Append(currentNode?.Left == null ? Spaces(1) : "/");
                        result.Append(Spaces(baseLength));
                        result.Append(currentNode?.Right == null ? Spaces(1) : "\\");
                        if (l < nodeCount - 1)
                        {
                            // Subtract 1 extra space for the error in the base calculation
                            result.Append(Spaces(parentBaseLength - baseLength - 1));
                        }
                    }
                    result.Append(Environment.NewLine);
                }
            }
            return result.ToString();
        }        

        private void GetAllNodes(IBinaryTreeNode<T> node, ref List<IBinaryTreeNode<T>> nodes)
        {
            if (node == null) return;
            nodes.Add(node);
            GetAllNodes(node.Left, ref nodes);
            GetAllNodes(node.Right, ref nodes);
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
    }
}