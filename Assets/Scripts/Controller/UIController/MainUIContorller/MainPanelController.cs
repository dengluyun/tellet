using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG;
using System;
using Common;

/// <summary>
/// ��ҳ�洦����
/// </summary>
public class MainPanelController : BaseView
{
    /// <summary>
    /// ���������·��
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/MainPanel";

    private Transform ToggleContent;

    private Transform Content;

    public MsgView msgView;
    public DriverView driverView;

    public ShootView shootView;
    public SynergyView synergyView;
    public HelpView helpView;


    private void Awake()
    {
        InitComponent();
        LocalizationManager.GetIns().Init();
    }

    IEnumerator Start()
    {
        InitView();
        InitOnClick();
        yield return new WaitForEndOfFrame();
        RefreshDriverView(true);
    }

    private void InitView()
    {
        //msgView = Content.gameObject.AddComponent<MsgView>();
        //driverView = driverView??Content.gameObject.AddComponent<DriverView>();
        //shootView = Content.gameObject.AddComponent<ShootView>();
        //synergyView = Content.gameObject.AddComponent<SynergyView>();

        //msgView = new MsgView(Content);
        //driverView = new DriverView(Content);
        //shootView = new ShootView(Content);
        //synergyView = new SynergyView(Content);
    }

    private void InitComponent()
    {
        ToggleContent = FindTransform("CenterContent/Training subject_Window/startPanel/ToggleContent");
        Content = FindTransform("CenterContent/Training subject_Window/startPanel/content");
        Debug.Log("��ʼ��");

    }
    /// <summary>
    /// ��ȡ���а�ť
    /// </summary>
    private void InitOnClick()
    {
        //��������
        AddBtnListener("CenterContent/Training subject_Window/startPanel/Create_Button", CreateOneScheme);
        // �Ǳ�λ
        AddBtnListener("CenterContent/Training subject_Window/startPanel/Button", ReSetBtnHandler);
        // ��ʼѵ��
        AddBtnListener("CenterContent/Training subject_Window/startPanel/StartTrain", StartTrain);
        for (int i = 0; i < ToggleContent.childCount; i++)
        {
            var trans = ToggleContent.GetChild(i);
            Toggle toggle = FindToggle(trans, "");
            toggle.onValueChanged.AddListener((isOn)=> 
            {
                if (isOn)
                {
                    var siblingIndex =  toggle.transform.GetSiblingIndex();
                    CourseChanged(siblingIndex);
                }
            });
        }
    }

    /// <summary>
    /// 0 ����ͨ��ָ��
    /// 1 �����ʻѵ��
    /// 2 �������ѵ��
    /// 3 ��������Эͬ
    /// 4 �����˳�ѵ��
    /// </summary>
    /// <param name="index"></param>
    private void CourseChanged(int index)
    {
        Debug.Log("�±�" + index);
        //����˳� ��������ȷ�Ͽ�
        if (index == 5)
        {
            //TODO
            MyData myData = new MyData();
            myData.Callback += QiutSystem;
            myData.Tips = "�Ƿ��˳�ѵ��";
            myData.ShowType = MyData.ShowTypeEnum.CONFIRM;
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View, myData);
        }
        else
        {
            //ˢ�½���
            RefreshView(index);
        }
    }

    private void QiutSystem(bool value)
    {
        if (value)
        {
            Debug.Log("�˳�ѵ��");
        }
    }

    private void RefreshView(int index)
    {
        RefreshMsgView(index == 0);
        RefreshDriverView(index == 1);
        RefreshShootView(index == 2);
        RefreshSynergyView(index == 3);
        RefreshHelpView(index == 4);
    }

    private void RefreshMsgView(bool value)
    {
        Debug.Log("RefreshMsgView" + value);
        msgView.RefreshView(value);
    }

    private void RefreshShootView(bool value)
    {
        Debug.Log("RefreshShootView" + value);
        shootView.RefreshView(value);
    }

    private void RefreshDriverView(bool value)
    {
        Debug.Log("RefreshDriverView" + value);
        driverView.RefreshView(value);
    }

    private void RefreshSynergyView(bool value)
    {
        Debug.Log("RefreshSynergyView" + value);
        synergyView.RefreshView(value);
    }

    private void RefreshHelpView(bool value)
    {
        Debug.Log("RefreshHelpView" + value);
        helpView.RefreshView(value);
    }

    private void StartTrain()
    {
        //TODO 
    }

    /// <summary>
    /// ������������¼�
    /// </summary>
    private void CreateOneScheme()
    {
        UIManager.GetIns().Close<NavPanelController>();
        UIManager.GetIns().Show<CreattrainPanelController>();
        this.Close();
    }

    /// <summary>
    /// �ָ��Ǳ�����λ��
    /// </summary>
    private void ReSetBtnHandler()
    {
        PhotonEngine.GetIns().SendOperation(OperationCode.PAVControl, new Dictionary<byte, object>()
            {
                 {(byte)ParametersCode.PAVReSet, DataManager.GetIns().ControllerID }
            });
    }
}
