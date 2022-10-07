namespace Whatever
{

    public class Follow : UnityEngine.MonoBehaviour
    {
        public UnityEngine.Transform tank;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void LateUpdate()
        {
            transform.position = new UnityEngine.Vector3(tank.position.x, transform.position.y,tank.position.z);
        }

    }
}