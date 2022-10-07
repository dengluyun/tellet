namespace Whatever
{
    public class NetworkBroadcast
    {
        public static string packString = "";

        public static System.Net.Sockets.Socket broadcastSocket;

        private static System.Net.IPAddress address = System.Net.IPAddress.Parse("255.255.255.255");
        private static int port;
        public static System.Net.IPEndPoint endPoint;


        public static System.Net.EndPoint zeroEndPoint;
        public static string localIPAddress;

        public static System.Net.EndPoint remoteEndPoint;

        public static System.Net.Sockets.SocketFlags socketFlag = System.Net.Sockets.SocketFlags.None;

        private static byte[] packBuff = new byte[1024];
        private static System.AsyncCallback broadcastCallback;
        private static System.Collections.Generic.Dictionary<string, System.Action<string>> broadcastActions = new System.Collections.Generic.Dictionary<string, System.Action<string>>();

        public static void RemoveAction(string name)
        {
            broadcastActions.Remove(name);
        }

        public static void AppendAction(string name, System.Action<string> action)
        {
            broadcastActions.Add(name, action);
        }

        public static void Close()
        {
            if (broadcastSocket != null)
            {
                broadcastSocket.Close();
                broadcastSocket = null;
            }

            broadcastActions.Clear();
            broadcastActions = null;
            //clientSocket.Close();
        }

        public static void Init()
        {
            string portStr = ConfigurationReader.GetConfigFile("Port.txt");
            Logger.Log("NetworkBroadcast config port: " + portStr);
            port = int.Parse(portStr);
            endPoint = new System.Net.IPEndPoint(address, port);
            zeroEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, port);
            remoteEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

            var addressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            var socketType = System.Net.Sockets.SocketType.Dgram;
            var protocalType = System.Net.Sockets.ProtocolType.Udp;
            var socketOptionLevel = System.Net.Sockets.SocketOptionLevel.Socket;
            var socketOptionName = System.Net.Sockets.SocketOptionName.Broadcast;
            broadcastCallback = new System.AsyncCallback(ReceiveBroadcastPack);
            broadcastSocket = new System.Net.Sockets.Socket(addressFamily, socketType, protocalType);
            broadcastSocket.SetSocketOption(socketOptionLevel, socketOptionName, 1);
            broadcastSocket.Bind(zeroEndPoint);
            broadcastSocket.BeginReceiveFrom(packBuff, 0, packBuff.Length, socketFlag, ref remoteEndPoint, broadcastCallback, broadcastSocket);
            localIPAddress = Tools.GetLocalIPv4();
            Logger.Log("local address:" + localIPAddress);
            Logger.LogWarning("NetworkBroadcast init over");
        }

        public static void SendBroadcastPack(string message)
        {
            Logger.LogWarning(">>>>>>>> ----------------------------------------------------- start broadcast ---------------------------------------------------------- >>>>>>>>");
            byte[] bytes = System.Text.Encoding.Default.GetBytes(message);
            Logger.Log(message);
            broadcastSocket.SendTo(bytes, endPoint);
            Logger.LogWarning(">>>>>>>> ----------------------------------------------------- end broadcast ---------------------------------------------------------- >>>>>>>>");
        }
        public static void ReceiveBroadcastPack(System.IAsyncResult asyncResult)
        {
            Logger.LogWarning("<<<<<<<<----------------------------------------------------- start broadcast -------------------------------------------------------- <<<<<<<<");

            if (!asyncResult.IsCompleted || broadcastSocket == null)
                return;

            try
            {
                var localEp = broadcastSocket.LocalEndPoint;
                int len = broadcastSocket.EndReceiveFrom(asyncResult, ref remoteEndPoint);
                var str = remoteEndPoint.ToString();
                if (str.Contains(localIPAddress))
                {
                }
                else
                {
                    Logger.Log("recieve broadcast from:" + str + ",length:" + len);
                    var msg = System.Text.Encoding.Default.GetString(packBuff, 0, len);
                    Logger.Log("pack data:" + msg);
                    packString = msg;
                    var sstrs = msg.Split(':');
                    var nm = sstrs[0];
                    var paras = sstrs[1];
                    var act = broadcastActions[nm];
                    act?.Invoke(paras);
                }

            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                Logger.LogWarning("NetworkBroadcast remoteIpEndPoint " + remoteEndPoint);
                broadcastSocket.BeginReceiveFrom(packBuff, 0, packBuff.Length, socketFlag, ref remoteEndPoint, broadcastCallback, broadcastSocket);
            }
            Logger.LogWarning("<<<<<<<<----------------------------------------------------- end broadcast-------------------------------------------------------- <<<<<<<<");
        }

    }

}