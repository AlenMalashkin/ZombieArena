using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

public class PistolSerializableSurrotgate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
        var pistol = (Pistol) obj;
        obj = pistol;
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        var pistol = (Pistol) obj;
        obj = pistol;
        return obj;
    }
}
