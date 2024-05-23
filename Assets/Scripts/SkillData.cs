using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Tracing;
using UnityEngine;

public class SkillData : MonoBehaviour
{
    public Skill resultSkillData;
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
          8(x): �������
        */
        
        
        public int SkillType { get; set; }
        
        //��ų ��� SP ��
        public int SkillCost {  get; set; }

        // ��ų �̸�
        public string[] SkillName { get; set; }

        // ��ų ����
        public string[] SkillDescription { get; set; }    

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

        SkillDatabase.AddSkill(new Skill(0, 8, new string[] { "-�������-", "-Empty-" }, 0, new string[] { "�� ���Կ� ��ų�� �Ҵ���� �ʾҽ��ϴ�.", "Skill is not assigned in this slot." }, -1));

        SkillDatabase.AddSkill(new Skill(1, 0, new string[] { "������ I", "Echoes I" }, 20, 
            new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ����� ������ 1.5��.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(2, 0, new string[] { "������ II", "Echoes II" }, 40, 
            new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.5��.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(3, 0, new string[] { "������ III", "Echoes III" }, 80, 
            new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.5��.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        SkillDatabase.AddSkill(new Skill(4, 0, new string[] { "������ IV", "Echoes IV" }, 120, 
            new string[] { "(�� ��ü�� ����)\n�������� �ĵ����� �� ��ü�� �����Ѵ�.\nŸ�̹� ���߸� ������ 1.5��.", "(Attacks all enemy)\nAttacks all enemy with energy waves.\nIf done perfectly, 1.5 times stronger damage." }, 6));
        
        SkillDatabase.AddSkill(new Skill(5, 3, new string[] { "ȸ��A I", "Heal-A I" }, 10, 
            new string[] { "(�Ʊ� 1�� HP 40 ȸ��)\nȸ���� ������ ȸ���Ѵ�.\nŸ�̹� ���߸� HP 80 ȸ��.", "(Heals 1 ally 40 HP)\nHeals with power of heal\nIf done perfectly, heals 80 HP" }, 1));
        SkillDatabase.AddSkill(new Skill(6, 3, new string[] { "ȸ��A II", "Heal-A II" }, 30, 
            new string[] { "(�Ʊ� 1�� HP 100 ȸ��)\nȸ���� ������ ȸ���Ѵ�.\nŸ�̹� ���߸� HP 200 ȸ��.", "(Heals 1 ally 100 HP)\nHeals with power of heal\nIf done perfectly, heals 200 HP" }, 1));
        SkillDatabase.AddSkill(new Skill(7, 3, new string[] { "ȸ��A III", "Heal-A III" }, 90, 
            new string[] { "(�Ʊ� 1�� HP 240 ȸ��)\nȸ���� ������ ȸ���Ѵ�.\nŸ�̹� ���߸� HP 480 ȸ��.", "(Heals 1 ally 240 HP)\nHeals with power of heal\nIf done perfectly, heals 480 HP" }, 1));
        
        SkillDatabase.AddSkill(new Skill(8, 3, new string[] { "ȸ��B I", "Heal-B I" }, 20, 
            new string[] { "(�Ʊ� ��ü HP 30 ȸ��)\nġ���ؼ� �� ���� ������ �����. �ʿ� ���� ���ؼ�.\nŸ�̹� ���߸� HP 60 ȸ��.", "(Heals all allies 30 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));
        SkillDatabase.AddSkill(new Skill(9, 3, new string[] { "ȸ��B II", "Heal-B II" }, 55, 
            new string[] { "(�Ʊ� ��ü HP 75 ȸ��)\nġ���ؼ� �� ���� ������ �����. �ʿ� ���� ���ؼ�.\nŸ�̹� ���߸� HP 150 ȸ��.", "(Heals all allies 75 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));
        SkillDatabase.AddSkill(new Skill(10, 3, new string[] { "ȸ��B III", "Heal-B III" }, 140, 
            new string[] { "(�Ʊ� ��ü HP 180 ȸ��)\nġ���ؼ� �� ���� ������ �����. �ʿ� ���� ���ؼ�.\nŸ�̹� ���߸� HP 360 ȸ��.", "(Heals all allies 30 HP)\nIt makes a better place\nFor you and for me\nIf done perfectly, heals 60 HP." }, 5));

        SkillDatabase.AddSkill(new Skill(11, 3, new string[] { "��������", "Refresh" }, 20, 
            new string[] { "(�Ʊ� 1�� �����̻� ȸ��)\n���� �𸣰� ��Ʈ���� ����.\nŸ�̹� ���߸� ��� �Ʊ��� �����̻� ȸ��.", "(Removes 1 ally's bad effects)\nSmells Like Mint Spirit\nIf done perfectly, Removes all ally's bad effects." }, 1));

        SkillDatabase.AddSkill(new Skill(12, 3, new string[] { "�����̺�", "Revive" }, 120, 
            new string[] { "(�Ʊ� 1�� HP 1/2�� ��Ȱ)\n[���⿡ �̸� �Է�]��.\n�Ͼ��, � �Ͼ!\n[���⿡ �̸� �Է�]��!\n Ÿ�̹� ���߸� HP 100%�� ��Ȱ.", "(Revives 1 ally with half of max HP)\nGet back, get back\nback to where you once belong\nGet back [Insert name here]!\nGo home\nIf done perfectly, heals 80 HP." }, 1));

        SkillDatabase.AddSkill(new Skill(13, 2, new string[] { "�극�γ����� A", "BrainNoise A" }, 20, 
            new string[] { "(�� 1�� ȥ�� �����̻�)\n�Ű� �帧�� ����...\n���� ����� ���� ���� ���� ������, ������ �׷��� �� ������.\n�׷��ϱ� �̷��� �ƴ϶� ���� �ʹ� �����ݾ�!\n �� ��� �̷����� �ƴ���. �ƹ�ư ���߷��� �帰��.\nŸ�̹� ���߸� ������ 2��.", "(Gives confusion to 1 enemy)\nTwists one's neural connect...\nthose wires and\nwanna hear about a plush in a watermelon?\nwait, what was I doing? anyway it makes confusion\nIf done perfectly, chances of success are doubled."}, 2));
        SkillDatabase.AddSkill(new Skill(14, 2, new string[] { "�극�γ����� B", "BrainNoise B" }, 65,
            new string[] { "(�� ��ü�� ȥ�� �����̻�)\n����\n��ø��� ���� �������̿��䡦��\n��! �Ű� �帧�� ���� ��ο��� ȥ���� �شٳ׿�.\nŸ�̹� ���߸� ������ 2��", "(Gives confusion to all enemy)\n����\ngive me some seconds I'm thinking something����\nOh, it says it gives confusion by twisting their neural connections.\nIf done perfectly, chances of success are doubled." }, 6));

        SkillDatabase.AddSkill(new Skill(15, 4, new string[] { "�Ƶ巹���� �ν�Ʈ", "Adrenaline Boost" }, 15, new string[] { "(�Ʊ� 1�� ����)\n�Ƶ巹������ ���� �������� �Ͻ������� ���ݼ��� �ø���.\nŸ�̹� ���߸� �Ʊ� ��ο��� ����.", "(Applies to 1 ally)\nMake more offensive temporary by increasing adrenaline amount.\nIf done perfectely, the effect applies to all allies." }, 1));
    }


    //��ų ȿ��
    public void UseSkillEffect(int skillId)
    {
        resultSkillData = SkillDatabase.GetSkill(skillId);

        switch (skillId)
        {
            case 1:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 2:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 3:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 4:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 5:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 6:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 7:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 8:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 9:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 10:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 11:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 12:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 13:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 14:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            case 15:
                Debug.Log($"{resultSkillData.SkillName[0]}��(��) �����");
                break;
            default:
                Debug.LogError($"��ų ID: {skillId}�� �ش��ϴ� ��ų�� �������� �ʽ��ϴ�.");
                break;
        }
    }

    //���� ��ų �˻�
    public void SetSearchSkill(int skillID)
    {
        resultSkillData = SkillDatabase.GetSkill(skillID);
    }
}
