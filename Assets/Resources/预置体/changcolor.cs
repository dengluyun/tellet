using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class changcolor : MonoBehaviour,IPointerExitHandler,IPointerEnterHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {   
        changall();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        endChnage();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void changall()
    {
        if (transform.parent != null)
        {
            foreach (Transform t in transform.parent)
            {
                if (t!=transform.parent.GetChild(8)&& t != transform.parent.GetChild(9))
                {
                    t.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/changeImage");
                }             
            }               
        }    
    }

    public void endChnage()
    {
        if (transform.parent != null)
        {
            foreach (Transform t in transform.parent)
            {
                if (t != transform.parent.GetChild(8)&& t != transform.parent.GetChild(9))
                {
                    t.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/no_change1");
                }
            }
        }

    }

}
