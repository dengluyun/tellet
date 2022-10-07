using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
public class selectGradeProfab : MonoBehaviour
{
    [SerializeField]
    Button update;
    [SerializeField]
    Button Delect;
    int count = 1;
    [SerializeField]
    GameObject tipPanel;
    string beforetext = "";
    
    
    //保存所有的Inputfield
    public List<InputField> inputs_list = new List<InputField>();
    // Start is called before the first frame update
    void Start()
    {
        beforetext = transform.GetChild(3).GetComponent<InputField>().text;
    }

    // Update is called once per frame
    void Update()
    {
       // dblclick();
    }

    //显示删除和修改按钮
    public void show_button()
    {
        //count单数为不显示，双数为显示
        count += 1;
        if (count % 2 == 0)
        {
            if (!update.gameObject.activeSelf)
            {
                update.gameObject.SetActive(true);
            }
            if (!Delect.gameObject.activeSelf)
            {
                Delect.gameObject.SetActive(true);
            }
        }
        else
        {
            if (update.gameObject.activeSelf)
            {
                update.gameObject.SetActive(false);
            }
            if (Delect.gameObject.activeSelf)
            {
                Delect.gameObject.SetActive(false);
            }
        }
    }

    //当点击修改按钮时候
    public void update_Message()
    {
        if (inputs_list.Count != 0)
        {
            foreach (var I in inputs_list)
            {
                if(I==inputs_list[2]||I==inputs_list[3])
                I.interactable = true;
            }
        }
    }

    //保存信息
    public void SaveMessage(string s)
    {

       
        if(transform.GetChild(3).GetChild(2).GetComponent<Text>().text!="优秀"&&transform.GetChild(3).GetChild(2).GetComponent<Text>().text != "良好"&& transform.GetChild(3).GetChild(2).GetComponent<Text>().text != "及格"&& transform.GetChild(3).GetChild(2).GetComponent<Text>().text != "不及格")
        {
            GameObject G = Instantiate(tipPanel, transform.parent.parent.parent);
            G.transform.GetChild(0).GetComponent<Text>().text = "成绩只包括优秀、良好、及格、不及格四个等级";
            transform.GetChild(3).GetComponent<InputField>().text = beforetext;
            return;
        }
       



        if (inputs_list.Count != 0)
        {
            foreach (var I in inputs_list)
            {
                if (I.interactable)
                {
                    I.interactable = false;
                }
            }
        }

        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        List<string> value_List = new List<string>();

        if (inputs_list.Count != 0)
        {
           for(int i=0;i<inputs_list.Count;i++)
            {
              value_List.Add(inputs_list[i].text);
            }                     
        }                
        bool updateResult = connect.updata_Grade_Message(value_List, connect.connection);
        if (updateResult)
        {
            print("成绩修改成功");
            beforetext = transform.GetChild(3).GetComponent<InputField>().text;

        }
        else
        {
            print("成绩修改失败");
        }
    }

    //删除信息
    public void delectMessage()
    {
        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        List<string> delectGrade_Message = new List<string>();
        //加入学员编号
        delectGrade_Message.Add(inputs_list[0].text);
        //加入训练科目
        delectGrade_Message.Add(inputs_list[6].text);
        //加入日期
        delectGrade_Message.Add(inputs_list[4].text);
        bool delectOperationResult = connect.delectGradeMessage(delectGrade_Message, connect.connection);
        if (delectOperationResult)
        {
            print("成绩删除成功");
            Destroy(gameObject);
        }         
       else
        {
            print("成绩删除失败");
        }
    }


    public static void showResult(string str)
    {
        Geade_tipPanel.instance.showTipPanel(str);
    }
    
       

    
}

