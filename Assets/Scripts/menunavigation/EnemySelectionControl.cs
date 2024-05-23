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


    //가로 입력 항목들
    private float horInputWaitTime = 0f;               //가로 입력 딜레이 시간 값
    private float horizontalInput = 0f;                 //가로 입력값
    private float prevHorizontalInput;                  //이전 가로 입력값
    private bool horReleased = true;                    //가로 한번 풀림

    void GetDirectionalInput()
    {
        //가로 입력 받기
        prevHorizontalInput = horizontalInput;
        horizontalInput = Input.GetAxisRaw("Horizontal");
        //이전 입력 가로 값이 입력 인식 범위 안이고, 현 입력값이 인식 범위 밖이면 가로 한번 풀렸다고 함
        if (prevHorizontalInput >= 0.3f || prevHorizontalInput <= -0.3f)
        {
            if (horizontalInput < 0.3f && horizontalInput > -0.3f) horReleased = true;
        }
    }

    void UpdateSelectionNum()
    {
        //입력 대기 시간 증가
        horInputWaitTime += Time.deltaTime;

        //한번 풀렸거나, 입력 대기 시간이 0.2초 이상일 때.
        if (horReleased || horInputWaitTime >= 0.2f)
        {
            if(horizontalInput >= 0.3f && curSelectNum != 4)
            {
                //입력 대기 시간 초기화, 풀리지 않음
                horInputWaitTime = 0f;
                horReleased = false;

                //현재 선택 위치 + 1에서 하나씩 찾다가 빈자리 아닌곳으로 선택 번호 업데이트
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
                //입력 대기 시간 초기화, 풀리지 않음
                horInputWaitTime = 0f;
                horReleased = false;

                //현재 선택 위치 + 1에서 하나씩 찾다가 빈자리 아닌곳으로 선택 번호 업데이트
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
        //현재 선택된 적의 할당 번호
        selectedAssignNum = enemyPlaceCache[selectionNum];

        //현재 선택된 적의 ID
        EnemyDisplay.GetComponent<BattleEnemyManager>().SetSearchEnemyAssign(selectedAssignNum);
        selectedID = EnemyDisplay.GetComponent<BattleEnemyManager>().resultEnemyStatus.EnemyID;

        //현재 선택된 적의 이름
        EnemyDB.GetComponent<EnemyData>().SetSearchEnemy(selectedID);
        selectedName = EnemyDB.GetComponent<EnemyData>().resultEnemyData.EnemyName[languageVal];
    }

    void UpdateDisplay()
    {
        //TODO: 적 선택 UI 구현
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
