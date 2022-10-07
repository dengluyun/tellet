using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
/// <summary>
/// �����
/// </summary>
public class TcpServer 
{
    Socket socket;
    Dictionary<string,Socket> clients = new Dictionary<string, Socket>();
    Queue<byte[]> messageQueue = new Queue<byte[]>();
    public static object _lock = new object();
    public TcpServer(IPAddress _serverIp, int port,int  maxListenNum)
    {
        socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        socket.Bind((EndPoint)(new IPEndPoint(_serverIp, port)));
        socket.Listen(maxListenNum);
        if (socket.Connected)
        {
            Debug.Log("���ӳɹ���");
        }
        WaitCallback acceptClientCallBack = new WaitCallback(AccetpClient); //�����ͻ��˽����߳�
        ThreadPool.QueueUserWorkItem(acceptClientCallBack);
    }
  
    public void Close()
    {
        socket.Close();
        socket.Dispose();
    }
    //���տͻ���
    void AccetpClient(object obj)
    { 
        while(true)
        {
            Socket socketClient = socket.Accept();
            string[] ip_port = socketClient.RemoteEndPoint.ToString().Split(':');
            if (clients.ContainsKey(ip_port[0]))//�ظ�����
            {
                CloseSocket(clients[ip_port[0]]);//�ر��ظ�����socket
                clients[ip_port[0]] = socketClient;//���¸�ֵ
            }
            else
                clients.Add(ip_port[0], socketClient);

            var reciverMsgCallBack = new WaitCallback(Receive); //���������߳�
            ThreadPool.QueueUserWorkItem(reciverMsgCallBack,socketClient);
        }
    }



    byte[] messageBuffer=new byte[1024*1024];
    int hasReadLength = 0;//�Ѿ���ȡ�ĳ���
    int dataLength = 0;
    //����������Ϣ
    public void Receive(object obj)
    {
        Socket clientSocket = obj as Socket;
        while (true)
        {

           // try
           // {
                int length = clientSocket.Receive(messageBuffer, hasReadLength, messageBuffer.Length-hasReadLength, SocketFlags.None);
                length += hasReadLength;//Ŀǰ���������ݳ���
                 hasReadLength = 0;
                    while (length > 4)
                    {
                        byte[] m = new byte[4];//���ݳ���
                        Buffer.BlockCopy(messageBuffer, hasReadLength, m, 0, 4);
                        dataLength = BitConverter.ToInt32(m, 0);
                    if (length >= dataLength + 4)
                    {
                        hasReadLength += 4;//�ӳ���ͷ����
                        byte[] message = new byte[dataLength];
                        Buffer.BlockCopy(messageBuffer, hasReadLength, message, 0, dataLength);
                        hasReadLength += message.Length;//�������ݳ���

                         Demo.datas.Enqueue(message);
                       // Debug.Log(Encoding.UTF8.GetString(message));
                        Debug.Log("�������ݳ���:" + message.Length);
                        length -= (message.Length + m.Length);
                        if (length == 0)
                        {
                            hasReadLength = 0;
                            break;
                        }
                        if (length <= 4)//�Ѿ�����һ����������Ϣ����ʱӦ�ñ�����Ϣ    
                        {
                            Buffer.BlockCopy(messageBuffer, hasReadLength, messageBuffer, 0, length);
                        hasReadLength = length;//���ʣ�೤��
                            break;
                        }
                    }
                     else
                    {
                        Buffer.BlockCopy(messageBuffer, hasReadLength, messageBuffer, 0, length);
                        hasReadLength = length;//���ʣ�೤��
                        break;
                    }
                 }                                  
          //  }
           /* catch (Exception e)
            {
                Debug.Log(e);
            }*/

            
        }    
    }

    

    public void CloseSocket(Socket socket)
    {
        socket.Shutdown(SocketShutdown.Both);
        socket.Close();
        socket.Dispose();
    }

    //�������ݷ���
    

}
