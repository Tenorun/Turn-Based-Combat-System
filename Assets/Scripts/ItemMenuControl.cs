using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuControl : MonoBehaviour
{
    //TODO: ItemMenuPointer, ItemMenuPointer, ItemExecuter�� ����ȭ�� �ڵ带 �ϳ��� ���ս�Ű�� ��.
    public GameObject BattleMaster;

    public Image[] ItemPointerArrows;                   //������ ȭ��ǥ ������Ʈ �̹���
    public int itemArrowNum = 0;                        //������ ȭ��ǥ ��ȣ
    public int currentPageNum = 1;                      //���� ������ ��ȣ
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

    public GameObject PlayerCharDB;
    private List<int> inventoryCache = new List<int>();

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

        //�ѹ� Ǭ �ڿ� �ٽ� �Է��ϰų� ������ �ð� ������� �Է� ����
        if(vertReleased || vertInputDelayTime >= 0.2f)
        {
            //ȭ��ǥ ������ ��
            if (verticalInput >= 0.3f && itemArrowNum > 0)
            {
                --itemArrowNum;
                vertInputDelayTime = 0f;
                vertReleased = false;
            }
            //ȭ��ǥ ������ �Ʒ�
            else if (verticalInput <= -0.3f && itemArrowNum < 4)
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
            }
            //�޴� ������ ���� (<-)
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
        //ȭ��ǥ ǥ�� ������Ʈ
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

        //������ ������ ǥ�� ������Ʈ
        PageText.sprite = PageDigit[currentPageNum];
        InventorySizeText.sprite = InventorySizeDigit[pageLength];
    }

    //ĳ�� �κ��丮 ���
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
