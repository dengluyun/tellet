namespace Whatever
{
    public class Logger:UnityEngine.MonoBehaviour
    {
        private static bool openLog = true;
        //private static string prefix = "-------- ";
        private static string prefix = "----------------";
        
        private static bool openWarning = true;

        public static void Log(object message)
        {
            if(openLog)
            {
                var msg = prefix + message.ToString();
                UnityEngine.Debug.Log(msg);
            }

        }

        public static void LogWarning(object message)
        {
            if (openWarning)
            {
                UnityEngine.Debug.LogWarning(message);
            }
        }

        public static void LogError(object message)
        {
            if (openLog)
                UnityEngine.Debug.LogError(message);
        }

        public UnityEngine.UI.InputField messageToSendInputField;
        public UnityEngine.UI.Text messageToSend;
        public UnityEngine.UI.Text messageNewestReceived;


        private bool broadcastOrUnicast;

        private bool bShowPanel;
        public UnityEngine.Transform panel;
        public void TogglePanel()
        {
            bShowPanel = !bShowPanel;
            panel.gameObject.SetActive(bShowPanel);
        }

        public void OnClickSend()
        {
            if(messageToSendInputField != null)
            {
                var msg = messageToSendInputField.text;
                messageToSend.text = msg;
                broadcastOrUnicast = false;
                Network.SendUnicastPack(msg);
            }

        }

        public void OnClickSendBroadcast()
        {
            if (messageToSendInputField != null)
            {
                var msg = messageToSendInputField.text;
                messageToSend.text = msg;
                broadcastOrUnicast = true;
                Network.SendBroadcastPack(msg);
            }

        }

        void Update()
        {
            if(broadcastOrUnicast)
                messageNewestReceived.text = NetworkBroadcast.packString;
            else
                messageNewestReceived.text = NetworkUnicast.packString;

        }

    }

}