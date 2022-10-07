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
/// 监控训练UI控制类
/// </summary>
public class TrainingViewPanelController : BaseView
{
    /// <summary>
    /// 主界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/TrainingViewPanel";

    /// <summary>
    /// 选择要观察的学员视角
    /// </summary>
    public GameObject SeleteStuShow;

    /// <summary>
    /// 训练车列表
    /// </summary>
    public Dropdown dropdown;

    /// <summary>
    /// 结束训练按钮
    /// </summary>
    public Button EndTraingBtn;

    /// <summary>
    /// 暂停按钮
    /// </summary>
    public Button PauseBtn;

    /// <summary>
    /// 恢复按钮
    /// </summary>
    public Button RecoverBtn;

    /// <summary>
    /// 向右切换学员信息按钮
    /// </summary>
    public Button StuMsgLeftBtn;

    /// <summary>
    /// 向左切换学员信息按钮
    /// </summary>
    public Button StuMsgRightBtn;

    /// <summary>
    /// 训练内容父节点
    /// </summary>
    public Transform TrainMessage;
    /// <summary>
    /// 学员信息父节点
    /// </summary>
    public Transform StudentMessage;

    /// <summary>
    /// 学员操作信息
    /// </summary>
    public Text StudentHandleMsg;
    /// <summary>
    /// 训练科目
    /// </summary>
    private SchemeVo _schemeVo;
    /// <summary>
    /// 本次科目进行训练的学员
    /// </summary>
    private List<StudentVo> studentVos = new List<StudentVo>();
    /// <summary>
    /// 学员列表下标
    /// </summary>
    private int valueIdx = 0;

    Demo viewPalyer;

    /// <summary>
    /// 结束界面
    /// </summary>
    public GameObject TipPanel;

    /// <summary>
    /// 训练是否是已完成
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
    /// 接收学员训练是否完成
    /// </summary>
    /// <param name="o"></param>
    private void AddGradeIndex(object o)
    {
        StudentGradeIndex++;
        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
        {
            IsTrainOK = true;
            TipPanel.SetActive(true);
            TipPanel.transform.GetChild(1).GetComponent<Text>().text = "训练结束";
            TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "重复训练";
        }
        else if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            if (StudentGradeIndex == 2)
            {
                IsTrainOK = true;
                TipPanel.SetActive(true);
                TipPanel.transform.GetChild(1).GetComponent<Text>().text = "训练结束";
                TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "重复训练";
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
        

        Debug.Log("发送科目信息!");

        InitTrainInfo();
    }
    /// <summary>
    /// 初始化训练信息
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
            Debug.Log("录制失败" + e);
        }
       

        
    }
    /// <summary>
    /// 展现训练科目信息
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
    /// 展现学员信息
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
    /// 展示学员操作信息
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
    /// 添加按钮点击事件
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
    /// 结束训练事件处理
    /// </summary>
    private void EndTraingBtnHandler()
    {
        IsTrainOK = false;
        TipPanel.SetActive(true);
        TipPanel.transform.GetChild(1).GetComponent<Text>().text = "是否结束训练";
        TipPanel.transform.GetChild(4).GetChild(0).GetComponent<Text>().text = "继续";
        Whatever.Nexus.GetInstance().EndTrain();
    }
    /// <summary>
    /// 暂停事件处理
    /// </summary>
    private void PauseBtnHandler()
    {
        JugdeTrainTypeController.JugdeTrainType(_schemeVo.Subject, _schemeVo.SubType, 1);
        Whatever.Nexus.GetInstance().PauseTrain();

    }
    /// <summary>
    /// 恢复事件处理
    /// </summary>
    private void RecoverBtnHandler()
    {
        JugdeTrainTypeController.JugdeTrainType(_schemeVo.Subject, _schemeVo.SubType, 2);
        Whatever.Nexus.GetInstance().ResumeTrain();

    }

    /// <summary>
    /// 向左切换学员信息按钮
    /// </summary>
    private void StuMsgLeftBtnHandler()
    {
        StuIndex--;
        ShowStudentInfo(StuIndex);
    }

    /// <summary>
    /// 向右切换学员信息按钮
    /// </summary>
    private void StuMsgRightBtnHandler()
    {
        StuIndex--;
        ShowStudentInfo(StuIndex);
    }

    /// <summary>
    /// 初始化选择视角列表
    /// </summary>
    private void InitSeleteStuShow()
    {
        List<int> carIDs = DataManager.GetIns().SynergyTraingDic.Keys.ToList();
        List<OptionData> carNames = new List<OptionData>();
        for (int i = 0; i < carIDs.Count; i++)
        {
            carNames.Add(new OptionData());
            carNames[i].text = carIDs[i] + "号车";
        }

        dropdown.options.Clear();
        dropdown.options = carNames;
    }

    /// <summary>
    /// 取消关闭训练界面
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
    /// 关闭训练界面
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
    /// 重新开始
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

    //定义一个ffmpeg进程
    Process proc;
    //定义一个文件名序号
    int i;
    //定义一个ffmpeg的值
    string arguments;
    string path;

    //开始录制
    public void CreateAudio()
    {
        proc = new Process();
        //DateTime.Now.ToString("yyyy-MM-dd-HH:mm:ss") -acodec aac -ar 48000 -ac 1 -vol 200 
        //for (i = 0; i < 1000; i++)
        //{
        //    if (!File.Exists("F:/ZJ-Project/Project2022/FebruaryProject/CSK181_PAV/EXE/Video/" + "out" + i + ".mp4"))
        //    {
        //        //arguments = "-f gdigrab -framerate 15 -video_size 2560*1440 -i desktop -pix_fmt yuv420p -i audio=\"Realtek High Definition Audio\" E:/软件使用/ffmpeg/录屏视频/" + "out" + i + ".mp4";
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
        Debug.Log("录制中");
    }
    //结束录制
    void EndAudio()
    {
        try
        {
            proc.StandardInput.WriteLine("q");//在这个进程的控制台中模拟输入q,用于暂停录制
        }
        catch(Exception e)
        {
            Debug.Log("暂停失败:" + e);
        }

        //暂停两秒等待生成视频后关闭ffmpeg进程
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
                Debug.Log("关闭ffmpeg失败");
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
        Debug.Log("结束录制");


        if (DataManager.GetIns().SchemeVo.SubType != SubjectType.Single)
        {
            if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
            {
                string destPath = DataManager.GetIns().VideoPath + DataManager.GetIns().SynergyTraingDic[0][1].StudentID + "/" + DataManager.GetIns().TrainTime + ".mp4";
                //DataManager.GetIns().SynergyTraingDic[0]
                File.Copy(path, destPath, true);      //不是文件夹即复制文件，true表示可以覆盖同名文件
            }

        }

    }
    /// <summary>
    /// 取消录制
    /// </summary>
    void CancelAudio()
    {
        try
        {
            proc.StandardInput.WriteLine("q");//在这个进程的控制台中模拟输入q,用于暂停录制
        }
        catch
        {

        }

        //暂停两秒等待生成视频后关闭ffmpeg进程
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
        Debug.Log("取消录制");
        if (File.Exists(path))
        {
            // 删除文件
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

        Debug.Log("录制时间：" + _datetime);
        return _datetime;
    }

}
