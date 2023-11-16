using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMenu : MonoBehaviour
{
    GameObject ItemMenuFrame;
    bool ItemMenuStatus = false;

    void Start()
    {
        ItemMenuFrame = GameObject.Find("Item Menu");
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<ChangeMenuSize>().isExpanded && this.GetComponent<ActionButton>().selectedBtnNum == 2)
        {
            ItemMenuStatus = true;
        }
        else
        {
            ItemMenuStatus = false;
        }

        ItemMenuFrame.SetActive(ItemMenuStatus);
    }
}
