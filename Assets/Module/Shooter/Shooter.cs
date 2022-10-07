namespace Whatever
{
    public class Shooter : UnityEngine.MonoBehaviour
    {
        public static string moduleName;
        public static bool needControl;
        private float maxRotateSpeed = 60;
        private float deltaRotation;

        public UnityEngine.Transform fort;
        public UnityEngine.Transform gun;
        private string mName;
        public  bool bOwner;

        private void OnDestroy()
        {
            NetworkBroadcast.RemoveAction(mName);
        }

        private void Awake()
        {
            mName = GetType().Name;

            NetworkBroadcast.AppendAction(mName, ShooterNet.ReceiveBroadcastMessage);
        }

        void Start()
        {
            bOwner = Common.sceneName.Contains(mName);

            SetRotationUpDownFlag(0);
            SetRotationLeftRightFlag(0);
        }

        private float elapsedTime;
        void Update()
        {
            if (!Common.bStarted)
                return;

            HandleTransform();
            if (bOwner)
            {
                HandleKeyboard();
                elapsedTime += UnityEngine.Time.deltaTime;
                if (elapsedTime > Common.interval)
                {
                    elapsedTime = 0;
                    ShooterNet.SendBroadcastMessage();
                }

            }

        }

        void HandleKeyboard()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
            {
                deltaRotation = maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationUpDownFlag(deltaRotation);
            }
            else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow))
            {
                deltaRotation = maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationUpDownFlag(-deltaRotation);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.UpArrow))
            {
                deltaRotation = 0;
                SetRotationUpDownFlag(deltaRotation);
            }
            else if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.DownArrow))
            {
                deltaRotation = 0;
                SetRotationUpDownFlag(deltaRotation);
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                deltaRotation = maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationLeftRightFlag(-deltaRotation);
            }
            else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                deltaRotation = maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationLeftRightFlag(deltaRotation);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.LeftArrow))
            {
                deltaRotation = 0;
                SetRotationLeftRightFlag(deltaRotation);
            }
            else if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.RightArrow))
            {
                deltaRotation = 0;
                SetRotationLeftRightFlag(deltaRotation);
            }

        }

        void HandleTransform()
        {
            fort.rotation *= UnityEngine.Quaternion.AngleAxis(ShooterNet.rotationLeftRightFlag, UnityEngine.Vector3.up);
            gun.rotation *= UnityEngine.Quaternion.AngleAxis(ShooterNet.rotationUpDownFlag, UnityEngine.Vector3.right);
            //gun.Rotate(UnityEngine.Vector3.right, rotationUpDownFlag / Common.preTime * UnityEngine.Time.deltaTime);
        }

        public static void SetRotationLeftRightFlag(float flag)
        {
            ShooterNet.rotationLeftRightFlag = flag;
        }

        public static void SetRotationUpDownFlag(float flag)
        {
            ShooterNet.rotationUpDownFlag = flag;
        }

    }

}