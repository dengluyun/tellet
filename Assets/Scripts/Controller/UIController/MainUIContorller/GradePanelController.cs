using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GradePanelController : BaseView
{
    /// <summary>
    /// 主界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/GradePanel";

    /// <summary>
    /// 所有训练记录
    /// </summary>
    private List<TrainingRecordVo> RecordVos = new List<TrainingRecordVo>();
    /// <summary>
    /// 所有记录列表
    /// </summary>
    private List<GameObject> RecordLine = new List<GameObject>();

    /// <summary>
    /// 成绩列表单行
    /// </summary>
    private GameObject GeadeLine;

    /// <summary>
    /// 成绩列表单行的父类
    /// </summary>
    public Transform ContentParent;

    //右侧模块功能
    /// <summary>
    /// 添加成绩按钮
    /// </summary>
    public Button AddGradeBtn;


    //左侧模块功能
    /// <summary>
    /// 查询类型列表
    /// </summary>
    public Dropdown searchType;
    /// <summary>
    /// 年龄列表
    /// </summary>
    public Dropdown AdeSearch;
    /// <summary>
    /// 学员编号查询
    /// </summary>
    public InputField StudentIDInput;
    /// <summary>
    /// 学员名字查询
    /// </summary>
    public InputField StudentNameInput;
    /// <summary>
    /// 方案名查询
    /// </summary>
    public InputField ObjectName;

    private int SearchIndex = 0;

    private int LineCount = 0;

    private void Awake()
    {
        AddBtnOnClick();
        GeadeLine = Resources.Load<GameObject>("UIPanel/ShowLine/GradeLine");
        Init();
        InitSearchModule();
        InitEvent();
    }
    /// <summary>
    /// 添加点击事件
    /// </summary>
    private void AddBtnOnClick()
    {
        AddGradeBtn.onClick.AddListener(AddGradeBtnHandler);
    }

    public override void RefleshView()
    {
        SearchUserInput("");
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        RecordVos = DataManager.GetIns().GetTRecordDic();
        if(LineCount < RecordVos.Count)
        {
            for (int i = 0; i < RecordVos.Count - LineCount; i++)
            {
                GameObject g = Instantiate(GeadeLine, ContentParent);
                GradeLineController OLC = g.GetComponent<GradeLineController>();
                OLC.Init(RecordVos[i], i + 1);
                RecordLine.Add(g);
            }
        }
        LineCount = RecordVos.Count;
        
    }
    /// <summary>
    /// 初始化事件
    /// </summary>
    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.DELETETRAINRECORD_EVENT, UpdateStudentShow);
    }

    /// <summary>
    /// 初始化查询模块
    /// </summary>
    private void InitSearchModule()
    {
        List<string> strType = new List<string>() { "科目名称", "学员编号", "学员姓名", "年龄" };
        searchType.ClearOptions();
        searchType.AddOptions(strType);

        List<string> Ages = new List<string>();

        for (int i = 0; i < 38; i++)
        {
            Ages.Add((i + 18).ToString());
        }
        AdeSearch.ClearOptions();
        AdeSearch.AddOptions(Ages);

        ObjectName.gameObject.SetActive(true);
        StudentIDInput.gameObject.SetActive(false);
        StudentNameInput.gameObject.SetActive(false);
        AdeSearch.gameObject.SetActive(false);

        searchType.onValueChanged.AddListener(ChangeSearchType);
        StudentIDInput.onValueChanged.AddListener(SearchUserInput);
        StudentNameInput.onValueChanged.AddListener(SearchUserInput);
        AdeSearch.onValueChanged.AddListener(SearchUserInput);
        ObjectName.onValueChanged.AddListener(SearchUserInput);
    }

    /// <summary>
    /// 添加成绩
    /// </summary>
    private void AddGradeBtnHandler()
    {
        UIManager.GetIns().Show<AddGradeDataPanelController>(UIType.Show_View);
    }

    /// <summary>
    /// 当选择对应的选项时，切换检索方法
    /// </summary>
    /// <param name="Index"></param>
    private void ChangeSearchType(int Index)
    {
        switch (Index)
        {
            case 0:
                SearchIndex = 0;
                ObjectName.gameObject.SetActive(true);
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(false);
                ObjectName.text = "";
                SearchUserInput("");
                break;
            case 1:
                SearchIndex = 1;
                ObjectName.gameObject.SetActive(false);
                StudentIDInput.gameObject.SetActive(true);
                StudentNameInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(false);
                StudentIDInput.text = "";
                SearchUserInput("");
                break;
            case 2:
                SearchIndex = 2;
                ObjectName.gameObject.SetActive(false);
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(true);
                AdeSearch.gameObject.SetActive(false);
                StudentNameInput.text = "";
                SearchUserInput("");
                break;
            case 3:
                SearchIndex = 3;
                ObjectName.gameObject.SetActive(false);
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(true);
                SearchUserInput(AdeSearch.captionText.text);
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 查询用户输入的文本
    /// 匹对DataManager中所有的数据
    /// </summary>
    public void SearchUserInput(string txt)
    {
        List<TrainingRecordVo> lines = DataManager.GetIns().GetTRecordDic();
        List<TrainingRecordVo> Slins = new List<TrainingRecordVo>();
        switch (SearchIndex)
        {
            case 0:
                List<SchemeVo> schemelist = DataManager.GetIns().schemeDic.Values.ToList();
                schemelist = schemelist.FindAll((SchemeVo item) => { return item.ProjectName.Contains(txt); });
                for (int i = 0; i < schemelist.Count; i++)
                {
                    Slins.AddRange(lines.FindAll((TrainingRecordVo Item) => { return Item.SchemeID == schemelist[i].ID; }));
                }
                break;
            case 1:
                List<StudentVo> slineIDs = DataManager.GetIns().GetStudentList();
                slineIDs = slineIDs.FindAll((StudentVo item) =>{return item.StudentID.Contains(txt);});
                for (int i = 0; i < slineIDs.Count; i++)
                {
                    Slins.AddRange(lines.FindAll((TrainingRecordVo Item) => { return Item.StudentID == slineIDs[i].ID; }));
                }
                break;
            case 2:
                List<StudentVo> slineNames = DataManager.GetIns().GetStudentList();
                slineNames = slineNames.FindAll((StudentVo item) => { return item.StudentName.Contains(txt); });
                for (int i = 0; i < slineNames.Count; i++)
                {
                    Slins.AddRange(lines.FindAll((TrainingRecordVo Item) => { return Item.StudentID == slineNames[i].ID; }));
                }
                break;
            case 3:
                break;
            default:
                break;
        }
        if (txt == "")
        {
            UpdateSearchResult(lines);
        }
        else
        {
            UpdateSearchResult(Slins);
        }

    }

    /// <summary>
    /// 查询用户输入的文本
    /// 匹对DataManager中所有的数据
    /// </summary>
    public void SearchUserInput(int AgeIndex)
    {
        List<StudentVo> lines = DataManager.GetIns().GetStudentList();
        List<StudentVo> SLines = lines.FindAll(
                (StudentVo item) =>
                {
                    return item.Age == AgeIndex + 18;
                }
            );

        List<TrainingRecordVo> Lines = DataManager.GetIns().GetTRecordDic();
        List<TrainingRecordVo> Slins = new List<TrainingRecordVo>();

        for (int i = 0; i < SLines.Count; i++)
        {
            Slins.AddRange(Lines.FindAll((TrainingRecordVo Item) => { return Item.StudentID == SLines[i].ID; }));
        }

        UpdateSearchResult(Slins);
    }

    /// <summary>
    /// 刷新查询结果
    /// </summary>
    private void UpdateSearchResult(List<TrainingRecordVo> lines)
    {
        for (int i = 0; i < RecordLine.Count; i++)
        {
            if (i < lines.Count)
            {
                GradeLineController OLC = RecordLine[i].GetComponent<GradeLineController>();
                OLC.Init(lines[i], i + 1);
                RecordLine[i].SetActive(true);
                continue;
            }
            RecordLine[i].SetActive(false);
        }
    }

    /// <summary>
    /// 更新列表
    /// </summary>
    /// <param name="obj">false为删除 true为添加</param>
    private void UpdateStudentShow(object obj)
    {
        RecordVos = DataManager.GetIns().UpdateTRecordDic();
        if (!(bool)obj && RecordVos.Count != 0)
        {
            //RecordVos.Remove(RecordVos[RecordVos.Count - 1]);
            //RecordLine.RemoveAt(RecordLine.Count - 1);
        }
        else
        {
            GameObject g = Instantiate(GeadeLine, ContentParent);
            RecordLine.Add(g);
            g.SetActive(false);
        }

        UpdateSearchResult(RecordVos);
    }
}
