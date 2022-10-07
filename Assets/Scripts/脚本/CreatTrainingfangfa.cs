using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreatTrainingfangfa : MonoBehaviour
{
    [SerializeField]
    Transform fangfaContent;
    [SerializeField]
    GameObject fangfaPro;
    [SerializeField]
    InputField fangfadaihao;
    [SerializeField]
    Toggle dan;
    [SerializeField]
    GameObject tippanl;
    public List<Dropdown> drop_List = new List<Dropdown>();

    [SerializeField]
    GameObject game;
    // Start is called before the first frame update
    void Start()
    {
        showallfangan();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void creat()
    {
        List<string> fanganMessage = new List<string>();
        if(fangfadaihao.text=="")
        {
            showtipPanel("训练代号不能为空！");
            return;
        }
        GameObject g = Instantiate(fangfaPro, fangfaContent);
        foreach (Transform t in fangfaContent)
        {                     
                if (t.GetChild(8).gameObject.activeSelf)
                {
                    g.transform.GetChild(8).gameObject.SetActive(true);
                    break;
                }
                else
                {
                    g.transform.GetChild(8).gameObject.SetActive(false);
                    break;
                }
            }
              
        g.transform.GetChild(7).GetComponent<Toggle>().group = game.GetComponent<ToggleGroup>();
        g.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =(fangfaContent.transform.childCount).ToString();
        g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = fangfadaihao.text;
        fanganMessage.Add(fangfadaihao.text);
        g.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = drop_List[0].captionText.text;
        fanganMessage.Add(drop_List[0].captionText.text);

        if (dan.isOn == true)
        {
            g.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "单兵训练";
            fanganMessage.Add("单兵训练");
            /* g.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "\\";
             g.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = "\\";
             g.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = "\\";*/
            g.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = drop_List[1].captionText.text;
            g.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = drop_List[2].captionText.text;
            g.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = drop_List[3].captionText.text;
            fanganMessage.Add(drop_List[1].captionText.text);
            fanganMessage.Add(drop_List[2].captionText.text);
            fanganMessage.Add(drop_List[3].captionText.text);

        }
        else
        {
            g.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "协同训练";
            fanganMessage.Add("协同训练");
            g.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = drop_List[1].captionText.text;
            g.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = drop_List[2].captionText.text;
            g.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = drop_List[3].captionText.text;
            fanganMessage.Add(drop_List[1].captionText.text);
            fanganMessage.Add(drop_List[2].captionText.text);
            fanganMessage.Add(drop_List[3].captionText.text);
        }

        if (fanganMessage.Count == 6)
        {
            SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
            bool result = connect.insertFangan(fanganMessage, connect.connection);
            if (result)
            {
               print("创建成功");            
            }
            connect.comman.Dispose();
            connect.connection.Close();
        }

    }

    public void showallfangan()
    {
        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        print("进入数据库");
        List<string> fananmessage = connect.selectFangan(connect.connection);
        foreach (string s in fananmessage)
        {
            string[] S = s.Split('，');
            GameObject g = Instantiate(fangfaPro, fangfaContent);
            g.transform.GetChild(7).GetComponent<Toggle>().group = game.GetComponent<ToggleGroup>();
            g.transform.GetChild(0).GetChild(0).GetComponent<Text>().text =(fangfaContent.childCount).ToString();
            if (fangfaContent.childCount == 1)
            {
                g.transform.GetChild(7).GetComponent<Toggle>().isOn = true;
            }
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text =S[0];
            g.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = S[1];
            g.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = S[2];
            g.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = S[3];
            g.transform.GetChild(5).GetChild(0).GetComponent<Text>().text = S[4];
            g.transform.GetChild(6).GetChild(0).GetComponent<Text>().text = S[5];

        }
        connect.comman.Dispose();
        connect.connection.Close();    
    }



    public void showalldelect_but()
    {
        foreach (Transform t in fangfaContent)
        {    
          t.GetChild(8).gameObject.SetActive(!t.GetChild(8).gameObject.activeSelf);           
        }   
    }





    public void showtipPanel(string str)
    {
        tippanl.SetActive(true);
        tippanl.transform.GetChild(0).GetComponent<Text>().text = str;
    
    }
    public void hidetipPanel()
    {
        tippanl.SetActive(false);
    }

}
