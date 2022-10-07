using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MsgView : BaseView
{
    // *======================== dely ======================*//
    public Text unitName;
    public Dropdown unitNames;

    public Text memberName;
    public Dropdown memberNames;

    public Text conditionTypeName;
    public Dropdown conditionTypeNames;

    public Text driveLevelName;
    public Dropdown driveLevelNames;

    public Text weatherTypeName;
    public Dropdown weatherTypeNames;

    public Text trainTypeName;
    public Dropdown trainTypeNames;

    private void OnUnitNamesChange(int value)
    {
        var lm = LocalizationManager.GetIns();
        lm.ChangeUnit(value);
        memberNames.ClearOptions();
        memberNames.AddOptions(lm.memberList);
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

        memberName.text = LocalizationManager.GetIns().MemberName;
        memberNames.ClearOptions();
        memberNames.AddOptions(lm.memberList);
        memberNames.onValueChanged.AddListener(OnMemberNamesChange);

        conditionTypeName.text = LocalizationManager.GetIns().ConditionTypeName;
        conditionTypeNames.ClearOptions();
        conditionTypeNames.AddOptions(LocalizationManager.GetIns().ConditionType.ToList());

        driveLevelName.text = LocalizationManager.GetIns().DriveLevelName;
        var driveLevelList = LocalizationManager.GetIns().DriveLevel.ToList();
        driveLevelNames.ClearOptions();
        driveLevelNames.AddOptions(driveLevelList);

        weatherTypeName.text = LocalizationManager.GetIns().WeatherTypeName;
        weatherTypeNames.ClearOptions();
        weatherTypeNames.AddOptions(LocalizationManager.GetIns().WeatherType.ToList());

        trainTypeName.text = LocalizationManager.GetIns().TrainTypeName;
        trainTypeNames.ClearOptions();
        trainTypeNames.AddOptions(LocalizationManager.GetIns().TrainType.ToList());
    }
    // *======================== dely ======================*//

    private Transform remark;
    private Dropdown trainDropdown;
    private Text contetTxt;
    private string[] courseArr = { "超短波电台操作使用", "超短波电台与车内通话器配合使用", "停止间通信", "车长任务终端指控软件操作使用" };
    private List<string> courseReamrk;
    public void Awake()
    {
        InitCourseReamrk();
    }

    public void Start()
    {
        this.remark = FindTransform("right/MsgRemark");
        this.trainDropdown = FindDropdown("left/Dropdown");
        this.contetTxt = FindText("left/remark/Scroll View/Viewport/Content");
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

    /// <summary>
    /// 当选择科目时
    /// </summary>
    public void SeleteSubjectValue(int Index)
    {
        Debug.Log("选择科目SynergyView" + Index);
        this.contetTxt.text = courseReamrk[Index];

    }

    public void InitCourseReamrk()
    {
        courseReamrk = new List<string>();
        string str = "内容：" +
            "外部检查；" +
            "参数预置；"+
            "电台模拟器使用。" +
            "方法：" +
            "操作者接收到开始指令后，开始练习；" +
            "逐次完成外部检查、参数预置（预置3个信道的参数）、使用电台模" +
            "拟器通信联络，与主控台取得联络后，按教练员要求完成转换工作方式、" +
            "转换信道实施通信联络等训练内容；" +
            "联络结束关闭电台模拟器电源开关。" +
            "评定：" +
            "外部检查内容全面；" +
            "参数预置准确迅速；" +
            "操作使用方法正确。";
        courseReamrk.Add(str); //超短波电台操作使用。

        str = "内容：" +
            "外部检查；" +
            "参数预置；" +
            "电台模拟器和车内通话器模拟器使用。" +
            "方法：" +
            "操作者接收到开始指令后，开始练习；" +
            "逐次进行外部检查、参数预置（预置3个信道的参数）、车内通话器" +
            "模拟器开关旋钮预置、使用电台发信通信联络，与主控台取得联络后，" +
            "按教练员要求完成车内通信联络、转信、转换工作方式、转换信道实施" +
            "车内外通信联络等训练内容；" +
            "联络结束关闭电台模拟器和车内通话器模拟器电源开关。" +
            "评定：" +
            "外部检查内容全面；" +
            "参数预置准确迅速；" +
            "操作使用方法正确。";
        courseReamrk.Add(str); //超短波电台与车内通话器配合使用。

        str = "内容：" +
            "停止间一般条件下通信；" +
            "停止间干扰条件下通信。" +
            "电台模拟器使用。" +
            "方法：" +
            "操作者接收到开始指令后，开始练习；" +
            "按照先一般条件下通信，后干扰条件下通信的顺序进行练习；" +
            "依据训练方案，采取主台诱导、车内出情况和观察战场的方法处置15" +
            "个战术情况，其中包括更换频率、更换功率和反冒充各1次，5个以上情" +
            "况必须进行车内外通话；" +
            "干扰条件下通信练习时，干扰人员可采取冒充上级欺骗诱导受训人员" +
            "或者采取强呼方式打乱受训人员通信联络。" +
            "评定：" +
            "操作使用电台和车内通话器正确；" +
            "建立联络迅速，听辨信号准确，处置情况及时正确；" +
            "反干扰措施有效，通信联络不间断；" +
            "遵守通信规则和通信纪律。";
        courseReamrk.Add(str); //停止间通信

        str = "内容：" +
            "车长任务终端指控软件操作使用" +
            "方法：" +
            "教练员从训练内容库中选择训练内容生成训练方案，训练开始后，自动下发至受训人员；" +
            "受训人员依据训练内容，逐项操作；" +
            "教练员可通过主控台显示器监控终端屏幕，实时记录训练情况。" +
            "评定：" +
            "操作方法步骤正确，动作准确；" +
            "软件使用正确，无误操作；" +
            "遵守指挥信息系统使用规定。";
        courseReamrk.Add(str); //车长任务终端指控软件操作使用
    }

}
