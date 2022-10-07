using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.InteropServices;
using autoreplyprint.cs;
using System.Threading;

public class PrintTool : MonoBehaviour
{
    public static PrintTool instance;
    UIntPtr result = (UIntPtr)0;
    public List<Dictionary<string, string>> Message = new List<Dictionary<string, string>>();
    private static readonly object lockObj = new object();
    public Dictionary<string, string> message = new Dictionary<string, string>();
    const int Max = 20;
    bool isOpen = true;
    Thread thread;
    public     Thread thread1;
    // Start is called before the first frame update
    void Start()
    {
       
        instance = this;
        // thread = new Thread(SaveGradeMessage);
        //thread.Start();
         thread1 = new Thread(getPrintMessage);
         thread1.Start();
    }

    //打开USB
    [System.Runtime.InteropServices.DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
    public static extern UIntPtr CP_Port_OpenUsb(String name, int autoreplymode);

    //打印机的速度
    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CP_Pos_SetPrintSpeed(UIntPtr handle, int nSpeed);
    // Update is called once per frame
    void Update()
    {
        
    }


    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CP_Pos_FullCutPaper(UIntPtr handle);

    public void getPrintMessage() //对成绩进行打印
    {
        while (true)
        {
            if (Message.Count > 0)
            {
                 lock (lockObj)
                {
                    foreach (var t in Message)
                    {
                        Print(t);//打印第一个数据   
                    }                   
                    Message.Clear();//移除第一个数据
                }
            }
            else
            {
                Thread.Sleep(50);            
            }
        }
    }

    //保存数据
   public void SaveGradeMessage()
   {
        while (isOpen)
        {         
           // m = message;
            if (Message.Count > 0)//数据不为空
            {
                lock (lockObj)
                {
                    if (Message.Count > Max)//超过了存储的范围
                    {
                        Message.Clear();//将其清空               
                    }
                    else
                    {
                        Dictionary<string, string> m = new Dictionary<string, string>();
                       // m = message;
                        foreach (string s in message.Keys)
                        {
                            m.Add(s, message[s]);
                        }
                        Message.Add(m);//进行添加数据  
                      
                    }
                    message.Clear();
                }
            }
            else if (message.Count == 0)
            {
                Thread.Sleep(50);
            }       
        } 
    }

    //打印
    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Unicode)]
    public static extern int CP_Pos_PrintTextInUTF8(UIntPtr handle, String str);

    //枚举本地USB打印口
    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl, CharSet = CharSet.Ansi, EntryPoint = "CP_Port_EnumUsb")]
    public static extern uint __CP_Port_EnumUsb(byte[] pBuf, uint cbBuf, ref uint pcbNeeded);
    public static String[] CP_Port_EnumUsb()
    {
        uint cbNeeded = 0;
        __CP_Port_EnumUsb(null, 0, ref cbNeeded);
        if (cbNeeded > 0)
        {
            byte[] pBuf = new byte[cbNeeded];
            if (pBuf != null)
            {
                __CP_Port_EnumUsb(pBuf, cbNeeded, ref cbNeeded);
                String s = System.Text.Encoding.Default.GetString(pBuf);
                String[] ss = s.Split(new char[] { '\0' }, StringSplitOptions.RemoveEmptyEntries);
                return ss;
            }
        }
        return null;
    }
    //判断端口是否打开
    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CP_Port_IsOpened(UIntPtr handle);

    //Dictionary<string, string> GradeMessage
    public void Print(Dictionary<string,string> GradeMessage)
    {
        if (GradeMessage.Count > 0)
        {
            string[] name = CP_Port_EnumUsb();
            if (name.Length > 0)
            {
                if (CP_Port_IsOpened(result) == 0)//USB未打开
                {
                    result = CP_Port_OpenUsb(name[1], 0);
                    int i = (int)result;
                    if (i != 0)
                    {
                        print("USB打开成功");
                      
                    }
                   

                    else
                        print("USB打开失败");
                }
            }
            Test_Page_SampleTicket_58mm_1(result, GradeMessage);
        }         
      }
    [DllImport("autoreplyprint.dll", CallingConvention = CallingConvention.Cdecl)]
    public static extern int CP_Pos_FeedAndHalfCutPaper(UIntPtr handle);


    public void Test_Page_SampleTicket_58mm_1(UIntPtr h,Dictionary<string,string>GradeMessage)
    {
        CP_Pos_SetPrintSpeed(result, 15000);
        int paperWidth = 450;
        int paperHeight = 700;

        AutoReplyPrint.CP_Page_SelectPageModeEx(h, 200, 200, 0, 0, paperWidth, paperHeight);//设置页面模式（页面的布局）
        AutoReplyPrint.CP_Pos_SetMultiByteMode(h);   
        AutoReplyPrint.CP_Pos_SetMultiByteEncoding(h, AutoReplyPrint.CP_MultiByteEncoding_UTF8);//设置打印机多字节编码

        //AutoReplyPrint.CP_Pos_SetTextScale(h, 1, 1); //设置文本放大倍数
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, AutoReplyPrint.CP_Page_DrawAlignment_HCenter, 10, "中国福利彩票");
        //AutoReplyPrint.CP_Pos_SetTextScale(h, 0, 0);
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, 0, 60, "销售期2015033");
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, AutoReplyPrint.CP_Page_DrawAlignment_Right, 60, "兑奖期2015033");
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, 0, 90, "站号230902001");
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, AutoReplyPrint.CP_Page_DrawAlignment_Right, 90, "7639-A");
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, 0, 120, "注数5");
        //AutoReplyPrint.CP_Page_DrawTextInUTF8(h, AutoReplyPrint.CP_Page_DrawAlignment_Right, 120, "金额10.00");

        AutoReplyPrint.CP_Pos_SetTextLineHeight(h, 60);
        AutoReplyPrint.CP_Pos_SetTextScale(h, 0, 1);
        AutoReplyPrint.CP_Pos_SetTextUnderline(h, 2);
        string content = "";
        foreach (string g in GradeMessage.Keys)
        {
            content += g + " :" + GradeMessage[g] + "\r\n";           
        }
        AutoReplyPrint.CP_Page_DrawTextInUTF8(h, 0, 160, content);
       // CP_Pos_FeedAndHalfCutPaper(result);
        print("打印完成");

        /* AutoReplyPrint.CP_Pos_SetTextScale(h, 0, 0);
         AutoReplyPrint.CP_Pos_SetTextUnderline(h, 0);
         AutoReplyPrint.CP_Pos_SetTextLineHeight(h, 30);

         AutoReplyPrint.CP_Pos_SetBarcodeHeight(h, 60);
         AutoReplyPrint.CP_Pos_SetBarcodeUnitWidth(h, 3);
         AutoReplyPrint.CP_Pos_SetBarcodeReadableTextPosition(h, AutoReplyPrint.CP_Pos_BarcodeTextPrintPosition_BelowBarcode);
         AutoReplyPrint.CP_Page_DrawBarcode(h, 0, 460, AutoReplyPrint.CP_Pos_BarcodeType_CODE128, "1234567890");
         AutoReplyPrint.CP_Pos_SetBarcodeUnitWidth(h, 4);
         AutoReplyPrint.CP_Page_DrawQRCode(h, 284, 460, 0, AutoReplyPrint.CP_QRCodeECC_L, "1234567890");

         AutoReplyPrint.CP_Page_PrintPage(h);
         AutoReplyPrint.CP_Page_ExitPageMode(h);

         AutoReplyPrint.CP_Pos_FeedAndHalfCutPaper(h);
         AutoReplyPrint.CP_Pos_KickOutDrawer(h, 0, 100, 100);
         AutoReplyPrint.CP_Pos_KickOutDrawer(h, 1, 100, 100);
         AutoReplyPrint.CP_Pos_Beep(h, 1, 500);*/

        Test_Pos_QueryPrintResult(h);
    }

    public void Test_Pos_QueryPrintResult(UIntPtr h)
    {

        int result = AutoReplyPrint.CP_Pos_QueryPrintResult(h, 1000);
        if (result != 0)
           print("Print Success");
        else
            print("Print Failed");
    }






}
