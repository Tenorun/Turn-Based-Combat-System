using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleEnemyManager : MonoBehaviour
{
    //
    //<적 할당 상태 관리>
    //

    //다음 할당 번호
    int nextAssignNum = 1;

    //현재 전투의 적 할당 상태
    public int[] assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };

    int enemyAmount = 0;                                    //적 개수


    //적 데이터 베이스
    public GameObject enemyDB;

    public AssignEnemy enemyStatus;

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

    public bool keepSomeOneMiddle;                 //누군가를 가운데에 고정할지 여부 
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
            if (assignedEnemies[i] != 0) enemyAmount++;
        }

        //고정된 적을 0으로 없에기
        int fixedEnemyLocation = findEnemyLocation(enemyToKeepMiddle);
        if(fixedEnemyLocation != -1)
        {
            assignedEnemies[fixedEnemyLocation] = 0;
        }

        //할당정보 배열의 0을 모두 왼쪽으로
        int index = 0;
        for(int i = 0; i < 5; i++)
        {
            if (assignedEnemies[i] != 0)
            {
                assignedEnemies[index++] = assignedEnemies[i];
            }
        }
        //남은 공간을 모두 0으로
        while (index < 5)
        {
            assignedEnemies[index++] = 0;
        }

        //formationTypeNum 정하기
        formationTypeNum = enemyAmount - 1;

        //누가 가운데 고정된 경우
        if (keepSomeOneMiddle)
        {
            //적 개수가 짝수일때, 포메이션 타입을 홀수로 하기.
            //예) 2명: 포메이션 타입 = 2(3명 배열)/4명: 포메이션 타입 = 4(5명 배열)
            if (enemyAmount % 2 == 0)
            {
                formationTypeNum = enemyAmount;
            }

            int middleLocation = formationTypeNum / 2;

            //뒤에서부터 가운데까지 하나씩 오른쪽으로 밀기.(가운데에 삽입될 적의 정보는 0으로 모두 오른쪽으로 밀렸기 때문에, 할당된것이 무엇인지 볼 필요는 없다.)
            for (int i = formationTypeNum; i > middleLocation; i--)
            {
                assignedEnemies[i] = assignedEnemies[i - 1];
            }
            //가운데에 고정할 적을 삽입
            assignedEnemies[middleLocation] = enemyToKeepMiddle;
        }
    }

    //적을 가운데 고정하기
    public void AssignMiddleEnemy(int targetPosition)
    {
        keepSomeOneMiddle = true;
        enemyToKeepMiddle = assignedEnemies[targetPosition];
    }

    //전투 적 할당 호출 함수
    public void AddEnemyAssign(int enemyID)
    {

        //첫 빈자리 위치 번호 구하기
        int emptyPlaceLocation = findEnemyLocation(0);

        if (emptyPlaceLocation != -1) //빈자리가 있음
        {
            //할당 상태의 빈자리에 적 할당 번호를 추가하기
            assignedEnemies[emptyPlaceLocation] = nextAssignNum;

            //추가 하려는 적의 정보 검색
            enemyDB.GetComponent<EnemyData>().SetSearchEnemy(enemyID);

            //할당 적 상태 추가하기
            AssignedEnemyStatus.AddEnemyAssignment(
                new AssignEnemy(nextAssignNum, enemyID,
                enemyDB.GetComponent<EnemyData>().enemy.MaxHP,
                enemyDB.GetComponent<EnemyData>().enemy.BaseAP));

            //적 할당 번호 1 진행
            ++nextAssignNum;
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
            assignedEnemies[targetLocation] = 0;

            //만약 타겟이 가운데에 고정된 적 이라면
            if(targetAssignNum == enemyToKeepMiddle)
            {
                enemyToKeepMiddle = 0;
                keepSomeOneMiddle = false;
            }
        }

        //적 표시기 업데이트
        UpdateDisplayerCodeSet();
    }

    //할당 적 초기화 호출 함수
    public void ResetEnemyAssign()
    {
        nextAssignNum = 1;
        assignedEnemies = new int[5] { 0, 0, 0, 0, 0 };
        AssignedEnemyStatus.ResetEnemyAssignment();
    }

    //원격 적 검색
    public void SetSearchEnemyAssign(int targetAssignNum)
    {
        enemyStatus = AssignedEnemyStatus.GetAssignedEnemy(targetAssignNum);
    }

    //
    //<적 표시 상태 관리>
    //

    public GameObject[] enemyDisplayObject = new GameObject[5];     //적 표시 오브젝트

    readonly int[,] enemyFormations = new int[5, 5] { { 0, 450, 450, 450, 450 }, { -90, 90, 450, 450, 450 }, { -180, 0, 180, 450, 450 }, { -225, -75, 75, 225, 450 }, { -252, -126, 0, 126, 252 } };
    //적 배치 X 좌표

    public int formationTypeNum;                                   //대열 형식
    /*대열 형식
     1: 1개 장소 배열     ■
     2: 2개 장소 배열    ■ ■
     3: 3개 장소 배열   ■ ■ ■
     4: 4개 장소 배열  ■ ■ ■ ■
     5: 5개 장소 배열 ■ ■ ■ ■ ■
     */


    //적 표시기 이미지 업데이트
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

                //i번째 적 표시기의 스프라이트 렌더러
                SpriteRenderer enemyDisplaySpriteRenderer = enemyDisplayObject[i].GetComponent<SpriteRenderer>();

                //i번째 적의 스프라이트 ID
                SetSearchEnemyAssign(assignedEnemies[i]);
                int targetSpriteID = enemyStatus.EnemySpriteImageID;

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


    //적 할당값, 디스플레이 이미지 업데이트, 적 움직임 코드셋
    public void UpdateDisplayerCodeSet()
    {
        UpdateAssignInfo();
        MoveEnemyToItsPlace();
        UpdateDisplayerImage();
    }

    //TODO: 적 선택 구현

    //
    //<그 외>
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

    //TODO: 제거할때 이미지가 우선적으로 바뀌어 서로 다른 적이 옆에 있을때 어색하게 없어지는것을 고치기
}
