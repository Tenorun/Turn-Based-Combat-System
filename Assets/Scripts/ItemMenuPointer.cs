using UnityEngine;
using UnityEngine.UI;

public class ItemMenuPointer : MonoBehaviour
{
    public Image[] ItemPointerArrows;

    public int itemPointerNum = 0;

    private float InputDelayTime = 0f;
    private float VerticalInput;
    private float prevVerticalInput;
    private bool onceReleased = true;

    private void GetDirectionalInput()
    {
        prevVerticalInput = VerticalInput;
        VerticalInput = Input.GetAxisRaw("Vertical");
    } 

    void UpdateItemPointerNum()
    {
        if(VerticalInput >= 0.3f)
        {
            if (itemPointerNum > 0) --itemPointerNum;
            InputDelayTime = 0f;
            onceReleased = false;
        }
        else if(VerticalInput <= -0.3f)
        {
            if (itemPointerNum < 4) ++itemPointerNum;
            InputDelayTime = 0f;
            onceReleased = false;
        }
    }

    void UpdateItemPointerDisplay()
    {
        for(int i = 0; i <= 4; i++)
        {
            if(i == itemPointerNum)
            {
                ItemPointerArrows[i].enabled = true;
            }
            else
            {
                ItemPointerArrows[i].enabled = false;
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        itemPointerNum = 0;
        UpdateItemPointerDisplay();
    }

    // Update is called once per frame
    void Update()
    {
        InputDelayTime += Time.deltaTime;
        GetDirectionalInput();
        
        if(prevVerticalInput >= 0.3f || prevVerticalInput <= -0.3f)
        {
            if(VerticalInput < 0.3f && VerticalInput > -0.3f) onceReleased = true;
        }

        if(onceReleased)
        {
            UpdateItemPointerNum();
        }
        else
        {
            if (InputDelayTime >= 0.2f)
            {
                UpdateItemPointerNum();
            }
        }

        UpdateItemPointerDisplay();
    }
}
