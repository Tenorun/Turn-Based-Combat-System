using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyManager : MonoBehaviour
{
    //
    //<�� �Ҵ� ���� ����>
    //

    //���� �Ҵ� ��ȣ
    int nextAssignNum = 1;

    //���� ������ �� �Ҵ� ����
    public int[] assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };

    int enemyAmount = 0;                                    //�� ����


    //�� ������ ���̽�
    public GameObject enemyDB;

    public AssignEnemy enemyStatus;

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

    public bool keepSomeOneMiddle;                 //�������� ����� �������� ���� 
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
            if (assignedEnemies[i] != 0) enemyAmount++;
        }

        //������ ���� 0���� ������
        int fixedEnemyLocation = findEnemyLocation(enemyToKeepMiddle);
        if(fixedEnemyLocation != -1)
        {
            assignedEnemies[fixedEnemyLocation] = 0;
        }

        //�Ҵ����� �迭�� 0�� ��� ��������
        int index = 0;
        for(int i = 0; i < 5; i++)
        {
            if (assignedEnemies[i] != 0)
            {
                assignedEnemies[index++] = assignedEnemies[i];
            }
        }
        //���� ������ ��� 0����
        while (index < 5)
        {
            assignedEnemies[index++] = 0;
        }

        //formationTypeNum ���ϱ�
        formationTypeNum = enemyAmount - 1;

        //���� ��� ������ ���
        if (keepSomeOneMiddle)
        {
            //�� ������ ¦���϶�, �����̼� Ÿ���� Ȧ���� �ϱ�.
            //��) 2��: �����̼� Ÿ�� = 2(3�� �迭)/4��: �����̼� Ÿ�� = 4(5�� �迭)
            if (enemyAmount % 2 == 0)
            {
                formationTypeNum = enemyAmount;
            }

            int middleLocation = formationTypeNum / 2;

            //�ڿ������� ������� �ϳ��� ���������� �б�.(����� ���Ե� ���� ������ 0���� ��� ���������� �зȱ� ������, �Ҵ�Ȱ��� �������� �� �ʿ�� ����.)
            for (int i = formationTypeNum; i > middleLocation; i--)
            {
                assignedEnemies[i] = assignedEnemies[i - 1];
            }
            //����� ������ ���� ����
            assignedEnemies[middleLocation] = enemyToKeepMiddle;
        }
    }

    //���� ��� �����ϱ�
    public void AssignMiddleEnemy(int targetPosition)
    {
        keepSomeOneMiddle = true;
        enemyToKeepMiddle = assignedEnemies[targetPosition];
    }

    //���� �� �Ҵ� ȣ�� �Լ�
    public void AddEnemyAssign(int enemyID)
    {

        //ù ���ڸ� ��ġ ��ȣ ���ϱ�
        int emptyPlaceLocation = findEnemyLocation(0);

        if (emptyPlaceLocation != -1) //���ڸ��� ����
        {
            //�Ҵ� ������ ���ڸ��� �� �Ҵ� ��ȣ�� �߰��ϱ�
            assignedEnemies[emptyPlaceLocation] = nextAssignNum;

            //�߰� �Ϸ��� ���� ���� �˻�
            enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);

            //�Ҵ� �� ���� �߰��ϱ�
            AssignedEnemyStatus.AddEnemyAssignment(
                new AssignEnemy(nextAssignNum, enemyID,
                enemyDB.GetComponent<EnemyData>().enemy.MaxHP,
                enemyDB.GetComponent<EnemyData>().enemy.BaseAP));

            //�� �Ҵ� ��ȣ 1 ����
            ++nextAssignNum;
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
            assignedEnemies[targetLocation] = 0;

            //���� Ÿ���� ����� ������ �� �̶��
            if(targetAssignNum == enemyToKeepMiddle)
            {
                enemyToKeepMiddle = 0;
                keepSomeOneMiddle = false;
            }
        }

        //�� ǥ�ñ� ������Ʈ
        UpdateDisplayerCodeSet();
    }

    //�Ҵ� �� �ʱ�ȭ ȣ�� �Լ�
    public void ResetEnemyAssign()
    {
        nextAssignNum = 1;
        assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //���� �� �˻�
    public void SetSearchEnemyAssign(int targetAssignNum)
    {
        enemyStatus = AssignedEnemyStatus.GetAssignedEnemy(targetAssignNum);
    }

    //
    //<�� ǥ�� ���� ����>
    //

    public GameObject[] enemyDisplayObject = new GameObject[5];     //�� ǥ�� ������Ʈ

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 450, 450, 450, 450 }, { -90, 90, 450, 450, 450 }, { -180, 0, 180, 450, 450 }, { -225, -75, 75, 225, 450 }, { -252, -126, 0, 126, 252 } };
    //�� ��ġ X ��ǥ

    public int formationTypeNum;                                   //�뿭 ����
    /*�뿭 ����
     1: 1�� ��� �迭     ��
     2: 2�� ��� �迭    �� ��
     3: 3�� ��� �迭   �� �� ��
     4: 4�� ��� �迭  �� �� �� ��
     5: 5�� ��� �迭 �� �� �� �� ��
     */


    //�� ǥ�ñ� �̹��� ������Ʈ
    public void UpdateDisplayerImage()
    {

        for (int i = 0; i < enemyDisplayObject.Length; i++)
        {
            if (assignedEnemies[i] == 0)
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
                SetSearchEnemyAssign(assignedEnemies[i]);
                int targetSpriteID = enemyStatus.EnemySpriteImageID;

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

            // Start coroutine for smooth movement
            StartCoroutine(MoveEnemyCoroutine(enemyDisplayObject[i].transform, targetPosition));
        }
    }

    // Coroutine for smooth movement of enemy display object
    IEnumerator MoveEnemyCoroutine(Transform enemyTransform, Vector3 targetPosition)
    {
        const float MOVING_SPEED = 0.2f;

        float elapsedTime = 0f;
        Vector3 startingPosition = enemyTransform.position;

        // Move towards the target position over time
        while (elapsedTime < MOVING_SPEED) // Moving in `MOVING_SPEED` seconds
        {
            // Calculate the interpolation ratio
            float t = elapsedTime / MOVING_SPEED; // `MOVING_SPEED` seconds duration

            // Smoothly move towards the target position using Lerp
            enemyTransform.position = Vector3.Lerp(startingPosition, targetPosition, t);

            // Increment elapsed time
            elapsedTime += Time.deltaTime;

            yield return null; // Wait for the next frame
        }

        // Ensure final position is precisely the target position
        enemyTransform.position = targetPosition;
    }


    //�� �Ҵ簪, ���÷��� �̹��� ������Ʈ, �� ������ �ڵ��
    public void UpdateDisplayerCodeSet()
    {
        UpdateAssignInfo();
        MoveEnemyToItsPlace();
        UpdateDisplayerImage();
    }

    //TODO: �� ���� ����

    //
    //<�� ��>
    //

    public void WakeEnemyManager(int[] enemyIDs, bool keepFirstEnemyMiddle)
    {
        ResetEnemyAssign();
        keepSomeOneMiddle = false;
        enemyToKeepMiddle = 0;
        enemyAmount = 0;
        formationTypeNum = 0;

        for(int i=0; i < enemyIDs.Length; i++)
        {
            AddEnemyAssign(enemyIDs[i]);
        }

        if (keepFirstEnemyMiddle)
        {
            AssignMiddleEnemy(0);
        }

        UpdateDisplayerCodeSet();
    }

    int findEnemyLocation(int enemyNum)
    {
        for (int i = 0; i < 5; i++)
        {
            if (assignedEnemies[i] == enemyNum)
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
    public bool TEST_TRIGGER_remove = false;

    private void Update()
    {
        if (TEST_TRIGGER_wakeup)
        {
            TEST_TRIGGER_wakeup = false;
            WakeEnemyManager(new int[] {1, 2 }, false);
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
            RemoveEnemyAssign(assignedEnemies[0]);
        }
    }

    //TODO: �����Ҷ� �̹����� �켱������ �ٲ�� ���� �ٸ� ���� ���� ������ ����ϰ� �������°��� ��ġ��
}
