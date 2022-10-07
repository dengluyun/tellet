using System.Net;
using System.Net.Sockets;
using UnityEngine;

/// <summary>
/// 获取当前主机IP
/// </summary>
public class IPHelp
{
    private static string defaultIP = "192.168.0.2";
   
    //获取本机IP地址
    public static string GetIp()
    {
        string ip4 = null;
        IPAddress[] ips = Dns.GetHostAddresses(Dns.GetHostName());   //Dns.GetHostName()获取本机名Dns.GetHostAddresses()根据本机名获取ip地址组
        foreach (IPAddress ip in ips)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                ip4 = ip.ToString();  //ipv4
                if (!string.IsNullOrEmpty(ip4))
                    return ip4;
            }
        }

        return ip4;
    }

    //判断本机IP与默认IP是否一致
    public static bool IsDefaultID()
    {
        return GetIp() == defaultIP;
    }
}
