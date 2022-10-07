using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Threading;
                        

public class allpaneController : MonoBehaviour
{

    [SerializeField]
    GameObject windows;
    [SerializeField]
    GameObject startPanel;
    [SerializeField]
    GameObject creattrainPanel;
    [SerializeField]
     GameObject creatPanel;
    bool IsStart = true;
    int count = 0;
    //存储所有的按钮
    public List<Button> Op_buttons_list = new List<Button>();

    //存储所有的额画面
    public List<GameObject>Op_panels_list = new List<GameObject>();


    //一个按钮名字对应一个打开的页面
   public Dictionary<string, GameObject> openPanel = new Dictionary<string, GameObject>();
    //一个按钮名字对应一个关闭的页面
    //public Dictionary<string, GameObject> closePanels = new Dictionary<string, GameObject>();
    void Start()
    {
        for (int i = 0; i < Op_buttons_list.Count; i++)
        {
            openPanel.Add(Op_buttons_list[i].name, Op_panels_list[i]);      
        }
       


    }

    // Update is called once per frame
    void Update()
    {
        showPanel();
    }

    public void showPanel()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject Object = EventSystem.current.currentSelectedGameObject;
            if(Object!=null)
            {
                switch (Object.name)
                {

                    case "TrailContent":
                        //closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["TrailContent"]);
                        break;
                    case "trainContentBackto_Window":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["trainContentBackto_Window"]);
                        break;
                    case "trainStart":
                        /* closePanel(Object.transform.parent.gameObject);
                         showPanel(openPanel["trainStart"]);*/
                        StartCoroutine(Starttraing(Object));
                       
                        break;
                    case "end_traing":
                        // StartCoroutine(endtraing(Object));      
                        
                        break;

                    case "Grade":
                       closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["Grade"]);
                        creattrainPanel.SetActive(false);
                        if (PrintTool.instance.thread1.ThreadState == ThreadState.Suspended)
                        {
                            PrintTool.instance.thread1.Resume();
                        }
                        /*if (PrintTool.instance.thread1.ThreadState == ThreadState.Stopped)
                        {
                            PrintTool.instance.thread1.Start();
                         
                        } */
                        
                        break;
                    case "GradeBacktoWindow":
                        Object.transform.parent.parent.gameObject.SetActive(false);
                        showPanel(openPanel["GradeBacktoWindow"]);
                        startPanel.SetActive(true);
                        creattrainPanel.SetActive(false);
                        creatPanel.SetActive(false);
                        windows.SetActive(true);
                        if (PrintTool.instance.thread1.ThreadState == ThreadState.Running)
                        {
                            PrintTool.instance.thread1.Suspend();//挂起线程
                        }
                      
                       
                        break;
                    case "GradeFind":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["GradeFind"]);
                        break;
                    case "BacktoGradeFind":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["BacktoGradeFind"]);
                        break;
                    case "FindButton":
                        StartCoroutine(GradeFindbutton(Object));
                        break;
                    case "BacktoGradefind_Panel":
                        /*closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["BacktoGradefind_Panel"]);*/
                        StartCoroutine(removeGradeMessage(Object));
                       
                        break;
                    case "PeopleManger":
                      //  closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["PeopleManger"]);
                        windows.SetActive(false);
                        break;
                    case "PeopleMangerBacktoWindow":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["PeopleMangerBacktoWindow"]);
                        break;
                    case "select":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["select"]);
                        break;
                    case "BacktoPeopleWindow":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["BacktoPeopleWindow"]);
                        break;
                    case "Backto_selectpanel":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["Backto_selectpanel"]);
                        break;
                    case "addPeople":                      
                        showPanel(openPanel["addPeople"]);
                        break;
                    case "addpeoples-panelBackto_peopleWindows":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["addpeoples-panelBackto_peopleWindows"]);
                        break;
                    case "fing_button":
                       //StartCoroutine(endtraing(Object));                   
                        break;
                    case "Create_Button":
                        closePanel(Object.transform.parent.gameObject);
                        showPanel(openPanel["Create_Button"]);                       
                        break;
                    case "show_CreatPanel_Button":
                        showPanel(openPanel["show_CreatPanel_Button"]);
                        break;
                    case "close_CreatfanganPanel":
                        closePanel(Object.transform.parent.gameObject);
                        break;
                    case "closexunlianfanan":
                        closePanel(Object.transform.parent.gameObject);
                        startPanel.SetActive(true);
                        break;
                   
                }

            }

        }   
    }

    IEnumerator endtraing(GameObject O)
    {
        yield return new WaitForSeconds(0.3F);
        closePanel(O.transform.parent.gameObject);
        showPanel(openPanel[O.name]);
        windows.gameObject.SetActive(true);       
    }
    IEnumerator Starttraing(GameObject O)
    {
        yield return new WaitForSeconds(0.3F);
        closePanel(O.transform.parent.gameObject);
        showPanel(openPanel[O.name]);
        showOrHideConsolePanel(true);
        windows.SetActive(false);
    }

    public void showOrHideConsolePanel(bool state)
    {
        /*foreach (Transform t in Game_Objects.instance.Canvas.transform)
        {                   
            t.gameObject.SetActive(state);           
        }*/
    }

    IEnumerator removeGradeMessage(GameObject O)
    {
        yield return new WaitForSeconds(0.1F);
        closePanel(O.transform.parent.gameObject);
        showPanel(openPanel[O.name]);

    }
    IEnumerator GradeFindbutton(GameObject O)
    {
        yield return new WaitForSeconds(0.3F);
        if (selectGrade.IsselectChangePanel)
        {
           // closePanel(O.transform.parent.gameObject);
            showPanel(openPanel[O.name]);
            selectGrade.IsselectChangePanel = false;
        }
     
    }

    public void closePanel(GameObject g)
    {
        if (g.activeSelf)
        {
            g.SetActive(false);
        } 
    }


    public void showPanel(GameObject g)
    {
        if (!g.activeSelf)
        {
            g.SetActive(true);
        }
    }



}
