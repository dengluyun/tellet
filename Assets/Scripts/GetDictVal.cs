using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetDictVal
{
    public static T GetData<T>(byte code, Dictionary<byte, object> data) where T : class
    {
        object returnVal;
        data.TryGetValue((byte)code, out returnVal);

        return returnVal as T;
    }
}
