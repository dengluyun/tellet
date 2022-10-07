using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadingViewController : BaseView
{
    public static string ViewName = "UIPanel/ShowPanel/LoadingView";

    public Image BackGround;

    public Text LoadText;

    public List<GameObject> _loadingcirclelist = new List<GameObject>();

    float _overtime = 0;

    float _texttime = 0;

    public override void RefleshView()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        _overtime += Time.deltaTime;
        _texttime += Time.deltaTime;

        if(_texttime >= 1.0f)
        {
            _texttime = 0;
            LoadText.text += ".";
        }

        if(_overtime >= 3)
        {
            _overtime = 0;
            LoadText.text = "加载中";
            UIManager.GetIns().Close<LoadingViewController>();
        }

        for(int i =0;i<_loadingcirclelist.Count;i++)
        {
            if(i%2==0)
            {
                _loadingcirclelist[i].transform.rotation *= Quaternion.AngleAxis(1, Vector3.forward);
            }
            else
            {
                _loadingcirclelist[i].transform.rotation *= Quaternion.AngleAxis(-1, Vector3.forward);
            }
        }
    }
}
