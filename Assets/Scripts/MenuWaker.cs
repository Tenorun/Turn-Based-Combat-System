using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWaker : MonoBehaviour
{
    private RectTransform uiRectTransform;

    public GameObject ItemMenuFrame;
    public GameObject SkillMenuFrame;

    const float _minTopValue_ = -287f;                  //창의 최소 길이
    const float _maxTopValue_ = -170f;                   //창의 최대 길이
    const float _movingSpeed_ = 100f;                   //창 확대 축소 속도

    private float topValue;                             //창의 길이
    private float topValueVar = 0.1f;                   //창의 길이 조정 함수 변수

    public bool changeSizeTrigger = false;              //창 바꾸기 트리거
    public bool isExpanded = false;                     //창 늘어나있음 여부


    //메뉴 깨우기 호출
    public void SetWakeMenu(bool displayStatus, int inputNum)
    {
        switch (inputNum)
        {
            case 1:
                SetChangeMenuSize(true);
                SkillMenuFrame.SetActive(displayStatus);
                break;
            case 2:
                SetChangeMenuSize(true);
                ItemMenuFrame.SetActive(displayStatus);
                break;
            case 4:
                SkillMenuFrame.SetActive(displayStatus);
                ItemMenuFrame.SetActive(displayStatus);
                SetChangeMenuSize(false);
                break;

        }
    }

    public void SetChangeMenuSize(bool inputType)
                                                        //(inputType)true: 늘리기, false: 줄이기
    {
        if (inputType && !isExpanded && !changeSizeTrigger)      //늘어나있지 않고 트리거가 꺼져있을때
        {
            changeSizeTrigger = true;
        }
        else if (!inputType && isExpanded && !changeSizeTrigger)       //늘어나있고 트리거가 꺼져있을때
        {
            changeSizeTrigger = true;
        }
    }

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

                this.GetComponent<ActionButton>().isActBtnLocked = false;
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
        topValue = _minTopValue_;
        uiRectTransform = GetComponent<RectTransform>();

        uiRectTransform.offsetMax = new Vector2(-159, _minTopValue_);
        SetWakeMenu(false, 4);
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSizeTrigger)
        {
            MenuSizeChange();
        }
    }
}
