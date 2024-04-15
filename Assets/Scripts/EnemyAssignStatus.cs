using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class EnemyAssignStatus : MonoBehaviour
{
    //적 데이터 베이스
    public GameObject enemyDB;

    public AssignEnemy enemy;

    // 할당된 적 상태 클래스
    public class AssignedEnemyStatus
    {
        // 할당 적 목록
        public static List<AssignEnemy> AssignedEnemies = new List<AssignEnemy>();

        // 적 할당을 추가하는 함수
        public static void AddEnemyAssignment(AssignEnemy assignEnemy)
        {
            AssignedEnemies.Add(assignEnemy);
        }

        // 적 할당을 초기화하는 함수
        public static void ResetEnemyAssignment()
        {
            AssignedEnemies = new List<AssignEnemy>();
        }

        // 할당 된 적의 값을 가져오는 함수
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

    // 할당 적 클래스
    public class AssignEnemy
    {
        // 배틀 할당 번호
        public int AssignNum { get; set; }



        // 적 고유번호
        public int EnemyID { get; set; }

        //적 스프라이트 이미지 ID (이미지 id는 기본적으로 enemyId에 10을 곱한것이며, 다른 모습으로 바꿀때는 거기에 수를 더한다. ID가 13인 적의 예시/ 기본: 130, 모습 1: 131, 모습 2: 132...)
        public int EnemySpriteImageID { get; set; }

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

        // 현재 HP 값
        public int CurrentHP { get; set; }

        // 현재 AP 값
        public int CurrentAP { get; set; }



        // 최대 체력 배수값
        public float MaxHPMultiplier { get; set; }

        // 베이스 AP 배수값
        public float BaseAPMultiplier { get; set; }

        // 공격력 배수값
        public float AttackMultiplier { get; set; }

        // 방어력 배수값
        public float DefenseMultiplier { get; set; }

        // 특수 공격력 배수값
        public float SpecialAttackMultiplier { get; set; }

        // 특수 방어력 배수값
        public float SpecialDefenseMultiplier { get; set; }

        // 속도 배수값
        public float SpeedMultiplier { get; set; }

        // 행운 배수값
        public float LuckMultiplier { get; set; }

        // 회피도 배수값
        public float avoidanceMultiplier { get; set; }

        // 명중률 배수값
        public float precisionMultiplier { get; set; }



        // 생성자
        public AssignEnemy(int assignNum, int enemyID, int enemyAI, int maxHP, int baseAP)
        {
            //적 기본 정보
            this.AssignNum = assignNum;
            this.EnemyID = enemyID;
            this.EnemySpriteImageID = enemyID * 10;
            this.EnemyAI = enemyAI;

            //현재 HP, AP
            this.CurrentHP = maxHP;
            this.CurrentAP = baseAP;

            //능력치 배수들 (기본값은 1)
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

    //다음 할당 번호
    int nextAssignNum = 1;

    //현재 전투의 적 할당 상태
    public int[] assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };

    void Start()
    {
        nextAssignNum = 1;
    }

    //전투 적 할당 호출 함수
    public void AddEnemyAssign(int enemyID, int enemyAI)
    {
        enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);
        AssignedEnemyStatus.AddEnemyAssignment(new AssignEnemy(nextAssignNum++, enemyID, enemyAI, enemyDB.GetComponent<EnemyData>().enemy.MaxHP, enemyDB.GetComponent<EnemyData>().enemy.BaseAP));
    }

    //할당 적 초기화 호출 함수
    public void ResetEnemyAssign()
    {
        nextAssignNum = 1;
        assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //원격 적 검색
    public void SetSearchEnemy(int enemyID)
    {
        enemy = AssignedEnemyStatus.GetAssignedEnemy(enemyID);
    }



    //TODO: EnemyFormationManager과 통합할것.
}
