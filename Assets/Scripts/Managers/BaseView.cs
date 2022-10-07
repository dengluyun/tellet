using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class BaseView : MonoBehaviour
{

    public object data;

    /// <summary>
    /// 代替Start功能
    /// 当每次启用脚本时调用此方法
    /// </summary>
    /// 
    public virtual void RefleshView()
    {

    }

    public void SetDataObject(object obj)
    {
        this.data = obj;
    }

    public void AddDropdownListener(Dropdown dropdown, UnityAction<int> call)
    {
        dropdown.onValueChanged.AddListener(call);
    }

    public void AddDropdownListener(string path, UnityAction<int> call)
    {
        FindDropdown(path).onValueChanged.AddListener(call);
    }

    public void AddBtnListener(Button btn, UnityAction call)
    {
        btn.onClick.AddListener(call);
    }

    public void AddBtnListener(string path, UnityAction call)
    {
        FindButton(path).onClick.AddListener(call);
    }

    public Transform FindTransform(string path)
    {
        if (path == String.Empty || path == "")
        {
            return transform;
        }
        return transform.Find(path);
    }

    public Transform FindTransform(Transform trans, string path)
    {
        if (path == String.Empty || path == "")
        {
            return trans;
        }
        return trans.Find(path);
    }

    public Button FindButton(string path)
    {
        return FindTransform(path).GetComponent<Button>();
    }

    public Button FindButton(Transform trans, string path)
    {
        return FindTransform(trans, path).GetComponent<Button>();
    }

    public Text FindText(string path)
    {
        return FindTransform(path).GetComponent<Text>();
    }

    public Text FindText(Transform trans, string path)
    {
        return FindTransform(trans, path).GetComponent<Text>();
    }

    public Toggle FindToggle(string path)
    {
        return FindTransform(path).GetComponent<Toggle>();
    }

    public Toggle FindToggle(Transform trans, string path)
    {
        return FindTransform(trans, path).GetComponent<Toggle>();
    }

    public Dropdown FindDropdown(string path)
    {
        return FindTransform(path).GetComponent<Dropdown>();
    }

    public Dropdown FindDropdown(Transform trans, string path)
    {
        return FindTransform(trans, path).GetComponent<Dropdown>();
    }

    public InputField FindInputField(string path)
    {
        return FindTransform(path).GetComponent<InputField>();
    }

    public InputField FindInputField(Transform trans, string path)
    {
        return FindTransform(trans, path).GetComponent<InputField>();
    }

    //下拉列表数据的添加,数据格式以逗号隔开
    public void addOptions(Dropdown dropdown, string options)
    {
        dropdown.ClearOptions();
        //数据切割，保存
        string[] S = options.Split('，');
        List<string> L = new List<string>();
        L.AddRange(S);
        dropdown.AddOptions(L);
    }

    //下拉列表数据的添加,数据格式以逗号隔开
    public void addOptions(Dropdown dropdown, string[] options)
    {
        dropdown.ClearOptions();
        List<string> L = new List<string>();
        L.AddRange(options);
        dropdown.AddOptions(L);
    }

    public void Close()
    {
        var type = this.GetType();
        UIManager.GetIns().Close(type);
    }

}
