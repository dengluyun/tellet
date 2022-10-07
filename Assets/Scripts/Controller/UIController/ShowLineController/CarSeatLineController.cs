using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.UI.Dropdown;

public class CarSeatLineController : MonoBehaviour
{
    private int CarID;

    /// <summary>
    /// 选择的学员
    /// </summary>
    StudentVo Drivervo;
    StudentVo Strikervo;

    /// <summary>
    /// 取消控制此导调台的控制权
    /// </summary>
    public Button ControlBtn;
    /// <summary>
    /// 射手席位下拉列表
    /// </summary>
    private Dropdown StrikerSeatSelete;
    /// <summary>
    /// 驾驶席位下拉列表
    /// </summary>
    private Dropdown DriverSeatSelete;
    List<OptionData> names = new List<OptionData>();


    /// <summary>
    /// 自动选择时，赋值
    /// </summary>
    public void InitSeleteData(int Dvo , int Svo)
    {
        StrikerSeatSelete.value = Svo;
        DriverSeatSelete.value = Dvo;
    }

    public int GetDriverSeatSelete()
    {
        return DriverSeatSelete.value;
    }
    public int GetStrikerSeatSelete()
    {
        return StrikerSeatSelete.value;
    }

    private void Awake()
    {
        StrikerSeatSelete = transform.GetChild(3).GetComponent<Dropdown>();
        DriverSeatSelete = transform.GetChild(4).GetComponent<Dropdown>();
       
        ControlBtn.onClick.AddListener(ControlBtnHandler);
    }


    private void Start()
    {
        List<StudentVo> vos = DataManager.GetIns().trainStus;
        for (int i = 0; i < vos.Count; i++)
        {
            names.Add(new OptionData());
            names[i].text = vos[i].StudentName;
        }
        StrikerSeatSelete.onValueChanged.AddListener(StrikerSeatSeleteHandler);
        DriverSeatSelete.onValueChanged.AddListener(DriverSeatSeleteHandler);

        if (DataManager.GetIns().SchemeVo.Subject == "多车协同训练")
        {
            StrikerSeatSelete.gameObject.SetActive(false);
            DriverSeatSelete.options = names;
            return;
        }
        
        StrikerSeatSelete.options = names;
        DriverSeatSelete.options = names;

        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            StrikerSeatSelete.value = 0;
            DriverSeatSelete.value = 1;
            ControlBtn.gameObject.SetActive(false);
        }
    }

    private void StrikerSeatSeleteHandler(int Index)
    {
        if(DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
           // DriverSeatSelete.captionText.text = Index == 0?names[1].text:names[0].text;
            DriverSeatSelete.value = Index == 0 ? 1 : 0;
            
        }
    }

    private void DriverSeatSeleteHandler(int Index)
    {
        if (DataManager.GetIns().SchemeVo.SubType == SubjectType.onesynergy)
        {
            //StrikerSeatSelete.captionText.text = Index == 0 ? names[1].text : names[0].text;
            StrikerSeatSelete.value = Index == 0 ? 1 : 0;
        }
    }


    private void ControlBtnHandler()
    {
        EventManager.GetIns().ExcuteEvent(EventConstant.CANCELMASTERSTATIONCONTROL_EVENT, gameObject);
        Destroy(this.gameObject,1.0f);
    }

    public void ToDestroy()
    {
        Destroy(this.gameObject);
    }
}
