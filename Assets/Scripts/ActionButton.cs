using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public GameObject BattleMaster;                         //���� ������ ������Ʈ

    public bool isActBtnLocked = false;                     //��ư ��� ����
    public bool isActLocked = false;                        //���� ��� ����(���� ��� ��ų�� ���õǰ�, ��ų Ÿ���� �����Ͽ�, ��ų ������ �����ϴ� �ܰ�� ������ true�� �ȴ�.)

    public Image[] actionButtons;                           //��ư��
    public Color[] highlightedColors;                       //���õ� ��ư�� �����
    public Color[] unselectedColors;                        //���þȵ� ��ư�� �����

    public Color disabledColor;                             //��Ȱ��ȭ�� ��ư ����

    [SerializeField] private int selectedBtnNum = 0;        //���õ� ��ư�� ��ȣ(�ڼ��Ѱ� �Ʒ��� ����)
    [SerializeField] private float HorizontalInput;         //���� �Է�
    [SerializeField] private float VerticalInput;           //���� �Է�
    //0: ���� ��ư
    //1: ��ų ��ư
    //2: ������ ��ư
    //3: ���� ��ư

    /*
    [��  ��][��  ų]
    [������][��  ��]
    */

    private void GetDirectionalInput()                      //���� �Է� �ޱ�
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MoveCursor()                               //Ŀ�� �̵��ϱ�
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

    private void UpdateBtnHighlight()                       //��ư ���̶���Ʈ �ٲٱ�
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

    void Update()
    {
        if (!isActLocked)                                   //������ ����� �ʾ�����
        {
            if (!isActBtnLocked)                            //��ư ������ ����� �ʾ�����
            {
                GetDirectionalInput();
                MoveCursor();

                if (Input.GetButtonDown("Submit"))          //Ȯ�� �Է�
                {
                    isActBtnLocked = true;
                    BattleMaster.GetComponent<BattleMaster>().GetActionMenuInput(selectedBtnNum);
                }
            }
            else                                            //��ư ������ �������
            {
                if (Input.GetButtonDown("Cancel"))          //��� �Է�
                {
                    isActBtnLocked = false;
                    BattleMaster.GetComponent<BattleMaster>().GetActionMenuInput(4);
                }
            }
        }
        UpdateBtnHighlight();
    }
}
