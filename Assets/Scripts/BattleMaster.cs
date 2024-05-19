using UnityEngine;

public class BattleMaster : MonoBehaviour
{
    public GameObject[] CharStatusWindows;                  //ĳ���� ����â
    public GameObject PlayerCharDB;                         //�÷��̾� ĳ���� DB
    public GameObject ItemDB;                               //������ DB
    public GameObject SkillDB;                              //��ų DB

    public int languageVal = 0;                             //��� ��

    public bool isEscapable;
    
    bool isSelectPhase;

    public int IDofSelectingChar;                           //���� �������� ĳ������ ID
    public int[] assignedChar = new int[2];                 //���� ������ ������ ĳ������ ID ���

    public int curSelectOrderNum;                           //���� �������� ��Ƽ���� ��°
    public int[,] playerAction = new int[2,5];              //���õ� �÷��̾� �ൿ
    

    public void StartBattle(int[] assignedChar,int[] assignedEnemy, int languageVal, bool isEscapable)
    {
        this.assignedChar[0] = assignedChar[0];
        this.assignedChar[1] = assignedChar[1];
        this.languageVal = languageVal;
        this.isEscapable = isEscapable;

        curSelectOrderNum = 0;
        IDofSelectingChar = assignedChar[curSelectOrderNum];
        isSelectPhase = true;

        //����â ��� �Ҵ�
        CharStatusWindows[0].GetComponent<CharacterStatusDisplay>().characterID = assignedChar[0];
        CharStatusWindows[1].GetComponent<CharacterStatusDisplay>().characterID = assignedChar[1];
    }

    public void SubmitAction(int charID, int actionType, int actionID, int targetType)
    {
        playerAction[curSelectOrderNum, 0] = charID;
        playerAction[curSelectOrderNum, 1] = actionType;
        playerAction[curSelectOrderNum, 2] = actionID;
        playerAction[curSelectOrderNum, 3] = targetType;


        //��: Ÿ�� ������ �ʿ� ���°���
        //�� Ÿ���� Ư�� ������ ��ü �϶�
        if (targetType == 4 || targetType == 5 || targetType == 6)
        {
            SubmitTarget(-1);
        }
        //�� Ÿ���� �ƹ��� ������
        else if (targetType == -1)
        {
            SubmitTarget(-2);
        }
        //�� Ÿ���� �ڱ� �ڽſ��Ը� �����Ҷ�
        else if (targetType == 3)
        {
            SubmitTarget(curSelectOrderNum);
        }
        //Ÿ�� ������ �ʿ��� ���
        else
        {
            //TODO: Ÿ�� �������� �ѱ��
        }

        //TODO: �Ʒ��� ���� �ѱ�⸦ Ÿ�� �������� �ű��
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
        StartBattle(new int[2] {1,2},new int[] {1}, 0, true);       //�׽�Ʈ��
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
