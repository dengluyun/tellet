using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PrintGradeMessage : MonoBehaviour
{
    [SerializeField]
    Transform GradePosirion;//成绩显示板
    [SerializeField]
    Button PrintBut;//打印按钮
    [SerializeField]
    Toggle all;
   
    public static string Titles = "方案名称，学员编号，姓名，训练科目，成绩，耗时，日期";
   public static List<string> Title = new List<string>();
    // Start is called before the first frame update
    void Start()
    {
        Title = GetTitle(Titles);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator waite()
    {
        yield return new WaitForSeconds(3f);
        PrintBut.interactable = true; 
    }

    //打印成绩
    public void PrintGrade()
    {
       // startTraining.instance.isPrint = true;
        List<Dictionary<string, string>> M = new List<Dictionary<string, string>>();
        foreach (Transform t in GradePosirion)
        {
            if (t.GetChild(9).GetComponent<Toggle>().isOn == true)
            {
                Dictionary<string, string> message = new Dictionary<string, string>();
                List<string> m = new List<string>();
                m.Add(FindChild.instance.getChildText(t.GetChild(6), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(1), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(2), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(7), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(3), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(4), 2));
                m.Add(FindChild.instance.getChildText(t.GetChild(5), 2));
                for (int i = 0; i < m.Count; i++)
                {
                    message.Add(Title[i], m[i]);
                }
                
                
                if (message.Count == 7)
                {
                    M.Add(message);
                }
            }
        }
        if (M.Count > 0)
        {
            printmessage(M);
        }
      
        //PrintBut.interactable = false;
       // StartCoroutine(waite());
    }
    public void allPrint(bool change)
    {
        change = all.isOn;
        print(change);
        if (change)
        {
            foreach (Transform t in GradePosirion)
            {
                t.GetChild(9).GetComponent<Toggle>().isOn = true;
            }
        }
        else
        {
            foreach (Transform t in GradePosirion)
            {
                t.GetChild(9).GetComponent<Toggle>().isOn = false;
            }
        }
    
    }

    public void printmessage(List<Dictionary<string,string>> GradeContent)
    {
        PrintTool.instance.Message = GradeContent;
       // PrintTool.instance.Print(GradeContent);
    }

    public List<string> GetTitle(string title)
    {
        string[] a = title.Split('，');
        List<string> s = new List<string>();
        s.AddRange(a);
        return s;
    }


}
