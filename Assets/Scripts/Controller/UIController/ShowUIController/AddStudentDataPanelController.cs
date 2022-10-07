using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddStudentDataPanelController : BaseView
{
    /// <summary>
    /// ѧԱ����
    /// </summary>
    StudentVo vo;

    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/AddStudentDataPanel";

    private InputField StudentAge;
    private InputField StudentName;
    private InputField StudentUnit;
    private InputField StudentID;

    private Toggle manToggle;
    private Toggle womenToggle;
    private bool IsSex;

    // Start is called before the first frame update
    void Start()
    {
        Init();
    }


    private void AddBtnOnClick()
    {
        AddBtnListener("AddBtn", AddStudentBtnHandler);
        AddBtnListener("CloseBtn", CloseBtnHandler);
    }


    private void Init()
    {
        StudentID = FindInputField("user_num/InputField");
        StudentName = FindInputField("name/InputField");
        StudentAge = FindInputField("age/InputField");
        StudentUnit = FindInputField("unit/InputField");
        manToggle = FindToggle("Sex/man");
        womenToggle = FindToggle("Sex/women");
        AddBtnOnClick();
    }

    private void AddStudentBtnHandler()
    {
        if(StudentID.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ��Ų���Ϊ��!");
            return;
        }
        if (GetTest(StudentID.text) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ���Ϊ����!");
            return;
        }
        if (StudentName.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ��������Ϊ��!");
            return;
        }
        if (StudentUnit.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ��λ����Ϊ��!");
            return;
        }
        if (GetTest(StudentAge.text)  != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ���䲻��Ϊ��,����Ϊ18-55֮�������!");
            return;
        }
        if(StudentAge.text.Length >= 3)
        {
            if (Convert.ToInt32(StudentAge.text) > 100)
            {
                UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
                EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ�ɼ����ܳ���100!");
                return;
            }
            
        }
        if (DataManager.GetIns().CheckStudentID(StudentID.text))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ����ظ�!");
            return;
        }
        vo = new StudentVo();
        vo.StudentID = StudentID.text;
        vo.StudentName = StudentName.text;
        vo.Unit = StudentUnit.text;
        vo.Age = Convert.ToInt32(StudentAge.text);
        vo.Sex = manToggle.isOn ? "��" : "Ů";

        Debug.Log(vo.ToString());
       

        DBManager.Getins().editor<StudentVo>(SQLEditorType.insert, vo, TableConst.STUDENTTABLE);
        EventManager.GetIns().ExcuteEvent(EventConstant.DELETESTUDENT_EVENT, true);
        Close();
    }

    private void CloseBtnHandler()
    {
        StudentID.text = "";
        StudentName.text = "";
        StudentAge.text = "";
        StudentUnit.text = "";
        Close();
    }


    private string GetTest(string s)
    {
        if (Regex.Match(s, "^\\d+$").Success)
        {
            
            return "number";
        }
        else if (Regex.Match(s, "^[a-zA-Z]+$").Success)
        {
            return "string";
        }
        else
        {
            return "other";
        }
    }

    private void Update()
    {
        OnToggleClick();
        if (IsSex)
        {
            manToggle.isOn = false;
            womenToggle.isOn = true;
        }
        else
        {
            manToggle.isOn = true;
            womenToggle.isOn = false;
        }
    }

    /// <summary>
    /// ��ѡ��
    /// </summary>
    public void OnToggleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = EventSystem.current.currentSelectedGameObject;
            if (g != null)
            {
                if (g.name == "man")
                {
                    if (manToggle.isOn)
                    {
                        IsSex= true;
                    }
                    else
                    {
                        IsSex = false;
                    }
                }
                else if (g.name == "women")
                {
                    if (womenToggle.isOn == true)
                    {
                        IsSex = false;
                    }
                    else
                    {
                        IsSex = true;
                    }
                }
            }
        }

    }
}
