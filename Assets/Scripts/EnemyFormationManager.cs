using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFormationManager : MonoBehaviour
{
    public GameObject[] enemyDisplayObject = new GameObject[5];     //�� ǥ�� ������Ʈ

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 400, 400, 400, 400 }, { -90, 90, 400, 400, 400 }, { -180, 0, 180, 400, 400 }, { -225, -75, 75, 225, 400 }, { -252, -126, 0, 126, 252 } };
    //�� ��ġ X ��ǥ

    public int[,] enemyAssignStatus = new int[2, 5];        //�� �Ҵ� ����
                                                            //0 ��: �Ҵ�� ���� ID
                                                            //1 ��: �Ҵ�� ���� ���� ��ȣ

    int enemyAmount = 0;                                    //�� ����
    int nextEnemyNumber = 1;                                //������ �Ҵ�� ���� ���� ��ȣ

    public int formationTypeNum;                                   //�뿭 ����
    /*�뿭 ����
     1: 1�� ��� �迭     ��
     2: 2�� ��� �迭    �� ��
     3: 3�� ��� �迭   �� �� ��
     4: 4�� ��� �迭  �� �� �� ��
     5: 5�� ��� �迭 �� �� �� �� ��
     */

    public bool keepSomeOneMiddle;                 //�������� ����� �������� ���� 
    int[] enemyToKeepMiddle;                       //����� ������ ���� ���� ��ȣ (����: ����) *0: ����
                                                   //�߰� ����: �������� ��� �迭 �ϸ� formationType�� �������� ��� ��ġ �� �� �ִ� Ȧ���� �Ǹ�, �� ����� ������ ����� �迭�ϱ�� �� ������ �迭�ȴ�.


    //�� ���÷��� ����
    public void WakeEnemyFormationManager(int[] enemyIDs, bool keepFirstEnemyMiddle)
    {
        //���� �ʱ�ȭ
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

            //���� ���� 5������ ���ٸ�(�߰��� 0�� ���Դٸ�)
            if (enemyIDs[i] == 0)
            {
                enemyAmount = i + 1;
                formationTypeNum = i;

                //ù° �� ����
                if (keepFirstEnemyMiddle) keepMiddle(enemyIDs[0]);
                UpdateAssignInfo();
                return;
            }
        }

        //���� 5���϶�
        enemyAmount = 5;
        formationTypeNum = 4;

        //�������� ��� �����ؾ� �Ѵٸ�.
        if (keepFirstEnemyMiddle) keepMiddle(enemyIDs[0]);
        UpdateAssignInfo();
        return;
    }


    //�� ����
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
            Debug.LogError($"{removingEnemyNum}, �ش� �� ��ȣ�� ���� ã�� �� �����ϴ�.");

            for(int i =0; i < 5; i++)
            {
                Debug.LogError(enemyAssignStatus[1, i]);
            }
        }

        UpdateAssignInfo();
    }


    //�� �߰�
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
            Debug.LogError("���� ���̻� �߰� �� �� �����ϴ�.");
        }
    }


    //�������� ��� ������Ű�� �Լ�
    public void keepMiddle(int middleEnemyNum)
    {
        int originLocation = findEnemyLocation(middleEnemyNum);
        if (originLocation != -1)
        {
            keepSomeOneMiddle = true;

            enemyToKeepMiddle = new int[2] { enemyAssignStatus[0, originLocation], middleEnemyNum };
            enemyAssignStatus[0, originLocation] = 0;
            enemyAssignStatus[1, originLocation] = 0;
        }
        else
        {
            Debug.LogError($"{middleEnemyNum}, �ش� �� ��ȣ�� ���� ã�� �� �����ϴ�.");
        }
    }


    //�Ҵ� ���� ������Ʈ
    public void UpdateAssignInfo()
    {
        //TODO: RemoveEnemy�� ���� ���� �� ��, �Ҵ������� ������Ʈ �ϸ� 0�� �� ���������� �и��� �ʴ� ���װ� ����. �̰��� ��ĥ��.

        //�Ҵ� ���� �迭���� 0�� ��� ���������� �б�
        int left = 0;
        int right = 4;

        while (left < right)
        {
            // ���ʿ��� Ÿ���� ã���� �����ʰ� ��ȯ
            if (enemyAssignStatus[1, left] == 0)
            {
                // ���������� 0 ���� �̵�
                for (int i = left; i < right; i++)
                {
                    int[] temp = new int[2] { enemyAssignStatus[0, i], enemyAssignStatus[1, i] };
                    enemyAssignStatus[0, i] = enemyAssignStatus[0, i + 1];
                    enemyAssignStatus[1, i] = enemyAssignStatus[1, i + 1];

                    enemyAssignStatus[0, i + 1] = temp[0];
                    enemyAssignStatus[0, i + 1] = temp[1];
                }
                right--; // �迭�� ������ ������ 0�� ��ġ�ϰ� �Ǿ����Ƿ�, ������ �ε����� ����
            }
            else
            {
                left++; // 0�� �ƴ� ���� �״�� �ΰ� ���� �ε����� ������Ŵ
            }
        }


        //formationTypeNum ���ϱ�
        formationTypeNum = enemyAmount - 1;


        //������ ��� ������ ���
        if (keepSomeOneMiddle)
        {
            //�� ������ ¦���Ͻ�, �����̼� Ÿ���� Ȧ���� ���߱�.
            //��) 2��: �����̼� Ÿ�� = 2(3�� �迭)/4��: �����̼� Ÿ�� = 4(5�� �迭)
            if (enemyAmount % 2 == 0)
            {
                formationTypeNum = enemyAmount;
            }

            int middleLocation = formationTypeNum / 2;                      //��� ��ġ ���ϱ�

            //�����̼� �ڿ������� ������� �ϳ��� ���������� �б�.(����� ���Ե� ���� ������ 0���� ��� ���������� �зȱ� ������, �Ҵ�Ȱ��� �������� �� �ʿ�� ����.)
            for (int i = formationTypeNum; i > middleLocation; i--)
            {
                enemyAssignStatus[0, i] = enemyAssignStatus[0, i - 1];
                enemyAssignStatus[1, i] = enemyAssignStatus[1, i - 1];
            }

            //�����̼� ����� ����� ������ ���� ����
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

    void Start()
    {
        //���� �ʱ�ȭ
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
            WakeEnemyFormationManager(new int[] { 1, 1, 1, 1, 1 }, false);
        }

        if (removeEnemyTrigger)
        {
            removeEnemyTrigger = false;
            RemoveEnemy(2);
        }

        if (testMoveEnemyTrigger)
        {
            testMoveEnemyTrigger = false;
            UpdateDisplay();
            MoveEnemy();
        }
    }
}