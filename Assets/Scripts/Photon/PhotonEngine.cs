using ExitGames.Client.Photon;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

/// <summary>
/// Photon引擎入口类
/// </summary>
public class PhotonEngine : IPhotonPeerListener
{
    private static PhotonEngine _Ins;

    public static PhotonEngine GetIns()
    {
        if(_Ins == null)
        {
            _Ins = new PhotonEngine();
        }
        return _Ins;
    }

    public PhotonPeer peer;

    private static Dictionary<string, object> cache;

    //初始化
    public void Init()
    {
        ConnectToServer();
    }

    //连接至photon服务器
    public void ConnectToServer()
    {
        peer = new PhotonPeer(this, ConnectionProtocol.Udp);
        cache = new Dictionary<string, object>();
        //获取IP地址  从文本中读取  便于更改
        string IPAndPort = GetIPAndPort();
        //Debug.Log(IPAndPort);
        peer.Connect(IPAndPort, "Server");
    }

    //从StreamAssets中获取IP地址和端口号,并以字符串的形式返回  eg:"192.168.0.2:5055"
    private string GetIPAndPort()
    {
        string[] allStr = ConfigurationReader.GetConfigFile("Config.txt").Split('\n');
        DataManager.GetIns().ControllerID = allStr[1].Remove(allStr[1].Length - 1,1);
        DataManager.GetIns().VideoPath = allStr[2].Split('&')[1].Replace('\n', ' ');
        DataManager.GetIns().VideoPath = DataManager.GetIns().VideoPath.Remove(DataManager.GetIns().VideoPath.Length - 1, 1);
        DataManager.GetIns().RecordingScreenOrder = allStr[3].Split('&')[1] + " ";
        //Debug.LogError(allStr[7].Split('?')[1]);
        return allStr[0].Split('?')[1];
    }

    //调试信息返回
    public void DebugReturn(DebugLevel level, string message)
    {
        Debug.Log(message);
    }

    //接收服务器事件
    public void OnEvent(EventData eventData)
    {
        string className = string.Format("{0}Event", (EventCode)eventData.Code);
        Debug.Log(className);
        if (className == "SendDataEvent")
        {
            return;
        }
        IEventBase eventBase = CreateObject<IEventBase>(className);
        eventBase.OnEvent(eventData);
    }

    //接收服务器操作
    public void OnOperationResponse(OperationResponse operationResponse)
    {
        Debug.Log("Receive Operation");

        //通过反射创建Operation
        //定义Operation类名为 OperationCode + className
        string className = string.Format("{0}Operation", (OperationCode)operationResponse.OperationCode);
        IOperationBase operation = CreateObject<IOperationBase>(className);

        operation.OnOperationResponse(operationResponse);
    }

    //连接状态发生变化
    public void OnStatusChanged(StatusCode statusCode)
    {
        Debug.Log(statusCode);
    }

    private void Update()
    {
        //每帧执行 和服务器通信
        peer.Service();
    }

    //通过反射创建对象
    private T CreateObject<T>(string className) where T : class
    {
        
        if (!cache.ContainsKey(className))
        {
            Type type = Type.GetType(className);
            object instance = Activator.CreateInstance(type);
            cache.Add(className, instance);
        }

        return cache[className] as T;
    }

    /// <summary>
    /// 将数据发送至服务器
    /// </summary>
    /// <param name="opCode">数据类型</param>
    /// <param name="sendData">发送的数据内容</param>
    public void SendOperation(OperationCode opCode, Dictionary<byte, object> sendData)
    {
        Debug.Log("Send data to server" + opCode.ToString());
        //Debug.Log(sendData[(byte)ParametersCode.Register]);
        peer.OpCustom((byte)opCode, sendData, true);
    }

    //脚本销毁时 断开与服务器之间的连接
    private void OnDestroy()
    {
        DisConnect();
    }

    //和photon服务器断开连接
    public void DisConnect()
    {
        if (peer != null && peer.PeerState == PeerStateValue.Connected)
        {
            peer.Disconnect();
        }
    }

    //应用程序关闭时  断开连接
    private void OnApplicationQuit()
    {
        //发送挂机命令
        //UDPClient.Instance.SocketSend(0x91, new byte[] { });
        DisConnect();
    }
}