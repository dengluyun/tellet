using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GradeLineController : MonoBehaviour
{
    /// <summary>
    /// 训练记录
    /// </summary>
    public TrainingRecordVo RecordVo;

    /// <summary>
    /// 删除按钮
    /// </summary>
    private Button DeleteBtn;

    /// <summary>
    /// 修改按钮
    /// </summary>
    private Button UpdateBtn;

    /// <summary>
    /// 播放视频按钮
    /// </summary>
    private Button PlayBtn;

    /// <summary>
    /// 是否允许删除
    /// </summary>
    private bool IsDelete = false;


    private void Awake()
    {
        InitBtn();
    }

    /// <summary>
    /// 初始化
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
    /// 初始化--为展示行赋予数据
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
        transform.GetChild(3).GetChild(0).GetComponent<Text>().text = vo.TrainMoudle == 0?"驾驶训练":"射击训练";
        transform.GetChild(4).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().StudentDic[vo.StudentID].StudentID;
        transform.GetChild(5).GetChild(0).GetComponent<Text>().text = DataManager.GetIns().StudentDic[vo.StudentID].StudentName;
        transform.GetChild(6).GetChild(0).GetComponent<Text>().text = vo.Grade.ToString();
        transform.GetChild(7).GetChild(0).GetComponent<Text>().text = vo.TotalTime;
        transform.GetChild(8).GetChild(0).GetComponent<Text>().text = vo.Time.ToShortDateString();
    }


    /// <summary>
    ///修改数据
    /// </summary>
    private void UpdateBtnHandler()
    {
        UIManager.GetIns().Show<UpdateGradeDataPanelController>(UIType.Show_View);
        EventManager.GetIns().ExcuteEvent(EventConstant.UPDATESTUDENTGRADE_EVENT,RecordVo);
    }


    /// <summary>
    /// 删除数据
    /// </summary>
    private void DeleteBtnHandler()
    {
        TippanelController tc = UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
        tc.OkBtnIsShow(2);
        DataManager.GetIns().NeedToDeleteRecord = RecordVo;
        EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "是否确认删除!");
    }


    /// <summary>
    /// 播放录像
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
