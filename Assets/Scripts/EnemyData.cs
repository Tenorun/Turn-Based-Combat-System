using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Enemy enemy;
    // 아이템 데이터베이스 클래스
    public class SkillDatabase
    {
        // 아이템 목록
        public static List<Enemy> enemies = new List<Enemy>();

        // 아이템을 추가하는 함수
        public static void AddSkill(Enemy enemy)
        {
            enemies.Add(enemy);
        }

        // 아이템을 가져오는 함수
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

    // 스킬 클래스
    public class Enemy
    {
        // 적 고유번호
        public int EnemyId { get; set; }

        // 적 이름
        public string[] EnemyName { get; set; }

        // 적 유형
        public int EnemyType { get; set; }
        /* 적 유형 번호
          1: 일반
          2: 보스
          3: 특수
         */

        // 적 AI 모델 유형
        public int EnemyAI { get; set; }
        /*AI 모델 번호
          0: "Nothing" 아무것도 안함
          1: "Dumb" 무작위 행동
          2: "Agressive" 공격을 할 확률이 높음
          3: "Defensive" 방어행동을 할 확률이 높음
          4: "Shy" 가능하면 도주 행동을 할 확률이 높음
          5: "Smart" 공격적 행동을 하다 일정 HP이하로 떨어지면 방어적인 행동으로 바꾸며, 매번 가장 효과적인 공격을 한다.
          6: "Agressive Extreme" 공격적 행동을 할 확률이 높으며, 매번 가장 효과적인 공격을 한다.

        (그 외의 번호는 고유 AI 모델이다.)
        */

        // 최대 HP 값
        public int MaxHP { get; set; }

        // 기본 AP 값
        public int DefaultAP { get; set; }

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




        //행동 팔레트

        // 공격 행동 팔레트
        public int[] AtkPalette { get; set; }

        // 방어 행동 팔레트
        public int[] DefPalette { get; set; }

        // 버프 행동 팔레트
        public int[] BuffPalette { get; set; }

        // 디버프 행동 팔레트
        public int[] DebuffPalette { get; set; }

        // 상태 이상 스킬 팔레트
        public int[] StatusEffectPalette { get; set; }

        // 기타 스킬 팔레트
        public int[] EtcSkillPalette { get; set; }

        // TODO: 적 스프라이트 데이터 구현

        // 생성자
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
            //TODO: 적 데이터 받기 마저 구현
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    //원격 적 검색
    public void SetSearchSkill(int skillID)
    {
        enemy = SkillDatabase.GetEnemy(skillID);
    }
}
