using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class HelpView : BaseView
{
    // *======================== dely ======================*//

    public void RefreshRightView()
    {
        var lm = LocalizationManager.GetIns();


    }
    // *======================== dely ======================*//
    public GameObject middle;
    public GameObject other1;
    public GameObject other2;
    public GameObject other3;
    public GameObject other4;
    public Dropdown dropdown;
    public Text titleText;
    private string defaultTitleText;
    public Text contentText;
    private string[] helpArray = { "主版本号", "子版本号", "修订版本号", "日期版本号", "希腊字母版本号" };
    private string[] contentTextArray = { 
        "当功能模块有较大的变动，比如增加多个模块或者整体架构发生变化。此版本号由项目决定是否修改。",
        "当功能有一定的增加或变化，比如增加了对权限控制、增加自定义视图等功能。此版本号由项目决定是否修改。",
        "一般是bug修复或是一些小的变动，要经常发布修订版，时间间隔不限，修复一个严重的bug即可发布一个修订版。此版本号由项目经理决定是否修改。",
        "用于记录修改项目的当前日期，每天对项目的修改都需要更改日期版本号。此版本号由开发人员决定是否修改。",
        "此版本号用于标注当前版本的软件处于哪个开发阶段，当软件进入到另一个阶段时需要修改此版本号。此版本号由项目决定是否修改。\n\r" +
            "Alpha版:此版本表示该软件在此阶段主要是以实现软件功能为主，通常只在软件开发者内部交流，一般而言，该版本软件的Bug较多，需要继续修改。\n\r" +
            "Beta版:该版本相对于α版已有了很大的改进，消除了严重的错误，但还是存在着一些缺陷，需要经过多次测试来进一步消除，此版本主要的修改对像是软件的UI。\n\r" +
            "RC版:该版本已经相当成熟了，基本上不存在导致错误的BUG，与即将发行的正式版相差无几。\n\r" +
            "Release版:该版本意味“最终版本”，在前面版本的一系列测试版之后，终归会有一个正式版本，是最终交付用户使用的一个版本。该版本有时也称为标准版。一般情况下，Release不会以单词形式出现在软件封面上，取而代之的是符号(Ｒ)。",

    };
    public void Awake()
    {
        defaultTitleText = titleText.text;
        addOptions(dropdown,helpArray);
        dropdown.onValueChanged.RemoveAllListeners();
        dropdown.onValueChanged.AddListener(SeleteSubjectValue);

    }

    public void Start()
    {
    }

    public void SeleteSubjectValue(int Index)
    {
        Debug.Log("HelpView" + Index);
        contentText.text = contentTextArray[Index];
    }

    public void RefreshView(bool value)
    {
        other1.SetActive(!value);
        other2.SetActive(!value);
        other3.SetActive(!value);
        other4.SetActive(!value);
        middle.SetActive(value);
        if(value)
        {
            string versionStr = ConfigurationReader.GetConfigFile("Version.txt");

            titleText.text = versionStr;

        }
        else
        {
            titleText.text = defaultTitleText;

        }

    }

}

