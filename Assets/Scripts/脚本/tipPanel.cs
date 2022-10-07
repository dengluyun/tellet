using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class tipPanel : MonoBehaviour
{

    public  static tipPanel instance;  
    // Start is called before the first frame update
    void Start()
    {
        instance = this;   
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showTipPanel(string tipMessage)
    {
        if (!transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(true);
            gameObject .transform.GetChild(0).GetChild(0).GetComponent<Text>().text = tipMessage;
        }
    }


    public void CloseTipPanel()
    {
        if (transform.GetChild(0).gameObject.activeSelf)
        {
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
