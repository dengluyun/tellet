using System;
using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class AddGradeDataPanelController : BaseView
{
    /// <summary>
    /// ѵ����¼����
    /// </summary>
    TrainingRecordVo vo;
    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/AddGradeDataPanel";

    /// <summary>
    /// �رս��水ť
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// ��Ӽ�¼����
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
    /// ������ݵ���¼�
    /// </summary>
    private void AddBtnHandler()
    {
        vo = new TrainingRecordVo();
        string ReadStr = "";
        //�������ķ�����
        ReadStr = this.transform.GetChild(4).GetChild(0).GetChild(2).GetComponent<InputField>().text;
        if (!DataManager.GetIns().CheckObjectName(ReadStr))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ѡ����������!");
            return;
        }
        vo.SchemeID = DataManager.GetIns().FindObjectID(ReadStr);

        //��������ѧԱ���
        ReadStr = this.transform.GetChild(4).GetChild(1).GetChild(2).GetComponent<InputField>().text;
        if (ReadStr == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ��Ų���Ϊ��!");
            return;
        }
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ���Ϊ����!");
            return;
        }
        vo.StudentID = DataManager.GetIns().FindStudentID(ReadStr);
        if(vo.StudentID == 0)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ѡ��Ա��Ų�����!");
        }

        //��������ѧԱ����
        ReadStr = this.transform.GetChild(4).GetChild(2).GetChild(2).GetComponent<InputField>().text;
        if (ReadStr == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "ѧԱ��������Ϊ��!");
            return;
        }
        if (!DataManager.GetIns().CheckStudentName(ReadStr, vo.StudentID))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ѡ��Ա��������!");
            return;
        }

        //�������ĳɼ�
        ReadStr = this.transform.GetChild(4).GetChild(3).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "�ɼ�Ϊ����!");
            return;
        }
        if(Convert.ToInt32(ReadStr) > 100)
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "����ĳɼ�����ȶ�ֵ!");
            return;
        }
        vo.Grade = Convert.ToInt32(ReadStr);

        //�������ĺ�ʱ
        ReadStr = this.transform.GetChild(4).GetChild(4).GetChild(2).GetComponent<InputField>().text;
        if (GetTest(ReadStr) != "number")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "��ʱΪ����!");
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
