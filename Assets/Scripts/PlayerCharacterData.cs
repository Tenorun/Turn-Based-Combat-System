using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterData : MonoBehaviour
{
    // �÷��̾� ĳ���� �����ͺ��̽� Ŭ����
    public class PlayerCharacterDatabase
    {
        // �÷��̾� ĳ���� ���
        public static List<PlayerCharacter> Players = new List<PlayerCharacter>();

        // �÷��̾� ĳ���͸� �߰��ϴ� �Լ�
        public static void AddPlayer(PlayerCharacter player)
        {
            Players.Add(player);
        }

        // �÷��̾� ĳ���͸� �������� �Լ�
        public static PlayerCharacter GetPlayer(int CharacterId)
        {
            foreach (PlayerCharacter player in Players)
            {
                if (player.CharacterId == CharacterId)
                {
                    return player;
                }
            }

            return null;
        }
    }

    // �÷��̾� ĳ���� Ŭ����
    public class PlayerCharacter
    {
        // �÷��̾� ĳ���� ������ȣ
        public int CharacterId { get; set; }

        // ĳ���� �̸�
        public string[] name { get; set; }

        // �ִ� HP
        public int MaxHp { get; set; }

        // ���� HP
        public int CurrentHp { get; set; }

        // �ִ� SP
        public int MaxSp { get; set; }

        // ���� SP
        public int CurrentSp { get; set; }

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

        // �κ��丮
        public int[] Inventory { get; set; }

        // ����
        public int Weapon { get; set; }

        // ��
        public int Armor { get; set; }

        // ��ű�
        public int Accessory { get; set; }

        // ����
        public int Level { get; set; }

        // ����ġ
        public int Experience { get; set; }

        // ���� ������
        public int[] GrowthPotential { get; set; }

        // ������
        public PlayerCharacter(int CharacterId, string[] name, int maxHp, int maxSp, int attack, int defense, int specialAttack, int specialDefense, int speed, int luck, int weapon, int armor, int accessory, int level, int experience, int[] growthPotential)
        {
            this.CharacterId = CharacterId;
            this.name = name;
            this.MaxHp = maxHp;
            this.CurrentHp = maxHp;
            this.MaxSp = maxSp;
            this.CurrentSp = maxSp;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = specialAttack;
            this.SpecialDefense = specialDefense;
            this.Speed = speed;
            this.Luck = luck;
            this.Inventory = new int[25];
            this.Weapon = weapon;
            this.Armor = armor;
            this.Accessory = accessory;
            this.Level = level;
            this.Experience = experience;
            this.GrowthPotential = growthPotential;
        }
    }

    void Start()
    {
        PlayerCharacterDatabase.AddPlayer(new PlayerCharacter(
            1,
            new string[] {"���� ȭ��ƮȦ", "Bend Whitehall"},
            40,     //�ִ� HP
            25,     //�ִ� SP
            11,     //���ݷ�
            14,     //����
            15,     //Ư��
            15,     //Ư��
            12,     //�ӵ�
            13,     //���
            -1,      //����(�ӽ÷� -1�� ����)
            -2,      //��(�ӽ÷� -2�� ����)
            -3,      //��ű�(�ӽ÷� -3���� ����)
            1,      //����
            0,       //����ġ
            new int[] { 7, 7, 3, 5, 6, 6, 4, 4 }    //���� ����
            ));
    }

}
