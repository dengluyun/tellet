using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clock1 : MonoBehaviour
{
    float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //开始运行
    public void start_Clock()
    {
        time += Time.deltaTime;

    
    }





}
