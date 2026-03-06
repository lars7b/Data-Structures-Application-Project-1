using System.Collections;

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
    public T this[int i]
    {
        get => _array[i];
        set => _array[i] = value;
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

        for (var i = 0; i < Count; i++)
            if (predicate(_array[i]))
                result.Add(_array[i]);
        return result;
    }

    public Result<T>? FindBy<K>(K key, Func<T, K, bool> comparer)
    {
        for(int i = 0; i < Count; i++)
        {
            if(comparer(_array[i], key)) return new Result<T>(true, _array[i]);
        }
        return default;
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
        // throw new NotImplementedException();
        R result = default;
        return Reduce(result, accumulator);
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
        for(int i = 0; i < Count-1; i++)
        {
            int min_index = i;
            for(int j = i+1; j < Count; j++)
            {
                if(comparison(_array[j], _array[min_index]) < 0) {
                    min_index = j;
                }
            }
            if(min_index != i)
            {
                T temp = _array[i];
                _array[i] = _array[min_index];
                    
                _array[min_index] = temp;
            }
        }
        Dirty = true;
    }

    private void Resize(T[] array, int size)
    {
        var nArray = new T[size];
        for (var i = 0; i < array.Length; i++) nArray[i] = array[i];
        _array = nArray;
    }

}