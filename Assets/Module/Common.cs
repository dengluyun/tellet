namespace Whatever
{
    public class Common : UnityEngine.MonoBehaviour
    {
        public static string sceneName;
        public static float preTime = 5f;
        //public static float interval = 0.05f;
        public static float interval = 0.0f;
        public static bool bStarted;
        public static bool serverOrClient;
        void OnDestroy()
        {
            Network.Close();
        }

        void Awake()
        {
            DontDestroyOnLoad(gameObject);
            sceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
            Logger.Log("Scene Awake SceneName:" + sceneName);
            serverOrClient = sceneName.Contains("Nexus");
            if (serverOrClient)
                NetworkUnicast.isServer = true;
            else
                NetworkUnicast.isServer = false;

            Network.Init();
            //bStarted = true;
        }

    }

}