using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AddGradeDataPanelController : BaseView
{
    /// <summary>
    /// 训练记录数据
    /// </summary>
    TrainingRecordVo vo;
    /// <summary>
    /// 弹窗型界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/AddGradeDataPanel";

    /// <summary>
    /// 关闭界面按钮
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// 添加记录数据
    /// </summary>
    public Button AddBtn;

    public Toggle Drive;
    public Toggle Shoot;

    bool MoudleType = true;

    public void InitBtn()
    {
        CloseBtn.onClick.AddListener(Close);
        AddBtn.onClick.AddListener(AddBtnHandler);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitBtn();
        Drive.onValueChanged.AddListener(DriveHandler);
        Shoot.onValueChanged.AddListener(ShootHandler);
    }

    private void DriveHandler(bool Ischange)
    {
        if (Ischange)
        {
            MoudleType = Ischange;
        }
        
    }

    private void ShootHandler(bool Ischange)
    {
        if (Ischange)
        {
            MoudleType = !Ischange;
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        if(MoudleType)
        {
            Drive.isOn = true;
            Shoot.isOn = false;
        }
        else
        {
            Drive.isOn = false;
            Shoot.isOn = true;
        }
    }




    /// <summary>
    /// 添加数据点击事件
    /// </summary>
    private void AddBtnHandler()
    {
        vo = new TrainingRecordVo();
        string ReadStr = "";
        //检查输入的方案名
        ReadStr = this.transform.GetChild(4).GetChild(0).GetChild(2).GetComponent<InputField>().text;
        if (!DataManager.GetIns().CheckObjectName(ReadStr))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "所选方案不存在!");
            return;
        }
        vo.SchemeID = DataManager.GetIns().FindObjectID(ReadStr);

        //检查输入的学员编号
        ReadStr = this.transform.GetChild(4).GetChild(1).GetChild(2).GetComponent<InputField>().text;
        if (ReadStr == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员编号不能为空!");
            return;
        }
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员编号为数字!");
            return;
        }
        vo.StudentID = DataManager.GetIns().FindStudentID(ReadStr);
        if(vo.StudentID == 0)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "所选人员编号不存在!");
        }

        //检查输入的学员名称
        ReadStr = this.transform.GetChild(4).GetChild(2).GetChild(2).GetComponent<InputField>().text;
        if (ReadStr == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "学员姓名不能为空!");
            return;
        }
        if (!DataManager.GetIns().CheckStudentName(ReadStr, vo.StudentID))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "所选人员名不存在!");
            return;
        }

        //检查输入的成绩
        ReadStr = this.transform.GetChild(4).GetChild(3).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "成绩为数字!");
            return;
        }
        if(Convert.ToInt32(ReadStr) > 100)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "输入的成绩大过既定值!");
            return;
        }
        vo.Grade = Convert.ToInt32(ReadStr);

        //检查输入的耗时
        ReadStr = this.transform.GetChild(4).GetChild(4).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "耗时为数字!");
            return;
        }
        vo.TotalTime = ReadStr;

        ReadStr = this.transform.GetChild(4).GetChild(5).GetChild(1).GetChild(0).GetComponent<Text>().text;

        vo.Time = Convert.ToDateTime(ReadStr);
        vo.TrainMoudle = Drive.isOn ? 0 : 1;
        DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.insert, vo, TableConst.TRAININGRECORDTABLE);
        EventManager.GetIns().ExcuteEvent(EventConstant.DELETETRAINRECORD_EVENT, true);
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
}
