using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class selectGrade : MonoBehaviour
{
    public List<GameObject> objects_List = new List<GameObject>();
    [SerializeField]
    GameObject Grade_message_Profab;
    [SerializeField]
    Transform content;

    [SerializeField]
    GameObject GradeTipPanel;

    public  static bool IsselectChangePanel = false;
    
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public SqlOperation query_People()
    {
        //连接数据库
        SqlOperation help = new SqlOperation(@"Data Source = " + Application.streamingAssetsPath + "/" + "Peoples.db");
        return help;
    }

    public void findtype()
    {
        for (int i = content.childCount - 1; i >= 0; i--)
        {
           // Destroy(content.GetChild(i).gameObject,0);
            DestroyImmediate(content.GetChild(i).gameObject);
            print(1);
        }
        foreach (Transform t in content)
        {
           
                       
                DestroyImmediate(t.gameObject);
                print(1);
        } 
        List<GameObject> g = new List<GameObject>();
        //遍历所有的对象，根据显示确定查找的方式
        foreach (var t in objects_List)
        {
            if (t.activeSelf)
            {
                g.Add(t);
            }
        }
        if (g.Count == 2)
        {
            switch (g[0].GetComponent<Dropdown>().captionText.text)
            {
                case "学员编号":
                    if (g[1].GetComponent<InputField>().text == "")
                    {
                        GameObject G = Instantiate(GradeTipPanel, transform.parent);
                        G.transform.GetChild(0).GetComponent<Text>().text = "查找的学员编号不能为空!!!";
                        //showTipPanel("查找的学员编号不能为空!!!");
                        break;
                    }
                    IsselectChangePanel = true;
                    string str = g[1].GetComponent<InputField>().text;
                    SqlOperation s = query_People();
                    List<string> Sql_Message = s.selectGrade("Solider_Number", str, s.connection);
                    creatPeo_MessagePro(Sql_Message);
                    break;

                case "姓名":
                    if (g[1].GetComponent<InputField>().text == "")
                    {
                        GameObject G = Instantiate(GradeTipPanel, transform.parent);
                        G.transform.GetChild(0).GetComponent<Text>().text = "查找的姓名不能为空!!!";
                        //showTipPanel("查找的姓名不能为空!!!");
                        break;
                    }
                    IsselectChangePanel = true;
                    string str1 = g[1].GetComponent<InputField>().text;
                    SqlOperation s1 = query_People();
                    List<string> Sql_Message1 = s1.selectGrade("Name", str1, s1.connection);
                    creatPeo_MessagePro(Sql_Message1);
                    break;

                case "年龄":
                    IsselectChangePanel = true;
                    string str2 = g[1].GetComponent<Dropdown>().captionText.text;
                    SqlOperation s2 = query_People();
                    List<string> Sql_Message2 = s2.selectGrade("Age", str2, s2.connection);
                    creatPeo_MessagePro(Sql_Message2);
                    break;

                case "部队":
                    IsselectChangePanel = true;
                    string str3 = g[1].GetComponent<Dropdown>().captionText.text;
                    if (g[1].transform.childCount >= 2 && g[1].transform.GetChild(0).name == "pai")
                    {
                        str3 += g[1].transform.GetChild(0).GetComponent<Dropdown>().captionText.text;
                        if (g[1].transform.GetChild(1).name == "ban")
                        {
                            str3 += g[1].transform.GetChild(1).GetComponent<Dropdown>().captionText.text;
                        }
                    }
                    SqlOperation s3 = query_People();
                    List<string> Sql_Message3 = s3.selectGrade("Troops", str3, s3.connection);
                    creatPeo_MessagePro(Sql_Message3);
                    break;
                case "方案名称":
                    if (g[1].GetComponent<InputField>().text == "")
                    {
                        GameObject G = Instantiate(GradeTipPanel, transform.parent);
                        G.transform.GetChild(0).GetComponent<Text>().text = "查找的训练名称不能为空!!!!!!";
                        //showTipPanel("查找的训练名不能为空!!!");
                        break;
                    }
                    IsselectChangePanel = true;
                    string str4= g[1].GetComponent<InputField>().text;
                    SqlOperation s4= query_People();
                    List<string> Sql_Message14 = s4.selectGrade("Solution_Name", str4, s4.connection);
                    creatPeo_MessagePro(Sql_Message14);
                    break;

            }

        }
    }

    


    //将数据库中的数据提取出来，显示在画布上
    public void creatPeo_MessagePro(List<string> Sql_Message)
    {
        if (Sql_Message.Count != 0)
        {
            foreach (string M in Sql_Message)
            {
                string[] m = M.Split('，');
                GameObject game = Instantiate(Grade_message_Profab, content);
                game.transform.GetChild(0).transform.GetComponent<InputField>().text = (content.childCount).ToString();
                game.transform.GetChild(6).transform.GetComponent<InputField>().text = m[0];
                game.transform.GetChild(1).transform.GetComponent<InputField>().text = m[1];
                game.transform.GetChild(2).transform.GetComponent<InputField>().text = m[2];
                game.transform.GetChild(7).transform.GetComponent<InputField>().text = m[3];
                game.transform.GetChild(3).transform.GetComponent<InputField>().text = m[4];
                game.transform.GetChild(4).transform.GetComponent<InputField>().text = m[5];
                game.transform.GetChild(5).transform.GetComponent<InputField>().text = m[6];

            }
        }
        else
        {
            GameObject G = Instantiate(GradeTipPanel, transform.parent);
            G.transform.GetChild(0).GetComponent<Text>().text = "未找到相应的成绩信息";     
        }
        
    }


    //查询结束，删除查询信息
    public void removeSqlMessage()
    {
        for (int i = 1; i <= content.transform.childCount; i++)
        {
            if (i != content.transform.childCount)
            {
                Destroy(content.GetChild(i).gameObject);
            }
            else if (i == content.childCount && content.childCount >= 2)
            {
                Destroy(content.GetChild(i - 1).gameObject);
            }

        }

    }

    public void closeTipPanel()
    {
        GradeTipPanel.SetActive(false);   
    }
    public void showTipPanel(string Message)
    {
        if (!GradeTipPanel.activeSelf)
        {
            GradeTipPanel.SetActive(true);
            GradeTipPanel.transform.GetChild(0).GetComponent<Text>().text = Message;
        }
    
    }


}
