using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// 数据管理器
/// </summary>
public class DataManager
{
    /// <summary>
    /// 单例 确保数据的唯一性
    /// </summary>
    private static DataManager _Ins;
    public static DataManager GetIns()
    {
        if(_Ins == null)
        {
            _Ins = new DataManager();
        }
        return _Ins;
        
    }

    public void Init()
    {
        DBManager.Getins().LoadPares<SchemeVo>(TableConst.SCHEMETABLE);
        DBManager.Getins().LoadPares<StudentVo>(TableConst.STUDENTTABLE);
        DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);
    }
    /// <summary>
    /// 导调台ID
    /// </summary>
    public string ControllerID = "";

    /// <summary>
    /// 录像路线
    /// </summary>
    public string VideoPath = "F:/ZJ-Project/Project2022/FebruaryProject/CSK181_PAV/EXE/Video/";

    /// <summary>
    /// 录屏指令
    /// </summary>
    public string RecordingScreenOrder = "-f gdigrab -framerate 15 -video_size 1920*1024 -i desktop -pix_fmt yuv420p";

    public string NeedLook = "";

    /// <summary>
    /// 训练开始时间
    /// </summary>
    public string TrainTime = "";

    /// <summary>
    /// 是否是多车协同训练
    /// </summary>
    public bool IsCars = false;


    /// <summary>
    /// 射手科目
    /// </summary>
    public string ShootCourse = "描绘信封靶，快速精确瞄准发射，平稳跟踪目标，搜索目标、测定距离，稳像工况原地对不动和运动目标射击，简易工况原地对不动和运动目标射击，使用辅助瞄准镜对空中目标射击，反坦克导弹对运动和不动目标射击，夜间稳像工况下陆上原地对运动和不动目标射击，稳像工况下行进间对运动和不动目标射击";

    /// <summary>
    /// 驾驶科目
    /// </summary>
    public string DriverCourse = "低速档驾驶，换挡与转向驾驶，各种速度驾驶，坡上驾驶，连续通过限制路和障碍物驾驶，上下模拟装载平台驾驶，场内模拟道路驾驶";

    /// <summary>
    /// 协同科目
    /// </summary>
    public string SynergyCourse = "单车多专业协同训练";

    /// <summary>
    /// 通信科目
    /// </summary>
    public string MsgCourse = "超短波电台操作使用，超短波电台与车内通话器配合使用，停止间通信，车长任务终端指控软件操作使用";

    /// <summary>
    /// 单项训练
    /// </summary>
    public string DrillContent = "启动，起步，挂挡，转弯，上下坡，斜坡行驶，越过弹坑，障碍物通行，越过沟渠，静止目标射击，运动目标射击，行驶，空中目标射击";
    /// <summary>
    /// 单车协同训练
    /// </summary>
    public string OneSynergyContent = "单车协同训练";
    /// <summary>
    /// 协同训练
    /// </summary>
    public string SynergyContent = "多车协同训练";
    /// <summary>
    /// 天气
    /// </summary>
    public string weather = "雨天，晴天，雪天，阴天，雾天";


    /// <summary>
    /// 地形-训练场，模式空中目标射击
    /// </summary>
    public string enenvironmentTrain = "沙漠";

    /// <summary>
    /// 地形-启动模式
    /// </summary>
    public string enenvironmentSetUp = "雪地，山地，泥泞地面，沙漠，蜿蜒山脉，训练场，城市，雪山，城镇";

    /// <summary>
    /// 地形-单独射击地形
    /// </summary>
    public string enenvironmentShoot = "雪地，山地，泥泞地面，沙漠，蜿蜒山脉，城镇，雪山，城市";

    /// <summary>
    /// 地形
    /// </summary>  
    public string Allenenvironment = "雪地，山地，泥泞地面，沙漠，蜿蜒山脉，城镇，雪山，城市";

    /// <summary>
    /// 单车协同训练地形
    /// </summary>
    public string Shootenenvironment = "沙漠";



    /// <summary>
    /// 时间
    /// </summary>
    public string time = "00:00，01:00，02:00，03:00，04:00，05:00，06:00，07:00，08:00，09:00，10:00，11:00，12:00，13:00，14:00，15:00，16:00，17:00，18:00，19:00，20:00，21:00，22:00，23:00";

    /// <summary>
    /// 需要下发的科目
    /// </summary>
    public SchemeVo SchemeVo;

    /// <summary>
    /// 需要被删除的记录
    /// </summary>
    public TrainingRecordVo NeedToDeleteRecord;

    /// <summary>
    /// 科目训练所需车辆
    /// </summary>
    public int TraingCarCount = 0;

    /// <summary>
    /// 将要进行训练的学员
    /// </summary>
    public List<StudentVo> trainStus = new List<StudentVo>();

    /// <summary>
    /// 协同训练时，所有训练的车编号以及车上的训练成员
    /// </summary>
    public Dictionary<int, List<StudentVo>> SynergyTraingDic = new Dictionary<int, List<StudentVo>>();

    /// <summary>
    /// 所有账户
    /// </summary>
    public Dictionary<int, AccountVo> accountDic = new Dictionary<int, AccountVo>();

    /// <summary>
    /// 所有学员
    /// </summary>
    public Dictionary<int, StudentVo> StudentDic = new Dictionary<int, StudentVo>();

    /// <summary>
    /// 所有方案
    /// </summary>
    public Dictionary<int, SchemeVo> schemeDic = new Dictionary<int, SchemeVo>();

    /// <summary>
    /// 所有训练记录
    /// </summary>
    public Dictionary<int, TrainingRecordVo> TRecordDic = new Dictionary<int, TrainingRecordVo>();

    /// <summary>
    /// 所有项目方案
    /// </summary>
    public List<UnityEngine.GameObject> Objects = new List<UnityEngine.GameObject>();


    /// <summary>
    /// 获取学员列表
    /// </summary>
    /// <returns></returns>
    public List<StudentVo> GetStudentList()
    {
        List<StudentVo> students = StudentDic.Values.ToList();
        //for (int i = 0; i < students.Count; i++)
        //{
        //    if(students[i].IsDelete == IsDeleteType.delete)
        //    {
        //        students.RemoveAt(i);
        //    }
        //}
        return students.FindAll((StudentVo item) => { return item.IsDelete == IsDeleteType.Use; });
    }

    /// <summary>
    /// 更新学员
    /// </summary>
    public List<StudentVo> UpdateStudentDic()
    {
        StudentDic.Clear();
        DBManager.Getins().LoadPares<StudentVo>(TableConst.STUDENTTABLE);
        return GetStudentList();
    }

    /// <summary>
    /// 更新科目方案
    /// </summary>
    public List<SchemeVo> UpdateSchemeDic()
    {
        schemeDic.Clear();
        DBManager.Getins().LoadPares<SchemeVo>(TableConst.SCHEMETABLE);

        List<SchemeVo> schemes = new List<SchemeVo>();
        schemes = schemeDic.Values.ToList();

        return schemes.FindAll((SchemeVo Item) => { return Item.IsDelete == IsDeleteType.Use; });
    }

    /// <summary>
    /// 更新科目方案
    /// </summary>
    public List<SchemeVo> GetSchemeList()
    {
        List<SchemeVo> schemes = new List<SchemeVo>();
        schemes = schemeDic.Values.ToList();

        return schemes.FindAll((SchemeVo Item) => { return Item.IsDelete == IsDeleteType.Use; });
    }


    /// <summary>
    /// 更新训练记录
    /// </summary>
    public List<TrainingRecordVo> UpdateTRecordDic()
    {
        TRecordDic.Clear();
        DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);
        return TRecordDic.Values.ToList();
    }

    /// <summary>
    /// 更新训练记录
    /// </summary>
    public List<TrainingRecordVo> GetTRecordDic()
    {
        return TRecordDic.Values.ToList();
    }

    /// <summary>
    /// 查询集合中是否有此方案
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CheckObjectName(string name)
    {
        List<SchemeVo> Schemelist = schemeDic.Values.ToList();

        Schemelist = Schemelist.FindAll((SchemeVo Item) => { return Item.ProjectName == name; });
        if(Schemelist.Count == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    /// <summary>
    /// 查询集合中是否有此方案,并返回
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public SchemeVo FindObjectName(int Index)
    {
        List<SchemeVo> Schemelist = schemeDic.Values.ToList();

        Schemelist = Schemelist.FindAll((SchemeVo Item) => { return Item.ID == Index; });
        if (Schemelist.Count == 0)
        {
            return null;
        }
        else
        {
            return Schemelist[0];
        }
    }

    /// <summary>
    /// 查询集合中是否有此学员名
    /// </summary>
    /// <param name="name"></param>
    /// <returns></returns>
    public bool CheckStudentName(string name,int ID)
    {
        List<StudentVo> Schemelist = StudentDic.Values.ToList();

        StudentVo Svo = Schemelist.Find((StudentVo Item) => { return Item.ID == ID; });
        if (Svo.StudentName == name)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    /// <summary>
    /// 寻找方案ID编号
    /// </summary>
    /// <returns></returns>
    public int FindObjectID(string name)
    {
        List<SchemeVo> Schemelist = schemeDic.Values.ToList();

        SchemeVo vo = Schemelist.Find((SchemeVo Item) => { return Item.ProjectName == name; });
        if (vo != null)
        {
            return vo.ID;
        }
        else
        {
            return 0;
        }
    }

    /// <summary>
    /// 寻找学员ID编号
    /// </summary>
    /// <returns></returns>
    public int FindStudentID(string StudentID)
    {
        List<StudentVo> Schemelist = StudentDic.Values.ToList();

        StudentVo vo = Schemelist.Find((StudentVo Item) => { return Item.StudentID == StudentID; });
        if (vo != null)
        {
            return vo.ID;
        }
        else
        {
            return 0;
        }
    }

    public StudentVo FindStudent(string StudentID)
    {
        List<StudentVo> Schemelist = StudentDic.Values.ToList();

        StudentVo vo = Schemelist.Find((StudentVo Item) => { return Item.ID == Convert.ToInt32(StudentID); });

        return vo;
    }

    /// <summary>
    /// 查找是否已有编号
    /// </summary>
    /// <returns></returns>
    public bool CheckStudentID(string ID)
    {

        List<StudentVo> vos = GetStudentList();

        StudentVo vo = vos.Find((StudentVo item) => { return item.StudentID == ID; });

        if(vo == null)
        {
            return false;
        }
        else
        {
            return true;
        }

        
    }

    /// <summary>
    /// 查找是否已有编号
    /// </summary>
    /// <returns></returns>
    public bool CheckSchemeVo(string Name)
    {

        List<SchemeVo> vos = GetSchemeList();

        SchemeVo vo = vos.Find((SchemeVo item) => { return item.ProjectName == Name; });

        if (vo == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public DateTime GetDateTime()
    {
        TrainTime = TrainTime.Replace("@", "/");
        TrainTime = TrainTime.Replace("#", ":");
        TrainTime = TrainTime.Replace("!", " ");
        return Convert.ToDateTime(TrainTime);
    }
}
