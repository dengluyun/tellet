using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class UpdateGradeDataPanelController : BaseView
{
    /// <summary>
    /// ѵ����¼����
    /// </summary>
    TrainingRecordVo vo;
    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/UpdateGradeDataPanel";

    /// <summary>
    /// �رս��水ť
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// �޸ļ�¼����
    /// </summary>
    public Button UpdateBtn;


    private void Awake()
    {
        Init();
    }
    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void Init()
    {
        InitBtnOnclick();
        InitEvent();
    }
    /// <summary>
    /// ��ʼ����ť����¼�
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
    /// ��Ҫ�޸ĵĳɼ�
    /// </summary>
    /// <param name="obj"></param>
    private void NeedToUpdateRecord(object obj)
    {
        vo = obj as TrainingRecordVo;
    }

    private void UpdateBtnHandler()
    {
        //�������ĳɼ�
        string ReadStr = this.transform.GetChild(4).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "����")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "�ɼ�Ϊ����!");
            return;
        }
        if (Convert.ToInt32(ReadStr) > 100)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "����ĳɼ�����ȶ�ֵ!");
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

            return "����";
        }
        else if (Regex.Match(s, "^[a-zA-Z]+$").Success)
        {
            return "�ַ�";
        }
        else
        {
            return "�������";
        }
    }
}
