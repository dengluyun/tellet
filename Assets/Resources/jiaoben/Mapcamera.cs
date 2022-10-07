using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mapcamera : MonoBehaviour
{
    public GameObject mapcamera;
    public GameObject car;
    public GameObject sign;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        mapcamera.transform.position = new Vector3(car.transform.position.x, transform.position.y, car.transform.position.z);
        sign.transform.position = new Vector3(car.transform.position.x, sign.transform.position.y, car.transform.position.z);
    }
}
