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
        MainMenu,
        AttackMenu,
        SkillMenu,
        ItemMenu,
        EtcMenu,
        EnemySelectionMenu,
        AllySelectionMenu,
        Locked
    }
    MenuOptions curMod = new MenuOptions();                     //���� �޴� ���

    public Image[] mainActBtns = new Image[4];              //��ư��
    public Color[] selectedMainBtnColor = new Color[4];     //���õ� ��ư�� �����
    public Color[] unselectedMainBtnColors = new Color[4];  //���þȵ� ��ư�� �����


    //���� ��ư �ɼ�
    public enum MainBtnOptions
    {
        Attack,
        Skill,
        Item,
        Etc
    }
    public MainBtnOptions curMainBtnSelect;                 //���� ���õ� ���� ��ư

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
            case MainBtnOptions.Attack:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Skill;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Item;

                break;
            case MainBtnOptions.Skill:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Attack;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Etc;

                break;
            case MainBtnOptions.Item:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Etc;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Attack;

                break;
            case MainBtnOptions.Etc:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Item;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Skill;

                break;
        }
    }

    private void UpdateMainBtnHighlight()                       //���� ��ư ���̶���Ʈ �ٲٱ�
    {
        int selectedMainBtnNum = 0;

        //���� ���õ� ���� ��ư�� ��ȣ�� ��ȯ
        switch (curMainBtnSelect)
        {
            case MainBtnOptions.Attack:
                selectedMainBtnNum = 0;
                break;
            case MainBtnOptions.Skill:
                selectedMainBtnNum = 1;
                break;
            case MainBtnOptions.Item:
                selectedMainBtnNum = 2;
                break;
            case MainBtnOptions.Etc:
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


    // Start is called before the first frame update
    void Start()
    {
        curMod = MenuOptions.MainMenu;
        curMainBtnSelect = MainBtnOptions.Attack;
        UpdateMainBtnHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionalInput();
        switch (curMod)
        {
            case MenuOptions.MainMenu:
                MoveMainMenuCursor();
                UpdateMainBtnHighlight();
                if (Input.GetButtonDown("Submit"))          //Ȯ�� �Է�
                {
                    //���� ���ø޴� Ȯ���ϰ� �ش� �޴� ����
                    switch (curMainBtnSelect)
                    {
                        //TODO: �޴� ����ǰ� �ϱ�
                        case MainBtnOptions.Attack:
                            Debug.Log("����");
                            break;
                        case MainBtnOptions.Skill:
                            Debug.Log("��ų");
                            break;
                        case MainBtnOptions.Item:
                            Debug.Log("������");
                            break;
                        case MainBtnOptions.Etc:
                            Debug.Log("��Ÿ");
                            break;
                    }
                }
                break;
        }
    }
}
