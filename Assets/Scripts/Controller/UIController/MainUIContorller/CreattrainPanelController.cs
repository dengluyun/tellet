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
/// 方案UI控制类
/// </summary>
public class CreattrainPanelController : BaseView
{
    /// <summary>
    /// 主界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/MainPanel/CreattrainPanel";

    /// <summary>
    /// 返回主界面按钮
    /// </summary>
    public Button ReturnToMainBtn;

    /// <summary>
    /// 删除方案按钮
    /// </summary>
    public Button DeleteProjectBtn;

    /// <summary>
    /// 科目开始
    /// </summary>
    public Button TrainStartBtn;

    /// <summary>
    /// 创建方案
    /// </summary>
    public Button CreatProjectBtn;

    /// <summary>
    /// 删除所有方案
    /// </summary>
    public Button CloseAllProjectBtn;

    /// <summary>
    /// 所有方案列表
    /// </summary>
    private List<SchemeVo> schemes = new List<SchemeVo>();

    /// <summary>
    /// 所有方案显示行
    /// </summary>
    private List<GameObject> objs;

    /// <summary>
    /// 方案显示行
    /// </summary>
    private GameObject ObjectLine;

    /// <summary>
    /// 科目行的父类
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
    /// 更新列表
    /// </summary>
    /// <param name="obj">false为删除 true为添加</param>
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
    /// 初始化
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
    /// 为按钮添加点击事件
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
    /// 回到主界面
    /// </summary>
    private void ReturnMainPanel()
    {
        UIManager.GetIns().Show<MainPanelController>();
        UIManager.GetIns().Show<NavPanelController>();
        this.Close();
    }

    /// <summary>
    /// 打开创建方案面板
    /// </summary>
    private void OpenCreateObjectView()
    {
        UIManager.GetIns().Show<CreatObjectPanelController>(UIType.Show_View);
    }

    /// <summary>
    /// 科目开始
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
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT,"请选择要开始的科目!");
    }

    /// <summary>
    /// 展示删除科目按钮
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
    /// 删除所有方案按钮事件
    /// </summary>
    private void CloseAllProject()
    {
        UIManager.GetIns().Show<TippanelController>(UIType.Show_View).OkBtnIsShow(3);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "是否选择删除所有方案!");
    }

    /// <summary>
    /// 点击确定后 清除所有方案
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
