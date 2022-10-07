using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AddScene : MonoBehaviour
{
    public static bool isLoadScene = false;
    public static string sceneType = "";
    public static string traingType = "";
    // Start is called before the first frame update
    void Start()
    {       
        
    }

    // Update is called once per frame
    void Update()
    {
      /*  if (isLoadScene)
        {
            SceneManager.LoadSceneAsync(2, LoadSceneMode.Additive);
            SceneManager.LoadSceneAsync(3, LoadSceneMode.Additive);
            isLoadScene = false;
        }*/
     
    }
}
