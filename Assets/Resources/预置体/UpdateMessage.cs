using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class UpdateMessage : MonoBehaviour,IPointerDownHandler
{
    double t2 = 0;
    double t1 = 0;
    public void OnPointerDown(PointerEventData eventData)
    { 
            if (Input.GetMouseButtonDown(0))
            {
                t2 = Time.realtimeSinceStartup;
                if (t2 - t1 < 0.3f)
                {
                gameObject.GetComponent<InputField>().interactable = true;
                }
                t1 = t2;

            }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void endEditor(string s)
    {
        gameObject.GetComponent<InputField>().interactable = false;
    
    }


}
