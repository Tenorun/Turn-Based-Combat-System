using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleActions : MonoBehaviour
{
    public GameObject MenuFrame;

    private bool CheckCompletelyExpended()
    {
        if(!MenuFrame.GetComponent<ChangeMenuSize>().changeSizeTrigger && MenuFrame.GetComponent<ChangeMenuSize>().isExpanded)
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
            Debug.Log("����");
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
            MenuFrame.GetComponent<ChangeMenuSize>().changeSize(true);
            Debug.Log("��ų");
        }
        else
        {
            MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
        }
    }

    public void OpenItemMenu()
    {
        if (!CheckCompletelyExpended())
        {
            MenuFrame.GetComponent<ChangeMenuSize>().changeSize(true);
            Debug.Log("������");
        }
        else
        {
            MenuFrame.GetComponent<ActionButton>().isActBtnLocked = false;
            MenuFrame.GetComponent<ItemMenuDisplay>().DisplayItemMenu(false);
        }
    }

    public void RunAway()
    {
        if (!CheckCompletelyExpended())
        {
            Debug.Log("����");
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
            Debug.Log("���");
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
            case 4:         //���
                Cancel();
                break;
        }
    }
}
