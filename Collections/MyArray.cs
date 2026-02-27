public class MyArray<T> : IMyCollection<T>
{
    private T[] _array;
    private int _count;
    public MyArray()
    {
        _array = new T[4];
        _count = 0;
    }
    public int Count => _count;

    public bool Dirty { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

    public void Add(T item)
    {
        // throw new NotImplementedException();
        if(_count == _array.Length)
        {
            int newCapacity = _array.Length * 2;
            T[] newArray = new T[newCapacity];
            for(int i = 0; i < _count; i++)
            {
                newArray[i] = _array[i];
            }
            _array = newArray;
        }
        _array[_count] = item;
        _count++;
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public T FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        throw new NotImplementedException();
    }

    public IMyIterator<T> GetIterator()
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

    public void Remove(T item)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}