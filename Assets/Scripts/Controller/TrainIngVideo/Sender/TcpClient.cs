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

    //�û���
    public class Clients
    {
        public Socket socket;
        public Clients(IPAddress serverIp, int port)
        {
            socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            socket.Connect(serverIp, port);
            if (socket.Connected)
            {
                Debug.Log("���ӳɹ�");
            }
        }
    }


    //�Ͽ����ӣ��ͷ��ڴ�
    public void Dispose()
    {
        if (client != null)
        {
            client.socket.Close();
            client.socket.Dispose();
        }
    }

    //��������
    public void ReConnect(IPAddress serverIp, int port)
    {
        //����Ͽ�����
        if (client != null && !client.socket.Connected)
        {
            client.socket.Connect(serverIp, port);
        }
    }

    //������Ϣ
    public void SendMessage(byte[] message)
    {
        if(client!=null&&client.socket.Connected)
         {
            client.socket.Send(message);
         }
    } 

}
