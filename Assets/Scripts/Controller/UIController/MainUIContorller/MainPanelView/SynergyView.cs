using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SynergyView : BaseView
{
    // *======================== dely ======================*//
    public Text unitName;
    public Dropdown unitNames;

    public Text memberName1;
    public Dropdown memberNames1;
    
    public Text memberName2;
    public Dropdown memberNames2;
    
    public Text memberName3;
    public Dropdown memberNames3;

    public Text conditionTypeName;
    public Dropdown conditionTypeNames;

    //public Text driveLevelName;
    //public Dropdown driveLevelNames;

    public Text weatherTypeName;
    public Dropdown weatherTypeNames;

    //public Text trainTypeName;
    //public Dropdown trainTypeNames;

    private void OnUnitNamesChange(int value)
    {
        var lm = LocalizationManager.GetIns();
        lm.ChangeUnit(value);
        memberNames1.ClearOptions();
        memberNames1.AddOptions(lm.memberList);

        memberNames2.ClearOptions();
        memberNames2.AddOptions(lm.memberList);

        memberNames3.ClearOptions();
        memberNames3.AddOptions(lm.memberList);

    }
    private void OnMemberNamesChange(int value)
    {

    }

    public void RefreshRightView()
    {
        var lm = LocalizationManager.GetIns();
        unitName.text = lm.UnitName;
        unitNames.ClearOptions();
        unitNames.AddOptions(lm.unitList);
        unitNames.onValueChanged.AddListener(OnUnitNamesChange);

        memberName1.text = LocalizationManager.GetIns().MemberType[0];
        memberNames1.ClearOptions();
        memberNames1.AddOptions(lm.memberList);
        memberNames1.onValueChanged.AddListener(OnMemberNamesChange);

        memberName2.text = LocalizationManager.GetIns().MemberType[1];
        memberNames2.ClearOptions();
        memberNames2.AddOptions(lm.memberList);
        memberNames2.onValueChanged.AddListener(OnMemberNamesChange);

        memberName3.text = LocalizationManager.GetIns().MemberType[2];
        memberNames3.ClearOptions();
        memberNames3.AddOptions(lm.memberList);
        memberNames3.onValueChanged.AddListener(OnMemberNamesChange);

        conditionTypeName.text = LocalizationManager.GetIns().ConditionTypeName;
        conditionTypeNames.ClearOptions();
        conditionTypeNames.AddOptions(LocalizationManager.GetIns().ConditionType.ToList());

        weatherTypeName.text = LocalizationManager.GetIns().WeatherTypeName;
        weatherTypeNames.ClearOptions();
        weatherTypeNames.AddOptions(LocalizationManager.GetIns().WeatherType.ToList());

        //trainTypeName.text = LocalizationManager.GetIns().TrainTypeName;
        //trainTypeNames.ClearOptions();
        //trainTypeNames.AddOptions(LocalizationManager.GetIns().TrainType.ToList());
    }
    // *======================== dely ======================*//
    private Transform remark;
    private Dropdown trainDropdown;
    private Text contetTxt;
    private string[] courseArr = { "单车多专业协同训练" };
    private List<string> courseReamrk;
    public void Awake()
    {
        InitCourseReamrk();
    }

    public void Start()
    {
        this.remark = FindTransform("right/SynergyRemark");
        this.trainDropdown = FindDropdown("left/Dropdown");
        this.contetTxt = FindText("left/remark/Scroll View/Viewport/Content");
    }

    /// <summary>
    /// 当选择科目时
    /// </summary>
    public void SeleteSubjectValue(int Index)
    {
        Debug.Log("选择科目SynergyView" + Index);
        this.contetTxt.text = courseReamrk[Index];

    }

    public void RefreshView(bool value)
    {
        this.remark.gameObject.SetActive(value);
        if (value)
        {
            this.trainDropdown.onValueChanged.RemoveAllListeners();
            this.trainDropdown.onValueChanged.AddListener(SeleteSubjectValue);
            SeleteSubjectValue(0);
            addOptions(this.trainDropdown, courseArr);
            RefreshRightView();
        }
    }

    public void InitCourseReamrk()
    {
        courseReamrk = new List<string>();
        string str = "内容：" +
            "训练三乘员之间的协同配合能力。" +
            "方法：" +
            "通过规定路段；" +
            "记录车辆行驶时间，计算平均运动速度；" +
            "沿途设置8个目标。" +
            "评定：" +
            "同时达到下列要求为合格，否则为不合格。" +
            "命中5个以上（含5个）目标；" +
            "全程平均速度不小于21千米/小时。";
        courseReamrk.Add(str); //单车多专业协同训练
    }
}

