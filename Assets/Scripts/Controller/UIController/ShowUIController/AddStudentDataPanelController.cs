using System;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AddStudentDataPanelController : BaseView
{
    /// <summary>
    /// 学员数据
    /// </summary>
    StudentVo vo;

    /// <summary>
    /// 弹窗型界面加载路径
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
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员编号不能为空!");
            return;
        }
        if (GetTest(StudentID.text) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员编号为数字!");
            return;
        }
        if (StudentName.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员姓名不能为空!");
            return;
        }
        if (StudentUnit.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员单位不能为空!");
            return;
        }
        if (GetTest(StudentAge.text)  != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员年龄不能为空,年龄为18-55之间的整数!");
            return;
        }
        if(StudentAge.text.Length >= 3)
        {
            if (Convert.ToInt32(StudentAge.text) > 100)
            {
                UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
                EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员成绩不能超过100!");
                return;
            }
            
        }
        if (DataManager.GetIns().CheckStudentID(StudentID.text))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员编号重复!");
            return;
        }
        vo = new StudentVo();
        vo.StudentID = StudentID.text;
        vo.StudentName = StudentName.text;
        vo.Unit = StudentUnit.text;
        vo.Age = Convert.ToInt32(StudentAge.text);
        vo.Sex = manToggle.isOn ? "男" : "女";

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
    /// 单选框
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
