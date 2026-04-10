using System.Collections;
using System.Security.AccessControl;

namespace Project_1.Collections;

public class MyBinarySearchTree<T> : IMyCollection<T>, IEnumerable<T>
{
    private Node? Root;
    public int Count { get; set; } // black height of the tree
    public bool Dirty { get; set; }

    private class Node
    {
        public T? Data { get; set; }
        public int Color { get; set; } // 0 for black, 1 for red
        public Node? Next { get; set; }
        public Node? Previous { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
            Previous = null;
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
        // If tree is empty, insert as root and color black, Count +1
        if (Root == null) 
        {
            Root = new Node(item) { Color = 0 };
            Count++;
            return;
        }

        // Standard bst insertion, node = red
        // If parent is black, done

        // If parent is red, check uncle
            // If uncle is red, recolor parent and uncle to black, grandparent to red, Change x = x's grandparent, repeat steps 2 and 3 for new x.
            
            // If uncle is black, 
                                    // 1. Left Left Case (p is left child of g and x is left child of p) swap colors of grandparent and parent after rotations
                                    // 2. Left Right Case (p is left child of g and x is the right child of p) swap colors of grandparent and inserted node after rotations
                                    // 3. Right Right Case (Mirror of case 1) swap colors of grandparent and parent after rotations
                                    // 4. Right Left Case (Mirror of case 2)  swap colors of grandparent and inserted node after rotations
    }
    
    
    public void Remove(T item) { }
    public Result<T> FindBy<K>(K key, Func<T, K, bool> comparer) { return default; }
    public IMyCollection<T> Filter(Func<T, bool> predicate) { return default; }
    public void Sort(Comparison<T> comparison) { }
    public R Reduce<R>(Func<R, T, R> accumulator) { return default; }
    public R Reduce<R>(R initial, Func<R, T, R> accumulator) { return default; }
    public IMyIterator<T> GetIterator() { return default; }
    public IEnumerator<T> GetEnumerator() { return default; }
    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}