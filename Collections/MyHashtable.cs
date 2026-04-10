namespace Project_1.Collections;

public class MyHashtable<K, V> : IMyCollection<Entry<K,V>>, IMyHashtable<K, V>, IEnumerable<Entry<K, V>>
{
    //Index is determined by first hashing the key with a hash function. 
    //Then modulo operater (%) it against associative array size a returns the remainder.
    //If someting the index is already occupied, 
    //We can do open adressing like quadratic probing or (Log probing?), NOT LINEAR PROBING. 
    //(this does require some form of wraparound for the hashtable/ associative array)or...
    //We could also do seperate chaining so that buckets could hold multiple entries.
    //If we do that use linkedlist for seperate chaining

    //LoadFactor is also an integral part of a Hashtable.
    //We determine Entries=> actual number of elements inside table.
    //And We determine Buckets => capacity of the table.
    //LoadFactor is determined as such: (Entries : Buckets) = loadfactor (expressed in range from 0 to 1)
    //LoadFactor% is determined as such: (Entries : Buckets) x 100 = loadfactor in percent (expressed in range from 0% to 100%)
    
    // public float LoadFactor{get;set;}
    // public float Index{get;set;}
    private Entry<K, V>[]? Buckets{get;set;}
    public int Count{get; private set;}
    public int Size => Buckets!= null ? Buckets.Length: -1;
    public bool Dirty{get;set;}

    public void Add(Entry<K, V> entry)
    {
        throw new NotImplementedException();
    }
    public bool Add(K key, V value)
    {   
        throw new NotImplementedException();
    }

    public bool Delete(K key)
    {
        throw new NotImplementedException();
    }

    public V? Find()
    {
        throw new NotImplementedException();
    }

    public int FindIndex(K key)
    {
        throw new NotImplementedException();
    }

    public void Hash(K key)
    {
        
    }
    
}

public interface IMyHashtable<T>
{
}