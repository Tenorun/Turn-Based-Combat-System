using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    private RectTransform uiRectTransform;

    const float _minTopValue_ = -152f;                  //â�� �ּ� ����
    const float _maxTopValue_ = -71f;                   //â�� �ִ� ����
    
    private float topValue;                             //â�� ����
    private float topValueVar = 0.1f;                   //â�� ���� ���� �Լ� ����

    private bool changeSizeTrigger = false;             //â �ٲٱ� Ʈ����

    [SerializeField] private bool isExpanded = false;   //â �þ���� ����

    public void changeSize(bool inputType)              //â ũ�� ���� ȣ��
                                                        //(inputType)true: �ø���, false: ���̱�
    {
        if(inputType)       //���̱�
        {
            if (!isExpanded)
            {
                if (!changeSizeTrigger)
                {
                    changeSizeTrigger = true;
                }
            }
        }
        else                //���̱�
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

    private void changeTopValue(float Top_)               //â ũ�� ����
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
            if (isExpanded)                 //���̱�
            {
                topValue = _maxTopValue_ - (Mathf.Log(topValueVar, 2) * 10 - 5);

                if (topValue <= _minTopValue_)//������ �پ������� ������
                {
                    isExpanded = false;
                    changeSizeTrigger = false;
                    topValue = _minTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += 1500f * Time.deltaTime;//â�� ���� ���� �Լ��� ����
                }

                if (topValue < _maxTopValue_)//ũ�� ǥ�� ����
                {
                    changeTopValue(topValue);
                }
            }
            else                            //�ø���
            {
                topValue = _minTopValue_ + (Mathf.Log(topValueVar, 2) * 10 - 5);
                if(topValue >= _maxTopValue_)//������ �þ���� ������
                {
                    isExpanded = true;
                    changeSizeTrigger = false;
                    topValue = _maxTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += 1500f*Time.deltaTime;//â�� ���� ���� �Լ��� ����
                }

                if(topValue > _minTopValue_)//ũ�� ǥ�� ����
                {
                    changeTopValue(topValue);
                }
            }
        }
    }
}
