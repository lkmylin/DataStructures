using NUnit.Framework;
using System.Collections.Generic;
using DataStructures.Models;
using DataStructures.Services;
using DataStructures.Test.Common;

namespace DataStructures.Test.Integration
{
    [TestFixture]
    public class DataStructuresServicesTest
    {
        private readonly IStringificationService<int> _stringificationService;
        private readonly IStructureBuilder<int> _structureBuilder;
        private IBinaryTree<int> _tree;
        private string _thenStringificationResult;

        public DataStructuresServicesTest()
        {
            _stringificationService = new StringificationService<int>();
            _structureBuilder = new StructureBuilder<int>();
        }

        [Test, TestCaseSource("GetBinaryTreeTestCases")]
        public void StructureBuilderShouldBuildTreeAndStringificationServiceShouldPrintIt(BinaryTreeTest testCase)
        {
            GivenTree(testCase.NodeCount);
            WhenStringify();
            ThenTreePrinted(testCase.Data);
        }

        private void GivenTree(int nodeCount)
        {
            var nodes = new List<IBinaryTreeNode<int>>();
            for (var i = 1; i <= nodeCount; i++)
            {
                nodes.Add(new BinaryTreeNode<int>(i));
            }
            _tree = _structureBuilder.BuildTree(nodes);
        }

        private void WhenStringify()
        {
            _thenStringificationResult = _stringificationService.Stringify(_tree);
        }

        private void ThenTreePrinted(string expected)
        {
            Assert.AreEqual(expected, _thenStringificationResult);
        }

        private static List<BinaryTreeTest> GetBinaryTreeTestCases()
        {
            return TestDataStore.TestData.BinaryTree;
        }
    }
}