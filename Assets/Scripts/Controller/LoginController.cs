using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;

/// <summary>
/// ��¼����
/// </summary>
public class LoginController : BaseView
{
    public static string ViewName = "UIPanel/LoginPanel";

    private string _account;
    private string _passWord;

    /// <summary>
    /// �˻���
    /// </summary>
    private InputField Account;
    /// <summary>
    /// ����
    /// </summary>
    private InputField Password;

    /// <summary>
    /// ��¼��ť
    /// </summary>
    private Button LoginBtn;

    /// <summary>
    /// ��¼ʧ����ʾ
    /// </summary>
    private GameObject ErrorHint;

    /// <summary>
    /// ��ʾ���ذ�ť
    /// </summary>
    private Button HintBackBtn;
    private bool zhBo;

    private void Awake()
    {
        GetLoginComponent();
        AddOnClick();

    }

    /// <summary>
    /// ��ȡ��¼��������
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
    /// ��ӵ���¼�
    /// </summary>
    private void AddOnClick()
    {
        LoginBtn.onClick.AddListener(CheckLogin);
        HintBackBtn.onClick.AddListener(HintToBackLogin);
    }


    /// <summary>
    /// ��¼���
    /// </summary>
    private void CheckLogin()
    {
        //��ȡ���ű�
        SqliteDataReader reader = DBManager.Getins().ReadFullTable("AccountTable");
        while (reader.Read())
        {
            //��ȡ�˺�
            _account = reader.GetString(reader.GetOrdinal("Account"));
            _passWord = reader.GetString(reader.GetOrdinal("Password"));
            if (_account == Account.text && _passWord == Password.text)
            {
                Debug.Log("��¼�ɹ�!");
                return;
            }
        }

        Debug.Log("��¼ʧ��!");

        ErrorHint.SetActive(true);
    }

    public void Register()
    {
        //��ȡ���ű�
        SqliteDataReader reader = DBManager.Getins().ReadFullTable("UserInfo");
        while (reader.Read())
        {
            //��ȡ�˺�
            _account = reader.GetString(reader.GetOrdinal("Account"));
            if (_account == Account.text)
            {
                zhBo = true;
                StartCoroutine(OnMessage("�˺��ظ���ע��ʧ�ܣ�"));
                break;
            }
        }
        if (zhBo != true)
        {
            //strsql=��Insert into mytable(username) values(���� & thename & ��')��        ˵����&��Ϊ+��Ҳ���԰ɣ��ַ�������
            DBManager.Getins().InsertValues("UserInfo", new string[] { "'" + Account.text + "'", "'" + Password.text + "'" });
            StartCoroutine(OnMessage("ע��ɹ���"));
        }
        else
        {
            return;
        }
    }

    /// <summary>
    /// ���ص�¼����
    /// </summary>
    private void HintToBackLogin()
    {
        ErrorHint.SetActive(false);
    }

    //��Ϣ��ʾ
    private IEnumerator OnMessage(string tip)
    {
        //TipT.text = tip;
        yield return new WaitForSeconds(1.0f);
        //TipT.text = "";
    }
}
