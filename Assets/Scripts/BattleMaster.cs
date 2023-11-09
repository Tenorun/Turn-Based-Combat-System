using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BattleMaster : MonoBehaviour
{

    bool isSelectPhase = true;
    int charSelecting;

    AllyCharacter[] Character = new AllyCharacter[2];

    public class AllyCharacter
    {
        string name;

        int maxHP;
        int currentHP;
        int maxSP;
        int currentSP;
        int offence;
        int defence;
        int speed;
        int luck;

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

        public void Attack()
        {
            Debug.Log("����");
        }
        public void OpenSkillMenu()
        {
            Debug.Log("��ų");
        }
        public void OpenItemMenu()
        {
            Debug.Log("������");
        }
        public void RunAway()
        {
            Debug.Log("����");
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
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Character[0] = new AllyCharacter();
        Character[1] = new AllyCharacter();

        Character[0].GetCharacterStatus("���� ȭ��ƮȦ",100, 100, 50, 50, 10, 13, 11, 5);
        Character[1].GetCharacterStatus("�ִ� �����",100, 100, 50, 50, 10, 13, 11, 5);

        charSelecting = 1;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
