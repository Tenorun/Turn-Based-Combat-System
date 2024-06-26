using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacterData : MonoBehaviour
{
    public PlayerCharacter resultCharacterData;

    // 플레이어 캐릭터 데이터베이스 클래스
    public class PlayerCharacterDatabase
    {
        // 플레이어 캐릭터 목록
        public static List<PlayerCharacter> Players = new List<PlayerCharacter>();

        // 플레이어 캐릭터를 추가하는 함수
        public static void AddPlayer(PlayerCharacter player)
        {
            Players.Add(player);
        }

        // 플레이어 캐릭터를 가져오는 함수
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

    // 플레이어 캐릭터 클래스
    public class PlayerCharacter
    {
        // 플레이어 캐릭터 고유번호
        public int CharacterId { get; set; }

        // 캐릭터 이름
        public string[] CharacterName { get; set; }

        // 최대 HP
        public int MaxHp { get; set; }

        // 현재 HP
        public int CurrentHp { get; set; }

        // 최대 SP
        public int MaxSp { get; set; }

        // 현재 SP
        public int CurrentSp { get; set; }

        // 상태 이상
        public int CurrentEffect {  get; set; }

        // 공격력
        public int Attack { get; set; }

        // 방어력
        public int Defense { get; set; }

        // 특수 공격력
        public int SpecialAttack { get; set; }

        // 특수 방어력
        public int SpecialDefense { get; set; }

        // 속도
        public int Speed { get; set; }

        // 행운
        public int Luck { get; set; }

        // 인벤토리
        public List<int> Inventory { get; set; }

        // 무기
        public int Weapon { get; set; }

        // 방어구
        public int Armor { get; set; }

        // 장신구
        public int Accessory { get; set; }

        // 스킬 슬롯
        public int[] SkillSlot = new int[4];

        // 레벨
        public int Level { get; set; }

        // 경험치
        public int Experience { get; set; }

        // 성장 적성도
        public int[] GrowthPotential { get; set; }

        // 생성자
        public PlayerCharacter(int characterId, string[] characterName, int maxHp, int maxSp, int attack, int defense, int specialAttack, int specialDefense, int speed, int luck, int weapon, int armor, int accessory, int[] skillSlot, int level, int experience, int[] growthPotential)
        {
            this.CharacterId = characterId;
            this.CharacterName = characterName;

            this.MaxHp = maxHp;
            this.CurrentHp = maxHp;
            this.MaxSp = maxSp;
            this.CurrentSp = maxSp;
            this.CurrentEffect = 0;
            this.Attack = attack;
            this.Defense = defense;
            this.SpecialAttack = specialAttack;
            this.SpecialDefense = specialDefense;
            this.Speed = speed;
            this.Luck = luck;

            this.Inventory = new List<int>();
            this.Weapon = weapon;
            this.Armor = armor;
            this.Accessory = accessory;
            this.SkillSlot = skillSlot;
            this.Level = level;
            this.Experience = experience;

            this.GrowthPotential = growthPotential;
        }
    }

    public void SetSearchCharacter(int characterID)
    {
        resultCharacterData = PlayerCharacterDatabase.GetPlayer(characterID);
    }

    void Start()
    {
        PlayerCharacterDatabase.AddPlayer(new PlayerCharacter(
            1,      //캐릭터 ID
            new string[] {"벤드 화이트홀", "Bend Whitehall"},
            45,     //최대 HP
            45,     //최대 SP
            11,     //공격력
            14,     //방어력
            15,     //특공
            15,     //특방
            12,     //속도
            13,     //행운
            -1,      //무기(임시로 -1로 설정)
            -2,      //방어구(임시로 -2로 설정)
            -3,      //장신구(임시로 -3으로 설정)
            new int[4], //스킬 슬롯
            1,      //레벨
            0,       //경혐치
            new int[] { 7, 7, 3, 5, 6, 6, 4, 4 }    //성장 적성
            ));

        //아래의 코드와 같이 GetPlayer(n)에 캐릭터 ID를 넣으면 됨
        //character = PlayerCharacterDatabase.GetPlayer(1);
        //Debug.Log(character.CharacterName[0]);
        //위 경우 아이디가 1인 캐릭터의 한국어 이름을 출력한다.
        

        //테스트 인벤토리
        resultCharacterData = PlayerCharacterDatabase.GetPlayer(1);

        resultCharacterData.Inventory.Add(1);
        resultCharacterData.Inventory.Add(2);
        resultCharacterData.Inventory.Add(3);
        resultCharacterData.Inventory.Add(4);
        resultCharacterData.Inventory.Add(5);
        resultCharacterData.Inventory.Add(4);
        resultCharacterData.Inventory.Add(2);
        resultCharacterData.Inventory.Add(2);
        resultCharacterData.Inventory.Add(1);
        resultCharacterData.Inventory.Add(5);
        resultCharacterData.Inventory.Add(2);
        resultCharacterData.Inventory.Add(2);

        resultCharacterData.SkillSlot = new int[4] { 14, 3, 15, 0 };





        PlayerCharacterDatabase.AddPlayer(new PlayerCharacter(
            2,      //캐릭터 ID
            new string[] { "애니 에놀라", "Annie Enola" },
            30,     //최대 HP
            60,     //최대 SP
            11,     //공격력
            14,     //방어력
            15,     //특공
            15,     //특방
            12,     //속도
            13,     //행운
            -1,      //무기(임시로 -1로 설정)
            -2,      //방어구(임시로 -2로 설정)
            -3,      //장신구(임시로 -3으로 설정)
            new int[4], //스킬 슬롯
            1,      //레벨
            0,       //경혐치
            new int[] { 7, 7, 3, 5, 6, 6, 4, 4 }    //성장 적성
            ));
        resultCharacterData = PlayerCharacterDatabase.GetPlayer(2);
        resultCharacterData.Inventory.Add(3);
        resultCharacterData.Inventory.Add(3);
        resultCharacterData.Inventory.Add(3);
    }
}
