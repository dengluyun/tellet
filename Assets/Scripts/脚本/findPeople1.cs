using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class findPeople1 : MonoBehaviour
{
    //将所有的DropDown保存下来
    public List<Dropdown> dropList_List = new List<Dropdown>();
    public List<GameObject> select_GameObject = new List<GameObject>();
    public string FindType = "学员编号，姓名，年龄，部队";
    public string lian = "一连，二连，三连";
    public string pai = "一排，二排，三排";
    public string ban = "一班，二班，三班";
    public int Maxage = 50;
    public int Minage = 18;

    [SerializeField]
    InputField soliderNumber;
    [SerializeField]
    InputField soliderName;

    [SerializeField]
    Transform content;
    private void Awake()
    {
        dropList_List[1].ClearOptions();
        List<string> L = new List<string>();
        //初始化年龄
        for (int i = Minage; i < Maxage; i++)
        {
            L.Add(i.ToString());
        }
        dropList_List[1].AddOptions(L);
        startDropDown(dropList_List[0], FindType);
        startDropDown(dropList_List[2], lian);
        startDropDown(dropList_List[3], pai);
        startDropDown(dropList_List[4], ban);

    }
    // Start is called before the first frame update
    void Start()
    {




    }

    // Update is called once per frame
    void Update()
    {
        showPanel();
    }

    public void startDropDown(Dropdown dropdown, string S)
    {
        dropdown.ClearOptions();
        string[] s = S.Split('，');
        List<string> list = new List<string>();
        list.AddRange(s);
        dropdown.AddOptions(list);
    }

    public void showPanel()
    {

        if (select_GameObject.Count == 4)
        {
            show_QueryMode(select_GameObject[2], "年龄");
            show_QueryMode(select_GameObject[0], "学员编号");
            show_QueryMode(select_GameObject[1], "姓名");
            // show_QueryMode(select_GameObject[2], "年龄");
            show_QueryMode(select_GameObject[3], "部队");
        }
    }

    public void show_QueryMode(GameObject g, string s)
    {
        if (dropList_List.Count != 0)
        {
            if (dropList_List[0].captionText.text == s)
            {
                if (!g.gameObject.activeSelf)
                {
                    g.gameObject.SetActive(true);
                }
                foreach (var game in select_GameObject)
                {
                    if (game.activeSelf && game != g)
                    {
                        game.SetActive(false);
                    }
                }
            }
        }
    }
    public void showdelectButton()
    {
        foreach (Transform t in content)
        {
            if (t != content.GetChild(0))
            {
                t.GetChild(6).gameObject.SetActive(!t.GetChild(6).gameObject.activeSelf);
            }

        }
    }
}
