using Common;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 学员操作信息事件
/// </summary>
internal class StepInfoEvent : IEventBase
{
    public void OnEvent(EventData eventData)
    {
        string SyncInfo = GetDictVal.GetData<string>((byte)ParametersCode.StartSync, eventData.Parameters);
        string[] data = SyncInfo.Split('-');// 导条台名字-客户端名字-操作
        EventManager.GetIns().ExcuteEvent(EventConstant.STUDENT_HANDLE_HINT_EVENT, data[2]);
    }
}

