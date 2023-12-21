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


    //�׽�Ʈ�� �κ��丮
    int[,] testReadInventory = new int[,] { { 1, 2, 3, 4, 5 }, { 1, 1, 1, 1, 1 }, { 2, 2, 2, 2, 2 }, { 3, 3, 3, 3, 3 }, { 5, 5, 5, 5, 5 } };

    // Update is called once per frame
    void Update()
    {
        itemPointerNum = Pointer.GetComponent<ItemMenuPointer>().itemPointerNum;
        currentPage = Page.GetComponent<ItemMenuPage>().currentPage;

        if (Input.GetButtonDown("Submit"))
        {
            ItemMaster.GetComponent<ItemData>().UseItemEffect(testReadInventory[currentPage - 1, itemPointerNum]);
            //TODO: �÷��̾� �κ��丮���� ȭ��ǥ�� �ö����ִ� ������ ���� ��ũ��Ʈ �ۼ�
        }
    }
}
