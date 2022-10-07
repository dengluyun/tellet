namespace Whatever
{
    public class NexusDriverData
    {

        public static int subject1LaunchScore;
        public static int subject1ShiftScore;
        public static int subject1StopleaveScore;

        public static int subject1TotalScore;

        public static System.Collections.Generic.List<NetworkProtocal.driverSubject1DeductType> deductScoreList = new System.Collections.Generic.List<NetworkProtocal.driverSubject1DeductType>();

        public void Clear()
        {
            deductScoreList.Clear();
            subject1LaunchScore = 5;
            subject1ShiftScore = 5;
            subject1StopleaveScore = 5;

            subject1TotalScore = subject1LaunchScore + subject1ShiftScore + subject1StopleaveScore;
        }

        public static void Deduct(NetworkProtocal.driverSubject1DeductType deductType)
        {
            deductScoreList.Add(deductType);
            switch (deductType)
            {
                case NetworkProtocal.driverSubject1DeductType.validLaunch:
                    subject1LaunchScore -= 1;
                    break;
                case NetworkProtocal.driverSubject1DeductType.validResetShift:
                    subject1LaunchScore -= 1;
                    break;
                case NetworkProtocal.driverSubject1DeductType.unLaunch:
                    subject1LaunchScore -= 2;
                    break;
                case NetworkProtocal.driverSubject1DeductType.validShift:
                    subject1ShiftScore -= 1;
                    break;
                case NetworkProtocal.driverSubject1DeductType.wrongShift:
                    subject1ShiftScore -= 2;
                    break;
                case NetworkProtocal.driverSubject1DeductType.validFlow:
                    subject1StopleaveScore -= 5;
                    break;
            }
            subject1LaunchScore = subject1LaunchScore < 0 ? 0 : subject1LaunchScore;
            subject1ShiftScore = subject1LaunchScore < 0 ? 0 : subject1LaunchScore;
            subject1StopleaveScore = subject1LaunchScore < 0 ? 0 : subject1LaunchScore;

            subject1TotalScore = subject1LaunchScore + subject1ShiftScore + subject1StopleaveScore;
            //RecordVos = DataManager.GetIns().GetTRecordDic();

        }

    }

}