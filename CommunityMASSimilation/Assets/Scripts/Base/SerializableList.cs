using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class SerializableList<T> : List<T>, ISerializationCallbackReceiver
{
    [SerializeField]
    private List<T> values = new List<T>();

    public void OnBeforeSerialize()
    {
        values.Clear();
        values.AddRange(this);
    }

    public void OnAfterDeserialize()
    {
        this.Clear();
        this.AddRange(values);
    }
}