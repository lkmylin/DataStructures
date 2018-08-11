using DataStructures.Models;

namespace DataStructures.Services
{
    public interface INodeProvider<T>
    {
        IBinaryTreeNode<T> GetBinaryTreeNode(T value);
    }

    public class NodeProvider<T> : INodeProvider<T>
    {
        public IBinaryTreeNode<T> GetBinaryTreeNode(T value)
        {
            return new BinaryTreeNode<T>(value);
        }
    }
}