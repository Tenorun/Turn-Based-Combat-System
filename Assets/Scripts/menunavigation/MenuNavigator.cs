using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class MenuNavigator : MonoBehaviour
{

    public GameObject BattleMaster;                         //���� ������ ������Ʈ

    //�޴� �ɼ�
    public enum MenuOptions
    { 
        Main,
        Attack,
        Skill,
        Item,
        Etc,
        EnemySelection,
        AllySelection,
        Locked
    }
    MenuOptions curMenuMod = new MenuOptions();                     //���� �޴� ���

    public Image[] mainActBtns = new Image[4];              //��ư��
    public Color[] selectedMainBtnColor = new Color[4];     //���õ� ��ư�� �����
    public Color[] unselectedMainBtnColors = new Color[4];  //���þȵ� ��ư�� �����


    //���� ��ư �ɼ�
    public enum MainBtn
    {
        Attack,
        Skill,
        Item,
        Etc
    }
    public MainBtn curMainBtnSelect;                 //���� ���õ� ���� ��ư

    /*
    [��  ��][��  ų]
    [������][��  ��]
    */

    [SerializeField] private float HorizontalInput;         //���� �Է�
    [SerializeField] private float VerticalInput;           //���� �Է�

    private void GetDirectionalInput()                      //���� �Է� �ޱ�
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }


    //���� �޴� Ŀ�� �̵�
    private void MoveMainMenuCursor()                               //Ŀ�� �̵��ϱ�
    {
        switch (curMainBtnSelect)
        {
            case MainBtn.Attack:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtn.Skill;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtn.Item;

                break;
            case MainBtn.Skill:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtn.Attack;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtn.Etc;

                break;
            case MainBtn.Item:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtn.Etc;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtn.Attack;

                break;
            case MainBtn.Etc:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtn.Item;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtn.Skill;

                break;
        }
    }

    private void UpdateMainBtnHighlight()                       //���� ��ư ���̶���Ʈ �ٲٱ�
    {
        int selectedMainBtnNum = 0;

        //���� ���õ� ���� ��ư�� ��ȣ�� ��ȯ
        switch (curMainBtnSelect)
        {
            case MainBtn.Attack:
                selectedMainBtnNum = 0;
                break;
            case MainBtn.Skill:
                selectedMainBtnNum = 1;
                break;
            case MainBtn.Item:
                selectedMainBtnNum = 2;
                break;
            case MainBtn.Etc:
                selectedMainBtnNum = 3;
                break;
        }

        for (int i = 0; i <= 3; i++)
        {
            if (i == selectedMainBtnNum)
            {
                mainActBtns[i].color = selectedMainBtnColor[i];
            }
            else
            {
                mainActBtns[i].color = unselectedMainBtnColors[i];
            }
        }
    }



    private RectTransform uiRectTransform;

    public GameObject ItemMenuFrame;
    public GameObject SkillMenuFrame;

    const float _minTopValue_ = -287f;                  //â�� �ּ� ����
    const float _maxTopValue_ = -170f;                  //â�� �ִ� ����
    const float _movingSpeed_ = 100f;                   //â Ȯ�� ��� �ӵ�

    private float topValue;                             //â�� ����
    private float topValueVar = 0.1f;                   //â�� ���� ���� �Լ� ����

    public bool changeSizeTrigger = false;              //â �ٲٱ� Ʈ����
    public bool isExpanded = false;                     //â �þ���� ����


    MenuOptions curTargetMenu = new MenuOptions();

    private void MenuSizeChange()
    {
        if (isExpanded)                 //���̱�
        {
            topValue = _minTopValue_ + 0.05f * Mathf.Pow(topValueVar - 44f, 2);

            if (topValue <= _minTopValue_ + 1)//������ �پ������� ������
            {
                isExpanded = false;
                changeSizeTrigger = false;
                topValue = _minTopValue_;
                topValueVar = 0.1f;

                curMenuMod = MenuOptions.Main;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//â�� ���� ���� �Լ��� ����
            }

            if (topValue < _maxTopValue_)//ũ�� ǥ�� ����
            {
                uiRectTransform.offsetMax = new Vector2(-159, topValue);
            }
        }
        else                            //�ø���
        {
            topValue = _maxTopValue_ - 0.05f * Mathf.Pow(topValueVar - 44f, 2);
            if (topValue >= _maxTopValue_ - 1)//������ �þ���� ������
            {
                isExpanded = true;
                changeSizeTrigger = false;
                topValue = _maxTopValue_;
                topValueVar = 0.1f;

                curMenuMod = curTargetMenu;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//â�� ���� ���� �Լ��� ����
            }

            if (topValue > _minTopValue_)//ũ�� ǥ�� ����
            {
                uiRectTransform.offsetMax = new Vector2(-159, topValue);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curMenuMod = MenuOptions.Main;
        curMainBtnSelect = MainBtn.Attack;
        UpdateMainBtnHighlight();


        uiRectTransform = GetComponent<RectTransform>();

        uiRectTransform.offsetMax = new Vector2(-159, _minTopValue_);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionalInput();
        switch (curMenuMod)
        {
            //���� �޴�
            case MenuOptions.Main:
                MoveMainMenuCursor();
                UpdateMainBtnHighlight();
                if (Input.GetButtonDown("Submit"))          //Ȯ�� �Է�
                {
                    //���� ���ø޴� Ȯ���ϰ� �ش� �޴� ����
                    switch (curMainBtnSelect)
                    {
                        //TODO: �޴� ����ǰ� �ϱ�
                        case MainBtn.Attack:
                            Debug.Log("����");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Attack;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Skill:
                            Debug.Log("��ų");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Skill;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Item:
                            Debug.Log("������");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Item;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Etc:
                            Debug.Log("��Ÿ");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Etc;
                            changeSizeTrigger = true;
                            break;
                    }
                }
                break;
            //���� �޴�
            case MenuOptions.Attack:
                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    changeSizeTrigger = true;
                }
                break;
            //��ų �޴�
            case MenuOptions.Skill:

                if (!SkillMenuFrame.activeSelf) SkillMenuFrame.SetActive(true);

                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    SkillMenuFrame.SetActive(false);
                    changeSizeTrigger = true;
                }
                break;
            //������ �޴�
            case MenuOptions.Item:

                if (!ItemMenuFrame.activeSelf) ItemMenuFrame.SetActive(true);

                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    ItemMenuFrame.SetActive(false);
                    changeSizeTrigger = true;
                }
                break;
            //��Ÿ �޴�
            case MenuOptions.Etc:
                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    changeSizeTrigger = true;
                }
                break;
            case MenuOptions.Locked:
                break;

        }

        if (changeSizeTrigger)
        {
            MenuSizeChange();
        }
    }
}
