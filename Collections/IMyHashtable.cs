namespace Project_1.Collections;
public interface IMyHashtable<K, V>
{
    bool Add(K key, V value);
    V? Find();
    int FindIndex(K key);
    bool Delete(K key);
}