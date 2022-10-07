namespace Whatever
{
    public class Network
    {
        public static void Close()
        {
            if(NetworkBroadcast.broadcastSocket!=null)
            {
                NetworkBroadcast.broadcastSocket.Close();
                NetworkBroadcast.broadcastSocket = null;
            }
            if (NetworkUnicast.unicastSocket != null)
            {
                NetworkUnicast.unicastSocket.Close();
                NetworkUnicast.unicastSocket = null;
            }

        }

        public static void Init()
        {
            NetworkBroadcast.Init();
            NetworkUnicast.Init();
            Logger.LogWarning("Network  init over");
        }


        public static void SendUnicastPack(string message)
        {
            NetworkUnicast.SendUnicastPack(message);
        }

        public static void SendBroadcastPack(string message)
        {
            NetworkBroadcast.SendBroadcastPack(message);
        }

    }

}