using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuControl : MonoBehaviour
{
    public bool[] isSlotFilled = new bool[4] { true, true, true, true };

    private int languageVal;                                    //��� ��

    public GameObject skillDatabase;
    public GameObject playerCharDatabase;
    public GameObject battleMaster;
    public GameObject Menu;

    public Image[] skillBtnBaseFrame;

    public Image[] skillIconFrame;

    public Sprite[] skillIconImage;
    public Image[] skillIcon;

    public GameObject[] pointerSquare;
    public TextMeshProUGUI[] skillBtnText;

    public TextMeshProUGUI skillDescriptionNameText;
    public TextMeshProUGUI skillDescriptionText;

    private string[,] skillTypeName = new string[9, 2]          //��ų Ÿ�� �̸�
    { {"Ư�� ����", "Special attack" }
        ,{"���� ����", "Physical attack"}
        ,{"���� �̻�", "Bad effect"}
        ,{"ȸ��", "Heal"}
        ,{"���� ��", "Buff"}
        ,{"���� �ٿ�", "Debuff"}
        ,{"���� ��ȭ", "Status change"}
        ,{"��Ÿ", "Etc"}
        ,{"",""}};
    public TextMeshProUGUI skillTypeText;

    public Sprite[] digitImage;
    public Image[] skillCostDisplay;

    public int[] skillSlotCache = new int[4];

    private string[] skillNameCache = new string[4];
    private string[] skillDescriptionCache = new string[4];
    private int[] skillTypeCache = new int[4];
    private int[] skillCostCache = new int[4];

    public int currentSelectNum = 0;
    private int currentCharID;                                  //���� ĳ���� ID

    private float verticalInput = 0f;
    private float horizontalInput = 0f;
    void GetDirectionalInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    void SetSlotFillStatus()
    {
        for (int i = 0; i < 4; i++)
        {
            if (skillSlotCache[i] == 0) isSlotFilled[i] = false;
            else isSlotFilled[i] = true;
        }
    }

    void GetCache()
    {

        //���� �������� ĳ���ͷ� �˻� ����
        playerCharDatabase.GetComponent<PlayerCharacterData>().SetSearchCharacter(currentCharID);

        //ĳ���� ��ų ���� ĳ�� ������Ʈ
        skillSlotCache = playerCharDatabase.GetComponent<PlayerCharacterData>().resultCharacterData.SkillSlot;

        for(int i = 0; i < 4; i++)
        {
            //��ų �˻� ����
            skillDatabase.GetComponent<SkillData>().SetSearchSkill(skillSlotCache[i]);

            skillNameCache[i] = skillDatabase.GetComponent<SkillData>().resultSkillData.SkillName[languageVal];                   //�̸�
            skillDescriptionCache[i] = skillDatabase.GetComponent<SkillData>().resultSkillData.SkillDescription[languageVal];     //����
            skillTypeCache[i] = skillDatabase.GetComponent<SkillData>().resultSkillData.SkillType;                                //Ÿ��
            skillCostCache[i] = skillDatabase.GetComponent<SkillData>().resultSkillData.SkillCost;                                //SP ���
        }
    }

    

    //0 1
    //2 3
    void UpdateCurrentSelectionNum()
    {
        switch(currentSelectNum)
        {
            case 0:
                if (horizontalInput >= 0.3f) currentSelectNum = 1;
                else if (verticalInput <= -0.3f) currentSelectNum = 2;
                break;
            case 1:
                if (horizontalInput <= -0.3f) currentSelectNum = 0;
                else if (verticalInput <= -0.3f) currentSelectNum = 3;
                break;
            case 2:
                if (horizontalInput >= 0.3f) currentSelectNum = 3;
                else if (verticalInput >= 0.3f) currentSelectNum = 0;
                break;
            case 3:
                if (horizontalInput <= -0.3f) currentSelectNum = 2;
                else if (verticalInput >= 0.3f) currentSelectNum = 1;
                break;

        }
    }

    void UpdateDisplay()
    {
        string skillCostString;

        //������ ������Ʈ
        for (int i = 0; i < 4; i++)
        {
            //������ ����
            skillIcon[i].sprite = skillIconImage[skillTypeCache[i]];
            //��ų ������ �̸� ����
            skillBtnText[i].text = skillNameCache[i];

            //���� ���ο� ���� ������Ʈ
            if (i == currentSelectNum)
            {

                //���� ��ư ������Ʈ
                skillBtnBaseFrame[i].color = Color.black;
                skillIconFrame[i].color = Color.white;
                skillIcon[i].color = Color.white;
                skillBtnText[i].color = Color.white;
                pointerSquare[i].SetActive(true);

                //��ų ���� �̸� ����
                skillDescriptionNameText.text = skillNameCache[i];
                //��ų ���� ����
                skillDescriptionText.text = skillDescriptionCache[i];
                //��ų Ÿ�� �̸� ����
                skillTypeText.text = skillTypeName[skillTypeCache[i], languageVal];


                //��ų ��� ǥ��

                if (isSlotFilled[i]) skillCostString = skillCostCache[i].ToString();
                else skillCostString = "";

                for(int j = 0; j < 3; j++)
                {
                    if(skillCostString.Length > j)
                    {
                        skillCostDisplay[j].color = Color.black;
                        skillCostDisplay[j].sprite = digitImage[int.Parse(skillCostString[j].ToString())];
                    }
                    else
                    {
                        skillCostDisplay[j].color = Color.white;
                    }
                }
            }
            else
            {
                //���� ��ư ������Ʈ
                skillBtnBaseFrame[i].color = Color.white;
                skillIconFrame[i].color = Color.black;
                skillIcon[i].color = Color.black;
                skillBtnText[i].color = Color.black;
                pointerSquare[i].SetActive(false);
            }
        }
    }

    void SubmitSkill(int skillID)
    {
        if(Input.GetButtonDown("Submit") && skillID != 0)
        {
            skillDatabase.GetComponent<SkillData>().SetSearchSkill(skillID);
            int targetType = skillDatabase.GetComponent<SkillData>().resultSkillData.UseTarget;

            battleMaster.GetComponent<BattleMaster>().SubmitAction(currentCharID, 1, skillID, targetType);

            //Menu.GetComponent<MenuWaker>().SetChangeMenu(false, 1);

            skillDatabase.GetComponent<SkillData>().UseSkillEffect(skillID);        //����׿�

            //�ݱ�
            Menu.GetComponent<MenuNavigator>().ShrinkMenuToMain();
            this.gameObject.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        languageVal = battleMaster.GetComponent<BattleMaster>().languageVal;
        currentCharID = battleMaster.GetComponent<BattleMaster>().IDofSelectingChar;
        currentSelectNum = 0;

        GetCache();
    }

    // Update is called once per frame
    void Update()
    {
        currentCharID = battleMaster.GetComponent<BattleMaster>().IDofSelectingChar;

        GetCache();
        SetSlotFillStatus();

        GetDirectionalInput();
        UpdateCurrentSelectionNum();
        SubmitSkill(skillSlotCache[currentSelectNum]);

        UpdateDisplay();
    }
}
