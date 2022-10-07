using Common;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentPanelController : BaseView
{
    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/StudentPanel";

    /// <summary>
    /// ���뱾��ѵ����ѧԱ
    /// </summary>
    List<StudentVo> TrainingStu = new List<StudentVo>();

    /// <summary>
    /// ��ʼѵ����ť
    /// </summary>
    public Button StartTrainBtn;
    /// <summary>
    /// �رհ�ť
    /// </summary>
    public Button CloseBtn;

    private SubjectType TrainingType;

    private GameObject SeleteStuLine;

    public InputField SearchOne;

    /// <summary>
    /// ����ѧԱ
    /// </summary>
    private List<StudentVo> students = new List<StudentVo>();
    /// <summary>
    /// ����ѧԱ�б�
    /// </summary>
    private List<GameObject> studentLine = new List<GameObject>();

    private List<Toggle> toggles = new List<Toggle>();
    /// <summary>
    /// ��Ŀ�еĸ���
    /// </summary>
    public Transform ContentParent;

    /// <summary>
    /// ����Эͬģʽ�µ�ѵ��ѧԱ�б�
    /// </summary>
    private List<int> OnesynergyS = new List<int>();

    private int Index = 0;

    public override void RefleshView()
    {
        TrainingStu.Clear();
        OnesynergyS.Clear();
        students = DataManager.GetIns().GetStudentList();
        TrainingType = DataManager.GetIns().SchemeVo.SubType;
        DataManager.GetIns().trainStus.Clear();
        for (int i = 0; i < studentLine.Count; i++)
        {
            Destroy(studentLine[i]);
        }

        studentLine.Clear();
        toggles.Clear();

        for (int i = 0; i < students.Count; i++)
        {
            GameObject g = Instantiate(SeleteStuLine, ContentParent);
            SeleteStudentLineController OLC = g.GetComponent<SeleteStudentLineController>();
            OLC.Init(students[i], i + 1);
            toggles.Add(OLC.toggle);
            studentLine.Add(g);
        }



        if (TrainingType == SubjectType.Single)
        {
            StartTrainBtn.transform.GetChild(0).GetComponent<Text>().text = "��ʼѵ��";
        }
        else
        {
            StartTrainBtn.transform.GetChild(0).GetComponent<Text>().text = "ѡ��ϯλ";
        }
    }

    private void Awake()
    {
        AddBtnOnClick();
        SeleteStuLine = Resources.Load<GameObject>("UIPanel/ShowLine/SeleteStudentLine");
        //Init();
        InitEvent();
    }

    /// <summary>
    /// ��ӵ���¼�
    /// </summary>
    private void AddBtnOnClick()
    {
        SearchOne.onValueChanged.AddListener(SearchUserInput);
        StartTrainBtn.onClick.AddListener(StartTrainingHandler);
        CloseBtn.onClick.AddListener(CloseBtnHandler);
    }

    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.SELETESTUDENTTRAING_EVENT, SeleteStudentToTraing);
        EventManager.GetIns().AddEventlistener(EventConstant.UNSELETESTUDENTTRAING_EVENT, UnSeleteStudentToTraing);
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void Init()
    {
        students = DataManager.GetIns().GetStudentList();
        Index = studentLine.Count;
        for (int i = 0; i < students.Count; i++)
        {
            GameObject g = Instantiate(SeleteStuLine, ContentParent);
            SeleteStudentLineController OLC = g.GetComponent<SeleteStudentLineController>();
            OLC.Init(students[i], i + 1);
            toggles.Add(OLC.toggle);
            studentLine.Add(g);
        }
    }

    /// <summary>
    /// ��ѡ��ѧԱʱ�����������±�
    /// </summary>
    /// <param name="o"></param>
    private void SeleteStudentToTraing(object o)
    {
        int Index = (int)o;
        if (TrainingType == SubjectType.Single)
        {
            SingleHandler(Index);
        }
        else if (TrainingType == SubjectType.onesynergy)
        {
            OnesynergyHandler(Index);
        }
        else if (TrainingType == SubjectType.synergy)
        {
            SynergyHandler(Index);
        }
    }

    /// <summary>
    /// ��ȡ��ѡ��ѧԱʱ�����������±�
    /// </summary>
    /// <param name="o"></param>
    private void UnSeleteStudentToTraing(object o)
    {
        int Index = (int)o;
        if (TrainingType == SubjectType.onesynergy)
        {
            if (OnesynergyS.Contains(Index))
            {
                OnesynergyS.Remove(Index);
            }
        }
    }

    /// <summary>
    /// ����ѵ���µ�ѧԱѡ����
    /// </summary>
    /// <param name="Index"></param>
    private void SingleHandler(int Index)
    {
        for (int i = 0; i < toggles.Count; i++)
        {
            if (Index == i + 1)
            {
                toggles[i].isOn = true;
            }
            else
            {
                toggles[i].isOn = false;
            }
        }
        DataManager.GetIns().trainStus.Clear();
        DataManager.GetIns().trainStus.Add(students[Index - 1]);
    }
    /// <summary>
    /// ����Эͬѵ���µ�ѧԱѡ����
    /// </summary>
    /// <param name="Index"></param>
    private void OnesynergyHandler(int Index)
    {
        if (OnesynergyS.Count < 2)
        {
            if (OnesynergyS.Count == 0)
            {
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (Index == i + 1)
                    {
                        toggles[i].isOn = true;
                    }
                    else
                    {
                        toggles[i].isOn = false;
                    }
                }
                OnesynergyS.Add(Index);
                DataManager.GetIns().trainStus.Clear();
                DataManager.GetIns().trainStus.Add(students[Index - 1]);
            }
            else
            {
                for (int i = 0; i < toggles.Count; i++)
                {
                    if (Index == i + 1 || i + 1 == OnesynergyS[0])
                    {
                        toggles[i].isOn = true;
                    }
                    else
                    {
                        toggles[i].isOn = false;
                    }
                }
                OnesynergyS.Add(Index);
                DataManager.GetIns().trainStus.Add(students[Index - 1]);
            }
        }
        else
        {
            //OnesynergyS[0] = OnesynergyS[1];
            //DataManager.GetIns().trainStus[0] = DataManager.GetIns().trainStus[1];
            for (int i = 0; i < OnesynergyS.Count; i++)
            {
                if (i < OnesynergyS.Count - 1)
                {
                    OnesynergyS[i] = OnesynergyS[i + 1];
                    DataManager.GetIns().trainStus[i] = DataManager.GetIns().trainStus[i + 1];
                }
            }
            for (int i = 0; i < toggles.Count; i++)
            {
                if (Index == i + 1 || i + 1 == OnesynergyS[0])
                {
                    toggles[i].isOn = true;
                }
                else
                {
                    toggles[i].isOn = false;
                }
            }

            OnesynergyS[1] = Index;

            DataManager.GetIns().trainStus[1] = students[Index - 1];
        }
    }

    /// <summary>
    /// Эͬѵ���µ�ѧԱѡ����
    /// </summary>
    /// <param name="Index"></param>
    private void SynergyHandler(int Index)
    {
        OnesynergyS.Add(Index);
        DataManager.GetIns().trainStus.Add(students[Index - 1]);
    }



    /// <summary>
    /// ��ʼѵ������¼�
    /// </summary>
    private void StartTrainingHandler()
    {
        int count = 0;//������
        if (TrainingType == SubjectType.Single)
        {
            for (int i = 0; i < studentLine.Count; i++)
            {
                SeleteStudentLineController OLC = studentLine[i].GetComponent<SeleteStudentLineController>();
                if (OLC.IsChange == true)
                {
                    CheckStrudentInfo();
                    UIManager.GetIns().Close<CreattrainPanelController>();
                    UIManager.GetIns().Show<LoadingViewController>();
                    Close();
                    UIManager.GetIns().Show<TrainingViewPanelController>();
                    return;
                }
            }
        }
        else if (TrainingType == SubjectType.onesynergy)
        {
            for (int i = 0; i < studentLine.Count; i++)
            {
                SeleteStudentLineController OLC = studentLine[i].GetComponent<SeleteStudentLineController>();
                if (OLC.IsChange == true)
                {
                    count++;
                }
            }
            if (count == 2)
            {
                Close();
                UIManager.GetIns().Show<SelectSeatsPanelController>(UIType.Show_View);
                return;
            }
        }
        else if (TrainingType == SubjectType.synergy)
        {
            for (int i = 0; i < studentLine.Count; i++)
            {
                SeleteStudentLineController OLC = studentLine[i].GetComponent<SeleteStudentLineController>();
                if (OLC.IsChange == true)
                {
                    count++;
                }
            }
            if (count % 2 != 1)
            {
                Close();
                UIManager.GetIns().Show<SelectSeatsPanelController>(UIType.Show_View);
                return;
            }
            else
            {
                UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
                EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѵ����ѧԱ������ƥ��!");
                return;
            }
        }

        UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ѡ��Ҫ��ʼѵ����ѧԱ!");
    }
    /// <summary>
    /// �رս�������-���ѡ��
    /// </summary>
    private void CloseBtnHandler()
    {
        if(TrainingType == SubjectType.synergy)
        {
            PhotonEngine.GetIns().SendOperation(OperationCode.ControToC, new Dictionary<byte, object>()
            {
                {(byte)ParametersCode.ControToC,DataManager.GetIns().ControllerID + "-" + "UnControl" }
            });
        }

        for (int i = 0; i < studentLine.Count; i++)
        {
            SeleteStudentLineController OLC = studentLine[i].GetComponent<SeleteStudentLineController>();
            if (OLC.IsChange == true)
            {
                OLC.IsChange = false;
                OLC.toggle.isOn = false;
            }
        }

        Close();
    }

    /// <summary>
    /// ��齫Ҫѵ����ѧԱ��Ϣ
    /// </summary>
    private void CheckStrudentInfo()
    {

    }


    /// <summary>
    /// ��ѯ�û�������ı�
    /// ƥ��DataManager�����е�����
    /// </summary>
    public void SearchUserInput(string txt)
    {
        List<StudentVo> lines = DataManager.GetIns().GetStudentList();
        List<StudentVo> SLines = lines.FindAll(
                (StudentVo item) =>
                {
                    return item.StudentName.Contains(txt) || item.StudentID.Contains(txt);
                }
            );
        if (txt == "")
        {
            UpdateSearchResult(lines);
        }
        else
        {
            UpdateSearchResult(SLines);
        }

    }

    /// <summary>
    /// ˢ�²�ѯ���
    /// </summary>
    private void UpdateSearchResult(List<StudentVo> lines)
    {
        for (int i = 0; i < students.Count; i++)
        {
            studentLine[i].SetActive(true);
            if (i < lines.Count)
            {
                SeleteStudentLineController OLC = studentLine[i].GetComponent<SeleteStudentLineController>();
                OLC.UpdateInit(lines[i]);
                continue;
            }
            studentLine[i].SetActive(false);
        }
    }
}
