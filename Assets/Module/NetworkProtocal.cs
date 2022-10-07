namespace Whatever
{
    public class NetworkProtocal
    {
        public static string matchReq = "MatchReq";//Match:Header    终端匹配请求   有三种类型Header Driver  Shooter
        public static string matchAck = "MatchAck";//Match:0   0代表第一次匹配  1终端类型不匹配 2重连匹配
        //public static string startAck = "StartAck";//Start:2  2代表匹配齐全人数 1 单项训练   2 协同训练

        //public static string pauseReq = "PauseReq";//暂停训练
        public static string pauseAck = "PauseAck";//暂停训练

        //public static string resumeReq = "ResumeReq";//暂停训练
        public static string resumeAck = "ResumeAck";//暂停训练

        //public static string endReq = "EndReq";//结束训练
        public static string endAck = "EndAck";//

        public static string beatheartReq = "BeatheartReq";
        public static string beatheartAck = "BeatheartAck";
        //=============================== driver subject 1 驾驶舱科目1 =================================//
        public static string driverSubject1LaunchReq = "DriverSubject1LaunchReq";
        public static string driverSubject1ShiftReq = "DriverSubject1ShiftReq";
        public static string driverSubject1StopleaveReq = "DriverSubject1StopleaveReq";

        public enum driverSubject1DeductType
        {
            validLaunch,//未按规定模式起动车辆1次扣1分
            validResetShift,//起动时换挡手柄未置于空挡1次扣1分
            unLaunch,//发动不成功1次扣2分

            validShift,//未按规定换挡模式换挡起车1次扣1分
            wrongShift,//挂错档位1次扣2分
            
            validFlow,//操作流程不正确扣5分
        }

        //=============================== subject 1 科目1 =================================//


        public static string allData = "T:n:H:n:D:n:S:n:E:n:O:n";//T坦克数据，H车长数据，D驾驶员数据，S炮手数据, E敌人数据,O物体数据,    n代表空数据
        //n数据格式 0,0,0|0,0,0|0,0,0

        //=============================== common =================================//
        public static string readyData = "Ready:Header";//Header|Driver|Shooter

        public static string updateTankPosAndForward = "tt:0.01|0.01|0.01";
        public static string updateEnermyState = "es:sthName";//some body killed  or decrease hp
        public static string updateObstacleState = "os:sbdName";//some thing destroyed or decrease hp

        //=============================== header =================================//
        public static string headerNoData = "Header";

        //=============================== driver =================================//
        public static string driverNoData = "Driver";
        public static string driverChangePos = "Driver:0.1|0.1|0.1";

        //=============================== shooter =================================//
        public static string shooterNoData = "Shooter";
        public static string shooterHitTarget = "Shooter:enermyName|100";//decrease 100 hp or killed directly

        //=============================== data =================================//

    }

}