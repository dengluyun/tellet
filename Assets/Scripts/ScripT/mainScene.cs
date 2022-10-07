using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class mainScene : MonoBehaviour
{
   
    //将所有的面板存储起来
      public  List<GameObject> gameObjects_list = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
             
    }
    // Update is called once per frame
    void Update()
    {
        showPanels();
    }
//显示按钮相对应的面板
    public void showPanels()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject G = EventSystem.current.currentSelectedGameObject;
            if (G != null)
            {               
                switch (G.name)
                {
                    case "xunlian":
                        show_orHide_Panel(gameObjects_list[0]);
                        break;

                    case "chengji":
                        show_orHide_Panel(gameObjects_list[1]);
                        break;

                    case "renyuan":
                        show_orHide_Panel(gameObjects_list[2]);
                        break;

                    case "xitong":
                        show_orHide_Panel(gameObjects_list[3]);
                        break;

                    case "xinshou":
                        show_orHide_Panel(gameObjects_list[4]);
                        break;

                    case "close":
                        G.transform.parent.gameObject.SetActive(false);
                        break;
                }
            }
        }
    }

    public void show_orHide_Panel(GameObject g)
    {       
       g.SetActive(!g.activeSelf);        
       setactive_Panel(g);
    }

    //面板的显示隐藏
    public void setactive_Panel(GameObject g)
    {
        List<GameObject> L_g = new List<GameObject>();
        L_g.AddRange(gameObjects_list);
        L_g.Remove(g);
        foreach (GameObject G in L_g)
        {
            if (G.activeSelf)
            {
                G.SetActive(false);
            }
        }
    }


    //关闭面板
    public static void close_Panel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject G = EventSystem.current.currentSelectedGameObject;
            if (G != null && G.name == "close")
            {
                G.transform.parent.gameObject.SetActive(false);
            }

        }

    }



}
