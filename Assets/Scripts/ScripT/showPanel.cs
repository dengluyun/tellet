using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class showPanel : MonoBehaviour
{
    //<button,Panel>,键值对
    Dictionary<GameObject, GameObject> Button_Panel = new Dictionary<GameObject, GameObject>();
   public  List<GameObject> buttons = new List<GameObject>();
   public  List<GameObject> objects = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        //将所有的按钮和对应的页面连接起来了
        int i = 0;
        while (i < buttons.Count)
        {
            Button_Panel.Add(buttons[i],objects[i]);
            i++;
        }
    }

    // Update is called once per frame
    void Update()
    {
        showpanel();
    }

    public void showpanel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = EventSystem.current.currentSelectedGameObject; 
            if(g!=null&&Button_Panel.ContainsKey(g))
            {
                Button_Panel[g].SetActive(true);

            }
        
        }
    }

}
