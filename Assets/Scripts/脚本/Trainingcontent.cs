using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mono.Data.Sqlite;
using UnityEngine.EventSystems;

public class Trainingcontent : MonoBehaviour
{
    [SerializeField]
    Toggle dan;
    
   public  static  Trainingcontent trainingcontent;
    //创建一个下拉列表框链表  
    public  List<Dropdown> dropdowns_List = new List<Dropdown>();   
   // string traincontent = "测地车的启动，平台扶正，初始数据装订（三维坐标装订），初始数据装订（经纬度装订），启动陀螺，校正，定位与测点标记，定向与方位角的输出";
    string danContent = "操控平台调平，定位定向训练，侦察控制操作训练(CCD)，侦察控制操作训练(热象仪)，激光测距操作，热像仪侦察校射操作训练，光电侦察校射操作训练";
    string xieContent="综合训练";
    string weather = "雨天，晴天，雪天，阴天，雾天";
    string enenvironment = "雪地，山地，水域，泥泞地段，沙地，上下平台，高速公路，弹坑"; 
    string time = "00:00，01:00，02:00，03:00，04:00，05:00，06:00，07:00，08:00，09:00，10:00，11:00，12:00，13:00，14:00，15:00，16:00，17:00，18:00，19:00，20:00，21:00，22:00，23:00";
    //存储训练环境内容，在训练画面中显示出来
    public List<Text> T = new List<Text>();
    private void Awake()
    {
       
        
        trainingcontent = this;
        //addOptions(dropdowns_List[0], traincontent);
        addOptions(dropdowns_List[1], weather);
        addOptions(dropdowns_List[2], enenvironment);
        addOptions(dropdowns_List[3], time);

    }
    // Start is called before the first frame update
    void Start()
    {
        if (dan.isOn)
        {
            addOptions(dropdowns_List[0], danContent);
        }
        else
        {
            addOptions(dropdowns_List[0], xieContent);
        }       
    }
    // Update is called once per frame
    void Update()
    {

        OnToggleClick();

      /*  if (T.Count != 0)
        {
            T[0].transform.GetChild(0).GetComponent<Text>().text = dropdowns_List[2].captionText.text;
            T[1].transform.GetChild(0).GetComponent<Text>().text = dropdowns_List[1].captionText.text;
            T[2].transform.GetChild(0).GetComponent<Text>().text = dropdowns_List[3].captionText.text;
        }*/
    }
    //下拉列表数据的添加,数据格式以逗号隔开
    public static void addOptions(Dropdown dropdown, string options)
    {
        //数据切割，保存
        string[] S = options.Split('，');
        List<string> L = new List<string>();
        L.AddRange(S);
        dropdown.ClearOptions();
        dropdown.AddOptions(L);
    }

    public void OnToggleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = EventSystem.current.currentSelectedGameObject;        
            if (g != null)
            {
                if (g.name == "Dan" && g.GetComponent<Toggle>().isOn == false)
                {
                    addOptions(dropdowns_List[0], danContent);
                }
                else if (g.name == "Xie" && g.GetComponent<Toggle>().isOn == false)
                {
                    addOptions(dropdowns_List[0], xieContent);
                }
            }          
        }
        
     }
}






