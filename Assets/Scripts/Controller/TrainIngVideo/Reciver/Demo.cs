using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.UI;
using System.Text;
using System.IO;

public class Demo : MonoBehaviour
{
    private TcpServer server;
    IPAddress serverIpAdress;
    int serverPort;
    //public bool isSend = false;
    [SerializeField]
    Camera mianCamera;
    Texture2D texture2D;
    int Index = 0;
    public RawImage rawImage;

    public void RefleshIamge()
    {
        if(serverIpAdress == null)
        {
            string[] ipPort = GetConfigContext("IPAndPort.txt").Split(':');
            serverIpAdress = IPAddress.Parse(ipPort[0]);
            serverPort = int.Parse(ipPort[1]);
            texture2D = new Texture2D(Screen.width, Screen.height);
        }
            server = new TcpServer(serverIpAdress, serverPort, 10);
    }

    public void CloseView()
    {
        //serverIpAdress = null;
        if(server != null)
        {
            server.Close();
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Index = 0;
        
    }
    
    // Update is called once per frame
    void Update()
    {
        if (datas!=null&&datas.Count > 0)
        {
            // 处理纹理数据，并显示
            texture2D.LoadImage(datas.Dequeue());
            //OutputRt(texture2D, Index);
            //Index++;
            rawImage.texture = texture2D;

        }

    }

    MemoryStream ms = null;
    public static Queue<byte[]> datas=new Queue<byte[]>();
    public void BytesToImage(byte[] bytes)
    {
        ms = new MemoryStream(bytes, 0,bytes.Length);
        datas.Enqueue(ms.ToArray());

        
    }

    public string GetConfigContext(string fileName)
    {
        return ConfigurationReader.GetConfigFile(fileName);
    }

    //using System.IO;需要引用


    public static void OutputRt(Texture2D png, int idx = 0)
    {
        byte[] dataBytes = png.EncodeToPNG();
        string strSaveFile = Application.dataPath + "/Resources/texture/rt_" + System.DateTime.Now.Minute + "_" + System.DateTime.Now.Second + "_" + idx + ".jpg";
        FileStream fs = File.Open(strSaveFile, FileMode.OpenOrCreate);
        fs.Write(dataBytes, 0, dataBytes.Length);
        fs.Flush();
        fs.Close();
        png = null;
        RenderTexture.active = null;
    }

    private void OnDestroy()
    {
        //server.Close();
    }

}
