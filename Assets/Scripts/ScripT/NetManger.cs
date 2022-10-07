using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public static class NetManger
{


    //身份判定
   
    //定义数据头
    public enum Mes_Head
    {
     Identity  


    }
    //创建一个委托
    public delegate  void  Process_Message(string message);
    //处理数据，将相应的数据对应相应的函数中
    public static Dictionary<string, Process_Message> Process_Event = new Dictionary<string, Process_Message >();
    //是否注册成功
    public static Boolean is_Register_Success = true;

    //信息列表
    public static List<string> list_Message = new List<string>();


   //连接服务器
   public static void  ConnectServer()
    {


    }

    //发送信息
    public static void SendMessage()
    { 
    

    }

    //接受信息
    public static void ReceiverMessage()
    {
        //接受的信息,信息的格式是  信息头|信息体
        string Message= "";
        //增加信息
        list_Message.Add(Message);
       
    }

    //处理信息
    public static void Processing_information()
    {
        //将信息切分成信息头和信息尾
        string[] S = list_Message[0].Split('|');
        //删除已经读的信息
        list_Message.Remove(list_Message[0]);
       //相应的信息头调用相应的委托
        Process_Event[S[0]](S[1]);
    }

    //增加委托
    public static void Adddelegate(string Head, Process_Message P)
    {
        Process_Event.Add(Head, P);    
    }
    //删除委托
    public static void Removedelegate(string Head)
    {
        Process_Event.Remove(Head);
    }
   
}
