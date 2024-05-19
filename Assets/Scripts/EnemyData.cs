using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Enemy enemy;
    // ������ �����ͺ��̽� Ŭ����
    public class SkillDatabase
    {
        // ������ ���
        public static List<Enemy> enemies = new List<Enemy>();

        // �������� �߰��ϴ� �Լ�
        public static void AddSkill(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        // �������� �������� �Լ�
        public static Enemy GetEnemy(int enemyId)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.EnemyId == enemyId)
                {
                    return enemy;
                }
            }

            return null;
        }
    }

    // ��ų Ŭ����
    public class Enemy
    {
        // �� ������ȣ
        public int EnemyId { get; set; }

        // �� �̸�
        public string[] EnemyName { get; set; }

        // �� ����
        public int EnemyType { get; set; }
        /* �� ���� ��ȣ
          1: �Ϲ�
          2: ����
          3: Ư��
         */

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

        // �ִ� HP ��
        public int MaxHP { get; set; }

        // �⺻ AP ��
        public int DefaultAP { get; set; }

        // ���ݷ�
        public int Attack { get; set; }

        // ����
        public int Defense { get; set; }

        // Ư�� ���ݷ�
        public int SpecialAttack { get; set; }

        // Ư�� ����
        public int SpecialDefense { get; set; }

        // �ӵ�
        public int Speed { get; set; }

        // ���
        public int Luck { get; set; }




        //�ൿ �ȷ�Ʈ

        // ���� �ൿ �ȷ�Ʈ
        public int[] AtkPalette { get; set; }

        // ��� �ൿ �ȷ�Ʈ
        public int[] DefPalette { get; set; }

        // ���� �ൿ �ȷ�Ʈ
        public int[] BuffPalette { get; set; }

        // ����� �ൿ �ȷ�Ʈ
        public int[] DebuffPalette { get; set; }

        // ���� �̻� ��ų �ȷ�Ʈ
        public int[] StatusEffectPalette { get; set; }

        // ��Ÿ ��ų �ȷ�Ʈ
        public int[] EtcSkillPalette { get; set; }

        // TODO: �� ��������Ʈ ������ ����

        // ������
        public Enemy(int enemyId, string[] enemyName, int enemyType, int enemyAI, int maxHP, int defaultAP, int attack, int defense, int spAtk, int spDef, int speed, int luck)
        {
            this.EnemyId = enemyId;
            this.EnemyName = enemyName;
            this.EnemyType = enemyType;
            this.EnemyAI = enemyAI;
            this.MaxHP = maxHP;
            this.DefaultAP = defaultAP;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = spAtk;
            this.SpecialDefense = spDef;
            this.Speed = speed;
            this.Luck = luck;
            //TODO: �� ������ �ޱ� ���� ����
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //���� �� �˻�
    public void SetSearchSkill(int skillID)
    {
        enemy = SkillDatabase.GetEnemy(skillID);
    }
}
