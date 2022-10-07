namespace Whatever
{
    public class Nexus : UnityEngine.MonoBehaviour
    {
        private static Nexus _instance;

        public static Nexus GetInstance()
        {
            return _instance;
        }

        public static System.Collections.Generic.Dictionary<string, Agent> agentMap = new System.Collections.Generic.Dictionary<string, Agent>();
        public static int maxMemberCount = 100;

        public static string headerName = "Header";
        public static string driverName = "Driver";
        public static string shooterName = "Shooter";

        private static string singleName = string.Empty;

        // ========================= net ===============================//
        void ReceiveDriverSubject1LaunchReq(string message, System.Net.EndPoint endPoint)
        {
            int descore = int.Parse(message);
            NexusDriverData.Deduct((NetworkProtocal.driverSubject1DeductType)descore);
        }

        void ReceiveDriverSubject1ShiftReq(string message, System.Net.EndPoint endPoint)
        {
            int descore = int.Parse(message);
            NexusDriverData.Deduct((NetworkProtocal.driverSubject1DeductType)descore);
        }

        void ReceiveDriverSubject1StopleaveReq(string message, System.Net.EndPoint endPoint)
        {
            int descore = int.Parse(message);
            NexusDriverData.Deduct((NetworkProtocal.driverSubject1DeductType)descore);
        }

        void ReceiveMatchReq(string message, System.Net.EndPoint endPoint)
        {
            var who = message;

            if (!who.Contains(headerName) && !who.Contains(driverName) && !who.Contains(shooterName))
            {
                Logger.LogWarning("data format error!");
                return;
            }

            if (agentMap.ContainsKey(who))
            {
                Agent agent;
                agentMap.TryGetValue(who, out agent);
                agent.remoteEndPoint = endPoint;

                if (agentMap.Count == maxMemberCount)
                {
                    SendStartAck(agent, maxMemberCount);
                }

            }
            else
            {
                if (string.IsNullOrEmpty(singleName) || singleName.Contains(who))
                {
                    var agt = new Agent();
                    agt.name = who;
                    agt.remoteEndPoint = endPoint;
                    agt.reqMessage = message;
                    agentMap.Add(who, agt);
                }
                else
                {

                }

            }

        }

        void SendStartAck(Agent agent, int memberCount)
        {
            agent.ackMessage = NetworkProtocal.matchAck + ':' + memberCount;
            NetworkUnicast.SendUnicastPack(agent.ackMessage, agent.remoteEndPoint);
        }

        //void ReceivePauseReq(string message, System.Net.EndPoint endPoint)
        //{
        //}

        void SendPauseAck(Agent agent)
        {
            agent.ackMessage = NetworkProtocal.pauseAck;
            NetworkUnicast.SendUnicastPack(agent.ackMessage, agent.remoteEndPoint);
        }

        //void ReceiveResumeReq(string message, System.Net.EndPoint endPoint)
        //{
        //}

        void SendResumeAck(Agent agent)
        {
            agent.ackMessage = NetworkProtocal.resumeAck;
            NetworkUnicast.SendUnicastPack(agent.ackMessage, agent.remoteEndPoint);
        }

        //void ReceiveEndReq(string message, System.Net.EndPoint endPoint)
        //{
        //}

        void SendEndAck(Agent agent)
        {
            agent.ackMessage = NetworkProtocal.endAck;
            NetworkUnicast.SendUnicastPack(agent.ackMessage, agent.remoteEndPoint);
        }
        // ========================= net ===============================//
        public static void Matching(bool isSingle, string name)
        {

            singleName = isSingle ? name : string.Empty;
            maxMemberCount = isSingle ? 1 : 3;
        }

        public void Together()
        {
            foreach (var kv in agentMap)
            {
                SendStartAck(kv.Value, maxMemberCount);
            }
            Loader.GetInstance().Load();
        }

        public void PauseTrain()
        {
            foreach (var kv in agentMap)
            {
                SendPauseAck(kv.Value);
            }
            Common.bStarted = false;
        }

        public void ResumeTrain()
        {
            foreach (var kv in agentMap)
            {
                SendResumeAck(kv.Value);
            }
            Common.bStarted = true;

        }

        public void StartSingleTrain()
        {
            //Common.bStarted = true;
            maxMemberCount = 1;
        }

        public void StartMultiTrain()
        {
            //Common.bStarted = true;
            maxMemberCount = 2;
        }

        public void StartTrain()
        {
            //Common.bStarted = true;
            maxMemberCount = 4;
        }

        public void EndTrain()
        {
            foreach (var kv in agentMap)
            {
                SendEndAck(kv.Value);
            }
            Common.bStarted = false;
            //todo
        }

        void OnDestroy()
        {
            NetworkUnicast.RemoveAction(NetworkProtocal.driverSubject1StopleaveReq);
            NetworkUnicast.RemoveAction(NetworkProtocal.driverSubject1ShiftReq);
            NetworkUnicast.RemoveAction(NetworkProtocal.driverSubject1LaunchReq);
            NetworkUnicast.RemoveAction(NetworkProtocal.matchReq);
        }

        void Awake()
        {
            _instance = this;
            NetworkUnicast.AppendAction(NetworkProtocal.matchReq, ReceiveMatchReq);
            NetworkUnicast.AppendAction(NetworkProtocal.driverSubject1LaunchReq, ReceiveDriverSubject1LaunchReq);
            NetworkUnicast.AppendAction(NetworkProtocal.driverSubject1ShiftReq, ReceiveDriverSubject1ShiftReq);
            NetworkUnicast.AppendAction(NetworkProtocal.driverSubject1StopleaveReq, ReceiveDriverSubject1StopleaveReq);

            agentMap.Clear();
        }

        private void Start()
        {

        }

        private void Update()
        {
            if (!Common.bStarted)
            {
                if (agentMap.Count == maxMemberCount)
                {
                    Together();
                    Common.bStarted = true;
                }

            }

        }

    }

}