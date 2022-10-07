using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using System;
using Mono.Data.Sqlite;
using UnityEngine.SceneManagement;
using System.Threading;

public class startTraining : MonoBehaviour
{
    [SerializeField]
 public Transform fannancontent;
    [SerializeField]
    GameObject title;
    [SerializeField]
    GameObject step;
    [SerializeField]
 public  Transform content;
    [SerializeField]
    GameObject timecount;
    [SerializeField]
    Transform clock_position;
   public  GameObject clock;
    [SerializeField]
    Button Pause;

    [SerializeField]
    Button recover;

    [SerializeField]
    Text carPosition;
    [SerializeField]
    Text peoplePosition;

    [SerializeField]
    public GameObject tipParent;

    [SerializeField]
    Transform traingfanganContent;

    [SerializeField]
    GameObject GradePrintPanel;
    public int count = 0;
    public bool isnext = false;
    public static startTraining instance;
    bool isending = false;
    public bool isPrint = false;//是否打印成绩
    [SerializeField]
    GameObject endtraingbuarent;
    [SerializeField]
    GameObject CreatPanel;
    [SerializeField]
    GameObject windows;

    //存储人物训练时的任务信息列表
    public List<GameObject> train_scene_Peo_Message = new List<GameObject>();    

    string timeMessage;
    public static List<GameObject> Task_steps = new List<GameObject>();

    public List<GameObject> text_List = new List<GameObject>();
    public  string trainName = "";

    
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       
    }

    
    // Update is called once per frame
    void Update()
    {
        //showcarAndpeoplePositon();
    }

    public void start_training()
    {
        GradePrintPanel.transform.GetChild(2).gameObject.SetActive(true);
        GradePrintPanel.transform.GetChild(3).gameObject.SetActive(true);
        isPrint = false;
        FindChild.instance.setChildText(GradePrintPanel.transform, 0, "训练结束是否打印训练成绩？");
        if (PrintTool.instance.thread1.ThreadState == ThreadState.Suspended)
        {
            //PrintTool.instance.thread1.Resume();//恢复程序
        }
        
        //StartCoroutine(changerenderTextrue.instance.changerender());
        //加载场景
       // AddScene.isLoadScene = true;
       
        //显示加载等待画面       
       // waitLoadScene.instance.showSelf();
       // StartCoroutine(waitLoadScene.instance.waite());
        GameObject t= Selectionplan(traingfanganContent);
        if (t != null)
        {
            string sqltext = "select * from Subjects_step where Subject_name='" + t.transform.GetChild(2).GetChild(0).GetComponent<Text>().text + "'";
            SqlOperation sqlOperation = new SqlOperation(@" Data Source = " + Application.streamingAssetsPath + "/" + "Peoples.db");
            //获取接受的数据库数据
            string sqlContent = sqlOperation.FindTable(sqltext, sqlOperation.connection);
            sqlOperation.closeSql();
            //禁锢开始训练按钮      
            readSqlData(sqlContent);
            print("hhh");
        }
        AddScene.sceneType = text_List[3].transform.GetChild(0).GetComponent<Text>().text;//场景
       // AddScene.traingType= text_List[1].transform.GetChild(0).GetComponent<Text>().text;//科目
        //下发训练信息
        Dictionary<byte, object> traing_Content = new Dictionary<byte, object>();
        traing_Content.Add(0, t.transform.GetChild(2).GetChild(0).GetComponent<Text>().text);
        traing_Content.Add(2, t.transform.GetChild(4).GetChild(0).GetComponent<Text>().text);
        traing_Content.Add(1, t.transform.GetChild(5).GetChild(0).GetComponent<Text>().text);
        traing_Content.Add(3, t.transform.GetChild(6).GetChild(0).GetComponent<Text>().text);
        //PhotonEngine1.Instance.SendOperation(102, traing_Content);
        //开始训练生成一个定时器
        clock = Instantiate(timecount, clock_position);
        clock.transform.position = clock_position.transform.position;
        showTrainScenePeopleMessage();
        Dictionary<byte, object> endtraingmessage = new Dictionary<byte, object>();
        endtraingmessage.Add(0, "False");
        //PhotonEngine1.Instance.SendOperation(104, endtraingmessage);

        //开启综合训练协程
        // StartCoroutine(allTraing.instance.alltraing());

    }
     
    public GameObject Selectionplan(Transform content)
    {
        foreach (Transform t in content)
        {
            if (t.GetChild(7).GetComponent<Toggle>().isOn)
            {
                //将方案信息显示在训练监控画面
                text_List[0].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(1).GetChild(0).GetComponent<Text>().text;
                text_List[1].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(2).GetChild(0).GetComponent<Text>().text;
                text_List[2].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(3).GetChild(0).GetComponent<Text>().text;
                text_List[3].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(4).GetChild(0).GetComponent<Text>().text;//场景
                print(text_List[3].transform.GetChild(0).GetComponent<Text>().text);
                text_List[4].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(5).GetChild(0).GetComponent<Text>().text;//天气
                text_List[5].transform.GetChild(0).GetComponent<Text>().text = t.GetChild(6).GetChild(0).GetComponent<Text>().text;//时间
                return t.gameObject;
            }     
        }
        return null;
    }

    //将训练步骤显示出来
    public  void readSqlData(string data)
    {
        string[] s = data.Split('，');
        title.GetComponent<Text>().text = s[0] + "训练步骤";
        trainName = s[0];
        for (int i = 1; i < s.Length; i++)
        {
            GameObject stepGameObjects = Instantiate(step, content);
            Task_steps.Add(stepGameObjects);
            stepGameObjects.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = (content.childCount-1).ToString();
            stepGameObjects.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = s[i];
            stepGameObjects.transform.GetChild(2).GetChild(0).GetComponent<Text>().text = "\\";
            stepGameObjects.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = "未完成";
        }
    }
    IEnumerator end()
    {
        yield return new    WaitUntil(() => { return isending == true; });
        isending = false;
      
        // 发送结束训练的消息
        End_Train.instance.End_Message();
        //存储训练时间
        savetime_Message(clock);
        //存储成绩 
        if (text_List[1].transform.GetChild(0).GetComponent<Text>().text == "综合训练")
        {
            trainName = "综合训练";
        }
        saveGradeMessage();
        yield return new WaitForSeconds(1f);
        foreach (Transform child in content)
        {
            // if(child!=content.GetChild(0))
            Destroy(child.gameObject);
        }
        //结束训练时将定时器删除
        Destroy(clock, 1);
        //将控制台的画面进行隐藏
        switch (AddScene.sceneType.Trim())
        {
            case "沙漠":
                SceneManager.UnloadSceneAsync(2);
                AddScene.sceneType = "";
                // AddScene.traingType = "";
                break;

            case "山区":
                SceneManager.UnloadSceneAsync(3);
                AddScene.sceneType = "";
                //AddScene.traingType = "";
                break;
            case "草地":
                SceneManager.UnloadSceneAsync(4);
                AddScene.sceneType = "";
                // AddScene.traingType = "";
                break;
            case "城市":
                SceneManager.UnloadSceneAsync(5);
                AddScene.sceneType = "";
                // AddScene.traingType = "";
                break;

            case "机场":
                SceneManager.UnloadSceneAsync(6);
                AddScene.sceneType = "";
                //  AddScene.traingType = "";
                break;
            case "要地":
                SceneManager.UnloadSceneAsync(7);
                AddScene.sceneType = "";
                //  AddScene.traingType = "";
                break;
            case "戈壁":
                SceneManager.UnloadSceneAsync(8);
                AddScene.sceneType = "";
                //  AddScene.traingType = "";
                break;

        }
        Dictionary<byte, object> endtraingmessage = new Dictionary<byte, object>();
        endtraingmessage.Add(0, "True");
        //PhotonEngine1.Instance.SendOperation(104, endtraingmessage);
        StartCoroutine(Clear());
        endtraingbuarent.SetActive(false);
        CreatPanel.SetActive(true);
        windows.SetActive(true);
        GradePrintPanel.SetActive(false);
        //PrintTool.instance.thread1.Suspend();//挂起线程
        GradePrintPanel.transform.GetChild(2).gameObject.SetActive(true);
        GradePrintPanel.transform.GetChild(3).gameObject.SetActive(true);


        //结束训练，卸载控制台成精
        // SceneManager.UnloadSceneAsync(2);

        // 发送借宿训练命令

        //关闭综合训练协程
        // StopCoroutine(allTraing.instance.alltraing());


    }
    public void ReallyPrint()
    {
     
        GradePrintPanel.transform.GetChild(2).gameObject.SetActive(false);
        GradePrintPanel.transform.GetChild(3).gameObject.SetActive(false);
        FindChild.instance.setChildText(GradePrintPanel.transform, 0, "正在打印训练成绩.....");
        isending = true;
        isPrint = true;
    }

    public void CancalPrint()
    {
        isending = true;
        isPrint = false;
    }




    public void endTraing()
    {
        isending = true;
       // isPrint = true;
      //  GradePrintPanel.SetActive(true);//成绩打印提示
        StartCoroutine(end());
        switch (AddScene.sceneType.Trim())
        {
            case "沙漠":
                SceneManager.UnloadSceneAsync(2);
                AddScene.sceneType = "";
                break;
            case "山区":
                SceneManager.UnloadSceneAsync(3);
                AddScene.sceneType = "";
                break;
            case "草地":
                SceneManager.UnloadSceneAsync(4);
                AddScene.sceneType = "";
                break;
            case "城市":
                SceneManager.UnloadSceneAsync(5);
                AddScene.sceneType = "";
                break;
            case "机场":
                SceneManager.UnloadSceneAsync(6);
                AddScene.sceneType = "";
                break;
            case "要地":
                SceneManager.UnloadSceneAsync(7);
                AddScene.sceneType = "";
                break;
            case "戈壁":
                SceneManager.UnloadSceneAsync(8);
                AddScene.sceneType = "";
                break;

        }
        //foreach (Transform child in content)
        //{
        //   // if(child!=content.GetChild(0))
        //    Destroy(child.gameObject);
        //}
        //// 发送结束训练的消息
        //End_Train.instance.End_Message();
        ////存储训练时间
        //savetime_Message(clock);
        ////存储成绩 
        //if (text_List[1].transform.GetChild(0).GetComponent<Text>().text=="综合训练")
        //{
        //  trainName = "综合训练";
        //}
        //saveGradeMessage();
        ////结束训练时将定时器删除
        //Destroy(clock, 1);
        //将控制台的画面进行隐藏
        /* switch (AddScene.sceneType)
         {
             case "城市":
                 SceneManager.UnloadSceneAsync(4);
                 AddScene.sceneType = "";
                 AddScene.traingType = "";
                 break;
             case "沙漠":
                 SceneManager.UnloadSceneAsync(5);
                 AddScene.sceneType = "";
                 AddScene.traingType = "";
                 break;
             case "野外": case "山区":case "要地":           
                 SceneManager.UnloadSceneAsync(6);
                 AddScene.sceneType = "";
                 AddScene.traingType = "";
                 break;
        // }*/
        //Dictionary<byte, object> endtraingmessage = new Dictionary<byte, object>();
        //endtraingmessage.Add(0, "True");
        //PhotonEngine1.Instance.SendOperation(104, endtraingmessage);
        //StartCoroutine(Clear());

        //结束训练，卸载控制台成精
        // SceneManager.UnloadSceneAsync(2);

        // 发送借宿训练命令

        //关闭综合训练协程
        // StopCoroutine(allTraing.instance.alltraing());

    }
    IEnumerator Clear()
    {
        yield return new WaitForSeconds(1f);
        Task_steps.Clear();
        Task_fulfilling.instance.IsFullTask.Clear();

    }
   

    //将计时器的信息保存起来
    public void savetime_Message(GameObject g)
    {
        string Message = "";
        for (int i = g.GetComponent<Clock>().texts_List.Count - 1; i >= 0; i--)
        {
            if (i != 0)
            {
                Message += g.GetComponent<Clock>().texts_List[i].text + "，";
            }
            else
            {
                Message += g.GetComponent<Clock>().texts_List[i].text;
            }
        }

        Console_scene.time_Message = Message;
    }

    public void pause_Clock()
    {
        if (clock != null)
        {
            clock.GetComponent<Clock>().IsStop = true;
            clock.GetComponent<Clock>().stop();
            Dictionary<byte, object> traingstate = new Dictionary<byte, object>();
            //traingstate.Add((byte)ParametersCode.pause, (object)"true");
           // PhotonEngine.Instance.SendOperation(OperationCode.traingState, traingstate);
            Debug.Log("暂停发送完毕");
        }
    }
    public void recover_Clock()
    {
        if (clock != null)
        {
            clock.GetComponent<Clock>().isrecover = true;
            clock.GetComponent<Clock>().recover();
            Dictionary<byte, object> traingstate = new Dictionary<byte, object>();
            //traingstate.Add((byte)ParametersCode.recover, (object)"true");
            //PhotonEngine.Instance.SendOperation(OperationCode.traingState, traingstate);
        }
    }

    //训练结束，存储成绩
    public void saveGradeMessage()
    {

    SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");         
        List<string> U = removeMyself();
        print("用户个数"+U.Count);
        
        //foreach (var u in U)
       // {
            
        if(U.Count>0)
          reviverGrade(U, connect);
            
       // }

    }


    public List<string> removeMyself()
    {
        List<string> U = new List<string>();
        U.AddRange(User_Num.user_numList);
        if (U.Contains(User_Num.solider_Number))
        {
            U.Remove(User_Num.solider_Number);
        }                                    
        return U;
    }


      public void  reviverGrade(List<string> user_Num, SqlOperation connect)
    {
       // yield return new WaitUntil(() => { return Console_scene.Score != null; });
       // print(Console_scene.Score);
        foreach (string user in user_Num)
        {
            List<string> grade = new List<string>();
            grade.Add(text_List[0].transform.GetChild(0).GetComponent<Text>().text);
            grade.Add(user);
            // grade.Add(text_List[1].transform.GetChild(0).GetComponent<Text>().text);
            grade.Add(trainName);
            print(trainName);
            /*if (text_List[1].transform.GetChild(0).GetComponent<Text>().text == "综合训练")
            {
                grade.Add(Console_scene.Score);
                grade.Add((allTraing.time / 60.0f).ToString("f2") + "分");
                print((allTraing.time / 60.0f).ToString("f2") + "分");
                grade.Add(System.DateTime.Now.ToString());
            }
            else*/
                string g = GetGrade();
                grade.Add(g);
                grade.Add(Console_scene.time_Message);
                grade.Add(System.DateTime.Now.ToString());

            if (user != null && g != null)
            {
                bool insertresult = connect.insertGradeMessage(grade, connect.connection);
                if (insertresult)
                {
                    count++;
                }
            }
           
           // StartCoroutine(Waite(grade, connect));
        }
        if (count >= 1)
        {
            print("count:"+count);
            isnext = true;
            count = 0;
        }
        if (connect != null)
        {
            connect.comman.Dispose();
            connect.connection.Close();
            connect.connection.Dispose();
            //Console_scene.Score = null;
        }
       
    }

    IEnumerator  Waite(List<string> grade,SqlOperation connect)
    {
        yield return new WaitForSeconds(0.3f);
        bool insertresult=connect.insertGradeMessage(grade, connect.connection);
        if (insertresult)
        {
            count++;
        }
        if (count >= 1)
        {          
            isnext = true;
            count = 0;
           
        }
        

    }
    //分数判定
    public string  GetGrade()
    {
        int IsComplete = 0;
        foreach (Transform t in content)
        {
            if (t.GetChild(3).GetChild(0).GetComponent<Text>().text == "完成")
            {
                IsComplete++;
            }      
        }
        float grade = IsComplete / (content.childCount * (1.0f));
        if (grade > 0.8f)
        {
            return "优秀";
        }
        else if (grade >= 0.7 && grade < 0.8)
        {
            return "良好";
        }
        else if (grade >= 0.6 && grade < 0.7)
        {
            return "及格";
        }
        else if (grade < 0.6)
        {
            return "不及格";
        }
        return null;

        
    
    }




    //登入显示训练人员信息
     public void showTrainScenePeopleMessage()
     {
         if (User_Num.user_numList.Count!= 0)
         {                        
            getPeopleMessage(train_scene_Peo_Message[0],User_Num.user_numList[0]);                                               
         }
     }

    public void getPeopleMessage(GameObject g, string number)
    {
        SqlOperation sql = new SqlOperation(@"Data Source = " + Application.streamingAssetsPath + "/" + "Peoples.db");
        string[] str = sql.Findspecific_Pople("Solider_Number", number, sql.connection)[0].Split('，');

        if (g.transform.childCount != 0)
        {
            g.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = str[0];
            g.transform.GetChild(1).GetChild(0).GetComponent<Text>().text = str[1];
            g.transform.GetChild(2).GetChild(0). GetComponent<Text>().text = str[2];
            g.transform.GetChild(3).GetChild(0).GetComponent<Text>().text = str[4];
        }
    }

    public void showcarAndpeoplePositon()
    {
        if (carandpeopleGamobject.instance != null)
        {
            carPosition.text= carandpeopleGamobject.instance.car.position.ToString();
            peoplePosition.text = carandpeopleGamobject.instance.people.position.ToString();
        }    
    }

}
