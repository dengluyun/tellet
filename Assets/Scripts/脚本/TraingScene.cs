using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraingScene : MonoBehaviour
{

    [SerializeField]
    GameObject Traing_Context;
    [SerializeField]
    GameObject traing_Scene;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void traingSceneshow()
    {
        //关闭训练内容画面，显示训练场景
        Traing_Context.SetActive(false);
        if (!traing_Scene.activeSelf)
        {
            traing_Scene.SetActive(true); 
        }
    }
    public void traing_Context()
    {
        //训练方案画面的显示
        Traing_Context.SetActive(true);
        traing_Scene.SetActive(false);     
    }



}
