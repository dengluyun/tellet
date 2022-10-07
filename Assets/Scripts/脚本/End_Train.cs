using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class End_Train : MonoBehaviour
{
   
     public static End_Train  instance;
    // Start is called before the first frame update
    void Start()
    {
        
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void End_Message()
    {
        Dictionary<byte, object> end_message = new Dictionary<byte, object>();
        //end_message.Add((byte)ParametersCode.end_Training, "end");
        //PhotonEngine.Instance.SendOperation(OperationCode.End_Train, end_message);
        Debug.Log("结束训练指令发送完毕");
    }



}
