using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemExecuter : MonoBehaviour
{
    public GameObject ItemMaster;

    int currentPage;
    int itemPointerNum;


    // Update is called once per frame
    void Update()
    {
        itemPointerNum = ItemMaster.GetComponent<ItemMenuPointer>().itemPointerNum;
        currentPage = ItemMaster.GetComponent<ItemMenuPage>().currentPage;

        if (Input.GetButtonDown("Submit"))
        {
            //TODO: 플레이어 인벤토리에서 화살표가 올라가져있는 아이템 실행 스크립트 작성
        }
    }
}
