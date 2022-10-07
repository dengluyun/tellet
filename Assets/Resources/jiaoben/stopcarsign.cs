using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class stopcarsign : MonoBehaviour
{
    public GameObject mysign;
    public Vector3 a;
    public Quaternion b;
    public GameObject car;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
         a = car.transform.position; 
         b = new Quaternion(0, 0, 0, 0);
    }

    public void stopsign()
    {
        GameObject Sphere = GameObject.Instantiate(mysign, a, b) as GameObject;

        Dictionary<byte, object> sign = new Dictionary<byte, object>();
        //sign.Add((byte)ParametersCode.OfficerNum, "SIGN");
        //PhotonEngine.Instance.SendOperation(OperationCode.ManPose, sign);
    }
}
