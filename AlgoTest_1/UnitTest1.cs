using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsDataStructures2;

namespace AlgoTest_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd_1()
        {
            SimpleTreeNode<Int32> root = new SimpleTreeNode<int>(100, null);
            SimpleTree<Int32> tree = new SimpleTree<Int32>(root);
            tree.AddChild(root, new SimpleTreeNode<int>(101, root));
            tree.AddChild(root, new SimpleTreeNode<int>(102, root));

            Assert.AreEqual(100, tree.Root.NodeValue);

            int count = 100;
            foreach (SimpleTreeNode<int> node in tree.Root.Children)
            {
                count++;
                Assert.AreEqual(count, node.NodeValue);
            }
                
        }
    }
}
