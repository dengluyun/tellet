using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using System;
using System.Data;
using System.Threading;

public class SqlOperation
{

    public SqliteConnection connection;
    public SqliteCommand comman;
    SqliteDataReader reader;
    public bool isSqlConn_Success = false;

    public SqlOperation(string ConncetSqlstring)
    {
        connectSql(ConncetSqlstring);
    }


    //连接数据库
    public void connectSql(string Sql_string)
    {
        try
        {
            //连接数据库
            connection = new SqliteConnection(Sql_string);
            //打开数据库
            connection.Open();

            //判断数据库是否连接成功
            if (connection.State == ConnectionState.Open)
            {
                isSqlConn_Success = true;
                Debug.Log("数据库连接成功");
            }
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }

    //查找数据库中的表
    public string FindTable(string Find_string, SqliteConnection connection)
    {
        string Content = null;
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = Find_string;

            reader = comman.ExecuteReader();

            while (reader.Read())
            {
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    if (j != reader.FieldCount - 1)
                    {
                        Content += reader[j].ToString() + "，";
                    }
                    else
                        Content += reader[j].ToString();
                }
                return Content;
            }
        }
        return Content;
    }


    //查找数据库中特定的数据
    public List<string> Findspecific_Pople(string type, string typeValue, SqliteConnection connection)
    {

        List<string> peopel_Message_List = new List<string>();
        if (isSqlConn_Success)
        {

            comman = connection.CreateCommand();
            comman.CommandText = "select* from peoples where" + " " + type + "=" + "'" + typeValue + "'";
            reader = comman.ExecuteReader();

            while (reader.Read())
            {

                //fieldCount列数
                string Content = "";
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    if (j != reader.FieldCount - 1)
                    {
                        Content += reader[j].ToString() + "，";
                    }
                    else
                        Content += reader[j].ToString();
                }
                peopel_Message_List.Add(Content);
            }
            reader.Close();
            comman.Dispose();
            connection.Dispose();
            return peopel_Message_List;
        }

        if (peopel_Message_List.Count != 0)
        {
            return peopel_Message_List;
        }
        else return null;
    }


    //更新数据库
    public bool updata_Message(List<string> value, SqliteConnection connection)
    {
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "update  peoples set" + "  Name=" + "'" + value[1] + "'" + " , " + "Sex=" + "'" + value[3] + "'" + " , " + "Age=" + int.Parse(value[2]) + " , " + "identity=" + "'" + value[4] + "'" + " , " + "Troops=" + "'" + value[5] + "'" + "," + "password=" + "'" + value[6] + "' " + "where  Solider_Number=" + "'" + value[0] + "'";
            int i = comman.ExecuteNonQuery();
            if (i == 1)
            {
                closeSql();
                return true;

            }
            else
            {
                closeSql();
                return false;
            }

        }
        return false;
    }

    //删除数据库
    public bool delect_Message(string value, SqliteConnection connection)
    {
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "delete from peoples where Solider_Number= " + "'" + value + "'";
            int i = comman.ExecuteNonQuery();
            if (i == 1)
            {
                closeSql();
                return true;

            }
            else
            {
                closeSql();
                return false;
            }

        }
        return false;
    }

    //插入数据
    public bool insert_message(List<string> value_List, SqliteConnection connection)
    {
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            if (value_List.Count == 7)
            {
                comman.CommandText = "select count(*) from peoples where Solider_Number=" + "'" + value_List[0] + "'";
                //comman.CommandText = "insert into peoples Values(" + "'"+value_List[0] +"'"+","+"'" + value_List[1] + "'" + ","+  int.Parse(value_List[2])  + ","+ "'" + value_List[3] + "'" + ","+ "'" + value_List[4] + "'" + ","+ "'" + value_List[5] + "'" +","+ "'" + value_List[6] + "'"+")";
            }

            reader = comman.ExecuteReader();
            if (reader.Read())
            {
                Debug.Log(reader[0].ToString());
                if (reader[0].ToString() == "1")
                {
                    closeSql();
                    return false;
                }
                if (reader[0].ToString() == "0")
                {
                    reader.Close();
                    //comman.Dispose();

                    comman.CommandText = "insert into peoples Values(" + "'" + value_List[0] + "'" + "," + "'" + value_List[1] + "'" + "," + int.Parse(value_List[2]) + "," + "'" + value_List[3] + "'" + "," + "'" + value_List[4] + "'" + "," + "'" + value_List[5] + "'" + "," + "'" + value_List[6] + "'" + ")";
                    int j = comman.ExecuteNonQuery();
                    if (j == 1)
                    {
                        closeSql();
                        return true;
                    }
                }
            }
        }


        return false;

    }

    //插入方案
    public bool insertFangan(List<string> value_List, SqliteConnection connection)
    {
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "insert into Scheme Values(" + "'" + value_List[0] + "'" + "," + "'" + value_List[1] + "'" + "," + "'" + value_List[2] + "'" + "," + "'" + value_List[3] + "'" + "," + "'" + value_List[4] + "'" + "," + "'" + value_List[5] + "'" + ")";
            int j = comman.ExecuteNonQuery();
            if (j == 1)
            {
                return true;
            }
        }
        return false;
    }

    //查找方案
    public List<string> selectFangan(SqliteConnection connection)
    {
        List<string> fangan_Message_List = new List<string>();
        if (isSqlConn_Success)
        {

            comman = connection.CreateCommand();
            comman.CommandText = "select * from Scheme";
            reader = comman.ExecuteReader();
            while (reader.Read())
            {

                //fieldCount列数
                string Content = "";
                for (int j = 0; j < reader.FieldCount; j++)
                {
                    if (j != reader.FieldCount - 1)
                    {
                        Content += reader[j].ToString() + "，";
                    }
                    else
                        Content += reader[j].ToString();
                }
                fangan_Message_List.Add(Content);
            }
            reader.Close();
            return fangan_Message_List;
        }
        return fangan_Message_List;
    }

    //删除方案
    public bool delectfanganMessage(List<string> fananMessage, SqliteConnection connection)
    {

        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "delete from Scheme where scheme_name= " + "'" + fananMessage[0] + "'" + " AND program=" + "'" + fananMessage[1] + "'" + " AND type=" + "'" + fananMessage[2] + "'"+ " AND Scene=" + "'"+ fananMessage[3]+"'" + " AND weather=" + "'" + fananMessage[4] + "'" + " AND time=" + "'" + fananMessage[5] + "'";
            int i = comman.ExecuteNonQuery();
            if (i == 1)
            {               
                return true;
            }
            else
            {           
                return false;
            }
        }
        return false;
    }


    //插入成绩数据

    public bool insertGradeMessage(List<string> gradeMessage,SqliteConnection connection)
{
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "select *from peoples where Solider_Number=" + "'" + gradeMessage[1] + "'";
            Debug.Log(comman);
            reader = comman.ExecuteReader();
           
            if (reader.Read())
            {
                gradeMessage.Add(reader[1].ToString());//姓名
                gradeMessage.Add(reader[5].ToString());//部队
                reader.Close();
            }
            else
            {
                //comman.Dispose();
                //connection.Close();
                //closeSql();
                return false;
            }
            if (gradeMessage.Count == 8)
            {

                gradeMessage[4] = changetoTimeformat(gradeMessage[4]);
                comman.CommandText = "insert into Grades Values(" + "'" + gradeMessage[0] + "'" + "," + "'" + gradeMessage[1] + "'" + "," + "'" + gradeMessage[6] + "'" + "," + "'" + gradeMessage[2] + "'" + "," + "'" + gradeMessage[3] + "'" + "," + "'" + gradeMessage[4] + "'" + "," + "'" + gradeMessage[5] + "'" + "," + "'" + gradeMessage[7] + "'" + ")";
                int j = comman.ExecuteNonQuery();
                if (j == 1)
                {
                    Dictionary<string, string> message = new Dictionary<string, string>();
                    gradeMessage.Remove(gradeMessage[7]);//删除部队
                    if (startTraining.instance.isPrint)
                    {
                        for (int i = 0; i < PrintGradeMessage.Title.Count; i++)
                        {
                            if (i < 2)
                            {
                                message.Add(PrintGradeMessage.Title[i], gradeMessage[i]);
                            }

                            else if (i == 2)
                            {
                                message.Add(PrintGradeMessage.Title[2], gradeMessage[6]);
                            }
                            else
                            {
                                message.Add(PrintGradeMessage.Title[i], gradeMessage[i - 1]);
                            }
                        }
                        if (message.Count > 0)
                        {
                            List<Dictionary<string, string>> m = new List<Dictionary<string, string>>();
                            m.Add(message);
                            PrintTool.instance.Message = m;
                            startTraining.instance.isPrint = false;
                        }
                    }                 
                    
                   // comman.Dispose();
                    //connection.Close();
                    //closeSql();
                    return true;
                }
            }
           // comman.Dispose();
           // connection.Close();
        }
        else
        {

            for (int i = 0; i < 3; i++)
            {
                connection.Open();
                if (connection.State == ConnectionState.Open)
                {
                    insertGradeMessage(gradeMessage, connection);
                }
            }
         
        }
       return false;
 }
    //查询成绩信息
    public List<string> selectGrade(string type, string typeValue, SqliteConnection connection)
    {
        List<string> Grade_Message_List = new List<string>();
        if (isSqlConn_Success)
        {

            comman = connection.CreateCommand();
            comman.CommandText = "select* from Grades where" + " " + type + "=" + "'" + typeValue + "'";
            reader = comman.ExecuteReader();
            while (reader.Read())
            {

                //fieldCount列数
                string Content = "";
                for (int j = 0; j < reader.FieldCount-1; j++)
                {
                    if (j != reader.FieldCount - 2)
                    {
                        Content += reader[j].ToString() + "，";
                    }
                    else
                        Content += reader[j].ToString();
                }
                Grade_Message_List.Add(Content);
              
            }
            reader.Close();
            comman.Dispose();
            connection.Dispose();
            return Grade_Message_List;
        }
        return Grade_Message_List;
    }
    //更新成绩信息
    public bool updata_Grade_Message(List<string> value, SqliteConnection connection)
    {
        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "update  Grades set" + "  Name=" + "'" + value[1] + "'" + " , " + "Grade=" + "'" + value[2] + "' "+"," + "Time=" + "'" + value[3] + "' " +","+ "date=" + "'" + value[4] + "' " + "where  Solider_Number=" + "'" + value[0] + "'"+" AND  Time="+"'"+value[3]+"'"+" AND date="+"'"+value[4]+"'"+ "AND Solution_Name="+"'"+value[5]+"'";
            int i = comman.ExecuteNonQuery();
            if (i == 1)
            {
                closeSql();
                return true;

            }
            else
            {
                closeSql();
                return false;
            }

        }
        return false;
    }

    //删除成绩
    public bool delectGradeMessage(List<string> gradeMessage, SqliteConnection connection)
    {

        if (isSqlConn_Success)
        {
            comman = connection.CreateCommand();
            comman.CommandText = "delete from Grades where Solider_Number= " + "'" + gradeMessage[0] + "'"+ " AND Traing_Content="+"'"+gradeMessage[1]+"'"+" AND date="+"'"+gradeMessage[2]+"'";
            int i = comman.ExecuteNonQuery();
            if (i == 1)
            {
                closeSql();
                return true;

            }
            else
            {
                closeSql();
                return false;
            }

        }
        return false;
    }



    public string  changetoTimeformat(string str)
    {
        string[] S = str.Split('，');
        string content = "";
        for (int i = 1; i <= S.Length; i++)
        {
            if (i % 2 == 0&&i!=S.Length)
            {
                content += S[i-1]+":";
                continue;

            }
            content += S[i-1];                   
        }
        return content;


    }





    //关闭数据库
    public void closeSql()
    {
        if (comman != null)
        {
            comman.Dispose();
        }
       
        if (reader != null)
        {
            reader.Close();
        }

        if (connection != null)
        {
            connection.Close();
            //connection.Dispose
        }
    
    }


}
