using Common;
using Mono.Data.Sqlite;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

/// <summary>
/// ����UI������
/// </summary>
public class CreattrainPanelController : BaseView
{
    /// <summary>
    /// ���������·��
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/CreattrainPanel";

    /// <summary>
    /// ���������水ť
    /// </summary>
    public Button ReturnToMainBtn;

    /// <summary>
    /// ɾ��������ť
    /// </summary>
    public Button DeleteProjectBtn;

    /// <summary>
    /// ��Ŀ��ʼ
    /// </summary>
    public Button TrainStartBtn;

    /// <summary>
    /// ��������
    /// </summary>
    public Button CreatProjectBtn;

    /// <summary>
    /// ɾ�����з���
    /// </summary>
    public Button CloseAllProjectBtn;

    /// <summary>
    /// ���з����б�
    /// </summary>
    private List<SchemeVo> schemes = new List<SchemeVo>();

    /// <summary>
    /// ���з�����ʾ��
    /// </summary>
    private List<GameObject> objs;

    /// <summary>
    /// ������ʾ��
    /// </summary>
    private GameObject ObjectLine;

    /// <summary>
    /// ��Ŀ�еĸ���
    /// </summary>
    public Transform ContentParent;

    private bool IsShowDeleteBtn = false;

    private void Awake()
    {
        ObjectLine = Resources.Load<GameObject>("UIPanel/ShowLine/ObjectLine");
        InitEvent();
        
    }

    public void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.DELETEOBJECT_EVENT, UpdateObjectShow);
        EventManager.GetIns().AddEventlistener(EventConstant.DELETE_ALL_PROJECT_EVENT, DeleteAllProjectHandler);
    }

    /// <summary>
    /// �����б�
    /// </summary>
    /// <param name="obj">falseΪɾ�� trueΪ���</param>
    private void UpdateObjectShow(object obj)
    {
        schemes = DataManager.GetIns().UpdateSchemeDic();
        if(!(bool)obj && objs.Count != 0)
        {
            //objs.Remove(objs[objs.Count - 1]);
        }
        else
        {
            GameObject g = Instantiate(ObjectLine, ContentParent);
            g.transform.GetChild(8).gameObject.SetActive(IsShowDeleteBtn);
            objs.Add(g);
        }

        bool istrue = false;

        for (int i = 0; i < objs.Count; i++)
        {
            if (i < schemes.Count)
            {
                ObjectLineController OLC = objs[i].GetComponent<ObjectLineController>();
                OLC.Init(schemes[i], i + 1);
                if (OLC.IsDelete)
                {
                    istrue = true;
                    Destroy(objs[i]);
                    objs.Remove(objs[i]);
                }
            }
            
        }

        if (!(bool)obj && !istrue)
        {
            Destroy(objs[objs.Count - 1]);
            objs.Remove(objs[objs.Count - 1]);
        }

        //DataManager.GetIns().Objects = objs;
    }

    private void Start()
    {
        schemes = DataManager.GetIns().GetSchemeList();
        AddBtnOnClick();
        Init();
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void Init()
    {
        objs = new List<GameObject>();
        for (int i = 0; i < schemes.Count; i++)
        {
            GameObject g = Instantiate(ObjectLine, ContentParent);
            ObjectLineController OLC = g.GetComponent<ObjectLineController>();
            OLC.Init(schemes[i],i+1);
            objs.Add(g);
        }
        //DataManager.GetIns().Objects = objs;
    }

    /// <summary>
    /// Ϊ��ť��ӵ���¼�
    /// </summary>
    private void AddBtnOnClick()
    {
        ReturnToMainBtn.onClick.AddListener(ReturnMainPanel);
        CreatProjectBtn.onClick.AddListener(OpenCreateObjectView);
        TrainStartBtn.onClick.AddListener(TrainStart);
        DeleteProjectBtn.onClick.AddListener(DeleteProject);
        CloseAllProjectBtn.onClick.AddListener(CloseAllProject);
        CloseAllProjectBtn.transform.DOScale(new Vector3(0, 0, 0), 0.1f);
        CloseAllProjectBtn.gameObject.SetActive(false);
    }

    /// <summary>
    /// �ص�������
    /// </summary>
    private void ReturnMainPanel()
    {
        UIManager.GetIns().Show<MainPanelController>();
        UIManager.GetIns().Show<NavPanelController>();
        this.Close();
    }

    /// <summary>
    /// �򿪴����������
    /// </summary>
    private void OpenCreateObjectView()
    {
        UIManager.GetIns().Show<CreatObjectPanelController>(UIType.Show_View);
    }

    /// <summary>
    /// ��Ŀ��ʼ
    /// </summary>
    private void TrainStart()
    {
        for (int i = 0; i < objs.Count; i++)
        {
            ObjectLineController OLC = objs[i].GetComponent<ObjectLineController>();
            if(OLC.IsChange == true)
            {
                DataManager.GetIns().SchemeVo = OLC._Schemevo;
                UIManager.GetIns().Show<StudentPanelController>(UIType.Show_View);
                if(DataManager.GetIns().SchemeVo.SubType == SubjectType.synergy)
                {
                    PhotonEngine.GetIns().SendOperation(OperationCode.ControToC, new Dictionary<byte, object>()
                    {
                        {(byte)ParametersCode.ControToC,DataManager.GetIns().ControllerID + "-" + "Control" }
                    });
                }
                //Close();
                return;
            }
        }
        UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT,"��ѡ��Ҫ��ʼ�Ŀ�Ŀ!");
    }

    /// <summary>
    /// չʾɾ����Ŀ��ť
    /// </summary>
    private void DeleteProject()
    {
        EventManager.GetIns().ExcuteEvent(EventConstant.SHOW_DELETEBTN_EVENT);
        IsShowDeleteBtn = !IsShowDeleteBtn;
        CloseAllProjectBtn.gameObject.SetActive(IsShowDeleteBtn);
        if (IsShowDeleteBtn)
        {
            CloseAllProjectBtn.transform.DOScale(new Vector3(1, 1, 1), 0.5f);
        }
        else
        {
            CloseAllProjectBtn.transform.DOScale(new Vector3(0, 0, 0), 0.5f);
        }
        
    }

    /// <summary>
    /// ɾ�����з�����ť�¼�
    /// </summary>
    private void CloseAllProject()
    {
        UIManager.GetIns().Show<TippanelController>(UIType.Show_View).OkBtnIsShow(3);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "�Ƿ�ѡ��ɾ�����з���!");
    }

    /// <summary>
    /// ���ȷ���� ������з���
    /// </summary>
    /// <param name="o"></param>
    private void DeleteAllProjectHandler(object o)
    {
        for (int i = 0; i < schemes.Count; i++)
        {
            DBManager.Getins().editor<SchemeVo>(SQLEditorType.update, schemes[i], TableConst.SCHEMETABLE);
            objs[i].GetComponent<ObjectLineController>().DeleteData();
            Destroy(objs[i]);
        }
        objs.Clear();
        DBManager.Getins().LoadPares<SchemeVo>(TableConst.SCHEMETABLE);
    }
}
