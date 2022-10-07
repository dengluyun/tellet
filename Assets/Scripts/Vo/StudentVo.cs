using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;

public class StudentVo : BaseVo
{
    /// <summary>
    /// 学员编号
    /// </summary>
    public string StudentID;

    /// <summary>
    /// 学员姓名
    /// </summary>
    public string StudentName;

    /// <summary>
    /// 年龄
    /// </summary>
    public int Age;

    /// <summary>
    /// 性别
    /// </summary>
    public string Sex;

    /// <summary>
    /// 单位
    /// </summary>
    public string Unit;

    /// <summary>
    /// 是否为删除学员
    /// </summary>
    public IsDeleteType IsDelete;

    /// <summary>
    /// 创建实例
    /// </summary>
    /// <param name="dr"></param>
    /// <returns></returns>
    public static StudentVo Parse(SqliteDataReader dr)
    {
        StudentVo vo = new StudentVo();

        vo.ID = Convert.ToInt32(dr["ID"]);
        vo.StudentID = dr["StudentID"].ToString().Trim();
        vo.StudentName = dr["StudentName"].ToString().Trim();
        vo.Age = Convert.ToInt32(dr["Age"]);
        vo.Sex = dr["Sex"].ToString().Trim();
        vo.Unit = dr["Unit"].ToString().Trim();
        vo.IsDelete = (IsDeleteType)Convert.ToInt32(dr["IsDelete"]);
        DataManager.GetIns().StudentDic[vo.ID] = vo;

        return vo;
    }

    public bool GetInsertStr(SqliteCommand dbCommand, SQLEditorType SQLtype, string TableConst)
    {

        string str = "INSERT INTO " + TableConst + " (StudentID, StudentName, Age, Sex, Unit, IsDelete) VALUES (@StudentID, @StudentName, @Age, @Sex, @Unit, @IsDelete)";
        dbCommand.CommandText = str;
        dbCommand.Parameters.AddWithValue("@StudentID", StudentID);
        dbCommand.Parameters.AddWithValue("@StudentName", StudentName);
        dbCommand.Parameters.AddWithValue("@Age", Age);
        dbCommand.Parameters.AddWithValue("@Sex", Sex);
        dbCommand.Parameters.AddWithValue("@Unit", Unit);
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

    public override string ToString()
    {
        return String.Format("学员编号:{0},学员姓名:{1},单位:{2},年龄:{3},性别:{4}", StudentID, StudentName, Unit, Age, Sex);
    }
}
