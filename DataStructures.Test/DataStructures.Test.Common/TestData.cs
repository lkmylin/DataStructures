using System;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using System.Collections.Generic;
using DataStructures.Models;

namespace DataStructures.Test.Common
{
    public static class TestDataStore
    {
        private static readonly string _testDataPath = $"..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}..{Path.DirectorySeparatorChar}DataStructures.Test.Common{Path.DirectorySeparatorChar}test_data.json";
        private static TestData _testData;

        public static TestData TestData => _testData ?? (_testData = GetTestData());

        private static TestData GetTestData()
        {
            var json = File.ReadAllText(_testDataPath);
            var testData = JsonConvert.DeserializeObject<TestData>(json);
            foreach (var test in testData.BinaryTree)
            {
                test.Data = Encoding.UTF8.GetString(Convert.FromBase64String(test.Data));
            }
            return testData;
        }
    }

    public class TestData
    {
        public List<BinaryTreeTest> BinaryTree { get; set; }
    }

    public class BinaryTreeTest
    {
        public List<int> Nodes { get; set; }
        public BinaryTreeType Type { get; set; }
        public string Data { get; set; }
    }
}