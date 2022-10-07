using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatObjectPanelController : BaseView
{
    /// <summary>
    /// 弹窗型界面加载路径
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/CreatPanel";
    /// <summary>
    /// 关闭界面按钮
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// 创建方案按钮
    /// </summary>
    public Button CreateBtn;

    /// <summary>
    /// 方案名称
    /// </summary>
    public InputField ProjectName;

    /// <summary>
    /// 训练类型-单项训练
    /// </summary>
    public Toggle _drill;

    /// <summary>
    /// 训练类型-单车协同训练协同训练
    /// </summary>
    public Toggle _OneSynergyTrain;

    /// <summary>
    /// 训练类型-协同训练
    /// </summary>
    public Toggle _SynergyTrain;

    /// <summary>
    /// 判断训练类型
    /// </summary>
    private SubjectType IsTrainType =  SubjectType.Single;

    /// <summary>
    /// 列表-下拉列表框
    /// </summary>
    public List<Dropdown> dropdowns = new List<Dropdown>();


    private void Awake()
    {
        InitBtnOnClick();
        InitDropdownSeleteEvent();
        
    }

    public override void RefleshView()
    {
        IsTrainType = SubjectType.Single;
        for (int i = 0; i < dropdowns.Count; i++)
        {
            dropdowns[i].ClearOptions();
        }
        addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
        addOptions(dropdowns[1], DataManager.GetIns().weather);
        addOptions(dropdowns[2], DataManager.GetIns().enenvironmentSetUp);
        addOptions(dropdowns[3], DataManager.GetIns().time);
    }

    /// <summary>
    /// 初始化下拉框选择事件
    /// </summary>
    private void InitDropdownSeleteEvent()
    {
        dropdowns[0].onValueChanged.AddListener(SeleteSubjectOnValue);
    }
    /// <summary>
    /// 当选择科目时
    /// </summary>
    private void SeleteSubjectOnValue(int Index)
    {
        string[] strs = new string[] { };

        if (_drill.isOn)
        {
            strs = DataManager.GetIns().DrillContent.Split('，');
            if(strs[Index] == "行驶")
            {
                addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
            }
            else if (strs[Index] == "启动")
            {
                addOptions(dropdowns[2], DataManager.GetIns().enenvironmentSetUp);
            }
            else if (strs[Index] == "静止目标射击" || strs[Index] == "运动目标射击" || strs[Index] == "空中目标射击")
            {
                addOptions(dropdowns[2], DataManager.GetIns().enenvironmentShoot);
            }
        }
        else if (_OneSynergyTrain.isOn)
        {
            strs = DataManager.GetIns().OneSynergyContent.Split('，');
            addOptions(dropdowns[2], DataManager.GetIns().Shootenenvironment);
        }
        else
        {
            strs = DataManager.GetIns().SynergyContent.Split('，');
            addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
        }
        


    }

    private void Start()
    {
        if (_drill.isOn)
        {
            addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
        }
        else if(_OneSynergyTrain.isOn)
        {
            addOptions(dropdowns[0], DataManager.GetIns().OneSynergyContent);
        }
        else
        {
            addOptions(dropdowns[0], DataManager.GetIns().SynergyContent);
        }
    }



    private void Update()
    {
        OnToggleClick();
        switch (IsTrainType)
        {
            case SubjectType.Single:
                _drill.isOn = true;
                _SynergyTrain.isOn = false;
                _OneSynergyTrain.isOn = false;
                break;
            case SubjectType.onesynergy:
                _drill.isOn = false;
                _SynergyTrain.isOn = false;
                _OneSynergyTrain.isOn = true;
                break;
            case SubjectType.synergy:
                _drill.isOn = false;
                _SynergyTrain.isOn = true;
                _OneSynergyTrain.isOn = false;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// 初始化按钮点击事件
    /// </summary>
    private void InitBtnOnClick()
    {
        CreateBtn.onClick.AddListener(CreatScheme);
        CloseBtn.onClick.AddListener(CloseFace);
    }

    //下拉列表数据的添加,数据格式以逗号隔开
    public static void addOptions(Dropdown dropdown, string options)
    {
        dropdown.ClearOptions();
        //数据切割，保存
        string[] S = options.Split('，');
        List<string> L = new List<string>();
        L.AddRange(S);
        dropdown.ClearOptions();
        dropdown.AddOptions(L);

    }

    /// <summary>
    /// 单选框
    /// </summary>
    public void OnToggleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = EventSystem.current.currentSelectedGameObject;
            if (g != null)
            {
                if (g.name == "SingleTrain")
                {
                    if (!_drill.isOn)
                    {
                        _drill.isOn = true;
                        _SynergyTrain.isOn = false;
                        _OneSynergyTrain.isOn = false;
                        IsTrainType =  SubjectType.Single;
                        addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
                    }   
                }
                else if (g.name == "SynergyTrain")
                {
                    if (!_SynergyTrain.isOn)
                    {
                        _drill.isOn = false;
                        _SynergyTrain.isOn = true;
                        _OneSynergyTrain.isOn = false;
                        IsTrainType = SubjectType.synergy;
                        addOptions(dropdowns[0], DataManager.GetIns().SynergyContent);
                        addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
                    }
                }
                else if (g.name == "OneSynergyTrain")
                {
                    if (!_OneSynergyTrain.isOn)
                    {
                        _drill.isOn = false;
                        _SynergyTrain.isOn = false;
                        _OneSynergyTrain.isOn = true;
                        IsTrainType = SubjectType.onesynergy;
                        addOptions(dropdowns[0], DataManager.GetIns().OneSynergyContent);
                        addOptions(dropdowns[2], DataManager.GetIns().Shootenenvironment);
                    }
                }
            }
        }
    }

    /// <summary>
    /// 创建科目方案
    /// </summary>
    public void CreatScheme()
    {
        if(ProjectName.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "方案名不能为空！");
            return;
        }
        if (DataManager.GetIns().CheckSchemeVo(ProjectName.text))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "已有方案名，请更换方案名！");
            return;
        }

        SchemeVo vo = new SchemeVo();
        vo.ID = DataManager.GetIns().schemeDic.Count + 1;
        vo.ProjectName = ProjectName.text;
        vo.SubType = IsTrainType;
        vo.Subject = dropdowns[0].captionText.text;
        vo.Weather = dropdowns[1].captionText.text;
        vo.Environment = dropdowns[2].captionText.text;
        vo.DataTime = dropdowns[3].captionText.text;

        bool isTrue = DBManager.Getins().editor<SchemeVo>(SQLEditorType.insert, vo, TableConst.SCHEMETABLE);

        if (isTrue)
        {
            EventManager.GetIns().ExcuteEvent(EventConstant.DELETEOBJECT_EVENT, true);
            Close();
        }


        //GameObject ObjectLine = Instantiate();
    }

    private void CloseFace()
    {
        this.Close();
    }
}
