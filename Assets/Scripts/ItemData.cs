using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemData : MonoBehaviour
{
    public int languageVal = 0;
    Item item;
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
        //아이템 데이터 베이스에 추가
        //ItemDatabase.AddItem(new Item((아이템 고유번호), new string[] { "(아이템 한국어 이름)", "(아이템 영어 이름)"}, new string[] { "(한국어 설명문)", "(영어 설명문)" }, new string[] { "(한국어 사용 특수 대사)", "(영어 사용 특수 대사)" }, (0~6 사용대상 값)));
        //ItemDatabase.AddItem(new Item(, new string[] { "", "" }, new string[] { "", "" }, new string[] { "", "" }, ));
        ItemDatabase.AddItem(new Item(1, new string[] { "햄버거", "Hamburger" }, new string[] { "(HP를 100회복)\n100% 유기농 순쇠패티", "(Restores 100 HP)\n100% Organic beef patty" }, new string[] { "", "" }, 1));
        ItemDatabase.AddItem(new Item(2, new string[] { "딸기두부", "Strawberry Tofu" }, new string[] { "(HP를 50회복)\n이 도데체 무슨 해괴한 음식이란 말인가...", "Restores 50 HP\n WHAT THE HELL IS THIS KIND OF FOOD" }, new string[] { "", "" }, 1));
        ItemDatabase.AddItem(new Item(3, new string[] { "팔랑거리는 종이", "Floppy paper" }, new string[] { "종이다.\n팔랑거린다.", "It's paper.\nfloppy" }, new string[] { "", "" }, 3));
        ItemDatabase.AddItem(new Item(4, new string[] { "감기약(?)", "Pill for cold(?)" }, new string[] { "감기와 관련된 무언가다.\n아마도...", "Pill does something with cold.\nI guess..." }, new string[] { "", "" }, 4));
        ItemDatabase.AddItem(new Item(5, new string[] { "행복의 흰 가루", "White powder of Joy" }, new string[] { "흐헤헤...히히...헤헤...오호호...", "Hee...Hee...hehe..." }, new string[] { "", "" }, 1));

        //Item item;
        item = ItemDatabase.GetItem(2);   //여기에 찾으려는 아이템의 고유번호를 넣는다.
        Debug.Log(item.ItemName[0]);  //"딸기두부"   ItemName의 배열 번호는 언어 값

    }


    //아이템 효과
    public void UseItemEffect(int itemId)
    {
        item = ItemDatabase.GetItem(itemId);

        switch (itemId)
        {
            case 1:
                Debug.Log($"{item.ItemName[languageVal]}을(를) 사용함");
                break;
            case 2:
                Debug.Log($"{item.ItemName[languageVal]}을(를) 사용함");
                break;
            case 3:
                Debug.Log($"{item.ItemName[languageVal]}을(를) 사용함");
                break;
            case 4:
                Debug.Log($"{item.ItemName[languageVal]}을(를) 사용함");
                break;
            case 5:
                Debug.Log($"{item.ItemName[languageVal]}을(를) 사용함");
                break;
            default:
                Debug.LogError($"아이템 고유번호 {itemId}에 해당하는 아이템은 존재하지 않습니다.");
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
