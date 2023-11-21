using UnityEngine;

public class BattleMaster : MonoBehaviour
{

    bool isSelectPhase = true;
    int charSelecting;

    AllyCharacter[] Character = new AllyCharacter[2];


    public class AllyCharacter
    {
        GameObject MenuFrame = GameObject.Find("Menu Frame");
        
        string name;

        int maxHP;
        int currentHP;
        int maxSP;
        int currentSP;
        int offence;
        int defence;
        int speed;
        int luck;

        int[] items = new int[8];

        public void GetCharacterStatus(string name, int maxHP, int currentHP, int maxSP, int currentSP, int offence, int defence, int speed, int luck)
        {
            this.name = name;

            this.maxHP = maxHP;
            this.currentHP = currentHP;
            this.maxSP = maxSP;
            this.currentSP = currentSP;
            this.offence = offence;
            this.defence = defence;
            this.speed = speed;
            this.luck = luck;
        }


        //액션 버튼 조작
        public void Attack()
        {
            if (!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger && !MenuFrame.GetComponent<ChangeMenuSize>().isExpanded)
            {
                Debug.Log("공격");
            }
            else
            {
                MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            }
        }
        public void OpenSkillMenu()
        {
            if (!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger && !MenuFrame.GetComponent<ChangeMenuSize>().isExpanded)
            {
                MenuFrame.GetComponent<ChangeMenuSize>().changeSize(true);
                Debug.Log("스킬");
            }
            else
            {
                MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            }
        }
        public void OpenItemMenu()
        {
            if (!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger && !MenuFrame.GetComponent<ChangeMenuSize>().isExpanded)
            {
                MenuFrame.GetComponent<ChangeMenuSize>().changeSize(true);
                Debug.Log("아이템");
            }
            else
            {
                MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
                MenuFrame.GetComponent<ItemMenuDisplay>().DisplayItemMenu(false);
            }
        }
        public void RunAway()
        {
            if (!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger && !MenuFrame.GetComponent<ChangeMenuSize>().isExpanded)
            {
                Debug.Log("도망");
            }
            else
            {
                MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            }
        }
        public void Cancel()
        {
            if (!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger)
            {
                MenuFrame.GetComponent<ChangeMenuSize>().changeSize(false);
                MenuFrame.GetComponent<ItemMenuDisplay>().DisplayItemMenu(false);
                Debug.Log("취소");
            }
            else
            {
                MenuFrame.GetComponent<ActionButton>().isActBtnLocked = true;
            }
        }
    }

    class Enemy
    {
        int maxHP;
        int currentHP;
        int offence;
        int defence;
        int speed;
        int luck;
    }

    public void GetActionMenuInput(int _inputNum)
    {
        switch (_inputNum)
        {
            case 0:
                Character[charSelecting].Attack();
                break;
            case 1:
                Character[charSelecting].OpenSkillMenu();
                break;
            case 2:
                Character[charSelecting].OpenItemMenu();
                break;
            case 3:
                Character[charSelecting].RunAway();
                break;
            case 4:         //취소
                Character[charSelecting].Cancel();
                break;
        }
    }

    // Start is called before the first frame update
    void Start()
    {

        Character[0] = new AllyCharacter();
        Character[1] = new AllyCharacter();

        Character[0].GetCharacterStatus("벤드 화이트홀",100, 100, 50, 50, 10, 13, 11, 5);
        Character[1].GetCharacterStatus("애니 에놀라",100, 100, 50, 50, 10, 13, 11, 5);

        charSelecting = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
