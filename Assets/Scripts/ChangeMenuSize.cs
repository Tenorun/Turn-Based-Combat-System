using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    private RectTransform uiRectTransform;

    const float _minTopValue_ = -206f;                  //창의 최소 길이
    const float _maxTopValue_ = -110f;                   //창의 최대 길이
    const float _movingSpeed_ = 100f;                   //창 확대 축소 속도
    
    private float topValue;                             //창의 길이
    private float topValueVar = 0.1f;                   //창의 길이 조정 함수 변수

    public bool changeSizeTrigger = false;              //창 바꾸기 트리거
    public bool isExpanded = false;                     //창 늘어나있음 여부

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
        uiRectTransform.offsetMax = new Vector2(-140, Top_);
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
                topValue = _minTopValue_ + 0.05f * Mathf.Pow(topValueVar - 44f,2);

                if (topValue <= _minTopValue_ + 1)//끝가지 줄어들었을때 끝내기
                {
                    isExpanded = false;
                    changeSizeTrigger = false;
                    topValue = _minTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += _movingSpeed_ * Time.deltaTime;//창의 길이 조정 함수값 변경
                }

                if (topValue < _maxTopValue_)//크기 표시 변경
                {
                    changeTopValue(topValue);
                }
            }
            else                            //늘리기
            {
                topValue = _maxTopValue_ - 0.05f * Mathf.Pow(topValueVar - 44f,2);
                if(topValue >= _maxTopValue_ - 1)//끝가지 늘어났을때 끝내기
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

                if(topValue > _minTopValue_)//크기 표시 변경
                {
                    changeTopValue(topValue);
                }
            }
        }
    }
}
