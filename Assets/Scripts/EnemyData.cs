using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Enemy resultEnemyData;
    // �� �����ͺ��̽� Ŭ����
    public class EnemyDatabase
    {
        // �� ���
        public static List<Enemy> enemies = new List<Enemy>();

        // ���� �߰��ϴ� �Լ�
        public static void AddEnemy(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        // ���� �������� �Լ�
        public static Enemy GetEnemy(int enemyId)
        {
            foreach (Enemy enemy in enemies)
            {
                if (enemy.EnemyID == enemyId)
                {
                    return enemy;
                }
            }

            return null;
        }
    }

    // �� Ŭ����
    public class Enemy
    {
        // �� ������ȣ
        public int EnemyID { get; set; }

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
        public int BaseAP { get; set; }

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



        //�÷��̾� ����

        //���� ����ġ
        public int RewardExp;

        //���� ��
        public int RewardMoney;

        //��� ������
        public int[] RewardItems { get; set; }

        //������ ��� Ȯ��
        public float ItemDropPossibility { get; set; }

        //������ ��� ����
        public int[] DropRatio { get; set; }

        // TODO: �� ��������Ʈ ������ ����

        // ������
        public Enemy(int enemyId, string[] enemyName, int enemyType, int enemyAI, 
            int maxHP, int baseAP, int attack, int defense, int spAtk, int spDef, int speed, int luck, 
            int[] atkPal, int[] defPal, int[] buffPal, int[] debuffPal, int[] effectPal, int[] etcPal,
            int rewardExp, int rewardMoney, int[] rewardItems, float itemDropPossibility, int[] dropRatio)
        {
            this.EnemyID = enemyId;
            this.EnemyName = enemyName;
            this.EnemyType = enemyType;
            this.EnemyAI = enemyAI;

            this.MaxHP = maxHP;
            this.BaseAP = baseAP;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = spAtk;
            this.SpecialDefense = spDef;
            this.Speed = speed;
            this.Luck = luck;

            this.AtkPalette = atkPal;
            this.DefPalette = defPal;
            this.BuffPalette = buffPal;
            this.DebuffPalette = debuffPal;
            this.StatusEffectPalette = effectPal;
            this.EtcSkillPalette = etcPal;

            this.RewardExp = rewardExp;
            this.RewardMoney = rewardMoney;
            this.RewardItems = rewardItems;
            this.ItemDropPossibility = itemDropPossibility;
            this.DropRatio = dropRatio;
        }
    }

    void Start()
    {
        //���Ͽ��� �� �̹��� ����
        LoadImages();


        // **����1** //
        EnemyDatabase.AddEnemy(new Enemy(
            1,      //�� ID
            new string[] { "����", "Dummie" },     //�� �̸�
            1,      //�� ����
            1,      //�� AI
            100,    //�ִ� HP
            100,    //���̽� AP
            20,     //���ݷ�
            20,     //����
            20,     //Ư��
            20,     //Ư��
            100,    //���ǵ�
            100,    //���
            new int[] { 1 },  //���� �ൿ �ȷ�Ʈ
            new int[] { 2, 3 },//��� �ൿ �ȷ�Ʈ
            new int[] { },  //���� �ൿ �ȷ�Ʈ
            new int[] { 10 }, //����� �ൿ �ȷ�Ʈ
            new int[] { 11 }, //�����̻� �ൿ �ȷ�Ʈ
            new int[] { },  //��Ÿ �ൿ �ȷ�Ʈ
            30,             //���� ����ġ
            100,            //���� ��
            new int[] { 2, 3 },//���� ������
            0.33f,          //������ ��ü�� ��� ���ɼ�
            new int[] { 10, 1 }//������ ��� ����
            ));


        // **����2** //
        EnemyDatabase.AddEnemy(new Enemy(
            2,      //�� ID
            new string[] { "���� ������", "Mr.Thunder Cloud" },     //�� �̸�
            1,      //�� ����
            1,      //�� AI
            100,    //�ִ� HP
            100,    //���̽� AP
            20,     //���ݷ�
            20,     //����
            20,     //Ư��
            20,     //Ư��
            100,    //���ǵ�
            100,    //���
            new int[] { 1 },  //���� �ൿ �ȷ�Ʈ
            new int[] { 2, 3 },//��� �ൿ �ȷ�Ʈ
            new int[] { },  //���� �ൿ �ȷ�Ʈ
            new int[] { 10 }, //����� �ൿ �ȷ�Ʈ
            new int[] { 11 }, //�����̻� �ൿ �ȷ�Ʈ
            new int[] { },  //��Ÿ �ൿ �ȷ�Ʈ
            30,             //���� ����ġ
            100,            //���� ��
            new int[] { 2, 3 },//���� ������
            0.33f,          //������ ��ü�� ��� ���ɼ�
            new int[] { 10, 1 }//������ ��� ����
            ));

    }


    //�� �̹���
    public string relativeFolderPath; // ��� ��η� ���� ��� ����
    public Sprite[] imageArray; // �̹����� ������ �迭

    void LoadImages()
    {
        string folderPath = Path.Combine(Application.dataPath, relativeFolderPath); // ��� ��θ� ���� ��η� ��ȯ

        string[] files = Directory.GetFiles(folderPath, "*.png"); // ���� ���� ��� PNG ���� ��������

        imageArray = new Sprite[(files.Length + 1) * 10]; // �̹��� �迭 �ʱ�ȭ

        foreach (string file in files)
        {
            string fileName = Path.GetFileNameWithoutExtension(file); // ���� �̸� (Ȯ���� ����)

            // ���� �̸����� ���� ����
            string[] splitName = fileName.Split('-');
            if (splitName.Length > 1)
            {
                int index;
                if (int.TryParse(splitName[0], out index)) // ���ڷ� ��ȯ
                {
                    // �̹��� �ε�
                    Texture2D texture = LoadTextureFromFile(file);
                    if (texture != null)
                    {
                        // Texture2D�� Sprite�� ��ȯ�ϸ鼭 FilterMode�� None���� ����
                        Sprite sprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), Vector2.one * 0.5f, 100, 0, SpriteMeshType.FullRect, new Vector4(0, 0, texture.width, texture.height), false);
                        sprite.texture.filterMode = FilterMode.Point; // FilterMode�� None���� ����

                        // �̹��� �迭�� �Ҵ�
                        imageArray[index] = sprite;
                    }
                }
            }
        }
    }

    Texture2D LoadTextureFromFile(string filePath)
    {
        byte[] fileData = File.ReadAllBytes(filePath);
        Texture2D texture = new Texture2D(2, 2);
        texture.LoadImage(fileData); // ���� �����ͷκ��� �ؽ�ó �ε�
        return texture;
    }


    //���� �� �˻�
    public void SetSearchEnemy(int enemyID)
    {
        resultEnemyData = EnemyDatabase.GetEnemy(enemyID);
    }
}
