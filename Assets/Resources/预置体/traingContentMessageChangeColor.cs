using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class traingContentMessageChangeColor : MonoBehaviour, IPointerExitHandler, IPointerEnterHandler,IPointerDownHandler
{
    [SerializeField]
    GameObject tip;

    GameObject T;
    [SerializeField]
    GameObject title;
    public void OnPointerEnter(PointerEventData eventData)
    {
       changall();           
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        endChnage();
       
    }

    double t2 = 0;
    double t1 = 0;
    public void OnPointerDown(PointerEventData eventData)
    {
        if (Input.GetMouseButtonDown(0))
        {
            t2 = Time.realtimeSinceStartup;
            if (t2 - t1 < 0.3f)
            {
                T = Instantiate(tip, jieshaoPanel.traingContent_jieshao);
                T.transform.position = jieshaoPanel.traingContent_jieshao.transform.position;
                switch (title.GetComponent<Text>().text)
                {
                    case "测地车启动":
                        //T.transform.GetChild(0).GetComponent<Text>().text = title.GetComponent<Text>().text;
                        //T.transform.GetChild(1).GetComponent<Text>().text = "利用平台手动扶正功能，将平台扶正到大约北西天位置；利用控显器显示平台的方位及水平，通过惯性测量装置的波段开关，方向选择开关以及扶正开关的联合使用来实现平台的扶正，具体步骤：1.平台方位角调制0附近（按A/a键，控显器显示平台方位角A->波段开关旋至H的“1”位置->点动（每次按压时间小于0.5s）扶正开关，观察方位角A是否向A=0变化。如果不正确，该bian）、2.平台横向调至水平附近、3.平台纵向调至水平附近、4平台方位角调至指西附近";
                        break;
                }

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
    public void changall()
    {
        if (transform.parent != null)
        {
            foreach (Transform t in transform.parent)
            {
                t.GetComponent<Image>().color = new Color(0,102/255f,159/255f,255/255f);
            }
        }
    }

    public void endChnage()
    {
        if (transform.parent != null)
        {
            foreach (Transform t in transform.parent)
            {              
              t.GetComponent<Image>().sprite = Resources.Load<Sprite>("images/addShooter-D");
              t.GetComponent<Image>().color = new Color(0, 160/255f, 251/255f, 255/255f);
            }
        }
    }


    //显示训练内容介绍
    IEnumerator showtraingContentMessage()
    {
        yield return new WaitForSeconds(2);
      
        
    
    }

  
}
