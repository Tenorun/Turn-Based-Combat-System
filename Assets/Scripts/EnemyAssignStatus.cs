using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAssignStatus : MonoBehaviour
{
    //�� ������ ���̽�
    public GameObject enemyDB;

    public AssignEnemy enemy;

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
        public static AssignEnemy GetAssignedEnemy(int enemyId)
        {
            foreach (AssignEnemy assignedEnemy in AssignedEnemies)
            {
                if (assignedEnemy.EnemyID == enemyId)
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
        public AssignEnemy(int assignNum, int enemyID, int enemyAI, int maxHP, int baseAP)
        {
            //�� �⺻ ����
            this.AssignNum = assignNum;
            this.EnemyID = enemyID;
            this.EnemySpriteImageID = enemyID * 10;
            this.EnemyAI = enemyAI;

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

    //���� �Ҵ� ��ȣ
    int nextAssignNum = 1;

    //���� ������ �� �Ҵ� ����
    public int[] assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };

    void Start()
    {
        nextAssignNum = 1;
    }

    //���� �� �Ҵ� ȣ�� �Լ�
    public void AddEnemyAssign(int enemyID, int enemyAI)
    {
        enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);
        AssignedEnemyStatus.AddEnemyAssignment(new AssignEnemy(nextAssignNum++, enemyID, enemyAI, enemyDB.GetComponent<EnemyData>().enemy.MaxHP, enemyDB.GetComponent<EnemyData>().enemy.BaseAP));
    }

    //�Ҵ� �� �ʱ�ȭ ȣ�� �Լ�
    public void ResetEnemyAssign()
    {
        nextAssignNum = 1;
        assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //���� �� �˻�
    public void SetSearchEnemy(int enemyID)
    {
        enemy = AssignedEnemyStatus.GetAssignedEnemy(enemyID);
    }



    //TODO: EnemyFormationManager�� �����Ұ�.
}
