namespace Project_1.Collections;

public class MyArray<T> : IMyCollection<T>
{
    private T[] _array;

    public int Count { get; private set; }

    public bool Dirty { get; set; }

    public MyArray()
    {
        _array = [];
        Count = 0;
    }

    public void Add(T item)
    {
        if (Count == _array.Length) Resize(_array, Count + 1);
        _array[Count] = item;
        Count++;
        Dirty = true;
    }

    public IMyCollection<T> Filter(Func<T, bool> predicate)
    {
        var result = new MyArray<T>();

        foreach (var item in _array)
            if (predicate(item))
                result.Add(item);
        return result;
    }

    public T? FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        throw new NotImplementedException();
    }

    public IEnumerator<T> GetEnumerator()
    {
        for (var i = 0; i < Count; i++)
            yield return _array[i];
    }

    public IMyIterator<T> GetIterator()
    {
        return new MyArrayIterator<T>(_array);
    }

    public R Reduce<R>(Func<R, T, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(R initial, Func<R, T, R> accumulator)
    {
        var result = initial;

        for (var i = 0; i < Count; i++)
            result = accumulator(result, _array[i]);
        return result;
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
                Count--;
            }
        }
    }

    public void Sort(Comparison<T> comparison)
    {
        throw new NotImplementedException();
    }

    private void Resize(T[] array, int size)
    {
        var nArray = new T[size];
        for (var i = 0; i < array.Length; i++) nArray[i] = array[i];
        _array = nArray;
    }
}