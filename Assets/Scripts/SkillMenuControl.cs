using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuControl : MonoBehaviour
{
    public bool[] isSlotFilled = new bool[4] { true, true, true, true };

    private int languageVal;

    public GameObject skillDatabase;
    public GameObject playerCharDatabase;
    public GameObject battleMaster;

    public Image[] skillBtnBaseFrame;

    public Image[] skillIconFrame;

    public Sprite[] skillIconImage;
    public Image[] skillIcon;

    public GameObject[] pointerSquare;
    public TextMeshProUGUI[] skillBtnText;

    public TextMeshProUGUI skillDescriptionNameText;
    public TextMeshProUGUI skillDescriptionText;

    private string[,] skillTypeName = new string[9, 2]
    { {"특수 공격", "Special attack" }
        ,{"물리 공격", "Physical attack"}
        ,{"상태 이상", "Bad effect"}
        ,{"회복", "Heal"}
        ,{"스텟 업", "Buff"}
        ,{"스텟 다운", "Debuff"}
        ,{"스텟 변화", "Status change"}
        ,{"기타", "Etc"}
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

        //현재 선택중인 캐릭터로 검색 설정
        playerCharDatabase.GetComponent<PlayerCharacterData>().SetSearchCharacter(battleMaster.GetComponent<BattleMaster>().charSelecting);

        //캐릭터 스킬 슬롯 캐시 업데이트
        skillSlotCache = playerCharDatabase.GetComponent<PlayerCharacterData>().character.SkillSlot;

        for(int i = 0; i < 4; i++)
        {
            //스킬 검색 설정
            skillDatabase.GetComponent<SkillData>().SetSearchSkill(skillSlotCache[i]);

            skillNameCache[i] = skillDatabase.GetComponent<SkillData>().skill.SkillName[languageVal];                   //이름
            skillDescriptionCache[i] = skillDatabase.GetComponent<SkillData>().skill.SkillDescription[languageVal];     //설명문
            skillTypeCache[i] = skillDatabase.GetComponent<SkillData>().skill.SkillType;                                //타입
            skillCostCache[i] = skillDatabase.GetComponent<SkillData>().skill.SkillCost;                                //SP 비용
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

        //아이콘 업데이트
        for (int i = 0; i < 4; i++)
        {
            //아이콘 변경
            skillIcon[i].sprite = skillIconImage[skillTypeCache[i]];
            //스킬 아이콘 이름 변경
            skillBtnText[i].text = skillNameCache[i];

            //선택 여부에 따른 업데이트
            if (i == currentSelectNum)
            {

                //선택 버튼 업데이트
                skillBtnBaseFrame[i].color = Color.black;
                skillIconFrame[i].color = Color.white;
                skillIcon[i].color = Color.white;
                skillBtnText[i].color = Color.white;
                pointerSquare[i].SetActive(true);

                //스킬 설명문 이름 변경
                skillDescriptionNameText.text = skillNameCache[i];
                //스킬 설명문 변경
                skillDescriptionText.text = skillDescriptionCache[i];
                //스킬 타입 이름 변경
                skillTypeText.text = skillTypeName[skillTypeCache[i], languageVal];


                //스킬 비용 표시

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
                //비선택 버튼 업데이트
                skillBtnBaseFrame[i].color = Color.white;
                skillIconFrame[i].color = Color.black;
                skillIcon[i].color = Color.black;
                skillBtnText[i].color = Color.black;
                pointerSquare[i].SetActive(false);
            }
        }
    }

    void ExecuteSkill(int skillID)
    {
        if(Input.GetButtonDown("Submit") && skillID != 0)
        {
            skillDatabase.GetComponent<SkillData>().UseSkillEffect(skillID);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        languageVal = skillDatabase.GetComponent<SkillData>().languageVal;
        currentSelectNum = 0;

        GetCache();
    }

    // Update is called once per frame
    void Update()
    {
        GetCache();
        SetSlotFillStatus();

        GetDirectionalInput();
        UpdateCurrentSelectionNum();
        ExecuteSkill(skillSlotCache[currentSelectNum]);

        UpdateDisplay();
    }
}
