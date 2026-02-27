namespace Project_1.Collections;

public class MyList : IMyCollection<MyList>
{
    public int Count { get; }
    public bool Dirty { get; set; }

    public void Add(MyList item)
    {
        throw new NotImplementedException();
    }

    public void Remove(MyList item)
    {
        throw new NotImplementedException();
    }

    public MyList FindBy<K>(K key, Func<MyList, K, bool> comparer)
    {
        throw new NotImplementedException();
    }

    public IMyCollection<MyList> Filter(Func<MyList, bool> predicate)
    {
        throw new NotImplementedException();
    }

    public void Sort(Comparison<MyList> comparison)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(Func<R, MyList, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public R Reduce<R>(R initial, Func<R, MyList, R> accumulator)
    {
        throw new NotImplementedException();
    }

    public IMyIterator<MyList> GetIterator()
    {
        throw new NotImplementedException();
    }

    public IEnumerator<MyList> GetEnumerator()
    {
        throw new NotImplementedException();
    }
}