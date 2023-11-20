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

        //아이템 창이 선택되어있는 상태로 화면이 확대가 되어있으면 표시
        if (this.GetComponent<ChangeMenuSize>().isExpanded
            && !this.GetComponent<ChangeMenuSize>().changeSizeTrigger
            && this.GetComponent<ActionButton>().selectedBtnNum == 2)
        {
            DisplayItemMenu(true);
        }
    }
}
