namespace Whatever
{
    public class DriverClient : UnityEngine.MonoBehaviour
    {
        public bool playWithNoServer;

        private void OnDestroy()
        {
            NetworkUnicast.RemoveAction(NetworkProtocal.endAck);
            NetworkUnicast.RemoveAction(NetworkProtocal.resumeAck);
            NetworkUnicast.RemoveAction(NetworkProtocal.pauseAck);
            NetworkUnicast.RemoveAction(NetworkProtocal.matchAck);
        }

        void Start()
        {
            NetworkUnicast.AppendAction(NetworkProtocal.matchAck, ReceiveMatchAck);
            NetworkUnicast.AppendAction(NetworkProtocal.pauseAck, ReceivePauseAck);
            NetworkUnicast.AppendAction(NetworkProtocal.resumeAck, ReceiveResumeAck);
            NetworkUnicast.AppendAction(NetworkProtocal.endAck, ReceiveEndAck);

            Loader.GetInstance().Load();
            SendMatchReq();
            Common.bStarted = playWithNoServer;
        }
        public static void SendMatchReq()
        {
            NetworkUnicast.SendUnicastPack(NetworkProtocal.matchReq + ":" + Common.sceneName);
        }
        public static void ReceiveMatchAck(string message, System.Net.EndPoint remoteEndPoint)
        {
            Common.bStarted = true;
        }

        public static void ReceivePauseAck(string message, System.Net.EndPoint remoteEndPoint)
        {
            Common.bStarted = false;
        }
        public static void ReceiveResumeAck(string message, System.Net.EndPoint remoteEndPoint)
        {
            Common.bStarted = true;
        }
        public static void ReceiveEndAck(string message, System.Net.EndPoint remoteEndPoint)
        {
            Common.bStarted = false;
        }

    }

}