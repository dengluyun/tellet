using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGradeDataPanelController : BaseView
{
    /// <summary>
    /// 训练记录数据
    /// </summary>
    TrainingRecordVo vo;
    /// <summary>
    /// 弹窗型界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/UpdateGradeDataPanel";

    /// <summary>
    /// 关闭界面按钮
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// 修改记录数据
    /// </summary>
    public Button UpdateBtn;


    private void Awake()
    {
        Init();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        InitBtnOnclick();
        InitEvent();
    }
    /// <summary>
    /// 初始化按钮点击事件
    /// </summary>
    private void InitBtnOnclick()
    {
        CloseBtn.onClick.AddListener(Close);
        UpdateBtn.onClick.AddListener(UpdateBtnHandler);
    }

    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.UPDATESTUDENTGRADE_EVENT, NeedToUpdateRecord);
    }

    /// <summary>
    /// 需要修改的成绩
    /// </summary>
    /// <param name="obj"></param>
    private void NeedToUpdateRecord(object obj)
    {
        vo = obj as TrainingRecordVo;
    }

    private void UpdateBtnHandler()
    {
        //检查输入的成绩
        string ReadStr = this.transform.GetChild(4).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "数字")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "成绩为数字!");
            return;
        }
        if (Convert.ToInt32(ReadStr) > 100)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "输入的成绩大过既定值!");
            return;
        }
        vo.Grade = Convert.ToInt32(ReadStr);
        DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.update, vo, TableConst.TRAININGRECORDTABLE);
        EventManager.GetIns().ExcuteEvent(EventConstant.DELETETRAINRECORD_EVENT, true);
        this.transform.GetChild(4).GetChild(2).GetComponent<InputField>().text = "";
        Close();
    }

    private string GetTest(string s)
    {
        if (Regex.Match(s, "^\\d+$").Success)
        {

            return "数字";
        }
        else if (Regex.Match(s, "^[a-zA-Z]+$").Success)
        {
            return "字符";
        }
        else
        {
            return "其他结果";
        }
    }
}
