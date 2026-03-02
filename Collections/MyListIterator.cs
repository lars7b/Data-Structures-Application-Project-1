namespace Project_1.Collections;

public class MyListIterator<T>(T[] array) : IMyIterator<T>
{
    private int _index;

    public bool HasNext()
    {
        return _index < array.Length;
    }

    public T Next()
    {
        return array[_index++];
    }

    public void Reset()
    {
        _index = 0;
    }
}