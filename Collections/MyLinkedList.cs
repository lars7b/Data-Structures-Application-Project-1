using System.Collections;

namespace Project_1.Collections;

public class MyLinkedList<T>
{
    private Node<T> head;
    private Node<T> tail;

    public void Add(T data)
    {
        var newNode = new Node<T>(data);
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
    }

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public T FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        throw new NotImplementedException();
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
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