using System;
using System.Linq;
using System.Text;
using DataStructures.Models;
using System.Collections.Generic;

namespace DataStructures.Services
{
    public interface IStringificationService<T>
    {
        string Stringify(IBinaryTree<T> tree);
    }

    public class StringificationService<T> : IStringificationService<T>
    {
        private const int _indentation = 2;
        private const int _branchLength = 1;

        public string Stringify(IBinaryTree<T> tree)
        {            
            var result = new StringBuilder();
            var indentationAdjustment = 0;
            var allNnodes = new List<IBinaryTreeNode<T>>();
            GetAllNodes(tree.Root, ref allNnodes);
            result.Append(Environment.NewLine);            
            for (var i = 0; i < tree.Depth; i++)
            {
                var nodes = allNnodes.Where(x => x.Level == i).ToList();
                // Length of the connection lines has to decrease exponentially as we move down the tree                
                var branchLength = _branchLength * (int)Math.Pow(2, tree.Depth - i);
                var parentBranchLength = i == 0 ? 0 : _branchLength * (int)Math.Pow(2, tree.Depth - i + 1);
                var parentBaseLength = i == 0 ? 0 : 2 * parentBranchLength - 1;
                indentationAdjustment += parentBaseLength / 2;
                var indentation = _indentation * (int)Math.Pow(2, tree.Depth) - indentationAdjustment;
                // Correction for cumulative error of 1 space per loop after first loop due to
                // half space in parentBaseLength calculation
                indentation -= i;
                for (var j = 0; j < nodes.Count; j++)
                {
                    // Must compensate for extra space(s) if value occupies more that 1 space
                    var valueLength = nodes[j].Value.ToString().Length;
                    if (j == 0)
                    {
                        result.Append(Spaces(indentation));
                    }
                    result.Append(nodes[j].Value);
                    if (j < nodes.Count - 1)
                    {
                        result.Append(Spaces(parentBaseLength - (valueLength > 1 ? valueLength - 1 : 0)));
                    }
                }
                result.Append(Environment.NewLine);
                if (i == tree.Depth - 1) return result.ToString();
                for (var k = 1; k < branchLength; k++)
                {
                    var baseLength = 2 * k - 1;
                    for (var l = 0; l < nodes.Count; l++)
                    {
                        if (l == 0)
                        {
                            result.Append(Spaces(indentation - k));
                        }
                        result.Append(nodes[l].Left == null ? " " : "/");
                        result.Append(Spaces(baseLength));
                        result.Append(nodes[l].Right == null ? " " : "\\");
                        if (l < nodes.Count - 1)
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