using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySelectionControl : MonoBehaviour
{
    public GameObject BattleMaster;
    public GameObject EnemyDisplay;
    public GameObject EnemyDB;
    public GameObject[] EnemyDisplayers = new GameObject[5];

    int languageVal;

    int[] enemyPlaceCache = new int[5];

    public int curSelectNum;
    public int selectedAssignNum;
    public int selectedID;
    public string selectedName;


    //���� �Է� �׸��
    private float horInputWaitTime = 0f;               //���� �Է� ������ �ð� ��
    private float horizontalInput = 0f;                 //���� �Է°�
    private float prevHorizontalInput;                  //���� ���� �Է°�
    private bool horReleased = true;                    //���� �ѹ� Ǯ��

    void GetDirectionalInput()
    {
        //���� �Է� �ޱ�
        prevHorizontalInput = horizontalInput;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //���� �Է� ���� ���� �Է� �ν� ���� ���̰�, �� �Է°��� �ν� ���� ���̸� ���� �ѹ� Ǯ�ȴٰ� ��
        if (prevHorizontalInput >= 0.3f || prevHorizontalInput <= -0.3f)
        {
            if (horizontalInput < 0.3f && horizontalInput > -0.3f) horReleased = true;
        }
    }

    void UpdateSelectionNum()
    {
        //�Է� ��� �ð� ����
        horInputWaitTime += Time.deltaTime;

        //�ѹ� Ǯ�Ȱų�, �Է� ��� �ð��� 0.2�� �̻��� ��.
        if (horReleased || horInputWaitTime >= 0.2f)
        {
            if(horizontalInput >= 0.3f && curSelectNum != 4)
            {
                //�Է� ��� �ð� �ʱ�ȭ, Ǯ���� ����
                horInputWaitTime = 0f;
                horReleased = false;

                //���� ���� ��ġ + 1���� �ϳ��� ã�ٰ� ���ڸ� �ƴѰ����� ���� ��ȣ ������Ʈ
                for(int i = curSelectNum + 1; i < 5; i++)
                {
                    curSelectNum = i;

                    if (enemyPlaceCache[i] != 0)
                    {
                        GetSelectedEnemyData(i);
                        break;
                    }
                }
            }
            else if(horizontalInput <= -0.3f && curSelectNum != 0)
            {
                //�Է� ��� �ð� �ʱ�ȭ, Ǯ���� ����
                horInputWaitTime = 0f;
                horReleased = false;

                //���� ���� ��ġ + 1���� �ϳ��� ã�ٰ� ���ڸ� �ƴѰ����� ���� ��ȣ ������Ʈ
                for (int i = curSelectNum - 1; i >= 0; i--)
                {
                    curSelectNum = i;

                    if (enemyPlaceCache[i] != 0)
                    {
                        GetSelectedEnemyData(i);
                        break;
                    }
                }
            }
        }
    }

    void GetSelectedEnemyData(int selectionNum)
    {
        //���� ���õ� ���� �Ҵ� ��ȣ
        selectedAssignNum = enemyPlaceCache[selectionNum];

        //���� ���õ� ���� ID
        EnemyDisplay.GetComponent<BattleEnemyManager>().SetSearchEnemyAssign(selectedAssignNum);
        selectedID = EnemyDisplay.GetComponent<BattleEnemyManager>().resultEnemyStatus.EnemyID;

        //���� ���õ� ���� �̸�
        EnemyDB.GetComponent<EnemyData>().SetSearchEnemy(selectedID);
        selectedName = EnemyDB.GetComponent<EnemyData>().resultEnemyData.EnemyName[languageVal];
    }

    void UpdateDisplay()
    {
        //TODO: �� ���� UI ����
    }

    void GetCache()
    {
        enemyPlaceCache = EnemyDisplay.GetComponent<BattleEnemyManager>().enemyPlaces;
    }

    // Start is called before the first frame update
    void Start()
    {
        languageVal = BattleMaster.GetComponent<BattleMaster>().languageVal;
    }


    public bool TEST_LOCK = true;
    // Update is called once per frame
    void Update()
    {
        if (!TEST_LOCK)
        {
            GetCache();
            GetDirectionalInput();
            UpdateSelectionNum();
        }
    }
}
