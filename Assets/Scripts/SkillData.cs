using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public int languageVal;         //����׿�

    public Skill skill;
    // ������ �����ͺ��̽� Ŭ����
    public class SkillDatabase
    {
        // ������ ���
        public static List<Skill> Skills = new List<Skill>();

        // �������� �߰��ϴ� �Լ�
        public static void AddSkill(Skill skill)
        {
            Skills.Add(skill);
        }

        // �������� �������� �Լ�
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

    // ��ų Ŭ����
    public class Skill
    {
        // ��ų ������ȣ
        public int SkillId { get; set; }

        // ��ų Ÿ��
        /*��ų Ÿ�� ��ȣ
          0(ũ�ν� ��� ���): Ư�� ���ݰ�
          1(�ָ� ���): ���� ���ݰ�
          2( :( ): ���� �̻�
          3(��Ʈ): ȸ����
          4(�� ���� >>): ���� ���
          5(�Ʒ� ���� >>): ���� �϶�
          6( )( ): ���� ��ȭ (������ ��°� �϶� ��� �ִ°�)
          7(���������� ����): �� �׸񿡼� ������ �ʴ°͵�
        */
        
        
        public int SkillType { get; set; }
        
        //��ų ��� SP ��
        public int SkillCost {  get; set; }

        // ��ų �̸�
        public string[] SkillName { get; set; }

        // ��ų ����
        public string[] SkillDescription { get; set; }

        // ��ų ��� �޽���(����θ� "{����� ĳ���� �̸�}(��)�� {��ų �̸�}(��)�� ����ߴ�." �� ��µ�)
        public string[] SkillUseMessage { get; set; }

        

        // ��� ��� ��
        /*��� ��� ��ȣ
            -1: ��� �Ұ�
            0: ��ü�� 1��
            1: �Ʊ��� 1��
            2: ���� 1��
            3: ����ϴ� �ڽſ��Ը�
            4: ��ü
            5: �Ʊ� ��ü
            6: �� ��ü
        */
        public int UseTarget { get; set; }

        // ������
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
        languageVal = 0;        //����׿�

        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "������ I", "Echoes I" }, 15, new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.2��", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" },6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "������ II", "Echoes II" }, 30, new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.2��.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "������ III", "Echoes III" }, 80, new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.2��", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
        SkillDatabase.AddSkill(new Skill(0, 0, new string[] { "������ IV", "Echoes IV" }, 120, new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.2��", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.2 times stronger." }, new string[] { "", "" }, 6));
    }


    //��ų ȿ��
    public void UseSkillEffect(int skillId)
    {
        skill = SkillDatabase.GetSkill(skillId);

        switch (skillId)
        {
            case 1:
                Debug.Log($"{skill.SkillName[languageVal]}��(��) �����");
                break;
            case 2:
                Debug.Log($"{skill.SkillName[languageVal]}��(��) �����");
                break;
            case 3:
                Debug.Log($"{skill.SkillName[languageVal]}��(��) �����");
                break;
            case 4:
                Debug.Log($"{skill.SkillName[languageVal]}��(��) �����");
                break;
            case 5:
                Debug.Log($"{skill.SkillName[languageVal]}��(��) �����");
                break;
            default:
                Debug.LogError($"��ų ������ȣ {skillId}�� �ش��ϴ� �������� �������� �ʽ��ϴ�.");
                break;
        }
    }

    //���� ������ �˻�
    public void RemoteSearchSkill(int skillID)
    {
        skill = SkillDatabase.GetSkill(skillID);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
