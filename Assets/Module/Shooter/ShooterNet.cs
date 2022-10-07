namespace Whatever
{
    public class ShooterNet
    {
        public static float rotationLeftRightFlag;
        public static float rotationUpDownFlag;

        public static void ReceiveBroadcastMessage(string message)
        {
            var strs = message.Split('|');
            var rot1Str = strs[0];
            var rot2Str = strs[1];
            rotationLeftRightFlag = float.Parse(rot1Str);
            rotationUpDownFlag = float.Parse(rot2Str);
        }

        public static void SendBroadcastMessage()
        {
            var msg1 = rotationLeftRightFlag.ToString();
            var msg2 = rotationUpDownFlag.ToString();
            var msg = msg1 + '|' + msg2;
            NetworkBroadcast.SendBroadcastPack(Common.sceneName + ":" + msg);
        }

        // ====================================== unicast =====================================//


        // ====================================== unicast =====================================//
    }

}