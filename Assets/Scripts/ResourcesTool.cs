using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class ResourcesTool
{
    /// <summary>
    /// 加载资源字典
    /// </summary>
    public static Dictionary<string, UnityEngine.Object> ResDic = new Dictionary<string, UnityEngine.Object>();

    /// <summary>
    /// 储存资源字典
    /// </summary>
    public static Dictionary<string, List<UnityEngine.Object>> InsDic = new Dictionary<string, List<UnityEngine.Object>>();

    public static T GetObject<T>(string _path)where T:UnityEngine.Object
    {
        if(!ResDic.ContainsKey(_path))
        {
            ResDic[_path] = Resources.Load<T>(_path);
        }

        if(!InsDic.ContainsKey(_path))
        {
            InsDic[_path] = new List<UnityEngine.Object>();
        }

        if(InsDic[_path].Count == 0)
        {
            Type _type = typeof(T);
            switch(_type.Name)
            {
                case "Sprite":
                    Sprite SP = ResDic[_path] as Sprite;
                    InsDic[_path].Add(SP);
                    break;
                case "GameObject":
                    T _insobj = UnityEngine.Object.Instantiate<T>(ResDic[_path] as T);                
                    InsDic[_path].Add(_insobj);
                    break;
                case "Material":
                    Material _M = ResDic[_path] as Material;
                    InsDic[_path].Add(_M);
                    break;
                default:
                    return null;
            }
        }

        T _t = InsDic[_path][0] as T;
        InsDic[_path].RemoveAt(0);

        return _t;
    }

    public static void Recover<T>(T _t,string _path)where T:UnityEngine.Object
    {
        if(!InsDic.ContainsKey(_path))
        {
            return;
        }
        InsDic[_path].Add(_t as UnityEngine.Object);
    }
}

