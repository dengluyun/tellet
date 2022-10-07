using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

/// <summary>
/// 操作基类
/// </summary>
public abstract class MyOperationBase
{
    //接收到服务器的请求
    public abstract void OnOperationResponse(OperationResponse operationResponse);

    //发送请求至服务器
    protected virtual void SendOperation() { }

    public static void ClearDelegate(Delegate del)
    {
        Delegate[] delegates = del.GetInvocationList();

        foreach(Delegate d in delegates)
        {
            del = null;
        }
    }
}
