using UnityEngine;

public class ChangeMenuSize : MonoBehaviour
{
    private RectTransform uiRectTransform;

    const float _minTopValue_ = -206f;                  //â�� �ּ� ����
    const float _maxTopValue_ = -110f;                   //â�� �ִ� ����
    const float _movingSpeed_ = 100f;                   //â Ȯ�� ��� �ӵ�
    
    private float topValue;                             //â�� ����
    private float topValueVar = 0.1f;                   //â�� ���� ���� �Լ� ����

    public bool changeSizeTrigger = false;              //â �ٲٱ� Ʈ����
    public bool isExpanded = false;                     //â �þ���� ����

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
            if (isExpanded)                 //���̱�
            {
                topValue = _minTopValue_ + 0.05f * Mathf.Pow(topValueVar - 44f,2);

                if (topValue <= _minTopValue_ + 1)//������ �پ������� ������
                {
                    isExpanded = false;
                    changeSizeTrigger = false;
                    topValue = _minTopValue_;
                    topValueVar = 0.1f;
                }
                else
                {
                    topValueVar += _movingSpeed_ * Time.deltaTime;//â�� ���� ���� �Լ��� ����
                }

                if (topValue < _maxTopValue_)//ũ�� ǥ�� ����
                {
                    changeTopValue(topValue);
                }
            }
            else                            //�ø���
            {
                topValue = _maxTopValue_ - 0.05f * Mathf.Pow(topValueVar - 44f,2);
                if(topValue >= _maxTopValue_ - 1)//������ �þ���� ������
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

                if(topValue > _minTopValue_)//ũ�� ǥ�� ����
                {
                    changeTopValue(topValue);
                }
            }
        }
    }
}
