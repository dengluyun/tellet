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

    public string UnitName = "��λ";
    public string MemberName = "��Ա";

    public string ConditionTypeName = "�۲�����";
    public string[] ConditionType = { "���ʹ��Ǳ����", "ҹ�俪���", "ҹ��ʹ��ҹ����" };

    public string DriveLevelName = "��ʻ�ȼ�";
    public string[] DriveLevel = { "����", "����", "һ��", "�ؼ�" };

    public string WeatherTypeName = "����";
    public string[] WeatherType = { "��", "��ѩ", "��ѩ", "����", "����", "С��", "���", "�з�", "΢��", "����", "��" };

    public string TrainTypeName = "ѵ�����";
    public string[] TrainType = { "��ϰ", "����" };

    public string MemberTypeName = "��Ա����";
    public string[] MemberType = { "�ڳ�", "��ʻ", "����" };


    public void Init()
    {
        //InitUnit();
        InitMember();
        dictionary.Clear();
        //strs = DataManager.GetIns().ShootCourse.Split('��');

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