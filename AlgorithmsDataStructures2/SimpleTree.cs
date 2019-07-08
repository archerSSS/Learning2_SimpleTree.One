using System;
using System.Collections.Generic;

namespace AlgorithmsDataStructures2
{
    public class SimpleTreeNode<T>
    {
        public T NodeValue; // значение в узле
        public SimpleTreeNode<T> Parent; // родитель или null для корня
        public List<SimpleTreeNode<T>> Children; // список дочерних узлов или null

        public SimpleTreeNode(T val, SimpleTreeNode<T> parent)
        {
            NodeValue = val;
            Parent = parent;
            Children = null;
        }
    }

    public class SimpleTree<T>
    {
        public SimpleTreeNode<T> Root; // корень, может быть null

        public SimpleTree(SimpleTreeNode<T> root)
        {
            Root = root;
        }

        public void AddChild(SimpleTreeNode<T> ParentNode, SimpleTreeNode<T> NewChild)
        {
            if (Root == null) Root = NewChild;
            else
            {
                if (ParentNode.Children == null) ParentNode.Children = new List<SimpleTreeNode<T>>();
                ParentNode.Children.Add(NewChild);
                NewChild.Parent = ParentNode;
            }
            // ваш код добавления нового дочернего узла существующему ParentNode
        }

        public void DeleteNode(SimpleTreeNode<T> NodeToDelete)
        {
            if (NodeToDelete.Parent == null) Root = null;
            else NodeToDelete.Parent.Children.Remove(NodeToDelete);
            // ваш код удаления существующего узла NodeToDelete
        }

        public List<SimpleTreeNode<T>> GetAllNodes()
        {
            if (Root != null)
            {
                List<SimpleTreeNode<T>> list = new List<SimpleTreeNode<T>>();
                list = CollectAllNodes(list, Root);
                return list;
            }

            // ваш код выдачи всех узлов дерева в определённом порядке
            return null;
        }

        public List<SimpleTreeNode<T>> FindNodesByValue(T val)
        {
            if (Root != null)
            {
                List<SimpleTreeNode<T>> list = new List<SimpleTreeNode<T>>();
                list = CollectNodesByValue(list, Root, val);
                return list;
            }

            // ваш код поиска узлов по значению
            return null;
        }

        public void MoveNode(SimpleTreeNode<T> OriginalNode, SimpleTreeNode<T> NewParent)
        {
            DeleteNode(OriginalNode);
            AddChild(NewParent, OriginalNode);
            //OriginalNode.Parent.Children.Remove(OriginalNode);
            //OriginalNode.Parent = NewParent;
            //NewParent.Children.Add(OriginalNode);

            // ваш код перемещения узла вместе с его поддеревом -- 
            // в качестве дочернего для узла NewParent
        }

        public int Count()
        {
            if (Root != null)
                return CountNodes(Root.Children) + 1;

            // количество всех узлов в дереве
            return 0;
        }

        public int LeafCount()
        {
            if (Root != null)
                return CountLeaf(Root.Children);
            // количество листьев в дереве
            return 0;
        }

        private List<SimpleTreeNode<T>> CollectAllNodes(List<SimpleTreeNode<T>> list, SimpleTreeNode<T> node)
        {
            list.Add(node);
            if (node.Children != null)
                foreach (SimpleTreeNode<T> child in node.Children)
                    list = CollectAllNodes(list, child);
            
            if (list.Count > 0) return list; 
            return null;
        }
        
        private List<SimpleTreeNode<T>> CollectNodesByValue(List<SimpleTreeNode<T>> list, SimpleTreeNode<T> node, T val)
        {
            if (node.NodeValue != null && node.NodeValue.Equals(val)) list.Add(node);
            if (node.Children != null)
                foreach (SimpleTreeNode<T> child in node.Children)
                    list = CollectNodesByValue(list, child, val);
            
            return list;
        }
        
        private int CountNodes(List<SimpleTreeNode<T>> list)
        {
            if (list != null)
            {
                int count = list.Count;
                foreach (SimpleTreeNode<T> node in list)
                    if (node.Children != null) count += CountNodes(node.Children);

                return count;
            }
            return 0;
        }
        
        private int CountLeaf(List<SimpleTreeNode<T>> list)
        {
            if (list != null)
            {
                int count = 1;
                foreach (SimpleTreeNode<T> child in list)
                    count += CountLeaf(child.Children);
                return count;
            }
            return 0;
        }
    }

}