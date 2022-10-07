using UnityEngine;
using System.Collections;
using Mono.Data.Sqlite;
using System;
using System.Reflection;

public class DBManager
{
    private static DBManager ins;
    public static DBManager Getins()
    {
        if (ins == null)
        {
            ins = new DBManager();
            //ins.con();
        }
        return ins;
    }

    /// <summary>
    /// 数据库连接定义
    /// </summary>
    private SqliteConnection dbConnection;

    /// <summary>
    /// SQL命令定义
    /// </summary>
    private SqliteCommand dbCommand;

    /// <summary>
    /// 数据读取定义
    /// </summary>
    private SqliteDataReader dataReader;

    /// <summary>
    /// 初始化数据库
    /// 载入数据库的数据
    /// </summary>
    public void Init(string SQLstr)
    {
        //SQLstr = @"Data Source=" + Application.streamingAssetsPath + "/DataBase/" + "PavDataBase.db";

        //SqliteDataReader sr = sql.ReadFullTable("StudentTable");
        try
        {
            //构造数据库连接
            dbConnection = new SqliteConnection(SQLstr);
            //打开数据库
            dbConnection.Open();
            Debug.Log("数据库已打开！");
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
    }


    /// <summary>
    /// 读取数据库数据
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="sqlTXT"></param>
    public void LoadPares<T>(string sqlTXT)
    {
        SqliteDataReader dr = ReadFullTable(sqlTXT);
        Type t = typeof(T);
        MethodInfo minfo = t.GetMethod("Parse");
        while (dr.Read())
        {
            minfo.Invoke(null, new object[] { dr });
        }
        dr.Close();
       
    }

    /// <summary>
    /// 对数据进行修改
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="SQLtype"></param>
    /// <param name="t"></param>
    public bool editor<T>(SQLEditorType SQLtype, T t, string TableConst)
    {
        int Id = 0;
        bool IsOk = false;
        dbCommand = dbConnection.CreateCommand();
        Type _t = typeof(T);

        FieldInfo fInfo = _t.GetField("ID");
        if (fInfo != null)
        {
            Id = (int)fInfo.GetValue(t);//获取ID
        }
        switch (SQLtype)
        {
            case SQLEditorType.insert:
                MethodInfo minfo = _t.GetMethod("GetInsertStr");
                if (minfo != null)
                {
                    IsOk = (bool)minfo.Invoke(t, new object[] { dbCommand, SQLtype, TableConst });//获取SQL增添数据语句需要的字符串
                }
                break;
            case SQLEditorType.delete:
                //SQLstr = "delete from " + TableConst + " where ID = " + Id;
                MethodInfo minfoDelete = _t.GetMethod("GetDeleteStr");
                if (minfoDelete != null)
                {
                    IsOk = (bool)minfoDelete.Invoke(t, new object[] { dbCommand, SQLtype, TableConst });//获取SQL删除数据语句需要的字符串
                }
                break;
            case SQLEditorType.update:
                MethodInfo _minfo = _t.GetMethod("GetUpdateStr");
                if (_minfo != null)
                {
                    IsOk = (bool)_minfo.Invoke(t, new object[] { dbCommand, SQLtype, TableConst });//获取SQL修改数据语句需要的字符串
                }
                break;
        }

        return IsOk;
    }

    /// <summary>
    /// 执行SQL命令
    /// </summary>
    /// <returns>The query.</returns>
    /// <param name="queryString">SQL命令字符串</param>
    public SqliteDataReader ExecuteQuery(string queryString)
    {
        dbCommand = dbConnection.CreateCommand();
        dbCommand.CommandText = queryString;
        dataReader = dbCommand.ExecuteReader();
        return dataReader;
    }

    /// <summary>
    /// 关闭数据库连接
    /// </summary>
    public void CloseConnection()
    {
        //销毁Command
        if (dbCommand != null)
        {
            dbCommand.Cancel();
        }
        dbCommand = null;

        //销毁Reader
        if (dataReader != null)
        {
            dataReader.Close();
        }
        dataReader = null;

        //销毁Connection
        if (dbConnection != null)
        {
            dbConnection.Close();
        }
        dbConnection = null;
    }

    /// <summary>
    /// 读取整张数据表
    /// </summary>
    /// <returns>The full table.</returns>
    /// <param name="tableName">数据表名称</param>
    public SqliteDataReader ReadFullTable(string tableName)
    {
        string queryString = "SELECT * FROM " + tableName;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 向指定数据表中插入数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="values">插入的数值</param>
    public SqliteDataReader InsertValues(string tableName, string[] values)
    {
        //获取数据表中字段数目
        int fieldCount = ReadFullTable(tableName).FieldCount;
        //当插入的数据长度不等于字段数目时引发异常
        if (values.Length != fieldCount)
        {
            throw new SqliteException("values.Length!=fieldCount");
        }

        string queryString = "INSERT INTO " + tableName + " VALUES (" + values[0];
        for (int i = 1; i < values.Length; i++)
        {
            queryString += ", " + values[i];
        }
        queryString += " )";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 更新指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    /// <param name="key">关键字</param>
    /// <param name="value">关键字对应的值</param>
    public SqliteDataReader UpdateValues(string tableName, string[] colNames, string[] colValues, string key, string operation, string value)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length");
        }

        string queryString = "UPDATE " + tableName + " SET " + colNames[0] + "=" + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += ", " + colNames[i] + "=" + colValues[i];
        }
        queryString += " WHERE " + key + operation + value;
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesOR(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += "OR " + colNames[i] + operations[0] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 删除指定数据表内的数据
    /// </summary>
    /// <returns>The values.</returns>
    /// <param name="tableName">数据表名称</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colValues">字段名对应的数据</param>
    public SqliteDataReader DeleteValuesAND(string tableName, string[] colNames, string[] operations, string[] colValues)
    {
        //当字段名称和字段数值不对应时引发异常
        if (colNames.Length != colValues.Length || operations.Length != colNames.Length || operations.Length != colValues.Length)
        {
            throw new SqliteException("colNames.Length!=colValues.Length || operations.Length!=colNames.Length || operations.Length!=colValues.Length");
        }

        string queryString = "DELETE FROM " + tableName + " WHERE " + colNames[0] + operations[0] + colValues[0];
        for (int i = 1; i < colValues.Length; i++)
        {
            queryString += " AND " + colNames[i] + operations[i] + colValues[i];
        }
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// 创建数据表
    /// </summary> +
    /// <returns>The table.</returns>
    /// <param name="tableName">数据表名</param>
    /// <param name="colNames">字段名</param>
    /// <param name="colTypes">字段名类型</param>
    public SqliteDataReader CreateTable(string tableName, string[] colNames, string[] colTypes)
    {
        string queryString = "CREATE TABLE " + tableName + "( " + colNames[0] + " " + colTypes[0];
        for (int i = 1; i < colNames.Length; i++)
        {
            queryString += ", " + colNames[i] + " " + colTypes[i];
        }
        queryString += "  ) ";
        return ExecuteQuery(queryString);
    }

    /// <summary>
    /// Reads the table.
    /// </summary>
    /// <returns>The table.</returns>
    /// <param name="tableName">Table name.</param>
    /// <param name="items">Items.</param>
    /// <param name="colNames">Col names.</param>
    /// <param name="operations">Operations.</param>
    /// <param name="colValues">Col values.</param>
    public SqliteDataReader ReadTable(string tableName, string[] items, string[] colNames, string[] operations, string[] colValues)
    {
        string queryString = "SELECT " + items[0];
        for (int i = 1; i < items.Length; i++)
        {
            queryString += ", " + items[i];
        }
        queryString += " FROM " + tableName + " WHERE " + colNames[0] + " " + operations[0] + " " + colValues[0];
        for (int i = 0; i < colNames.Length; i++)
        {
            queryString += " AND " + colNames[i] + " " + operations[i] + " " + colValues[0] + " ";
        }
        return ExecuteQuery(queryString);
    }
}
/// <summary>
/// 执行类型
/// </summary>
public enum SQLEditorType
{
    /// <summary>
    /// 增加数据
    /// </summary>
    insert,
    /// <summary>
    /// 删除数据
    /// </summary>
    delete,
    /// <summary>
    /// 修改数据
    /// </summary>
    update,
}
