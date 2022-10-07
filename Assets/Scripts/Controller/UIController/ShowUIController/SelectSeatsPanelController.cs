using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectSeatsPanelController : BaseView
{
    /// <summary>
    /// 弹窗型界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/SelectSeatsPanel";

    /// <summary>
    /// 开始训练按钮
    /// </summary>
    public Button StartTrainBtn;
    /// <summary>
    /// 关闭按钮
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// 自动填入按钮
    /// </summary>
    public Button SeleteBtn;

    /// <summary>
    /// 所有需要训练的学员
    /// </summary>
    private List<StudentVo> TrainStu = new List<StudentVo>();

    /// <summary>
    /// 训练车辆
    /// </summary>
    private List<GameObject> AllCar = new List<GameObject>();

    /// <summary>
    /// 车位
    /// </summary>
    private GameObject SeleteCarLine;


    /// <summary>
    /// 车位的父类
    /// </summary>
    public Transform ContentParent;

    private void Awake()
    {
        SeleteCarLine = Resources.Load<GameObject>("UIPanel/ShowLine/CarSeatLine");
        InitBtnOnClick();
        InitEvent();
    }

    public override void RefleshView()
    {
        Init();
    }

    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.CANCELMASTERSTATIONCONTROL_EVENT, CancelControlEvent);
    }

    /// <summary>
    /// 初始化界面设置
    /// </summary>
    private void Init()
    {
        if (AllCar.Count > 0)
        {
            for (int i = 0; i < AllCar.Count; i++)
            {
                AllCar[i].GetComponent<CarSeatLineController>().ToDestroy();
            }
        }
        AllCar.Clear();
        TrainStu = DataManager.GetIns().trainStus;
        DataManager.GetIns().SynergyTraingDic.Clear();
        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            GameObject g = Instantiate(SeleteCarLine, ContentParent);
            AllCar.Add(g);
        }
        else if(DataManager.GetIns().SchemeVo.SubType == SubjectType.synergy)
        {
            if(DataManager.GetIns().SchemeVo.Subject == "多车协同训练")
            {
                for (int i = 0; i < TrainStu.Count; i++)
                {
                    GameObject g = Instantiate(SeleteCarLine, ContentParent);
                    AllCar.Add(g);
                    DataManager.GetIns().SynergyTraingDic[i + 1] = new List<StudentVo>();
                }
                return;
            }
            int count = TrainStu.Count / 2;
            for (int i = 0; i < count; i++)
            {
                GameObject g = Instantiate(SeleteCarLine, ContentParent);
                AllCar.Add(g);
                
                DataManager.GetIns().SynergyTraingDic[i + 1] = new List<StudentVo>();
            }
        }

    }

    private void InitBtnOnClick()
    {
        CloseBtn.onClick.AddListener(CloseHandler);
        StartTrainBtn.onClick.AddListener(StartTrainBtnHandler);
        SeleteBtn.onClick.AddListener(SeleteBtnHandler);
    }

    /// <summary>
    /// 自动填入按钮点击事件
    /// 将学员数据按循序依次填入席位中
    /// </summary>
    private void SeleteBtnHandler()
    {
        int a = 0;
        for (int i = 0; i < AllCar.Count; i++)
        {
            AllCar[i].GetComponent<CarSeatLineController>().InitSeleteData(a,a+1);
            a += 2;
        }
    }

    /// <summary>
    /// 开始训练
    /// </summary>
    private void StartTrainBtnHandler()
    {
        //Whatever.Nexus.Matching(true,Whatever.Nexus.driverName);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "目前不支持多车协同训练!");
        return;
        if (DataManager.GetIns().SchemeVo.Subject == "多车协同训练")
        {
            List<int> checkID = new List<int>();
            List<StudentVo> sID = new List<StudentVo>();
            for (int i = 0; i < AllCar.Count; i++)
            {
                if (!checkID.Contains(AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()))
                {
                    checkID.Add(AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete());
                    DataManager.GetIns().SynergyTraingDic[i + 1].Add(TrainStu[AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()]);
                    sID.Add(TrainStu[AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()]);
                }
                else
                {
                    UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
                    EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "不同车上有同一学员!");
                    return;
                }
            }
           
            string StudentID = "";
            for (int i = 0; i < sID.Count; i++)
            {
                StudentID += sID[i].ID;
                if( i < sID.Count - 1)
                {
                    StudentID += "|";
                }
            }

            PhotonEngine.GetIns().SendOperation(OperationCode.ControToC, new Dictionary<byte, object>()
            {
                {(byte)ParametersCode.ControToC,DataManager.GetIns().ControllerID + "-" + "StartTrain" + "-" + DataManager.GetIns().SchemeVo.ID + "-" + StudentID}
            });
            UIManager.GetIns().Show<LoadingViewController>();
            UIManager.GetIns().Close<CreattrainPanelController>();
            Close();
            UIManager.GetIns().Show<TrainingViewPanelController>();
        }
        //else
        //{
        //    return;
        //}

        //if (DataManager.GetIns().SchemeVo.SubType == SubjectType.synergy)
        //{
        //    List<int> checkID = new List<int>();
        // //   List<StudentVo> students = new List<StudentVo>();
        //    for (int i = 0; i < AllCar.Count; i++)
        //    {

        //        if (!checkID.Contains(AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()))
        //        {
        //            checkID.Add(AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete());
        //            DataManager.GetIns().SynergyTraingDic[i + 1].Add(TrainStu[AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()]);
        //     //       students.Add(TrainStu[AllCar[i].GetComponent<CarSeatLineController>().GetDriverSeatSelete()]);

        //        }
        //        else
        //        {
        //            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        //            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "不同车上有同一学员!");
        //            return;
        //        }

        //        if (!checkID.Contains(AllCar[i].GetComponent<CarSeatLineController>().GetStrikerSeatSelete()))
        //        {
        //            checkID.Add(AllCar[i].GetComponent<CarSeatLineController>().GetStrikerSeatSelete());
        //            DataManager.GetIns().SynergyTraingDic[i + 1].Add(TrainStu[AllCar[i].GetComponent<CarSeatLineController>().GetStrikerSeatSelete()]);
        //        }
        //        else
        //        {
        //            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        //            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "不同车上有同一学员!");
        //            return;
        //        }
        //    }
        //}
        else
        {
            DataManager.GetIns().SynergyTraingDic[0] = new List<StudentVo>();
            DataManager.GetIns().SynergyTraingDic[0].Add(TrainStu[AllCar[0].GetComponent<CarSeatLineController>().GetDriverSeatSelete()]);
            DataManager.GetIns().SynergyTraingDic[0].Add(TrainStu[AllCar[0].GetComponent<CarSeatLineController>().GetStrikerSeatSelete()]);

        }
        

        UIManager.GetIns().Show<LoadingViewController>();
        UIManager.GetIns().Close<CreattrainPanelController>();
        Close();
        UIManager.GetIns().Show<TrainingViewPanelController>();
    }


    /// <summary>
    /// 当主控制台取消某一导调台的控制权时
    /// </summary>
    /// <param name="o"></param>
    private void CancelControlEvent(object o)
    {
        AllCar.Remove(o as GameObject);
    }

    /// <summary>
    /// 关闭界面处理
    /// </summary>
    private void CloseHandler()
    {
        UIManager.GetIns().Show<StudentPanelController>(UIType.Show_View);
        Close();
    }
}
