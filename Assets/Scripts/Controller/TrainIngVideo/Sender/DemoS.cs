using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.Threading;

public class DemoS : MonoBehaviour
{
    private TcpClient client;
    IPAddress serverIpAdress = IPAddress.Parse("192.168.31.190");
    int serverPort = 8080;
    [SerializeField]
    Camera mianCamera;
    Queue<byte[]> images = new Queue<byte[]>();
    static object _lock = new object();
    List<byte> m = new List<byte>();

    RenderTexture cameraView = null;
    Texture2D screenShot = null;

    Texture2D texture2D;
    public RawImage rawImage;
    byte[] bytes;

    bool J = false;

    public void RefleshIamge()
    {
        J = true;
        Application.targetFrameRate = 60;
        cameraView = new RenderTexture(1300, 585, 24);
        cameraView.enableRandomWrite = true;
        mianCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        mianCamera.GetComponent<Camera>().targetTexture = cameraView;
        client = new TcpClient(serverIpAdress, serverPort);

        texture2D = new Texture2D(Screen.width, Screen.height);
        ThreadPool.SetMinThreads(5, 5);
        ThreadPool.SetMaxThreads(10, 10);

        var sendMsgCallBack = new WaitCallback(send); //���������߳�
        ThreadPool.QueueUserWorkItem(sendMsgCallBack);

        if (null == screenShot)
        {
            screenShot = new Texture2D(512, 384, TextureFormat.RGB24, false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!J) return;
        if (Time.frameCount % 3 == 0)
        {
            SendTexture();
        }
    }




    public void send(object obj)
    {
        while (true)
        {

            lock (_lock)
            {
                try
                {
                    /* if (bytes != null)
                     {
                         m.Clear();
                         m.AddRange(BitConverter.GetBytes(bytes.Length));
                         m.AddRange(bytes);
                         client.SendMessage(m.ToArray());
                         Debug.Log("��������" + bytes.Length);
                     }*/

                    if (client != null && images.Count > 0)
                    {
                        m.Clear();
                        byte[] bytes = images.Dequeue();
                        m.AddRange(BitConverter.GetBytes(bytes.Length));
                        m.AddRange(bytes);
                        client.SendMessage(m.ToArray());
                        Debug.Log("��������" + DateTime.Now);
                        Debug.Log(bytes.Length);

                    }

                }

                catch (Exception e)
                {
                    Debug.Log(e.Message);
                }
            }

            Thread.Sleep(20);
        }
    }


    void SendTexture()
    {
        //��ȡ��Ļ���ؽ�����Ⱦ
        RenderTexture.active = cameraView;
        screenShot.ReadPixels(new Rect(0, 0, cameraView.width, cameraView.height), 0, 0);
        RenderTexture.active = null;
        bytes = screenShot.EncodeToJPG(100);
        lock (_lock)
        {
            images.Enqueue(bytes);
        }

        print(DateTime.Now);

        /*  List<byte> m = new List<byte>();
          m.AddRange(BitConverter.GetBytes(bytes.Length));
          m.AddRange(bytes);
          *//* byte[] l = BitConverter.GetBytes(bytes.Length);
           byte[] m = new byte[bytes.Length + l.Length];
           Buffer.BlockCopy(l, 0, m, 0, l.Length);
           Buffer.BlockCopy(bytes, 0, m, l.Length, bytes.Length);*//*
          if (client != null)
          {
              client.SendMessage(m.ToArray());
              Debug.Log("���ݳ���:" + (m.Count-4));
          }*/


    }
}
