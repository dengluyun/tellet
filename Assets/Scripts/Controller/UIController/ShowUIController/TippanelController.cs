using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TippanelController : BaseView
{
    /// <summary>
    /// 弹窗型界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/tippanel";

    /// <summary>
    /// 处理事务的类别
    /// </summary>
    private int _Index = -1;

    /// <summary>
    /// 提示文本
    /// </summary>
    private Text HintTxt;

    /// <summary>
    /// 关闭按钮
    /// </summary>
    private Button CloseBtn;

    /// <summary>
    /// 确认按钮
    /// </summary>
    private Button OkBtn;

    public override void RefleshView()
    {
        MyData data = this.data as MyData;
        if (data != null)
        {
            if (data.ShowType == MyData.ShowTypeEnum.CONFIRM)
            {
                CloseBtn.gameObject.SetActive(true);
                OkBtn.gameObject.SetActive(true);
            }
            else if(data.ShowType == MyData.ShowTypeEnum.TIPS)
            {
                CloseBtn.gameObject.SetActive(false);
                OkBtn.gameObject.SetActive(true);
            }
            ShowHintTxt(data.Tips);
        }
        this.transform.SetAsLastSibling();
    }

    private void Awake()
    {
        Init();
        AddBtnOnClick();
    }
    /// <summary>
    /// 初始化
    /// </summary>
    private void Init()
    {
        HintTxt = this.transform.GetChild(1).GetComponent<Text>();
        CloseBtn = this.transform.GetChild(2).GetComponent<Button>();
        OkBtn = this.transform.GetChild(3).GetComponent<Button>();
        EventManager.GetIns().AddEventlistener(EventConstant.HINTTEXT_EVENT, ShowHintTxt);
    }

    /// <summary>
    /// 根据下标决定处理的数据
    /// </summary>
    /// <param name="Index">0:项目方案  1:学员数据   2:训练记录   3:所有方案</param>
    public void OkBtnIsShow(int Index)
    {
        OkBtn.gameObject.SetActive(true);
        _Index = Index;
    }


    /// <summary>
    /// 添加按钮点击事件
    /// </summary>
    private void AddBtnOnClick()
    {
        CloseBtn.onClick.AddListener(CloseView);
        OkBtn.onClick.AddListener(ExcuteOkEvent);
    }

    /// <summary>
    /// 展示信息
    /// </summary>
    /// <param name="obj"></param>
    private void ShowHintTxt(object obj)
    {
        HintTxt.text = obj as string;
    }

    private void CloseView()
    {
        MyData data = this.data as MyData;
        if (data != null)
        {
            if (data.Callback != null)
            {
                data.Callback(false);
            }
            this.Close();
            return;
        }

        switch (_Index)
        {
            case 0:
                EventManager.GetIns().ExcuteEvent(EventConstant.CANCELDELETEOBJECT_EVENT);
                break;
            case 1:
                EventManager.GetIns().ExcuteEvent(EventConstant.CANCELDELETESTUDENT_EVENT);
                break;
            case 2:
                DataManager.GetIns().NeedToDeleteRecord = null;
                break;
            case 3:
                
                break;
            default:
                break;
        }
        _Index = -1;
        OkBtn.gameObject.SetActive(true);
        this.Close();
    }

    /// <summary>
    /// 向主类反馈删除信息
    /// </summary>
    private void ExcuteOkEvent()
    {
        MyData data = this.data as MyData;
        if (data != null)
        {
            if (data.Callback != null)
            {
                data.Callback(true);
            }
            this.Close();
            return;
        }

        switch (_Index)
        {
            case 0:
                SendMessageUpwards("DeleteObjectData", SendMessageOptions.DontRequireReceiver);
                break;
            case 1:
                SendMessageUpwards("DeleteStudentData", SendMessageOptions.DontRequireReceiver);
                break;
            case 2:
                if(DataManager.GetIns().NeedToDeleteRecord != null)
                {
                    DBManager.Getins().editor<TrainingRecordVo>(SQLEditorType.delete, DataManager.GetIns().NeedToDeleteRecord, TableConst.TRAININGRECORDTABLE);
                    EventManager.GetIns().ExcuteEvent(EventConstant.DELETETRAINRECORD_EVENT, false);
                }
                break;
            case 3:
                EventManager.GetIns().ExcuteEvent(EventConstant.DELETE_ALL_PROJECT_EVENT);
                break;
            default:
                break;
        }
        _Index = -1;
        OkBtn.gameObject.SetActive(true);
        this.Close();
    }
}
