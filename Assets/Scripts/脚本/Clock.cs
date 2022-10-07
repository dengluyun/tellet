using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// 计时器，训练开始计时器，训练时间的开始结束
/// </summary>
public class Clock : MonoBehaviour
{
    public bool IsStart = true;
    public bool IsStop = false;  
    public bool isrecover = false;
    //存储时分秒显示器
    public  List<Text> texts_List = new List<Text>();
    float second=0;
    float second1 = 0;
    float minute = 0;
    float minute1 = 0;
    float hour = 0;
    float hour1 = 0;
    public static float time = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(timeCount());
       
    }

    // Update is called once per frame
    void Update()
    {
        changeTime();
        showText(texts_List[0], second1);
        showText(texts_List[1], second);
        showText(texts_List[2], minute1);
        showText(texts_List[3], minute);
        showText(texts_List[4], hour1);
        showText(texts_List[5], hour);
    }


    IEnumerator timeCount()
    {
        while (IsStart)
        {
            yield return new WaitForSeconds(1);
            second1 += 1;
            if (second1 == 10)
            {
                second1 = 0;
                second += 1;
                if (second == 6)
                {
                    second = 0;
                    minute1 += 1;
                    if (minute1 == 10)
                    {
                        minute1 = 0;
                        minute += 1;
                        if (minute == 6)
                        {
                            minute = 0;
                            hour1 += 1;
                            if (hour1 == 10)
                            {
                                hour1 = 0;
                                hour += 1;
                                if (hour == 10)
                                {
                                    //时间太长，停止计时
                                    StopCoroutine(timeCount());
                                }
                            }
                        }
                    }
                }
            }
        }
    }

    public void changeTime()
    {
    time= (int.Parse(second.ToString())*10 + int.Parse(second1.ToString())) + (int.Parse(minute.ToString())*600 + int.Parse(minute1.ToString())*60 + int.Parse(hour.ToString())*36000)+ (int.Parse(hour1.ToString())*3600);
      
    }


    public void showText(Text text, float time)
    {
        text.text = time.ToString();        
    }

    //停止计时
    IEnumerator  stopClock()
    {
        yield return new WaitUntil(() => { return IsStop; });    
            IsStart = false;
            IsStop = false;
    }

    //恢复计时
    IEnumerator recoverTime_Count()
    {
        yield return new WaitUntil(() => { return isrecover; });
        IsStart = true;
        StartCoroutine(timeCount());
    }

    //暂停时间
    public void stop()
    {
        StartCoroutine(stopClock());
    }
    //恢复时间
    public void recover()
    {
        StartCoroutine(recoverTime_Count());
    }



}
