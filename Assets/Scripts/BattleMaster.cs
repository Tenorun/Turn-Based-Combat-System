using UnityEngine;

public class BattleMaster : MonoBehaviour
{
    public GameObject[] CharStatusWindows;                  //캐릭터 상태창
    public GameObject PlayerCharDB;                         //플레이어 캐릭터 DB
    public GameObject ItemDB;                               //아이템 DB
    public GameObject SkillDB;                              //스킬 DB

    public int languageVal = 0;                             //언어 값

    public bool isEscapable;
    
    bool isSelectPhase;

    public int IDofSelectingChar;                           //현재 선택중인 캐릭터의 ID
    public int[] assignedChar = new int[2];                 //현재 전투에 배정된 캐릭터의 ID 목록

    public int curSelectOrderNum;                           //현재 선택중인 파티원의 번째
    public int[,] playerAction = new int[2,5];              //선택된 플레이어 행동
    

    public void StartBattle(int[] assignedChar,int[] assignedEnemy, int languageVal, bool isEscapable)
    {
        this.assignedChar[0] = assignedChar[0];
        this.assignedChar[1] = assignedChar[1];
        this.languageVal = languageVal;
        this.isEscapable = isEscapable;

        curSelectOrderNum = 0;
        IDofSelectingChar = assignedChar[curSelectOrderNum];
        isSelectPhase = true;

        //상태창 대상 할당
        CharStatusWindows[0].GetComponent<CharacterStatusDisplay>().characterID = assignedChar[0];
        CharStatusWindows[1].GetComponent<CharacterStatusDisplay>().characterID = assignedChar[1];
    }

    public void SubmitAction(int charID, int actionType, int actionID, int targetType)
    {
        playerAction[curSelectOrderNum, 0] = charID;
        playerAction[curSelectOrderNum, 1] = actionType;
        playerAction[curSelectOrderNum, 2] = actionID;
        playerAction[curSelectOrderNum, 3] = targetType;


        //☆: 타겟 선택이 필요 없는경우들
        //☆ 타겟이 특정 집단의 전체 일때
        if (targetType == 4 || targetType == 5 || targetType == 6)
        {
            SubmitTarget(-1);
        }
        //☆ 타겟이 아무도 없을때
        else if (targetType == -1)
        {
            SubmitTarget(-2);
        }
        //☆ 타겟이 자기 자신에게만 가능할때
        else if (targetType == 3)
        {
            SubmitTarget(curSelectOrderNum);
        }
        //타겟 선택이 필요한 경우
        else
        {
            //TODO: 타겟 선택으로 넘기기
        }

        //TODO: 아래의 순서 넘기기를 타겟 선택으로 옮기기
        if (curSelectOrderNum < 1)
        {
            curSelectOrderNum++;
            IDofSelectingChar = assignedChar[curSelectOrderNum];
        }
        else
        {
            curSelectOrderNum = 0;
            IDofSelectingChar = assignedChar[curSelectOrderNum];
            isSelectPhase = false;
        }
    }

    public void SubmitTarget(int targetAssignNum)
    {
        playerAction[curSelectOrderNum, 4] = targetAssignNum;
    }

    void Start()
    {
        StartBattle(new int[2] {1,2},new int[] {1}, 0, true);       //테스트용
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
