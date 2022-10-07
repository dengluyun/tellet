using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mono.Data.Sqlite;
using UnityEngine.UI;
using Common;
using System;

/// <summary>
/// ��������
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
        //sql.InsertValues("PlayerInfo", new string[] { "'2'", "'����'", "'22'" });
        //Application.dataPath ��λ��Assets�ļ���
        //������Ϊsqlite4unity�����ݿ�
        PhotonEngine.GetIns().Init();
        DBManager.Getins().Init(@"Data Source=" + Application.streamingAssetsPath + "/DataBase/PavDataBase.db");
        UIManager.GetIns().InitParent(this.transform.GetChild(1), this.transform.GetChild(2));
        DataManager.GetIns().Init();
        LocalizationManager.GetIns().Init();
    }
    /// <summary>
    /// ��ʼ���¼�
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
        Debug.Log("����ע����Ϣ!");
    }

    /// <summary>
    /// ����̨��������
    /// </summary>
    /// <param name="o"></param>
    private void StarControl(object o)
    {
        UIManager.GetIns().Show<StopControllerPanelController>(UIType.Show_View);
    }
    /// <summary>
    /// ������ȡ������
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
    /// �������� ָ��Ҫɾ��������
    /// </summary>
    public void DeleteObjectData()
    {
        BroadcastMessage("DeleteObjectLineControllerData");
    }
    /// <summary>
    /// ѧԱ���� ָ��Ҫɾ��������
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
        //ÿִ֡�� �ͷ�����ͨ��
        //PhotonEngine.GetIns().peer.Service();

    }

    
    
}

/// <summary>
/// ѵ����Ŀ
/// </summary>
public enum SchemeName
{
    None,//��
    StartUp,//����
    MoveOff,//��
    Shift,//����
    Turn,//ת��
    UpSlope,//����
    DownSwing,//����
    SideSlope,//������ʻ
    CrossTheRidge,//��Խɽ��
    Obstacle,//�ϰ���ͨ��
    Sand,//ɳ����ʻ
    Muddy,//��Ţ������ʻ
    Snow//ѩ����ʻ
}

