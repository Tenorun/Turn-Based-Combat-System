using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour
{
    //TODO: ItemMenuPointer, ItemMenuPointer, ItemExecuter�� ����ȭ�� �ڵ带 �ϳ��� ���ս�Ű�� ��.
    public GameObject battleMaster;
    public GameObject ItemDB;
    public GameObject Menu;

    public Image[] ItemPointerArrows;                   //������ ȭ��ǥ ������Ʈ �̹���
    public int itemArrowNum = 0;                        //������ ȭ��ǥ ��ȣ
    public int currentPageNum = 1;                      //���� ������ ��ȣ
    public int pointingItemID;                          //���� ������ ID
    [SerializeField] private int pageLength;            //������ �ִ� ���� ��ȣ

    public Sprite[] PageDigit;                          //���� ������ ��ȣ ���� �̹���
    public Sprite[] InventorySizeDigit;                 //�κ��丮 ũ�� ��ȣ ���� �̹���

    public Image PageText;                               //���� ������ ��ȣ ������Ʈ �̹���
    public Image InventorySizeText;                      //�κ��丮 ũ�� ��ȣ ������Ʈ �̹���


    //���� �Է� �׸��
    private float vertInputDelayTime = 0f;              //���� �Է� ������ �ð� ��
    private float verticalInput = 0f;                   //���� �Է°�
    private float prevVerticalInput;                    //���� ���� �Է°�
    private bool vertReleased = true;                   //���� �ѹ� Ǯ��

    //���� �Է� �׸��
    private float horInputDelayTime = 0f;               //���� �Է� ������ �ð� ��
    private float horizontalInput = 0f;                 //���� �Է°�
    private float prevHorizontalInput;                  //���� ���� �Է°�
    private bool horReleased = true;                    //���� �ѹ� Ǯ��

    //�κ��丮 ĳ��
    public GameObject PlayerCharDB;
    public List<int> inventoryCache = new List<int>();
    public int[] previewInventory = new int[5];

    //������ ���� ����
    public TextMeshProUGUI[] MenuItemNameText;          //������ ���� �޴� ǥ�� �̸�
    public TextMeshProUGUI DescriptionItemNameText;     //������ ���� ǥ�� �̸�
    public TextMeshProUGUI ItemDescriptionText;         //������ ����

    private int currentCharID;                          //���� ĳ���� ID
    private int prevCharID;                             //���� ĳ���� ID
    public string[] itemNames = new string[5];          //������ �̸�
    public string itemDescription;                      //������ ����

    private void GetDirectionalInput()
    {
        //���� �Է� �ޱ�
        prevVerticalInput = verticalInput;
        verticalInput = Input.GetAxisRaw("Vertical");
        //���� �Է� ���� ���� �Է� �ν� ���� ���̰�, �� �Է°��� �ν� ���� ���̸� ���� �ѹ� Ǯ�ȴٰ� ��
        if (prevVerticalInput >= 0.3f || prevVerticalInput <= -0.3f)
        {
            if (verticalInput < 0.3f && verticalInput > -0.3f) vertReleased = true;
        }

        //���� �Է� �ޱ�
        prevHorizontalInput = horizontalInput;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //���� �Է� ���� ���� �Է� �ν� ���� ���̰�, �� �Է°��� �ν� ���� ���̸� ���� �ѹ� Ǯ�ȴٰ� ��
        if (prevHorizontalInput >= 0.3f || prevHorizontalInput <= -0.3f)
        {
            if (horizontalInput < 0.3f && horizontalInput > -0.3f) horReleased = true;
        }
    }

    //������ ȭ��ǥ ��ȣ ������Ʈ
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

        //�ѹ� Ǭ �ڿ� �ٽ� �Է��ϰų� ������ �ð� ������� �Է� ����
        if (vertReleased || vertInputDelayTime >= 0.2f)
        {
            //ȭ��ǥ ������ ��
            if (verticalInput >= 0.3f && itemArrowNum > 0)
            {
                --itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
            //ȭ��ǥ ������ �Ʒ�
            else if (verticalInput <= -0.3f && itemArrowNum < 4 && previewInventory[itemArrowNum + 1] != 0)
            {
                ++itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
        }
    }

    //������ ������ ��ȣ ������Ʈ
    void UpdatePageNum()
    {
        horInputDelayTime += Time.deltaTime;
        pageLength = inventoryCache.Count / 5 + 1;

        //�ѹ� Ǭ �ڿ� �ٽ� �Է��ϰų� ������ �ð� ������� �Է� ����
        if (horReleased || horInputDelayTime >= 0.2f)
        {
            //�޴� ������ ���� (->)
            if(horizontalInput >= 0.3f && currentPageNum < pageLength)
            {
                ++currentPageNum;
                horInputDelayTime = 0f;
                horReleased = false;
                GetPreviewInventory();
            }
            //�޴� ������ ���� (<-)
            else if(horizontalInput <= -0.3f && currentPageNum > 1)
            {
                --currentPageNum;
                horInputDelayTime = 0f;
                horReleased = false;
                GetPreviewInventory();
            }
        }
    }



    //ǥ�� ������Ʈ
    void UpdateDisplay()
    {
        //ȭ��ǥ ǥ�� ������Ʈ
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

        //������ ������ ǥ�� ������Ʈ
        PageText.sprite = PageDigit[currentPageNum];
        InventorySizeText.sprite = InventorySizeDigit[pageLength];

        //������ ���� �޴� �̸� ������Ʈ
        for (int i = 0; i < 5; i++)
        {
            MenuItemNameText[i].text = itemNames[i];
        }

        //������ ���� �̸� ������Ʈ
        DescriptionItemNameText.text = itemNames[itemArrowNum];
        //������ ���� ������Ʈ
        ItemDescriptionText.text = itemDescription;
    }

    //ĳ�� �κ��丮 ���
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

    //ǥ�� �κ��丮 ������Ʈ
    void GetPreviewInventory()
    {
        int searchLocationNum;
        previewInventory = new int[5];         //�ʱ�ȭ

        for (int i = 0; i < 5; i++)
        {
            searchLocationNum = (currentPageNum - 1) * 5 + i;

            if (searchLocationNum < inventoryCache.Count)
            {
                previewInventory[i] = inventoryCache[(currentPageNum - 1) * 5 + i];
            }
        }
    }

    //�̸�, ���� ���
    void GetNameAndDescription()
    {
        int languageVal = battleMaster.GetComponent<BattleMaster>().languageVal;

        //������ �̸�
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

        //������ ����
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

    //������ �����ϱ�
    void SubmitItem()
    {
        if(Input.GetButtonDown("Submit") && pointingItemID != 0)
        {
            ItemDB.GetComponent<ItemData>().SetSearchItem(pointingItemID);
            int targetType = ItemDB.GetComponent<ItemData>().item.UseTarget;

            battleMaster.GetComponent<BattleMaster>().SubmitAction(currentCharID, 2, pointingItemID, targetType);

            Menu.GetComponent<MenuWaker>().SetChangeMenu(false, 2);
            ItemDB.GetComponent<ItemData>().UseItemEffect(pointingItemID);   //����׿�
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
