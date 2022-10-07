namespace Whatever
{
    public class Driver : UnityEngine.MonoBehaviour
    {
        public float clutch;//离合器
        public float throttle;//油门
        public float brake;//刹车
        public float wheel;//方向盘

        private float maxRotateSpeed = 40;
        public static float maxSpeed = 15;
        private float deltaRotation;
        private float deltaSpeed;
        private UnityEngine.Vector3 deltaPosition;

        private UnityEngine.Transform pTransform;
        private string mName;
        public  bool bOwner;

        private void OnDestroy()
        {
            NetworkBroadcast.RemoveAction(mName);
        }

        private void Awake()
        {
            mName = GetType().Name;

            NetworkBroadcast.AppendAction(mName, DriverNet.ReceiveBroadcastMessage);
        }

        void Start()
        {
            bOwner = Common.sceneName.Contains(mName);
            pTransform = transform.parent;
            SetPositionFlag(0);
            SetRotationFlag(0);
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
                    DriverNet.SendBroadcastMessage();
                }

            }

        }

        void HandleKeyboard()
        {
            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.UpArrow))
            {
                deltaSpeed = maxSpeed * UnityEngine.Time.deltaTime;

                SetPositionFlag(deltaSpeed);
            }
            else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.DownArrow))
            {
                deltaSpeed = -maxSpeed * UnityEngine.Time.deltaTime;

                SetPositionFlag(deltaSpeed);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.UpArrow))
            {
                deltaSpeed = 0;

                SetPositionFlag(deltaSpeed);
            }
            else if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.DownArrow))
            {
                deltaSpeed = 0;

                SetPositionFlag(deltaSpeed);
            }

            if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.LeftArrow))
            {
                deltaRotation = -maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationFlag(deltaRotation);
            }
            else if (UnityEngine.Input.GetKeyDown(UnityEngine.KeyCode.RightArrow))
            {
                deltaRotation = maxRotateSpeed * UnityEngine.Time.deltaTime;
                SetRotationFlag(deltaRotation);
            }

            if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.LeftArrow))
            {
                deltaRotation = 0;
                SetRotationFlag(0);
            }
            else if (UnityEngine.Input.GetKeyUp(UnityEngine.KeyCode.RightArrow))
            {
                deltaRotation = 0;
                SetRotationFlag(0);
            }

        }

        void HandleTransform()
        {
            pTransform.position = pTransform.position + DriverNet.positionFlag*pTransform.forward;
            pTransform.rotation *= UnityEngine.Quaternion.AngleAxis(DriverNet.rotationFlag,UnityEngine.Vector3.up);

        }

        public static void SetPositionFlag(float point)
        {
            DriverNet.positionFlag = point;
        }

        public static void SetRotationFlag(float point)
        {
            DriverNet.rotationFlag = point;
        }

    }

}