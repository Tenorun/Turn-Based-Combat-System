using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemMenuPage : MonoBehaviour
{
    public GameObject BattleMaster;
    public GameObject ItemPointerArea;

    // Start is called before the first frame update
    public Sprite[] PageDigit;
    public Sprite[] InventorySizeDigit;

    public Image PageNum;
    public Image InventorySizeNum;

    public Image[] Arrow;

    public int currentPage = 1;
    private int maxPage;


    //ют╥б
    private float inputDelayTime = 0f;
    private float horizontalInput = 0f;
    private float prevHorizontalInput;
    private bool onceReleased = true;


    private void GetDirectoinalInput()
    {
        prevHorizontalInput = horizontalInput;
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }

    private void UpdateNum()
    {
        //maxPage = BattleMaster.GetComponent<BattleMaster>().GetPageSize();
        maxPage = 5;
        //testcode
        

        if(horizontalInput < -0.3f && currentPage != 1)
        {
            --currentPage;
            inputDelayTime = 0f;
            onceReleased = false;

            ItemPointerArea.GetComponent<ItemMenuPointer>().itemPointerNum = 0;
        }
        else if(horizontalInput > 0.3f && currentPage != maxPage)
        {
            ++currentPage;
            inputDelayTime = 0f;
            onceReleased = false;

            ItemPointerArea.GetComponent<ItemMenuPointer>().itemPointerNum = 0;
        }
    }

    private void UpdateDisplay()
    {
        PageNum.sprite = PageDigit[currentPage];
        InventorySizeNum.sprite = InventorySizeDigit[maxPage];
    }

    void Update()
    {
        inputDelayTime += Time.deltaTime;
        GetDirectoinalInput();

        if (prevHorizontalInput >= 0.3f || prevHorizontalInput <= -0.3f)
        {
            if (horizontalInput < 0.3f && horizontalInput > -0.3f) onceReleased = true;
        }

        if (onceReleased)
        {
            UpdateNum();
            UpdateDisplay();
        }
        else
        {
            if (inputDelayTime >= 0.2f)
            {
                UpdateNum();
                UpdateDisplay();
            }
        }
    }
}
