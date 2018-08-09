using DataStructures.Models;

namespace DataStructures.Services
{
    public interface INodeProvider<T>
    {
        BinaryTreeNode<T> GetBinaryTreeNode(T value);
    }

    public class NodeProvider<T> : INodeProvider<T>
    {
        public BinaryTreeNode<T> GetBinaryTreeNode(T value)
        {
            return new BinaryTreeNode<T>(value);
        }
    }
}