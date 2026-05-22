namespace Project_1.Collections;

public class MyArrayIterator<T>(T[] array, int count) : IMyIterator<T>
{
    private int _index;

    public bool HasNext()
    {
        return _index < count;
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