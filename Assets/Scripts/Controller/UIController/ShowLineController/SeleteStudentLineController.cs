using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SeleteStudentLineController : MonoBehaviour
{
    StudentVo _vo;

    public Toggle toggle;

    private int Index;

    public bool IsChange = false;

    private void Awake()
    {
        toggle = transform.GetChild(4).GetComponent<Toggle>();
        toggle.onValueChanged.AddListener(ValueChangeHandler);
        InitEvent();
    }
    public void Init(StudentVo studentVo,int index)
    {
        _vo = studentVo;
        SetStudentData();
        IsChange = false;
        toggle.isOn = false;
        Index = index;
    }

    public void UpdateInit(StudentVo studentVo)
    {
        _vo = studentVo;
        SetStudentData();
        if(DataManager.GetIns().trainStus.Contains(_vo))
        {
            IsChange = true;
            toggle.isOn = true;
        }
        else
        {
            IsChange = false;
            toggle.isOn = false;
        }
    }

    private void SetStudentData()
    {
        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = _vo.StudentID;
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _vo.StudentName;
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = _vo.Age.ToString();
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = _vo.Sex;
    }

    public void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.SINGLECHOOSESTUDENT_EVENT, CancelChoose);
    }

    // Start is called before the first frame update
    void Start()
    {
       
    }

    private void ValueChangeHandler(bool Change)
    {
        if (Change)
        {
            IsChange = true;
            EventManager.GetIns().ExcuteEvent(EventConstant.SELETESTUDENTTRAING_EVENT, Index);
        }
        else
        {
            IsChange = false;
            if (DataManager.GetIns().trainStus.Contains(_vo))
            {
                DataManager.GetIns().trainStus.Remove(_vo);
                EventManager.GetIns().ExcuteEvent(EventConstant.UNSELETESTUDENTTRAING_EVENT, Index);
            }
        }
        
        //if (Change && !IsChange)
        //{
        //    if (DataManager.GetIns().SchemeVo.SubType == SubjectType.Single)
        //    {
        //        EventManager.GetIns().ExcuteEvent(EventConstant.SINGLECHOOSESTUDENT_EVENT);
        //        toggle.isOn = true;
        //        IsChange = true;
        //        DataManager.GetIns().trainStus.Clear();
        //        DataManager.GetIns().trainStus.Add(_vo);
        //    }
            
        //}
    }

    public void OnDestroy()
    {
        EventManager.GetIns().RemoveEvent(EventConstant.SINGLECHOOSESTUDENT_EVENT, CancelChoose);
    }

    /// <summary>
    /// 取消选中
    /// </summary>
    /// <param name="o"></param>
    private void CancelChoose(object o = null)
    {
        if (IsChange)
        {
            toggle.isOn = false;
            IsChange = false;
        }
    }
}
