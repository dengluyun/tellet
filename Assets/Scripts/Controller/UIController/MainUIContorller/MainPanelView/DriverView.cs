using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class DriverView : BaseView
{
    private Transform remark;
    private Text contetTxt;
    private Dropdown unitDropdown;
    private Dropdown peopleDropdown;
    private Dropdown conditionDropdown;
    private Dropdown levelDropdown;
    private Dropdown climateDropdown;
    private Dropdown typeDropdown;

    // *======================== dely ======================*//
    public Dropdown trainDropdown;

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

    private string[] courseArr = { "低速档驾驶", "换挡与转向驾驶", "各种速度驾驶", "坡上驾驶", "连续通过限制路和障碍物驾驶", "上下模拟装载平台驾驶", "场内模拟道路驾驶" };
    private List<string> courseReamrk;
    public void Awake()
    {
        InitCourseReamrk();
    }

    public void Start()
    {
        remark = FindTransform("right/DriverRemark");
        trainDropdown = FindDropdown("left/Dropdown");
        //unitDropdown = FindDropdown("right/DriverRemark/unit");
        //peopleDropdown = FindDropdown("right/DriverRemark/people");
        //conditionDropdown = FindDropdown("right/DriverRemark/condition");
        //levelDropdown = FindDropdown("right/DriverRemark/level");
        //climateDropdown = FindDropdown("right/DriverRemark/climate");
        //typeDropdown = FindDropdown("right/DriverRemark/type");
        contetTxt = FindText("left/remark/Scroll View/Viewport/Content");

        //AddDropdownListener(unitDropdown, SeleteUnit);
        //AddDropdownListener(peopleDropdown, SeletePeople);
        //AddDropdownListener(conditionDropdown, SeleteCondition);
        //AddDropdownListener(levelDropdown, SeleteLevel);
        //AddDropdownListener(climateDropdown, SeleteClimate);
        //AddDropdownListener(typeDropdown, SeleteType);

        GetAllUnit();

    }

    private void GetAllUnit()
    {

    }

    /// <summary>
    /// 选择单位
    /// </summary>
    public void SeleteUnit(int Index)
    {
    }

    /// <summary>
    /// 选择人员
    /// </summary>
    public void SeletePeople(int Index)
    {
    }

    /// <summary>
    /// 选择条件
    /// </summary>
    public void SeleteCondition(int Index)
    {
    }

    /// <summary>
    /// 选择等级
    /// </summary>
    public void SeleteLevel(int Index)
    {
    }

    /// <summary>
    /// 选择气候
    /// </summary>
    public void SeleteClimate(int Index)
    {
    }

    /// <summary>
    /// 选择类型
    /// </summary>
    public void SeleteType(int Index)
    {
    }

    /// <summary>
    /// 当选择科目时
    /// </summary>
    public void SeleteSubjectValue(int Index)
    {
        Debug.Log("选择科目DriverView" + Index);
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
            "1. 起动发动机、检查仪表及熄火；" +
            "2.一挡起车、转向、制动、停车和倒车；" +
            "3.二挡起车、转向、制动、停车；" +
            "4.用一、二挡驾驶车辆；" +
            "5.原位转向。" +
            "条件：" +
            "昼间，先升座开窗，后降座关窗；场地平坦" +
            "方法：" +
            "1.反复练习一挡起车,不同角度转向,停车,原位转向；" +
            "2.反复练习二挡起车,不同角度转向,制动,停车,原位转向；" +
            "3.按指挥反复练习倒车；" +
            "4.按规定路线用一，二挡驾驶车辆，根据地形情况进行各种转向,制动和停车。" +
            "评定：" +
            "能够按要求驾驶车辆,动作要领基本正确为合格。";
        courseReamrk.Add(str); //低速挡驾驶

        str = "内容：" +
            "1. 依次换挡；" +
            "2.转向。" +
            "条件：" +
            "昼间，先升座开窗，后降座关窗；场地应有平坦地、小起伏地和用于各种速度练习转向的地段，六杆直线通路和四杆原位转向通路。" +
            "方法：" +
            "1.在150米距离内反复练习二、三挡互换，在120米距离内反复练习二挡至四挡、四挡至二挡依次换挡；" +
            "2.高速（运动速度不低于25千米 / 小时。下同）通过六杆直线通路；高速接近（车头距接近端10米外运动速度不低于25千米 / 小时。下同）四杆原位转向通路，" +
            "通过后迅速离开（车尾距驶离端4米内至少增高一级排挡。下同）。" +
            "评定：" +
            "在150米距离内进行二、三挡互换5次，成功率达80 % 以上；" +
            "在120米距离内按二挡至四挡、四挡换二挡的顺序依次换挡，成功率达80 % 以上；" +
            "达到以上标准为合格。";
        courseReamrk.Add(str); //换挡和转向驾驶

        str = "内容：" +
            "1. 换挡：依次换挡；" +
            "2. 转向：各种角度转向；" +
            "3. 制动：发动机制动，制动器制动，联合制动；" +
            "4. 定点停车、起车。" +
            "条件：" +
            "昼、夜间降座关窗，夜间先使用大灯后使用夜视仪；场地内应有平坦地、小起伏地和用于各种速度练习转向的地段，六杆直线通路和四杆原位转向通路，五杆" +
            "转向通路。" +
            "方法：" +
            "1. 在120米距离内反复练习二挡至四挡、四挡至二挡依次换挡；" +
            "2. 高速通过六杆直线通路；使用任意位置转向高速通过五杆转向通路，高速接近并迅速离开四杆原位转向通路；" +
            "3. 距停车点10米外高速接近，平稳停车，二挡起车。";
        courseReamrk.Add(str); //各种速度驾驶

        str = "内容：" +
            "1. 坡上停车、起车；" +
            "2. 坡上制动；" +
            "3. 坡上转向。" +
            "4. 定点停车、起车。" +
            "条件：" +
            "昼间降座关窗；场地应有不同长度的上下坡和侧倾坡。" +
            "方法：" +
            "在缓（10度以下）、中（10~20度）、陡（20度以上）坡上，依次练习定点停车、起车和制动，在上、下坡和侧倾坡上练习转向。";
        courseReamrk.Add(str); //坡上驾驶

        str = "内容：" +
            "1. 通过直线桩间限制路；" +
            "2. 通过弯道限制路" +
            "3. 通过下坡桩间限制路；" +
            "4. 通过车辙桥；" +
            "5. 通过土岭；" +
            "6. 通过弹坑。" +
            "条件：" +
            "昼、夜间降座关窗，夜间使用夜视仪；" +
            "场地为中等起伏地，路线长3.5~4.5千米，各限制路与障碍物之间的距离不小于200米；" +
            "路线上应有15~25度、长30~50米的上坡和10~20度、长50~100米的下坡，转向角度大于90度的转向地点和下坡（8~12度）转向地点各1~2处；" +
            "各限制路与障碍物旁应设有明显的标志物，下坡桩间限制路设在8~12度的坡上，通过障碍物的规定宽度为车宽加0.8米。" +
            "方法：" +
            "1. 高速接近并迅速离开直线桩间限制路、下坡桩间限制路（不要求高速接近）、车辙桥、土岭和弹坑，高速通过弯道限制路。" +
            "2. 各限制路和障碍物处设评分员，准确评定成绩；出发线设计时员，准确登记单车运动时间，计算平均运动速度。" +
            "评定：" +
            "降低平均运动速度的扣分规定（从通过限制路和障碍物的所得分中扣除）；" +
            "降低5%（含）以内，扣5%；降低5%以上、10%（含）以内，扣10%；" +
            "降低10%以上、15%（含）以内，扣15%；" +
            "降低15%以上、20%（含）以内，扣20%；" +
            "降低20%以上，扣60%。";
        courseReamrk.Add(str); //连续通过限制路和障碍物驾驶

        str = "内容：" +
            "上下模拟装载平台驾驶。" +
            "条件：" +
            "无等级驾驶员昼间升座开窗，二级驾驶员昼、夜间降座关窗，夜间使用大灯，一级驾驶员昼、夜间降座关窗，夜间使用夜视仪。" +
            "方法：" +
            "高速接近并迅速离开。";
        courseReamrk.Add(str); //上下模拟装载平台驾驶

        str = "内容：" +
            "1. 通过弯道限制路；" +
            "2. 通过“T”形限制路；" +
            "3. 通过桥式限制路；" +
            "4. 通过地雷场通路；" +
            "5. 通过凸凹地段；" +
            "6. 通过染毒地段；" +
            "7. 通过200米（150米）增速地段；" +
            "8. 根据驻地及作战地区道路情况，设置弹坑等模拟地段。" +
            "条件：" +
            "昼、夜间降座关窗；路线长3.5~4.5千米，桥式限制路宽度为车宽加1米、长20米、杆距5米，地雷场通路宽度为车宽加1米、长50米、杆距10米，" +
            "凸凹路段长30~100米，染毒地段长200~500米；" +
            "通过弹坑的规定宽度为车宽加0.8米，200米增速地段场地条件不能满足时可缩减为150米各模拟地段之间距离不小于200米。" +
            "方法：" +
            "1. 200（150）米增速地段以一挡起车，依次增挡至地形允许的最高速度通过；" +
            "2. 高速驶入“T”形限制路，二挡通过；高速通过弯道限制路和桥式限制路，高速接近并迅速离开弹坑，以地形允许的最高速度通过地雷场通路和染毒地段；" +
            "3. 各模拟地段处设评分员，准确评定成绩；出发线设计时员，准确登记单车运动时间。";
        courseReamrk.Add(str); //场内模拟道路驾驶
    }

}
