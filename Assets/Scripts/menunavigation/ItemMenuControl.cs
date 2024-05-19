using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour
{
    //TODO: ItemMenuPointer, ItemMenuPointer, ItemExecuter로 파편화된 코드를 하나로 통합시키는 것.
    public GameObject battleMaster;
    public GameObject ItemDB;
    public GameObject Menu;

    public Image[] ItemPointerArrows;                   //아이템 화살표 오브젝트 이미지
    public int itemArrowNum = 0;                        //아이템 화살표 번호
    public int currentPageNum = 1;                      //현재 페이지 번호
    public int pointingItemID;                          //지목 아이템 ID
    [SerializeField] private int pageLength;            //페이지 최대 길이 번호

    public Sprite[] PageDigit;                          //현재 페이지 번호 파일 이미지
    public Sprite[] InventorySizeDigit;                 //인벤토리 크기 번호 파일 이미지

    public Image PageText;                               //현재 페이지 번호 오브젝트 이미지
    public Image InventorySizeText;                      //인벤토리 크기 번호 오브젝트 이미지


    //세로 입력 항목들
    private float vertInputDelayTime = 0f;              //세로 입력 딜레이 시간 값
    private float verticalInput = 0f;                   //세로 입력값
    private float prevVerticalInput;                    //이전 세로 입력값
    private bool vertReleased = true;                   //세로 한번 풀림

    //가로 입력 항목들
    private float horInputDelayTime = 0f;               //가로 입력 딜레이 시간 값
    private float horizontalInput = 0f;                 //가로 입력값
    private float prevHorizontalInput;                  //이전 가로 입력값
    private bool horReleased = true;                    //가로 한번 풀림

    //인벤토리 캐시
    public GameObject PlayerCharDB;
    public List<int> inventoryCache = new List<int>();
    public int[] previewInventory = new int[5];

    //아이템 설명문 관련
    public TextMeshProUGUI[] MenuItemNameText;          //아이템 선택 메뉴 표시 이름
    public TextMeshProUGUI DescriptionItemNameText;     //아이템 설명문 표시 이름
    public TextMeshProUGUI ItemDescriptionText;         //아이템 설명문

    private int currentCharID;                          //현재 캐릭터 ID
    private int prevCharID;                             //이전 캐릭터 ID
    public string[] itemNames = new string[5];          //아이템 이름
    public string itemDescription;                      //아이템 설명문

    private void GetDirectionalInput()
    {
        //세로 입력 받기
        prevVerticalInput = verticalInput;
        verticalInput = Input.GetAxisRaw("Vertical");
        //이전 입력 세로 값이 입력 인식 범위 안이고, 현 입력값이 인식 범위 밖이면 세로 한번 풀렸다고 함
        if (prevVerticalInput >= 0.3f || prevVerticalInput <= -0.3f)
        {
            if (verticalInput < 0.3f && verticalInput > -0.3f) vertReleased = true;
        }

        //가로 입력 받기
        prevHorizontalInput = horizontalInput;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //이전 입력 가로 값이 입력 인식 범위 안이고, 현 입력값이 인식 범위 밖이면 가로 한번 풀렸다고 함
        if (prevHorizontalInput >= 0.3f || prevHorizontalInput <= -0.3f)
        {
            if (horizontalInput < 0.3f && horizontalInput > -0.3f) horReleased = true;
        }
    }

    //아이템 화살표 번호 업데이트
    void UpdateItemArrowNum()
    {
        vertInputDelayTime += Time.deltaTime;

        if (pointingItemID == 0)
        {
            for(int i = 1; i < 5; i++)
            {
                if (previewInventory[i] == 0)
                {
                    itemArrowNum = i - 1;
                    break;
                }
                else itemArrowNum = 0;
            }
        }

        //한번 푼 뒤에 다시 입력하거나 딜레이 시간 벗어났을때 입력 가능
        if (vertReleased || vertInputDelayTime >= 0.2f)
        {
            //화살표 움직임 위
            if (verticalInput >= 0.3f && itemArrowNum > 0)
            {
                --itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
            //화살표 움직임 아래
            else if (verticalInput <= -0.3f && itemArrowNum < 4 && previewInventory[itemArrowNum + 1] != 0)
            {
                ++itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
        }
    }

    //아이템 페이지 번호 업데이트
    void UpdatePageNum()
    {
        horInputDelayTime += Time.deltaTime;
        pageLength = inventoryCache.Count / 5 + 1;

        //한번 푼 뒤에 다시 입력하거나 딜레이 시간 벗어났을때 입력 가능
        if (horReleased || horInputDelayTime >= 0.2f)
        {
            //메뉴 페이지 진행 (->)
            if(horizontalInput >= 0.3f && currentPageNum < pageLength)
            {
                ++currentPageNum;
                horInputDelayTime = 0f;
                horReleased = false;
                GetPreviewInventory();
            }
            //메뉴 페이지 진행 (<-)
            else if(horizontalInput <= -0.3f && currentPageNum > 1)
            {
                --currentPageNum;
                horInputDelayTime = 0f;
                horReleased = false;
                GetPreviewInventory();
            }
        }
    }



    //표시 업데이트
    void UpdateDisplay()
    {
        //화살표 표시 업데이트
        for (int i = 0; i < 5; i++)
        {
            if (i == itemArrowNum)
            {
                ItemPointerArrows[i].enabled = true;
            }
            else
            {
                ItemPointerArrows[i].enabled = false;
            }
        }

        //아이템 페이지 표시 업데이트
        PageText.sprite = PageDigit[currentPageNum];
        InventorySizeText.sprite = InventorySizeDigit[pageLength];

        //아이템 선택 메뉴 이름 업데이트
        for (int i = 0; i < 5; i++)
        {
            MenuItemNameText[i].text = itemNames[i];
        }

        //아이템 설명문 이름 업데이트
        DescriptionItemNameText.text = itemNames[itemArrowNum];
        //아이템 설명문 업데이트
        ItemDescriptionText.text = itemDescription;
    }

    //캐시 인벤토리 얻기
    void GetInventoryCache(int selectingCharNum)
    {
        PlayerCharDB.GetComponent<PlayerCharacterData>().SetSearchCharacter(selectingCharNum);
        inventoryCache = PlayerCharDB.GetComponent<PlayerCharacterData>().character.Inventory;
        if(prevCharID != currentCharID)
        {
            itemArrowNum = 0;
            currentPageNum = 1;
        }
    }

    //표시 인벤토리 업데이트
    void GetPreviewInventory()
    {
        int searchLocationNum;
        previewInventory = new int[5];         //초기화

        for (int i = 0; i < 5; i++)
        {
            searchLocationNum = (currentPageNum - 1) * 5 + i;

            if (searchLocationNum < inventoryCache.Count)
            {
                previewInventory[i] = inventoryCache[(currentPageNum - 1) * 5 + i];
            }
        }
    }

    //이름, 설명문 얻기
    void GetNameAndDescription()
    {
        int languageVal = battleMaster.GetComponent<BattleMaster>().languageVal;

        //아이템 이름
        for (int i = 0; i < 5; i++)
        {
            if (previewInventory[i] != 0)
            {
                ItemDB.GetComponent<ItemData>().SetSearchItem(previewInventory[i]);
                itemNames[i] = ItemDB.GetComponent<ItemData>().item.ItemName[languageVal];
            }
            else
            {
                itemNames[i] = "";
            }
        }

        //아이템 설명문
        if(pointingItemID != 0)
        {
            ItemDB.GetComponent<ItemData>().SetSearchItem(pointingItemID);
            itemDescription = ItemDB.GetComponent<ItemData>().item.ItemDescription[languageVal];
        }
        else
        {
            itemDescription = "";
        }
    }

    //아이템 실행하기
    void SubmitItem()
    {
        if(Input.GetButtonDown("Submit") && pointingItemID != 0)
        {
            ItemDB.GetComponent<ItemData>().SetSearchItem(pointingItemID);
            int targetType = ItemDB.GetComponent<ItemData>().item.UseTarget;

            battleMaster.GetComponent<BattleMaster>().SubmitAction(currentCharID, 2, pointingItemID, targetType);

            Menu.GetComponent<MenuWaker>().SetChangeMenu(false, 2);
            ItemDB.GetComponent<ItemData>().UseItemEffect(pointingItemID);   //디버그용
        }
    }

    void Start()
    {
        itemArrowNum = 0;
        UpdateDisplay();
        StartCoroutine(LateStart(0.01f));
    }

    // Update is called once per frame
    void Update()
    {
        prevCharID = currentCharID;
        currentCharID = battleMaster.GetComponent<BattleMaster>().IDofSelectingChar;
        GetInventoryCache(battleMaster.GetComponent<BattleMaster>().IDofSelectingChar);

        GetDirectionalInput();
        UpdateItemArrowNum();
        UpdatePageNum();
        pointingItemID = previewInventory[itemArrowNum];
        GetNameAndDescription();
        UpdateDisplay();

        SubmitItem();
    }

    IEnumerator LateStart(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        GetInventoryCache(battleMaster.GetComponent<BattleMaster>().IDofSelectingChar);
        GetPreviewInventory();
    }
}
