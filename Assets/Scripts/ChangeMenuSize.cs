using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    private RectTransform uiRectTransform;

    void ChangeSize(bool isExpand)
    {
        if(isExpand)
        {
            // 원하는 크기로 UI의 높이를 조절합니다.
            float newHeight = 109f; // 원하는 높이 값으로 변경하세요.
            uiRectTransform.sizeDelta = new Vector2(uiRectTransform.sizeDelta.x, newHeight);

            // UI를 세로로 늘렸기 때문에 Top 값을 조절하여 가운데 정렬합니다.
            float topOffset = (Screen.height - newHeight) / 2f;
            uiRectTransform.anchoredPosition = new Vector2(uiRectTransform.anchoredPosition.x, -topOffset);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        uiRectTransform = GetComponent<RectTransform>();
        ChangeSize(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
