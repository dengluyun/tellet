using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carandpeopleGamobject : MonoBehaviour
{
   
  public   Transform car;    
  public Transform people;
    public static  carandpeopleGamobject instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
