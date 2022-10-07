using System.Linq;

namespace Whatever
{
    public class Tools
    {
        public static UnityEngine.Vector3 GetVector3ByString(string vectorString)
        {
            var ret = UnityEngine.Vector3.zero;
            var strs = vectorString.Split(',');
            var first = strs[0];
            var second = strs[1];
            var third = strs[2];
            var x = float.Parse(first.Substring(1));
            var y = float.Parse(second);
            var z = float.Parse(third.Substring(0, third.Length-1));
            return ret = new UnityEngine.Vector3(x,y,z);
        }

        public static UnityEngine.Quaternion GetQuaternionByString(string quaternionString)
        {
            var ret = UnityEngine.Quaternion.identity;
            var strs = quaternionString.Split(',');
            var first = strs[0];
            var second = strs[1];
            var third = strs[2];
            var forth = strs[3];
            var x = float.Parse(first.Substring(1));
            var y = float.Parse(second);
            var z = float.Parse(third);
            var w = float.Parse(forth.Substring(0, forth.Length - 1));
            return ret = new UnityEngine.Quaternion(x, y, z,w);
        }

        public static string GetLocalIPv4()
        {
                var ipv4s = System.Net.Dns.GetHostAddresses(System.Net.Dns.GetHostName()) //根据主机名称解析主机ip地址
                            .Where(p => p.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork) //筛选出所有ipv4地址
                            .Select(p => p.MapToIPv4().ToString());
                if (ipv4s.Count() > 1)
                    return ipv4s.Where(p => p.StartsWith("192")).FirstOrDefault(); //如果有多个，返回192开头的ipv4地址
                else
                    return ipv4s.FirstOrDefault();
            
        }

    }

}