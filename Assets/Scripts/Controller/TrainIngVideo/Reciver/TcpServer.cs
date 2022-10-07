using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using UnityEngine;
/// <summary>
/// 服务端
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
            Debug.Log("链接成功！");
        }
        WaitCallback acceptClientCallBack = new WaitCallback(AccetpClient); //开启客户端接收线程
        ThreadPool.QueueUserWorkItem(acceptClientCallBack);
    }
  
    public void Close()
    {
        socket.Close();
        socket.Dispose();
    }
    //接收客户端
    void AccetpClient(object obj)
    { 
        while(true)
        {
            Socket socketClient = socket.Accept();
            string[] ip_port = socketClient.RemoteEndPoint.ToString().Split(':');
            if (clients.ContainsKey(ip_port[0]))//重复连接
            {
                CloseSocket(clients[ip_port[0]]);//关闭重复连接socket
                clients[ip_port[0]] = socketClient;//重新赋值
            }
            else
                clients.Add(ip_port[0], socketClient);

            var reciverMsgCallBack = new WaitCallback(Receive); //开启接收线程
            ThreadPool.QueueUserWorkItem(reciverMsgCallBack,socketClient);
        }
    }



    byte[] messageBuffer=new byte[1024*1024];
    int hasReadLength = 0;//已经读取的长度
    int dataLength = 0;
    //接收数据信息
    public void Receive(object obj)
    {
        Socket clientSocket = obj as Socket;
        while (true)
        {

           // try
           // {
                int length = clientSocket.Receive(messageBuffer, hasReadLength, messageBuffer.Length-hasReadLength, SocketFlags.None);
                length += hasReadLength;//目前缓冲区数据长度
                 hasReadLength = 0;
                    while (length > 4)
                    {
                        byte[] m = new byte[4];//数据长度
                        Buffer.BlockCopy(messageBuffer, hasReadLength, m, 0, 4);
                        dataLength = BitConverter.ToInt32(m, 0);
                    if (length >= dataLength + 4)
                    {
                        hasReadLength += 4;//加长包头长度
                        byte[] message = new byte[dataLength];
                        Buffer.BlockCopy(messageBuffer, hasReadLength, message, 0, dataLength);
                        hasReadLength += message.Length;//加上数据长度

                         Demo.datas.Enqueue(message);
                       // Debug.Log(Encoding.UTF8.GetString(message));
                        Debug.Log("接受数据长度:" + message.Length);
                        length -= (message.Length + m.Length);
                        if (length == 0)
                        {
                            hasReadLength = 0;
                            break;
                        }
                        if (length <= 4)//已经不是一条完整的信息，此时应该保存信息    
                        {
                            Buffer.BlockCopy(messageBuffer, hasReadLength, messageBuffer, 0, length);
                        hasReadLength = length;//最后剩余长度
                            break;
                        }
                    }
                     else
                    {
                        Buffer.BlockCopy(messageBuffer, hasReadLength, messageBuffer, 0, length);
                        hasReadLength = length;//最后剩余长度
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

    //接收数据分析
    

}
