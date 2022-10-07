using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

/// <summary>
/// ���ݹ�����
/// </summary>
public class DataManager
{
    /// <summary>
    /// ���� ȷ�����ݵ�Ψһ��
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
    /// ����̨ID
    /// </summary>
    public string ControllerID = "";

    /// <summary>
    /// ¼��·��
    /// </summary>
    public string VideoPath = "F:/ZJ-Project/Project2022/FebruaryProject/CSK181_PAV/EXE/Video/";

    /// <summary>
    /// ¼��ָ��
    /// </summary>
    public string RecordingScreenOrder = "-f gdigrab -framerate 15 -video_size 1920*1024 -i desktop -pix_fmt yuv420p";

    public string NeedLook = "";

    /// <summary>
    /// ѵ����ʼʱ��
    /// </summary>
    public string TrainTime = "";

    /// <summary>
    /// �Ƿ��Ƕ೵Эͬѵ��
    /// </summary>
    public bool IsCars = false;


    /// <summary>
    /// ���ֿ�Ŀ
    /// </summary>
    public string ShootCourse = "����ŷ�У����پ�ȷ��׼���䣬ƽ�ȸ���Ŀ�꣬����Ŀ�ꡢ�ⶨ���룬���񹤿�ԭ�ضԲ������˶�Ŀ����������׹���ԭ�ضԲ������˶�Ŀ�������ʹ�ø�����׼���Կ���Ŀ���������̹�˵������˶��Ͳ���Ŀ�������ҹ�����񹤿���½��ԭ�ض��˶��Ͳ���Ŀ����������񹤿����н�����˶��Ͳ���Ŀ�����";

    /// <summary>
    /// ��ʻ��Ŀ
    /// </summary>
    public string DriverCourse = "���ٵ���ʻ��������ת���ʻ�������ٶȼ�ʻ�����ϼ�ʻ������ͨ������·���ϰ����ʻ������ģ��װ��ƽ̨��ʻ������ģ���·��ʻ";

    /// <summary>
    /// Эͬ��Ŀ
    /// </summary>
    public string SynergyCourse = "������רҵЭͬѵ��";

    /// <summary>
    /// ͨ�ſ�Ŀ
    /// </summary>
    public string MsgCourse = "���̲���̨����ʹ�ã����̲���̨�복��ͨ�������ʹ�ã�ֹͣ��ͨ�ţ����������ն�ָ���������ʹ��";

    /// <summary>
    /// ����ѵ��
    /// </summary>
    public string DrillContent = "�������𲽣��ҵ���ת�䣬�����£�б����ʻ��Խ�����ӣ��ϰ���ͨ�У�Խ����������ֹĿ��������˶�Ŀ���������ʻ������Ŀ�����";
    /// <summary>
    /// ����Эͬѵ��
    /// </summary>
    public string OneSynergyContent = "����Эͬѵ��";
    /// <summary>
    /// Эͬѵ��
    /// </summary>
    public string SynergyContent = "�೵Эͬѵ��";
    /// <summary>
    /// ����
    /// </summary>
    public string weather = "���죬���죬ѩ�죬���죬����";


    /// <summary>
    /// ����-ѵ������ģʽ����Ŀ�����
    /// </summary>
    public string enenvironmentTrain = "ɳĮ";

    /// <summary>
    /// ����-����ģʽ
    /// </summary>
    public string enenvironmentSetUp = "ѩ�أ�ɽ�أ���Ţ���棬ɳĮ������ɽ����ѵ���������У�ѩɽ������";

    /// <summary>
    /// ����-�����������
    /// </summary>
    public string enenvironmentShoot = "ѩ�أ�ɽ�أ���Ţ���棬ɳĮ������ɽ��������ѩɽ������";

    /// <summary>
    /// ����
    /// </summary>  
    public string Allenenvironment = "ѩ�أ�ɽ�أ���Ţ���棬ɳĮ������ɽ��������ѩɽ������";

    /// <summary>
    /// ����Эͬѵ������
    /// </summary>
    public string Shootenenvironment = "ɳĮ";



    /// <summary>
    /// ʱ��
    /// </summary>
    public string time = "00:00��01:00��02:00��03:00��04:00��05:00��06:00��07:00��08:00��09:00��10:00��11:00��12:00��13:00��14:00��15:00��16:00��17:00��18:00��19:00��20:00��21:00��22:00��23:00";

    /// <summary>
    /// ��Ҫ�·��Ŀ�Ŀ
    /// </summary>
    public SchemeVo SchemeVo;

    /// <summary>
    /// ��Ҫ��ɾ���ļ�¼
    /// </summary>
    public TrainingRecordVo NeedToDeleteRecord;

    /// <summary>
    /// ��Ŀѵ�����賵��
    /// </summary>
    public int TraingCarCount = 0;

    /// <summary>
    /// ��Ҫ����ѵ����ѧԱ
    /// </summary>
    public List<StudentVo> trainStus = new List<StudentVo>();

    /// <summary>
    /// Эͬѵ��ʱ������ѵ���ĳ�����Լ����ϵ�ѵ����Ա
    /// </summary>
    public Dictionary<int, List<StudentVo>> SynergyTraingDic = new Dictionary<int, List<StudentVo>>();

    /// <summary>
    /// �����˻�
    /// </summary>
    public Dictionary<int, AccountVo> accountDic = new Dictionary<int, AccountVo>();

    /// <summary>
    /// ����ѧԱ
    /// </summary>
    public Dictionary<int, StudentVo> StudentDic = new Dictionary<int, StudentVo>();

    /// <summary>
    /// ���з���
    /// </summary>
    public Dictionary<int, SchemeVo> schemeDic = new Dictionary<int, SchemeVo>();

    /// <summary>
    /// ����ѵ����¼
    /// </summary>
    public Dictionary<int, TrainingRecordVo> TRecordDic = new Dictionary<int, TrainingRecordVo>();

    /// <summary>
    /// ������Ŀ����
    /// </summary>
    public List<UnityEngine.GameObject> Objects = new List<UnityEngine.GameObject>();


    /// <summary>
    /// ��ȡѧԱ�б�
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
    /// ����ѧԱ
    /// </summary>
    public List<StudentVo> UpdateStudentDic()
    {
        StudentDic.Clear();
        DBManager.Getins().LoadPares<StudentVo>(TableConst.STUDENTTABLE);
        return GetStudentList();
    }

    /// <summary>
    /// ���¿�Ŀ����
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
    /// ���¿�Ŀ����
    /// </summary>
    public List<SchemeVo> GetSchemeList()
    {
        List<SchemeVo> schemes = new List<SchemeVo>();
        schemes = schemeDic.Values.ToList();

        return schemes.FindAll((SchemeVo Item) => { return Item.IsDelete == IsDeleteType.Use; });
    }


    /// <summary>
    /// ����ѵ����¼
    /// </summary>
    public List<TrainingRecordVo> UpdateTRecordDic()
    {
        TRecordDic.Clear();
        DBManager.Getins().LoadPares<TrainingRecordVo>(TableConst.TRAININGRECORDTABLE);
        return TRecordDic.Values.ToList();
    }

    /// <summary>
    /// ����ѵ����¼
    /// </summary>
    public List<TrainingRecordVo> GetTRecordDic()
    {
        return TRecordDic.Values.ToList();
    }

    /// <summary>
    /// ��ѯ�������Ƿ��д˷���
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
    /// ��ѯ�������Ƿ��д˷���,������
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
    /// ��ѯ�������Ƿ��д�ѧԱ��
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
    /// Ѱ�ҷ���ID���
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
    /// Ѱ��ѧԱID���
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
    /// �����Ƿ����б��
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
    /// �����Ƿ����б��
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
