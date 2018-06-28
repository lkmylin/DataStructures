namespace DataStructures.Models
{
    public interface IBinaryTree<T>
    {
        IBinaryTreeNode<T> Root { get; }
        int NodeCount { get; }
        int Depth { get; }
    }

    public class BinaryTree<T> : IBinaryTree<T>
    {
        public IBinaryTreeNode<T> Root { get; }
        public int NodeCount { get; }
        public int Depth { get; }

        public BinaryTree(IBinaryTreeNode<T> root, int nodeCount, int depth)
        {
            Root = root;
            NodeCount = nodeCount;
            Depth = depth;
        }
    }
}