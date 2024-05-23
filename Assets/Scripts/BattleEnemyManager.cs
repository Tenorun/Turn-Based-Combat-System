using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleEnemyManager : MonoBehaviour
{
    //
    //<�� �Ҵ� ���� ����>
    //

    //���� �Ҵ� ��ȣ
    int nextAssignNum = 1;

    //���� ������ �� �Ҵ� ����
    public int[] enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };

    int enemyAmount = 0;                                    //�� ����


    //�� ������ ���̽�
    public GameObject enemyDB;

    public AssignEnemy resultEnemyStatus;

    // �Ҵ�� �� ���� Ŭ����
    public class AssignedEnemyStatus
    {
        // �Ҵ� �� ���
        public static List<AssignEnemy> AssignedEnemies = new List<AssignEnemy>();

        // �� �Ҵ��� �߰��ϴ� �Լ�
        public static void AddEnemyAssignment(AssignEnemy assignEnemy)
        {
            AssignedEnemies.Add(assignEnemy);
        }

        // �� �Ҵ��� �ʱ�ȭ�ϴ� �Լ�
        public static void ResetEnemyAssignment()
        {
            AssignedEnemies = new List<AssignEnemy>();
        }

        // �Ҵ� �� ���� ���� �������� �Լ�
        public static AssignEnemy GetAssignedEnemy(int assignNum)
        {
            foreach (AssignEnemy assignedEnemy in AssignedEnemies)
            {
                if (assignedEnemy.AssignNum == assignNum)
                {
                    return assignedEnemy;
                }
            }

            return null;
        }
    }

    // �Ҵ� �� Ŭ����
    public class AssignEnemy
    {
        // ��Ʋ �Ҵ� ��ȣ
        public int AssignNum { get; set; }



        // �� ������ȣ
        public int EnemyID { get; set; }

        //�� ��������Ʈ �̹��� ID (�̹��� id�� �⺻������ enemyId�� 10�� ���Ѱ��̸�, �ٸ� ������� �ٲܶ��� �ű⿡ ���� ���Ѵ�. ID�� 13�� ���� ����/ �⺻: 130, ��� 1: 131, ��� 2: 132...)
        public int EnemySpriteImageID { get; set; }

        // �� AI �� ����
        public int EnemyAI { get; set; }
        /*AI �� ��ȣ
          0: "Nothing" �ƹ��͵� ����
          1: "Dumb" ������ �ൿ
          2: "Agressive" ������ �� Ȯ���� ����
          3: "Defensive" ����ൿ�� �� Ȯ���� ����
          4: "Shy" �����ϸ� ���� �ൿ�� �� Ȯ���� ����
          5: "Smart" ������ �ൿ�� �ϴ� ���� HP���Ϸ� �������� ������� �ൿ���� �ٲٸ�, �Ź� ���� ȿ������ ������ �Ѵ�.
          6: "Agressive Extreme" ������ �ൿ�� �� Ȯ���� ������, �Ź� ���� ȿ������ ������ �Ѵ�.

        (�� ���� ��ȣ�� ���� AI ���̴�.)
        */

        // ���� HP ��
        public int CurrentHP { get; set; }

        // ���� AP ��
        public int CurrentAP { get; set; }



        // �ִ� ü�� �����
        public float MaxHPMultiplier { get; set; }

        // ���̽� AP �����
        public float BaseAPMultiplier { get; set; }

        // ���ݷ� �����
        public float AttackMultiplier { get; set; }

        // ���� �����
        public float DefenseMultiplier { get; set; }

        // Ư�� ���ݷ� �����
        public float SpecialAttackMultiplier { get; set; }

        // Ư�� ���� �����
        public float SpecialDefenseMultiplier { get; set; }

        // �ӵ� �����
        public float SpeedMultiplier { get; set; }

        // ��� �����
        public float LuckMultiplier { get; set; }

        // ȸ�ǵ� �����
        public float avoidanceMultiplier { get; set; }

        // ���߷� �����
        public float precisionMultiplier { get; set; }



        // ������
        public AssignEnemy(int assignNum, int enemyID, int maxHP, int baseAP)
        {
            //�� �⺻ ����
            this.AssignNum = assignNum;
            this.EnemyID = enemyID;
            this.EnemySpriteImageID = enemyID * 10;

            //���� HP, AP
            this.CurrentHP = maxHP;
            this.CurrentAP = baseAP;

            //�ɷ�ġ ����� (�⺻���� 1)
            this.MaxHPMultiplier = 1.0f;
            this.BaseAPMultiplier = 1.0f;
            this.AttackMultiplier = 1.0f;
            this.DefenseMultiplier = 1.0f;
            this.SpecialAttackMultiplier = 1.0f;
            this.SpecialDefenseMultiplier = 1.0f;
            this.SpeedMultiplier = 1.0f;
            this.LuckMultiplier = 1.0f;
            this.avoidanceMultiplier = 1.0f;
            this.precisionMultiplier = 1.0f;
        }
    }

    public bool keepEnemyMiddle;                 //�������� ����� �������� ���� 
    int enemyToKeepMiddle;                         //����� ������ ���� ���� ��ȣ (����: ����) *0: ����
                                                   //�߰� ����: �������� ��� �迭 �ϸ� formationType�� �������� ��� ��ġ �� �� �ִ� Ȧ���� �Ǹ�, �� ����� ������ ����� �迭�ϱ�� �� ������ �迭�ȴ�.

    //
    //�Ҵ� �� ���� ������Ʈ
    //

    public void UpdateAssignInfo()
    {

        //�� ���� ����
        enemyAmount = 0;
        for(int i = 0; i < 5; i++)
        {
            if (enemyPlaces[i] != 0) enemyAmount++;
        }

        //���� ��� ������ ���
        //TODO: ���� ��� ������ ����� ������ ���� �����̼� ���� �߾ӿ� ������ �ϴ� �ڵ带 �ۼ�

        //��� ������ �Ǿ� ������, �� ������ ���� ����ġ�� ���� ���� ��
        if (keepEnemyMiddle && enemyPlaces[2] != enemyToKeepMiddle)
        {

            //���� ��� ���� �� ���� ����
            int[] nonMidEnemies = new int[4] { 0, 0, 0, 0 };

            int nonMidEnemyAmount = 0;
            

            for(int i = 0; i < 5; i++)
            {
                if (enemyPlaces[i] != 0 && enemyPlaces[i] != enemyToKeepMiddle)
                {
                    nonMidEnemies[nonMidEnemyAmount++] = enemyPlaces[i];
                }
            }

            //�Ҵ� �� ������ �ʱ�ȭ
            enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };

            //��� ������ �� �Ҵ�
            enemyPlaces[2] = enemyToKeepMiddle;

            //nonMidEnemies���� ���� ���� ��Һ��� 1,3,0,4�� �ڸ������� �����Ѵ�. 
            if (nonMidEnemyAmount >= 1) enemyPlaces[1] = nonMidEnemies[0];
            if (nonMidEnemyAmount >= 2) enemyPlaces[3] = nonMidEnemies[1];
            if (nonMidEnemyAmount >= 3) enemyPlaces[0] = nonMidEnemies[2];
            if (nonMidEnemyAmount >= 4) enemyPlaces[4] = nonMidEnemies[3];

        }
    }

    //���� �� �Ҵ� ȣ�� �Լ�
    public void AddEnemyAssign(int enemyID)
    {

        //ù ���ڸ� ��ġ ��ȣ ���ϱ�
        int targetPlaceLocation = findEnemyLocation(0);

        if (targetPlaceLocation != -1) //���ڸ��� ����
        {

            //��� ������ ���� �ִٸ� 1,3,0,4 �켱 ������ �ڸ� �߰��ϱ�.
            if (keepEnemyMiddle)
            {
                bool isGotPlaceLocation = false;
                if (!isGotPlaceLocation && enemyPlaces[1] == 0)
                {
                    targetPlaceLocation = 1;
                    isGotPlaceLocation = true;
                }
                if (!isGotPlaceLocation && enemyPlaces[3] == 0)
                {
                    targetPlaceLocation = 3;
                    isGotPlaceLocation = true;
                }
                if (!isGotPlaceLocation && enemyPlaces[0] == 0)
                {
                    targetPlaceLocation = 0;
                    isGotPlaceLocation = true;
                }
                if (!isGotPlaceLocation && enemyPlaces[4] == 0)
                {
                    targetPlaceLocation = 4;
                    isGotPlaceLocation = true;
                }
            }

            //�� �Ҵ� �迭�� �� �Ҵ� ��ȣ ���� �߰�
            enemyPlaces[targetPlaceLocation] = nextAssignNum;

            //�߰� �Ϸ��� ���� ���� �˻�
            enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);

            //�Ҵ� �� ���� �߰��ϱ�
            AssignedEnemyStatus.AddEnemyAssignment(
                new AssignEnemy(nextAssignNum++, enemyID,
                enemyDB.GetComponent<EnemyData>().resultEnemyData.MaxHP,
                enemyDB.GetComponent<EnemyData>().resultEnemyData.BaseAP));
            
            //�뿭 ���� ��ȣ�� 4���� �۰�, ���� �뿭 ���Ŀ��� ������ �� ������ �ʰ� �� �� �뿭 ���� ����
            if(formationTypeNum < 4 && formationTypeNum <= enemyAmount - 1)
            {
                ++formationTypeNum;
            } 
        }
        else //���ڸ��� ����
        {
            Debug.LogError("���� ���̻� �߰� �� �� �����ϴ�.");
        }

        //�� ǥ�ñ� ������Ʈ
        UpdateDisplayerCodeSet();
    }

    //�Ҵ� �� ���� �Լ�
    public void RemoveEnemyAssign(int targetAssignNum)
    {
        //���� ��ǥ ��ġ ��ȣ ���ϱ�
        int targetLocation = findEnemyLocation(targetAssignNum);

        if(targetLocation != -1) //�����Ϸ��� ���� ����
        {
            enemyPlaces[targetLocation] = 0;

            //���� Ÿ���� ����� ������ �� �̶��
            if(targetAssignNum == enemyToKeepMiddle)
            {
                enemyToKeepMiddle = 0;
                keepEnemyMiddle = false;
            }
        }

        //�� ǥ�ñ� ������Ʈ
        UpdateDisplayerCodeSet();
    }

    //�Ҵ� �� �ʱ�ȭ ȣ�� �Լ�
    public void ResetEnemyAssign()
    {
        formationTypeNum = -1;
        enemyAmount = 0;
        nextAssignNum = 1;
        enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //���� �� �˻�
    public void SetSearchEnemyAssign(int targetAssignNum)
    {
        resultEnemyStatus = AssignedEnemyStatus.GetAssignedEnemy(targetAssignNum);
    }

    //
    //<�� ǥ�� ���� ����>
    //

    public GameObject[] enemyDisplayObject = new GameObject[5];     //�� ǥ�� ������Ʈ

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 450, 450, 450, 450 }, { -90, 90, 450, 450, 450 }, { -180, 0, 180, 450, 450 }, { -225, -75, 75, 225, 450 }, { -252, -126, 0, 126, 252 } };
    //�� ��ġ X ��ǥ

    public int formationTypeNum = -1;                                   //�뿭 ����
    /*�뿭 ����
     0: 1�� ��� �迭     ��
     1: 2�� ��� �迭    �� ��
     2: 3�� ��� �迭   �� �� ��
     3: 4�� ��� �迭  �� �� �� ��
     4: 5�� ��� �迭 �� �� �� �� ��
     */


    //�� ǥ�ñ� �̹��� ������Ʈ
    public void UpdateDisplayerImage()
    {

        for (int i = 0; i < enemyDisplayObject.Length; i++)
        {
            if (enemyPlaces[i] == 0)
            {
                //Hide Enemy
                enemyDisplayObject[i].SetActive(false);
            }
            else
            {
                //Show Enemy
                enemyDisplayObject[i].SetActive(true);

                //i��° �� ǥ�ñ��� ��������Ʈ ������
                SpriteRenderer enemyDisplaySpriteRenderer = enemyDisplayObject[i].GetComponent<SpriteRenderer>();

                //i��° ���� ��������Ʈ ID
                SetSearchEnemyAssign(enemyPlaces[i]);
                int targetSpriteID = resultEnemyStatus.EnemySpriteImageID;

                //i��° ���� ��������Ʈ �̹���
                Sprite targetSpriteData = enemyDB.GetComponent<EnemyData>().imageArray[targetSpriteID];

                //��ǥ�ñ� ��������Ʈ ����
                enemyDisplaySpriteRenderer.sprite = targetSpriteData;

            }
        }
    }

    public void MoveEnemyToItsPlace()
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

            enemyDisplayObject[i].transform.position = targetPosition;
        }
    }

    //�� �Ҵ簪, ���÷��� �̹��� ������Ʈ, �� ������ �ڵ��
    public void UpdateDisplayerCodeSet()
    {
        UpdateAssignInfo();
        MoveEnemyToItsPlace();
        UpdateDisplayerImage();
    }


    //
    //<�� ��>
    //

    public void WakeEnemyManager(int[] enemyIDs, bool keepFirstEnemyMiddle)
    {
        ResetEnemyAssign();
        keepEnemyMiddle = false;
        enemyToKeepMiddle = 0;
        enemyAmount = 0;
        formationTypeNum = -1;

        for(int i = 0; i < enemyIDs.Length; i++)
        {
            AddEnemyAssign(enemyIDs[i]);
        }

        if (keepFirstEnemyMiddle)
        {
            keepEnemyMiddle = true;
            enemyToKeepMiddle = enemyPlaces[0];
            formationTypeNum = 4;
        }

        UpdateDisplayerCodeSet();
    }

    int findEnemyLocation(int enemyNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (enemyPlaces[i] == enemyNum)
            {
                return i;
            }
        }
        return -1;
    }

    void Start()
    {
        nextAssignNum = 1;
        ResetEnemyAssign();
    }

    public bool TEST_TRIGGER_wakeup = false;
    public bool TEST_TRIGGER_add1 = false;
    public bool TEST_TRIGGER_add2 = false;


    public int TEST_removeTargetPosition = 0;
    public bool TEST_TRIGGER_remove = false;

    private void Update()
    {
        if (TEST_TRIGGER_wakeup)
        {
            TEST_TRIGGER_wakeup = false;
            WakeEnemyManager(new int[] {1, 2, 2, 2, 1}, false);
        }
        if (TEST_TRIGGER_add1)
        {
            TEST_TRIGGER_add1 = false;
            AddEnemyAssign(1);
        }
        if (TEST_TRIGGER_add2)
        {
            TEST_TRIGGER_add2 = false;
            AddEnemyAssign(2);
        }
        if (TEST_TRIGGER_remove)
        {
            TEST_TRIGGER_remove = false;
            RemoveEnemyAssign(enemyPlaces[TEST_removeTargetPosition]);
        }
    }
}
