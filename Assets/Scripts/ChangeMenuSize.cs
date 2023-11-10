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
            // ���ϴ� ũ��� UI�� ���̸� �����մϴ�.
            float newHeight = 109f; // ���ϴ� ���� ������ �����ϼ���.
            uiRectTransform.sizeDelta = new Vector2(uiRectTransform.sizeDelta.x, newHeight);

            // UI�� ���η� �÷ȱ� ������ Top ���� �����Ͽ� ��� �����մϴ�.
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
