using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

public class UIManager
{
    private static UIManager _ins;
    public static UIManager GetIns()
    {
        if (_ins == null)
        {
            _ins = new UIManager();
        }
        return _ins;
    }
    private Transform Parent_0;
    private Transform Parent_1;

    /// <summary>
    /// 初始化所有UI的父类
    /// </summary>
    /// <param name="_main">所有主界面父类</param>
    /// <param name="_show">所有弹窗型界面父类</param>
    public void InitParent(Transform _main, Transform _show)
    {
        Parent_0 = _main;
        Parent_1 = _show;
    }

    /// <summary>
    /// 存储所有的UI界面
    /// </summary>
    public Dictionary<string, GameObject> UIdic = new Dictionary<string, GameObject>();

    /// <summary>
    /// 打开UI界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="_uitype"></param>
    public T Show<T>(UIType _uitype = UIType.Main_View,object arg = null) where T : MonoBehaviour
    {
        Type _t = typeof(T);
        FieldInfo _finfo_n = _t.GetField("ViewName");
        string _viewname = (string)_finfo_n.GetValue(null);
        if (!UIdic.ContainsKey(_viewname))
        {
            GameObject _obj = Resources.Load<GameObject>(_viewname);
            GameObject _view = UnityEngine.Object.Instantiate<GameObject>(_obj);
            UISetParent(_view, _uitype);
            _view.transform.localPosition = Vector3.zero;
            _view.transform.localScale = Vector3.one;
            UIdic.Add(_viewname, _view);
        }
        UIdic[_viewname].SetActive(true);

        BaseView _bview = UIdic[_viewname].GetComponent<BaseView>();
        _bview.SetDataObject(arg);
        _bview.RefleshView();
        return UIdic[_viewname].GetComponent<T>() as T;
    }

    private void UISetParent(GameObject _view, UIType _uitype = UIType.Main_View)
    {
        switch (_uitype)
        {
            case UIType.Main_View:
                _view.transform.SetParent(Parent_0);
                break;
            case UIType.Show_View:
                _view.transform.SetParent(Parent_1);
                break;
            default:
                return;
        }
    }

    /// <summary>
    /// 关闭UI界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close<T>()
    {
        Type _t = typeof(T);
        Close(_t);
    }
    /// <summary>
    /// 关闭UI界面
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public void Close(Type _t)
    {
        FieldInfo _minfo_n = _t.GetField("ViewName");
        string _viewname = (string)_minfo_n.GetValue(null);
        if (!UIdic.ContainsKey(_viewname))
        {
            return;
        }
        UIdic[_viewname].SetActive(false);
    }
}

public enum UIType
{
    Main_View,
    Show_View
}
