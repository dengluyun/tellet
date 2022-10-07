using System.Collections.Generic;

public class LocalizationManager
{
    private System.Collections.Generic.Dictionary<string, string> dictionary = new System.Collections.Generic.Dictionary<string, string>();

    private static LocalizationManager _Ins;

    public static LocalizationManager GetIns()
    {
        if (_Ins == null)
        {
            _Ins = new LocalizationManager();
        }
        return _Ins;

    }

    public List<string> unitList = new List<string>();
    public int currentCompanyIndex = 0;
    public string currentCompanyName = "";

    public List<string> memberList = new List<string>();
    public int currentMemberIndex = 0;
    public string currentMemberName = "";

    public void InitMember()
    {
        var reader = DBManager.Getins().ReadFullTable("StudentTable");
        memberList.Clear();
        while (reader.Read())
        {
            var n = reader["StudentName"];
            memberList.Add(n.ToString());
        }

    }

    public void InitUnit()
    {
        var reader = DBManager.Getins().ReadFullTable("CompanyTable");
        unitList.Clear();
        while (reader.Read())
        {
            var n = reader["Name"];
            unitList.Add(n.ToString());
        }
        ChangeUnit(0);
    }

    public void ChangeUnit(int index)
    {
        currentCompanyIndex = index;
        currentCompanyName = unitList[currentCompanyIndex];
        var reader = DBManager.Getins().ReadFullTable("StudentTable" + " WHERE Unit=" + "'" + currentCompanyName + "'");
        memberList.Clear();
        while (reader.Read())
        {
            var n = reader["StudentName"];
            memberList.Add(n.ToString());
        }

    }

    public string UnitName = "单位";
    public string MemberName = "人员";

    public string ConditionTypeName = "观察条件";
    public string[] ConditionType = { "昼间使用潜望镜", "夜间开大灯", "夜间使用夜视仪" };

    public string DriveLevelName = "驾驶等级";
    public string[] DriveLevel = { "新手", "二级", "一级", "特级" };

    public string WeatherTypeName = "气候";
    public string[] WeatherType = { "晴", "大雪", "中雪", "暴雨", "中雨", "小雨", "大风", "有风", "微风", "多云", "雾" };

    public string TrainTypeName = "训练类别";
    public string[] TrainType = { "练习", "考核" };

    public string MemberTypeName = "车员类型";
    public string[] MemberType = { "炮长", "驾驶", "车长" };


    public void Init()
    {
        //InitUnit();
        InitMember();
        dictionary.Clear();
        //strs = DataManager.GetIns().ShootCourse.Split('，');

    }

    public string GetDataByKey(string key)
    {
        string ret = key;
        var tryS = dictionary.TryGetValue(key, out ret);
        //if (!tryS)
        //    return key;

        return ret;
    }

}