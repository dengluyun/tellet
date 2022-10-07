using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CreatObjectPanelController : BaseView
{
    /// <summary>
    /// �����ͽ������·��
    /// </summary>
    public static string ViewName = "UIPanel/ShowPanel/CreatPanel";
    /// <summary>
    /// �رս��水ť
    /// </summary>
    public Button CloseBtn;
    /// <summary>
    /// ����������ť
    /// </summary>
    public Button CreateBtn;

    /// <summary>
    /// ��������
    /// </summary>
    public InputField ProjectName;

    /// <summary>
    /// ѵ������-����ѵ��
    /// </summary>
    public Toggle _drill;

    /// <summary>
    /// ѵ������-����Эͬѵ��Эͬѵ��
    /// </summary>
    public Toggle _OneSynergyTrain;

    /// <summary>
    /// ѵ������-Эͬѵ��
    /// </summary>
    public Toggle _SynergyTrain;

    /// <summary>
    /// �ж�ѵ������
    /// </summary>
    private SubjectType IsTrainType =  SubjectType.Single;

    /// <summary>
    /// �б�-�����б��
    /// </summary>
    public List<Dropdown> dropdowns = new List<Dropdown>();


    private void Awake()
    {
        InitBtnOnClick();
        InitDropdownSeleteEvent();
        
    }

    public override void RefleshView()
    {
        IsTrainType = SubjectType.Single;
        for (int i = 0; i < dropdowns.Count; i++)
        {
            dropdowns[i].ClearOptions();
        }
        addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
        addOptions(dropdowns[1], DataManager.GetIns().weather);
        addOptions(dropdowns[2], DataManager.GetIns().enenvironmentSetUp);
        addOptions(dropdowns[3], DataManager.GetIns().time);
    }

    /// <summary>
    /// ��ʼ��������ѡ���¼�
    /// </summary>
    private void InitDropdownSeleteEvent()
    {
        dropdowns[0].onValueChanged.AddListener(SeleteSubjectOnValue);
    }
    /// <summary>
    /// ��ѡ���Ŀʱ
    /// </summary>
    private void SeleteSubjectOnValue(int Index)
    {
        string[] strs = new string[] { };

        if (_drill.isOn)
        {
            strs = DataManager.GetIns().DrillContent.Split('��');
            if(strs[Index] == "��ʻ")
            {
                addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
            }
            else if (strs[Index] == "����")
            {
                addOptions(dropdowns[2], DataManager.GetIns().enenvironmentSetUp);
            }
            else if (strs[Index] == "��ֹĿ�����" || strs[Index] == "�˶�Ŀ�����" || strs[Index] == "����Ŀ�����")
            {
                addOptions(dropdowns[2], DataManager.GetIns().enenvironmentShoot);
            }
        }
        else if (_OneSynergyTrain.isOn)
        {
            strs = DataManager.GetIns().OneSynergyContent.Split('��');
            addOptions(dropdowns[2], DataManager.GetIns().Shootenenvironment);
        }
        else
        {
            strs = DataManager.GetIns().SynergyContent.Split('��');
            addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
        }
        


    }

    private void Start()
    {
        if (_drill.isOn)
        {
            addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
        }
        else if(_OneSynergyTrain.isOn)
        {
            addOptions(dropdowns[0], DataManager.GetIns().OneSynergyContent);
        }
        else
        {
            addOptions(dropdowns[0], DataManager.GetIns().SynergyContent);
        }
    }



    private void Update()
    {
        OnToggleClick();
        switch (IsTrainType)
        {
            case SubjectType.Single:
                _drill.isOn = true;
                _SynergyTrain.isOn = false;
                _OneSynergyTrain.isOn = false;
                break;
            case SubjectType.onesynergy:
                _drill.isOn = false;
                _SynergyTrain.isOn = false;
                _OneSynergyTrain.isOn = true;
                break;
            case SubjectType.synergy:
                _drill.isOn = false;
                _SynergyTrain.isOn = true;
                _OneSynergyTrain.isOn = false;
                break;
            default:
                break;
        }
    }

    /// <summary>
    /// ��ʼ����ť����¼�
    /// </summary>
    private void InitBtnOnClick()
    {
        CreateBtn.onClick.AddListener(CreatScheme);
        CloseBtn.onClick.AddListener(CloseFace);
    }

    //�����б����ݵ����,���ݸ�ʽ�Զ��Ÿ���
    public static void addOptions(Dropdown dropdown, string options)
    {
        dropdown.ClearOptions();
        //�����и����
        string[] S = options.Split('��');
        List<string> L = new List<string>();
        L.AddRange(S);
        dropdown.ClearOptions();
        dropdown.AddOptions(L);

    }

    /// <summary>
    /// ��ѡ��
    /// </summary>
    public void OnToggleClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameObject g = EventSystem.current.currentSelectedGameObject;
            if (g != null)
            {
                if (g.name == "SingleTrain")
                {
                    if (!_drill.isOn)
                    {
                        _drill.isOn = true;
                        _SynergyTrain.isOn = false;
                        _OneSynergyTrain.isOn = false;
                        IsTrainType =  SubjectType.Single;
                        addOptions(dropdowns[0], DataManager.GetIns().DrillContent);
                    }   
                }
                else if (g.name == "SynergyTrain")
                {
                    if (!_SynergyTrain.isOn)
                    {
                        _drill.isOn = false;
                        _SynergyTrain.isOn = true;
                        _OneSynergyTrain.isOn = false;
                        IsTrainType = SubjectType.synergy;
                        addOptions(dropdowns[0], DataManager.GetIns().SynergyContent);
                        addOptions(dropdowns[2], DataManager.GetIns().Allenenvironment);
                    }
                }
                else if (g.name == "OneSynergyTrain")
                {
                    if (!_OneSynergyTrain.isOn)
                    {
                        _drill.isOn = false;
                        _SynergyTrain.isOn = false;
                        _OneSynergyTrain.isOn = true;
                        IsTrainType = SubjectType.onesynergy;
                        addOptions(dropdowns[0], DataManager.GetIns().OneSynergyContent);
                        addOptions(dropdowns[2], DataManager.GetIns().Shootenenvironment);
                    }
                }
            }
        }
    }

    /// <summary>
    /// ������Ŀ����
    /// </summary>
    public void CreatScheme()
    {
        if(ProjectName.text == "")
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "����������Ϊ�գ�");
            return;
        }
        if (DataManager.GetIns().CheckSchemeVo(ProjectName.text))
        {
            UIManager.GetIns().Show<TippanelController>(UIType.Show_View);
            EventManager.GetIns().ExcuteEvent(EventConstant.HINTTEXT_EVENT, "���з��������������������");
            return;
        }

        SchemeVo vo = new SchemeVo();
        vo.ID = DataManager.GetIns().schemeDic.Count + 1;
        vo.ProjectName = ProjectName.text;
        vo.SubType = IsTrainType;
        vo.Subject = dropdowns[0].captionText.text;
        vo.Weather = dropdowns[1].captionText.text;
        vo.Environment = dropdowns[2].captionText.text;
        vo.DataTime = dropdowns[3].captionText.text;

        bool isTrue = DBManager.Getins().editor<SchemeVo>(SQLEditorType.insert, vo, TableConst.SCHEMETABLE);

        if (isTrue)
        {
            EventManager.GetIns().ExcuteEvent(EventConstant.DELETEOBJECT_EVENT, true);
            Close();
        }


        //GameObject ObjectLine = Instantiate();
    }

    private void CloseFace()
    {
        this.Close();
    }
}
