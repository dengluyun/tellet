using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class addpeoples : MonoBehaviour
{

 public  List<Dropdown> drop_List = new List<Dropdown>();
    string identity = "学员，教员";
    string lian = "一连，二连，三连";
    string pai = "一排，二排，三排";
    string ban = "一班，二班，三班";

    [SerializeField]
    InputField user_Num;
    [SerializeField]
    InputField Name;
    [SerializeField]
    GameObject man;
    [SerializeField]
    GameObject women;

    [SerializeField]
    GameObject tipPanel;


    public string original_password = "123456";


    public int MinAge = 18;
    public int MaxAge = 60;
    // Start is called before the first frame update
    void Start()
    {
        if (drop_List.Count != 0)
            drop_List[0].ClearOptions();
        List<string> age_List = new List<string>();
        for (int i = MinAge; i <= MaxAge; i++)
        {
            age_List.Add(i.ToString());
        }
        drop_List[0].AddOptions(age_List);

        Trainingcontent.addOptions(drop_List[1], identity);
        Trainingcontent.addOptions(drop_List[2], lian);
        Trainingcontent.addOptions(drop_List[3], pai);
        Trainingcontent.addOptions(drop_List[4], ban);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void addPeoples()
    {
        if (user_Num.text == "" || Name.text == "")
        {
            GameObject g = Instantiate(tipPanel, transform.parent);
            g.transform.GetChild(0).GetComponent<Text>().text = "请输入完整信息！";
            return ;
        }
          
        SqlOperation connect = new SqlOperation(@"Data Source=" + Application.streamingAssetsPath + "/" + "Peoples.db");
        bool addOperationResult = connect.insert_message(addPeople_Message(), connect.connection);
        if (addOperationResult)
        {
            GameObject g = Instantiate(tipPanel, transform.parent);
            g.transform.GetChild(0).GetComponent<Text>().text = "添加成功！";                  
        }
        else
        {
            GameObject g = Instantiate(tipPanel, transform.parent);
            g.transform.GetChild(0).GetComponent<Text>().text = "添加失败,请检查学员编号是否正确！";          
        }
    }

    public void showResult(string str)
    {
        if (!tipPanel.activeSelf)
        {
            tipPanel.SetActive(true);
            tipPanel.transform.GetChild(0).GetComponent<Text>().text = str;
        }
        else
        {
            tipPanel.transform.GetChild(0).GetComponent<Text>().text = str;
        }
    }

    public void CloseTipPanel()
    {
        if (tipPanel.activeSelf)
        {
            tipPanel.SetActive(false);      
        }   
    }
    


    public List<string> addPeople_Message()
    {
        List<string> add_message = new List<string>();
        add_message.Add(user_Num.text);
        add_message.Add(Name.text);
        add_message.Add(drop_List[0].captionText.text);
        if (man.GetComponent<Toggle>().isOn)
        {
            add_message.Add("男");
        }
        else
        {
            add_message.Add("女");
        }
        add_message.Add(drop_List[1].captionText.text);
        string budui = drop_List[2].captionText.text + drop_List[3].captionText.text + drop_List[4].captionText.text;
        add_message.Add(budui);
        add_message.Add(original_password);
        return add_message;
            
    }



}
