using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int languageVal;         //디버그용

    public Skill skill;
    // 아이템 데이터베이스 클래스
    public class SkillDatabase
    {
        // 아이템 목록
        public static List<Skill> Skills = new List<Skill>();

        // 아이템을 추가하는 함수
        public static void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }

        // 아이템을 가져오는 함수
        public static Skill GetSkill(int skillId)
        {
            foreach (Skill skill in Skills)
            {
                if (skill.SkillId == skillId)
                {
                    return skill;
                }
            }

            return null;
        }
    }

    // 스킬 클래스
    public class Skill
    {
        // 스킬 고유번호
        public int SkillId { get; set; }

        // 스킬 타입
        /*스킬 타입 번호
          0(크로스 헤어 모양): 특수 공격계
          1(주먹 모양): 물리 공격계
          2( :( ): 상태 이상
          3(하트): 회복계
          4(위 방향 >>): 스텟 상승
          5(아래 방향 >>): 스텟 하락
          6( )( ): 스텟 변화 (스텟의 상승과 하락 모두 있는것)
          7(기하학적인 문양): 위 항목에서 속하지 않는것들
        */
        
        
        public int SkillType { get; set; }
        
        //스킬 사용 SP 값
        public int SkillCost {  get; set; }

        // 스킬 이름
        public string[] SkillName { get; set; }

        // 스킬 설명문
        public string[] SkillDescription { get; set; }

        // 스킬 사용 메시지(비워두면 "{사용한 캐릭터 이름}(은)는 {스킬 이름}(을)를 사용했다." 로 출력됨)
        public string[] SkillUseMessage { get; set; }

        

        // 사용 대상 값
        /*사용 대상 번호
            -1: 사용 불가
            0: 전체중 1명
            1: 아군중 1명
            2: 적중 1명
            3: 사용하는 자신에게만
            4: 전체
            5: 아군 전체
            6: 적 전체
        */
        public int UseTarget { get; set; }

        // 생성자
        public Skill(int skillId, int skillType, string[] skillName, int skillCost, string[] skillDescription, string[] skillUseMessage, int useTarget)
        {
            this.SkillId = skillId;
            this.SkillType = skillType;
            this.SkillName = skillName;
            this.SkillCost = skillCost;
            this.SkillDescription = skillDescription;
            this.SkillUseMessage = skillUseMessage;
            this.UseTarget = useTarget;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        languageVal = 0;        //디버그용

        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "에코즈 I", "Echoes I" }, 15, new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.2배", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" },6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "에코즈 II", "Echoes II" }, 30, new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.2배.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "에코즈 III", "Echoes III" }, 80, new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.2배", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "에코즈 IV", "Echoes IV" }, 120, new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.2배", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
    }


    //스킬 효과
    public void UseSkillEffect(int skillId)
    {
        skill = SkillDatabase.GetSkill(skillId);

        switch (skillId)
        {
            case 1:
                Debug.Log($"{skill.SkillName[languageVal]}을(를) 사용함");
                break;
            case 2:
                Debug.Log($"{skill.SkillName[languageVal]}을(를) 사용함");
                break;
            case 3:
                Debug.Log($"{skill.SkillName[languageVal]}을(를) 사용함");
                break;
            case 4:
                Debug.Log($"{skill.SkillName[languageVal]}을(를) 사용함");
                break;
            case 5:
                Debug.Log($"{skill.SkillName[languageVal]}을(를) 사용함");
                break;
            default:
                Debug.LogError($"스킬 고유번호 {skillId}에 해당하는 아이템은 존재하지 않습니다.");
                break;
        }
    }

    //원격 아이템 검색
    public void RemoteSearchSkill(int skillID)
    {
        skill = SkillDatabase.GetSkill(skillID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
