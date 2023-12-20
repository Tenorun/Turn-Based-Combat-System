using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
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
        ItemDatabase.AddItem(new Item(1, new string[] { "ȸ����", "Healing Potion" }, new string[] { "ü���� ȸ���մϴ�.", "Restores HP." }, new string[] { "", "" }, 2));
        ItemDatabase.AddItem(new Item(2, new string[] { "���� �κ�", "Strawberry Tofu" }, new string[] { "HP 50 ȸ��\n �� ����ü ���� �ر��� �����̶� ���ΰ�...", "Restores 50 HP\n WHAT THE HELL IS THIS KIND OF FOOD" }, new string[] {"",""}, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
