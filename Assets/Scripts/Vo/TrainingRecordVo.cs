using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 训练记录
/// </summary>
public class TrainingRecordVo : BaseVo
{
    /// <summary>
    /// 方案ID
    /// </summary>
    public int SchemeID;

    /// <summary>
    /// 学员ID
    /// </summary>
    public int StudentID;

    /// <summary>
    /// 成绩
    /// </summary>
    public int Grade;

    /// <summary>
    /// 完成时间
    /// </summary>
    public DateTime Time;

    /// <summary>
    /// 完成时长
    /// </summary>
    public string TotalTime;

    /// <summary>
    /// 训练席位
    /// 0---驾驶位
    /// 1---射击位
    /// </summary>
    public int TrainMoudle;

    public static TrainingRecordVo Parse(SqliteDataReader dr)
    {
        TrainingRecordVo vo = new TrainingRecordVo();

        vo.ID = Convert.ToInt32(dr["ID"]);
        vo.SchemeID = Convert.ToInt32(dr["SchemeID"]);
        vo.StudentID = Convert.ToInt32(dr["StudentID"]);
        vo.Grade = Convert.ToInt32(dr["Grade"]);
        vo.TotalTime = dr["TotalTime"].ToString().Trim();
        object o = dr["Time"];
        vo.Time = Convert.ToDateTime(dr["Time"]);
        vo.TrainMoudle = Convert.ToInt32(dr["TrainMoudle"]);
        DataManager.GetIns().TRecordDic[vo.ID] = vo;

        return vo;
    }


    /// <summary>
    /// 提供SQL增添语句中values的部分
    /// </summary>
    /// <returns></returns>
    public bool GetInsertStr(SqliteCommand dbCommand, SQLEditorType SQLtype, string TableConst)
    {

        string str = "INSERT INTO " + TableConst + " (SchemeID, StudentID, Grade, TotalTime, Time, TrainMoudle) VALUES (@SchemeID,@StudentID,@Grade,@TotalTime,@Time,@TrainMoudle)";
        dbCommand.CommandText = str;
        dbCommand.Parameters.AddWithValue("@SchemeID", SchemeID);
        dbCommand.Parameters.AddWithValue("@StudentID", StudentID);
        dbCommand.Parameters.AddWithValue("@Grade", Grade);
        dbCommand.Parameters.AddWithValue("@TotalTime", TotalTime);
        dbCommand.Parameters.AddWithValue("@Time", Time.ToString());
        dbCommand.Parameters.AddWithValue("@TrainMoudle", TrainMoudle);

        int num = dbCommand.ExecuteNonQuery();
        if (num >= 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 提供SQL修改语句中set的部分
    /// </summary>
    /// <returns></returns>
    public bool GetUpdateStr(SqliteCommand dbCommand, SQLEditorType SQLtype, string TableConst)
    {
        string str = " UPDATE " + TableConst + " SET Grade = " + Grade + "  WHERE rowid = " + ID;
        dbCommand.CommandText = str;
        int num = dbCommand.ExecuteNonQuery();
        if (num >= 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 提供SQL修改语句中set的部分
    /// </summary>
    /// <returns></returns>
    public bool GetDeleteStr(SqliteCommand dbCommand, SQLEditorType SQLtype, string TableConst)
    {
        string str = "delete from " + TableConst + " where ID = " + ID;
        dbCommand.CommandText = str;
        int num = dbCommand.ExecuteNonQuery();
        if (num >= 1)
        {
            return true;
        }
        return false;
    }
}
