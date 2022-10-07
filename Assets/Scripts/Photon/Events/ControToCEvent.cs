using Common;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// 导条信息发送事件
/// </summary>
class ControToCEvent : IEventBase
{
    public void OnEvent(EventData eventData)
    {
        string ControToC = GetDictVal.GetData<string>((byte)ParametersCode.ControToC, eventData.Parameters);
        string[] data = ControToC.Split('-');
        switch (data[1])
        {
            case "Control":
                EventManager.GetIns().ExcuteEvent(EventConstant.STARTCONTROL_EVENT);
                break;
            case "UnControl":
                EventManager.GetIns().ExcuteEvent(EventConstant.CANCELCONTROL_EVENT);
                break;
            case "StartTrain":
                EventManager.GetIns().ExcuteEvent(EventConstant.STARTTRAINCONTROL_EVENT, ControToC);
                break;
        }
    }
}

