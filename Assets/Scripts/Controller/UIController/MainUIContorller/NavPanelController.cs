using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class NavPanelController : BaseView
{
    /// <summary>
    /// 主界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/NavPanel";

    /// <summary>
    /// 训练科目按钮
    /// </summary>
    private Button TrainingSubjectBtn;

    /// <summary>
    /// 成绩按钮
    /// </summary>
    private Button GradeBtn;

    /// <summary>
    /// 人员管理
    /// </summary>
    private Button PeopleMangerBtn;

    /// <summary>
    /// 车辆态势
    /// </summary>
    private Button CarMangerBtn;

    /// <summary>
    /// 退出
    /// </summary>
    private Button QuitBtn;

    /// <summary>
    /// 用于控制所有底部按钮
    /// </summary>
    List<Button> Btns = new List<Button>();

    /// <summary>
    /// 按钮下标
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
    /// 初始化按钮信息--颜色 
    /// </summary>
    private void InitBtn()
    {
        NavIndex = 0;
        ChangeBtnState(NavIndex);
    }

    /// <summary>
    /// 初始化按钮点击事件
    /// </summary>
    private void InitOnClick()
    {
        //底部导航栏
        AddBtnListener(TrainingSubjectBtn, TrainingSubjectBtnHandler);
        AddBtnListener(GradeBtn, GradeBtnHandler);
        AddBtnListener(PeopleMangerBtn, PeopleMangerBtnHandler);
        AddBtnListener(CarMangerBtn, CarMangerBtnHandler);
        AddBtnListener(QuitBtn, QuitBtnHandler);
    }

    /// <summary>
    /// 训练科目点击事件
    /// </summary>
    private void TrainingSubjectBtnHandler()
    {
        if(NavIndex == 0)
        {
            return;
        }
        Debug.Log("训练科目点击事件");
        NavIndex = 0;
        ChangeBtnState(NavIndex);
    }
    /// <summary>
    /// 成绩点击事件
    /// </summary>
    private void GradeBtnHandler()
    {
        if (NavIndex == 1)
        {
            return;
        }
        Debug.Log("成绩点击事件");
        NavIndex = 1;
        ChangeBtnState(NavIndex);
    }
    /// <summary>
    /// 人员管理点击事件
    /// </summary>
    private void PeopleMangerBtnHandler()
    {
        if (NavIndex == 2)
        {
            return;
        }
        Debug.Log("人员管理点击事件");
        NavIndex = 2;
        ChangeBtnState(NavIndex);
    }


    /// <summary>
    /// 车俩态势点击事件
    /// </summary>
    private void CarMangerBtnHandler()
    {
        
    }

    /// <summary>
    /// 退出点击事件
    /// </summary>
    private void QuitBtnHandler()
    {
        if (NavIndex == 3)
        {
            return;
        }
        NavIndex = 3;
        ChangeBtnState(NavIndex);
        Debug.Log("退出点击事件");
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }

    /// <summary>
    /// 当点击按钮时更改按钮状态
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
