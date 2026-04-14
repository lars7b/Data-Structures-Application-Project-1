using System.Collections;

namespace Project_1.Collections;

public class MyBinarySearchTree<T> : IMyCollection<T>, IEnumerable<T> where T : IComparable<T>
{
    private Node? Root;
    private int _count;
    public int Count
    {
    get => _count;
    private set => _count = Math.Max(0, value);
    }
    public bool Dirty { get; set; }

    private class Node
    {
        public T Data { get; set; }
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public int Height { get; set; }


        public Node(T data)
        {
            Data = data;
            Left = null;
            Right = null;
            Height = 1;
        }
    }

    public MyBinarySearchTree()
    {
        Root = null;
        Dirty = false;
        Count = 0;
    }

    public void Add(T item)
    {
        Root = InsertHelp(Root, item);
        Dirty = true;
    }

    private Node InsertHelp(Node? node, T item)
    {
        // Standard BST insertion
        if (node == null)
        {
            Count++;
            return new Node(item);
        }

        if (item.CompareTo(node.Data) < 0)
            node.Left = InsertHelp(node.Left, item);
        else if (item.CompareTo(node.Data) > 0)
            node.Right = InsertHelp(node.Right, item);
        else
            return node; // If duplicate id

        UpdateHeight(node);
        int balance = GetBalance(node);

        // Unbalanced cases

        // LL
        if (balance > 1 && item.CompareTo(node.Left!.Data) < 0)
            return RightRotate(node);

        // RR
        if (balance < -1 && item.CompareTo(node.Right!.Data) > 0)
            return LeftRotate(node);

        // LR
        if (balance > 1 && item.CompareTo(node.Left!.Data) > 0)
        {
            node.Left = LeftRotate(node.Left);
            return RightRotate(node);
        }

        // RL
        if (balance < -1 && item.CompareTo(node.Right!.Data) < 0)
        {
            node.Right = RightRotate(node.Right);
            return LeftRotate(node);
        }

        return node;
    }

    public void Remove(T item)
    {
        bool wasRemoved = false;
        Root = RemoveHelp(Root, item, ref wasRemoved);

        if (wasRemoved)
        {
            Count--;
            Dirty = true;
        }
    }

    private Node? RemoveHelp(Node? node, T item, ref bool removed)
    {
        if (node == null) return null;

        if (item.CompareTo(node.Data) < 0)
            node.Left = RemoveHelp(node.Left, item, ref removed);
        else if (item.CompareTo(node.Data) > 0)
            node.Right = RemoveHelp(node.Right, item, ref removed);
        else
        {
            removed = true;

            // Node has 0 or 1 child
            if (node.Left == null) return node.Right;
            if (node.Right == null) return node.Left;

            // Node has 2 children
            Node successor = node.Right;
            while (successor.Left != null)
                successor = successor.Left;

            node.Data = successor.Data;

            bool dummy = false;
            node.Right = RemoveHelp(node.Right, successor.Data, ref dummy);
        }

        UpdateHeight(node);

        int balance = GetBalance(node);


        // Left Heavy
        if (balance > 1)
        {
            // LL
            if (GetBalance(node.Left) >= 0)
                return RightRotate(node);

            // LR
            if (GetBalance(node.Left) < 0)
            {
                node.Left = LeftRotate(node.Left!);
                return RightRotate(node);
            }
        }

        // Right Heavy
        if (balance < -1)
        {
            // RR
            if (GetBalance(node.Right) <= 0)
                return LeftRotate(node);

            // RL
            if (GetBalance(node.Right) > 0)
            {
                node.Right = RightRotate(node.Right!);
                return LeftRotate(node);
            }
        }

        // Return balanced node up the recursive chain
        return node;
    }


    // O(n) DFS traversal because the Func does not specify the search direction
    public Result<T> FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        if (Root == null) return new Result<T>(false, default!);

        Stack<Node> stack = new Stack<Node>();
        stack.Push(Root);

        while (stack.Count > 0)
        {
            Node current = stack.Pop();

            if (comparer(current.Data, key))
            {
                return new Result<T>(true, current.Data);
            }

            if (current.Right != null) stack.Push(current.Right);
            if (current.Left != null) stack.Push(current.Left);
        }

        return new Result<T>(false, default!);
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        var result = new MyBinarySearchTree<T>();

        foreach (var item in this)
        {
            if (predicate(item))
            {
                result.Add(item);
            }
        }

        return result;
    }

    public void Sort(Comparison<T> comparison) { }
    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        R result = default!;
        return Reduce(result, accumulator);
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        R result = initial;

        foreach (var item in this)
        {
            result = accumulator(result, item);
        }

        return result;
    }

    public IMyIterator<T> GetIterator()
    {
        return new BstIterator(Root);
    }

    private class BstIterator : IMyIterator<T>
    {
        private readonly Stack<Node> _stack;
        private readonly Node? _root;

        public BstIterator(Node? root)
        {
            _root = root;
            _stack = new Stack<Node>();
            PushLeftBranch(_root);
        }

        private void PushLeftBranch(Node? node)
        {
            while (node != null)
            {
                _stack.Push(node);
                node = node.Left;
            }
        }

        public bool HasNext()
        {
            return _stack.Count > 0;
        }

        public T Next()
        {
            if (_stack.Count == 0) throw new InvalidOperationException("End of tree.");

            Node current = _stack.Pop();
            T data = current.Data;

            if (current.Right != null)
            {
                PushLeftBranch(current.Right);
            }

            return data;
        }

        public void Reset()
        {
            _stack.Clear();
            PushLeftBranch(_root);
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        return InOrderTraversal(Root).GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    private IEnumerable<T> InOrderTraversal(Node? node)
    {
        if (node != null)
        {
            foreach (var item in InOrderTraversal(node.Left))
                yield return item;

            yield return node.Data;

            foreach (var item in InOrderTraversal(node.Right))
                yield return item;
        }
    }

    private Node RightRotate(Node y)
    {
        Node x = y.Left!;
        Node? T2 = x.Right;

        //  Rotation
        x.Right = y;
        y.Left = T2;

        // Update heights
        UpdateHeight(y);
        UpdateHeight(x);

        return x; // Return new root
    }

    private Node LeftRotate(Node x)
    {
        Node y = x.Right!;
        Node? T2 = y.Left;

        //  Rotation
        y.Left = x;
        x.Right = T2;

        // Update heights
        UpdateHeight(x);
        UpdateHeight(y);

        return y; // Return new root
    }

    private int GetHeight(Node? node)
    {
        return node?.Height ?? 0;
    }

    // Positive number = leaning left. Negative = leaning right.
    private int GetBalance(Node? node)
    {
        if (node == null) return 0;
        return GetHeight(node.Left) - GetHeight(node.Right);
    }

    // Updates the height of a node based on its children
    private void UpdateHeight(Node node)
    {
        node.Height = 1 + Math.Max(GetHeight(node.Left), GetHeight(node.Right));
    }
}