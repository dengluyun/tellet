using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 用于相应任务匹配，做出相应的反应
/// </summary>
public class Task_fulfilling : MonoBehaviour
{
    [SerializeField]
    Transform clockPosition;
    public float time=0;
    public static Task_fulfilling instance;
    public Dictionary<byte, object> taskFullstate = new Dictionary<byte, object>();
    public List<byte> IsFullTask = new List<byte>();



    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        //Task_matching();
    }


    //任务匹配
    public void Task_matching()
    {
        //将已完成的项目删选出来
        foreach (var t in taskFullstate.Keys)
        {
            if (taskFullstate[t].ToString() == "1")
            {
                if (!IsFullTask.Contains(t))
                {
                    IsFullTask.Add(t);
                    taskFullmatch(t.ToString());
                }                       
            }    
        }   
        
    }

    public void taskFullmatch(string i)
    {
        switch (i)
        {
            case "0":
                taskmacth(0);
            break;

            case "1":               
                taskmacth(1);
            break;

            case "2":              
                taskmacth(2);
            break;
             
            case "3":
                taskmacth(3);
            break;

            case "4":
                taskmacth(4);
            break;

            case "5":
                taskmacth(5);
            break;

            case "6":
                taskmacth(6);
            break;
            case "7":
                taskmacth(7);
            break;
            case "8":
                taskmacth(8);
            break;
            case "9":
                taskmacth(9);
                break;
            case "10":
                taskmacth(10);
                break;
            case "11":
                taskmacth(11);
                break;
            case "12":
                taskmacth(12);
                break;
            case "13":
                taskmacth(13);
                break;
            case "14":
                taskmacth(14);
                break;
            case "15":
                taskmacth(15);
                break;
        }
    }
    


    //任务匹配，做出响应
    public void taskmacth(int i)
    {

        if (startTraining.Task_steps[i] != null)
        {
            if (startTraining.Task_steps[i].transform.GetChild(0) != null)
            {

                if (clockPosition.childCount != 0 && i == 0)
                {
                    time = Clock.time;

                    startTraining.Task_steps[i].transform.GetChild(2).GetChild(0).GetComponent<Text>().text = (time / 60.0f).ToString("f2") + "分";
                }
                else
                {
                    startTraining.Task_steps[i].transform.GetChild(2).GetChild(0).GetComponent<Text>().text = ((Clock.time - time) / 60.0f).ToString("f2") + "分";
                    print("完成时间：" + (Clock.time - time));
                    time = Clock.time;
                }
            }
            if (startTraining.Task_steps[i].transform.GetChild(3) != null)
            {
                startTraining.Task_steps[i].transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "完成";
            }
          //  Training_CompletionEvent.Task_Message = "-1";
        }
          
    }

    





}
