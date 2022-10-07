using Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 根据科目类型判断是哪个席位的训练
/// </summary>
public class JugdeTrainTypeController
{ 
    /// <summary>
    /// 根据不同科目向对应席位发送命令
    /// </summary>
    /// <param name="TrainName">科目名</param>
    /// <param name="subjectType">训练类型</param>
    public static void JugdeTrainType(string TrainName, SubjectType subjectType,int Index)
    {
        if(subjectType == SubjectType.Single)
        {
            SingleHandler(TrainName, Index);
        }
        else if (subjectType == SubjectType.onesynergy)
        {
            OnesynergyHandler(TrainName, Index);
        }
        else if (subjectType == SubjectType.synergy)
        {
            Synergy(TrainName, Index);
        }
    }

    /// <summary>
    /// 单项训练处理
    /// </summary>
    private static void SingleHandler(string TrainName , int Index)
    {
        string str = JugdeSingleString(DataManager.GetIns().DrillContent,TrainName);

        PhotonEngine.GetIns().SendOperation(OperationCode.TrainingStateChange, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.TrainingStateChange, DataManager.GetIns().ControllerID.Substring(0,2)+ str +":" + Index}
            });
    }

    /// <summary>
    /// 单车训练处理
    /// </summary>
    private static void OnesynergyHandler(string TrainName, int Index)
    {
        PhotonEngine.GetIns().SendOperation(OperationCode.TrainingStateChange, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.TrainingStateChange, DataManager.GetIns().ControllerID.Substring(0,2)+ "D" +":" + Index}
            });
        PhotonEngine.GetIns().SendOperation(OperationCode.TrainingStateChange, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.TrainingStateChange, DataManager.GetIns().ControllerID.Substring(0,2)+ "S" +":" + Index}
            });
    }

    /// <summary>
    /// 多车训练处理
    /// </summary>
    private static void Synergy(string TrainName, int Index)

    {
        PhotonEngine.GetIns().SendOperation(OperationCode.TrainingStateChange, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.TrainingStateChange, DataManager.GetIns().ControllerID + ":" + Index}
            });
    }

    /// <summary>
    /// 与科目表对比
    /// </summary>
    /// <param name="Trains"></param>
    public static string JugdeSingleString(string Trains,string TrainName)
    {
        //数据切割，保存
        string[] S = Trains.Split('，');
        List<string> L = new List<string>();
        L.AddRange(S);
        string Str = "";
        switch (TrainName)
        {
            case "启动":
                Str = "D";
                break;
            case "起步":
                Str = "D";
                break;
            case "挂挡":
                Str = "D";
                break;
            case "转弯":
                Str = "D";
                break;
            case "上下坡":
                Str = "D";
                break;
            case "斜坡行驶":
                Str = "D";
                break;
            case "越过弹坑":
                Str = "D";
                break;
            case "障碍物通行":
                Str = "D";
                break;
            case "越过沟渠":
                Str = "D";
                break;
            case "静止目标射击":
                Str = "S";
                break;
            case "运动目标射击":
                Str = "S";
                break;
            case "空中目标射击":
                Str = "S";
                break;
            case "行驶":
                Str = "D";
                break;
            default:
                break;
        }
        return Str;
    }
}

