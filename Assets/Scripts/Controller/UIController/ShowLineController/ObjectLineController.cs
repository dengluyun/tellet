using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectLineController : MonoBehaviour
{
    /// <summary>
    /// 训练科目
    /// </summary>
    public SchemeVo _Schemevo;

    /// <summary>
    /// 删除按钮
    /// </summary>
    private Button DeleteBtn;

    /// <summary>
    /// 选择框
    /// </summary>
    private Toggle ChooseTg;

    /// <summary>
    /// 是否允许删除
    /// </summary>
    public bool IsDelete = false;

    /// <summary>
    /// 是否允许更改
    /// </summary>
    public bool IsChange = false;

    private void Awake()
    {
        DeleteBtn = transform.GetChild(8).GetComponent<Button>();
        ChooseTg = transform.GetChild(7).GetComponent<Toggle>();
        DeleteBtn.onClick.AddListener(DeleteObject);
        DeleteBtn.gameObject.SetActive(false);
        ChooseTg.onValueChanged.AddListener(IsChoose);

        InitEvent();

    }

    /// <summary>
    /// 初始化
    /// </summary>
    /// <param name="vo"></param>
    public void Init(SchemeVo vo , int Index)
    {
        if(vo == null)
        {
            return;
        }
        _Schemevo = vo;
        
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Index.ToString();
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _Schemevo.ProjectName;
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = _Schemevo.Subject;
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = _Schemevo.SubTypeShow;
        transform.GetChild(4).GetChild(0).GetComponent<Text>().text = _Schemevo.Environment;
        transform.GetChild(5).GetChild(0).GetComponent<Text>().text = _Schemevo.Weather;
        transform.GetChild(6).GetChild(0).GetComponent<Text>().text = _Schemevo.DataTime;

    }

    public void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.SHOW_DELETEBTN_EVENT,ShowDeleteBtn);
        EventManager.GetIns().AddEventlistener(EventConstant.CANCELDELETEOBJECT_EVENT, CancelDeleteObject);
        EventManager.GetIns().AddEventlistener(EventConstant.CHOOSEONEOBJECT_EVENT, CancelChoose);
    }

    /// <summary>
    /// 是否选中
    /// </summary>
    /// <param name="arg0"></param>
    private void IsChoose(bool arg0)
    {
        if (arg0 && !IsChange)
        {
            EventManager.GetIns().ExcuteEvent(EventConstant.CHOOSEONEOBJECT_EVENT);
            ChooseTg.isOn = true;
            IsChange = true;
            DataManager.GetIns().SchemeVo = _Schemevo;
        }

        if(!arg0 && IsChange)
        {
            ChooseTg.isOn = false;
            IsChange = false;
        }
    }

    /// <summary>
    /// 取消选中
    /// </summary>
    /// <param name="o"></param>
    private void CancelChoose(object o = null)
    {
        if (IsChange)
        {
            ChooseTg.isOn = false;
            IsChange = false;
        }
    }

    /// <summary>
    /// 是否展现删除按钮
    /// </summary>
    /// <param name="o"></param>
    private void ShowDeleteBtn(object o = null)
    {
        if (DeleteBtn.gameObject.activeSelf)
        {
            DeleteBtn.gameObject.SetActive(false);
        }
        else
        {
            DeleteBtn.gameObject.SetActive(true);
        }
        
    }

    //删除方案
    public void DeleteObject()
    {
        TippanelController tc = UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "是否确认删除!");
        tc.OkBtnIsShow(0);
        IsDelete = true;
    }

    /// <summary>
    /// 取消删除
    /// </summary>
    /// <param name="o"></param>
    private void CancelDeleteObject(object o = null)
    {
        IsDelete = false;
    }


    /// <summary>
    /// 删除数据
    /// </summary>
    public void DeleteObjectLineControllerData()
    {
        if (IsDelete)
        {
            DBManager.Getins().editor<SchemeVo>(SQLEditorType.update, _Schemevo, TableConst.SCHEMETABLE);

            EventManager.GetIns().RemoveEvent(EventConstant.SHOW_DELETEBTN_EVENT, ShowDeleteBtn);
            EventManager.GetIns().RemoveEvent(EventConstant.CANCELDELETEOBJECT_EVENT, CancelDeleteObject);
            EventManager.GetIns().RemoveEvent(EventConstant.CHOOSEONEOBJECT_EVENT, CancelChoose);
            EventManager.GetIns().ExcuteEvent(EventConstant.DELETEOBJECT_EVENT,false);
            Debug.Log("删除方案");
        }

    }

    public void DeleteData()
    {
        EventManager.GetIns().RemoveEvent(EventConstant.SHOW_DELETEBTN_EVENT, ShowDeleteBtn);
        EventManager.GetIns().RemoveEvent(EventConstant.CANCELDELETEOBJECT_EVENT, CancelDeleteObject);
        EventManager.GetIns().RemoveEvent(EventConstant.CHOOSEONEOBJECT_EVENT, CancelChoose);
    }
}

