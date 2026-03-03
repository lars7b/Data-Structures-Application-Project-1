namespace Project_1.Collections;

public class MyList<T> : IMyCollection<T>
{
    private T[] _array;
    private int _count;

    public int Count => _count;

    public bool Dirty { get; set; }

    public MyList()
    {
        _array = new T[4];
        _count = 0;
    }

    public void Add(T item)
    {
        throw new NotImplementedException();
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public T FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        // throw new NotImplementedException();
        for(int i = 0; i < _count; i++)
        {
            if(_array[i].Equals(key)) return _array[i];
        }
        return default;
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
        for(int i = 0; i < Count; i++)
        {
            if(Equals(_array[i], item))
            {
                for (int j = i; j < Count - 1; j++)
                {
                    _array[j] = _array[j + 1];
                }
                _count--;
            }
        }
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }
}