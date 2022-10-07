using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class waitLoadScene : MonoBehaviour
{


    [SerializeField]
    GameObject T;

    public static waitLoadScene instance;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
       // StartCoroutine(waite());
    }

    // Update is called once per frame
    void Update()
    {
      
    }

  public  IEnumerator waite()
    {
        yield return new WaitUntil(() => { return (T != null); });
        while (true)
        {
            if (T != null)
            {
                foreach (Transform t in T.transform)
                {
                    t.gameObject.GetComponent<Text>().color = Color.black;
                    t.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(t.GetComponent<RectTransform>().anchoredPosition3D.x, t.GetComponent<RectTransform>().anchoredPosition3D.y + 3, 0);
                    yield return new WaitForSeconds(0.2f);
                    t.gameObject.GetComponent<Text>().color = Color.white;
                    t.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(t.GetComponent<RectTransform>().anchoredPosition3D.x, t.GetComponent<RectTransform>().anchoredPosition3D.y - 3, 0);

                }
            }
        }
    }
    //显示加载画面
    public void showSelf()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }
    //隐藏加载画面 
    public void hideSelf()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }




}
