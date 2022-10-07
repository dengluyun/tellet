using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class singleFind : MonoBehaviour
{
    //本类用于单人查询界面功能
    //对查询界面的下拉框进行初始化

    //创建一个链表将下拉框存储起来
    public List<Dropdown> Find_dropdowns_List = new List<Dropdown>();
    public List<Text> Text_List = new List<Text>();
    [SerializeField]
    InputField soliderNumber;
    string FindType = "学员编号，姓名、部队、年龄";
    string lian = "一连，二连，三连";
    string pai = "一排，二排，三排";
    string ban = "一班，二班，三班";
    public int MaxAge = 50;
    public int MinAge = 18;
    private void Awake()
    {
        //查找方式选择
        Trainingcontent.addOptions(Find_dropdowns_List[0], FindType);
        Trainingcontent.addOptions(Find_dropdowns_List[1], lian);
        Trainingcontent.addOptions(Find_dropdowns_List[2], pai);
        Trainingcontent.addOptions(Find_dropdowns_List[3], ban);
        //年龄下拉列表初始话
        Find_dropdowns_List[4].ClearOptions();
        List<string> L = new List<string>();
        for (int i = MinAge; i <= MaxAge; i++)
        {
            L.Add(i.ToString());
        }
        Find_dropdowns_List[4].AddOptions(L);
    }


    void Start()
    {


    }

    // Update is called once per frame
    void Update()
    {
        S_type();
     mainScene.close_Panel();
    }

    public void S_type()
    {
        if (Find_dropdowns_List[0].captionText.text == "学员编号")
        {
            if (!soliderNumber.gameObject.activeSelf)
            {
                soliderNumber.gameObject.SetActive(true);
            }
            foreach (var t in Text_List)
            {
                if (t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(false);
                }

            }

        }
        else
        {
            soliderNumber.gameObject.SetActive(false);
            foreach (var t in Text_List)
            {
                if (!t.gameObject.activeSelf)
                {
                    t.gameObject.SetActive(true);
                }
            }
        
        }
    }


   

}
