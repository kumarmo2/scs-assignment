namespace InventoryManagement;


class Bucket
{
}

class BucketTreeNode
{
    internal int BucketId { get; set; }
    internal int Height { get; set; }
    internal BucketTreeNode Left { get; set; }
    internal BucketTreeNode Right { get; set; }

    internal float InventoryAtBucket { get; set; }
    internal float LeftTreeInventory { get; set; }
    internal float RightTreeInventory { get; set; }


    internal BucketTreeNode(int bucketId)
    {
        BucketId = bucketId;
    }
}



// This is a private class and internal data structure.
class BucketAVLTree
{
    private BucketTreeNode _root;

    internal void InsertBucket(int bucketId)
    {
        // NOTE: Caller needs to make sure bucket is inserted a single time.
        _root = Insert(_root, bucketId);
    }

    internal void PrintInorderTree()
    {
        Inorder(_root);
    }
    private void Inorder(BucketTreeNode node)
    {
        if (node is null)
        {
            return;
        }
        Inorder(node.Left);
        Console.WriteLine($"{node.BucketId}, height: {node.Height}");
        Inorder(node.Right);
    }

    internal int GetHeight()
    {
        return Height(_root);
    }

    private int Height(BucketTreeNode node)
    {
        if (node is null)
        {
            return 0;
        }
        return node.Height;
    }


    private BucketTreeNode SingleLLRotate(BucketTreeNode node)
    {
        BucketTreeNode toReturn = node.Left;
        node.Left = toReturn.Right;
        toReturn.Right = node;
        node.Height = int.Max(Height(node.Left), Height(node.Right)) + 1;
        toReturn.Height = int.Max(Height(toReturn.Left), node.Height) + 1;
        Console.WriteLine($"single rotated bucketId: {node.BucketId}, node's height: {node.Height}");

        return toReturn;
    }

    private BucketTreeNode SingleRRRotate(BucketTreeNode node)
    {
        var toReturn = node.Right;
        node.Right = toReturn.Left;
        toReturn.Left = node;
        node.Height = int.Max(Height(node.Left), Height(node.Right)) + 1;
        toReturn.Height = int.Max(Height(toReturn.Right), node.Height) + 1;
        Console.WriteLine($"single RR rotated bucketId: {node.BucketId}, node's height: {node.Height}");

        return toReturn;
    }

    private BucketTreeNode DoubleLRRotate(BucketTreeNode node)
    {
        node.Left = SingleRRRotate(node.Left);
        return SingleLLRotate(node);
    }

    private BucketTreeNode DoubleRLRotate(BucketTreeNode node)
    {
        node.Right = SingleLLRotate(node.Right);
        return SingleRRRotate(node);
    }

    private BucketTreeNode Insert(BucketTreeNode root, int newBucketId)
    {
        if (root is null)
        {
            root = new BucketTreeNode(newBucketId);
            // return root;
        }
        else if (newBucketId < root.BucketId)
        {
            Console.WriteLine($"going to left tree, {newBucketId}, root.bucketId: {root.BucketId}");
            root.Left = Insert(root.Left, newBucketId);
            if ((Height(root.Left) - Height(root.Right) == 2))
            {
                if (newBucketId < root.Left.BucketId)
                {
                    root = SingleLLRotate(root);
                }
                else
                {
                    root = DoubleLRRotate(root);
                }
            }
        }
        else if (newBucketId > root.BucketId)
        {
            root.Right = Insert(root.Right, newBucketId);
            Console.WriteLine($"going to right tree, {newBucketId}, root.bucketId: {root.BucketId}, root.right: {root.Right.BucketId}");


            if ((Height(root.Right) - Height(root.Left) == 2))
            {
                if (newBucketId > root.Right.BucketId)
                {
                    root = SingleRRRotate(root);
                }
                else
                {
                    root = DoubleRLRotate(root);
                }
            }
        }
        root.Height = int.Max(Height(root.Left), Height(root.Right)) + 1;
        Console.WriteLine($"returning root for bucketId: {newBucketId}, its height: {root.Height}");
        return root;
    }
}

public class Store
{
    private int _bucketCount;
    internal BucketAVLTree _tree;

    private Store(int bucketCount)
    {
        _bucketCount = bucketCount;
    }

    public void PrintInorderTree()
    {
        _tree.PrintInorderTree();
    }

    public static Store CreateStore(int totalBucketCount)
    {
        var store = new Store(totalBucketCount);
        store._tree = new BucketAVLTree();

        for (var i = 0; i < totalBucketCount; i++)
        {
            store._tree.InsertBucket(i);
        }
        return store;
    }

    public int Height { get { return _tree.GetHeight(); } }
}
