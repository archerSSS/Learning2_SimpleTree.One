using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
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
            SimpleTreeNode<int>[] nodesArray = GetNodesArray_1();
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
            SimpleTreeNode<int>[] nodesArray = GetNodesArray_1();
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

        [TestMethod]
        public void TestGetAllNodes_1()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);
            nodes = ChangeArrayOrder(nodes);
            
            List<SimpleTreeNode<int>> list = tree.GetAllNodes();
            int index = 0;
            foreach (SimpleTreeNode<int> node in list)
            {
                Assert.AreEqual(nodes[index], node);
                index++;
            }   
        }
        
        [TestMethod]
        public void TestGetAllNodes_2()
        {
            SimpleTreeNode<int> root = new SimpleTreeNode<int>(1, null);
            SimpleTree<int> tree = new SimpleTree<int>(root);
            
            List<SimpleTreeNode<int>> list = tree.GetAllNodes();
            
            foreach (SimpleTreeNode<int> node in list)
                Assert.AreEqual(root, node);
        }

        [TestMethod]
        public void TestGetAllNodes_3()
        {
            SimpleTreeNode<int> root = new SimpleTreeNode<int>(1, null);
            SimpleTreeNode<int> node1 = new SimpleTreeNode<int>(11, null);
            SimpleTreeNode<int> node2 = new SimpleTreeNode<int>(12, null);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(13, null);
            SimpleTreeNode<int>[] nodes = new SimpleTreeNode<int>[]
            {
                root, node1, node2, node3
            };

            SimpleTree<int> tree = new SimpleTree<int>(root);
            tree.AddChild(root, node1);
            tree.AddChild(root, node2);
            tree.AddChild(root, node3);

            List<SimpleTreeNode<int>> list = tree.GetAllNodes();

            int index = 0;
            foreach (SimpleTreeNode<int> node in list)
            {
                Assert.AreEqual(nodes[index], node);
                index++;
            }
                
        }

        [TestMethod]
        public void TestFindNodesByValue_1()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_2();
            SimpleTree<int> tree = new SimpleTree<int>(nodes[0]);
            tree.AddChild(nodes[0], nodes[1]);
            tree.AddChild(nodes[0], nodes[2]);
            tree.AddChild(nodes[0], nodes[3]);

            List<SimpleTreeNode<int>> list = tree.FindNodesByValue(11);

            foreach (SimpleTreeNode<int> node in list)
                Assert.AreEqual(11, node.NodeValue);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestFindNodesByValue_2()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_2();
            SimpleTree<int> tree = new SimpleTree<int>(nodes[0]);
            tree.AddChild(nodes[0], nodes[1]);
            tree.AddChild(nodes[1], nodes[2]);
            tree.AddChild(nodes[2], nodes[3]);

            List<SimpleTreeNode<int>> list = tree.FindNodesByValue(11);
            
            foreach (SimpleTreeNode<int> node in list)
                Assert.AreEqual(11, node.NodeValue);
            Assert.AreEqual(2, list.Count);
        }

        [TestMethod]
        public void TestMoveNode_1()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);
            Assert.AreEqual(nodes[3], tree.FindNodesByValue(135).Find(
                delegate(SimpleTreeNode<int> node) { return node.Equals(nodes[13]); }).Parent);
            
            tree.MoveNode(nodes[13], nodes[1]);
            Assert.AreEqual(nodes[1], tree.FindNodesByValue(135).Find(
                delegate (SimpleTreeNode<int> node) { return node.Equals(nodes[13]); }).Parent);
        }

        [TestMethod]
        public void TestMoveNode_2()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);

            tree.MoveNode(nodes[6], nodes[3]);
            tree.MoveNode(nodes[7], nodes[3]);
            tree.MoveNode(nodes[8], nodes[3]);
            Assert.AreEqual(nodes[3], tree.FindNodesByValue(121).Find(
                delegate (SimpleTreeNode<int> node) { return node.Equals(nodes[6]); }).Parent);
            Assert.AreEqual(nodes[3], tree.FindNodesByValue(122).Find(
                delegate (SimpleTreeNode<int> node) { return node.Equals(nodes[7]); }).Parent);
            Assert.AreEqual(nodes[3], tree.FindNodesByValue(123).Find(
                delegate (SimpleTreeNode<int> node) { return node.Equals(nodes[8]); }).Parent);
            Assert.AreEqual(8, tree.FindNodesByValue(13).Find(
                delegate (SimpleTreeNode<int> node) { return node.Equals(nodes[3]); }).Children.Count);
        }

        [TestMethod]
        public void TestCount_1()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);

            Assert.AreEqual(14, tree.Count());
        }

        [TestMethod]
        public void TestCountLeaf_1()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);

            Assert.AreEqual(10, tree.LeafCount());
        }
        
        [TestMethod]
        public void TestCountLeaf_2()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);
            tree.AddChild(nodes[4], new SimpleTreeNode<int>(1111, null));
            tree.AddChild(nodes[5], new SimpleTreeNode<int>(1121, null));
            tree.AddChild(nodes[6], new SimpleTreeNode<int>(1211, null));
            tree.AddChild(nodes[7], new SimpleTreeNode<int>(1221, null));
            tree.AddChild(nodes[8], new SimpleTreeNode<int>(1231, null));
            tree.AddChild(nodes[9], new SimpleTreeNode<int>(1311, null));
            tree.AddChild(nodes[10], new SimpleTreeNode<int>(1321, null));
            tree.AddChild(nodes[11], new SimpleTreeNode<int>(1331, null));
            tree.AddChild(nodes[12], new SimpleTreeNode<int>(1341, null));
            tree.AddChild(nodes[13], new SimpleTreeNode<int>(1351, null));

            Assert.AreEqual(10, tree.LeafCount());
        }

        [TestMethod]
        public void TestCountLeaf_3()
        {
            SimpleTreeNode<int> root = new SimpleTreeNode<int>(0, null);
            SimpleTree<int> tree = new SimpleTree<int>(root);

            Assert.AreEqual(1, tree.LeafCount());
        }

        [TestMethod]
        public void TestCountLeaf_4()
        {
            SimpleTreeNode<int> root = new SimpleTreeNode<int>(0, null);
            SimpleTreeNode<int> node1 = new SimpleTreeNode<int>(0, null);
            SimpleTreeNode<int> node2 = new SimpleTreeNode<int>(0, null);
            SimpleTreeNode<int> node3 = new SimpleTreeNode<int>(0, null);
            SimpleTree<int> tree = new SimpleTree<int>(root);
            tree.AddChild(root, node1);
            tree.AddChild(root, node2);
            tree.AddChild(root, node3);

            Assert.AreEqual(3, tree.LeafCount());
        }

        [TestMethod]
        public void TestSetLevel()
        {
            SimpleTreeNode<int>[] nodes = GetNodesArray_1();
            SimpleTree<int> tree = GetTree(nodes);

            tree.SetLevel();
            List<SimpleTreeNode<int>> list = tree.GetAllNodes();

            int count = 1;
            foreach (SimpleTreeNode<int> node in list)
            {
                if (count == 1) Assert.AreEqual(1, node.level);
                if (count == 2 || count == 5 || count == 9) Assert.AreEqual(2, node.level);
                if (count  > 2 && count < 5 ||
                        count > 5 && count < 9 || count > 9)
                            Assert.AreEqual(3, node.level);
                count++;
            }
        }

        // Возвращает массив узлов для класса SimpleTree<int>
        //
        private SimpleTreeNode<int>[] GetNodesArray_1()
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


        // Возвращает массив узлов для класса SimpleTree<int>
        //
        private SimpleTreeNode<int>[] GetNodesArray_2()
        {
            return new SimpleTreeNode<int>[]
            {
                new SimpleTreeNode<int>(10, null),
                new SimpleTreeNode<int>(10, null),
                new SimpleTreeNode<int>(11, null),
                new SimpleTreeNode<int>(11, null)
            };
        }

        // Меняет опрядок узлов массива для метода TestGetAllNodes_1()
        //
        private SimpleTreeNode<int>[] ChangeArrayOrder(SimpleTreeNode<int>[] nodes)
        {
            SimpleTreeNode<int>[] new_nodes = new SimpleTreeNode<int>[14];
            new_nodes[0] = nodes[0];
            new_nodes[1] = nodes[1];
            new_nodes[2] = nodes[4];
            new_nodes[3] = nodes[5];
            new_nodes[4] = nodes[2];
            new_nodes[5] = nodes[6];
            new_nodes[6] = nodes[7];
            new_nodes[7] = nodes[8];
            new_nodes[8] = nodes[3];
            new_nodes[9] = nodes[9];
            new_nodes[10] = nodes[10];
            new_nodes[11] = nodes[11];
            new_nodes[12] = nodes[12];
            new_nodes[13] = nodes[13];

            return new_nodes;
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
