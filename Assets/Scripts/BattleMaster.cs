using UnityEngine;

public class BattleMaster : MonoBehaviour
{

    bool isSelectPhase = true;

    public int IDofSelectingChar;               //���� �������� ĳ������ ID

    public int curSelectOrderNum;               //���� �������� ��Ƽ���� ��°
    void Start()
    {
        IDofSelectingChar = 1;
        curSelectOrderNum = 2;
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }
}
