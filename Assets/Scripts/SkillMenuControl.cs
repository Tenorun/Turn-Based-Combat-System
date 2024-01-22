using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillMenuControl : MonoBehaviour
{
    public Image[] skillBtnBaseFrame;
    public Image[] skillIconFrame;
    public Image[] skillIcon;
    public GameObject[] pointerSquare;
    public TextMeshProUGUI[] skillBtnNames;

    public int currentSelectNum = 0;


    private float verticalInput = 0f;
    private float horizontalInput = 0f;
    void GetDirectionalInput()
    {
        verticalInput = Input.GetAxisRaw("Vertical");
        horizontalInput = Input.GetAxisRaw("Horizontal");
    }


    //0 1
    //2 3
    void UpdateCurrentSelectionNum()
    {
        switch(currentSelectNum)
        {
            case 0:
                if (horizontalInput >= 0.3f) currentSelectNum = 1;
                else if (verticalInput <= -0.3f) currentSelectNum = 2;
                break;
            case 1:
                if (horizontalInput <= -0.3f) currentSelectNum = 0;
                else if (verticalInput <= -0.3f) currentSelectNum = 3;
                break;
            case 2:
                if (horizontalInput >= 0.3f) currentSelectNum = 3;
                else if (verticalInput >= 0.3f) currentSelectNum = 0;
                break;
            case 3:
                if (horizontalInput <= -0.3f) currentSelectNum = 2;
                else if (verticalInput >= 0.3f) currentSelectNum = 1;
                break;

        }
    }

    void UpdateDisplay()
    {
        for(int i =0; i<4; i++)
        {
            if(i == currentSelectNum)
            {
                skillBtnBaseFrame[i].color = Color.black;
                skillIconFrame[i].color = Color.white;
                skillIcon[i].color = Color.white;
                skillBtnNames[i].color = Color.white;
                pointerSquare[i].SetActive(true);
            }
            else
            {
                skillBtnBaseFrame[i].color = Color.white;
                skillIconFrame[i].color = Color.black;
                skillIcon[i].color = Color.black;
                skillBtnNames[i].color = Color.black;
                pointerSquare[i].SetActive(false);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        currentSelectNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionalInput();
        UpdateCurrentSelectionNum();

        UpdateDisplay();
    }
}
