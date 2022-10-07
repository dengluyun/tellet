using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using Common;
using System;

/// <summary>
/// 主控制器
/// </summary>
public class MainController : MonoBehaviour
{
    public void OnClickStartTrain()
    {

    }

    private void Awake()
    {
        Init();
        InitEvent();
    }

    private void Init()
    {
        //"INSERT INTO UserInfo VALUES (hill, 123 )";
        //sql.InsertValues("PlayerInfo", new string[] { "'2'", "'张三'", "'22'" });
        //Application.dataPath 定位到Assets文件夹
        //创建名为sqlite4unity的数据库
        PhotonEngine.GetIns().Init();
        DBManager.Getins().Init(@"Data Source=" + Application.streamingAssetsPath + "/DataBase/PavDataBase.db");
        UIManager.GetIns().InitParent(this.transform.GetChild(1), this.transform.GetChild(2));
        DataManager.GetIns().Init();
        LocalizationManager.GetIns().Init();
    }
    /// <summary>
    /// 初始化事件
    /// </summary>
    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.STARTCONTROL_EVENT, StarControl);
        EventManager.GetIns().AddEventlistener(EventConstant.CANCELCONTROL_EVENT, CancelControl);
        EventManager.GetIns().AddEventlistener(EventConstant.STARTTRAINCONTROL_EVENT, StartTrainControl);
    }

    // Start is called before the first frame update
    void Start()

    {
        //StartCoroutine(RegisterDeviceInfo());
        
        UIManager.GetIns().Show<LoadingViewController>(UIType.Show_View);
        UIManager.GetIns().Show<MainPanelController>();
        UIManager.GetIns().Show<NavPanelController>();
    }

    IEnumerator RegisterDeviceInfo()
    {
        yield return new WaitUntil(() => { return PhotonEngine.GetIns().peer.PeerState == ExitGames.Client.Photon.PeerStateValue.Connecting; });

        PhotonEngine.GetIns().SendOperation(OperationCode.RegisterDeviceInfo, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.Register, DataManager.GetIns().ControllerID}
            });
        Debug.Log("发送注册信息!");
    }

    /// <summary>
    /// 主控台开启控制
    /// </summary>
    /// <param name="o"></param>
    private void StarControl(object o)
    {
        UIManager.GetIns().Show<StopControllerPanelController>(UIType.Show_View);
    }
    /// <summary>
    /// 主客体取消控制
    /// </summary>
    /// <param name="o"></param>
    private void CancelControl(object o)
    {
        UIManager.GetIns().Close<StopControllerPanelController>();
    }

    private void StartTrainControl(object o)
    {
        string data = o as string;
        string[] datas = data.Split('-');
        int num = Convert.ToInt32(datas[2]);
        DataManager.GetIns().SchemeVo = DataManager.GetIns().FindObjectName(num);

        string student = datas[3];
        string[] students = student.Split('|');
        
        for (int i = 0; i < students.Length; i++)
        {
            DataManager.GetIns().trainStus.Add(DataManager.GetIns().FindStudent(students[i]));
        }

        DataManager.GetIns().IsCars = true;
        UIManager.GetIns().Close<StopControllerPanelController>();
        UIManager.GetIns().Show<LoadingViewController>();
        UIManager.GetIns().Show<TrainingViewPanelController>();
    }

    /// <summary>
    /// 方案数据 指定要删除的数据
    /// </summary>
    public void DeleteObjectData()
    {
        BroadcastMessage("DeleteObjectLineControllerData");
    }
    /// <summary>
    /// 学员数据 指定要删除的数据
    /// </summary>
    public void DeleteStudentData()
    {
        BroadcastMessage("DeleteStudentLineContorllerData");
    }

    private void OnDestroy()
    {
        DBManager.Getins().CloseConnection();
    }
    private void OnApplicationQuit()
    {
        DBManager.Getins().CloseConnection();

    }
    // Update is called once per frame
    void Update()
    { 
        //每帧执行 和服务器通信
        //PhotonEngine.GetIns().peer.Service();

    }

    
    
}

/// <summary>
/// 训练科目
/// </summary>
public enum SchemeName
{
    None,//无
    StartUp,//启动
    MoveOff,//起步
    Shift,//换挡
    Turn,//转向
    UpSlope,//上坡
    DownSwing,//下坡
    SideSlope,//侧坡行驶
    CrossTheRidge,//穿越山脊
    Obstacle,//障碍物通行
    Sand,//沙地行驶
    Muddy,//泥泞地面行驶
    Snow//雪地行驶
}

