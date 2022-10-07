
using System;

public class MyData
{
    public enum ShowTypeEnum
    {
        TIPS,
        CONFIRM,
    }

    private Action<bool> callback;

    private string m_tips;
    private ShowTypeEnum m_showType;

    public Action<bool> Callback { get => callback; set => callback = value; }
    public string Tips { get => m_tips; set => m_tips = value; }
    public ShowTypeEnum ShowType { get => m_showType; set => m_showType = value; }
}
