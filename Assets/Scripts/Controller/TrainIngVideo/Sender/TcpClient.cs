using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class TcpClient
{
    Clients client;
    public TcpClient(IPAddress serverIp, int port)
    {
        client = new Clients(serverIp, port);
    }

    //用户类
    public class Clients
    {
        public Socket socket;
        public Clients(IPAddress serverIp, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(serverIp, port);
            if (socket.Connected)
            {
                Debug.Log("连接成功");
            }
        }
    }


    //断开连接，释放内存
    public void Dispose()
    {
        if (client != null)
        {
            client.socket.Close();
            client.socket.Dispose();
        }
    }

    //重新连接
    public void ReConnect(IPAddress serverIp, int port)
    {
        //如果断开连接
        if (client != null && !client.socket.Connected)
        {
            client.socket.Connect(serverIp, port);
        }
    }

    //发送消息
    public void SendMessage(byte[] message)
    {
        if(client!=null&&client.socket.Connected)
         {
            client.socket.Send(message);
         }
    } 

}
