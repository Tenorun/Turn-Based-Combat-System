using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int languageVal = 0;
    Item item;
    // ������ �����ͺ��̽� Ŭ����
    public class ItemDatabase
    {
        // ������ ���
        public static List<Item> Items = new List<Item>();

        // �������� �߰��ϴ� �Լ�
        public static void AddItem(Item item)
        {
            Items.Add(item);
        }

        // �������� �������� �Լ�
        public static Item GetItem(int itemId)
        {
            foreach (Item item in Items)
            {
                if (item.ItemId == itemId)
                {
                    return item;
                }
            }

            return null;
        }
    }

    // ������ Ŭ����
    public class Item
    {
        // ������ ������ȣ
        public int ItemId { get; set; }

        // ������ �̸�
        public string[] ItemName { get; set; }

        // ������ ����
        public string[] ItemDescription { get; set; }

        // ������ ��� �޽���(����θ� "{����� ĳ���� �̸�}(��)�� {������ �̸�}(��)�� ����ߴ�." �� ��µ�)
        public string[] ItemUseMessage { get; set; }

        // ��� ��� ��
        /*��� ��� ��ȣ
            0: ��ü�� 1��
            1: �Ʊ��� 1��
            2: ���� 1��
            3: ����ϴ� �ڽſ��Ը�
            4: ��ü
            5: �Ʊ� ��ü
            6: �� ��ü
        */
        public int UseTarget { get; set; }

        // ������
        public Item(int itemId, string[] itemName, string[] itemDescription, string[] itemUseMessage, int useTarget)
        {
            this.ItemId = itemId;
            this.ItemName = itemName;
            this.ItemDescription = itemDescription;
            this.ItemUseMessage = itemUseMessage;
            this.UseTarget = useTarget;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        //������ ������ ���̽��� �߰�
        //ItemDatabase.AddItem(new Item((������ ������ȣ), new string[] { "(������ �ѱ��� �̸�)", "(������ ���� �̸�)"}, new string[] { "(�ѱ��� ����)", "(���� ����)" }, new string[] { "(�ѱ��� ��� Ư�� ���)", "(���� ��� Ư�� ���)" }, (0~6 ����� ��)));
        //ItemDatabase.AddItem(new Item(, new string[] { "", "" }, new string[] { "", "" }, new string[] { "", "" }, ));
        ItemDatabase.AddItem(new Item(1, new string[] { "�ܹ���", "Hamburger" }, new string[] { "(HP�� 100ȸ��)\n100% ����� ������Ƽ", "(Restores 100 HP)\n100% Organic beef patty" }, new string[] { "", "" }, 1));
        ItemDatabase.AddItem(new Item(2, new string[] { "����κ�", "Strawberry Tofu" }, new string[] { "(HP�� 50ȸ��)\n�� ����ü ���� �ر��� �����̶� ���ΰ�...", "Restores 50 HP\n WHAT THE HELL IS THIS KIND OF FOOD" }, new string[] { "", "" }, 1));
        ItemDatabase.AddItem(new Item(3, new string[] { "�ȶ��Ÿ��� ����", "Floppy paper" }, new string[] { "���̴�.\n�ȶ��Ÿ���.", "It's paper.\nfloppy" }, new string[] { "", "" }, 3));
        ItemDatabase.AddItem(new Item(4, new string[] { "�����(?)", "Pill for cold(?)" }, new string[] { "����� ���õ� ���𰡴�.\n�Ƹ���...", "Pill does something with cold.\nI guess..." }, new string[] { "", "" }, 4));
        ItemDatabase.AddItem(new Item(5, new string[] { "�ູ�� �� ����", "White powder of Joy" }, new string[] { "������...����...����...��ȣȣ...", "Hee...Hee...hehe..." }, new string[] { "", "" }, 1));

        //Item item;
        item = ItemDatabase.GetItem(2);   //���⿡ ã������ �������� ������ȣ�� �ִ´�.
        Debug.Log(item.ItemName[0]);  //"����κ�"   ItemName�� �迭 ��ȣ�� ��� ��

    }


    //������ ȿ��
    public void UseItemEffect(int itemId)
    {
        item = ItemDatabase.GetItem(itemId);

        switch (itemId)
        {
            case 1:
                Debug.Log($"{item.ItemName[languageVal]}��(��) �����");
                break;
            case 2:
                Debug.Log($"{item.ItemName[languageVal]}��(��) �����");
                break;
            case 3:
                Debug.Log($"{item.ItemName[languageVal]}��(��) �����");
                break;
            case 4:
                Debug.Log($"{item.ItemName[languageVal]}��(��) �����");
                break;
            case 5:
                Debug.Log($"{item.ItemName[languageVal]}��(��) �����");
                break;
            default:
                Debug.LogError($"������ ������ȣ {itemId}�� �ش��ϴ� �������� �������� �ʽ��ϴ�.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
