using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleManglePanelController : BaseView
{
    /// <summary>
    /// 主界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/PeopleManglePanel";

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
    private InputField StudentIDInput;
    /// <summary>
    /// 学员名字查询
    /// </summary>
    private InputField StudentNameInput;
    /// <summary>
    /// 学员单位查询
    /// </summary>
    private InputField StudentUnitInput;

    /// <summary>
    /// 所有学员
    /// </summary>
    private List<StudentVo> students = new List<StudentVo>();

    private List<GameObject> studentLine = new List<GameObject>();

    private GameObject StuLine;

    /// <summary>
    /// 科目行的父类
    /// </summary>
    public Transform ContentParent;

    /// <summary>
    /// 删除学员按钮
    /// </summary>
    public Button DeleteStudentBtn;
    /// <summary>
    /// 添加学员按钮
    /// </summary>
    public Button AddStudentBtn;
    /// <summary>
    /// 是否展现删除按钮
    /// </summary>
    private bool IsShowDeleteBtn = false;

    private int SearchIndex = 0;
    private void Awake()
    {
        students = DataManager.GetIns().GetStudentList();
        StuLine = Resources.Load<GameObject>("UIPanel/ShowLine/StudentLine");
        InitEvent();
    }

    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.DELETESTUDENT_EVENT, UpdateStudentShow);
    }

    // Start is called before the first frame update
    void Start()
    {
        Init();
        InitSearchModule();
        InitBtnOnClick();
    }

    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        for (int i = 0; i < students.Count; i++)
        {
            GameObject g = Instantiate(StuLine, ContentParent);
            StudentLineContorller OLC = g.GetComponent<StudentLineContorller>();
            OLC.Init(students[i], i + 1);
            studentLine.Add(g);
        }
        StudentIDInput = FindInputField("QueryModule/SoilderNumber");
        StudentNameInput = FindInputField("QueryModule/soliderName");
        StudentUnitInput = FindInputField("QueryModule/SoilderUnit");

    }
    /// <summary>
    /// 初始化点击事件
    /// </summary>
    private void InitBtnOnClick()
    {
        DeleteStudentBtn.onClick.AddListener(DeleteStudentHandler);
        AddStudentBtn.onClick.AddListener(AddStudentHandler);
    }
    /// <summary>
    /// 初始化查询模块
    /// </summary>
    private void InitSearchModule()
    {
        List<string> strType = new List<string>() { "学员编号", "学员姓名", "年龄", "学员单位" };
        searchType.ClearOptions();
        searchType.AddOptions(strType);

        List<string> Ages = new List<string>();

        for (int i = 0; i < 38; i++)
        {
            Ages.Add((i + 18).ToString());
        }
        AdeSearch.ClearOptions();
        AdeSearch.AddOptions(Ages);

        StudentIDInput.gameObject.SetActive(true);
        StudentNameInput.gameObject.SetActive(false);
        StudentUnitInput.gameObject.SetActive(false);
        AdeSearch.gameObject.SetActive(false);

        searchType.onValueChanged.AddListener(ChangeSearchType);
        StudentIDInput.onValueChanged.AddListener(SearchUserInput);
        StudentNameInput.onValueChanged.AddListener(SearchUserInput);
        StudentUnitInput.onValueChanged.AddListener(SearchUserInput);
        AdeSearch.onValueChanged.AddListener(SearchUserInput);
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
                StudentIDInput.gameObject.SetActive(true);
                StudentNameInput.gameObject.SetActive(false);
                StudentUnitInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(false);
                StudentIDInput.text = "";
                SearchUserInput("");
                break;
            case 1:
                SearchIndex = 1;
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(true);
                StudentUnitInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(false);
                StudentNameInput.text = "";
                SearchUserInput("");
                break;
            case 2:
                SearchIndex = 2;
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(false);
                StudentUnitInput.gameObject.SetActive(false);
                AdeSearch.gameObject.SetActive(true);
                SearchUserInput(AdeSearch.captionText.text);
                break;
            case 3:
                SearchIndex = 3;
                StudentIDInput.gameObject.SetActive(false);
                StudentNameInput.gameObject.SetActive(false);
                StudentUnitInput.gameObject.SetActive(true);
                AdeSearch.gameObject.SetActive(false);
                StudentUnitInput.text = "";
                SearchUserInput("");
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
        List<StudentVo> lines = DataManager.GetIns().GetStudentList();
        List<StudentVo> SLines = lines.FindAll(
                (StudentVo item) =>
                {
                    if(SearchIndex == 0)
                    {
                        return item.StudentID.Contains(txt);
                    }
                    else if (SearchIndex == 1)
                    {
                        return item.StudentName.Contains(txt);
                    }
                    else if(SearchIndex == 2)
                    {
                        return item.Age.ToString() == txt;
                    }
                    else if (SearchIndex == 3)
                    {
                        return item.Unit.ToString() == txt;
                    }
                    return false;
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
        UpdateSearchResult(SLines);
    }

    /// <summary>
    /// 刷新查询结果
    /// </summary>
    private void UpdateSearchResult(List<StudentVo> lines)
    {
        for (int i = 0; i < students.Count; i++)
        {
            studentLine[i].SetActive(true);
            if (i < lines.Count)
            {
                StudentLineContorller OLC = studentLine[i].GetComponent<StudentLineContorller>();
                OLC.Init(lines[i],i+1);
                continue;
            }
            studentLine[i].SetActive(false);
        }
    }

    /// <summary>
    /// 删除学员
    /// </summary>
    private void DeleteStudentHandler()
    {
        EventManager.GetIns().ExcuteEvent(EventConstant.SHOW_STUDENTDELETEBTN_EVENT);
        IsShowDeleteBtn = !IsShowDeleteBtn;
    }

    /// <summary>
    /// 添加学员
    /// </summary>
    private void AddStudentHandler()
    {
        UIManager.GetIns().Show<AddStudentDataPanelController>(UIType.Show_View);
    }


    /// <summary>
    /// 更新列表
    /// </summary>
    /// <param name="obj">false为删除 true为添加</param>
    private void UpdateStudentShow(object obj)
    {
        students = DataManager.GetIns().UpdateStudentDic();
        if (!(bool)obj && students.Count != 0)
        {
            //students.Remove(students[students.Count - 1]);
            //studentLine.RemoveAt(studentLine.Count - 1);
        }
        else
        {
            GameObject g = Instantiate(StuLine, ContentParent);
            g.transform.GetChild(6).GetComponent<Button>().gameObject.SetActive(IsShowDeleteBtn);
            studentLine.Add(g);
        }
        bool istrue = false;
        for (int i = 0; i < students.Count; i++)
        {
            studentLine[i].SetActive(true);
            if (i < students.Count)
            {
                StudentLineContorller OLC = studentLine[i].GetComponent<StudentLineContorller>();
                OLC.Init(students[i], i + 1);
                if (OLC.IsDelete)
                {
                    Destroy(studentLine[i]);
                    studentLine.Remove(studentLine[i]);
                    istrue = true;
                }
            }
        }
        if (!(bool)obj && !istrue)
        {
            Destroy(studentLine[studentLine.Count - 1]);
            studentLine.Remove(studentLine[studentLine.Count - 1]);
        }
    }
}
