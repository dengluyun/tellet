using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PeopleManglePanelController : BaseView
{
    /// <summary>
    /// ���������·��
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/PeopleManglePanel";

    /// <summary>
    /// ��ѯ�����б�
    /// </summary>
    public Dropdown searchType;
    /// <summary>
    /// �����б�
    /// </summary>
    public Dropdown AdeSearch;
    /// <summary>
    /// ѧԱ��Ų�ѯ
    /// </summary>
    private InputField StudentIDInput;
    /// <summary>
    /// ѧԱ���ֲ�ѯ
    /// </summary>
    private InputField StudentNameInput;
    /// <summary>
    /// ѧԱ��λ��ѯ
    /// </summary>
    private InputField StudentUnitInput;

    /// <summary>
    /// ����ѧԱ
    /// </summary>
    private List<StudentVo> students = new List<StudentVo>();

    private List<GameObject> studentLine = new List<GameObject>();

    private GameObject StuLine;

    /// <summary>
    /// ��Ŀ�еĸ���
    /// </summary>
    public Transform ContentParent;

    /// <summary>
    /// ɾ��ѧԱ��ť
    /// </summary>
    public Button DeleteStudentBtn;
    /// <summary>
    /// ���ѧԱ��ť
    /// </summary>
    public Button AddStudentBtn;
    /// <summary>
    /// �Ƿ�չ��ɾ����ť
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
    /// ��ʼ��
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
    /// ��ʼ������¼�
    /// </summary>
    private void InitBtnOnClick()
    {
        DeleteStudentBtn.onClick.AddListener(DeleteStudentHandler);
        AddStudentBtn.onClick.AddListener(AddStudentHandler);
    }
    /// <summary>
    /// ��ʼ����ѯģ��
    /// </summary>
    private void InitSearchModule()
    {
        List<string> strType = new List<string>() { "ѧԱ���", "ѧԱ����", "����", "ѧԱ��λ" };
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
    /// ��ѡ���Ӧ��ѡ��ʱ���л���������
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
    /// ��ѯ�û�������ı�
    /// ƥ��DataManager�����е�����
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
    /// ��ѯ�û�������ı�
    /// ƥ��DataManager�����е�����
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
    /// ˢ�²�ѯ���
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
    /// ɾ��ѧԱ
    /// </summary>
    private void DeleteStudentHandler()
    {
        EventManager.GetIns().ExcuteEvent(EventConstant.SHOW_STUDENTDELETEBTN_EVENT);
        IsShowDeleteBtn = !IsShowDeleteBtn;
    }

    /// <summary>
    /// ���ѧԱ
    /// </summary>
    private void AddStudentHandler()
    {
        UIManager.GetIns().Show<AddStudentDataPanelController>(UIType.Show_View);
    }


    /// <summary>
    /// �����б�
    /// </summary>
    /// <param name="obj">falseΪɾ�� trueΪ���</param>
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
