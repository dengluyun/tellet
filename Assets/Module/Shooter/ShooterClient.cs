namespace Whatever
{
    public class ShooterClient : UnityEngine.MonoBehaviour
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
            NetworkUnicast.AppendAction(NetworkProtocal.matchAck, ReceiveStartAck);
            NetworkUnicast.AppendAction(NetworkProtocal.pauseAck, ReceivePauseAck);
            NetworkUnicast.AppendAction(NetworkProtocal.resumeAck, ReceiveResumeAck);
            NetworkUnicast.AppendAction(NetworkProtocal.endAck, ReceiveEndAck);

            Loader.GetInstance().Load();
            SendStartReq();
            Common.bStarted = playWithNoServer;
        }
        public static void SendStartReq()
        {
            NetworkUnicast.SendUnicastPack(NetworkProtocal.matchReq + ":" + Common.sceneName);
        }
        public static void ReceiveStartAck(string message, System.Net.EndPoint remoteEndPoint)
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