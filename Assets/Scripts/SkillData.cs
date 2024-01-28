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
        public Skill(int skillId, int skillType, string[] skillName, int skillCost, string[] skillDescription, int useTarget)
        {
            this.SkillId = skillId;
            this.SkillType = skillType;
            this.SkillName = skillName;
            this.SkillCost = skillCost;
            this.SkillDescription = skillDescription;
            this.UseTarget = useTarget;
        }
    }


    // Start is called before the first frame update
    void Start()
    {
        languageVal = 0;        //디버그용

        SkillDatabase.AddSkill(new Skill(1, 0, new string[] { "에코즈 I", "Echoes I" }, 20, 
            new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞출시 데미지 1.5배.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(2, 0, new string[] { "에코즈 II", "Echoes II" }, 40, 
            new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.5배.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(3, 0, new string[] { "에코즈 III", "Echoes III" }, 80, 
            new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.5배.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(4, 0, new string[] { "에코즈 IV", "Echoes IV" }, 120, 
            new string[] { "(적 전체에 공격)\n에너지의 파동으로 적 전체를 공격한다.\n타이밍 맞추면 데미지 1.5배.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        
        SkillDatabase.AddSkill(new Skill(5, 3, new string[] { "회복A I", "Heal-A I" }, 10, 
            new string[] { "(아군 1명 HP 40 회복)\n회복의 힘으로 회복한다.\n타이밍 맞추면 HP 80 회복.", "(Heals 1 ally 40 HP)\nHeals with power of heal\nIf done perfectly, heals 80 HP" }, 1));
        SkillDatabase.AddSkill(new Skill(6, 3, new string[] { "회복A II", "Heal-A II" }, 30, 
            new string[] { "(아군 1명 HP 100 회복)\n회복의 힘으로 회복한다.\n타이밍 맞추면 HP 200 회복.", "(Heals 1 ally 100 HP)\nHeals with power of heal\nIf done perfectly, heals 200 HP" }, 1));
        SkillDatabase.AddSkill(new Skill(7, 3, new string[] { "회복A III", "Heal-A III" }, 90, 
            new string[] { "(아군 1명 HP 240 회복)\n회복의 힘으로 회복한다.\n타이밍 맞추면 HP 480 회복.", "(Heals 1 ally 240 HP)\nHeals with power of heal\nIf done perfectly, heals 480 HP" }, 1));
        
        SkillDatabase.AddSkill(new Skill(8, 3, new string[] { "회복B I", "Heal-B I" }, 20, 
            new string[] { "(아군 전체 HP 30 회복)\n치유해서 더 좋은 세상을 만든다. 너와 나를 위해서.\n타이밍 맞추면 HP 60 회복.", "(Heals all allies 30 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));
        SkillDatabase.AddSkill(new Skill(9, 3, new string[] { "회복B II", "Heal-B II" }, 55, 
            new string[] { "(아군 전체 HP 75 회복)\n치유해서 더 좋은 세상을 만든다. 너와 나를 위해서.\n타이밍 맞추면 HP 150 회복.", "(Heals all allies 75 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));
        SkillDatabase.AddSkill(new Skill(10, 3, new string[] { "회복B III", "Heal-B III" }, 140, 
            new string[] { "(아군 전체 HP 180 회복)\n치유해서 더 좋은 세상을 만든다. 너와 나를 위해서.\n타이밍 맞추면 HP 360 회복.", "(Heals all allies 30 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));

        SkillDatabase.AddSkill(new Skill(11, 3, new string[] { "리프레시", "Refresh" }, 20, 
            new string[] { "(아군 1명 상태이상 회복)\n왠지 모르게 민트향이 난다.\n타이밍 맞추면 모든 아군의 상태이상 회복.", "(Removes 1 ally's bad effects)\nSmells Like Mint Spirit\nIf done perfectly, Removes all ally's bad effects." }, 1));

        SkillDatabase.AddSkill(new Skill(12, 3, new string[] { "리바이브", "Revive" }, 120, 
            new string[] { "(아군 1명 HP 1/2로 부활)\n[여기에 이름 입력]아.\n일어서라, 어서 일어서!\n[여기에 이름 입력]아!\n 타이밍 맞추면 HP 100%로 부활.", "(Revives 1 ally with half of max HP)\nGet back, get back\nback to where you once belong\nGet back [Insert name here]!\nGo home\nIf done perfectly, heals 80 HP." }, 1));

        SkillDatabase.AddSkill(new Skill(13, 2, new string[] { "브레인노이즈 A", "BrainNoise A" }, 20, 
            new string[] { "(적 1명에 혼란 상태이상)\n신경 흐름을 꼬아...\n놓은 면발의 국수 였고 어제 저녁은, 오늘은 그래서 뭘 먹을까.\n그러니까 이럴게 아니라 방이 너무 더럽잖아!\n 아 잠깐 이럴때가 아니지. 아무튼 집중력을 흐린다.\n타이밍 맞추면 성공률 2배.", "(Gives confusion to 1 enemy)\nTwists one's neural connect...\nthose wires and\nwanna hear about a plush in a watermelon?\nwait, what was I doing? anyway it makes confusion\nIf done perfectly, chances of success are doubled."}, 2));
        SkillDatabase.AddSkill(new Skill(14, 2, new string[] { "브레인노이즈 B", "BrainNoise B" }, 65,
            new string[] { "(적 전체에 혼란 상태이상)\n……\n잠시만요 지금 생각중이에요……\n아! 신경 흐름을 꼬아 모두에게 혼란을 준다네요.\n타이밍 맞추면 성공률 2배", "(Gives confusion to all enemy)\n……\ngive me some seconds I'm thinking something……\nOh, it says it gives confusion by twisting their neural connections.\nIf done perfectly, chances of success are doubled." }, 6));

        SkillDatabase.AddSkill(new Skill(15, 4, new string[] { "아드레날린 부스트", "Adrenaline Boost" }, 15, new string[] { "(아군 1명에 적용)\n아드레날린의 양을 증가시켜 일시적으로 공격성을 올린다.\n타이밍 맞추면 아군 모두에게 적용.", "(Applies to 1 ally)\nMake more offensive temporary by increasing adrenaline amount.\nIf done perfectely, the effect applies to all allies." }, 1));
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
    public void SetSearchSkill(int skillID)
    {
        skill = SkillDatabase.GetSkill(skillID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
