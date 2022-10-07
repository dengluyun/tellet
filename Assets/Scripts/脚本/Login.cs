using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using UnityEngine.SceneManagement;

public class Login : MonoBehaviour
{
    [SerializeField]
    InputField user_Num;
    [SerializeField]
    InputField Password;
    [SerializeField]
    GameObject toolTip_Panel;
    [SerializeField]
    Text toolTip_Panel_Content;
    public static Login instance;
    public bool isLogin = false;
    public bool isRepeat = false;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void login()
    {
        if (user_Num.text == "" || Password.text == "")
        {
            tipMessage("请完整你的信息");
        }
        else
        {
            //字典添加你的发送数据
            Dictionary<byte, object> P = new Dictionary<byte, object>();
            //添加账号密码
            P.Add((byte)0, user_Num.text);
            P.Add((byte)1,Password.text);
            //进行提示登入操作
            //PhotonEngine1.Instance.SendOperation(101,P);
           // PhotonEngine.Instance.SendOperation(OperationCode.Login,P);
            StartCoroutine(waitForLogin());
            StartCoroutine(isReapetForLogin());
        }
    
    }


    //提示信息
    public void tipMessage(string tip)
    {
        if (!toolTip_Panel.activeSelf)
        {
            toolTip_Panel.SetActive(true);
            toolTip_Panel_Content.text = tip;
        }  
    }

    public void Close_TipPanel()
    {
        toolTip_Panel.SetActive(false);
    }

    //登入等待协程
    IEnumerator waitForLogin()
    { 
        yield return new  WaitUntil(()=> {return isLogin; });
        isLogin = false;
        User_Num.solider_Number = user_Num.text;//保存登入的账号
        SceneManager.LoadSceneAsync(1, LoadSceneMode.Single);
    }

    IEnumerator isReapetForLogin()
    {
        yield return new WaitUntil(() => { return isRepeat; });
        isRepeat = false;
        tipMessage("此账号已经在线！");
    }

}
