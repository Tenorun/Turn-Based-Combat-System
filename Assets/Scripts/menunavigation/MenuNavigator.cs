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
        Main,
        Attack,
        Skill,
        Item,
        Etc,
        EnemySelection,
        AllySelection,
        Locked
    }
    MenuOptions curMenuMod = new MenuOptions();                     //현재 메뉴 모드

    public Image[] mainActBtns = new Image[4];              //버튼들
    public Color[] selectedMainBtnColor = new Color[4];     //선택된 버튼의 색상들
    public Color[] unselectedMainBtnColors = new Color[4];  //선택안된 버튼의 색상들


    //메인 버튼 옵션
    public enum MainBtn
    {
        Attack,
        Skill,
        Item,
        Etc
    }
    public MainBtn curMainBtnSelect;                 //현재 선택된 메인 버튼

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
            case MainBtn.Attack:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtn.Skill;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtn.Item;

                break;
            case MainBtn.Skill:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtn.Attack;
                else if (VerticalInput <= -0.3f) curMainBtnSelect = MainBtn.Etc;

                break;
            case MainBtn.Item:
                if (HorizontalInput >= 0.3f) curMainBtnSelect = MainBtn.Etc;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtn.Attack;

                break;
            case MainBtn.Etc:
                if (HorizontalInput <= -0.3f) curMainBtnSelect = MainBtn.Item;
                else if (VerticalInput >= 0.3f) curMainBtnSelect = MainBtn.Skill;

                break;
        }
    }

    private void UpdateMainBtnHighlight()                       //메인 버튼 하이라이트 바꾸기
    {
        int selectedMainBtnNum = 0;

        //현재 선택된 메인 버튼을 번호로 변환
        switch (curMainBtnSelect)
        {
            case MainBtn.Attack:
                selectedMainBtnNum = 0;
                break;
            case MainBtn.Skill:
                selectedMainBtnNum = 1;
                break;
            case MainBtn.Item:
                selectedMainBtnNum = 2;
                break;
            case MainBtn.Etc:
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



    private RectTransform uiRectTransform;

    public GameObject ItemMenuFrame;
    public GameObject SkillMenuFrame;

    const float _minTopValue_ = -287f;                  //창의 최소 길이
    const float _maxTopValue_ = -170f;                  //창의 최대 길이
    const float _movingSpeed_ = 100f;                   //창 확대 축소 속도

    private float topValue;                             //창의 길이
    private float topValueVar = 0.1f;                   //창의 길이 조정 함수 변수

    public bool changeSizeTrigger = false;              //창 바꾸기 트리거
    public bool isExpanded = false;                     //창 늘어나있음 여부


    MenuOptions curTargetMenu = new MenuOptions();

    private void MenuSizeChange()
    {
        if (isExpanded)                 //줄이기
        {
            topValue = _minTopValue_ + 0.05f * Mathf.Pow(topValueVar - 44f, 2);

            if (topValue <= _minTopValue_ + 1)//끝가지 줄어들었을때 끝내기
            {
                isExpanded = false;
                changeSizeTrigger = false;
                topValue = _minTopValue_;
                topValueVar = 0.1f;

                curMenuMod = MenuOptions.Main;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//창의 길이 조정 함수값 변경
            }

            if (topValue < _maxTopValue_)//크기 표시 변경
            {
                uiRectTransform.offsetMax = new Vector2(-159, topValue);
            }
        }
        else                            //늘리기
        {
            topValue = _maxTopValue_ - 0.05f * Mathf.Pow(topValueVar - 44f, 2);
            if (topValue >= _maxTopValue_ - 1)//끝가지 늘어났을때 끝내기
            {
                isExpanded = true;
                changeSizeTrigger = false;
                topValue = _maxTopValue_;
                topValueVar = 0.1f;

                curMenuMod = curTargetMenu;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//창의 길이 조정 함수값 변경
            }

            if (topValue > _minTopValue_)//크기 표시 변경
            {
                uiRectTransform.offsetMax = new Vector2(-159, topValue);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        curMenuMod = MenuOptions.Main;
        curMainBtnSelect = MainBtn.Attack;
        UpdateMainBtnHighlight();


        uiRectTransform = GetComponent<RectTransform>();

        uiRectTransform.offsetMax = new Vector2(-159, _minTopValue_);
    }

    // Update is called once per frame
    void Update()
    {
        GetDirectionalInput();
        switch (curMenuMod)
        {
            //메인 메뉴
            case MenuOptions.Main:
                MoveMainMenuCursor();
                UpdateMainBtnHighlight();
                if (Input.GetButtonDown("Submit"))          //확인 입력
                {
                    //현재 선택메뉴 확인하고 해당 메뉴 실행
                    switch (curMainBtnSelect)
                    {
                        //TODO: 메뉴 실행되게 하기
                        case MainBtn.Attack:
                            Debug.Log("공격");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Attack;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Skill:
                            Debug.Log("스킬");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Skill;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Item:
                            Debug.Log("아이템");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Item;
                            changeSizeTrigger = true;
                            break;
                        case MainBtn.Etc:
                            Debug.Log("기타");
                            curMenuMod = MenuOptions.Locked;
                            curTargetMenu = MenuOptions.Etc;
                            changeSizeTrigger = true;
                            break;
                    }
                }
                break;
            //공격 메뉴
            case MenuOptions.Attack:
                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    changeSizeTrigger = true;
                }
                break;
            //스킬 메뉴
            case MenuOptions.Skill:

                if (!SkillMenuFrame.activeSelf) SkillMenuFrame.SetActive(true);

                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    SkillMenuFrame.SetActive(false);
                    changeSizeTrigger = true;
                }
                break;
            //아이템 메뉴
            case MenuOptions.Item:

                if (!ItemMenuFrame.activeSelf) ItemMenuFrame.SetActive(true);

                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    ItemMenuFrame.SetActive(false);
                    changeSizeTrigger = true;
                }
                break;
            //기타 메뉴
            case MenuOptions.Etc:
                if (Input.GetButtonDown("Cancel"))
                {
                    curMenuMod = MenuOptions.Locked;
                    curTargetMenu = MenuOptions.Main;
                    changeSizeTrigger = true;
                }
                break;
            case MenuOptions.Locked:
                break;

        }

        if (changeSizeTrigger)
        {
            MenuSizeChange();
        }
    }
}
