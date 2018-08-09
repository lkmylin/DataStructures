using System;
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
        private readonly INodeProvider<int> _nodeProvider;
        private IBinaryTree<int> _tree;
        private string _thenStringificationResult;

        public DataStructuresServicesTest()
        {
            _stringificationService = new StringificationService<int>();
            _nodeProvider = new NodeProvider<int>();
            _structureBuilder = new StructureBuilder<int>(_nodeProvider);
        }

        [Test, TestCaseSource("GetBinaryTreeTestCases")]
        public void StructureBuilderShouldBuildTreeAndStringificationServiceShouldPrintIt(BinaryTreeTest testCase)
        {
            GivenTree(testCase.Nodes, testCase.Type);
            WhenStringify();
            ThenTreePrinted(testCase.Data.Replace("\r\n", Environment.NewLine));
        }

        private void GivenTree(List<int> nodes, BinaryTreeType type)
        {
            _tree = _structureBuilder.BuildTree(nodes, type);
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