using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuWaker : MonoBehaviour
{
    private RectTransform uiRectTransform;

    public GameObject ItemMenuFrame;
    public GameObject SkillMenuFrame;

    const float _minTopValue_ = -287f;                  //â�� �ּ� ����
    const float _maxTopValue_ = -170f;                   //â�� �ִ� ����
    const float _movingSpeed_ = 100f;                   //â Ȯ�� ��� �ӵ�

    private float topValue;                             //â�� ����
    private float topValueVar = 0.1f;                   //â�� ���� ���� �Լ� ����

    public bool changeSizeTrigger = false;              //â �ٲٱ� Ʈ����
    public bool isExpanded = false;                     //â �þ���� ����


    //�޴� ����� ȣ��
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
                                                        //(inputType)true: �ø���, false: ���̱�
    {
        if (inputType && !isExpanded && !changeSizeTrigger)      //�þ���� �ʰ� Ʈ���Ű� ����������
        {
            changeSizeTrigger = true;
        }
        else if (!inputType && isExpanded && !changeSizeTrigger)       //�þ�ְ� Ʈ���Ű� ����������
        {
            changeSizeTrigger = true;
        }
    }

    private void MenuSizeChange()
    {
        if (isExpanded)                 //���̱�
        {
            topValue = _minTopValue_ + 0.05f * Mathf.Pow(topValueVar - 44f, 2);

            if (topValue <= _minTopValue_ + 1)//������ �پ������� ������
            {
                isExpanded = false;
                changeSizeTrigger = false;
                topValue = _minTopValue_;
                topValueVar = 0.1f;

                this.GetComponent<ActionButton>().isActBtnLocked = false;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//â�� ���� ���� �Լ��� ����
            }

            if (topValue < _maxTopValue_)//ũ�� ǥ�� ����
            {
                uiRectTransform.offsetMax = new Vector2(-159, topValue);
            }
        }
        else                            //�ø���
        {
            topValue = _maxTopValue_ - 0.05f * Mathf.Pow(topValueVar - 44f, 2);
            if (topValue >= _maxTopValue_ - 1)//������ �þ���� ������
            {
                isExpanded = true;
                changeSizeTrigger = false;
                topValue = _maxTopValue_;
                topValueVar = 0.1f;
            }
            else
            {
                topValueVar += _movingSpeed_ * Time.deltaTime;//â�� ���� ���� �Լ��� ����
            }

            if (topValue > _minTopValue_)//ũ�� ǥ�� ����
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
