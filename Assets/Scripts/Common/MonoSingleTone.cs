using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 单例类
/// </summary>
/// where 作用： 约束T只能为MonoSingleTone类或其子类
public class MonoSingleTone<T> : MonoBehaviour where T : MonoSingleTone<T>
{

    //缺点  awake中赋值，脚本并非所有时候都可以被调用
    //public static T instance { get; private set; }

    //public void Awake()
    //{
    //    instance = this as T;
    //}

    //改进： 任何时候都可以调用
    private static T instance;

    public static T Instance
    {
        get
        {
            if (instance == null)
            {
                //在场景中根据类型查找引用   若场景中没有该脚本则为null
                instance = FindObjectOfType<T>();

                //没有的话 创建一个（不能直接new 脚本，所有new Gameobject代替）
                if (instance == null)
                {
                    //创建新对象 （立即执行Awake）
                    new GameObject("Singleton of " + typeof(T)).AddComponent<T>();
                }
               //确保只执行一次初始化函数，不重复再awake中执行
                else
                {
                    instance.Init();
                }

            }

            return instance;
        }
    }

    public void Awake()
    {
        if (instance == null)
        {
            instance = this as T;
            Init();
        }
    }

    public virtual void Init() { }
}
