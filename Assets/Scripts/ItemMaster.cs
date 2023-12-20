using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMaster : MonoBehaviour
{
    // 아이템 데이터베이스 클래스
    public class ItemDatabase
    {
        // 아이템 목록
        public static List<Item> Items = new List<Item>();

        // 아이템을 추가하는 함수
        public static void AddItem(Item item)
        {
            Items.Add(item);
        }

        // 아이템을 가져오는 함수
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

    // 아이템 클래스
    public class Item
    {
        // 아이템 고유번호
        public int ItemId { get; set; }

        // 아이템 이름
        public string[] ItemName { get; set; }

        // 아이템 설명문
        public string[] ItemDescription { get; set; }

        // 아이템 사용 메시지(비워두면 "{사용한 캐릭터 이름}(은)는 {아이템 이름}(을)를 사용했다." 로 출력됨)
        public string[] ItemUseMessage { get; set; }

        // 사용 대상 값
        /*사용 대상 번호
            0: 전체중 1명
            1: 아군중 1명
            2: 적중 1명
            3: 사용하는 자신에게만
            4: 전체
            5: 아군 전체
            6: 적 전체
        */
        public int UseTarget { get; set; }

        // 생성자
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
        ItemDatabase.AddItem(new Item(1, new string[] { "회복약", "Healing Potion" }, new string[] { "체력을 회복합니다.", "Restores HP." }, new string[] { "", "" }, 2));
        ItemDatabase.AddItem(new Item(2, new string[] { "딸기 두부", "Strawberry Tofu" }, new string[] { "HP 50 회복\n 이 도데체 무슨 해괴한 음식이란 말인가...", "Restores 50 HP\n WHAT THE HELL IS THIS KIND OF FOOD" }, new string[] {"",""}, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
