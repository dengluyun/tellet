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
public interface IOperationBase
{
    //接收到服务器的请求
    void OnOperationResponse(OperationResponse operationResponse);
}
