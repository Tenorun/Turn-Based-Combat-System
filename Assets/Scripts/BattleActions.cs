using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
    public GameObject MenuFrame;

    private bool CheckCompletelyExpended()
    {
        if(!MenuFrame.GetComponent<MenuWaker>().changeSizeTrigger && MenuFrame.GetComponent<MenuWaker>().isExpanded)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void Attack()
    {
        if (!CheckCompletelyExpended())
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
        if (!CheckCompletelyExpended())
        {
            MenuFrame.GetComponent<MenuWaker>().SetWakeMenu(true, 1);
            Debug.Log("스킬");
        }
        else
        {
            MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            MenuFrame.GetComponent<MenuWaker>().SetWakeMenu(false, 1);
        }
    }

    public void OpenItemMenu()
    {
        if (!CheckCompletelyExpended())
        {
            MenuFrame.GetComponent<MenuWaker>().SetWakeMenu(true, 2);
            Debug.Log("아이템");
        }
        else
        {
            MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            MenuFrame.GetComponent<MenuWaker>().SetWakeMenu(false, 2);
        }
    }

    public void RunAway()
    {
        if (!CheckCompletelyExpended())
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
        if (CheckCompletelyExpended())
        {
            MenuFrame.GetComponent<MenuWaker>().SetWakeMenu(false, 4);
            Debug.Log("취소");
        }
        else
        {
            MenuFrame.GetComponent<ActionButton>().isActBtnLocked = true;
        }
    }

    public void GetActionMenuInput(int _inputNum)
    {
        switch (_inputNum)
        {
            case 0:
                Attack();
                break;
            case 1:
                OpenSkillMenu();
                break;
            case 2:
                OpenItemMenu();
                break;
            case 3:
                RunAway();
                break;
            case 4:         //취소
                Cancel();
                break;
        }
    }
}
