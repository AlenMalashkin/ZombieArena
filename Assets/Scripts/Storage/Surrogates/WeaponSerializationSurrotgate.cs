using System.Runtime.Serialization;

public class WeaponSerializationSurrotgate : ISerializationSurrogate
{
    public void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
    {
    }

    public object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
    {
        var weapon = (WeaponAbstract) obj;
        obj = weapon;
        return obj;
    }
}