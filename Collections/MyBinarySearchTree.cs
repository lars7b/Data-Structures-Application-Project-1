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
        public char Color { get; set; } 
        public Node? Left { get; set; }
        public Node? Right { get; set; }
        public Node? Parent { get; set; }

        public Node(T data)
        {
            Data = data;
            Color = 'R';
            Left = null;
            Right = null;
            Parent = null;
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
        // If tree is empty, insert as root and color black
        if (Root == null) 
        {
            Root = new Node(item);
            Root.Color = 'B';
            return;
        }
        else Root = InsertHelp(Root, item);
    }
    
    
    public void Remove(T item)
    {
        // Remove the node using standard BST rules.
        //If a black node is deleted, a "double black" condition might arise, which requires specific fixes.


        // When deleting a black node, resolve "double-black" based on the sibling's color:

        // If the sibling is red, rotate the parent, and recolor.
        // If the sibling is black:
        // If all of the sibling's children are black, recolor the sibling and propagate the issue.
        // If at least one of the sibling's child is red:
        // a. If the far child is red, rotate the parent and sibling, and recolor.
        // b. If the near child is red, rotate the sibling and its child, then handle as above.
    }

    public Result<T> FindBy<K>(K key, Func<T, K, bool> comparer) 
    { 
        // If the target value equals the current node's value, the node is found.
        // If less, move left; if greater, move right.
        // Repeat until the target is found or a NIL node is reached.
        return default; 
    }

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

    private Node InsertHelp(Node node, T item)
    {
        // Standard bst insertion, node = red
        // If parent is black, done

        // If parent is red, check uncle
            // If uncle is red, recolor parent and uncle to black, grandparent to red, Change x = x's grandparent, repeat steps 2 and 3 for new x.
            
            // If uncle is black, 
                                    // 1. Left Left Case (p is left child of g and x is left child of p) swap colors of grandparent and parent after rotations
                                    // 2. Left Right Case (p is left child of g and x is the right child of p) swap colors of grandparent and inserted node after rotations
                                    // 3. Right Right Case (Mirror of case 1) swap colors of grandparent and parent after rotations
                                    // 4. Right Left Case (Mirror of case 2)  swap colors of grandparent and inserted node after rotations
        return default;
    }
    private void LeftRotate(Node x)
    {
        // Detach Subtree: Move y's left subtree to become x's new right subtree.
        // Shift Parent Link: Update y’s parent to be x’s current parent.
        // Relink Parent: Update x’s parent to point to y instead of x.
        // Promote Child: Set y’s left child to x.
        // Finalize Parent: Set x’s parent to y.
    }

    private void RightRotate(Node x)
    {
        // Detach Subtree: Move y’s right subtree to become x’s new left subtree.
        // Shift Parent Link: Update y’s parent to be x’s current parent.
        // Relink Parent: Update x’s parent to point to y instead of x.
        // Promote Child: Set y’s right child to x.
        // Finalize Parent: Set x’s parent to y.
    }
}