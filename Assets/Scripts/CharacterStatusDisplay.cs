using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static PlayerCharacterData;

public class CharacterStatusDisplay : MonoBehaviour
{
    public GameObject PlayerCharData;
    public GameObject BattleMaster;
    public int displayerID;

    public int characterID;

    int currentHPCache;
    int currentSPCache;
    int maxHPCache;
    int maxSPCache;

    public TextMeshProUGUI StatusText;

    public Sprite[] DigitImage;

    public Image[] HPDisplay;
    public Image[] SPDisplay;

    public GameObject HPBar;
    public GameObject SPBar;

    public bool HPDownTrigger = false;
    public bool SPDownTrigger = false;

    //Status Frame Elements
    public Image BaseFrame;
    public Image EdgeLine;
    public Image PortraitFrame;
    public Image StatusFrame;
    public Image HitAndSpecialStencil;

    public void UpdateDisplay(int ID)
    {
        //ĳ�� ������Ʈ
        PlayerCharData.GetComponent<PlayerCharacterData>().SetSearchCharacter(ID);

        currentHPCache = PlayerCharData.GetComponent<PlayerCharacterData>().character.CurrentHp;
        currentSPCache = PlayerCharData.GetComponent<PlayerCharacterData>().character.CurrentSp;
        maxHPCache = PlayerCharData.GetComponent<PlayerCharacterData>().character.MaxHp;
        maxSPCache = PlayerCharData.GetComponent<PlayerCharacterData>().character.MaxSp;



        //���� ���÷��� ������Ʈ
        string curHPStr;
        string curSPStr;
        if (currentHPCache >= 0) curHPStr = currentHPCache.ToString();
        else curHPStr = "0";

        if (currentSPCache >= 0) curSPStr = currentSPCache.ToString();
        else curSPStr = "0";



        //HP ������Ʈ
        for (int i = 0; i < HPDisplay.Length && i < curHPStr.Length; i++)
        {
            HPDisplay[i].sprite = DigitImage[int.Parse(curHPStr[i].ToString())];
        }

        //SP ������Ʈ
        for (int i = 0; i < SPDisplay.Length && i < curSPStr.Length; i++)
        {
            SPDisplay[i].sprite = DigitImage[int.Parse(curSPStr[i].ToString())];
        }

        //�� ������Ʈ
        RectTransform targetBar;

        float HPRightStretch = Mathf.Max(0, 45f - ((float)currentHPCache / maxHPCache * 44f + 1f));
        float SPRightStretch = Mathf.Max(0, 45f - ((float)currentSPCache / maxSPCache * 44f + 1f));

        if(HPBar != null)
        {
            targetBar = HPBar.GetComponent<RectTransform>();
            targetBar.offsetMax = new Vector2(HPRightStretch * -1, 0);
        }
        if(SPBar != null)
        {
            targetBar = SPBar.GetComponent<RectTransform>();
            targetBar.offsetMax = new Vector2(SPRightStretch * -1, -16);
        }

        //���� ������Ʈ
        UpdateColor();
    }

    void UpdateColor()
    {

        string curHPStr;
        string curSPStr;
        if (currentHPCache >= 0) curHPStr = currentHPCache.ToString();
        else curHPStr = "0";

        if (currentSPCache >= 0) curSPStr = currentSPCache.ToString();
        else curSPStr = "0";

        //���õ� ĳ������ ����â ���� ������Ʈ
        if (displayerID == BattleMaster.GetComponent<BattleMaster>().curSelectOrderNum)
        {
            //HP ������Ʈ
            for (int i = 0; i < HPDisplay.Length && i < curHPStr.Length; i++)
            {
                HPDisplay[i].color = Color.white;
            }
            for (int i = curHPStr.Length; i < HPDisplay.Length; i++)
            {
                HPDisplay[i].color = Color.black; 
            }

            //SP ������Ʈ
            for (int i = 0; i < SPDisplay.Length && i < curSPStr.Length; i++)
            {
                SPDisplay[i].color = Color.white;
            }
            for (int i = curSPStr.Length; i < SPDisplay.Length; i++)
            {
                SPDisplay[i].color = Color.black;
            }

            //������ ���� ������Ʈ
            BaseFrame.color = Color.black;
            EdgeLine.color = Color.white;
            PortraitFrame.color = Color.white;
            StatusFrame.color = Color.white;
            StatusText.color = Color.white;
            HitAndSpecialStencil.color = Color.black;
        }
        else
        {
            //HP ������Ʈ
            for (int i = 0; i < HPDisplay.Length && i < curHPStr.Length; i++)
            {
                HPDisplay[i].color = Color.black;
            }
            for (int i = curHPStr.Length; i < HPDisplay.Length; i++)
            {
                HPDisplay[i].color = Color.white;
            }

            //SP ������Ʈ
            for (int i = 0; i < SPDisplay.Length && i < curSPStr.Length; i++)
            {
                SPDisplay[i].color = Color.black;
            }
            for (int i = curSPStr.Length; i < SPDisplay.Length; i++)
            {
                SPDisplay[i].color = Color.white;
            }

            //������ ���� ������Ʈ
            BaseFrame.color = Color.white;
            EdgeLine.color = Color.black;
            PortraitFrame.color = Color.black;
            StatusFrame.color = Color.black;
            StatusText.color = Color.black;
            HitAndSpecialStencil.color = Color.white;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(LateStart(0.01f));
    }

    // Update is called once per frame
    void Update()
    {
        if (HPDownTrigger)
        {
            HPDownTrigger = false;
            PlayerCharData.GetComponent<PlayerCharacterData>().SetSearchCharacter(characterID);
            PlayerCharData.GetComponent<PlayerCharacterData>().character.CurrentHp -= 10;
            UpdateDisplay(characterID);
        }
        else if (SPDownTrigger)
        {
            SPDownTrigger = false;
            PlayerCharData.GetComponent<PlayerCharacterData>().SetSearchCharacter(characterID);
            PlayerCharData.GetComponent<PlayerCharacterData>().character.CurrentSp -= 10;
            UpdateDisplay(characterID);
        }

        UpdateDisplay(characterID);
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        UpdateDisplay(characterID);
    }
}
