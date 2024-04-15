using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationManager : MonoBehaviour
{
    public GameObject[] enemyDisplayObject = new GameObject[5];     //적 표시 오브젝트

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 450, 450, 450, 450 }, { -90, 90, 450, 450, 450 }, { -180, 0, 180, 450, 450 }, { -225, -75, 75, 225, 450 }, { -252, -126, 0, 126, 252 } };
    //적 배치 X 좌표

    public int[,] enemyAssignStatus = new int[2, 5];        //적 할당 상태
                                                            //0 행: 할당된 적의 ID
                                                            //1 행: 할당된 적의 전투 번호

    int enemyAmount = 0;                                    //적 개수
    int nextEnemyNumber = 1;                                //다음에 할당될 적의 전투 번호

    public int formationTypeNum;                                   //대열 형식
    /*대열 형식
     1: 1개 장소 배열     ■
     2: 2개 장소 배열    ■ ■
     3: 3개 장소 배열   ■ ■ ■
     4: 4개 장소 배열  ■ ■ ■ ■
     5: 5개 장소 배열 ■ ■ ■ ■ ■
     */

    public bool keepSomeOneMiddle;                 //누군가를 가운데에 고정할지 여부 
    int[] enemyToKeepMiddle;                       //가운데에 고정할 적의 전투 번호 (예시: 보스) *0: 없음
                                                   //추가 설명: 누군가를 가운데 배열 하면 formationType는 누군가를 가운데 배치 할 수 있는 홀수가 되며, 그 가운데는 무조건 가운데에 배열하기로 한 적으로 배열된다.


    //적 디스플레이 시작
    public void WakeEnemyFormationManager(int[] enemyIDs, bool keepFirstEnemyMiddle)
    {
        //변수 초기화
        enemyAssignStatus = new int[2, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        keepSomeOneMiddle = false;
        enemyToKeepMiddle = new int[2] { 0, 0 };
        nextEnemyNumber = 1;
        enemyAmount = 0;
        formationTypeNum = 0;

        for (int i = 0; i < 5; i++)
        {
            enemyAssignStatus[0, i] = enemyIDs[i];
            enemyAssignStatus[1, i] = nextEnemyNumber;
            nextEnemyNumber++;

            //만약 적이 5명보다 적다면(중간에 0이 나왔다면)
            if (enemyIDs[i] == 0)
            {
                enemyAmount = i + 1;
                formationTypeNum = i;

                //첫째 적 고정
                if (keepFirstEnemyMiddle) keepMiddle(enemyIDs[0]);
                UpdateAssignInfo();
                return;
            }
        }

        //적이 5명일때
        enemyAmount = 5;
        formationTypeNum = 4;

        //누군가를 가운데 고정해야 한다면.
        if (keepFirstEnemyMiddle) keepMiddle(enemyIDs[0]);
        UpdateAssignInfo();
        return;
    }


    //적 제거
    public void RemoveEnemy(int removingEnemyNum)
    {
        int removeEnemyLocation = findEnemyLocation(removingEnemyNum);
        if (removeEnemyLocation != -1)
        {
            enemyAssignStatus[0, removeEnemyLocation] = 0;
            enemyAssignStatus[1, removeEnemyLocation] = 0;
            enemyAmount--;
        }
        else
        {
            Debug.LogError($"{removingEnemyNum}, 해당 적 번호의 적을 찾을 수 없습니다.");
            Debug.LogError($"현재 적 번호들: {enemyAssignStatus[1, 0]}, {enemyAssignStatus[1, 1]}, {enemyAssignStatus[1, 2]}, {enemyAssignStatus[1, 3]}, {enemyAssignStatus[1, 4]}");
        }

        UpdateAssignInfo();
    }


    //적 추가
    public void AddEnemy(int enemyID)
    {
        int lastLine = findEnemyLocation(0);

        if (lastLine != -1)
        {
            enemyAssignStatus[0, lastLine] = enemyID;
            enemyAssignStatus[1, lastLine] = nextEnemyNumber;
            nextEnemyNumber++;
            enemyAmount++;
        }
        else
        {
            Debug.LogError("적을 더이상 추가 할 수 없습니다.");
        }
    }


    //누군가를 가운데 고정시키는 함수
    public void keepMiddle(int middleEnemyNum)
    {
        int originLocation = findEnemyLocation(middleEnemyNum);
        if (originLocation != -1)
        {
            keepSomeOneMiddle = true;

            enemyToKeepMiddle = new int[2] { enemyAssignStatus[0, originLocation], middleEnemyNum };
            enemyAssignStatus[0, originLocation] = 0;
            enemyAssignStatus[1, originLocation] = 0;

            UpdateAssignInfo();
        }
        else
        {
            Debug.LogError($"{middleEnemyNum}, 해당 적 번호의 적을 찾을 수 없습니다.");
        }
    }


    //할당 상태 업데이트
    public void UpdateAssignInfo()
    {
        //가운데 고정된 적을 0으로 일단 없에기
        int midEnemyLocation = findEnemyLocation(enemyToKeepMiddle[1]);
        if (midEnemyLocation != -1)
        {
            enemyAssignStatus[0, midEnemyLocation] = 0;
            enemyAssignStatus[1, midEnemyLocation] = 0;
        }

        //할당정보 배열의 적을 모두 오른쪽으로 밀기
        int index = 0;
        for (int i = 0; i < 5; i++)
        {
            if (enemyAssignStatus[1, i] != 0)
            {
                enemyAssignStatus[0, index] = enemyAssignStatus[0, i];
                enemyAssignStatus[1, index++] = enemyAssignStatus[1, i];
            }
        }

        // 남은 공간을 0으로 채움
        while (index < 5)
        {
            enemyAssignStatus[0, index] = 0;
            enemyAssignStatus[1, index++] = 0;
        }

        //formationTypeNum 정하기
        formationTypeNum = enemyAmount - 1;

        //누군가 가운데 고정된 경우
        if (keepSomeOneMiddle)
        {
            //적 개수가 짝수일시, 포메이션 타입을 홀수로 맞추기.
            //예) 2명: 포메이션 타입 = 2(3명 배열)/4명: 포메이션 타입 = 4(5명 배열)
            if (enemyAmount % 2 == 0)
            {
                formationTypeNum = enemyAmount;
            }

            int middleLocation = formationTypeNum / 2;                      //가운데 위치 구하기
            Debug.Log (middleLocation);

            //포메이션 뒤에서부터 가운데까지 하나씩 오른쪽으로 밀기.(가운데에 삽입될 적의 정보는 0으로 모두 오른쪽으로 밀렸기 때문에, 할당된것이 무엇인지 볼 필요는 없다.)
            for (int i = formationTypeNum; i > middleLocation; i--)
            {
                enemyAssignStatus[0, i] = enemyAssignStatus[0, i - 1];
                enemyAssignStatus[1, i] = enemyAssignStatus[1, i - 1];
            }

            //포메이션 가운데에 가운데에 고정될 적을 삽입
            enemyAssignStatus[0, middleLocation] = enemyToKeepMiddle[0];
            enemyAssignStatus[1, middleLocation] = enemyToKeepMiddle[1];
        }
    }

    void UpdateDisplay()
    {
        for(int i = 0; i<enemyDisplayObject.Length; i++)
        {
            if (enemyAssignStatus[0, i] == 0)
            {
                //Hide Enemy
                enemyDisplayObject[i].SetActive(false);
            }
            else
            {
                //Show Enemy
                enemyDisplayObject[i].SetActive(true);
            }
        }

        //TODO: 적 스프라이트 할당
    }

    void MoveEnemy()
    {
        // Ensure that the enemy formation type number is within bounds
        if (formationTypeNum < 0 || formationTypeNum >= enemyFormations.GetLength(0))
        {
            Debug.LogError("Invalid formation type number.");
            return;
        }

        // Move each enemy display object to its designated position
        for (int i = 0; i < enemyDisplayObject.Length; i++)
        {
            // Get the target position from the enemyFormations array
            Vector3 targetPosition = new Vector3(enemyFormations[formationTypeNum, i], 0f, 0f); // Assuming Y and Z positions are 0

            // Start coroutine for smooth movement
            StartCoroutine(MoveEnemyCoroutine(enemyDisplayObject[i].transform, targetPosition));
        }
    }

    // Coroutine for smooth movement of enemy display object
    IEnumerator MoveEnemyCoroutine(Transform enemyTransform, Vector3 targetPosition)
    {
        float elapsedTime = 0f;
        Vector3 startingPosition = enemyTransform.position;

        // Move towards the target position over time
        while (elapsedTime < 0.5f) // Moving in 0.5 seconds
        {
            // Calculate the interpolation ratio
            float t = elapsedTime / 0.5f; // 0.5 seconds duration

            // Smoothly move towards the target position using Lerp
            enemyTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure final position is precisely the target position
        enemyTransform.position = targetPosition;
    }

    //TODO: Implement EnemyStatusData





    int findEnemyLocation(int enemyNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (enemyAssignStatus[1, i] == enemyNum)
            {
                return i;
            }
        }
        return -1;
    }







    public bool testMoveEnemyTrigger = false;
    public bool testWakeFormationManagerTrigger = false;
    public bool removeEnemyTrigger = false;

    public bool printEnemyNumTrigger = false;
    public bool testKeepMiddleTrigger = false;

    void Start()
    {
        //변수 초기화
        enemyAssignStatus = new int[2, 5] { { 0, 0, 0, 0, 0 }, { 0, 0, 0, 0, 0 } };
        keepSomeOneMiddle = false;
        enemyToKeepMiddle = new int[2] { 0, 0 };
        nextEnemyNumber = 1;
        enemyAmount = 0;
        formationTypeNum = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (testWakeFormationManagerTrigger)
        {
            testWakeFormationManagerTrigger = false;
            WakeEnemyFormationManager(new int[] { 1, 1, 1, 1, 2 }, false);
        }

        if (removeEnemyTrigger)
        {
            removeEnemyTrigger = false;
            RemoveEnemy(2);
            //RemoveEnemy(4);
        }

        if (testMoveEnemyTrigger)
        {
            testMoveEnemyTrigger = false;
            UpdateDisplay();
            MoveEnemy();
        }

        if (printEnemyNumTrigger)
        {
            printEnemyNumTrigger = false;
            Debug.Log($"현재 적 번호들: {enemyAssignStatus[1,0]}, {enemyAssignStatus[1, 1]}, {enemyAssignStatus[1, 2]}, {enemyAssignStatus[1, 3]}, {enemyAssignStatus[1, 4]}");
            Debug.Log($"현재 적 ID들: {enemyAssignStatus[0, 0]}, {enemyAssignStatus[0, 1]}, {enemyAssignStatus[0, 2]}, {enemyAssignStatus[0, 3]}, {enemyAssignStatus[0, 4]}");
        }

        if (testKeepMiddleTrigger)
        {
            testKeepMiddleTrigger = false;
            keepMiddle(5);
        }

    }
}
