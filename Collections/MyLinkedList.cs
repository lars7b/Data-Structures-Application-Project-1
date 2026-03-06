using System.Collections;
using System.Security.AccessControl;

namespace Project_1.Collections;

public class MyLinkedList<T> : IMyCollection<T>
{
    private Node head;
    private Node tail;
    private int _count;
    public int Count
    {
        get
        {
            if (_count < 0)
            {
                _count = 0;
            }
            return _count;
        }
        private set
        {
            _count = value;
        }
    }

    private class Node
    {
        public T Data { get; set; }
        public Node? Next { get; set; }

        public Node(T data)
        {
            Data = data;
            Next = null;
        }
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
            tail.Next = newNode;
            tail = newNode;
        }
        Count++;
    }

    public void Remove(T item)
    {
        if (head == null) return;
        if (head.Data.Equals(item))
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
            if (current.Next.Data.Equals(item))
            {
                current.Next = current.Next.Next;
                Count--;

                if (current.Next == null)
                {
                    tail = current;
                }
            }
            current = current.Next;
        }
    }

    public T? FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        Node? current = head;
        while (current != null)
        {
            if (comparer(current.Data, key))
            {
                return current.Data;
            }
            current = current.Next;
        }
        return default;
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public IMyIterator<T> GetIterator() //cant use System.Collections.Generic
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }
}