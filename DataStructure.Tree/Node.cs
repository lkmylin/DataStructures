using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace DataStructure.Tree
{
    public class BinaryTreeNode<T> : Node<T>
    {
        private int? _level;
        private int? _offset;
        public BinaryTreeNode<T> Parent { get; set; }
        public BinaryTreeNode<T> Left { get => Children?[0] as BinaryTreeNode<T>; set => SetChildren(value, null); }
        public BinaryTreeNode<T> Right { get => Children?[1] as BinaryTreeNode<T>; set => SetChildren(null, value); }
        public int Level => (int)(_level ?? (_level = GetLevel()));
        public int Offset => (int)(_offset ?? (_offset = GetOffset()));
        public BinaryTreeNodeType Type { get; set; }

        public BinaryTreeNode() { }
        public BinaryTreeNode(T value) : base(value) { }
        public BinaryTreeNode(T value, BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            Value = value;
            SetChildren(left, right);
        }

        private void SetChildren(BinaryTreeNode<T> left, BinaryTreeNode<T> right)
        {
            if (Children == null) Children = new Children<T>(2);
            if (left != null) Children[0] = left;
            if (right != null) Children[1] = right;
        }

        private int GetLevel()
        {
            if (Parent == null) return 0;
            return Parent.Level + 1;
        }

        private int GetOffset()
        {
            if (Parent == null) return 0;
            return Parent.Offset * 2 + (Type == BinaryTreeNodeType.Right ? 1 : 0);
        }
    }

    public enum BinaryTreeNodeType
    {
        Left,
        Right,
        Root
    }

    public class Node<T>
    {
        public T Value { get; set; }
        protected Children<T> Children { get; set; }

        public Node() { }
        public Node(T value) : this(value, null) { }
        public Node(T value, Children<T> children)
        {
            Value = value;
            Children = children;
        }
    }

    public class Children<T> : Collection<Node<T>>
    {
        public Children() : base() { }
        public Children(int size)
        {
            for (var i = 0; i < size; i++)
            {
                Items.Add(default(Node<T>));
            }
        }

        public Node<T> Find(Func<Node<T>, bool> predicate)
        {
            return Items.FirstOrDefault(predicate);
        }
    }
}