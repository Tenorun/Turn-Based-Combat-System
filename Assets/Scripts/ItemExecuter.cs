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
            //TODO: �÷��̾� �κ��丮���� ȭ��ǥ�� �ö����ִ� ������ ���� ��ũ��Ʈ �ۼ�
        }
    }
}
