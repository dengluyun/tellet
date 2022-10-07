using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

/// <summary>
/// 服务器事件接收处理接口
/// </summary>
public interface IEventBase
{
    void OnEvent(EventData eventData);
}

