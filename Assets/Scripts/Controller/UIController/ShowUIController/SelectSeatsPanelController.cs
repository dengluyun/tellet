using Common;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SelectSeatsPanelController : BaseView
{
    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/SelectSeatsPanel";

    /// <summary>
    /// ��ʼѵ����ť
    /// </summary>
    public Button StartTrainBtn;
    /// <summary>
    /// �رհ�ť
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// �Զ����밴ť
    /// </summary>
    public Button SeleteBtn;

    /// <summary>
    /// ������Ҫѵ����ѧԱ
    /// </summary>
    private List<StudentVo> TrainStu = new List<StudentVo>();

    /// <summary>
    /// ѵ������
    /// </summary>
    private List<GameObject> AllCar = new List<GameObject>();

    /// <summary>
    /// ��λ
    /// </summary>
    private GameObject SeleteCarLine;


    /// <summary>
    /// ��λ�ĸ���
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
    /// ��ʼ����������
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
            if(DataManager.GetIns().SchemeVo.Subject == "�೵Эͬѵ��")
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
    /// �Զ����밴ť����¼�
    /// ��ѧԱ���ݰ�ѭ����������ϯλ��
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
    /// ��ʼѵ��
    /// </summary>
    private void StartTrainBtnHandler()
    {
        //Whatever.Nexus.Matching(true,Whatever.Nexus.driverName);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "Ŀǰ��֧�ֶ೵Эͬѵ��!");
        return;
        if (DataManager.GetIns().SchemeVo.Subject == "�೵Эͬѵ��")
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
                    EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ͬ������ͬһѧԱ!");
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
        //            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ͬ������ͬһѧԱ!");
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
        //            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ͬ������ͬһѧԱ!");
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
    /// ��������̨ȡ��ĳһ����̨�Ŀ���Ȩʱ
    /// </summary>
    /// <param name="o"></param>
    private void CancelControlEvent(object o)
    {
        AllCar.Remove(o as GameObject);
    }

    /// <summary>
    /// �رս��洦��
    /// </summary>
    private void CloseHandler()
    {
        UIManager.GetIns().Show<StudentPanelController>(UIType.Show_View);
        Close();
    }
}
