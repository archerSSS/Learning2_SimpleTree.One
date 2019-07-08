using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using AlgorithmsDataStructures2;

namespace AlgoTest_1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestAdd_A_1()
        {
            SimpleTreeNode<Int32> root = new SimpleTreeNode<int>(100, null);
            SimpleTree<Int32> tree = new SimpleTree<Int32>(root);
            tree.AddChild(root, new SimpleTreeNode<int>(101, root));
            tree.AddChild(root, new SimpleTreeNode<int>(102, root));

            Assert.AreEqual(100, tree.Root.NodeValue);
            Assert.AreEqual(2, tree.Root.Children.Count);

            int value = 100;
            foreach (SimpleTreeNode<int> node in tree.Root.Children)
            {
                value++;
                Assert.AreEqual(value, node.NodeValue);
            }
        }

        [TestMethod]
        public void TestAdd_A_2()
        {
            SimpleTreeNode<Int32> root = new SimpleTreeNode<int>(1, null);
            SimpleTreeNode<Int32> node1 = new SimpleTreeNode<int>(11, root);
            SimpleTreeNode<Int32> node2 = new SimpleTreeNode<int>(12, root);
            SimpleTreeNode<Int32> node11 = new SimpleTreeNode<int>(111, node1);
            SimpleTreeNode<Int32> node21 = new SimpleTreeNode<int>(121, node2);
            SimpleTreeNode<Int32> node22 = new SimpleTreeNode<int>(122, node2);
            SimpleTreeNode<Int32> node23 = new SimpleTreeNode<int>(123, node2);

            SimpleTree<Int32> tree = new SimpleTree<Int32>(root);
            tree.AddChild(root, node1);
            tree.AddChild(root, node2);

            Assert.AreEqual(1, tree.Root.NodeValue);
            
            int value = 10;
            foreach (SimpleTreeNode<int> node in tree.Root.Children)
            {
                value++;
                Assert.AreEqual(value, node.NodeValue);
            }

            tree.AddChild(node1, node11);

            value = 100;
            foreach (SimpleTreeNode<int> node in tree.Root.Children)
            {
                value += 10;
                if (node.Children != null)
                {
                    foreach (SimpleTreeNode<int> childNode in node.Children)
                    {
                        value++;
                        Assert.AreEqual(value, childNode.NodeValue);
                    }
                    value -= node.Children.Count;
                }
                
            }

            tree.AddChild(node2, node21);
            tree.AddChild(node2, node22);
            tree.AddChild(node2, node23);

            value = 100;
            foreach (SimpleTreeNode<int> node in tree.Root.Children)
            {
                if (root.Children != null)
                {
                    value += 10;
                    foreach (SimpleTreeNode<int> childNode in node.Children)
                    {
                        value++;
                        Assert.AreEqual(value, childNode.NodeValue);
                    }
                    value -= node.Children.Count;
                }
            }
        }
        
        [TestMethod]
        public void TestDelete_A_1()
        {
            SimpleTreeNode<Int32> root = new SimpleTreeNode<Int32>(100, null);
            SimpleTreeNode<Int32> node1 = new SimpleTreeNode<Int32>(101, root);
            SimpleTreeNode<Int32> node2 = new SimpleTreeNode<Int32>(102, root);

            SimpleTree<Int32> tree = new SimpleTree<Int32>(root);
            tree.AddChild(root, node1);
            tree.AddChild(root, node2);

            tree.DeleteNode(node1);
            Assert.AreEqual(root, tree.Root);
            foreach (SimpleTreeNode<Int32> node in tree.Root.Children)
                Assert.AreEqual(node2, node);
            Assert.AreEqual(1, tree.Root.Children.Count);
            
            tree.DeleteNode(root);
            Assert.AreEqual(null, tree.Root);
        }
        
        [TestMethod]
        public void TestDelete_A_2()
        {
            SimpleTreeNode<int>[] nodesArray = GetNodesArray();
            SimpleTree<int> tree = GetTree(nodesArray);

            tree.DeleteNode(nodesArray[5]);
            tree.DeleteNode(nodesArray[11]);
            
            int indexA = 0;
            int indexB = 3;
            int count = 1;
            
            if (tree.Root.Children != null)
                foreach (SimpleTreeNode<int> node in tree.Root.Children)
                {
                    count += node.Children.Count + 1;
                    indexA++;
                    Assert.AreEqual(nodesArray[indexA], node);
                    if (node.Children != null)
                        foreach (SimpleTreeNode<int> deepnode in node.Children)
                        {
                            indexB++;
                            if (indexB == 5 || indexB == 11) indexB++;
                            Assert.AreEqual(nodesArray[indexB], deepnode);
                        }
                }
            Assert.AreEqual(12, count);
        }

        [TestMethod]
        public void TestDelete_A_3()
        {
            SimpleTreeNode<int>[] nodesArray = GetNodesArray();
            SimpleTree<int> tree = GetTree(nodesArray);

            tree.DeleteNode(nodesArray[1]);
            tree.DeleteNode(nodesArray[5]);
            tree.DeleteNode(nodesArray[6]);
            tree.DeleteNode(nodesArray[8]);
            tree.DeleteNode(nodesArray[9]);
            tree.DeleteNode(nodesArray[10]);
            tree.DeleteNode(nodesArray[11]);
            tree.DeleteNode(nodesArray[12]);
            
            int indexA = 0;
            int indexB = 4;
            int count = 1;

            if (tree.Root.Children != null)
                foreach (SimpleTreeNode<int> node in tree.Root.Children)
                {
                    count++;
                    indexA++;

                    // Пропуск одной ячейки массива. Узел в данной ячейке несуществует в дереве - был удален.
                    if (indexA == 1) indexA++;

                    Assert.AreEqual(nodesArray[indexA], node);
                    if (node.Children != null)
                        foreach (SimpleTreeNode<int> deepnode in node.Children)
                        {
                            indexB++;

                            // Пропуск тех ячеек массива, узлы которых в дереве были удалены
                            if (indexB > 4 && indexB < 7) indexB = 7;
                            else if (indexB > 7 && indexB < 13) indexB = 13;

                            Assert.AreEqual(nodesArray[indexB], deepnode);
                        }
                }
        }



        // Возвращает массив узлов для класса SimpleTree<int>
        //
        private SimpleTreeNode<int>[] GetNodesArray()
        {
            SimpleTreeNode<int>[] nodes = new SimpleTreeNode<int>[]
            {
                new SimpleTreeNode<int>(1, null),
                new SimpleTreeNode<int>(11, null),
                new SimpleTreeNode<int>(12, null),
                new SimpleTreeNode<int>(13, null),
                new SimpleTreeNode<int>(111, null),
                new SimpleTreeNode<int>(112, null),
                new SimpleTreeNode<int>(121, null),
                new SimpleTreeNode<int>(122, null),
                new SimpleTreeNode<int>(123, null),
                new SimpleTreeNode<int>(131, null),
                new SimpleTreeNode<int>(132, null),
                new SimpleTreeNode<int>(133, null),
                new SimpleTreeNode<int>(134, null),
                new SimpleTreeNode<int>(135, null)
            };
            return nodes;
        }

        // Возвращает дерево на основе массива узлов.
        //
        // Структура дерева:
        //      0:1-2-3, 1:1-2, 2:1-2-3, 3:1-2-3-4-5.
        //
        private SimpleTree<int> GetTree(SimpleTreeNode<int>[] nodes)
        {
            SimpleTree<int> tree = new SimpleTree<int>(nodes[0]);
            tree.AddChild(tree.Root, nodes[1]);
            tree.AddChild(tree.Root, nodes[2]);
            tree.AddChild(tree.Root, nodes[3]);

            tree.AddChild(nodes[1], nodes[4]);
            tree.AddChild(nodes[1], nodes[5]);
            tree.AddChild(nodes[2], nodes[6]);
            tree.AddChild(nodes[2], nodes[7]);
            tree.AddChild(nodes[2], nodes[8]);
            tree.AddChild(nodes[3], nodes[9]);
            tree.AddChild(nodes[3], nodes[10]);
            tree.AddChild(nodes[3], nodes[11]);
            tree.AddChild(nodes[3], nodes[12]);
            tree.AddChild(nodes[3], nodes[13]);
            return tree;
        }
    }
}
