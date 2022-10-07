using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class delectfangan : MonoBehaviour
{
    private SchemeVo _Schemevo;

    private void Awake()
    {

    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="vo"></param>
    public void Init(SchemeVo vo)
    {
        _Schemevo = vo;
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    //删除方案
    public void delectfanan()
    {
        List<string> message = new List<string>();
        for (int i = 1; i < 7; i++)
        {
            message.Add(transform.GetChild(i).GetChild(0).GetComponent<Text>().text);
        }
        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        bool result = connect.delectfanganMessage(message, connect.connection);
        DestroyImmediate(gameObject);
        print("content数量" + startTraining.instance.fannancontent.childCount);
        for (int i = 1; i <= startTraining.instance.fannancontent.childCount; i++)
        {
            startTraining.instance.fannancontent.GetChild(i - 1).GetChild(0).GetChild(0).GetComponent<Text>().text = (i).ToString();
        }


    }






}
