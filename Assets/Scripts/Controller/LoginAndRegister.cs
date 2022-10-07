using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class LoginAndRegister : MonoBehaviour
{
    private DBManager sql;
    public Text account;
    public InputField password;
    string zh;
    bool zhBo = false;
    string mm;
    bool dlBo;

    public GameObject LoginAndRegisterPanel;
    public GameObject MainPanel;

    Text TipT;

    private void Awake()
    {
#if UNITY_EDITOR || UNITY_STANDALONE
        //Application.dataPath 定位到Assets文件夹
        //创建名为sqlite4unity的数据库
        //sql = new DBManager(@"Data Source=" + Application.streamingAssetsPath + "/EasyLocalAreaNetworkChat.db");
        //在IOS中
#elif UNITY_IPHONE
        file = new FileInfo("file://" + Application.dataPath + "/Raw/" + "/ChatRecord.txt");
        //在Android中
#elif UNITY_ANDROID
        if(!File.Exists(Application.persistentDataPath+ "/EasyLocalAreaNetworkChat.db"))
        {
            WWW www = new WWW(Application.streamingAssetsPath + "/EasyLocalAreaNetworkChat.db");
            if (www.isDone)
            {
                File.WriteAllBytes(Application.persistentDataPath + "/EasyLocalAreaNetworkChat.db", www.bytes);
            }
        }
        sql = new DBManager(@"Data Source=" + Application.persistentDataPath + "/EasyLocalAreaNetworkChat.db");
#endif
    }

    // Start is called before the first frame update
    void Start()
    {
        //"INSERT INTO UserInfo VALUES (hill, 123 )";
        //sql.InsertValues("PlayerInfo", new string[] { "'2'", "'张三'", "'22'" });
        GameObject root = GameObject.Find("Canvas");
        Transform root1 = root.transform.Find("LoginAndRegisterPanel");
        TipT = root1.transform.Find("TipT").GetComponent<Text>();
    }
    public void Register()
    {
        //读取整张表
        SqliteDataReader reader = sql.ReadFullTable("UserInfo");
        while (reader.Read())
        {
            //读取账号
            zh = reader.GetString(reader.GetOrdinal("Account"));
            if (zh == account.text)
            {
                zhBo = true;
                StartCoroutine(OnMessage("账号重复，注册失败！"));
                break;
            }
        }
        if (zhBo != true)
        {
            //strsql=”Insert into mytable(username) values(‘” & thename & “')”        说明：&改为+号也可以吧，字符串连接
            sql.InsertValues("UserInfo", new string[] { "'" + account.text + "'", "'" + password.text + "'" });
            StartCoroutine(OnMessage("注册成功！"));
        }
        else
        {
            return;
        }
    }
    public void Login()
    {
        //读取整张表
        SqliteDataReader reader = sql.ReadFullTable("UserInfo");
        while (reader.Read())
        {
            //读取账号
            zh = reader.GetString(reader.GetOrdinal("Account"));
            mm = reader.GetString(reader.GetOrdinal("Password"));
            if (zh == account.text && mm == password.text)
            {
                dlBo = true;
                MainPanel.SetActive(true);
                LoginAndRegisterPanel.SetActive(false);
                break;
            }
        }
        if (dlBo != true)
        {
            StartCoroutine(OnMessage("登录失败！"));
        }
    }
    //信息提示
    private IEnumerator OnMessage(string tip)
    {
        TipT.text = tip;
        yield return new WaitForSeconds(1.0f);
        TipT.text = "";
    }
    //private void OnMessage(string tip)
    //{
    //    TipT.text = tip;
    //    Thread.Sleep(1000);
    //    TipT.text = "";
    //}
    private void OnDestroy()
    {
        sql.CloseConnection();
    }
    private void OnApplicationQuit()
    {
        sql.CloseConnection();
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
