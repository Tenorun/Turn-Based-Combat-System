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

        public int[] items = new int[25];

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

    //페이지 크기 받기
    public int GetPageSize()
    {
        int itemAmount = Character[charSelecting].items.Length;

        if (itemAmount % 5 == 0)
        {
            return itemAmount / 5;
        }
        else
        {
            return itemAmount / 5 + 1;
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
