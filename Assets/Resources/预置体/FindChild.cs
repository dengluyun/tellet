using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FindChild : MonoBehaviour
{
    public static FindChild instance;
    /// <summary>
    /// 获取子类的属性
    /// </summary>
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {

    }
    //获取某个子物体的Text属性
    public string getChildText(Transform parent, int indexChild)
    {
        if (parent.childCount > indexChild)//子类的下标不能越界
        {
            if (hasComponent<Text>(parent.GetChild(indexChild)))
            {
                return parent.GetChild(indexChild).GetComponent<Text>().text;
            }
        }
        return parent.GetChild(indexChild).GetComponent<Text>().text;
    }

    //设置某个子物体的Text属性
    public void setChildText(Transform parent, int indexChild, string setMessage)
    {
        if (parent.childCount > indexChild)//子类的下标不能越界
        {
            if (hasComponent<Text>(parent.GetChild(indexChild)))
            {
                parent.GetChild(indexChild).GetComponent<Text>().text = setMessage;
            }
        }
    }
    //获取子类的InputFiled属性
    public string getChildInputFiled(Transform parent, int indexChild)
    {
        if (parent.childCount > indexChild)//子类的下标不能越界
        {
            if (hasComponent<InputField>(parent.GetChild(indexChild)))
            {
                return parent.GetChild(indexChild).GetComponent<InputField>().text;
            }
        }
        return parent.GetChild(indexChild).GetComponent<InputField>().text;
    }


    //设置子类的sprite
    public void setChildImage(Transform parent, int indexChild, Sprite setSprite)
    {
        if (parent.childCount > indexChild)//子类的下标不能越界
        {
            if (hasComponent<Image>(parent.GetChild(indexChild)))
            {
                parent.GetChild(indexChild).GetComponent<Image>().sprite = setSprite;
            }
        }
    }



    //获取子类的sprite

    public Sprite getChildImage(Transform parent, int indexChild)
    {
        if (parent.childCount > indexChild)//子类的下标不能越界
        {
            if (hasComponent<Image>(parent.GetChild(indexChild)))
            {
                return parent.GetChild(indexChild).GetComponent<Image>().sprite;
            }
        }
        return parent.GetChild(indexChild).GetComponent<Image>().sprite;
    }


    //判断某个物体时是否含有某个组件
    public bool hasComponent<T>(Transform game) where T : Component
    {
        if (game.GetComponent<T>() != null)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //清除某物体的所有的子物体
    public void ClearChilds(Transform parent)
    {
        for (int i = parent.childCount - 1; i >= 0; i--)
        {
            DestroyImmediate(parent.GetChild(i).gameObject);
        }
    }


}


