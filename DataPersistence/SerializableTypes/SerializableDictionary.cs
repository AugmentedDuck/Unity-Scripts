using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializableDictionary<TKey, TValue> : Dictionary<TKey, TValue>, ISerializationCallbackReceiver
{
    //This makes the Dictionary type able to be saved in a JSON format, the format used by this save system

    [SerializeField] List<TKey> keys = new List<TKey>();
    [SerializeField] List<TValue> values = new List<TValue>();

    //This happens when the dictionary is made saveable - Goes from "Dictionary" to a serializable form
    public void OnBeforeSerialize()
    {
        //Reset the lists
        keys.Clear();
        values.Clear();

        //Then for each entry add the key-value pair
        foreach(KeyValuePair<TKey, TValue> pair in this)
        {
            keys.Add(pair.Key);
            values.Add(pair.Value);
        }
    }

    //This happens after the data is loaded - Goes from the serializable form to a "Dictionary"
    public void OnAfterDeserialize()
    {
        this.Clear();

        if (keys.Count != values.Count)
        {
            Debug.LogError("Tried to deserialize a SerializableDictionary, but the amount of keys (" + keys.Count + ") does not match the number of values (" + values.Count + ")");
        }

        for (int i = 0; i < keys.Count; i++)
        {
            this.Add(keys[i], values[i]);
        }
    }
}
