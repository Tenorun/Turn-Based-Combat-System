using UnityEngine;

public class BattleMaster : MonoBehaviour
{

    bool isSelectPhase = true;

    public int IDofSelectingChar;               //현재 선택중인 캐릭터의 ID

    public int curSelectOrderNum;               //현재 선택중인 파티원의 번째
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
