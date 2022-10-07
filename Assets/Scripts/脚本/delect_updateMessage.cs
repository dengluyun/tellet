using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class delect_updateMessage : MonoBehaviour
{
    [SerializeField]
    Button update;
    [SerializeField]
    Button Delect;
    int count = 1;

  

  
    //保存所有的Inputfield
    public List<InputField> inputs_list = new List<InputField>();
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
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
                I.interactable = true;            
            }       
        }   
    }

    //保存信息
    public void SaveMessage(string s)
    {
        /*if (inputs_list.Count != 0)
        {
            foreach (var I in inputs_list)
            {
                if (I.interactable)
                {
                    I.interactable = false;
                }
            } 
        }*/
        
        SqlOperation connect=new SqlOperation(@"Data Source=" + Application.streamingAssetsPath+ "/" +"Peoples.db");
        List<string> value_List = new List<string>();
         for (int i=0; i< inputs_list.Count; i++)
         {
             value_List.Add(inputs_list[i].text);
         }
        bool   updateResult=connect.updata_Message(value_List, connect.connection);
        /*if (updateResult)
        {
            showResult("修改成功");           
        }
        else
        {
            showResult("修改失败");           
        }*/
    }



    //删除信息
    public void delectMessage()
    {
        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        bool delectOperationResult = connect.delect_Message(inputs_list[0].text, connect.connection);
        if (delectOperationResult)
        {
            print("删除成功");
            Destroy(gameObject);
        }
        else
        {
            print("删除失败");
        }

    }


    public static void showResult(string str)
    {
        tipPanel.instance.showTipPanel(str);       
    }



}
