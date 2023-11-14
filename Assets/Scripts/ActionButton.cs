using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActionButton : MonoBehaviour
{
    public GameObject BattleMaster;                         //전투 관리자 오브젝트

    public bool isActBtnLocked = false;                     //버튼 잠금 여부
    public bool isActLocked = false;                        //조작 잠금 여부(예를 들어 스킬이 선택되고, 스킬 타입을 선택하여, 스킬 종류를 선택하는 단계로 왔을때 true로 된다.)

    public Image[] actionButtons;                           //버튼들
    public Color[] highlightedColors;                       //선택된 버튼의 색상들
    public Color[] unselectedColors;                        //선택안된 버튼의 색상들

    public Color disabledColor;                             //비활성화된 버튼 색상

    [SerializeField] private int selectedBtnNum = 0;        //선택된 버튼의 번호(자세한건 아래를 참조)
    [SerializeField] private float HorizontalInput;         //가로 입력
    [SerializeField] private float VerticalInput;           //세로 입력
    //0: 공격 버튼
    //1: 스킬 버튼
    //2: 아이템 버튼
    //3: 도망 버튼

    /*
    [공  격][스  킬]
    [아이템][도  망]
    */

    private void GetDirectionalInput()                      //방향 입력 받기
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }

    private void MoveCursor()                               //커서 이동하기
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

    private void UpdateBtnHighlight()                       //버튼 하이라이트 바꾸기
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
        if (!isActLocked)                                   //조작이 잠기지 않았을때
        {
            if (!isActBtnLocked)                            //버튼 조작이 잠기지 않았을때
            {
                GetDirectionalInput();
                MoveCursor();

                if (Input.GetButtonDown("Submit"))          //확인 입력
                {
                    isActBtnLocked = true;
                    BattleMaster.GetComponent<BattleMaster>().GetActionMenuInput(selectedBtnNum);
                }
            }
            else                                            //버튼 조작이 잠겼을때
            {
                if (Input.GetButtonDown("Cancel"))          //취소 입력
                {
                    isActBtnLocked = false;
                    BattleMaster.GetComponent<BattleMaster>().GetActionMenuInput(4);
                }
            }
        }
        UpdateBtnHighlight();
    }
}
