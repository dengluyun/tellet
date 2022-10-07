using Common;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;
using Debug = UnityEngine.Debug;

/// <summary>
/// ���ѵ��UI������
/// </summary>
public class TrainingViewPanelController : BaseView
{
    /// <summary>
    /// ���������·��
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/TrainingViewPanel";

    /// <summary>
    /// ѡ��Ҫ�۲��ѧԱ�ӽ�
    /// </summary>
    public GameObject SeleteStuShow;

    /// <summary>
    /// ѵ�����б�
    /// </summary>
    public Dropdown dropdown;

    /// <summary>
    /// ����ѵ����ť
    /// </summary>
    public Button EndTraingBtn;

    /// <summary>
    /// ��ͣ��ť
    /// </summary>
    public Button PauseBtn;

    /// <summary>
    /// �ָ���ť
    /// </summary>
    public Button RecoverBtn;

    /// <summary>
    /// �����л�ѧԱ��Ϣ��ť
    /// </summary>
    public Button StuMsgLeftBtn;

    /// <summary>
    /// �����л�ѧԱ��Ϣ��ť
    /// </summary>
    public Button StuMsgRightBtn;

    /// <summary>
    /// ѵ�����ݸ��ڵ�
    /// </summary>
    public Transform TrainMessage;
    /// <summary>
    /// ѧԱ��Ϣ���ڵ�
    /// </summary>
    public Transform StudentMessage;

    /// <summary>
    /// ѧԱ������Ϣ
    /// </summary>
    public Text StudentHandleMsg;
    /// <summary>
    /// ѵ����Ŀ
    /// </summary>
    private SchemeVo _schemeVo;
    /// <summary>
    /// ���ο�Ŀ����ѵ����ѧԱ
    /// </summary>
    private List<StudentVo> studentVos = new List<StudentVo>();
    /// <summary>
    /// ѧԱ�б��±�
    /// </summary>
    private int valueIdx = 0;

    Demo viewPalyer;

    /// <summary>
    /// ��������
    /// </summary>
    public GameObject TipPanel;

    /// <summary>
    /// ѵ���Ƿ��������
    /// </summary>
    bool IsTrainOK = false;

    private int StuIndex
    {
        get { return valueIdx; }
        set
        {
            if (value < 0)
            {
                value = studentVos.Count - 1;
            }
            else if (value >= studentVos.Count)
            {
                value = 0;
            }
            valueIdx = value;
        }
    }

    int StudentGradeIndex = 0;

    private void Awake()
    {
        viewPalyer = transform.GetChild(0).GetChild(2).GetComponent<Demo>();
        InitEvent();
        Whatever.Nexus.GetInstance().StartSingleTrain();
    }

    // Start is called before the first frame update
    void Start()
    {
        AddBtnOnClick();
    }

    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.RECEIVE_GRADE_EVENT, AddGradeIndex);
        EventManager.GetIns().AddEventlistener(EventConstant.STUDENT_HANDLE_HINT_EVENT, ShowStudentHandle);
    }
    /// <summary>
    /// ����ѧԱѵ���Ƿ����
    /// </summary>
    /// <param name="o"></param>
    private void AddGradeIndex(object o)
    {
        StudentGradeIndex++;
        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
        {
            IsTrainOK = true;
            TipPanel.SetActive(true);
            TipPanel.transform.GetChild(1).GetComponent<Text>().text = "ѵ������";
            TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "�ظ�ѵ��";
        }
        else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            if (StudentGradeIndex == 2)
            {
                IsTrainOK = true;
                TipPanel.SetActive(true);
                TipPanel.transform.GetChild(1).GetComponent<Text>().text = "ѵ������";
                TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "�ظ�ѵ��";
                PhotonEngine.GetIns().SendOperation(OperationCode.EndSyncTrain, new Dictionary<byte, object>()
                {
                    {(byte)ParametersCode.StartSync, DataManager.GetIns().ControllerID.Substring(0,2)}
                });
            }
        }
        else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.synergy)
        {
            //EndAudio();
            //Close();
        }
    }

    public override void RefleshView()
    {
        TipPanel.SetActive(false);
        StudentGradeIndex = 0;
        if (DataManager.GetIns().IsCars)
        {

        }
        else
        {
            if (DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
            {
                PhotonEngine.GetIns().SendOperation(OperationCode.SubjectOrder, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.SingleReceiver,DataManager.GetIns().SchemeVo.GetShowInfo() },
                 {(byte)ParametersCode.StartSync, (int)DataManager.GetIns().SchemeVo.SubType},
                 {(byte)ParametersCode.Register, DataManager.GetIns().ControllerID.Substring(0,2) + "|" + DataManager.GetIns().ControllerID.Substring(0,2) + JugdeTrainTypeController.JugdeSingleString(DataManager.GetIns().DrillContent,DataManager.GetIns().SchemeVo.Subject)}
            });
            }
            else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
            {
                PhotonEngine.GetIns().SendOperation(OperationCode.SubjectOrder, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.SingleReceiver,DataManager.GetIns().SchemeVo.GetShowInfo() },
                 {(byte)ParametersCode.StartSync, (int)DataManager.GetIns().SchemeVo.SubType},
                 {(byte)ParametersCode.Register, DataManager.GetIns().ControllerID.Substring(0,2)}
            });
            }
        }
        

        Debug.Log("���Ϳ�Ŀ��Ϣ!");

        InitTrainInfo();
    }
    /// <summary>
    /// ��ʼ��ѵ����Ϣ
    /// </summary>
    private void InitTrainInfo()
    {
        _schemeVo = DataManager.GetIns().SchemeVo;
        studentVos = DataManager.GetIns().trainStus;

        InitSubjectInfo();
        InitStudentInfo();
        CreateAudio();
        try
        {
            viewPalyer.RefleshIamge();
        }
        catch (Exception e)
        {
            viewPalyer.CloseView();
            viewPalyer.RefleshIamge();
            Debug.Log("¼��ʧ��" + e);
        }
       

        
    }
    /// <summary>
    /// չ��ѵ����Ŀ��Ϣ
    /// </summary>
    private void InitSubjectInfo()
    {
        TrainMessage.GetChild(1).GetChild(0).GetComponent<Text>().text = _schemeVo.ProjectName;
        TrainMessage.GetChild(2).GetChild(0).GetComponent<Text>().text = _schemeVo.Subject;
        TrainMessage.GetChild(3).GetChild(0).GetComponent<Text>().text = _schemeVo.SubTypeShow;
        TrainMessage.GetChild(4).GetChild(0).GetComponent<Text>().text = _schemeVo.Weather;
        TrainMessage.GetChild(5).GetChild(0).GetComponent<Text>().text = _schemeVo.DataTime;
        TrainMessage.GetChild(6).GetChild(0).GetComponent<Text>().text = _schemeVo.Environment;
    }
    /// <summary>
    /// չ��ѧԱ��Ϣ
    /// </summary>
    private void InitStudentInfo()
    {
        SeleteStuShow.SetActive(false);
        ShowStudentInfo(StuIndex);
        if (_schemeVo.SubType == SubjectType.Single)
        {
            StuMsgLeftBtn.gameObject.SetActive(false);
            StuMsgRightBtn.gameObject.SetActive(false);
        }
        else if (_schemeVo.SubType == SubjectType.onesynergy)
        {

        }
        else if (_schemeVo.SubType == SubjectType.synergy)
        {
            //SeleteStuShow.SetActive(true);
            //InitSeleteStuShow();
        }
    }

    private void ShowStudentInfo(int Index)
    {
        StudentMessage.GetChild(2).GetChild(0).GetChild(0).GetComponent<Text>().text = studentVos[Index].StudentID;
        StudentMessage.GetChild(2).GetChild(1).GetChild(0).GetComponent<Text>().text = studentVos[Index].StudentName;
        StudentMessage.GetChild(2).GetChild(2).GetChild(0).GetComponent<Text>().text = studentVos[Index].Age.ToString();
        StudentMessage.GetChild(2).GetChild(3).GetChild(0).GetComponent<Text>().text = studentVos[Index].Sex;
    }

    /// <summary>
    /// չʾѧԱ������Ϣ
    /// </summary>
    public void ShowStudentHandle(object Msg)
    {
        StudentHandleMsg.text = (string)Msg;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            CreateAudio();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            EndAudio();
        }
    }

    /// <summary>
    /// ��Ӱ�ť����¼�
    /// </summary>
    private void AddBtnOnClick()
    {
        EndTraingBtn.onClick.AddListener(EndTraingBtnHandler);
        PauseBtn.onClick.AddListener(PauseBtnHandler);
        RecoverBtn.onClick.AddListener(RecoverBtnHandler);
        StuMsgLeftBtn.onClick.AddListener(StuMsgLeftBtnHandler);
        StuMsgRightBtn.onClick.AddListener(StuMsgRightBtnHandler);


        TipPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(CancelCloseTrain);
        TipPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(CloseTrain);
        TipPanel.transform.GetChild(4).GetComponent<Button>().onClick.AddListener(ReTrain);
    }

    /// <summary>
    /// ����ѵ���¼�����
    /// </summary>
    private void EndTraingBtnHandler()
    {
        IsTrainOK = false;
        TipPanel.SetActive(true);
        TipPanel.transform.GetChild(1).GetComponent<Text>().text = "�Ƿ����ѵ��";
        TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "����";
        Whatever.Nexus.GetInstance().EndTrain();
    }
    /// <summary>
    /// ��ͣ�¼�����
    /// </summary>
    private void PauseBtnHandler()
    {
        JugdeTrainTypeController.JugdeTrainType(_schemeVo.Subject, _schemeVo.SubType, 1);
        Whatever.Nexus.GetInstance().PauseTrain();

    }
    /// <summary>
    /// �ָ��¼�����
    /// </summary>
    private void RecoverBtnHandler()
    {
        JugdeTrainTypeController.JugdeTrainType(_schemeVo.Subject, _schemeVo.SubType, 2);
        Whatever.Nexus.GetInstance().ResumeTrain();

    }

    /// <summary>
    /// �����л�ѧԱ��Ϣ��ť
    /// </summary>
    private void StuMsgLeftBtnHandler()
    {
        StuIndex--;
        ShowStudentInfo(StuIndex);
    }

    /// <summary>
    /// �����л�ѧԱ��Ϣ��ť
    /// </summary>
    private void StuMsgRightBtnHandler()
    {
        StuIndex--;
        ShowStudentInfo(StuIndex);
    }

    /// <summary>
    /// ��ʼ��ѡ���ӽ��б�
    /// </summary>
    private void InitSeleteStuShow()
    {
        List<int> carIDs = DataManager.GetIns().SynergyTraingDic.Keys.ToList();
        List<OptionData> carNames = new List<OptionData>();
        for (int i = 0; i < carIDs.Count; i++)
        {
            carNames.Add(new OptionData());
            carNames[i].text = carIDs[i] + "�ų�";
        }

        dropdown.options.Clear();
        dropdown.options = carNames;
    }

    /// <summary>
    /// ȡ���ر�ѵ������
    /// </summary>
    private void CancelCloseTrain()
    {
        if (IsTrainOK)
        {
            UIManager.GetIns().Show<CreattrainPanelController>();
            DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);
            viewPalyer.CloseView();
            EndAudio();
            Close();
        }
        
    }

    /// <summary>
    /// �ر�ѵ������
    /// </summary>
    private void CloseTrain()
    {
        
        if (!IsTrainOK)
        {
            JugdeTrainTypeController.JugdeTrainType(_schemeVo.Subject, _schemeVo.SubType, 0);
            if (_schemeVo.SubType != SubjectType.Single)
                PhotonEngine.GetIns().SendOperation(OperationCode.EndSyncTrain, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.StartSync, DataManager.GetIns().ControllerID.Substring(0,2)}
            });
        }
        UIManager.GetIns().Show<CreattrainPanelController>();
        DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);

        try
        {
            viewPalyer.CloseView();
            EndAudio();
        }
        catch (Exception)
        {
        }

        
        Close();
    }

    /// <summary>
    /// ���¿�ʼ
    /// </summary>
    private void ReTrain()
    {
        if (IsTrainOK)
        {
            DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);
            viewPalyer.CloseView();
            EndAudio();
            UIManager.GetIns().Show<LoadingViewController>();
            StartCoroutine("ReStartTrain");
        }
        else
        {
            TipPanel.SetActive(false);
        }
        
    }

    IEnumerator ReStartTrain()
    {
        yield return new WaitForSeconds(3.0f);
        RefleshView();
    }

    //����һ��ffmpeg����
    Process proc;
    //����һ���ļ������
    int i;
    //����һ��ffmpeg��ֵ
    string arguments;
    string path;

    //��ʼ¼��
    public void CreateAudio()
    {
        proc = new Process();
        //DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") -acodec aac -ar 48000 -ac 1 -vol 200 
        //for (i = 0; i < 1000; i++)
        //{
        //    if (!File.Exists("F:/ZJ-Project/Project2022/FebruaryProject/CSK181_PAV/EXE/Video/" + "out" + i + ".mp4"))
        //    {
        //        //arguments = "-f gdigrab -framerate 15 -video_size 2560*1440 -i desktop -pix_fmt yuv420p -i audio=\"Realtek High Definition Audio\" E:/���ʹ��/ffmpeg/¼����Ƶ/" + "out" + i + ".mp4";
        //        arguments = "-f gdigrab -framerate 15 -video_size 1920*1080 -i desktop -pix_fmt yuv420p F:/ZJ-Project/Project2022/FebruaryProject/CSK181_PAV/EXE/Video/" + "out" + i + ".mp4";
        //        break;
        //    }
        //}

        DataManager.GetIns().TrainTime = GetNowdateTime();

        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
        {
            if (!Directory.Exists(DataManager.GetIns().VideoPath + DataManager.GetIns().trainStus[0].StudentID))
            {
                Directory.CreateDirectory(DataManager.GetIns().VideoPath + DataManager.GetIns().trainStus[0].StudentID);
            }

            path = DataManager.GetIns().VideoPath + DataManager.GetIns().trainStus[0].StudentID + "/" + DataManager.GetIns().TrainTime + ".mp4";
        }
        else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            if (!Directory.Exists(DataManager.GetIns().VideoPath + DataManager.GetIns().SynergyTraingDic[0][1].StudentID))
            {
                Directory.CreateDirectory(DataManager.GetIns().VideoPath + DataManager.GetIns().SynergyTraingDic[0][1].StudentID);
            }

            path = DataManager.GetIns().VideoPath + DataManager.GetIns().SynergyTraingDic[0][0].StudentID + "/" + DataManager.GetIns().TrainTime + ".mp4";
        }


        if (!File.Exists(path))
        {
            arguments = DataManager.GetIns().RecordingScreenOrder + path;
        }

        proc.StartInfo.FileName = "ffmpeg.exe";
        proc.StartInfo.Arguments = arguments;
        proc.StartInfo.UseShellExecute = false;
        proc.StartInfo.CreateNoWindow = true;
        proc.StartInfo.RedirectStandardError = true;
        proc.StartInfo.RedirectStandardOutput = true;
        proc.StartInfo.RedirectStandardInput = true;
        proc.Start();
        proc.BeginOutputReadLine();
        proc.BeginErrorReadLine();
        Debug.Log("¼����");
    }
    //����¼��
    void EndAudio()
    {
        try
        {
            proc.StandardInput.WriteLine("q");//��������̵Ŀ���̨��ģ������q,������ͣ¼��
        }
        catch(Exception e)
        {
            Debug.Log("��ͣʧ��:" + e);
        }

        //��ͣ����ȴ�������Ƶ��ر�ffmpeg����
        Thread.Sleep(2000);
        Process[] process = Process.GetProcesses();
        foreach (Process p in process)
        {
            try
            {
                if (p.ProcessName == "ffmpeg")
                {
                    p.Close();
                }
            }
            catch
            {
                Debug.Log("�ر�ffmpegʧ��");
            }
        }
        Process[] process1 = Process.GetProcesses();
        foreach (Process p in process1)
        {
            try
            {
                if (p.ProcessName == "ffmpeg")
                {
                    p.Kill();
                }
            }
            catch
            {

            }
        }
        Debug.Log("����¼��");


        if (DataManager.GetIns().SchemeVo.SubType != SubjectType.Single)
        {
            if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
            {
                string destPath = DataManager.GetIns().VideoPath + DataManager.GetIns().SynergyTraingDic[0][1].StudentID + "/" + DataManager.GetIns().TrainTime + ".mp4";
                //DataManager.GetIns().SynergyTraingDic[0]
                File.Copy(path, destPath, true);      //�����ļ��м������ļ���true��ʾ���Ը���ͬ���ļ�
            }

        }

    }
    /// <summary>
    /// ȡ��¼��
    /// </summary>
    void CancelAudio()
    {
        try
        {
            proc.StandardInput.WriteLine("q");//��������̵Ŀ���̨��ģ������q,������ͣ¼��
        }
        catch
        {

        }

        //��ͣ����ȴ�������Ƶ��ر�ffmpeg����
        Thread.Sleep(2000);
        Process[] process = Process.GetProcesses();
        foreach (Process p in process)
        {
            try
            {
                if (p.ProcessName == "ffmpeg")
                {
                    p.Close();
                }
            }
            catch
            {

            }
        }
        Process[] process1 = Process.GetProcesses();
        foreach (Process p in process1)
        {
            try
            {
                if (p.ProcessName == "ffmpeg")
                {
                    p.Kill();
                }
            }
            catch
            {

            }
        }
        Debug.Log("ȡ��¼��");
        if (File.Exists(path))
        {
            // ɾ���ļ�
            File.Delete(path);
        }
    }

    private string GetNowdateTime()
    {
        string _datetime = DateTime.Now.ToString();

        //string[] strs = _datetime.Split('/');
        //_datetime = "";
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    _datetime += strs[i];
        //}

        //strs = _datetime.Split(' ');
        //_datetime = "";
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    _datetime += strs[i];
        //}

        //strs = _datetime.Split(':');
        //_datetime = "";
        //for (int i = 0; i < strs.Length; i++)
        //{
        //    _datetime += strs[i];
        //}

        _datetime = _datetime.Replace("/","@");
        _datetime = _datetime.Replace(":","#");
        _datetime = _datetime.Replace(" ","!");

        Debug.Log("¼��ʱ�䣺" + _datetime);
        return _datetime;
    }

}
