using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BattleEnemyManager : MonoBehaviour
{
    //
    //<적 할당 상태 관리>
    //

    //다음 할당 번호
    int nextAssignNum = 1;

    //현재 전투의 적 할당 상태
    public int[] enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };

    int enemyAmount = 0;                                    //적 개수


    //적 데이터 베이스
    public GameObject enemyDB;

    public AssignEnemy resultEnemyStatus;

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
        public AssignEnemy(int assignNum, int enemyID, int maxHP, int baseAP)
        {
            //적 기본 정보
            this.AssignNum = assignNum;
            this.EnemyID = enemyID;
            this.EnemySpriteImageID = enemyID * 10;

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

    public bool keepEnemyMiddle;                 //누군가를 가운데에 고정할지 여부 
    int enemyToKeepMiddle;                         //가운데에 고정할 적의 전투 번호 (예시: 보스) *0: 없음
                                                   //추가 설명: 누군가를 가운데 배열 하면 formationType는 누군가를 가운데 배치 할 수 있는 홀수가 되며, 그 가운데는 무조건 가운데에 배열하기로 한 적으로 배열된다.

    //
    //할당 적 정보 업데이트
    //

    public void UpdateAssignInfo()
    {

        //적 개수 세기
        enemyAmount = 0;
        for(int i = 0; i < 5; i++)
        {
            if (enemyPlaces[i] != 0) enemyAmount++;
        }

        //누가 가운데 고정된 경우
        //TODO: 누가 가운데 고정된 경우의 고정된 적이 포메이션 정보 중앙에 오도록 하는 코드를 작성

        //가운데 고정이 되어 있으며, 그 고정된 적이 정위치에 있지 않을 때
        if (keepEnemyMiddle && enemyPlaces[2] != enemyToKeepMiddle)
        {

            //현재 가운데 고정 적 제외 적들
            int[] nonMidEnemies = new int[4] { 0, 0, 0, 0 };

            int nonMidEnemyAmount = 0;
            

            for(int i = 0; i < 5; i++)
            {
                if (enemyPlaces[i] != 0 && enemyPlaces[i] != enemyToKeepMiddle)
                {
                    nonMidEnemies[nonMidEnemyAmount++] = enemyPlaces[i];
                }
            }

            //할당 적 데이터 초기화
            enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };

            //가운데 고정할 적 할당
            enemyPlaces[2] = enemyToKeepMiddle;

            //nonMidEnemies들을 먼저 넣은 요소부터 1,3,0,4번 자리순서로 배정한다. 
            if (nonMidEnemyAmount >= 1) enemyPlaces[1] = nonMidEnemies[0];
            if (nonMidEnemyAmount >= 2) enemyPlaces[3] = nonMidEnemies[1];
            if (nonMidEnemyAmount >= 3) enemyPlaces[0] = nonMidEnemies[2];
            if (nonMidEnemyAmount >= 4) enemyPlaces[4] = nonMidEnemies[3];

        }
    }

    //전투 적 할당 호출 함수
    public void AddEnemyAssign(int enemyID)
    {

        //첫 빈자리 위치 번호 구하기
        int targetPlaceLocation = findEnemyLocation(0);

        if (targetPlaceLocation != -1) //빈자리가 있음
        {

            //가운데 고정된 적이 있다면 1,3,0,4 우선 순위로 자리 추가하기.
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

            //적 할당 배열에 적 할당 번호 정보 추가
            enemyPlaces[targetPlaceLocation] = nextAssignNum;

            //추가 하려는 적의 정보 검색
            enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);

            //할당 적 상태 추가하기
            AssignedEnemyStatus.AddEnemyAssignment(
                new AssignEnemy(nextAssignNum++, enemyID,
                enemyDB.GetComponent<EnemyData>().resultEnemyData.MaxHP,
                enemyDB.GetComponent<EnemyData>().resultEnemyData.BaseAP));
            
            //대열 형식 번호가 4보다 작고, 현재 대열 형식에서 가능한 적 개수를 초과 할 때 대열 형식 수정
            if(formationTypeNum < 4 && formationTypeNum <= enemyAmount - 1)
            {
                ++formationTypeNum;
            } 
        }
        else //빈자리가 없음
        {
            Debug.LogError("적을 더이상 추가 할 수 없습니다.");
        }

        //적 표시기 업데이트
        UpdateDisplayerCodeSet();
    }

    //할당 적 제거 함수
    public void RemoveEnemyAssign(int targetAssignNum)
    {
        //제거 목표 위치 번호 구하기
        int targetLocation = findEnemyLocation(targetAssignNum);

        if(targetLocation != -1) //제거하려는 적이 있음
        {
            enemyPlaces[targetLocation] = 0;

            //만약 타겟이 가운데에 고정된 적 이라면
            if(targetAssignNum == enemyToKeepMiddle)
            {
                enemyToKeepMiddle = 0;
                keepEnemyMiddle = false;
            }
        }

        //적 표시기 업데이트
        UpdateDisplayerCodeSet();
    }

    //할당 적 초기화 호출 함수
    public void ResetEnemyAssign()
    {
        formationTypeNum = -1;
        enemyAmount = 0;
        nextAssignNum = 1;
        enemyPlaces = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //원격 적 검색
    public void SetSearchEnemyAssign(int targetAssignNum)
    {
        resultEnemyStatus = AssignedEnemyStatus.GetAssignedEnemy(targetAssignNum);
    }

    //
    //<적 표시 상태 관리>
    //

    public GameObject[] enemyDisplayObject = new GameObject[5];     //적 표시 오브젝트

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 450, 450, 450, 450 }, { -90, 90, 450, 450, 450 }, { -180, 0, 180, 450, 450 }, { -225, -75, 75, 225, 450 }, { -252, -126, 0, 126, 252 } };
    //적 배치 X 좌표

    public int formationTypeNum = -1;                                   //대열 형식
    /*대열 형식
     0: 1개 장소 배열     ■
     1: 2개 장소 배열    ■ ■
     2: 3개 장소 배열   ■ ■ ■
     3: 4개 장소 배열  ■ ■ ■ ■
     4: 5개 장소 배열 ■ ■ ■ ■ ■
     */


    //적 표시기 이미지 업데이트
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

                //i번째 적 표시기의 스프라이트 렌더러
                SpriteRenderer enemyDisplaySpriteRenderer = enemyDisplayObject[i].GetComponent<SpriteRenderer>();

                //i번째 적의 스프라이트 ID
                SetSearchEnemyAssign(enemyPlaces[i]);
                int targetSpriteID = resultEnemyStatus.EnemySpriteImageID;

                //i번째 적의 스프라이트 이미지
                Sprite targetSpriteData = enemyDB.GetComponent<EnemyData>().imageArray[targetSpriteID];

                //적표시기 스프라이트 변경
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

    //적 할당값, 디스플레이 이미지 업데이트, 적 움직임 코드셋
    public void UpdateDisplayerCodeSet()
    {
        UpdateAssignInfo();
        MoveEnemyToItsPlace();
        UpdateDisplayerImage();
    }


    //
    //<그 외>
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
