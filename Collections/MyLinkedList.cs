using System.Collections;

namespace Project_1.Collections;

public class MyLinkedList<T> : IMyCollection<T>, IEnumerable<T>
{
    private Node? head;
    private Node? tail;
    public bool Dirty { get; set; }
    private int _count;
    public int Count
    {
    get => _count;
    private set => _count = Math.Max(0, value);
    }

    private class Node
    {
        public T? Data { get; set; }
        public Node? Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
    }

    public MyLinkedList()
    {
        head = null;
        tail = null;
        Dirty = false;
    }

    public void Add(T data)
    {
        var newNode = new Node(data);
        if (head == null)
        {
            head = newNode;
            tail = newNode;
        }
        else
        {
            tail!.Next = newNode;
            tail = newNode;
        }
        Count++;
        Dirty = true;
    }

    public void Remove(T item)
    {
        if (head == null) return;
        if (object.Equals(head.Data, item))
        {
            head = head.Next;
            Count--;

            if (head == null)
            {
                tail = null;
            }
            return;
        }

        Node current = head;
        while (current.Next != null)
        {
            if (object.Equals(current.Next.Data, item))
            {
                current.Next = current.Next.Next;
                Count--;

                if (current.Next == null)
                {
                    tail = current;
                }
                break;
            }
            current = current.Next;
        }
    }

    Result<T> IMyCollection<T>.FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        Node? current = head;
        while (current != null)
        {
            if (current.Data != null && comparer(current.Data, key))
            {
                return new Result<T>(true, current.Data);
            }
            current = current.Next;
        }
        return new Result<T>(false, default!);
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        var filteredList = new MyLinkedList<T>();
        Node? current = head;
        
        while (current != null)
        {
            if (current.Data != null && predicate(current.Data))
            {
                filteredList.Add(current.Data);
            }
            current = current.Next;
        }
        
        return filteredList;
    }

    public void Sort(Comparison<T> comparison)
    {
        if (head == null || head.Next == null) return;

        bool swapped;
        do
        {
            swapped = false;
            Node? current = head;

            while (current != null && current.Next != null)
            {
                if (current.Data != null && current.Next.Data != null && 
                    comparison(current.Data, current.Next.Data) > 0)
                {
                    var temp = current.Data;
                    current.Data = current.Next.Data;
                    current.Next.Data = temp;
                    
                    swapped = true;
                }
                current = current.Next;
            }
        } while (swapped);
    }

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

    public IMyIterator<T> GetIterator() //cant use System.Collections.Generic
    {
        return new LinkedListIterator(head);
    }

    private class LinkedListIterator : IMyIterator<T>
    {
        private readonly Node? head;
        private Node? current;

        public LinkedListIterator(Node? startNode)
        {
            head = startNode;
            current = startNode;
        }

        public bool HasNext()
        {
            return current != null;
        }

        public T Next()
        {
            if (current == null) throw new InvalidOperationException("End of list.");
            T data = current.Data!;
            current = current.Next;
            return data;
        }

        public void Reset()
        {
            current = head;
        }
    }

    public IEnumerator<T> GetEnumerator()
    {
        Node? current = head;
        while (current != null)
        {
            if (current.Data != null)
            {
                yield return current.Data;
            }
            current = current.Next;
        }
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}