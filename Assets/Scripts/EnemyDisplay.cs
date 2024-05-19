using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEditor;
using UnityEngine;

public class EnemyDisplay : MonoBehaviour
{
    public GameObject[] enemyDisplayObject = new GameObject[5];     //�� ǥ�� ������Ʈ

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 400, 400, 400, 400 }, { -90, 90, 400, 400, 400 }, { -180, 0, 180, 400, 400 }, { -225, -75, 75, 225, 400 }, { -252, -126, 0, 126, 252 } };
    //�� ��ġ X ��ǥ

    public int[,] enemyAssignStatus = new int[2, 5];        //�� �Ҵ� ����
                                                            //0 ��: �Ҵ�� ���� ID
                                                            //1 ��: �Ҵ�� ���� ���� ��ȣ

    int enemyAmount = 0;                                    //�� ����
    int nextEnemyNumber = 1;                                //������ �Ҵ�� ���� ���� ��ȣ

    int formationTypeNum;                                   //�뿭 ����
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
    public void WakeBattleEnemyFormation(int[] enemyIDs, bool keepFirstEnemyMiddle)
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

            //���� ���� 5���� ���ٸ�(�߰��� 0�� ���Դٸ�)
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
        if(removeEnemyLocation != -1)
        {
            enemyAssignStatus[0, removeEnemyLocation] = 0;
            enemyAssignStatus[1, removeEnemyLocation] = 0;
            enemyAmount--;
        }
        else
        {
            Debug.LogError($"{removingEnemyNum}, �ش� �� ��ȣ�� ���� ã�� �� �����ϴ�.");
        }
    }


    //�� �߰�
    public void AddEnemy(int enemyID)
    {
        int lastLine = findEnemyLocation(0);

        if(lastLine != -1)
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
        if(originLocation != -1)
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
            if(enemyAmount % 2 == 0)
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

    void giveEnemyDirection()
    {
        //TODO: �Ҵ�� �� ǥ�ñ⿡ ������ ���, X��ǥ, �� ID�� �ֱ�.
    }

    int findEnemyLocation(int enemyNum)
    {
        for(int i = 0; i < 5; i++)
        {
            if (enemyAssignStatus[1, i] == enemyNum)
            {
                return i;
            }
        }
        return -1;
    }

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
        
    }
}
