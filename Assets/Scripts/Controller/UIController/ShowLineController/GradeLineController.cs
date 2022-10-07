using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeLineController : MonoBehaviour
{
    /// <summary>
    /// ѵ����¼
    /// </summary>
    public TrainingRecordVo RecordVo;

    /// <summary>
    /// ɾ����ť
    /// </summary>
    private Button DeleteBtn;

    /// <summary>
    /// �޸İ�ť
    /// </summary>
    private Button UpdateBtn;

    /// <summary>
    /// ������Ƶ��ť
    /// </summary>
    private Button PlayBtn;

    /// <summary>
    /// �Ƿ�����ɾ��
    /// </summary>
    private bool IsDelete = false;


    private void Awake()
    {
        InitBtn();
    }

    /// <summary>
    /// ��ʼ��
    /// </summary>
    private void InitBtn()
    {
        UpdateBtn = transform.GetChild(9).GetComponent<Button>();
        DeleteBtn = transform.GetChild(10).GetComponent<Button>();
        PlayBtn = transform.GetChild(11).GetComponent<Button>();
        UpdateBtn.onClick.AddListener(UpdateBtnHandler);
        DeleteBtn.onClick.AddListener(DeleteBtnHandler);
        PlayBtn.onClick.AddListener(PlayBtnHandler);
    }

    /// <summary>
    /// ��ʼ��--Ϊչʾ�и�������
    /// </summary>
    public void Init(TrainingRecordVo vo,int Index)
    {
        if(vo == null)
        {
            return;
        }
        RecordVo = vo;

        transform.GetChild(0).GetChild(0).GetComponent<Text>().text = Index.ToString();
        transform.GetChild(1).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().schemeDic[vo.SchemeID].ProjectName.ToString();
        transform.GetChild(2).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().schemeDic[vo.SchemeID].Subject.ToString();
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = vo.TrainMoudle == 0?"��ʻѵ��":"���ѵ��";
        transform.GetChild(4).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().StudentDic[vo.StudentID].StudentID;
        transform.GetChild(5).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().StudentDic[vo.StudentID].StudentName;
        transform.GetChild(6).GetChild(0).GetComponent<Text>().text = vo.Grade.ToString();
        transform.GetChild(7).GetChild(0).GetComponent<Text>().text = vo.TotalTime;
        transform.GetChild(8).GetChild(0).GetComponent<Text>().text = vo.Time.ToShortDateString();
    }


    /// <summary>
    ///�޸�����
    /// </summary>
    private void UpdateBtnHandler()
    {
        UIManager.GetIns().Show<UpdateGradeDataPanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.UPDATESTUDENTGRADE_EVENT,RecordVo);
    }


    /// <summary>
    /// ɾ������
    /// </summary>
    private void DeleteBtnHandler()
    {
        TippanelController tc = UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        tc.OkBtnIsShow(2);
        DataManager.GetIns().NeedToDeleteRecord = RecordVo;
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "�Ƿ�ȷ��ɾ��!");
    }


    /// <summary>
    /// ����¼��
    /// </summary>
    private void PlayBtnHandler()
    {
        //DataManager.GetIns().NeedLook = RecordVo.StudentID + @"\" + RecordVo.Time;
        DataManager.GetIns().NeedLook = DataManager.GetIns().StudentDic[RecordVo.StudentID].StudentID + "/" + GetNowdateTime() + ".mp4";

        UIManager.GetIns().Show<Video_PlayerController>(UIType.Show_View);
    }

    private string GetNowdateTime()
    {
        string _datetime = RecordVo.Time.ToString();

        _datetime = _datetime.Replace("/", "@");
        _datetime = _datetime.Replace(":", "#");
        _datetime = _datetime.Replace(" ", "!");
        return _datetime;
    }
}
