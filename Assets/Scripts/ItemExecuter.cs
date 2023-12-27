using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExecuter : MonoBehaviour
{
    public GameObject ItemMaster;
    public GameObject Pointer;
    public GameObject Page;

    int currentPage;
    int itemPointerNum;

    int selectingItemNum;

    //테스트용 인벤토리
    int[] testReadInventory = new int[] {1, 2, 3, 4, 5, 1, 1, 1, 1, 1, 2, 2, 2, 2, 2, 3, 3, 3, 3, 3, 5, 5, 5, 5, 5 };

    // Update is called once per frame
    void Update()
    {
        itemPointerNum = Pointer.GetComponent<ItemMenuPointer>().itemPointerNum;
        currentPage = Page.GetComponent<ItemMenuPage>().currentPage;
        selectingItemNum = testReadInventory[(currentPage - 1) * 5 + itemPointerNum];

        if (Input.GetButtonDown("Submit") && selectingItemNum != 0)
        {
            ItemMaster.GetComponent<ItemData>().UseItemEffect(selectingItemNum);
            //TODO: 플레이어 인벤토리에서 화살표가 올라가져있는 아이템 실행 스크립트 작성
        }
    }
}
