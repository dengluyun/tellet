using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ShootView : BaseView
{
    // *======================== dely ======================*//
    public Text unitName;
    public Dropdown unitNames;

    public Text memberName;
    public Dropdown memberNames;

    //public Text conditionTypeName;
    //public Dropdown conditionTypeNames;

    //public Text driveLevelName;
    //public Dropdown driveLevelNames;

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

        //conditionTypeName.text = LocalizationManager.GetIns().ConditionTypeName;
        //conditionTypeNames.ClearOptions();
        //conditionTypeNames.AddOptions(LocalizationManager.GetIns().ConditionType.ToList());

        //driveLevelName.text = LocalizationManager.GetIns().DriveLevelName;
        //var driveLevelList = LocalizationManager.GetIns().DriveLevel.ToList();
        //driveLevelNames.ClearOptions();
        //driveLevelNames.AddOptions(driveLevelList);

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
    private string[] courseArr = { "描绘信封靶", "快速精确瞄准发射", "平稳跟踪目标", "搜索目标、测定距离", "稳像工况原地对不动和运动目标射击", "简易工况原地对不动和运动目标射击", "使用辅助瞄准镜对空中目标射击", "反坦克导弹对运动和不动目标射击", "夜间稳像工况下陆上原地对运动和不动目标射击", "稳像工况下行进间对运动和不动目标射击" };
    private List<string> courseReamrk;
    public void Awake()
    {
        InitCourseReamrk();
    }

    public void Start()
    {
        this.remark = FindTransform("right/ShootRemark");
        this.trainDropdown = FindDropdown("left/Dropdown");
        this.contetTxt = FindText("left/remark/Scroll View/Viewport/Content");
        this.trainDropdown.onValueChanged.AddListener(SeleteSubjectValue);
    }

    /// <summary>
    /// 当选择科目时
    /// </summary>
    public void SeleteSubjectValue(int Index)
    {
        Debug.Log("选择科目ShootView" + Index);
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
            "操作操纵台描绘信封靶" +
            "方法：" +
            "按下测距按钮开始描绘" +
            "按下击发按钮结束描绘" +
            "评定：" +
            "描绘出全部图形，35秒内合格";
        courseReamrk.Add(str); //描绘信封靶

        str = "内容：" +
            "对不同性质的目标瞄准发射" +
            "方法：" +
            "对炮目标及枪目标直接使用击发钮" +
            "选择正确弹种射击，否则虽命中但不记录成绩" +
            "评定：" +
            "60秒内，命中18,15,12次分别为优秀，良好，及格" +
            "12次以下为不及格" +
            "命中率低于80%成绩降一等";
        courseReamrk.Add(str); //快速精确瞄准射击

        str = "内容：" +
            "平稳跟踪目标" +
            "方法：" +
            "按住测距钮开始跟踪" +
            "按下击发钮结束跟踪" +
            "评定：" +
            "60秒内，有效跟踪8、6、4次分别为优秀，良好，及格" +
            "4次以下为不及格。";
        courseReamrk.Add(str); //平稳跟踪目标

        str = "内容：" +
            "搜索目标、测定距离" +
            "方法：" +
            "对各目标进行搜索、判定、瞄准和测距。" +
            "评定：" +
            "在2分钟内，搜索到全部目标且正确测定距离正确为优秀；"+
            "遗漏1个目标或测定距离错误1次为良好;"+
            "遗漏2个目标或测定距离错误2次为及格。";
        courseReamrk.Add(str); //搜索目标、测定距离

        str = "内容：" +
            "稳像工况原地对不动和运动目标射击"+
            "方法：" +
            "炮手操纵火控系统稳像工况使用不同的弹种（坦克、装甲输送车、步兵战车使用30穿甲弹，步兵群使用100杀爆榴弹）" +
            "对各个目标测距和瞄准射击。" +
            "评定：" +
            "弹种转换正确，命中所有四个目标成绩为优秀；" +
            "命中三个目标成绩为良好；" +
            "命中两个目标成绩为及格；" +
            "及格，命中两个目标以下成绩为不及格。";
        courseReamrk.Add(str); //稳像工况原地对不动和运动目标射击

        str = "内容：" +
            "简易工况原地对不动和运动目标射击" +
            "方法：" +
            "炮手操纵火控系统简易工况使用不同的弹种（坦克、装甲输送车、步兵战车使用30穿甲弹，步兵群使用100杀爆榴弹）" +
            "对各个目标测距和瞄准射击。" +
            "评定：" +
            "弹种转换正确，命中所有四个目标成绩为优秀；" +
            "命中三个目标成绩为良好；" +
            "命中两个目标成绩为及格；" +
            "及格，命中两个目标以下成绩为不及格。";
        courseReamrk.Add(str); //简易工况原地对不动和运动目标射击

        str = "内容：" +
            "对空捕捉目标瞄准发射" +
            "方法：" +
            "搜索瞄准目标发射" +
            "选择正确弹种射击，否则虽命中但不记录成绩" +
            "评定：" +
            "60秒内，捕捉目标瞄准正确，击发果断为合格";
        courseReamrk.Add(str); //使用辅助瞄准镜对空中目标射击

        str = "内容：" +
            "使用反坦克导弹对运动和不动目标射击" +
            "方法：" +
            "使用反坦克导弹，瞄准目标发射" +
            "选择正确弹种射击，否则虽命中但不记录成绩" +
            "评定：" +
            "命中目标为合格";
        courseReamrk.Add(str); //反坦克导弹对运动和不动目标射击

        str = "内容：" +
            "夜间稳像工况下陆上原地对运动和不动目标射击" +
            "方法：" +
            "炮手操纵火控系统稳像工况在夜间行进间使用不同的弹种" +
            "对各个目标测距和瞄准射击。" +
            "评定：" +
            "命中三个目标成绩为优秀"+
            "命中两个炮目标成绩为良好"+
            "命中一个炮目标成绩为及格";
        courseReamrk.Add(str); //夜间稳像工况下陆上原地对运动和不动目标射击

        str = "内容：" +
            "稳像工况下行进间对运动和不动目标射击" +
            "方法：" +
            "炮手操纵火控系统稳像工况行进间使用不同的弹种" +
            "对各个目标测距和瞄准射击。" +
            "评定：" +
            "命中三个目标成绩为优秀" +
            "命中两个炮目标成绩为良好" +
            "命中一个炮目标成绩为及格";
        courseReamrk.Add(str); //稳像工况下行进间对运动和不动目标射击
    }

}
