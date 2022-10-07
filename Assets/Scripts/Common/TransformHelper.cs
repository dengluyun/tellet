using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 变换组件帮组类
/// </summary>
public static class TransformHelper
{
    /// <summary>
    /// 未知层级，查找后代指定名称的变换组件
    /// </summary>
    /// <param name="currentTF">当代变换组件</param>
    /// <param name="childName">后代物体名称</param>
    /// <returns></returns>
    public static Transform FindChildByName(this Transform currentTF, string childName)
    {
        //递归： 在方法内部调用自身
        //1.在子物体中查找
        Transform childTF = currentTF.Find(childName);

        if (childTF != null)
            return childTF;

        for (int i = 0; i < currentTF.childCount; i++)
        {
            //2.递归  将任务交给子物体
            childTF = FindChildByName(currentTF.GetChild(i), childName);

            if (childTF != null)
                return childTF;
        }

        return null;
    }

}
