using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour
{
    //TODO: ItemMenuPointer, ItemMenuPointer, ItemExecuter로 파편화된 코드를 하나로 통합시키는 것.
    public GameObject BattleMaster;

    public Image[] ItemPointerArrows;                   //아이템 화살표 오브젝트 이미지
    public int itemArrowNum = 0;                        //아이템 화살표 번호
    public int currentPageNum = 1;                      //현재 페이지 번호
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

    public GameObject PlayerCharDB;
    private List<int> inventoryCache = new List<int>();

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

        //한번 푼 뒤에 다시 입력하거나 딜레이 시간 벗어났을때 입력 가능
        if(vertReleased || vertInputDelayTime >= 0.2f)
        {
            //화살표 움직임 위
            if (verticalInput >= 0.3f && itemArrowNum > 0)
            {
                --itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
            //화살표 움직임 아래
            else if (verticalInput <= -0.3f && itemArrowNum < 4)
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
            }
            //메뉴 페이지 진행 (<-)
            else if(horizontalInput <= -0.3f && currentPageNum > 1)
            {
                --currentPageNum;
                horInputDelayTime = 0f;
                horReleased = false;
            }
        }
    }


    void UpdateDisplay()
    {
        //화살표 표시 업데이트
        for (int i = 0; i <= 4; i++)
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
    }

    //캐시 인벤토리 얻기
    void GetInventoryCache(int selectingCharNum)
    {
        PlayerCharDB.GetComponent<PlayerCharacterData>().SetSearchCharacter(selectingCharNum);
        inventoryCache = PlayerCharDB.GetComponent<PlayerCharacterData>().character.Inventory;
    }

    void Start()
    {
        itemArrowNum = 0;
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        GetInventoryCache(BattleMaster.GetComponent<BattleMaster>().charSelecting);


        GetDirectionalInput();
        UpdateItemArrowNum();
        UpdatePageNum();
        UpdateDisplay();
    }
}
