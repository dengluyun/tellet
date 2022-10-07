namespace Whatever
{
    public class DriverNet
    {
        public static float positionFlag;
        public static float rotationFlag;
        // ====================================== broadcast =====================================//
        public static  void ReceiveBroadcastMessage(string message)
        {
            //Logger.Log("DriverNet ReceiveMessage");
            //Logger.Log(message);
            var strs = message.Split('|');
            var posStr = strs[0];
            var rotStr = strs[1];
            positionFlag = float.Parse(posStr);
            rotationFlag = float.Parse(rotStr);
        }

        public static void SendBroadcastMessage()
        {
            var msg = positionFlag.ToString() + "|" + rotationFlag.ToString();
            NetworkBroadcast.SendBroadcastPack(Common.sceneName + ":"+msg);
        }
        // ====================================== broadcast =====================================//
        
        // ====================================== unicast =====================================//

        // ====================================== unicast =====================================//
    }

}