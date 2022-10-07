using Common;
using ExitGames.Client.Photon;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 接收成绩事件
/// </summary>
public class GradeEvent : IEventBase
{
    public void OnEvent(EventData eventData)
    {
        string SyncInfo = GetDictVal.GetData<string>((byte)ParametersCode.StartSync, eventData.Parameters);
        string[] data = SyncInfo.Split('-');
        
        string GradeInfo = GetDictVal.GetData<string>((byte)ParametersCode.Grade, eventData.Parameters);
        string[] Gradedata = GradeInfo.Split('-');

        if(DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
        {
            TrainingRecordVo vo = new TrainingRecordVo();
            vo.SchemeID = DataManager.GetIns().SchemeVo.ID;
            vo.Grade = Convert.ToInt32(Gradedata[0]);
            vo.Time = DataManager.GetIns().GetDateTime();
            vo.TotalTime = Gradedata[2];
            string str = DataManager.GetIns().ControllerID.Substring(0,2);
            vo.TrainMoudle = data[1].Substring(2, 1) == "D" ? 0 : 1;
            vo.StudentID = DataManager.GetIns().trainStus[0].ID;
            DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.insert, vo, TableConst.TRAININGRECORDTABLE);
        }
        else if(DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            TrainingRecordVo vo = new TrainingRecordVo();
            vo.SchemeID = DataManager.GetIns().SchemeVo.ID;
            vo.Grade = Convert.ToInt32(Gradedata[0]);
            vo.Time = Convert.ToDateTime(Gradedata[1]);
            vo.TotalTime = Gradedata[2];
            string str = DataManager.GetIns().ControllerID.Substring(0, 2);
            vo.TrainMoudle = data[1].Substring(2, 1) == "D" ? 0 : 1;

            vo.StudentID = DataManager.GetIns().SynergyTraingDic[0][vo.TrainMoudle].ID;
            DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.insert, vo, TableConst.TRAININGRECORDTABLE);
        }
        else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.synergy)
        {
            TrainingRecordVo vo = new TrainingRecordVo();
            vo.SchemeID = DataManager.GetIns().SchemeVo.ID;
            vo.Grade = Convert.ToInt32(Gradedata[0]);
            vo.Time = Convert.ToDateTime(Gradedata[1]);
            vo.TotalTime = Gradedata[2];
            vo.TrainMoudle = data[1].Substring(2,1) == "D" ? 0 : 1;

            string str = data[1].Substring(1, 1);
            int index = Convert.ToInt32(str);
            vo.StudentID = DataManager.GetIns().SynergyTraingDic[index][vo.TrainMoudle].ID;
            DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.insert, vo, TableConst.TRAININGRECORDTABLE);
        }

        EventManager.GetIns().ExcuteEvent(EventConstant.RECEIVE_GRADE_EVENT);

    }
}

