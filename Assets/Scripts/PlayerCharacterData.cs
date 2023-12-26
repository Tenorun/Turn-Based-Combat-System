using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterData : MonoBehaviour
{
    public class PlayerCharacterDataBase
    {
        public string name;

        public int maxHp;
        public int curHp;

        public int maxSp;
        public int curSp;

        public int attack;
        public int defense;

        public int special;

        public int specialAttack;
        public int specialDefense;
        public float specialSuitability;

        public int speed;
        public int luck;

        public int[] inventory = new int[25];
        public int Level = 0;
        public int Exp = 0;

        public PlayerCharacterDataBase(string name, int maxHp, int curHp, int maxSp, int curSp, int attack, int defense, int special, float specialSuitability, int speed, int luck)
        {
            this.name = name;
            this.maxHp = maxHp;
            this.curHp = curHp;
            this.maxSp = maxSp;
            this.curSp = curSp;
            this.attack = attack;
            this.defense = defense;
            this.special = special;
            this.specialSuitability = specialSuitability;
            this.speed = speed;
            this.luck = luck;

            //특공 특방 값 계산
            this.specialAttack = Mathf.RoundToInt((special * 2) * (attack / (attack + defense)) * specialSuitability);
            this.specialDefense = Mathf.RoundToInt((special * 2) * (defense / (attack + defense)) * specialSuitability);
        }
    }



    // Start is called before the first frame update
    void Start()
    {
        PlayerCharacterData[] playerCharacters = new PlayerCharacterData[2];

        playerCharacters[0] = new PlayerCharacterData(
            ""
            );
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
