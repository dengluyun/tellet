using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class showDelectBut : MonoBehaviour
{
    [SerializeField]
    Transform content;

    [SerializeField]
    Transform PeopleWindows;
    [SerializeField]
    Transform addpeoples_panel;
    [SerializeField]
    Transform mangerPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void showdelectButton()
    {
        foreach (Transform t in content)
        {
            if (t != content.GetChild(0))
            {
                t.GetChild(8).gameObject.SetActive(!t.GetChild(8).gameObject.activeSelf);      
            }
        
        }   
    }

    public void PeopleMangerCahngetoGradeManger()
    {
        PeopleWindows.gameObject.SetActive(false);
        addpeoples_panel.gameObject.SetActive(false);
        mangerPanel.gameObject.SetActive(true);  
    }

    public void GradeMangertoPeopleManger()
    {
        mangerPanel.gameObject.SetActive(false);
        addpeoples_panel.gameObject.SetActive(false);
        PeopleWindows.gameObject.SetActive(true);     
    }

}
