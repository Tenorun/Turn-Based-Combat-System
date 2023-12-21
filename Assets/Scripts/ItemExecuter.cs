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


    //테스트용 인벤토리
    int[,] testReadInventory = new int[,] { { 1, 2, 3, 4, 5 }, { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 5, 5, 5, 5, 5 } };

    // Update is called once per frame
    void Update()
    {
        itemPointerNum = Pointer.GetComponent<ItemMenuPointer>().itemPointerNum;
        currentPage = Page.GetComponent<ItemMenuPage>().currentPage;

        if (Input.GetButtonDown("Submit"))
        {
            ItemMaster.GetComponent<ItemData>().UseItemEffect(testReadInventory[currentPage - 1, itemPointerNum]);
            //TODO: 플레이어 인벤토리에서 화살표가 올라가져있는 아이템 실행 스크립트 작성
        }
    }
}
