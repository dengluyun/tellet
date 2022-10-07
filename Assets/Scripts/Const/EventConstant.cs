using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 事件常量
/// </summary>
public class EventConstant
{
    /// <summary>
    /// 提示文本应提示内容
    /// </summary>
    public const string HINTTEXT_EVENT = "HINTTEXT_EVENT";

    /// <summary>
    /// 在训练方案中选中某一项时
    /// </summary>
    public const string CHOOSEONEOBJECT_EVENT = "CHOOSEONEOBJECT_EVENT";

    /// <summary>
    /// 在学员中选中某一位时
    /// </summary>
    public const string SINGLECHOOSESTUDENT_EVENT = "SINGLECHOOSESTUDENT_EVENT";

    /// <summary>
    /// 展示删除按钮
    /// </summary>
    public const string SHOW_DELETEBTN_EVENT = "SHOW_DELETEBTN_EVENT";

    /// <summary>
    /// 展示学员删除按钮
    /// </summary>
    public const string SHOW_STUDENTDELETEBTN_EVENT = "SHOW_STUDENTDELETEBTN_EVENT";

    /// <summary>
    /// 删除和添加项目方案时执行
    /// </summary>
    public const string DELETEOBJECT_EVENT = "DELETEOBJECT_EVENT";

    /// <summary>
    ///  删除和添加学员时执行
    /// </summary>
    public const string DELETESTUDENT_EVENT = "DELETESTUDENT_EVENT";

    /// <summary>
    ///  删除和添加记录时执行
    /// </summary>
    public const string DELETETRAINRECORD_EVENT = "DELETETRAINRECORD_EVENT";

    /// <summary>
    /// 取消删除项目方案时
    /// </summary>
    public const string CANCELDELETEOBJECT_EVENT = "CANCELDELETEOBJECT_EVENT";

    /// <summary>
    /// 取消删除学员时
    /// </summary>
    public const string CANCELDELETESTUDENT_EVENT = "CANCELDELETESTUDENT_EVENT";

    /// <summary>
    /// 修改学生成绩时，通知需要修改的记录
    /// </summary>
    public const string UPDATESTUDENTGRADE_EVENT = "UPDATESTUDENTGRADE_EVENT";

    /// <summary>
    /// 开始训练时，当勾选学员时发送对应的下标
    /// </summary>
    public const string SELETESTUDENTTRAING_EVENT = "SELETESTUDENTTRAING_EVENT";

    /// <summary>
    /// 取消勾选学员时发送对应的下标
    /// </summary>
    public const string UNSELETESTUDENTTRAING_EVENT = "UNSELETESTUDENTTRAING_EVENT";

    /// <summary>
    /// 协同训练时，取消对某一导调台的控制权
    /// </summary>
    public const string CANCELMASTERSTATIONCONTROL_EVENT = "CANCELMASTERSTATIONCONTROL_EVENT";

    /// <summary>
    /// 当主控台发送控制终端时
    /// </summary>
    public const string STARTCONTROL_EVENT = "STARTCONTROL_EVENT";

    /// <summary>
    /// 当主控台取消控制终端时
    /// </summary>
    public const string CANCELCONTROL_EVENT = "CANCELCONTROL_EVENT";

    /// <summary>
    /// 当主控台开始训练时
    /// </summary>
    public const string STARTTRAINCONTROL_EVENT = "STARTTRAINCONTROL_EVENT";

    /// <summary>
    /// 接收来自客户端的成绩
    /// </summary>
    public const string RECEIVE_GRADE_EVENT = "RECEIVE_GRADE_EVENT";

    /// <summary>
    /// 删除所有的方案
    /// </summary>
    public const string DELETE_ALL_PROJECT_EVENT = "DELETE_ALL_PROJECT_EVENT";

    /// <summary>
    /// 接收来自客户端的操作信息
    /// </summary>
    public const string STUDENT_HANDLE_HINT_EVENT = "STUDENT_HANDLE_HINT_EVENT";
}

