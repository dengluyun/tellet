namespace Whatever
{
    public class Loader : UnityEngine.MonoBehaviour
    {
        private static Loader _instance = null;
        public static Loader GetInstance()
        {
            return _instance;
        }

        public UnityEngine.Transform sceneRoot;
        public UnityEngine.Transform dRoot;

        private UnityEngine.Transform tankPrefab;
        private UnityEngine.Transform scenePrefab;

        bool isLoaded;
        public void Unload()
        {
            Destroy(sceneRoot);
            Destroy(dRoot);
            
            isLoaded = false;
        }

        public void Load()
        {
            if (isLoaded)
                return;

            var tank = Instantiate(tankPrefab);
            tank.SetParent(dRoot);
            tank.localPosition = UnityEngine.Vector3.zero;

            var scene = Instantiate(scenePrefab);
            scene.SetParent(sceneRoot);
            scene.localPosition = new UnityEngine.Vector3(280, -34.5f, 0);

            isLoaded = true;
        }

        private void OnDestroy()
        {

        }

        void Awake()
        {

            tankPrefab = UnityEngine.Resources.Load<UnityEngine.Transform>("Tank04");
            scenePrefab = UnityEngine.Resources.Load<UnityEngine.Transform>("xunlianchang");
            _instance = this;
            
            isLoaded = false;
        }

        // Update is called once per frame
        void Update()
        {

        }

    }

}