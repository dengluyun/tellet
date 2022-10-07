using Mono.Data.Sqlite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 训练方案类
/// </summary>
public class SchemeVo : BaseVo
{
    /// <summary>
    /// 方案名
    /// </summary>
    public string ProjectName;

    /// <summary>
    /// 科目名
    /// </summary>
    public string Subject;

    /// <summary>
    /// 科目类型
    /// </summary>
    public SubjectType SubType;

    public string SubTypeShow
    {
        get
        {
            if(SubType == SubjectType.Single)
            {
                return "单项训练";
            }
            else if(SubType == SubjectType.synergy)
            {
                return "协同训练";
            }
            else
            {
                return "单车协同训练";
            }
        }
    }

    /// <summary>
    /// 环境
    /// </summary>
    public string Environment;

    /// <summary>
    /// 天气--下标
    /// </summary>
    public string Weather;

    /// <summary>
    /// 时间日期
    /// </summary>
    public string DataTime;

    /// <summary>
    /// 是否为删除方案
    /// </summary>
    public IsDeleteType IsDelete;

    /// <summary>
    /// 获取下发科目信息
    /// </summary>
    /// <returns></returns>
    public string GetShowInfo()
    {
        return string.Format(Subject + "-" + Weather + "-" + Environment + "-" + DataTime.Substring(0,2) + "-" + SubTypeShow);
    }

    public static SchemeVo Parse(SqliteDataReader dr)
    {
        SchemeVo vo = new SchemeVo();

        vo.ID = Convert.ToInt32(dr["ID"]);
        vo.ProjectName = dr["ProjectName"].ToString().Trim();
        vo.Subject = dr["Subject"].ToString().Trim();
        vo.SubType = (SubjectType)Convert.ToInt32(dr["SubType"]);
        vo.Environment = dr["Environment"].ToString().Trim();
        vo.Weather = (dr["Weather"]).ToString().Trim();
        vo.DataTime = dr["DataTime"].ToString().Trim();
        vo.IsDelete = (IsDeleteType)Convert.ToInt32(dr["IsDelete"]);
        DataManager.GetIns().schemeDic[vo.ID] = vo;

        return vo;
    }

    /// <summary>
    /// 提供SQL增添语句中values的部分
    /// </summary>
    /// <returns></returns>
    public bool GetInsertStr(SqliteCommand dbCommand , SQLEditorType SQLtype , string TableConst)
    {

        string str = "INSERT INTO " + TableConst + " (ProjectName, Subject, SubType, Environment, Weather, DataTime,IsDelete) VALUES (@ProjectName,@Subject,@SubType,@Environment,@Weather,@DataTime,@IsDelete)";
        dbCommand.CommandText = str;
        dbCommand.Parameters.AddWithValue("@ProjectName", ProjectName);
        dbCommand.Parameters.AddWithValue("@Subject", Subject);
        dbCommand.Parameters.AddWithValue("@SubType", (int)SubType);
        dbCommand.Parameters.AddWithValue("@Environment", Environment);
        dbCommand.Parameters.AddWithValue("@Weather", Weather);
        dbCommand.Parameters.AddWithValue("@DataTime", DataTime);
        dbCommand.Parameters.AddWithValue("@IsDelete", 1);

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
        string str = " UPDATE " + TableConst + " SET IsDelete = 0  WHERE rowid = " + ID;
        dbCommand.CommandText = str;
        int num = dbCommand.ExecuteNonQuery();
        if (num >= 1)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// 更新
    /// </summary>
    public void ToUpdate()
    {
        DBManager.Getins().editor<SchemeVo>(SQLEditorType.update, this, TableConst.SCHEMETABLE);
    }

}

/// <summary>
/// 科目类型
/// </summary>
public enum SubjectType:int
{
    /// <summary>
    /// 单项
    /// </summary>
    Single,
    /// <summary>
    /// 单车协同
    /// </summary>
    onesynergy,
    /// <summary>
    /// 协同
    /// </summary>
    synergy
}

public enum IsDeleteType : int
{
    delete,
    Use
}
