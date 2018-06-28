using System;
using System.Linq;
using System.Collections.ObjectModel;

namespace DataStructures.Models
{
    public interface IBinaryTreeNode<T> : INode<T>
    {
        IBinaryTreeNode<T> Left { get; set; }
        IBinaryTreeNode<T> Right { get; set; }
        IBinaryTreeNode<T> Parent { get; set; }
        int Level { get; }
        int Offset { get; }
        BinaryTreeNodeType Type { get; set; }
    }

    public class BinaryTreeNode<T> : Node<T>, IBinaryTreeNode<T>
    {
        private int? _level;
        private int? _offset;
        public IBinaryTreeNode<T> Parent { get; set; }
        public IBinaryTreeNode<T> Left { get => Children?.Get(0) as IBinaryTreeNode<T>; set => SetChildren(value, null); }
        public IBinaryTreeNode<T> Right { get => Children?.Get(1) as IBinaryTreeNode<T>; set => SetChildren(null, value); }
        public int Level => (int)(_level ?? (_level = GetLevel()));
        public int Offset => (int)(_offset ?? (_offset = GetOffset()));
        public BinaryTreeNodeType Type { get; set; }

        public BinaryTreeNode() { }
        public BinaryTreeNode(T value) : base(value) { }
        public BinaryTreeNode(T value, IBinaryTreeNode<T> left, IBinaryTreeNode<T> right)
        {
            Value = value;
            SetChildren(left, right);
        }

        private void SetChildren(IBinaryTreeNode<T> left, IBinaryTreeNode<T> right)
        {
            if (Children == null) Children = new Children<T>(2);
            if (left != null) Children.Set(0, left);
            if (right != null) Children.Set(1, right);
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

    public interface INode<T>
    {
        T Value { get; set; }
        IChildren<T> Children { get; set; }
    }

    public class Node<T> : INode<T>
    {
        public T Value { get; set; }
        public IChildren<T> Children { get; set; }

        public Node() { }
        public Node(T value) : this(value, null) { }
        public Node(T value, IChildren<T> children)
        {
            Value = value;
            Children = children;
        }
    }

    public interface IChildren<T>
    {
        INode<T> Get(int index);
        void Set(int index, INode<T> node);
        INode<T> Find(Func<INode<T>, bool> predicate);
    }

    public class Children<T> : Collection<INode<T>>, IChildren<T>
    {
        public Children() : base() { }
        public Children(int size)
        {
            for (var i = 0; i < size; i++)
            {
                Items.Add(default(INode<T>));
            }
        }

        public INode<T> Get(int index)
        {
            return Items[index];
        }

        public void Set(int index, INode<T> node)
        {
            Items[index] = node;
        }

        public INode<T> Find(Func<INode<T>, bool> predicate)
        {
            return Items.FirstOrDefault(predicate);
        }
    }
}