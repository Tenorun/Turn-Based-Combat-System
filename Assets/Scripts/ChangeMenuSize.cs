using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    private RectTransform uiRectTransform;

    const float _minTopValue_ = -152f;                  //창의 최소 길이
    const float _maxTopValue_ = -71f;                   //창의 최대 길이
    
    private float topValue;                             //창의 길이
    private float topValueVar = 0.1f;                   //창의 길이 조정 함수 변수

    private bool changeSizeTrigger = false;             //창 바꾸기 트리거

    [SerializeField] private bool isExpanded = false;   //창 늘어나있음 여부

    public void changeSize(bool inputType)              //창 크기 변경 호출
                                                        //(inputType)true: 늘리기, false: 줄이기
    {
        if(inputType)       //늘이기
        {
            if (!isExpanded)
            {
                if (!changeSizeTrigger)
                {
                    changeSizeTrigger = true;
                }
            }
        }
        else                //줄이기
        {
            if (isExpanded)
            {
                if (!changeSizeTrigger)
                {
                    changeSizeTrigger = true;
                }
            }
        }
    }

    private void changeTopValue(float Top_)               //창 크기 변경
    {
        uiRectTransform.offsetMax = new Vector2(-131, Top_);
    }

    void Start()
    {
        topValue = _minTopValue_;
        uiRectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        if (changeSizeTrigger)
        {
            if (isExpanded)                 //줄이기
            {
                topValue = _maxTopValue_ - (Mathf.Log(topValueVar, 2) * 10 - 5);

                if (topValue <= _minTopValue_)//끝가지 줄어들었을때 끝내기
                {
                    isExpanded = false;
                    changeSizeTrigger = false;
                    topValue = _minTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += 1500f * Time.deltaTime;//창의 길이 조정 함수값 변경
                }

                if (topValue < _maxTopValue_)//크기 표시 변경
                {
                    changeTopValue(topValue);
                }
            }
            else                            //늘리기
            {
                topValue = _minTopValue_ + (Mathf.Log(topValueVar, 2) * 10 - 5);
                if(topValue >= _maxTopValue_)//끝가지 늘어났을때 끝내기
                {
                    isExpanded = true;
                    changeSizeTrigger = false;
                    topValue = _maxTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += 1500f*Time.deltaTime;//창의 길이 조정 함수값 변경
                }

                if(topValue > _minTopValue_)//크기 표시 변경
                {
                    changeTopValue(topValue);
                }
            }
        }
    }
}
