using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;
public class Video_PlayerController : BaseView
{

    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/MoviePlayPanel";
    public GameObject mv_obj;
    //public VideoClip shipin1;
    public VideoPlayer videoPlayer;

    public Button play_button;   //���Ű�ť
    public Button pause_button;  //��ͣ��ť
    public Button exit_button;   //�رս���
    public Text mv_length_text;   //��ʾ��Ƶ�ĳ���
    public Slider slider_kuajin;
    private bool isDrag;
    private bool isStart;
    private int hour, minute, second;  // ʱ  ��  ��
    // Start is called before the first frame update
    void Start()
    { 
        play_button.gameObject.SetActive(true);
        pause_button.gameObject.SetActive(false);
        play_button.onClick.AddListener(MV_Play);
        pause_button.onClick.AddListener(MV_Pause);
        exit_button.onClick.AddListener(delegate
        {
            OnorOff_MVjiemian(false);
        });
        ShiPin_Length();
        // ÿ�ε����βʱ
        videoPlayer.loopPointReached += EndReached;
    }

    public override void RefleshView()
    {
        play_button.gameObject.SetActive(true);
        pause_button.gameObject.SetActive(false);
        videoPlayer.url = DataManager.GetIns().VideoPath + DataManager.GetIns().NeedLook;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        Close();
    }
    //�򿪻��߹ر���Ƶ����
    public void OnorOff_MVjiemian(bool tra)
    {
        Close();
    }
    //��Ƶ����
    public void MV_Play()
    {
        play_button.gameObject.SetActive(false);
        pause_button.gameObject.SetActive(true);
        videoPlayer.Play();
    }
    //��Ƶ��ͣ
    public void MV_Pause()
    {
        play_button.gameObject.SetActive(true);
        pause_button.gameObject.SetActive(false);
        videoPlayer.Pause();
        isStart = false;
    }
    //��ʾ��Ƶ��ʱ��
    public void ShiPin_Length()
    {
        hour = (int)videoPlayer.time / 3600;
        minute = ((int)videoPlayer.time - hour * 3600) / 60;
        second = (int)videoPlayer.time - hour * 3600 - minute * 60;
        mv_length_text.text = string.Format("{0:D2}:{1:D2}:{2:D2}", hour, minute, second);
    }
    /// �޸���Ƶ���Ž���  
    private void ChangeVideoPlayTime()
    {
        if (isDrag == true)
        {
            videoPlayer.time = slider_kuajin.value * videoPlayer.length;
        }
    }

    //�޸Ľ�����
    public void XiuGaiJinDuTiao()
    {
        slider_kuajin.value = (float)(videoPlayer.time / videoPlayer.length);
    }
    //��ʼ��ק
    public void OnDragdrop()
    {
        isDrag = true;
    }
    //������ק
    public void OnEndDrag()
    {
        isDrag = false;
        videoPlayer.time = slider_kuajin.value * videoPlayer.length;

    }
    // Update is called once per frame
    void Update()
    {

        ChangeVideoPlayTime();
        ShiPin_Length();
        if (!isDrag)
        {
            slider_kuajin.transform.GetChild(2).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0, 0);
            slider_kuajin.transform.GetChild(1).GetChild(0).GetComponent<RectTransform>().anchoredPosition = new Vector2(0,0f);
            XiuGaiJinDuTiao();
        }

    }
}