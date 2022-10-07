using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavPanelController : BaseView
{
    /// <summary>
    /// ���������·��
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/NavPanel";

    /// <summary>
    /// ѵ����Ŀ��ť
    /// </summary>
    private Button TrainingSubjectBtn;

    /// <summary>
    /// �ɼ���ť
    /// </summary>
    private Button GradeBtn;

    /// <summary>
    /// ��Ա����
    /// </summary>
    private Button PeopleMangerBtn;

    /// <summary>
    /// ����̬��
    /// </summary>
    private Button CarMangerBtn;

    /// <summary>
    /// �˳�
    /// </summary>
    private Button QuitBtn;

    /// <summary>
    /// ���ڿ������еײ���ť
    /// </summary>
    List<Button> Btns = new List<Button>();

    /// <summary>
    /// ��ť�±�
    /// </summary>
    private int NavIndex;
    private void Awake()
    {
        InitComponent();
    }

    private void Start()
    {
        InitOnClick();
        InitBtn();
    }

    private void InitComponent()
    {
        TrainingSubjectBtn = FindButton("BottomNavigation/TrainingSubject");
        GradeBtn = FindButton("BottomNavigation/Grade");
        PeopleMangerBtn = FindButton("BottomNavigation/PeopleManger");
        GradeBtn = FindButton("BottomNavigation/Grade");
        CarMangerBtn = FindButton("BottomNavigation/CarManger");
        QuitBtn = FindButton("BottomNavigation/Quit");

        Btns.Add(TrainingSubjectBtn);
        Btns.Add(GradeBtn);
        Btns.Add(PeopleMangerBtn);
        Btns.Add(CarMangerBtn);
        Btns.Add(QuitBtn);

    }

    /// <summary>
    /// ��ʼ����ť��Ϣ--��ɫ 
    /// </summary>
    private void InitBtn()
    {
        NavIndex = 0;
        ChangeBtnState(NavIndex);
    }

    /// <summary>
    /// ��ʼ����ť����¼�
    /// </summary>
    private void InitOnClick()
    {
        //�ײ�������
        AddBtnListener(TrainingSubjectBtn, TrainingSubjectBtnHandler);
        AddBtnListener(GradeBtn, GradeBtnHandler);
        AddBtnListener(PeopleMangerBtn, PeopleMangerBtnHandler);
        AddBtnListener(CarMangerBtn, CarMangerBtnHandler);
        AddBtnListener(QuitBtn, QuitBtnHandler);
    }

    /// <summary>
    /// ѵ����Ŀ����¼�
    /// </summary>
    private void TrainingSubjectBtnHandler()
    {
        if(NavIndex == 0)
        {
            return;
        }
        Debug.Log("ѵ����Ŀ����¼�");
        NavIndex = 0;
        ChangeBtnState(NavIndex);
    }
    /// <summary>
    /// �ɼ�����¼�
    /// </summary>
    private void GradeBtnHandler()
    {
        if (NavIndex == 1)
        {
            return;
        }
        Debug.Log("�ɼ�����¼�");
        NavIndex = 1;
        ChangeBtnState(NavIndex);
    }
    /// <summary>
    /// ��Ա�������¼�
    /// </summary>
    private void PeopleMangerBtnHandler()
    {
        if (NavIndex == 2)
        {
            return;
        }
        Debug.Log("��Ա�������¼�");
        NavIndex = 2;
        ChangeBtnState(NavIndex);
    }


    /// <summary>
    /// ����̬�Ƶ���¼�
    /// </summary>
    private void CarMangerBtnHandler()
    {
        
    }

    /// <summary>
    /// �˳�����¼�
    /// </summary>
    private void QuitBtnHandler()
    {
        if (NavIndex == 3)
        {
            return;
        }
        NavIndex = 3;
        ChangeBtnState(NavIndex);
        Debug.Log("�˳�����¼�");
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    /// <summary>
    /// �������ťʱ���İ�ť״̬
    /// </summary>
    private void ChangeBtnState(int Index)
    {
        for (int i = 0; i < Btns.Count; i++)
        {
            if(i == Index)
            {
                Btns[i].transform.localScale = new Vector3(1.1f,1.1f,1.1f);
                Btns[i].transform.GetChild(0).GetComponent<Image>().color = new Color(1f, 1f, 1f);
                continue;
            }
            Btns[i].transform.localScale = new Vector3(0.8f,0.8f,1.0f);
            Btns[i].transform.GetChild(0).GetComponent<Image>().color = new Color(0.7f, 0.7f, 0.7f);
        }

        switch (Index)
        {
            case 0:
                UIManager.GetIns().Show<MainPanelController>();
                UIManager.GetIns().Close<GradePanelController>();
                UIManager.GetIns().Close<PeopleManglePanelController>();
                break;
            case 1:
                UIManager.GetIns().Close<MainPanelController>();
                UIManager.GetIns().Show<GradePanelController>();
                UIManager.GetIns().Close<PeopleManglePanelController>();
                break;
            case 2:
                UIManager.GetIns().Close<MainPanelController>();
                UIManager.GetIns().Close<GradePanelController>();
                UIManager.GetIns().Show<PeopleManglePanelController>();
                break;
            default:
                break;
        }
    }
}
