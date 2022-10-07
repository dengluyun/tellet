using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

/// <summary>
/// 登录界面
/// </summary>
public class LoginController : BaseView
{
    public static string ViewName = "UIPanel/LoginPanel";

    private string _account;
    private string _passWord;

    /// <summary>
    /// 账户名
    /// </summary>
    private InputField Account;
    /// <summary>
    /// 密码
    /// </summary>
    private InputField Password;

    /// <summary>
    /// 登录按钮
    /// </summary>
    private Button LoginBtn;

    /// <summary>
    /// 登录失败提示
    /// </summary>
    private GameObject ErrorHint;

    /// <summary>
    /// 提示返回按钮
    /// </summary>
    private Button HintBackBtn;
    private bool zhBo;

    private void Awake()
    {
        GetLoginComponent();
        AddOnClick();

    }

    /// <summary>
    /// 获取登录所需的组件
    /// </summary>
    private void GetLoginComponent()
    {
        Account = this.transform.GetChild(1).GetChild(0).GetComponent<InputField>();
        Password = this.transform.GetChild(2).GetChild(0).GetComponent<InputField>();
        LoginBtn = this.transform.GetChild(3).GetComponent<Button>();
        ErrorHint = this.transform.GetChild(4).gameObject;
        HintBackBtn = ErrorHint.transform.GetChild(1).GetComponent<Button>();
    }

    /// <summary>
    /// 添加点击事件
    /// </summary>
    private void AddOnClick()
    {
        LoginBtn.onClick.AddListener(CheckLogin);
        HintBackBtn.onClick.AddListener(HintToBackLogin);
    }


    /// <summary>
    /// 登录检测
    /// </summary>
    private void CheckLogin()
    {
        //读取整张表
        SqliteDataReader reader = DBManager.Getins().ReadFullTable("AccountTable");
        while (reader.Read())
        {
            //读取账号
            _account = reader.GetString(reader.GetOrdinal("Account"));
            _passWord = reader.GetString(reader.GetOrdinal("Password"));
            if (_account == Account.text && _passWord == Password.text)
            {
                Debug.Log("登录成功!");
                return;
            }
        }

        Debug.Log("登录失败!");

        ErrorHint.SetActive(true);
    }

    public void Register()
    {
        //读取整张表
        SqliteDataReader reader = DBManager.Getins().ReadFullTable("UserInfo");
        while (reader.Read())
        {
            //读取账号
            _account = reader.GetString(reader.GetOrdinal("Account"));
            if (_account == Account.text)
            {
                zhBo = true;
                StartCoroutine(OnMessage("账号重复，注册失败！"));
                break;
            }
        }
        if (zhBo != true)
        {
            //strsql=”Insert into mytable(username) values(‘” & thename & “')”        说明：&改为+号也可以吧，字符串连接
            DBManager.Getins().InsertValues("UserInfo", new string[] { "'" + Account.text + "'", "'" + Password.text + "'" });
            StartCoroutine(OnMessage("注册成功！"));
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// 返回登录界面
    /// </summary>
    private void HintToBackLogin()
    {
        ErrorHint.SetActive(false);
    }

    //信息提示
    private IEnumerator OnMessage(string tip)
    {
        //TipT.text = tip;
        yield return new WaitForSeconds(1.0f);
        //TipT.text = "";
    }
}
