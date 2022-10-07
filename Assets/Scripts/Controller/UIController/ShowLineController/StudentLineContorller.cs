using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StudentLineContorller : MonoBehaviour
{
    /// <summary>
    /// ѧԱ��Ϣ
    /// </summary>
    private StudentVo _StudentVo;

    /// <summary>
    /// ɾ����ť
    /// </summary>
    private Button DeleteBtn;

    /// <summary>
    /// �Ƿ�����ɾ��
    /// </summary>
    public bool IsDelete = false;

    private void Awake()
    {
        DeleteBtn = transform.GetChild(6).GetComponent<Button>();
        DeleteBtn.onClick.AddListener(DeleteObject);
        DeleteBtn.gameObject.SetActive(false);
        InitEvent();
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    /// <param name="vo"></param>
    public void Init(StudentVo vo, int Index)
    {
        if (vo == null)
        {
            return;
        }
        _StudentVo = vo;

        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Index.ToString();
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = _StudentVo.StudentID;
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = _StudentVo.StudentName;
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = _StudentVo.Unit;
        transform.GetChild(4).GetChild(0).GetComponent<Text>().text = _StudentVo.Age.ToString();
        transform.GetChild(5).GetChild(0).GetComponent<Text>().text = _StudentVo.Sex;
    }


    private void InitEvent()
    {
        EventManager.GetIns().AddEventlistener(EventConstant.SHOW_STUDENTDELETEBTN_EVENT, ShowDeleteBtn);
        EventManager.GetIns().AddEventlistener(EventConstant.CANCELDELETESTUDENT_EVENT, CancelDeleteObject);
    }

    /// <summary>
    /// �Ƿ�չ��ɾ����ť
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

    //ɾ������
    public void DeleteObject()
    {
        TippanelController tc = UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "�Ƿ�ȷ��ɾ��!");
        tc.OkBtnIsShow(1);
        IsDelete = true;
    }

    /// <summary>
    /// ȡ��ɾ��
    /// </summary>
    /// <param name="o"></param>
    private void CancelDeleteObject(object o = null)
    {
        IsDelete = false;
    }

    /// <summary>
    /// ɾ������
    /// </summary>
    public void DeleteStudentLineContorllerData()
    {
        if (IsDelete)
        {
            DBManager.Getins().editor<StudentVo>(SQLEditorType.update, _StudentVo, TableConst.STUDENTTABLE);
            EventManager.GetIns().ExcuteEvent(EventConstant.DELETESTUDENT_EVENT, false);
            EventManager.GetIns().RemoveEvent(EventConstant.SHOW_STUDENTDELETEBTN_EVENT, ShowDeleteBtn);
            EventManager.GetIns().RemoveEvent(EventConstant.CANCELDELETESTUDENT_EVENT, CancelDeleteObject);
            //Destroy(gameObject);
        }
    }

}
