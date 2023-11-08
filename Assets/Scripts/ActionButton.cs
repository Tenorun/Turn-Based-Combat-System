using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public Image[] actionButtons;
    public Color[] highlightedColors;
    public Color[] unselectedColors;

    public Color disabledColor;

    [SerializeField] private int selectedBtnNum = 0;
    [SerializeField] private float HorizontalInput;
    [SerializeField] private float VerticalInput;
    //0: 공격 버튼
    //1: 스킬 버튼
    //2: 아이템 버튼
    //3: 도망 버튼

    /*
    [공  격][스  킬]
    [아이템][도  망]
    */

    private void GetInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MoveCursor()//커서 이동 입력 받기
    {
        switch (selectedBtnNum)
        {
            case 0://공격
                if (HorizontalInput >= 0.3f)
                {
                    selectedBtnNum = 1;
                }
                else if (VerticalInput <= -0.3f)
                {
                    selectedBtnNum = 2;
                }
                break;
            case 1://스킬
                if (HorizontalInput <= -0.3f)
                {
                    selectedBtnNum = 0;
                }
                else if (VerticalInput <= -0.3f)
                {
                    selectedBtnNum = 3;
                }
                break;
            case 2://아이템
                if (HorizontalInput >= 0.3f)
                {
                    selectedBtnNum = 3;
                }
                else if (VerticalInput >= 0.3f)
                {
                    selectedBtnNum = 0;
                }
                break;
            case 3://도망
                if (HorizontalInput <= -0.3f)
                {
                    selectedBtnNum = 2;
                }
                else if (VerticalInput >= 0.3f)
                {
                    selectedBtnNum = 1;
                }
                break;
        }
    }

    private void UpdateBtnHighlight()//버튼 하이라이트 바꾸기
    {
        for(int i = 0; i <= 3; i++)
        {
            if(i == selectedBtnNum)
            {
                actionButtons[i].color = highlightedColors[i];
            }
            else
            {
                actionButtons[i].color = unselectedColors[i];
            }
        }
    }

    void Start()
    {
        UpdateBtnHighlight();
    }

    void FixedUpdate()
    {
        GetInput();
        MoveCursor();
        UpdateBtnHighlight();
    }
}
