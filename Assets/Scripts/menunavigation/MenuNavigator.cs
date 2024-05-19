using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.UI;


public class MenuNavigator : MonoBehaviour
{
    public GameObject BattleMaster;                         //전투 관리자 오브젝트

    //메뉴 옵션
    public enum MenuOptions
    { 
        MainMenu,
        AttackMenu,
        SkillMenu,
        ItemMenu,
        EtcMenu,
        EnemySelectionMenu,
        AllySelectionMenu,
        Locked
    }
    MenuOptions curMod = new MenuOptions();                     //현재 메뉴 모드

    public Image[] mainActBtns = new Image[4];              //버튼들
    public Color[] selectedMainBtnColor = new Color[4];     //선택된 버튼의 색상들
    public Color[] unselectedMainBtnColors = new Color[4];  //선택안된 버튼의 색상들


    //메인 버튼 옵션
    public enum MainBtnOptions
    {
        Attack,
        Skill,
        Item,
        Etc
    }
    public MainBtnOptions curMainBtnSelect;                 //현재 선택된 메인 버튼

    /*
    [공  격][스  킬]
    [아이템][도  망]
    */

    [SerializeField] private float HorizontalInput;         //가로 입력
    [SerializeField] private float VerticalInput;           //세로 입력

    private void GetDirectionalInput()                      //방향 입력 받기
    {
        HorizontalInput = Input.GetAxisRaw("Horizontal");
        VerticalInput = Input.GetAxisRaw("Vertical");
    }


    //메인 메뉴 커서 이동
    private void MoveMainMenuCursor()                               //커서 이동하기
    {
        switch (curMainBtnSelect)
        {
            case MainBtnOptions.Attack:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Skill;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Item;

                break;
            case MainBtnOptions.Skill:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Attack;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Etc;

                break;
            case MainBtnOptions.Item:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Etc;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Attack;

                break;
            case MainBtnOptions.Etc:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtnOptions.Item;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtnOptions.Skill;

                break;
        }
    }

    private void UpdateMainBtnHighlight()                       //메인 버튼 하이라이트 바꾸기
    {
        int selectedMainBtnNum = 0;

        //현재 선택된 메인 버튼을 번호로 변환
        switch (curMainBtnSelect)
        {
            case MainBtnOptions.Attack:
                selectedMainBtnNum = 0;
                break;
            case MainBtnOptions.Skill:
                selectedMainBtnNum = 1;
                break;
            case MainBtnOptions.Item:
                selectedMainBtnNum = 2;
                break;
            case MainBtnOptions.Etc:
                selectedMainBtnNum = 3;
                break;
        }

        for (int i = 0; i <= 3; i++)
        {
            if (i == selectedMainBtnNum)
            {
                mainActBtns[i].color = selectedMainBtnColor[i];
            }
            else
            {
                mainActBtns[i].color = unselectedMainBtnColors[i];
            }
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        curMod = MenuOptions.MainMenu;
        curMainBtnSelect = MainBtnOptions.Attack;
        UpdateMainBtnHighlight();
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionalInput();
        switch (curMod)
        {
            case MenuOptions.MainMenu:
                MoveMainMenuCursor();
                UpdateMainBtnHighlight();
                if (Input.GetButtonDown("Submit"))          //확인 입력
                {
                    //현재 선택메뉴 확인하고 해당 메뉴 실행
                    switch (curMainBtnSelect)
                    {
                        //TODO: 메뉴 실행되게 하기
                        case MainBtnOptions.Attack:
                            Debug.Log("공격");
                            break;
                        case MainBtnOptions.Skill:
                            Debug.Log("스킬");
                            break;
                        case MainBtnOptions.Item:
                            Debug.Log("아이템");
                            break;
                        case MainBtnOptions.Etc:
                            Debug.Log("기타");
                            break;
                    }
                }
                break;
        }
    }
}
