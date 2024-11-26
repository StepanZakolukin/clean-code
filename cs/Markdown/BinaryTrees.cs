using System.Collections;

namespace Markdown;

public class BinaryTree<T> : IEnumerable<T>
    where T : IComparable
{
    private TreeNode Root;
    public void Add(T key)
    {
        if (Equals(Root, null)) { Root = new TreeNode(key, null); return; }

        var currentSubtree = Root;

        while (true)
        {
            if (key.CompareTo(currentSubtree.Value) >= 0)
            {
                currentSubtree.HeightOfTheRight++;
                if (currentSubtree.Right == null) { currentSubtree.Right = new TreeNode(key, currentSubtree); return; }
                else currentSubtree = currentSubtree.Right;
            }
            else
            {
                currentSubtree.HeightOfTheLeft++;
                if (currentSubtree.Left == null) { currentSubtree.Left = new TreeNode(key, currentSubtree); return; }
                else currentSubtree = currentSubtree.Left;
            }
        }
    }

    public bool Contains(T key)
    {
        var currentSubtree = Root;

        while (!Equals(currentSubtree, null))
        {
            if (key.CompareTo(currentSubtree.Value) == 0)
                return true;

            if (key.CompareTo(currentSubtree.Value) > 0)
                currentSubtree = currentSubtree.Right;
            else currentSubtree = currentSubtree.Left;
        }

        return false;
    }

    public T this[int i]
    {
        get
        {
            if (Root.HeightOfTheRight + Root.HeightOfTheLeft < i || i < 0)
                throw new IndexOutOfRangeException();

            var currentSubtree = Root;
            var index = 0;

            while (true)
            {
                if (currentSubtree.HeightOfTheLeft + index == i) return currentSubtree.Value;
                else if (currentSubtree.HeightOfTheLeft + index > i)
                    currentSubtree = currentSubtree.Left;
                else if (currentSubtree.HeightOfTheLeft < i)
                {
                    index += currentSubtree.HeightOfTheLeft + 1;
                    currentSubtree = currentSubtree.Right;
                }
            }
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        if (Root == null) yield break;

        foreach (var subtree in Root)
            yield return subtree.Value;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public class TreeNode : IEnumerable<TreeNode>
    {
        public T Value;
        public int HeightOfTheLeft { get; set; }
        public int HeightOfTheRight { get; set; }

        public TreeNode Left, Right, Ancestor;

        public TreeNode(T value, TreeNode ancestor)
        {
            Value = value;
            Ancestor = ancestor;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerator<TreeNode> GetEnumerator()
        {
            var treeNode = this;

            while (!Equals(treeNode.Left, null))
                treeNode = treeNode.Left;

            while (true)
            {
                yield return treeNode;

                if (treeNode.Right != null)
                {
                    foreach (var tree in treeNode.Right)
                        yield return tree;
                }

                if (treeNode == this) break;

                treeNode = treeNode.Ancestor;
            }
        }
    }
}