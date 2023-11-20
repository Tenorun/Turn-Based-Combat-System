using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    GameObject ItemMenuFrame;

    public void DisplayItemMenu(bool displayStatus)
    {
        ItemMenuFrame.SetActive(displayStatus);
    }
    void Start()
    {
        ItemMenuFrame = GameObject.Find("Item Menu");
        DisplayItemMenu(false);
    }

    // Update is called once per frame
    void Update()
    {

        //������ â�� ���õǾ��ִ� ���·� ȭ���� Ȯ�밡 �Ǿ������� ǥ��
        if (this.GetComponent<ChangeMenuSize>().isExpanded
            && !this.GetComponent<ChangeMenuSize>().changeSizeTrigger
            && this.GetComponent<ActionButton>().selectedBtnNum == 2)
        {
            DisplayItemMenu(true);
        }
    }
}
