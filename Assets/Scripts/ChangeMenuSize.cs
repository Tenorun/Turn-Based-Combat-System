using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    
    private RectTransform uiRectTransform;

    const float _defaultTopValue_ = -152f;
    const float _maxTopValue_ = -71f;
    
    private float topValue;
    private float topValueVar = 0.1f;

    public bool changeSizeTrigger = false;

    [SerializeField] private bool isExpanded = false;

    void changeTopValue(float Top_)
    {
        uiRectTransform.offsetMax = new Vector2(-131, Top_);
    }

    // Start is called before the first frame update
    void Start()
    {
        topValue = _defaultTopValue_;
        uiRectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (changeSizeTrigger)
        {
            if (isExpanded)                 //줄이기
            {
                isExpanded = false;
                changeSizeTrigger = false;
                topValue = _defaultTopValue_;
                changeTopValue(topValue);
            }
            else                            //늘리기
            {
                topValue = _defaultTopValue_ + (Mathf.Log(topValueVar, 2)*10-5);
                if(topValue >= _maxTopValue_)
                {
                    isExpanded = true;
                    changeSizeTrigger = false;
                    topValue = _maxTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += 1500f*Time.deltaTime;
                }
                if(topValue > _defaultTopValue_)
                {
                    changeTopValue(topValue);
                }
            }
        }
    }
}
