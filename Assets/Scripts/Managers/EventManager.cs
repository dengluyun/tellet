using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 事件管理器
/// </summary>
public class EventManager
{
    private static EventManager _Ins;
    public static EventManager GetIns()
    {
        if(_Ins == null)
        {
            _Ins = new EventManager();
        }
        return _Ins;
    }

    /// <summary>
    /// 存储所有的事件的字典
    /// </summary>
    Dictionary<string, CallFun> CallDic = new Dictionary<string, CallFun>();

    /// <summary>
    /// 添加监听事件
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="fun">委托方法</param>
    public void AddEventlistener(string eventName , CallFun fun)
    {
        if (CallDic.ContainsKey(eventName))
        {
            CallDic[eventName] += fun;
        }
        else
        {
            CallDic.Add(eventName , fun);
        }
    }

    /// <summary>
    /// 执行监听
    /// </summary>
    /// <param name="eventName">事件名</param>
    /// <param name="obj">需要获取的参数参数</param>
    public void ExcuteEvent(string eventName , object obj = null)
    {
        if (CallDic.ContainsKey(eventName))
        {
            CallDic[eventName](obj);
        }
    }

    /// <summary>
    /// 撤销事件
    /// </summary>
    /// <param name="eventName"></param>
    public void RemoveEvent(string eventName , CallFun fun = null)
    {
        
        if (CallDic.ContainsKey(eventName))
        {
            if (fun == null)
            {
                CallDic.Remove(eventName);
            }
            else
            {
                CallDic[eventName] -= fun;
            }
        }
    }

}
