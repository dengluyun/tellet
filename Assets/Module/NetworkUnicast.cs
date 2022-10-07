namespace Whatever
{
    public class NetworkUnicast
    {
        public static string packString = "";

        public static System.Net.Sockets.Socket unicastSocket;

        public static System.Net.IPEndPoint serverIpEndPoint;
        public static bool isServer;
        public static System.Net.EndPoint anyEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);
        public static System.Net.EndPoint remoteEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Any, 0);

        public static System.Net.Sockets.SocketFlags socketFlag = System.Net.Sockets.SocketFlags.None;

        public static byte[] packBuff = new byte[1024];
        public static System.AsyncCallback unicastCallback;
        public static System.Collections.Generic.Dictionary<string, System.Action<string, System.Net.EndPoint>> unicastActions = new System.Collections.Generic.Dictionary<string, System.Action<string, System.Net.EndPoint>>();
        public static void RemoveAction(string name)
        {
            unicastActions.Remove(name);
        }

        public static void AppendAction(string name, System.Action<string, System.Net.EndPoint> action)
        {
            unicastActions.Add(name, action);
        }

        public static void Close()
        {
            if (unicastSocket != null)
            {
                unicastSocket.Close();
                unicastSocket = null;
            }
            //clientSocket.Close();
        }

        public static void Init()
        {
            string[] strs = ConfigurationReader.GetConfigFile("PointServer.txt").Split('|');
            var sserverStr = strs[0];
            var serverStr = strs[1];
            var serverPointStr = serverStr.Split(':');
            string serverIp = serverPointStr[0];
            string serverPort = serverPointStr[1];
            Logger.Log("NetworkUnicast config server ip: " + serverIp + ", port:" + serverPort);

            var sPort = System.Convert.ToInt32(serverPort);
            serverIpEndPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse(serverIp), sPort);
            InitUnicastSocket();

            Logger.LogWarning("NetworkUnicast init over");
        }

        private static void InitUnicastSocket()
        {
            var addressFamily = System.Net.Sockets.AddressFamily.InterNetwork;
            var socketType = System.Net.Sockets.SocketType.Dgram;
            var protocalType = System.Net.Sockets.ProtocolType.Udp;
            unicastCallback = new System.AsyncCallback(ReceiveUnicastPack);
            unicastSocket = new System.Net.Sockets.Socket(addressFamily, socketType, protocalType);
            if (isServer)
                unicastSocket.Bind(serverIpEndPoint);
            unicastSocket.BeginReceiveFrom(packBuff, 0, packBuff.Length, socketFlag, ref anyEndPoint, unicastCallback, unicastSocket);
        }

        public static void SendUnicastPack(string message, System.Net.EndPoint endPoint = null)
        {
            Logger.LogWarning(">>>>>>>> ----------------------------------------------------- start unicast ---------------------------------------------------------- >>>>>>>>");
            Logger.Log("send "  + " msg:" + message);
            byte[] bytes = System.Text.Encoding.Default.GetBytes(message);
            unicastSocket.SendTo(bytes, endPoint == null ? serverIpEndPoint : endPoint);
            Logger.LogWarning(">>>>>>>> ----------------------------------------------------- end unicast ---------------------------------------------------------- >>>>>>>>");
        }
        public static void ReceiveUnicastPack(System.IAsyncResult asyncResult)
        {
            Logger.LogWarning("<<<<<<<<----------------------------------------------------- start unicast -------------------------------------------------------- <<<<<<<<");
            if (!asyncResult.IsCompleted || unicastSocket == null)
                return;

            try
            {
                int len = unicastSocket.EndReceiveFrom(asyncResult, ref remoteEndPoint);

                Logger.Log("recieve unicast from:" + remoteEndPoint.ToString() + ",length:" + len);
                var msg = System.Text.Encoding.Default.GetString(packBuff, 0, len);
                Logger.Log("pack data:" + msg);
                packString = msg;
                var sstrs = msg.Split(':');
                var nm = sstrs[0];
                var paras = sstrs[1];
                var act = unicastActions[nm];
                act?.Invoke(paras, remoteEndPoint);
            }
            catch (System.Exception ex)
            {
                Logger.LogError(ex);
            }
            finally
            {
                Logger.LogWarning("NetworkUnicast remoteIpEndPoint " + remoteEndPoint.ToString());
                unicastSocket.BeginReceiveFrom(packBuff, 0, packBuff.Length, socketFlag, ref anyEndPoint, unicastCallback, unicastSocket);
            }
            Logger.LogWarning("<<<<<<<<----------------------------------------------------- end unicast -------------------------------------------------------- <<<<<<<<");
        }

    }

}