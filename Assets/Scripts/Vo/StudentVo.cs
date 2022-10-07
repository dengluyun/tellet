using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;

public class StudentVo : BaseVo
{
    /// <summary>
    /// ѧԱ���
    /// </summary>
    public string StudentID;

    /// <summary>
    /// ѧԱ����
    /// </summary>
    public string StudentName;

    /// <summary>
    /// ����
    /// </summary>
    public int Age;

    /// <summary>
    /// �Ա�
    /// </summary>
    public string Sex;

    /// <summary>
    /// ��λ
    /// </summary>
    public string Unit;

    /// <summary>
    /// �Ƿ�Ϊɾ��ѧԱ
    /// </summary>
    public IsDeleteType IsDelete;

    /// <summary>
    /// ����ʵ��
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
    /// �ṩSQL�޸������set�Ĳ���
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
        return String.Format("ѧԱ���:{0},ѧԱ����:{1},��λ:{2},����:{3},�Ա�:{4}", StudentID, StudentName, Unit, Age, Sex);
    }
}
