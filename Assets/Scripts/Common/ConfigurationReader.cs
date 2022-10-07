using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;

/// <summary>
/// 配置文件读取器
/// </summary>
public class ConfigurationReader
{
    //加载文件  移动端唯一加载方式
    public static string GetConfigFile(string fileName)
    {
        //加载ConfigMap.txt 文件        移动端唯一加载方式
        //因为存在StreamingAssets文件夹中，该文件夹发布后无法确定具体位置，因此使用Application.streamingAssetsPath获取文件夹位置
        //file:// 表示加载本地文件

        string url;

        //string url = "file://" + Application.streamingAssetsPath + "/" + fileName;
        //该路径读取方法在大部分情况下有用，偶尔可能失效
        //解决：分平台写代码

        //在编译器下或PC端
        //if (Application.platform == RuntimePlatform.WindowsEditor)   性能稍差
        //使用unity宏标签
#if UNITY_EDITOR || UNITY_STANDALONE
        //Application.dataPath 定位到Assets文件夹
        url = Application.dataPath + "/StreamingAssets/" + fileName;

        //在IOS中
#elif UNITY_IPHONE
        url = "file://" + Application.dataPath + "/Raw/" + fileName;

        //在Android中
#elif UNITY_ANDROID
        url = "jar:file://" + Application.dataPath + "!/assets/" + fileName;
#endif
        WWW www = new WWW(url);

        //死循环 直到成功加载完文件才返回
        while (true)
        {
            if (www.isDone)
                return www.text;
        }
    }


    /// <summary>
    /// 读取配置文件
    /// </summary>
    /// <param name="fileContent">文件内容</param>
    /// <param name="handler">处理逻辑</param>
    public static void Reader(string fileContent, Action<string> handler)
    {
        using (StringReader reader = new StringReader(fileContent))
        {
            string line;

            while ((line = reader.ReadLine()) != null)
            {
                handler(line);
            }
        }
    }
}
