namespace Project_1.Collections;

public interface IMyCollection<T>
{
    int Count { get; }
    bool Dirty { get; set; }
    void Add(T item);
    void Remove(T item);
    T? FindBy<K>(K key, Func<T, K, bool> comparer);
    IMyCollection<T> Filter(Func<T, bool> predicate);
    void Sort(Comparison<T> comparison);
    R Reduce<R>(Func<R, T, R> accumulator);
    R Reduce<R>(R initial, Func<R, T, R> accumulator);
    IMyIterator<T> GetIterator(); //todo: make custom Iterator, cant use System.Collections.Generic
    IEnumerator<T> GetEnumerator();
}

public interface IMyIterator<T>
{
    bool HasNext();
    T Next();
    void Reset();
}