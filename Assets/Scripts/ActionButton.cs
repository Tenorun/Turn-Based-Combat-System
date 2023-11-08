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
    //0: ���� ��ư
    //1: ��ų ��ư
    //2: ������ ��ư
    //3: ���� ��ư

    /*
    [��  ��][��  ų]
    [������][��  ��]
    */

    private void GetInput()
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MoveCursor()//Ŀ�� �̵� �Է� �ޱ�
    {
        switch (selectedBtnNum)
        {
            case 0://����
                if (HorizontalInput >= 0.3f)
                {
                    selectedBtnNum = 1;
                }
                else if (VerticalInput <= -0.3f)
                {
                    selectedBtnNum = 2;
                }
                break;
            case 1://��ų
                if (HorizontalInput <= -0.3f)
                {
                    selectedBtnNum = 0;
                }
                else if (VerticalInput <= -0.3f)
                {
                    selectedBtnNum = 3;
                }
                break;
            case 2://������
                if (HorizontalInput >= 0.3f)
                {
                    selectedBtnNum = 3;
                }
                else if (VerticalInput >= 0.3f)
                {
                    selectedBtnNum = 0;
                }
                break;
            case 3://����
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

    private void UpdateBtnHighlight()//��ư ���̶���Ʈ �ٲٱ�
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
